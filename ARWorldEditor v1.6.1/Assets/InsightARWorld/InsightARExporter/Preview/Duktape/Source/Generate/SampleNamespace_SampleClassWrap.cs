//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: SampleNamespace.SampleClass
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_SampleNamespace_SampleClass : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
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
                var o = new SampleNamespace.SampleClass(arg0, arg1);
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
        public static int Bind_DispatchTestEvent(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.DispatchTestEvent();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TestDelegate1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.TestDelegate1();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TestDelegate4(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.TestDelegate4();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TestVector3(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                self.TestVector3(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TestType1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Type arg0;
                duk_get_type(ctx, 0, out arg0);
                var ret = self.TestType1(arg0);
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
        public static int Bind_SetEnum(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                SampleEnum arg0;
                arg0 = (SampleEnum)DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.SetEnum(arg0);
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
        public static int Bind_CheckingVA(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                SampleNamespace.SampleClass self;
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
                var ret = self.CheckingVA(arg0);
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
        public static int Bind_CheckingVA2(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                int[] arg1 = null;
                if (argc - 1 > 0)
                {
                    arg1 = new int[argc - 1];
                    for (var i = 1; i < argc; i++)
                    {
                        arg1[i - 1] = DuktapeDLL.duk_get_int(ctx, i);
                    }
                }
                var ret = self.CheckingVA2(arg0, arg1);
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
        public static int Bind_MethodOverride(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        SampleNamespace.SampleClass self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        self.MethodOverride(arg0, arg1);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            self.MethodOverride(arg0);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            self.MethodOverride(arg0);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Vector3)))
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            UnityEngine.Vector3 arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            self.MethodOverride(arg0);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 0)
                    {
                        SampleNamespace.SampleClass self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        self.MethodOverride();
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
        public static int Bind_MethodOverride2(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                self.MethodOverride2(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_MethodOverride2F(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.MethodOverride2(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_MethodOverride3(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        SampleNamespace.SampleClass self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        object arg3;
                        duk_get_classvalue(ctx, 3, out arg3);
                        self.MethodOverride3(arg0, arg1, arg2, arg3);
                        return 0;
                    }
                    if (argc >= 3)
                    {
                        if (argc == 3)
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float arg0;
                            arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            self.MethodOverride3(arg0, arg1, arg2);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(float), typeof(float), typeof(float))
                         && duk_match_param_types(ctx, 3, argc, typeof(int)))
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float arg0;
                            arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            int[] arg3 = null;
                            if (argc - 3 > 0)
                            {
                                arg3 = new int[argc - 3];
                                for (var i = 3; i < argc; i++)
                                {
                                    arg3[i - 3] = DuktapeDLL.duk_get_int(ctx, i);
                                }
                            }
                            self.MethodOverride3(arg0, arg1, arg2, arg3);
                            return 0;
                        }
                    }
                    if (argc >= 2)
                    {
                        if (argc == 2)
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float arg0;
                            arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            self.MethodOverride3(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(float), typeof(float))
                         && duk_match_param_types(ctx, 2, argc, typeof(int)))
                        {
                            SampleNamespace.SampleClass self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float arg0;
                            arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            int[] arg2 = null;
                            if (argc - 2 > 0)
                            {
                                arg2 = new int[argc - 2];
                                for (var i = 2; i < argc; i++)
                                {
                                    arg2[i - 2] = DuktapeDLL.duk_get_int(ctx, i);
                                }
                            }
                            self.MethodOverride3(arg0, arg1, arg2);
                            return 0;
                        }
                    }
                    if (argc == 1)
                    {
                        SampleNamespace.SampleClass self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        self.MethodOverride3(arg0);
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
        public static int Bind_TestInstancedRawJSCFunction(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                return self.TestInstancedRawJSCFunction(ctx);
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Sum(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int[] arg0;
                duk_get_primitive_array(ctx, 0, out arg0);
                var ret = self.Sum(arg0);
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
        public static int Bind_TestDuktapeArray(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Duktape.DuktapeArray arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.TestDuktapeArray(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetPositions(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int[] arg0;
                duk_get_primitive_array(ctx, 0, out arg0);
                var ret = self.GetPositions(arg0);
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
        public static int BindStatic_DispatchStaticTestEvent(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass.DispatchStaticTestEvent();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_GetBytes(IntPtr ctx)
        {
            try
            {
                var ret = SampleNamespace.SampleClass.GetBytes();
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
        public static int BindStatic_InputBytes(IntPtr ctx)
        {
            try
            {
                byte[] arg0;
                duk_get_primitive_array(ctx, 0, out arg0);
                var ret = SampleNamespace.SampleClass.InputBytes(arg0);
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
        public static int BindStatic_AnotherBytesTest(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                SampleNamespace.SampleClass.AnotherBytesTest(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_GetSample(IntPtr ctx)
        {
            try
            {
                var ret = SampleNamespace.SampleClass.GetSample();
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
        public static int BindStatic_TestStaticRawJSCFunction(IntPtr ctx)
        {
            try
            {
                return SampleNamespace.SampleClass.TestStaticRawJSCFunction(ctx);
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DoNothing(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass.DoNothing();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DoNothing1(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                SampleNamespace.SampleClass.DoNothing1(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_WriteLog(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                SampleNamespace.SampleClass.WriteLog(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_TestDelegate(IntPtr ctx)
        {
            try
            {
                System.Action arg0;
                duk_get_delegate(ctx, 0, out arg0);
                SampleNamespace.SampleClass.TestDelegate(arg0);
                return 0;
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
                SampleNamespace.SampleClass self;
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
        public static int BindRead_sampleEnum(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.sampleEnum;
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
        public static int BindRead_delegateFoo1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.delegateFoo1;
                duk_push_delegate(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_delegateFoo1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                SampleNamespace.SampleClass.DelegateFoo value;
                duk_get_delegate(ctx, 0, out value);
                self.delegateFoo1 = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_delegateFoo2(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.delegateFoo2;
                duk_push_delegate(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_delegateFoo2(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                SampleNamespace.SampleClass.DelegateFoo2 value;
                duk_get_delegate(ctx, 0, out value);
                self.delegateFoo2 = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_delegateFoo4(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.delegateFoo4;
                duk_push_delegate(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_delegateFoo4(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                SampleNamespace.SampleClass.DelegateFoo4 value;
                duk_get_delegate(ctx, 0, out value);
                self.delegateFoo4 = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_action1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.action1;
                duk_push_delegate(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_action1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Action value;
                duk_get_delegate(ctx, 0, out value);
                self.action1 = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_action2(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.action2;
                duk_push_delegate(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_action2(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Action<string> value;
                duk_get_delegate(ctx, 0, out value);
                self.action2 = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_actions1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.actions1;
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
        public static int BindWrite_actions1(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Action[] value;
                duk_get_delegate_array(ctx, 0, out value);
                self.actions1 = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_logText(IntPtr ctx)
        {
            try
            {
                var ret = SampleNamespace.SampleClass.logText;
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
        public static int BindStaticWrite_logText(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Text value;
                duk_get_classvalue(ctx, 0, out value);
                SampleNamespace.SampleClass.logText = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindAdd_testEvent(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Action value;
                duk_get_delegate(ctx, 0, out value);
                self.testEvent += value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRemove_testEvent(IntPtr ctx)
        {
            try
            {
                SampleNamespace.SampleClass self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Action value;
                duk_get_delegate(ctx, 0, out value);
                self.testEvent -= value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindProxy_testEvent(IntPtr ctx)
        {
            try
            {
                DuktapeDLL.duk_push_this(ctx);
                duk_add_event_instanced(ctx, "testEvent", BindAdd_testEvent, BindRemove_testEvent, -1);
                DuktapeDLL.duk_remove(ctx, -2);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticAdd_staticTestEvent(IntPtr ctx)
        {
            try
            {
                System.Action value;
                duk_get_delegate(ctx, 0, out value);
                SampleNamespace.SampleClass.staticTestEvent += value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRemove_staticTestEvent(IntPtr ctx)
        {
            try
            {
                System.Action value;
                duk_get_delegate(ctx, 0, out value);
                SampleNamespace.SampleClass.staticTestEvent -= value;
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
            duk_begin_namespace(ctx, "SampleNamespace");
            duk_begin_class(ctx, "SampleClass", typeof(SampleNamespace.SampleClass), BindConstructor);
            duk_add_method(ctx, "dispatchTestEvent", Bind_DispatchTestEvent, -1);
            duk_add_method(ctx, "testDelegate1", Bind_TestDelegate1, -1);
            duk_add_method(ctx, "testDelegate4", Bind_TestDelegate4, -1);
            duk_add_method(ctx, "testVector3", Bind_TestVector3, -1);
            duk_add_method(ctx, "testType1", Bind_TestType1, -1);
            duk_add_method(ctx, "setEnum", Bind_SetEnum, -1);
            duk_add_method(ctx, "checkingVA", Bind_CheckingVA, -1);
            duk_add_method(ctx, "checkingVA2", Bind_CheckingVA2, -1);
            duk_add_method(ctx, "methodOverride", Bind_MethodOverride, -1);
            duk_add_method(ctx, "methodOverride2", Bind_MethodOverride2, -1);
            duk_add_method(ctx, "methodOverride2F", Bind_MethodOverride2F, -1);
            duk_add_method(ctx, "methodOverride3", Bind_MethodOverride3, -1);
            duk_add_method(ctx, "testInstancedRawJSCFunction", Bind_TestInstancedRawJSCFunction, -1);
            duk_add_method(ctx, "sum", Bind_Sum, -1);
            duk_add_method(ctx, "testDuktapeArray", Bind_TestDuktapeArray, -1);
            duk_add_method(ctx, "getPositions", Bind_GetPositions, -1);
            duk_add_method(ctx, "DispatchStaticTestEvent", BindStatic_DispatchStaticTestEvent, -2);
            duk_add_method(ctx, "GetBytes", BindStatic_GetBytes, -2);
            duk_add_method(ctx, "InputBytes", BindStatic_InputBytes, -2);
            duk_add_method(ctx, "AnotherBytesTest", BindStatic_AnotherBytesTest, -2);
            duk_add_method(ctx, "GetSample", BindStatic_GetSample, -2);
            duk_add_method(ctx, "TestStaticRawJSCFunction", BindStatic_TestStaticRawJSCFunction, -2);
            duk_add_method(ctx, "DoNothing", BindStatic_DoNothing, -2);
            duk_add_method(ctx, "DoNothing1", BindStatic_DoNothing1, -2);
            duk_add_method(ctx, "WriteLog", BindStatic_WriteLog, -2);
            duk_add_method(ctx, "TestDelegate", BindStatic_TestDelegate, -2);
            duk_add_property(ctx, "name", BindRead_name, null, -1);
            duk_add_property(ctx, "sampleEnum", BindRead_sampleEnum, null, -1);
            duk_add_field(ctx, "delegateFoo1", BindRead_delegateFoo1, BindWrite_delegateFoo1, -1);
            duk_add_field(ctx, "delegateFoo2", BindRead_delegateFoo2, BindWrite_delegateFoo2, -1);
            duk_add_field(ctx, "delegateFoo4", BindRead_delegateFoo4, BindWrite_delegateFoo4, -1);
            duk_add_field(ctx, "action1", BindRead_action1, BindWrite_action1, -1);
            duk_add_field(ctx, "action2", BindRead_action2, BindWrite_action2, -1);
            duk_add_field(ctx, "actions1", BindRead_actions1, BindWrite_actions1, -1);
            duk_add_field(ctx, "logText", BindStaticRead_logText, BindStaticWrite_logText, -2);
            duk_add_property(ctx, "testEvent", BindProxy_testEvent, null, -1);
            duk_add_event(ctx, "staticTestEvent", BindStaticAdd_staticTestEvent, BindStaticRemove_staticTestEvent, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
