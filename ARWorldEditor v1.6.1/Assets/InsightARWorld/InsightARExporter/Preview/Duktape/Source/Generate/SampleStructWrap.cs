//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: SampleStruct
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS__SampleStruct : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new SampleStruct();
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
        public static int Bind_ChangeFieldA(IntPtr ctx)
        {
            try
            {
                SampleStruct self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                self.ChangeFieldA(arg0);
                duk_rebind_this(ctx, self);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Foo(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc >= 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(int), typeof(int))
                         && duk_match_param_types(ctx, 2, argc, typeof(int)))
                        {
                            SampleStruct self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_structvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int[] arg2 = null;
                            if (argc - 2 > 0)
                            {
                                arg2 = new int[argc - 2];
                                for (var i = 2; i < argc; i++)
                                {
                                    arg2[i - 2] = DuktapeDLL.duk_get_int(ctx, i);
                                }
                            }
                            var ret = SampleStructExtensions.Foo(self, arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string))
                         && duk_match_param_types(ctx, 2, argc, typeof(string)))
                        {
                            SampleStruct self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_structvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            string[] arg2 = null;
                            if (argc - 2 > 0)
                            {
                                arg2 = new string[argc - 2];
                                for (var i = 2; i < argc; i++)
                                {
                                    arg2[i - 2] = DuktapeDLL.duk_get_string(ctx, i);
                                }
                            }
                            var ret = SampleStructExtensions.Foo(self, arg0, arg1, arg2);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
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
        public static int BindStatic_StaticMethodWithReturnAndNoOverride(IntPtr ctx)
        {
            try
            {
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                float arg1;
                duk_get_primitive(ctx, 1, out arg1);
                string[] arg2;
                var ret = SampleStruct.StaticMethodWithReturnAndNoOverride(arg0, ref arg1, out arg2);
                if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 1))
                {
                    DuktapeDLL.duk_push_number(ctx, arg1);
                    DuktapeDLL.duk_unity_put_target_i(ctx, 1);
                }
                if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 2))
                {
                    duk_push_classvalue(ctx, arg2);
                    DuktapeDLL.duk_unity_put_target_i(ctx, 2);
                }
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
        public static int BindRead_readonly_property_c(IntPtr ctx)
        {
            try
            {
                SampleStruct self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.readonly_property_c;
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
        public static int BindRead_readwrite_property_d(IntPtr ctx)
        {
            try
            {
                SampleStruct self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.readwrite_property_d;
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_readwrite_property_d(IntPtr ctx)
        {
            try
            {
                SampleStruct self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.readwrite_property_d = value;
                duk_rebind_this(ctx, self);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_static_readwrite_property_d(IntPtr ctx)
        {
            try
            {
                var ret = SampleStruct.static_readwrite_property_d;
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticWrite_static_readwrite_property_d(IntPtr ctx)
        {
            try
            {
                double value;
                value = DuktapeDLL.duk_get_number(ctx, 0);
                SampleStruct.static_readwrite_property_d = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_field_a(IntPtr ctx)
        {
            try
            {
                SampleStruct self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.field_a;
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
        public static int BindWrite_field_a(IntPtr ctx)
        {
            try
            {
                SampleStruct self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.field_a = value;
                duk_rebind_this(ctx, self);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_static_field_b(IntPtr ctx)
        {
            try
            {
                var ret = SampleStruct.static_field_b;
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
        public static int BindStaticWrite_static_field_b(IntPtr ctx)
        {
            try
            {
                string value;
                value = DuktapeDLL.duk_get_string(ctx, 0);
                SampleStruct.static_field_b = value;
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
            duk_begin_namespace(ctx, "");
            duk_begin_class(ctx, "SampleStruct", typeof(SampleStruct), BindConstructor);
            duk_add_method(ctx, "changeFieldA", Bind_ChangeFieldA, -1);
            duk_add_method(ctx, "foo", Bind_Foo, -1);
            duk_add_method(ctx, "StaticMethodWithReturnAndNoOverride", BindStatic_StaticMethodWithReturnAndNoOverride, -2);
            duk_add_property(ctx, "readonly_property_c", BindRead_readonly_property_c, null, -1);
            duk_add_property(ctx, "readwrite_property_d", BindRead_readwrite_property_d, BindWrite_readwrite_property_d, -1);
            duk_add_property(ctx, "static_readwrite_property_d", BindStaticRead_static_readwrite_property_d, BindStaticWrite_static_readwrite_property_d, -2);
            duk_add_field(ctx, "field_a", BindRead_field_a, BindWrite_field_a, -1);
            duk_add_field(ctx, "static_field_b", BindStaticRead_static_field_b, BindStaticWrite_static_field_b, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
