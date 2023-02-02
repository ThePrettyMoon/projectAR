//#if UNITY_STANDALONE_WIN
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Type: System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_System_Collections_Generic_List_String : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var o = new System.Collections.Generic.List<string>(arg0);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Collections.Generic.IEnumerable<string>)))
                        {
                            System.Collections.Generic.IEnumerable<string> arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var o = new System.Collections.Generic.List<string>(arg0);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 0)
                    {
                        var o = new System.Collections.Generic.List<string>();
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
        public static int Bind_get_Item(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self[arg0];
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
        public static int Bind_set_Item(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                self[arg0] = arg1;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Add(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                self.Add(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_AddRange(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Collections.Generic.IEnumerable<string> arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.AddRange(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_AsReadOnly(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.AsReadOnly();
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
        public static int Bind_BinarySearch(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        string arg2;
                        arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                        System.Collections.Generic.IComparer<string> arg3;
                        duk_get_classvalue(ctx, 3, out arg3);
                        var ret = self.BinarySearch(arg0, arg1, arg2, arg3);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.Collections.Generic.IComparer<string> arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = self.BinarySearch(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.BinarySearch(arg0);
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
        public static int Bind_Clear(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.Clear();
                return 0;
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
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
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
        public static int Bind_CopyTo(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        string[] arg1;
                        duk_get_primitive_array(ctx, 1, out arg1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        self.CopyTo(arg0, arg1, arg2, arg3);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        self.CopyTo(arg0, arg1);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        self.CopyTo(arg0);
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
        public static int Bind_Exists(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Predicate<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                var ret = self.Exists(arg0);
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
        public static int Bind_Find(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Predicate<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                var ret = self.Find(arg0);
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
        public static int Bind_FindAll(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Predicate<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                var ret = self.FindAll(arg0);
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
        public static int Bind_FindIndex(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        System.Predicate<string> arg2;
                        duk_get_delegate(ctx, 2, out arg2);
                        var ret = self.FindIndex(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        System.Predicate<string> arg1;
                        duk_get_delegate(ctx, 1, out arg1);
                        var ret = self.FindIndex(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        System.Predicate<string> arg0;
                        duk_get_delegate(ctx, 0, out arg0);
                        var ret = self.FindIndex(arg0);
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
        public static int Bind_FindLast(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Predicate<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                var ret = self.FindLast(arg0);
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
        public static int Bind_FindLastIndex(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        System.Predicate<string> arg2;
                        duk_get_delegate(ctx, 2, out arg2);
                        var ret = self.FindLastIndex(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        System.Predicate<string> arg1;
                        duk_get_delegate(ctx, 1, out arg1);
                        var ret = self.FindLastIndex(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        System.Predicate<string> arg0;
                        duk_get_delegate(ctx, 0, out arg0);
                        var ret = self.FindLastIndex(arg0);
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
        public static int Bind_ForEach(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Action<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                self.ForEach(arg0);
                return 0;
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
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetEnumerator();
                duk_push_structvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetRange(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                var ret = self.GetRange(arg0, arg1);
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
        public static int Bind_IndexOf(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
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
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.IndexOf(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.IndexOf(arg0);
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
        public static int Bind_Insert(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                self.Insert(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_InsertRange(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                System.Collections.Generic.IEnumerable<string> arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                self.InsertRange(arg0, arg1);
                return 0;
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
                    if (argc == 3)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
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
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.LastIndexOf(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.LastIndexOf(arg0);
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
        public static int Bind_Remove(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.Remove(arg0);
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
        public static int Bind_RemoveAll(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Predicate<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                var ret = self.RemoveAll(arg0);
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
        public static int Bind_RemoveAt(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                self.RemoveAt(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_RemoveRange(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                self.RemoveRange(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Reverse(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        self.Reverse(arg0, arg1);
                        return 0;
                    }
                    if (argc == 0)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        self.Reverse();
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
        public static int Bind_Sort(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        System.Collections.Generic.IComparer<string> arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        self.Sort(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(System.Collections.Generic.IComparer<string>)))
                        {
                            System.Collections.Generic.List<string> self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Collections.Generic.IComparer<string> arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            self.Sort(arg0);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Comparison<string>)))
                        {
                            System.Collections.Generic.List<string> self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Comparison<string> arg0;
                            duk_get_delegate(ctx, 0, out arg0);
                            self.Sort(arg0);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 0)
                    {
                        System.Collections.Generic.List<string> self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        self.Sort();
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
        public static int Bind_ToArray(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ToArray();
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
        public static int Bind_TrimExcess(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.TrimExcess();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TrueForAll(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Predicate<string> arg0;
                duk_get_delegate(ctx, 0, out arg0);
                var ret = self.TrueForAll(arg0);
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
        public static int BindRead_Capacity(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Capacity;
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
        public static int BindWrite_Capacity(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.Capacity = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_Count(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Count;
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
            duk_begin_namespace(ctx, "System", "Collections", "Generic");
            duk_begin_class(ctx, "List_String", typeof(System.Collections.Generic.List<string>), BindConstructor);
            duk_add_method(ctx, "add", Bind_Add, -1);
            duk_add_method(ctx, "addRange", Bind_AddRange, -1);
            duk_add_method(ctx, "asReadOnly", Bind_AsReadOnly, -1);
            duk_add_method(ctx, "binarySearch", Bind_BinarySearch, -1);
            duk_add_method(ctx, "clear", Bind_Clear, -1);
            duk_add_method(ctx, "contains", Bind_Contains, -1);
            duk_add_method(ctx, "copyTo", Bind_CopyTo, -1);
            duk_add_method(ctx, "exists", Bind_Exists, -1);
            duk_add_method(ctx, "find", Bind_Find, -1);
            duk_add_method(ctx, "findAll", Bind_FindAll, -1);
            duk_add_method(ctx, "findIndex", Bind_FindIndex, -1);
            duk_add_method(ctx, "findLast", Bind_FindLast, -1);
            duk_add_method(ctx, "findLastIndex", Bind_FindLastIndex, -1);
            duk_add_method(ctx, "forEach", Bind_ForEach, -1);
            duk_add_method(ctx, "getEnumerator", Bind_GetEnumerator, -1);
            duk_add_method(ctx, "getRange", Bind_GetRange, -1);
            duk_add_method(ctx, "indexOf", Bind_IndexOf, -1);
            duk_add_method(ctx, "insert", Bind_Insert, -1);
            duk_add_method(ctx, "insertRange", Bind_InsertRange, -1);
            duk_add_method(ctx, "lastIndexOf", Bind_LastIndexOf, -1);
            duk_add_method(ctx, "remove", Bind_Remove, -1);
            duk_add_method(ctx, "removeAll", Bind_RemoveAll, -1);
            duk_add_method(ctx, "removeAt", Bind_RemoveAt, -1);
            duk_add_method(ctx, "removeRange", Bind_RemoveRange, -1);
            duk_add_method(ctx, "reverse", Bind_Reverse, -1);
            duk_add_method(ctx, "sort", Bind_Sort, -1);
            duk_add_method(ctx, "toArray", Bind_ToArray, -1);
            duk_add_method(ctx, "trimExcess", Bind_TrimExcess, -1);
            duk_add_method(ctx, "trueForAll", Bind_TrueForAll, -1);
            duk_add_property(ctx, "Capacity", BindRead_Capacity, BindWrite_Capacity, -1);
            duk_add_property(ctx, "Count", BindRead_Count, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
