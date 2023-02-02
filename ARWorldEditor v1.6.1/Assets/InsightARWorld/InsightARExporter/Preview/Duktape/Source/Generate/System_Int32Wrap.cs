//#if UNITY_STANDALONE_WIN
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Type: System.Int32
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_System_Int32 : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_CompareTo(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(object)))
                        {
                            int self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_int(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.CompareTo(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            int self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_int(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.CompareTo(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
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
        public static int Bind_Equals(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(object)))
                        {
                            int self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_int(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.Equals(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            int self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_int(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.Equals(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        break;
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
        public static int Bind_GetHashCode(IntPtr ctx)
        {
            try
            {
                int self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_int(ctx, -1);
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
        public static int Bind_ToString(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        int self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_int(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.IFormatProvider arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = self.ToString(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            int self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_int(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.ToString(arg0);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.IFormatProvider)))
                        {
                            int self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_int(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            System.IFormatProvider arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.ToString(arg0);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 0)
                    {
                        int self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_int(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.ToString();
                        DuktapeDLL.duk_push_string(ctx, ret);
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
        public static int Bind_GetTypeCode(IntPtr ctx)
        {
            try
            {
                int self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_int(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetTypeCode();
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
        public static int BindStatic_Parse(IntPtr ctx)
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
                        System.Globalization.NumberStyles arg1;
                        arg1 = (System.Globalization.NumberStyles)DuktapeDLL.duk_get_int(ctx, 1);
                        System.IFormatProvider arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        var ret = int.Parse(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.Globalization.NumberStyles)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.Globalization.NumberStyles arg1;
                            arg1 = (System.Globalization.NumberStyles)DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = int.Parse(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.IFormatProvider)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.IFormatProvider arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var ret = int.Parse(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = int.Parse(arg0);
                        DuktapeDLL.duk_push_int(ctx, ret);
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
        public static int BindStatic_TryParse(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.Globalization.NumberStyles arg1;
                        arg1 = (System.Globalization.NumberStyles)DuktapeDLL.duk_get_int(ctx, 1);
                        System.IFormatProvider arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        int arg3;
                        var ret = int.TryParse(arg0, arg1, arg2, out arg3);
                        if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 3))
                        {
                            DuktapeDLL.duk_push_int(ctx, arg3);
                            DuktapeDLL.duk_unity_put_target_i(ctx, 3);
                        }
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        var ret = int.TryParse(arg0, out arg1);
                        if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 1))
                        {
                            DuktapeDLL.duk_push_int(ctx, arg1);
                            DuktapeDLL.duk_unity_put_target_i(ctx, 1);
                        }
                        DuktapeDLL.duk_push_boolean(ctx, ret);
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
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "System");
            duk_begin_class(ctx, "Int32", typeof(int), object_private_ctor);
            duk_add_method(ctx, "compareTo", Bind_CompareTo, -1);
            duk_add_method(ctx, "equals", Bind_Equals, -1);
            duk_add_method(ctx, "getHashCode", Bind_GetHashCode, -1);
            duk_add_method(ctx, "toString", Bind_ToString, -1);
            duk_add_method(ctx, "getTypeCode", Bind_GetTypeCode, -1);
            duk_add_method(ctx, "Parse", BindStatic_Parse, -2);
            duk_add_method(ctx, "TryParse", BindStatic_TryParse, -2);
            duk_add_const(ctx, "MaxValue", 2147483647, -2);
            duk_add_const(ctx, "MinValue", -2147483648, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
