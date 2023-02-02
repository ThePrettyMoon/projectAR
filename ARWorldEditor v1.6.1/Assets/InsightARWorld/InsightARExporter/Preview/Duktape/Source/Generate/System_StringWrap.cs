//#if UNITY_STANDALONE_WIN
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Type: System.String
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_System_String : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        var o = new string(arg0, arg1, arg2);
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        char arg0;
                        arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var o = new string(arg0, arg1);
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        var o = new string(arg0);
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
        public static int Bind_Equals(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.StringComparison arg1;
                        arg1 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.Equals(arg0, arg1);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(object)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.Equals(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
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
        public static int Bind_CopyTo(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                char[] arg1;
                duk_get_primitive_array(ctx, 1, out arg1);
                int arg2;
                arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                int arg3;
                arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                self.CopyTo(arg0, arg1, arg2, arg3);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ToCharArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.ToCharArray(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.ToCharArray();
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
        public static int Bind_GetHashCode(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
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
        public static int Bind_Split(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(char[]), typeof(int), typeof(System.StringSplitOptions)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            System.StringSplitOptions arg2;
                            arg2 = (System.StringSplitOptions)DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.Split(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string[]), typeof(int), typeof(System.StringSplitOptions)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            System.StringSplitOptions arg2;
                            arg2 = (System.StringSplitOptions)DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.Split(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(char[]), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.Split(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(char[]), typeof(System.StringSplitOptions)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            System.StringSplitOptions arg1;
                            arg1 = (System.StringSplitOptions)DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.Split(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string[]), typeof(System.StringSplitOptions)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            System.StringSplitOptions arg1;
                            arg1 = (System.StringSplitOptions)DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.Split(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (duk_match_types(ctx, argc)
                     && duk_match_param_types(ctx, 0, argc, typeof(char)))
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0 = null;
                        if (argc > 0)
                        {
                            arg0 = new char[argc];
                            for (var i = 0; i < argc; i++)
                            {
                                arg0[i] = (char)DuktapeDLL.duk_get_int(ctx, i);
                            }
                        }
                        var ret = self.Split(arg0);
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
        public static int Bind_Substring(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.Substring(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = self.Substring(arg0);
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
        public static int Bind_Trim(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.Trim();
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (duk_match_types(ctx, argc)
                     && duk_match_param_types(ctx, 0, argc, typeof(char)))
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0 = null;
                        if (argc > 0)
                        {
                            arg0 = new char[argc];
                            for (var i = 0; i < argc; i++)
                            {
                                arg0[i] = (char)DuktapeDLL.duk_get_int(ctx, i);
                            }
                        }
                        var ret = self.Trim(arg0);
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
        public static int Bind_TrimStart(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                char[] arg0 = null;
                if (argc > 0)
                {
                    arg0 = new char[argc];
                    for (var i = 0; i < argc; i++)
                    {
                        arg0[i] = (char)DuktapeDLL.duk_get_int(ctx, i);
                    }
                }
                var ret = self.TrimStart(arg0);
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
        public static int Bind_TrimEnd(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                char[] arg0 = null;
                if (argc > 0)
                {
                    arg0 = new char[argc];
                    for (var i = 0; i < argc; i++)
                    {
                        arg0[i] = (char)DuktapeDLL.duk_get_int(ctx, i);
                    }
                }
                var ret = self.TrimEnd(arg0);
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
        public static int Bind_IsNormalized(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        System.Text.NormalizationForm arg0;
                        arg0 = (System.Text.NormalizationForm)DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = self.IsNormalized(arg0);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.IsNormalized();
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Normalize(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        System.Text.NormalizationForm arg0;
                        arg0 = (System.Text.NormalizationForm)DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = self.Normalize(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.Normalize();
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
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.CompareTo(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
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
        public static int Bind_Contains(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.Contains(arg0);
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
        public static int Bind_EndsWith(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        System.Globalization.CultureInfo arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        var ret = self.EndsWith(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.StringComparison arg1;
                        arg1 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.EndsWith(arg0, arg1);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.EndsWith(arg0);
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_IndexOf(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        System.StringComparison arg3;
                        arg3 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 3);
                        var ret = self.IndexOf(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.IndexOf(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(System.StringComparison)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            System.StringComparison arg2;
                            arg2 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.IndexOf(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(char), typeof(int), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.IndexOf(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(char), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.IndexOf(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.IndexOf(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.StringComparison)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.StringComparison arg1;
                            arg1 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.IndexOf(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(char)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.IndexOf(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.IndexOf(arg0);
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
        public static int Bind_IndexOfAny(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        var ret = self.IndexOfAny(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.IndexOfAny(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        var ret = self.IndexOfAny(arg0);
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
        public static int Bind_LastIndexOf(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        System.StringComparison arg3;
                        arg3 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 3);
                        var ret = self.LastIndexOf(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.LastIndexOf(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(System.StringComparison)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            System.StringComparison arg2;
                            arg2 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.LastIndexOf(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(char), typeof(int), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.LastIndexOf(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(char), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.LastIndexOf(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.LastIndexOf(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.StringComparison)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.StringComparison arg1;
                            arg1 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.LastIndexOf(arg0, arg1);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(char)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.LastIndexOf(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.LastIndexOf(arg0);
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
        public static int Bind_LastIndexOfAny(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        var ret = self.LastIndexOfAny(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.LastIndexOfAny(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        char[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        var ret = self.LastIndexOfAny(arg0);
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
        public static int Bind_PadLeft(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        char arg1;
                        arg1 = (char)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.PadLeft(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = self.PadLeft(arg0);
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
        public static int Bind_PadRight(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        char arg1;
                        arg1 = (char)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.PadRight(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = self.PadRight(arg0);
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
        public static int Bind_StartsWith(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        System.Globalization.CultureInfo arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        var ret = self.StartsWith(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.StringComparison arg1;
                        arg1 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.StartsWith(arg0, arg1);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.StartsWith(arg0);
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ToLower(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        System.Globalization.CultureInfo arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ToLower(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.ToLower();
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
        public static int Bind_ToLowerInvariant(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ToLowerInvariant();
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
        public static int Bind_ToUpper(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        System.Globalization.CultureInfo arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ToUpper(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.ToUpper();
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
        public static int Bind_ToUpperInvariant(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ToUpperInvariant();
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
        public static int Bind_ToString(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        System.IFormatProvider arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ToString(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
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
        public static int Bind_Clone(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Clone();
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
        public static int Bind_Insert(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                var ret = self.Insert(arg0, arg1);
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
        public static int Bind_Replace(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(char), typeof(char)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            char arg0;
                            arg0 = (char)DuktapeDLL.duk_get_int(ctx, 0);
                            char arg1;
                            arg1 = (char)DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.Replace(arg0, arg1);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string)))
                        {
                            string self;
                            DuktapeDLL.duk_push_this(ctx);
                            self = DuktapeDLL.duk_get_string(ctx, -1);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            var ret = self.Replace(arg0, arg1);
                            DuktapeDLL.duk_push_string(ctx, ret);
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
        public static int Bind_Remove(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.Remove(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string self;
                        DuktapeDLL.duk_push_this(ctx);
                        self = DuktapeDLL.duk_get_string(ctx, -1);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = self.Remove(arg0);
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
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
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
        public static int Bind_GetEnumerator(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetEnumerator();
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
        public static int BindStatic_Join(IntPtr ctx)
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
                        string[] arg1;
                        duk_get_primitive_array(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        var ret = string.Join(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.Collections.Generic.IEnumerable<string> arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = string.Join(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc >= 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string))
                         && duk_match_param_types(ctx, 1, argc, typeof(string)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new string[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    arg1[i - 1] = DuktapeDLL.duk_get_string(ctx, i);
                                }
                            }
                            var ret = string.Join(arg0, arg1);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string))
                         && duk_match_param_types(ctx, 1, argc, typeof(object)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            object[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new object[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    duk_get_classvalue(ctx, i, out arg1[i - 1]);
                                }
                            }
                            var ret = string.Join(arg0, arg1);
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
        public static int BindStatic_Equals(IntPtr ctx)
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
                        string arg1;
                        arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                        System.StringComparison arg2;
                        arg2 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 2);
                        var ret = string.Equals(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        string arg1;
                        arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                        var ret = string.Equals(arg0, arg1);
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_IsNullOrEmpty(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = string.IsNullOrEmpty(arg0);
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
        public static int BindStatic_IsNullOrWhiteSpace(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = string.IsNullOrWhiteSpace(arg0);
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
        public static int BindStatic_Compare(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 7)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool), typeof(System.Globalization.CultureInfo)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            string arg2;
                            arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            bool arg5;
                            arg5 = DuktapeDLL.duk_get_boolean(ctx, 5);
                            System.Globalization.CultureInfo arg6;
                            duk_get_classvalue(ctx, 6, out arg6);
                            var ret = string.Compare(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(System.Globalization.CultureInfo), typeof(System.Globalization.CompareOptions)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            string arg2;
                            arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            System.Globalization.CultureInfo arg5;
                            duk_get_classvalue(ctx, 5, out arg5);
                            System.Globalization.CompareOptions arg6;
                            arg6 = (System.Globalization.CompareOptions)DuktapeDLL.duk_get_int(ctx, 6);
                            var ret = string.Compare(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 6)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            string arg2;
                            arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            bool arg5;
                            arg5 = DuktapeDLL.duk_get_boolean(ctx, 5);
                            var ret = string.Compare(arg0, arg1, arg2, arg3, arg4, arg5);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(System.StringComparison)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            string arg2;
                            arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            System.StringComparison arg5;
                            arg5 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 5);
                            var ret = string.Compare(arg0, arg1, arg2, arg3, arg4, arg5);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 5)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        string arg2;
                        arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        int arg4;
                        arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                        var ret = string.Compare(arg0, arg1, arg2, arg3, arg4);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(System.Globalization.CultureInfo), typeof(System.Globalization.CompareOptions)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            System.Globalization.CultureInfo arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            System.Globalization.CompareOptions arg3;
                            arg3 = (System.Globalization.CompareOptions)DuktapeDLL.duk_get_int(ctx, 3);
                            var ret = string.Compare(arg0, arg1, arg2, arg3);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(bool), typeof(System.Globalization.CultureInfo)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            bool arg2;
                            arg2 = DuktapeDLL.duk_get_boolean(ctx, 2);
                            System.Globalization.CultureInfo arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            var ret = string.Compare(arg0, arg1, arg2, arg3);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(bool)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            bool arg2;
                            arg2 = DuktapeDLL.duk_get_boolean(ctx, 2);
                            var ret = string.Compare(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(System.StringComparison)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            System.StringComparison arg2;
                            arg2 = (System.StringComparison)DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = string.Compare(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        string arg1;
                        arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                        var ret = string.Compare(arg0, arg1);
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
        public static int BindStatic_CompareOrdinal(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 5)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        string arg2;
                        arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        int arg4;
                        arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                        var ret = string.CompareOrdinal(arg0, arg1, arg2, arg3, arg4);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        string arg1;
                        arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                        var ret = string.CompareOrdinal(arg0, arg1);
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
        public static int BindStatic_Format(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 5)
                    {
                        System.IFormatProvider arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        string arg1;
                        arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                        object arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        object arg3;
                        duk_get_classvalue(ctx, 3, out arg3);
                        object arg4;
                        duk_get_classvalue(ctx, 4, out arg4);
                        var ret = string.Format(arg0, arg1, arg2, arg3, arg4);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(object), typeof(object), typeof(object)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            object arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            object arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            object arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            var ret = string.Format(arg0, arg1, arg2, arg3);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.IFormatProvider), typeof(string), typeof(object), typeof(object)))
                        {
                            System.IFormatProvider arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            object arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            object arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            var ret = string.Format(arg0, arg1, arg2, arg3);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(object), typeof(object)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            object arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            object arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            var ret = string.Format(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.IFormatProvider), typeof(string), typeof(object)))
                        {
                            System.IFormatProvider arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            object arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            var ret = string.Format(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc >= 2)
                    {
                        if (argc == 2)
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            object arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var ret = string.Format(arg0, arg1);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.IFormatProvider), typeof(string))
                         && duk_match_param_types(ctx, 2, argc, typeof(object)))
                        {
                            System.IFormatProvider arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            object[] arg2 = null;
                            if (argc - 2 > 0)
                            {
                                arg2 = new object[argc - 2];
                                for (var i = 2; i < argc; i++)
                                {
                                    duk_get_classvalue(ctx, i, out arg2[i - 2]);
                                }
                            }
                            var ret = string.Format(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                    }
                    if (argc >= 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string))
                         && duk_match_param_types(ctx, 1, argc, typeof(object)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            object[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new object[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    duk_get_classvalue(ctx, i, out arg1[i - 1]);
                                }
                            }
                            var ret = string.Format(arg0, arg1);
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
        public static int BindStatic_Copy(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = string.Copy(arg0);
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
        public static int BindStatic_Concat(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(object), typeof(object), typeof(object), typeof(object)))
                        {
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            object arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            object arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            object arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            var ret = string.Concat(arg0, arg1, arg2, arg3);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(string), typeof(string)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            string arg2;
                            arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                            string arg3;
                            arg3 = DuktapeDLL.duk_get_string(ctx, 3);
                            var ret = string.Concat(arg0, arg1, arg2, arg3);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(object), typeof(object), typeof(object)))
                        {
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            object arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            object arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            var ret = string.Concat(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(string)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            string arg2;
                            arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                            var ret = string.Concat(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(object), typeof(object)))
                        {
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            object arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var ret = string.Concat(arg0, arg1);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            var ret = string.Concat(arg0, arg1);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(object)))
                        {
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = string.Concat(arg0);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Collections.Generic.IEnumerable<string>)))
                        {
                            System.Collections.Generic.IEnumerable<string> arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = string.Concat(arg0);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (duk_match_types(ctx, argc)
                     && duk_match_param_types(ctx, 0, argc, typeof(object)))
                    {
                        object[] arg0 = null;
                        if (argc > 0)
                        {
                            arg0 = new object[argc];
                            for (var i = 0; i < argc; i++)
                            {
                                duk_get_classvalue(ctx, i, out arg0[i]);
                            }
                        }
                        var ret = string.Concat(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (duk_match_types(ctx, argc)
                     && duk_match_param_types(ctx, 0, argc, typeof(string)))
                    {
                        string[] arg0 = null;
                        if (argc > 0)
                        {
                            arg0 = new string[argc];
                            for (var i = 0; i < argc; i++)
                            {
                                arg0[i] = DuktapeDLL.duk_get_string(ctx, i);
                            }
                        }
                        var ret = string.Concat(arg0);
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
        public static int BindStatic_Intern(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = string.Intern(arg0);
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
        public static int BindStatic_IsInterned(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = string.IsInterned(arg0);
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
        public static int BindRead_Length(IntPtr ctx)
        {
            try
            {
                string self;
                DuktapeDLL.duk_push_this(ctx);
                self = DuktapeDLL.duk_get_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Length;
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
        public static int BindStaticRead_Empty(IntPtr ctx)
        {
            try
            {
                var ret = string.Empty;
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
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
            duk_begin_class(ctx, "String", typeof(string), BindConstructor);
            duk_add_method(ctx, "equals", Bind_Equals, -1);
            duk_add_method(ctx, "copyTo", Bind_CopyTo, -1);
            duk_add_method(ctx, "toCharArray", Bind_ToCharArray, -1);
            duk_add_method(ctx, "getHashCode", Bind_GetHashCode, -1);
            duk_add_method(ctx, "split", Bind_Split, -1);
            duk_add_method(ctx, "substring", Bind_Substring, -1);
            duk_add_method(ctx, "trim", Bind_Trim, -1);
            duk_add_method(ctx, "trimStart", Bind_TrimStart, -1);
            duk_add_method(ctx, "trimEnd", Bind_TrimEnd, -1);
            duk_add_method(ctx, "isNormalized", Bind_IsNormalized, -1);
            duk_add_method(ctx, "normalize", Bind_Normalize, -1);
            duk_add_method(ctx, "compareTo", Bind_CompareTo, -1);
            duk_add_method(ctx, "contains", Bind_Contains, -1);
            duk_add_method(ctx, "endsWith", Bind_EndsWith, -1);
            duk_add_method(ctx, "indexOf", Bind_IndexOf, -1);
            duk_add_method(ctx, "indexOfAny", Bind_IndexOfAny, -1);
            duk_add_method(ctx, "lastIndexOf", Bind_LastIndexOf, -1);
            duk_add_method(ctx, "lastIndexOfAny", Bind_LastIndexOfAny, -1);
            duk_add_method(ctx, "padLeft", Bind_PadLeft, -1);
            duk_add_method(ctx, "padRight", Bind_PadRight, -1);
            duk_add_method(ctx, "startsWith", Bind_StartsWith, -1);
            duk_add_method(ctx, "toLower", Bind_ToLower, -1);
            duk_add_method(ctx, "toLowerInvariant", Bind_ToLowerInvariant, -1);
            duk_add_method(ctx, "toUpper", Bind_ToUpper, -1);
            duk_add_method(ctx, "toUpperInvariant", Bind_ToUpperInvariant, -1);
            duk_add_method(ctx, "toString", Bind_ToString, -1);
            duk_add_method(ctx, "clone", Bind_Clone, -1);
            duk_add_method(ctx, "insert", Bind_Insert, -1);
            duk_add_method(ctx, "replace", Bind_Replace, -1);
            duk_add_method(ctx, "remove", Bind_Remove, -1);
            duk_add_method(ctx, "getTypeCode", Bind_GetTypeCode, -1);
            duk_add_method(ctx, "getEnumerator", Bind_GetEnumerator, -1);
            duk_add_method(ctx, "Join", BindStatic_Join, -2);
            duk_add_method(ctx, "Equals", BindStatic_Equals, -2);
            duk_add_method(ctx, "IsNullOrEmpty", BindStatic_IsNullOrEmpty, -2);
            duk_add_method(ctx, "IsNullOrWhiteSpace", BindStatic_IsNullOrWhiteSpace, -2);
            duk_add_method(ctx, "Compare", BindStatic_Compare, -2);
            duk_add_method(ctx, "CompareOrdinal", BindStatic_CompareOrdinal, -2);
            duk_add_method(ctx, "Format", BindStatic_Format, -2);
            duk_add_method(ctx, "Copy", BindStatic_Copy, -2);
            duk_add_method(ctx, "Concat", BindStatic_Concat, -2);
            duk_add_method(ctx, "Intern", BindStatic_Intern, -2);
            duk_add_method(ctx, "IsInterned", BindStatic_IsInterned, -2);
            duk_add_property(ctx, "Length", BindRead_Length, null, -1);
            duk_add_field(ctx, "Empty", BindStaticRead_Empty, null, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
