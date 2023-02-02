//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.GameObject
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_GameObject : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc >= 1)
                    {
                        if (argc == 1)
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var o = new UnityEngine.GameObject(arg0);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(string))
                         && duk_match_param_types(ctx, 1, argc, typeof(System.Type)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.Type[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new System.Type[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    duk_get_type(ctx, i, out arg1[i - 1]);
                                }
                            }
                            var o = new UnityEngine.GameObject(arg0, arg1);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                    }
                    if (argc == 0)
                    {
                        var o = new UnityEngine.GameObject();
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetActive(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool arg0;
                arg0 = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.SetActive(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Find(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                //var ret = UnityEngine.GameObject.Find(arg0);
                // fixed by wy，查找所有根物体下子物体
                var ret = GameObjectUtility.Find(arg0);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_transform(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.transform;
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_layer(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.layer;
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_layer(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.layer = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_activeSelf(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.activeSelf;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_activeInHierarchy(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.activeInHierarchy;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_InstantiateFromFile(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        UnityEngine.Transform arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        System.Action<UnityEngine.GameObject> arg2;
                        duk_get_delegate(ctx, 2, out arg2);
                        GameObjectExtension.InstantiateFromFile(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.Action<UnityEngine.GameObject> arg1;
                        duk_get_delegate(ctx, 1, out arg1);
                        GameObjectExtension.InstantiateFromFile(arg0, arg1);
                        return 0;
                    }
                } while (false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.name;
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_parent(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.getParent();
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_children(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetComponentsInChildren<UnityEngine.GameObject>();
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Instantiate(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.GameObject arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Transform arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        UnityEngine.GameObject.Instantiate(arg0, arg1);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.GameObject arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.GameObject.Instantiate(arg0);
                        return 0;
                    }
                } while (false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Destroy(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                UnityEngine.GameObject.Destroy(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_getComponent(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.GameObject self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.GetComponent(arg0);
                        if (ret != null)
                        {
                            var type = ret.GetType();
                            UnityEngine.Component[] comps = self.GetComponents(type);
                            if (comps != null && comps.Length > arg1)
                            {
                                duk_push_classvalue(ctx, comps[arg1]);
                                return 1;
                            }
                        }
                        else
                        {
                              ScriptRunner[] scriptRunnerList = self.GetComponents<ScriptRunner>();

                              List<ScriptRunner> funcList = new List<ScriptRunner>();

                              if (scriptRunnerList != null && scriptRunnerList.Length > 0)
                              { 
                                  for (int i = 0; i < scriptRunnerList.Length; i++)
                                  {
                                      if (scriptRunnerList[i].RelativeFilePath == arg0)
                                      {
                                          funcList.Add(scriptRunnerList[i]);
                                      }
                                  }
                              }

                              if (funcList != null && arg1 < funcList.Count)
                              {
                                DuktapeBinding.duk_push_classvalue(ctx, funcList[arg1].GetModuleInstance());
                                return 1;
                              }
                        }
                    }
                    if (argc == 1)
                    {
                        UnityEngine.GameObject self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.GetComponent(arg0);
                        if (ret != null)
                        {
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        else
                        {
                             ScriptRunner scriptRunner = self.GetComponent<ScriptRunner>();

                            if (scriptRunner != null)
                            {
                                if (scriptRunner.RelativeFilePath == arg0)
                                {
                                    DuktapeBinding.duk_push_classvalue(ctx, scriptRunner.GetModuleInstance());
                                    return 1;
                                }
                            }
                        }
                    }
                } while (false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ToString();
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_FrustumCulled(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.frustumCulled();
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_FrustumCulled(IntPtr ctx)
        {
            try
            {
                UnityEngine.GameObject self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                //self.layer = value;
                self.frustumCulled(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "GameObject", typeof(UnityEngine.GameObject), BindConstructor);
            duk_add_property(ctx, "activeInHierarchy", BindRead_activeInHierarchy, null, -1);
            duk_add_property(ctx, "activeSelf", BindRead_activeSelf, null, -1);
            duk_add_property(ctx, "frustumCulled", BindRead_FrustumCulled, BindWrite_FrustumCulled, -1);
            duk_add_property(ctx, "layer", BindRead_layer, BindWrite_layer, -1);
            duk_add_property(ctx, "name", BindRead_name, null, -1);
            duk_add_property(ctx, "transform", BindRead_transform, null, -1);
            duk_add_property(ctx, "parent", BindRead_parent, null, -1);
            duk_add_property(ctx, "children", BindRead_children, null, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "getComponent", Bind_getComponent, -1);
            duk_add_method(ctx, "setActive", Bind_SetActive, -1);
            duk_add_method(ctx, "InstantiateFromFile", BindStatic_InstantiateFromFile, -2);
            duk_add_method(ctx, "Instantiate", BindStatic_Instantiate, -2);
            duk_add_method(ctx, "Find", BindStatic_Find, -2);
            duk_add_method(ctx, "Destroy", BindStatic_Destroy, -2);         
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
