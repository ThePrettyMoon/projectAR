// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Object
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Object : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Object();
                duk_bind_native(ctx, o);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetInstanceID(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetInstanceID();
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
        public static int Bind_GetHashCode(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetHashCode();
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
        public static int Bind_Equals(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                object arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.Equals(arg0);
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
        public static int Bind_ToString(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
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
        public static int BindStatic_Instantiate(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Vector3 arg1;
                        duk_get_structvalue(ctx, 1, out arg1);
                        UnityEngine.Quaternion arg2;
                        duk_get_structvalue(ctx, 2, out arg2);
                        UnityEngine.Transform arg3;
                        duk_get_classvalue(ctx, 3, out arg3);
                        var ret = UnityEngine.Object.Instantiate(arg0, arg1, arg2, arg3);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Object), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
                        {
                            UnityEngine.Object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            UnityEngine.Vector3 arg1;
                            duk_get_structvalue(ctx, 1, out arg1);
                            UnityEngine.Quaternion arg2;
                            duk_get_structvalue(ctx, 2, out arg2);
                            var ret = UnityEngine.Object.Instantiate(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Object), typeof(UnityEngine.Transform), typeof(bool)))
                        {
                            UnityEngine.Object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            UnityEngine.Transform arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            bool arg2;
                            arg2 = DuktapeDLL.duk_get_boolean(ctx, 2);
                            var ret = UnityEngine.Object.Instantiate(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Transform arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = UnityEngine.Object.Instantiate(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = UnityEngine.Object.Instantiate(arg0);
                        duk_push_classvalue(ctx, ret);
                        return 1;
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
        public static int BindStatic_Destroy(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        UnityEngine.Object.Destroy(arg0, arg1);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Object.Destroy(arg0);
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
        public static int BindStatic_DestroyImmediate(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        UnityEngine.Object.DestroyImmediate(arg0, arg1);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Object arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Object.DestroyImmediate(arg0);
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
        public static int BindStatic_FindObjectsOfType(IntPtr ctx)
        {
            try
            {
                System.Type arg0;
                duk_get_type(ctx, 0, out arg0);
                var ret = UnityEngine.Object.FindObjectsOfType(arg0);
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
        public static int BindStatic_DontDestroyOnLoad(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                UnityEngine.Object.DontDestroyOnLoad(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_FindObjectOfType(IntPtr ctx)
        {
            try
            {
                System.Type arg0;
                duk_get_type(ctx, 0, out arg0);
                var ret = UnityEngine.Object.FindObjectOfType(arg0);
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
        public static int BindRead_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
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
        public static int BindWrite_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string value;
                value = DuktapeDLL.duk_get_string(ctx, 0);
                self.name = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_hideFlags(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.hideFlags;
                DuktapeDLL.duk_push_int(ctx, (int)ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_hideFlags(IntPtr ctx)
        {
            try
            {
                UnityEngine.Object self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.HideFlags value;
                value = (UnityEngine.HideFlags)DuktapeDLL.duk_get_int(ctx, 0);
                self.hideFlags = value;
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
            duk_begin_class(ctx, "Object", typeof(UnityEngine.Object), BindConstructor);
            duk_add_method(ctx, "getInstanceID", Bind_GetInstanceID, -1);
            duk_add_method(ctx, "getHashCode", Bind_GetHashCode, -1);
            duk_add_method(ctx, "equals", Bind_Equals, -1);
            duk_add_method(ctx, "toString", Bind_ToString, -1);
            duk_add_method(ctx, "Instantiate", BindStatic_Instantiate, -2);
            duk_add_method(ctx, "Destroy", BindStatic_Destroy, -2);
            duk_add_method(ctx, "DestroyImmediate", BindStatic_DestroyImmediate, -2);
            duk_add_method(ctx, "FindObjectsOfType", BindStatic_FindObjectsOfType, -2);
            duk_add_method(ctx, "DontDestroyOnLoad", BindStatic_DontDestroyOnLoad, -2);
            duk_add_method(ctx, "FindObjectOfType", BindStatic_FindObjectOfType, -2);
            duk_add_property(ctx, "name", BindRead_name, BindWrite_name, -1);
            duk_add_property(ctx, "hideFlags", BindRead_hideFlags, BindWrite_hideFlags, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
