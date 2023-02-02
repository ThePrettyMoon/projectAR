//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Matrix4x4
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Matrix4x4 : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Insight.Matrix4x4();
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
        public static int Bind_set(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                float arg3;
                arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                float arg4;
                arg4 = (float)DuktapeDLL.duk_get_number(ctx, 4);
                float arg5;
                arg5 = (float)DuktapeDLL.duk_get_number(ctx, 5);
                float arg6;
                arg6 = (float)DuktapeDLL.duk_get_number(ctx, 6);
                float arg7;
                arg7 = (float)DuktapeDLL.duk_get_number(ctx, 7);
                float arg8;
                arg8 = (float)DuktapeDLL.duk_get_number(ctx, 8);
                float arg9;
                arg9 = (float)DuktapeDLL.duk_get_number(ctx, 9);
                float arg10;
                arg10 = (float)DuktapeDLL.duk_get_number(ctx, 10);
                float arg11;
                arg11 = (float)DuktapeDLL.duk_get_number(ctx, 11);
                float arg12;
                arg12 = (float)DuktapeDLL.duk_get_number(ctx, 12);
                float arg13;
                arg13 = (float)DuktapeDLL.duk_get_number(ctx, 13);
                float arg14;
                arg14 = (float)DuktapeDLL.duk_get_number(ctx, 14);
                float arg15;
                arg15 = (float)DuktapeDLL.duk_get_number(ctx, 15);
                var ret = self.set(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
        public static int Bind_getComponent(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.getComponent(arg0);
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
        public static int Bind_setComponent(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.setComponent(arg0, arg1);
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
        public static int Bind_clone(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.clone();
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
        public static int Bind_copy(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.copy(arg0);
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
        public static int Bind_copyPosition(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.copyPosition(arg0);
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
        public static int Bind_extractBasis(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = self.extractBasis(arg0, arg1, arg2);
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
        public static int Bind_makeBasis(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = self.makeBasis(arg0, arg1, arg2);
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
        public static int Bind_extractRotation(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.extractRotation(arg0);
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
        public static int Bind_makeRotationFromQuaternion(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.makeRotationFromQuaternion(arg0);
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
        public static int Bind_lookAt(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = self.lookAt(arg0, arg1, arg2);
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
        public static int Bind_multiply(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.multiply(arg0);
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
        public static int Bind_premultiply(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.premultiply(arg0);
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
        public static int Bind_multiplyMatrices(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Matrix4x4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.multiplyMatrices(arg0, arg1);
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
        public static int Bind_multiplyScalar(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.multiplyScalar(arg0);
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
        public static int Bind_determinant(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.determinant();
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
        public static int Bind_transpose(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.transpose();
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
        public static int Bind_setPosition(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.setPosition(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.setPosition(arg0);
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
        public static int Bind_getInverse(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Matrix4x4 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        var ret = self.getInverse(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Matrix4x4 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.getInverse(arg0);
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
        public static int Bind_scale(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.scale(arg0);
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
        public static int Bind_getMaxScaleOnAxis(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.getMaxScaleOnAxis();
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
        public static int Bind_makeTranslation(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = self.makeTranslation(arg0, arg1, arg2);
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
        public static int Bind_makeRotationX(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.makeRotationX(arg0);
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
        public static int Bind_makeRotationY(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.makeRotationY(arg0);
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
        public static int Bind_makeRotationZ(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.makeRotationZ(arg0);
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
        public static int Bind_makeRotationAxis(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.makeRotationAxis(arg0, arg1);
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
        public static int Bind_makeScale(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = self.makeScale(arg0, arg1, arg2);
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
        public static int Bind_makeShear(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = self.makeShear(arg0, arg1, arg2);
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
        public static int Bind_compose(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = self.compose(arg0, arg1, arg2);
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
        public static int Bind_decompose(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = self.decompose(arg0, arg1, arg2);
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
        public static int Bind_makePerspective(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 6)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        float arg4;
                        arg4 = (float)DuktapeDLL.duk_get_number(ctx, 4);
                        float arg5;
                        arg5 = (float)DuktapeDLL.duk_get_number(ctx, 5);
                        var ret = self.makePerspective(arg0, arg1, arg2, arg3, arg4, arg5);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 4)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        var ret = self.makePerspective(arg0, arg1, arg2, arg3);
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
        public static int Bind_makeOrthographic(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                float arg3;
                arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                float arg4;
                arg4 = (float)DuktapeDLL.duk_get_number(ctx, 4);
                float arg5;
                arg5 = (float)DuktapeDLL.duk_get_number(ctx, 5);
                var ret = self.makeOrthographic(arg0, arg1, arg2, arg3, arg4, arg5);
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
        public static int Bind_equals(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.equals(arg0);
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
        public static int Bind_fromArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.fromArray(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        var ret = self.fromArray(arg0);
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
        public static int Bind_toArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(float[]), typeof(int)))
                        {
                            Insight.Matrix4x4 self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.toArray(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Collections.Generic.List<float>), typeof(int)))
                        {
                            Insight.Matrix4x4 self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Collections.Generic.List<float> arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            var ret = self.toArray(arg0, arg1);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(float[])))
                        {
                            Insight.Matrix4x4 self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            var ret = self.toArray(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Collections.Generic.List<float>)))
                        {
                            Insight.Matrix4x4 self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Collections.Generic.List<float> arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.toArray(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 0)
                    {
                        Insight.Matrix4x4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.toArray();
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
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.toString();
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
        public static int Bind_getRotation(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.getRotation();
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
        public static int Bind_getPosition(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.getPosition();
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
        public static int Bind_getColumn(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.getColumn(arg0);
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
        public static int Bind_getRow(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.getRow(arg0);
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
        public static int Bind_setColumn(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                self.setColumn(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_setRow(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                self.setRow(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_multiplyVector4(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.multiplyVector4(arg0);
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
        public static int Bind_setTRS(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                self.setTRS(arg0, arg1, arg2);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_New(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Matrix4x4.New();
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
        public static int BindStatic_Copy(IntPtr ctx)
        {
            try
            {
                UnityEngine.Matrix4x4 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                var ret = Insight.Matrix4x4.Copy(arg0);
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
        public static int BindStatic_MultiplyPoint(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Matrix4x4.MultiplyPoint(arg0, arg1);
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
        public static int BindStatic_MultiplyPoint3x4(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Matrix4x4.MultiplyPoint3x4(arg0, arg1);
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
        public static int BindStatic_MultiplyVector3(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Matrix4x4.MultiplyVector3(arg0, arg1);
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
        public static int BindStatic_MultiplyVector4(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Matrix4x4.MultiplyVector4(arg0, arg1);
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
        public static int BindStatic_LookAt(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = Insight.Matrix4x4.LookAt(arg0, arg1, arg2);
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
        public static int BindStatic_Ortho(IntPtr ctx)
        {
            try
            {
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                float arg3;
                arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                float arg4;
                arg4 = (float)DuktapeDLL.duk_get_number(ctx, 4);
                float arg5;
                arg5 = (float)DuktapeDLL.duk_get_number(ctx, 5);
                var ret = Insight.Matrix4x4.Ortho(arg0, arg1, arg2, arg3, arg4, arg5);
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
        public static int BindStatic_Perspective(IntPtr ctx)
        {
            try
            {
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                float arg3;
                arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                var ret = Insight.Matrix4x4.Perspective(arg0, arg1, arg2, arg3);
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
        public static int BindStatic_Rotate(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = Insight.Matrix4x4.Rotate(arg0);
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
        public static int BindStatic_Scale(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = Insight.Matrix4x4.Scale(arg0);
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
        public static int BindStatic_Translate(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = Insight.Matrix4x4.Translate(arg0);
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
        public static int BindStatic_TRS(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Vector3 arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var ret = Insight.Matrix4x4.TRS(arg0, arg1, arg2);
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
        public static int BindStaticRead_zero(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Matrix4x4.zero;
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
        public static int BindStaticRead_identity(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Matrix4x4.identity;
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
        public static int BindRead_inverse(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.inverse;
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
        public static int BindRead_isIdentity(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isIdentity;
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
        public static int BindRead_lossyScale(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.lossyScale;
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
        public static int BindRead_rotation(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.rotation;
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
        public static int BindRead_elements(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.elements;
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
        public static int BindWrite_elements(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float[] value;
                duk_get_primitive_array(ctx, 0, out value);
                self.elements = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_identity(IntPtr ctx)
        {
            try
            {
                Insight.Matrix4x4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.Matrix4x4.identity;
                duk_push_classvalue(ctx, ret);
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
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "Matrix4x4", typeof(Insight.Matrix4x4), BindConstructor);   
            duk_add_property(ctx, "inverse", BindRead_inverse, null, -1);
            duk_add_property(ctx, "isIdentity", BindRead_isIdentity, null, -1);
            duk_add_property(ctx, "lossyScale", BindRead_lossyScale, null, -1);
            duk_add_property(ctx, "rotation", BindRead_rotation, null, -1);
            duk_add_method(ctx, "transpose", Bind_transpose, -1); //少了property？
            duk_add_property(ctx, "zero", BindStaticRead_zero, null, -2);
            duk_add_property(ctx, "identity", BindStaticRead_identity, null, -2);
            duk_add_method(ctx, "set", Bind_set, -1);
            duk_add_method(ctx, "identity", Bind_identity, -1);
            duk_add_method(ctx, "clone", Bind_clone, -1);
            duk_add_method(ctx, "copy", Bind_copy, -1);
            duk_add_method(ctx, "copyPosition", Bind_copyPosition, -1);
            duk_add_method(ctx, "extractBasis", Bind_extractBasis, -1);
            duk_add_method(ctx, "makeBasis", Bind_makeBasis, -1);
            duk_add_method(ctx, "extractRotation", Bind_extractRotation, -1);
            duk_add_method(ctx, "makeRotationFromQuaternion", Bind_makeRotationFromQuaternion, -1);
            duk_add_method(ctx, "lookAt", Bind_lookAt, -1);
            duk_add_method(ctx, "multiply", Bind_multiply, -1);
            duk_add_method(ctx, "determinant", Bind_determinant, -1); //少了property?
            duk_add_method(ctx, "setPosition", Bind_setPosition, -1);
            duk_add_method(ctx, "getInverse", Bind_getInverse, -1);
            duk_add_method(ctx, "scale", Bind_scale, -1);
            duk_add_method(ctx, "getMaxScaleOnAxis", Bind_getMaxScaleOnAxis, -1);
            duk_add_method(ctx, "makeTranslation", Bind_makeTranslation, -1);
            duk_add_method(ctx, "makeRotationX", Bind_makeRotationX, -1);
            duk_add_method(ctx, "makeRotationY", Bind_makeRotationY, -1);
            duk_add_method(ctx, "makeRotationZ", Bind_makeRotationZ, -1);
            duk_add_method(ctx, "makeRotationAxis", Bind_makeRotationAxis, -1);
            duk_add_method(ctx, "makeScale", Bind_makeScale, -1);
            duk_add_method(ctx, "makeShear", Bind_makeShear, -1);
            duk_add_method(ctx, "compose", Bind_compose, -1);
            duk_add_method(ctx, "decompose", Bind_decompose, -1);
            duk_add_method(ctx, "makePerspective", Bind_makePerspective, -1);
            duk_add_method(ctx, "makeOrthographic", Bind_makeOrthographic, -1);
            duk_add_method(ctx, "equals", Bind_equals, -1);
            duk_add_method(ctx, "fromArray", Bind_fromArray, -1);
            duk_add_method(ctx, "toArray", Bind_toArray, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "getRotation", Bind_getRotation, -1);
            duk_add_method(ctx, "getPosition", Bind_getPosition, -1);
            duk_add_method(ctx, "getColumn", Bind_getColumn, -1);
            duk_add_method(ctx, "getRow", Bind_getRow, -1);
            duk_add_method(ctx, "setColumn", Bind_setColumn, -1);
            duk_add_method(ctx, "setRow", Bind_setRow, -1);
            duk_add_method(ctx, "getComponent", Bind_getComponent, -1);
            duk_add_method(ctx, "setComponent", Bind_setComponent, -1);
            duk_add_method(ctx, "premultiply", Bind_premultiply, -1);
            duk_add_method(ctx, "multiplyMatrices", Bind_multiplyMatrices, -1);
            duk_add_method(ctx, "multiplyScalar", Bind_multiplyScalar, -1);
            duk_add_method(ctx, "multiplyVector4", Bind_multiplyVector4, -1);
            duk_add_method(ctx, "setTRS", Bind_setTRS, -1);
            duk_add_method(ctx, "New", BindStatic_New, -2);
            duk_add_method(ctx, "Copy", BindStatic_Copy, -2);
            duk_add_method(ctx, "MultiplyPoint", BindStatic_MultiplyPoint, -2);
            duk_add_method(ctx, "MultiplyPoint3x4", BindStatic_MultiplyPoint3x4, -2);
            duk_add_method(ctx, "MultiplyVector3", BindStatic_MultiplyVector3, -2);
            duk_add_method(ctx, "MultiplyVector4", BindStatic_MultiplyVector4, -2);
            duk_add_method(ctx, "LookAt", BindStatic_LookAt, -2);
            duk_add_method(ctx, "Ortho", BindStatic_Ortho, -2);
            duk_add_method(ctx, "Perspective", BindStatic_Perspective, -2);
            duk_add_method(ctx, "Rotate", BindStatic_Rotate, -2);
            duk_add_method(ctx, "Scale", BindStatic_Scale, -2);
            duk_add_method(ctx, "Translate", BindStatic_Translate, -2);
            duk_add_method(ctx, "TRS", BindStatic_TRS, -2);
            duk_add_field(ctx, "elements", BindRead_elements, BindWrite_elements, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
