//#if UNITY_STANDALONE_WIN
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Type: System.Array
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_System_Array : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_CopyTo(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            self.CopyTo(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            self.CopyTo(arg0, arg1);
                            return 0;
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
        public static int Bind_Clone(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
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
        public static int Bind_GetLongLength(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetLongLength(arg0);
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
        public static int Bind_GetValue(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(long), typeof(long), typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            long arg0;
                            arg0 = (long)DuktapeDLL.duk_get_number(ctx, 0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            long arg2;
                            arg2 = (long)DuktapeDLL.duk_get_number(ctx, 2);
                            var ret = self.GetValue(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(int), typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = self.GetValue(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(long), typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            long arg0;
                            arg0 = (long)DuktapeDLL.duk_get_number(ctx, 0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            var ret = self.GetValue(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.GetValue(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            long arg0;
                            arg0 = (long)DuktapeDLL.duk_get_number(ctx, 0);
                            var ret = self.GetValue(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetValue(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (duk_match_types(ctx, argc)
                     && duk_match_param_types(ctx, 0, argc, typeof(long)))
                    {
                        System.Array self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        long[] arg0 = null;
                        if (argc > 0)
                        {
                            arg0 = new long[argc];
                            for (var i = 0; i < argc; i++)
                            {
                                arg0[i] = (long)DuktapeDLL.duk_get_number(ctx, i);
                            }
                        }
                        var ret = self.GetValue(arg0);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (duk_match_types(ctx, argc)
                     && duk_match_param_types(ctx, 0, argc, typeof(int)))
                    {
                        System.Array self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int[] arg0 = null;
                        if (argc > 0)
                        {
                            arg0 = new int[argc];
                            for (var i = 0; i < argc; i++)
                            {
                                arg0[i] = DuktapeDLL.duk_get_int(ctx, i);
                            }
                        }
                        var ret = self.GetValue(arg0);
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
        public static int Bind_SetValue(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(object), typeof(long), typeof(long), typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            long arg2;
                            arg2 = (long)DuktapeDLL.duk_get_number(ctx, 2);
                            long arg3;
                            arg3 = (long)DuktapeDLL.duk_get_number(ctx, 3);
                            self.SetValue(arg0, arg1, arg2, arg3);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(object), typeof(int), typeof(int), typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            self.SetValue(arg0, arg1, arg2, arg3);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(object), typeof(long), typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            long arg2;
                            arg2 = (long)DuktapeDLL.duk_get_number(ctx, 2);
                            self.SetValue(arg0, arg1, arg2);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(object), typeof(int), typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            self.SetValue(arg0, arg1, arg2);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(object), typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            self.SetValue(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(object), typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            self.SetValue(arg0, arg1);
                            return 0;
                        }
                        break;
                    }
                    if (argc >= 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(object))
                         && duk_match_param_types(ctx, 1, argc, typeof(long)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            long[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new long[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    arg1[i - 1] = (long)DuktapeDLL.duk_get_number(ctx, i);
                                }
                            }
                            self.SetValue(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(object))
                         && duk_match_param_types(ctx, 1, argc, typeof(int)))
                        {
                            System.Array self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new int[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    arg1[i - 1] = DuktapeDLL.duk_get_int(ctx, i);
                                }
                            }
                            self.SetValue(arg0, arg1);
                            return 0;
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
        public static int Bind_GetEnumerator(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
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
        public static int Bind_GetLength(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetLength(arg0);
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
        public static int Bind_GetLowerBound(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetLowerBound(arg0);
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
        public static int Bind_GetUpperBound(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetUpperBound(arg0);
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
        public static int Bind_Initialize(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.Initialize();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_CreateInstance(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        System.Type arg0;
                        duk_get_type(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        var ret = System.Array.CreateInstance(arg0, arg1, arg2, arg3);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Type), typeof(int), typeof(int)))
                        {
                            System.Type arg0;
                            duk_get_type(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = System.Array.CreateInstance(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Type), typeof(int[]), typeof(int[])))
                        {
                            System.Type arg0;
                            duk_get_type(ctx, 0, out arg0);
                            int[] arg1;
                            duk_get_primitive_array(ctx, 1, out arg1);
                            int[] arg2;
                            duk_get_primitive_array(ctx, 2, out arg2);
                            var ret = System.Array.CreateInstance(arg0, arg1, arg2);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        System.Type arg0;
                        duk_get_type(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = System.Array.CreateInstance(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc >= 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Type))
                         && duk_match_param_types(ctx, 1, argc, typeof(long)))
                        {
                            System.Type arg0;
                            duk_get_type(ctx, 0, out arg0);
                            long[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new long[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    arg1[i - 1] = (long)DuktapeDLL.duk_get_number(ctx, i);
                                }
                            }
                            var ret = System.Array.CreateInstance(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Type))
                         && duk_match_param_types(ctx, 1, argc, typeof(int)))
                        {
                            System.Type arg0;
                            duk_get_type(ctx, 0, out arg0);
                            int[] arg1 = null;
                            if (argc - 1 > 0)
                            {
                                arg1 = new int[argc - 1];
                                for (var i = 1; i < argc; i++)
                                {
                                    arg1[i - 1] = DuktapeDLL.duk_get_int(ctx, i);
                                }
                            }
                            var ret = System.Array.CreateInstance(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
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
        public static int BindStatic_BinarySearch(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 5)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        object arg3;
                        duk_get_classvalue(ctx, 3, out arg3);
                        System.Collections.IComparer arg4;
                        duk_get_classvalue(ctx, 4, out arg4);
                        var ret = System.Array.BinarySearch(arg0, arg1, arg2, arg3, arg4);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 4)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        object arg3;
                        duk_get_classvalue(ctx, 3, out arg3);
                        var ret = System.Array.BinarySearch(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        System.Collections.IComparer arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        var ret = System.Array.BinarySearch(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = System.Array.BinarySearch(arg0, arg1);
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
        public static int BindStatic_Copy(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 5)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(long), typeof(System.Array), typeof(long), typeof(long)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            long arg1;
                            arg1 = (long)DuktapeDLL.duk_get_number(ctx, 1);
                            System.Array arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            long arg3;
                            arg3 = (long)DuktapeDLL.duk_get_number(ctx, 3);
                            long arg4;
                            arg4 = (long)DuktapeDLL.duk_get_number(ctx, 4);
                            System.Array.Copy(arg0, arg1, arg2, arg3, arg4);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(int), typeof(System.Array), typeof(int), typeof(int)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            System.Array arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            System.Array.Copy(arg0, arg1, arg2, arg3, arg4);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(System.Array), typeof(long)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            System.Array arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            long arg2;
                            arg2 = (long)DuktapeDLL.duk_get_number(ctx, 2);
                            System.Array.Copy(arg0, arg1, arg2);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(System.Array), typeof(int)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            System.Array arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            System.Array.Copy(arg0, arg1, arg2);
                            return 0;
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
        public static int BindStatic_IndexOf(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        var ret = System.Array.IndexOf(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        var ret = System.Array.IndexOf(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = System.Array.IndexOf(arg0, arg1);
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
        public static int BindStatic_LastIndexOf(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        var ret = System.Array.LastIndexOf(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        var ret = System.Array.LastIndexOf(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        object arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = System.Array.LastIndexOf(arg0, arg1);
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
        public static int BindStatic_Reverse(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        System.Array.Reverse(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        System.Array.Reverse(arg0);
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
        public static int BindStatic_Sort(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 5)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        System.Array arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        System.Collections.IComparer arg4;
                        duk_get_classvalue(ctx, 4, out arg4);
                        System.Array.Sort(arg0, arg1, arg2, arg3, arg4);
                        return 0;
                    }
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(int), typeof(int), typeof(System.Collections.IComparer)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            System.Collections.IComparer arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            System.Array.Sort(arg0, arg1, arg2, arg3);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(System.Array), typeof(int), typeof(int)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            System.Array arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            System.Array.Sort(arg0, arg1, arg2, arg3);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(int), typeof(int)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            System.Array.Sort(arg0, arg1, arg2);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(System.Array), typeof(System.Collections.IComparer)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            System.Array arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            System.Collections.IComparer arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            System.Array.Sort(arg0, arg1, arg2);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(System.Collections.IComparer)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            System.Collections.IComparer arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            System.Array.Sort(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Array), typeof(System.Array)))
                        {
                            System.Array arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            System.Array arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            System.Array.Sort(arg0, arg1);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        System.Array arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        System.Array.Sort(arg0);
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
        public static int BindStatic_Clear(IntPtr ctx)
        {
            try
            {
                System.Array arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                int arg2;
                arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                System.Array.Clear(arg0, arg1, arg2);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_ConstrainedCopy(IntPtr ctx)
        {
            try
            {
                System.Array arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                System.Array arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                int arg3;
                arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                int arg4;
                arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                System.Array.ConstrainedCopy(arg0, arg1, arg2, arg3, arg4);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_LongLength(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.LongLength;
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
        public static int BindRead_IsFixedSize(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.IsFixedSize;
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
        public static int BindRead_IsReadOnly(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.IsReadOnly;
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
        public static int BindRead_IsSynchronized(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.IsSynchronized;
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
        public static int BindRead_SyncRoot(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.SyncRoot;
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
        public static int BindRead_Length(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
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
        public static int BindRead_Rank(IntPtr ctx)
        {
            try
            {
                System.Array self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Rank;
                DuktapeDLL.duk_push_int(ctx, ret);
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
            duk_begin_class(ctx, "Array", typeof(System.Array), object_private_ctor);
            duk_add_method(ctx, "copyTo", Bind_CopyTo, -1);
            duk_add_method(ctx, "clone", Bind_Clone, -1);
            duk_add_method(ctx, "getLongLength", Bind_GetLongLength, -1);
            duk_add_method(ctx, "getValue", Bind_GetValue, -1);
            duk_add_method(ctx, "setValue", Bind_SetValue, -1);
            duk_add_method(ctx, "getEnumerator", Bind_GetEnumerator, -1);
            duk_add_method(ctx, "getLength", Bind_GetLength, -1);
            duk_add_method(ctx, "getLowerBound", Bind_GetLowerBound, -1);
            duk_add_method(ctx, "getUpperBound", Bind_GetUpperBound, -1);
            duk_add_method(ctx, "initialize", Bind_Initialize, -1);
            duk_add_method(ctx, "CreateInstance", BindStatic_CreateInstance, -2);
            duk_add_method(ctx, "BinarySearch", BindStatic_BinarySearch, -2);
            duk_add_method(ctx, "Copy", BindStatic_Copy, -2);
            duk_add_method(ctx, "IndexOf", BindStatic_IndexOf, -2);
            duk_add_method(ctx, "LastIndexOf", BindStatic_LastIndexOf, -2);
            duk_add_method(ctx, "Reverse", BindStatic_Reverse, -2);
            duk_add_method(ctx, "Sort", BindStatic_Sort, -2);
            duk_add_method(ctx, "Clear", BindStatic_Clear, -2);
            duk_add_method(ctx, "ConstrainedCopy", BindStatic_ConstrainedCopy, -2);
            duk_add_property(ctx, "LongLength", BindRead_LongLength, null, -1);
            duk_add_property(ctx, "IsFixedSize", BindRead_IsFixedSize, null, -1);
            duk_add_property(ctx, "IsReadOnly", BindRead_IsReadOnly, null, -1);
            duk_add_property(ctx, "IsSynchronized", BindRead_IsSynchronized, null, -1);
            duk_add_property(ctx, "SyncRoot", BindRead_SyncRoot, null, -1);
            duk_add_property(ctx, "Length", BindRead_Length, null, -1);
            duk_add_property(ctx, "Rank", BindRead_Rank, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
