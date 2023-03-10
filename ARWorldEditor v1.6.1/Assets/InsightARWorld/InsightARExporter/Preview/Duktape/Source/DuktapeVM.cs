using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace Duktape
{
    using System.Runtime.InteropServices;
    using UnityEngine;
    using duk_ret_t = System.Int32;

    public struct UnrefAction
    {
        public uint refid;
        public object target;
        public Action<IntPtr, uint, object> action;
    }

    public class DuktapeVM // : Scripting.ScriptEngine
    {
        // duktape-unity 版本, 生成规则发生无法兼容的改变时增加版本号
        public const int VERSION = 0x10001;
        // public const string HEAP_STASH_PROPS_REGISTRY = "registry";
        // duk_add_event 生成的 object 中存储 this 的属性名
        // public static readonly string EVENT_PROP_THIS = DuktapeDLL.DUK_HIDDEN_SYMBOL("this");
        // 在jsobject实例上记录关联的本地对象 object cache refid
        // public static readonly string OBJ_PROP_NATIVE = DuktapeDLL.DUK_HIDDEN_SYMBOL("1");
        // public static readonly string OBJ_PROP_NATIVE_WEAK = DuktapeDLL.DUK_HIDDEN_SYMBOL("2");
        // public static readonly string OBJ_PROP_TYPE = DuktapeDLL.DUK_HIDDEN_SYMBOL("3");
        // 导出类的js构造函数隐藏属性, 记录在vm中的注册id
        // public static readonly string OBJ_PROP_EXPORTED_REFID = DuktapeDLL.DUK_HIDDEN_SYMBOL("4");
        // public static readonly string OBJ_PROP_SPECIAL_REFID = DuktapeDLL.DUK_HIDDEN_SYMBOL("special-refid");

        public const string _DuktapeDelegates = "_DuktapeDelegates";

        public const string SPECIAL_ENUM = "Enum";
        public const string SPECIAL_DELEGATE = "Delegate";
        public const string SPECIAL_ARRAY = "Array";
        public const string SPECIAL_CSHARP = "CSharp";

        private static DuktapeVM _instance;
        private uint _updateTimer;
        private DuktapeContext _ctx;
        private IFileResolver _fileResolver;
        private IO.ByteBufferAllocator _byteBufferAllocator;
        private uint _memAllocPoolSize;
        private IntPtr _memAllocPool; // 底层预分配内存
        private ObjectCache _objectCache = new ObjectCache();
        private Queue<UnrefAction> _unrefActions = new Queue<UnrefAction>();

        // 已经导出的本地类
        private Dictionary<Type, DuktapeFunction> _exported = new Dictionary<Type, DuktapeFunction>();
        private Dictionary<Type, int> _exportedTypeIndexer = new Dictionary<Type, int>();
        private List<Type> _exportedTypes = new List<Type>(); // 可用 索引 反查 Type

        private Dictionary<string, DuktapeFunction> _specialTypes = new Dictionary<string, DuktapeFunction>(); // 从 refid 反查 Type

        private Dictionary<Type, MethodInfo> _delegates = new Dictionary<Type, MethodInfo>(); // 委托对应的 duktape 绑定函数

        // private static int _thread = 0;

        private static Dictionary<IntPtr, DuktapeContext> _contexts = new Dictionary<IntPtr, DuktapeContext>();
        private static IntPtr _lastContextPtr;
        private static DuktapeContext _lastContext;

        public DuktapeContext context
        {
            get { return _ctx; }
        }

        public IntPtr ctx
        {
            get { return _ctx != null ? _ctx.rawValue : IntPtr.Zero; }
        }

        public IFileResolver fileResolver
        {
            get { return _fileResolver; }
        }

        // poolSize: 预分配内存
        public DuktapeVM(IO.ByteBufferAllocator allocator = null, int poolSize = 0)
        {
            _instance = this;
            _byteBufferAllocator = allocator;
            _memAllocPoolSize = poolSize >= 0 ? (uint) poolSize : 0;
            _memAllocPool = _memAllocPoolSize != 0 ? Marshal.AllocHGlobal(poolSize) : IntPtr.Zero;
            var ctx = DuktapeDLL.duk_unity_create_heap(_memAllocPool, _memAllocPoolSize);

            _ctx = new DuktapeContext(this, ctx);
            DuktapeDLL.duk_unity_open(ctx);
            DuktapeAux.duk_open(ctx);
            DuktapeVM.duk_open_module(ctx);
        }

        public IO.ByteBufferAllocator GetByteBufferAllocator()
        {
            return _byteBufferAllocator;
        }

        public void GetMemoryState(out uint count, out uint size, out uint poolSize)
        {
            poolSize = _memAllocPoolSize;
            DuktapeDLL.duk_unity_get_memory_state(ctx, out count, out size);
        }

        public static DuktapeVM GetInstance()
        {
            return _instance;
        }

        public static void addContext(DuktapeContext context)
        {
            var ctx = context.rawValue;
            if (ctx != IntPtr.Zero)
            {
                _contexts[ctx] = context;
                _lastContext = context;
                _lastContextPtr = ctx;
            }
        }

        public static void removeContext(DuktapeContext context)
        {
            var ctx = context.rawValue;
            if (ctx != IntPtr.Zero)
            {
                _contexts.Remove(ctx);
                if (_lastContext == context)
                {
                    _lastContext = null;
                    _lastContextPtr = IntPtr.Zero;
                }
            }
        }

        public static DuktapeVM GetVM(IntPtr ctx)
        {
            return GetContext(ctx)?.vm;
        }

        public static ObjectCache GetObjectCache(IntPtr ctx)
        {
            return GetContext(ctx)?.vm?._objectCache;
        }

        public ObjectCache GetObjectCache()
        {
            return _objectCache;
        }

        public static DuktapeContext GetContext(IntPtr ctx)
        {
            if (_lastContextPtr == ctx)
            {
                return _lastContext;
            }
            return TryGetContext(ctx);
        }

        private static DuktapeContext TryGetContext(IntPtr ctx)
        {
            DuktapeContext context;
            if (_contexts.TryGetValue(ctx, out context))
            {
                _lastContext = context;
                _lastContextPtr = ctx;
                return context;
            }
            // fixme 如果是 thread 则获取对应 main context
            // DuktapeDLL.duk_push_current_thread(ctx);
            // var parent = DuktapeDLL.duk_get_parent_context(ctx, -1);
            // DuktapeDLL.duk_pop(ctx);
            // return parent != ctx ? TryGetContext(parent) : null;
            return null;
        }

        public void AddSearchPath(string path)
        {
            _fileResolver.AddSearchPath(path);
        }

        public DuktapeFunction GetSpecial(string name)
        {
            DuktapeFunction val;
            if (_specialTypes.TryGetValue(name, out val))
            {
                return val;
            }
            return null;
        }

        public uint AddSpecial(string name, DuktapeFunction val)
        {
            // Debug.LogFormat("Add Special {0} {1}", name, val.rawValue);
            var refid = val.rawValue;
            _specialTypes[name] = val;
            return refid;
        }

        public void AddDelegate(Type type, MethodInfo method)
        {
            _delegates[type] = method;
            // Debug.LogFormat("Add Delegate {0} {1}", type, method);
        }

        public Delegate CreateDelegate(Type type, DuktapeDelegate fn)
        {
            MethodInfo method;
            if (_delegates.TryGetValue(type, out method))
            {
                var target = Delegate.CreateDelegate(type, fn, method, true);
                fn.target = target;
                return target;
            }
            return null;
        }

        public int AddExportedType(Type type, DuktapeFunction fn)
        {
            _exported.Add(type, fn);
            var index = _exportedTypes.Count;
            _exportedTypes.Add(type);
            _exportedTypeIndexer[type] = index;
            // Debug.Log($"add export: {type}");
            return index;
        }

        public int GetExportedTypeCount()
        {
            return _exportedTypes.Count;
        }

        public Type GetExportedType(int index)
        {
            return index >= 0 && index < _exportedTypes.Count ? _exportedTypes[index] : null;
        }

        public bool TryGetExportedType(Type type, out int index)
        {
            return _exportedTypeIndexer.TryGetValue(type, out index);
        }

        // 得到注册在js中的类型对应的构造函数
        public DuktapeFunction GetExported(Type type)
        {
            DuktapeFunction value;
            return _exported.TryGetValue(type, out value) ? value : null;
        }

        public void GC(uint refid, object target, Action<IntPtr, uint, object> op)
        {
            var act = new UnrefAction()
            {
                refid = refid,
                action = op,
                target = target,
            };
            lock (_unrefActions)
            {
                _unrefActions.Enqueue(act);
            }
        }

        private void OnUpdate()
        {
            var ctx = _ctx.rawValue;
            lock (_unrefActions)
            {
                while (true)
                {
                    var count = _unrefActions.Count;
                    if (count == 0)
                    {
                        break;
                    }
                    var act = _unrefActions.Dequeue();
                    act.action(ctx, act.refid, act.target);
                    // Debug.LogFormat("duktape gc {0}", act.refid);
                }
            }
            if (_byteBufferAllocator != null)
            {
                _byteBufferAllocator.Drain();
            }
        }

        public static void duk_open_module(IntPtr ctx)
        {
            DuktapeDLL.duk_push_object(ctx);
            DuktapeDLL.duk_push_c_function(ctx, cb_resolve_module, DuktapeDLL.DUK_VARARGS);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "resolve");
            DuktapeDLL.duk_push_c_function(ctx, cb_load_module, DuktapeDLL.DUK_VARARGS);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "load");
            DuktapeDLL.duk_module_node_init(ctx);
        }

        public static string EnsureExtension(string filename)
        {
            return filename != null && filename.EndsWith(".js") ? filename : filename + ".js";
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        private static duk_ret_t cb_resolve_module(IntPtr ctx)
        {
            var module_id = EnsureExtension(DuktapeAux.duk_require_string(ctx, 0));
            var parent_id = DuktapeAux.duk_require_string(ctx, 1);
            var resolve_to = module_id;
            // Debug.LogFormat("cb_resolve_module module_id:'{0}', parent_id:'{1}'\n", module_id, parent_id);

            if (module_id.StartsWith("./") || module_id.StartsWith("../") || module_id.Contains("/./") || module_id.Contains("/../"))
            {
                // 显式相对路径直接从 parent 模块路径拼接
                var parent_path = PathUtils.GetDirectoryName(parent_id);
                try
                {
                    resolve_to = PathUtils.ExtractPath(PathUtils.Combine(parent_path, module_id), '/');
                }
                catch
                {
                    // 不能提升到源代码目录外面
                    return DuktapeDLL.duk_type_error(ctx, string.Format("invalid module path (out of sourceRoot): {0}", module_id));
                }
            }
            // Debug.LogFormat("resolve_cb(1): id:{0}', parent-id:'{1}', resolve-to:'{2}'", module_id, parent_id, resolve_to);
            // if (GetVM(ctx).ResolvePath(resolve_to) == null)
            // {
            //     DuktapeDLL.duk_type_error(ctx, "cannot find module: %s", module_id);
            //     return 1;
            // }

            if (resolve_to != null)
            {
                DuktapeDLL.duk_push_string(ctx, resolve_to);
                return 1;
            }
            return DuktapeDLL.duk_type_error(ctx, string.Format("cannot find module: {0}", module_id));
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        private static duk_ret_t cb_load_module(IntPtr ctx)
        {
            var module_id = DuktapeAux.duk_require_string(ctx, 0);
            DuktapeDLL.duk_get_prop_string(ctx, 2, "filename");
            var filename = DuktapeAux.duk_require_string(ctx, -1);
            var source = GetVM(ctx)._fileResolver.ReadAllBytes(filename);
            // Debug.LogFormat("cb_load_module module_id:'{0}', filename:'{1}', resolved:'{2}'\n", module_id, filename, resolvedPath);
            if (source != null && source.Length > 0) // bytecode is unsupported
            {
                if (source[0] != 0xbf)
                {
                    DuktapeDLL.duk_unity_push_lstring(ctx, source, (uint)source.Length);
                    return 1;
                }
                return DuktapeDLL.duk_type_error(ctx, string.Format("cannot load module (bytecode): {0}", module_id));
            }
            return DuktapeDLL.duk_type_error(ctx, string.Format("cannot load module: {0}", module_id));
        }

        private IEnumerator _InitializeStep(IDuktapeListener listener, int step)
        {
            DuktapeDLL.duk_push_global_object(ctx);
            DuktapeJSBuiltins.reg(ctx);
            listener?.OnTypesBinding(this);
            var ctxAsArgs = new object[] { ctx };
            var bindingTypes = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int assemblyIndex = 0, assemblyCount = assemblies.Length; assemblyIndex < assemblyCount; assemblyIndex++)
            {
                var assembly = assemblies[assemblyIndex];
                try
                {
                    if (assembly.IsDynamic)
                    {
                        continue;
                    }
                    var exportedTypes = assembly.GetExportedTypes();
                    for (int i = 0, size = exportedTypes.Length; i < size; i++)
                    {
                        var type = exportedTypes[i];
#if UNITY_EDITOR
                        if (type.IsDefined(typeof(JSAutoRunAttribute), false))
                        {
                            try
                            {
                                var run = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
                                if (run != null)
                                {
                                    run.Invoke(null, null);
                                }
                            }
                            catch (Exception exception)
                            {
                                Debug.LogWarning($"JSAutoRun failed: {exception}");
                            }
                            continue;
                        }
#endif
                        var attributes = type.GetCustomAttributes(typeof(JSBindingAttribute), false);
                        if (attributes.Length == 1)
                        {
                            var jsBinding = attributes[0] as JSBindingAttribute;
                            if (jsBinding.Version == 0 || jsBinding.Version == VERSION)
                            {
                                bindingTypes.Add(type);
                            }
                            else
                            {
                                if (listener != null)
                                {
                                    listener.OnBindingError(this, type);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("assembly: {0}, {1}", assembly, e);
                }
            }
            var numRegInvoked = bindingTypes.Count;
            for (var i = 0; i < numRegInvoked; ++i)
            {
                var type = bindingTypes[i];
                var reg = type.GetMethod("reg");
                if (reg != null)
                {
                    reg.Invoke(null, ctxAsArgs);
                    if (listener != null)
                    {
                        listener.OnProgress(this, i, numRegInvoked);
                    }

                    if (i % step == 0)
                    {
                        yield return null;
                    }
                }
            }
            if (listener != null)
            {
                listener.OnBinded(this, numRegInvoked);
            }
            // Debug.LogFormat("exported {0} classes", _exported.Count);

            // 设置导出类的继承链
            foreach (var kv in _exported)
            {
                var type = kv.Key;
                var baseType = type.BaseType;
                if (baseType == null)
                {
                    // Debug.Log($"baseType is null, for {type}");
                    continue;
                }
                var fn = kv.Value;
                fn.PushPrototype(ctx);
                if (PushChainedPrototypeOf(ctx, baseType))
                {
                    // Debug.LogFormat($"set {type} super {baseType}");
                    DuktapeDLL.duk_set_prototype(ctx, -2);
                }
                else
                {
                    Debug.LogWarning($"fail to push prototype, for {type}: {baseType}");
                }
                DuktapeDLL.duk_pop(ctx);
            }

            DuktapeJSBuiltins.postreg(ctx);
            DuktapeDLL.duk_pop(ctx); // pop global 

            _updateTimer = DuktapeRunner.SetInterval(this.OnUpdate, 100);

            if (listener != null)
            {
                listener.OnLoaded(this);
            }
        }

        public void Initialize(IDuktapeListener listener, int step = 30)
        {
            Initialize(new FileResolver(new DefaultFileSystem()), listener, step);
        }

        public void Initialize(IFileSystem fileSystem, IDuktapeListener listener, int step = 30)
        {
            Initialize(new FileResolver(fileSystem), listener, step);
        }

        public void Initialize(IFileResolver fileResolver, IDuktapeListener listener, int step = 30)
        {
            var byteBufferAllocator = new IO.ByteBufferPooledAllocator(0, IO.ByteBufferAllocator.DEFAULT_SIZE, IO.ByteBufferAllocator.DEFAULT_MAX_CAPACITY, false);
            Initialize(byteBufferAllocator, fileResolver, listener, step);
        }

        public void Initialize(IO.ByteBufferAllocator byteBufferAllocator, IFileResolver fileResolver, IDuktapeListener listener, int step = 30)
        {
            _byteBufferAllocator = byteBufferAllocator;
            _fileResolver = fileResolver;

#if UNITY_EDITOR
			// 预览模式下，step传入-1，同步初始化Duktape。
            if (step < 0)
            {
                var e = _InitializeStep(listener, step);
                while (e.MoveNext()) { }
                return;
            }
#endif

            var runner = DuktapeRunner.GetRunner();
            if (runner != null)
            {
                runner.StartCoroutine(_InitializeStep(listener, step));
            }
            else
            {
                var e = _InitializeStep(listener, step);
                while (e.MoveNext()) ;
            }
        }

        // 将 type 的 prototype 压栈 （未导出则向父类追溯）
        // 没有对应的基类 prototype 时, 不压栈
        public bool PushChainedPrototypeOf(IntPtr ctx, Type baseType)
        {
            if (baseType == null)
            {
                // Debug.LogFormat("super terminated {0}", baseType);
                return false;
            }
            if (baseType == typeof(Enum))
            {
                DuktapeFunction val;
                if (_specialTypes.TryGetValue(SPECIAL_ENUM, out val))
                {
                    val.PushPrototype(ctx);
                    return true;
                }
            }
            DuktapeFunction fn;
            if (_exported.TryGetValue(baseType, out fn))
            {
                fn.PushPrototype(ctx);
                return true;
            }
            return PushChainedPrototypeOf(ctx, baseType.BaseType);
        }

        public DuktapeObject EvalSource(string filename, byte[] source)
        {
            if (source == null || source.Length == 0)
            {
                return null;
            }
            if (filename == null)
            {
                filename = "eval";
            }

            var ctx = _ctx.rawValue;
            if (source[0] == 0xbf)
            {
                // load bytecode...
                var buffer_ptr = DuktapeDLL.duk_push_fixed_buffer(ctx, (uint)source.Length);
                Marshal.Copy(source, 0, buffer_ptr, source.Length);
                DuktapeDLL.duk_load_function(ctx);
            }
            else
            {
                DuktapeDLL.duk_unity_push_lstring(ctx, source, (uint)source.Length);
                DuktapeDLL.duk_push_string(ctx, filename);
                if (DuktapeDLL.duk_pcompile(ctx, 0) != 0)
                {
                    DuktapeAux.PrintError(ctx, -1, filename);
                    DuktapeDLL.duk_pop(ctx);
                    throw new Exception("[duktape] source compile failed");
                }
            }

            // Debug.LogFormat("check top {0}", DuktapeDLL.duk_get_top(ctx));
            if (DuktapeDLL.duk_pcall(ctx, 0) != DuktapeDLL.DUK_EXEC_SUCCESS)
            {
                DuktapeAux.PrintError(ctx, -1, filename);
                DuktapeDLL.duk_pop(ctx);
                throw new Exception("[duktape] source eval failed");
            }

            IntPtr ptr = DuktapeDLL.duk_get_heapptr(ctx, -1);
            uint refid = DuktapeDLL.duk_unity_ref(ctx);
            DuktapeObject o = new DuktapeObject(ctx, refid, ptr);
            DuktapeDLL.duk_pop(ctx);
            Debug.LogFormat("check top {0}", DuktapeDLL.duk_get_top(ctx));
            return o;
        }

        public void EvalFile(string filename)
        {
            filename = EnsureExtension(filename);
            var ctx = _ctx.rawValue;
            var source = _fileResolver.ReadAllBytes(filename);
            EvalSource(filename, source);
        }

        public DuktapeObject EvalCustomSource(string filename,ref Dictionary<string, IntPtr> funcPtrs)
        {
            filename = EnsureExtension(filename);
            var source = _fileResolver.ReadAllBytes(filename);
            return  EvalMain(filename, source,funcPtrs);
        }

        public void EvalMain(string filename)
        {
            filename = EnsureExtension(filename);
            var source = _fileResolver.ReadAllBytes(filename);
             EvalMain(filename, source);
        }

        public void EvalMain(string filename, byte[] source)
        {
            if (source == null || source.Length == 0)
            {
                return ;
            }
            if (source[0] == 0xbf)
            {
                //NOTE: module is not supported in bytecode mode
                 EvalSource(filename, source);
            }
            else
            {
                if (filename == null)
                {
                    filename = "eval";
                }
                var ctx = _ctx.rawValue;
                var top = DuktapeDLL.duk_get_top(ctx);

                DuktapeDLL.duk_unity_push_lstring(ctx, source, (uint)source.Length);
                //var err = DuktapeDLL.duk_module_node_peval_main(ctx, filename);
                var err = DuktapeDLL.duk_peval(ctx);
                // var err = DuktapeDLL.duk_peval_string_noresult(ctx, source);
                //  Debug.Log($"load main module: {filename} ({resolvedPath})");
                if (err != 0)
                {
                    DuktapeAux.PrintError(ctx, -1, filename);
                    // Debug.LogErrorFormat("eval main error: {0}\n{1}", DuktapeDLL.duk_safe_to_string(ctx, -1), filename);
                }
                DuktapeDLL.duk_set_top(ctx, top);
            }
        }

        public DuktapeObject EvalMain(string filename, byte[] source, Dictionary<string, IntPtr> funcPtrs)
        {
            if (source == null || source.Length == 0)
            {
                return null;
            }
            if (source[0] == 0xbf)
            {
                //NOTE: module is not supported in bytecode mode
                return EvalSource(filename, source);
            }
            else
            {
                if (filename == null)
                {
                    filename = "eval";
                }
                var ctx = _ctx.rawValue;
                var top = DuktapeDLL.duk_get_top(ctx);

                DuktapeDLL.duk_unity_push_lstring(ctx, source, (uint)source.Length);
                //var err = DuktapeDLL.duk_module_node_peval_main(ctx, filename);
                 var err = DuktapeDLL.duk_peval(ctx);
                // var err = DuktapeDLL.duk_peval_string_noresult(ctx, source);
                //  Debug.Log($"load main module: {filename} ({resolvedPath})");
                if (err != 0)
                {
                    DuktapeAux.PrintError(ctx, -1, filename);
                    // Debug.LogErrorFormat("eval main error: {0}\n{1}", DuktapeDLL.duk_safe_to_string(ctx, -1), filename);
                }

                if (!DuktapeDLL.duk_get_prop_string(ctx, -1, "prototype"))
                {
                    Debug.Log("The script should return a function\n");
                }


                DuktapeObject o = new DuktapeObject(ctx, 0, IntPtr.Zero); 

                int funcLength = ConstMethodInfo.FUNCTION_NAMES.Length;
                IntPtr[] functionsPtrs = new IntPtr[funcLength];
                for (int i = 0; i < funcLength; ++i)
                {
                    string name = ConstMethodInfo.FUNCTION_NAMES[i];
                    if (ctx != IntPtr.Zero)
                    {
                        if (DuktapeDLL.duk_get_prop_string(ctx, -1, name))
                        {
                            if (DuktapeDLL.duk_is_function(ctx, -1))
                            {
                               funcPtrs[name] = DuktapeDLL.duk_get_heapptr(ctx, -1);
                            }
                            else
                            {
                            }
                        }
                        DuktapeDLL.duk_pop(ctx);
                    }
                }

                DuktapeDLL.duk_pop(ctx);

                IntPtr modulePtr = DuktapeDLL.duk_get_heapptr(ctx, -1);
                var refid = DuktapeDLL.duk_unity_ref(ctx);
                o.refId = refid;
                o.heapPtr = modulePtr;

                DuktapeDLL.duk_set_top(ctx, top);
                return o;
            }
        }

        public byte[] DumpBytecode(string filename, byte[] source)
        {
            return _ctx != null ? DuktapeAux.DumpBytecode(_ctx.rawValue, filename, source) : null;
        }

        public void Destroy()
        {
            try
            {
                _instance = null;
                if (_ctx != null)
                {
                    var ctx = _ctx.rawValue;
                    _ctx.Destroy();
                    _ctx = null;
                    _lastContextPtr = IntPtr.Zero;
                    _lastContext = null;
                    _contexts.Clear();
                    _objectCache.Clear();
                    DuktapeDLL.duk_unity_destroy_heap(ctx);
                    // Debug.LogWarning("duk_destroy_heap");
                }

                if (_updateTimer != 0)
                {
                    DuktapeRunner.Clear(_updateTimer);
                    _updateTimer = 0;
                }
            }
            finally
            {
                if (_memAllocPool != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(_memAllocPool);
                    _memAllocPool = IntPtr.Zero;
                }
            }
        }
    }
}