//#if UNITY_STANDALONE_WIN
// Special: _DuktapeDelegates
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class _DuktapeDelegates : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(System.Predicate<string>))]
        public static bool _DuktapeDelegates0(Duktape.DuktapeDelegate fn, string obj) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            DuktapeDLL.duk_push_string(ctx, obj);
            fn.EndInvokeWithReturnValue(ctx);
            bool ret0;
            ret0 = DuktapeDLL.duk_get_boolean(ctx, -1);
            DuktapeDLL.duk_pop(ctx);
            return ret0;
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(System.Action<string>))]
        public static void _DuktapeDelegates1(Duktape.DuktapeDelegate fn, string obj) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            DuktapeDLL.duk_push_string(ctx, obj);
            fn.EndInvokeWithReturnValue(ctx);
            DuktapeDLL.duk_pop(ctx);
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(System.Comparison<string>))]
        public static int _DuktapeDelegates2(Duktape.DuktapeDelegate fn, string x, string y) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            DuktapeDLL.duk_push_string(ctx, x);
            DuktapeDLL.duk_push_string(ctx, y);
            fn.EndInvokeWithReturnValue(ctx);
            int ret0;
            ret0 = DuktapeDLL.duk_get_int(ctx, -1);
            DuktapeDLL.duk_pop(ctx);
            return ret0;
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(System.Action<UnityEngine.GameObject>))]
        public static void _DuktapeDelegates3(Duktape.DuktapeDelegate fn, UnityEngine.GameObject obj) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            duk_push_classvalue(ctx, obj);
            fn.EndInvokeWithReturnValue(ctx);
            DuktapeDLL.duk_pop(ctx);
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(System.Action<bool, string>))]
        public static void _DuktapeDelegates4(Duktape.DuktapeDelegate fn, bool arg1, string arg2) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            DuktapeDLL.duk_push_boolean(ctx, arg1);
            DuktapeDLL.duk_push_string(ctx, arg2);
            fn.EndInvokeWithReturnValue(ctx);
            DuktapeDLL.duk_pop(ctx);
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(SampleNamespace.SampleClass.DelegateFoo))]
        [Duktape.JSDelegateAttribute(typeof(SampleNamespace.SampleClass.DelegateFoo2))]
        public static void _DuktapeDelegates5(Duktape.DuktapeDelegate fn, string a, string b) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            DuktapeDLL.duk_push_string(ctx, a);
            DuktapeDLL.duk_push_string(ctx, b);
            fn.EndInvokeWithReturnValue(ctx);
            DuktapeDLL.duk_pop(ctx);
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(SampleNamespace.SampleClass.DelegateFoo4))]
        public static double _DuktapeDelegates6(Duktape.DuktapeDelegate fn, int a, float b) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            DuktapeDLL.duk_push_int(ctx, a);
            DuktapeDLL.duk_push_number(ctx, b);
            fn.EndInvokeWithReturnValue(ctx);
            double ret0;
            ret0 = DuktapeDLL.duk_get_number(ctx, -1);
            DuktapeDLL.duk_pop(ctx);
            return ret0;
        }
        [UnityEngine.Scripting.Preserve]
        [Duktape.JSDelegateAttribute(typeof(System.Action))]
        public static void _DuktapeDelegates7(Duktape.DuktapeDelegate fn) {
            var ctx = fn.ctx;
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException("duktape vm context has already been released.");
            }
            fn.BeginInvoke(ctx);
            fn.EndInvokeWithReturnValue(ctx);
            DuktapeDLL.duk_pop(ctx);
        }
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            var type = typeof(_DuktapeDelegates);
            var vm = DuktapeVM.GetVM(ctx);
            var methods = type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            for (int i = 0, size = methods.Length; i < size; i++)
            {
                var method = methods[i];
                var attributes = method.GetCustomAttributes(typeof(JSDelegateAttribute), false);
                var attributesLength = attributes.Length;
                if (attributesLength > 0)
                {
                    for (var a = 0; a < attributesLength; a++)
                    {
                        var attribute = attributes[a] as JSDelegateAttribute;
                        vm.AddDelegate(attribute.target, method);
                    }
                    duk_begin_namespace(ctx, "DuktapeJS");
                    var name = "Delegate" + (method.GetParameters().Length - 1);
                    if (!DuktapeDLL.duk_get_prop_string(ctx, -1, name))
                    {
                        DuktapeDLL.duk_get_prop_string(ctx, -2, "Dispatcher");
                        DuktapeDLL.duk_put_prop_string(ctx, -3, name);
                    }
                    DuktapeDLL.duk_pop(ctx);
                    duk_end_namespace(ctx);
                }
            }
            return 0;
        }
    }
}
//#endif
