//#if UNITY_STANDALONE_OSX
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Vector4
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Vector4 : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);

                do
                {
                    if (argc == 4)
                    {
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        var o = new Insight.Vector4(arg0, arg1, arg2,arg3);
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                    if (argc == 0)
                    {
                        var o = new Insight.Vector4();
                        duk_bind_native(ctx, o);
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
        public static int Bind_set(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
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
                var ret = self.set(arg0, arg1, arg2, arg3);
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
        public static int Bind_setScalar(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.setScalar(arg0);
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
        public static int Bind_setX(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.setX(arg0);
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
        public static int Bind_setY(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.setY(arg0);
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
        public static int Bind_setZ(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.setZ(arg0);
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
        public static int Bind_setW(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.setW(arg0);
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
        public static int Bind_setComponent(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
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
        public static int Bind_getComponent(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
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
        public static int Bind_clone(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
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
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
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
        public static int Bind_add(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        Insight.Vector4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector4 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector4 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = self.add(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Vector4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector4 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.add(arg0);
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
        public static int Bind_addScalar(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.addScalar(arg0);
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
        public static int Bind_addVectors(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.addVectors(arg0, arg1);
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
        public static int Bind_addScaledVector(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.addScaledVector(arg0, arg1);
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
        public static int Bind_sub(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.sub(arg0);
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
        public static int Bind_subScalar(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.subScalar(arg0);
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
        public static int Bind_subVectors(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.subVectors(arg0, arg1);
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
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
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
        public static int Bind_divide(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.divide(arg0);
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
                Insight.Vector4 self;
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
        public static int Bind_multiplyVectors(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.multiplyVectors(arg0, arg1);
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
        public static int Bind_applyMatrix4(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.applyMatrix4(arg0);
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
        public static int Bind_divideScalar(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.divideScalar(arg0);
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
        public static int Bind_divideVectors(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.divideVectors(arg0, arg1);
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
        public static int Bind_setAxisAngleFromQuaternion(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.setAxisAngleFromQuaternion(arg0);
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
        public static int Bind_min(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.min(arg0);
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
        public static int Bind_max(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.max(arg0);
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
        public static int Bind_clamp(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.clamp(arg0, arg1);
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
        public static int Bind_clampScalar(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.clampScalar(arg0, arg1);
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
        public static int Bind_clampLength(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.clampLength(arg0, arg1);
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
        public static int Bind_floor(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.floor();
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
        public static int Bind_ceil(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ceil();
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
        public static int Bind_round(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.round();
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
        public static int Bind_roundToZero(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.roundToZero();
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
        public static int Bind_negate(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.negate();
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
        public static int Bind_dot(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.dot(arg0);
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
        public static int Bind_lengthSq(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.lengthSq();
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
        public static int Bind_length(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.length();
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
        public static int Bind_manhattanLength(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.manhattanLength();
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
        public static int Bind_manhattanDistanceTo(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.manhattanDistanceTo(arg0);
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
        public static int Bind_normalize(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.normalize();
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
        public static int Bind_setLength(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.setLength(arg0);
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
        public static int Bind_lerp(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.lerp(arg0, arg1);
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
        public static int Bind_lerpVectors(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = self.lerpVectors(arg0, arg1, arg2);
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
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
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
                        Insight.Vector4 self;
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
                        Insight.Vector4 self;
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
                        Insight.Vector4 self;
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
                    if (argc == 1)
                    {
                        Insight.Vector4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        var ret = self.toArray(arg0);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        Insight.Vector4 self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.toArray();
                        duk_push_classvalue(ctx, ret);
                        return 1;
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
                Insight.Vector4 self;
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
        public static int BindStatic_New(IntPtr ctx)
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
                var ret = Insight.Vector4.New(arg0, arg1, arg2, arg3);
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
        public static int BindStatic_Distance(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Vector4.Distance(arg0, arg1);
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
        public static int BindStatic_Dot(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Vector4.Dot(arg0, arg1);
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
        public static int BindStatic_Lerp(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Vector4.Lerp(arg0, arg1, arg2);
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
        public static int BindStatic_LerpUnclamped(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Vector4.LerpUnclamped(arg0, arg1, arg2);
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
        public static int BindStatic_Max(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Vector4.Max(arg0, arg1);
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
        public static int BindStatic_Min(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Vector4.Min(arg0, arg1);
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
        public static int BindStatic_MoveTowards(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Vector4.MoveTowards(arg0, arg1, arg2);
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
        public static int BindStatic_Project(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Vector4.Project(arg0, arg1);
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
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Vector4.Scale(arg0, arg1);
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
        public static int BindRead_r(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.r;
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
        public static int BindWrite_r(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.r = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_g(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.g;
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
        public static int BindWrite_g(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.g = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_b(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.b;
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
        public static int BindWrite_b(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.b = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_a(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.a;
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
        public static int BindWrite_a(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.a = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_width(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.width;
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
        public static int BindWrite_width(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.width = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_height(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.height;
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
        public static int BindWrite_height(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.height = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_magnitude(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.magnitude;
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
        public static int BindRead_normalized(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.normalized;
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
        public static int BindRead_sqrMagnitude(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.sqrMagnitude;
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
        public static int BindStaticRead_black(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.black;
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
        public static int BindStaticRead_blue(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.blue;
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
        public static int BindStaticRead_clear(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.clear;
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
        public static int BindStaticRead_cyan(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.cyan;
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
        public static int BindStaticRead_gray(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.gray;
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
        public static int BindStaticRead_grey(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.grey;
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
        public static int BindStaticRead_green(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.green;
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
        public static int BindStaticRead_magenta(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.magenta;
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
        public static int BindStaticRead_negativeInfinity(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.negativeInfinity;
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
        public static int BindStaticRead_one(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.one;
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
        public static int BindStaticRead_positiveInfinity(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.positiveInfinity;
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
        public static int BindStaticRead_red(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.red;
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
        public static int BindStaticRead_white(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.white;
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
        public static int BindStaticRead_yellow(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Vector4.yellow;
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
                var ret = Insight.Vector4.zero;
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
        public static int BindRead_x(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.x;
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
        public static int BindWrite_x(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.x = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_y(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.y;
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
        public static int BindWrite_y(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.y = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_z(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.z;
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
        public static int BindWrite_z(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.z = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_w(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.w;
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
        public static int BindWrite_w(IntPtr ctx)
        {
            try
            {
                Insight.Vector4 self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.w = value;
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
            duk_begin_class(ctx, "Vector4", typeof(Insight.Vector4), BindConstructor);
            duk_add_field(ctx, "x", BindRead_x, BindWrite_x, -1);
            duk_add_field(ctx, "y", BindRead_y, BindWrite_y, -1);
            duk_add_field(ctx, "z", BindRead_z, BindWrite_z, -1);
            duk_add_field(ctx, "w", BindRead_w, BindWrite_w, -1);
            duk_add_property(ctx, "r", BindRead_r, BindWrite_r, -1);
            duk_add_property(ctx, "g", BindRead_g, BindWrite_g, -1);
            duk_add_property(ctx, "b", BindRead_b, BindWrite_b, -1);
            duk_add_property(ctx, "a", BindRead_a, BindWrite_a, -1);
            duk_add_property(ctx, "width", BindRead_width, BindWrite_width, -1);
            duk_add_property(ctx, "height", BindRead_height, BindWrite_height, -1);
            duk_add_property(ctx, "black", BindStaticRead_black, null, -2);
            duk_add_property(ctx, "blue", BindStaticRead_blue, null, -2);
            duk_add_property(ctx, "clear", BindStaticRead_clear, null, -2);
            duk_add_property(ctx, "cyan", BindStaticRead_cyan, null, -2);
            duk_add_property(ctx, "gray", BindStaticRead_gray, null, -2);
            duk_add_property(ctx, "grey", BindStaticRead_grey, null, -2);
            duk_add_property(ctx, "green", BindStaticRead_green, null, -2);
            duk_add_property(ctx, "magenta", BindStaticRead_magenta, null, -2);
            duk_add_property(ctx, "negativeInfinity", BindStaticRead_negativeInfinity, null, -2);
            duk_add_property(ctx, "one", BindStaticRead_one, null, -2);
            duk_add_property(ctx, "positiveInfinity", BindStaticRead_positiveInfinity, null, -2);
            duk_add_property(ctx, "red", BindStaticRead_red, null, -2);
            duk_add_property(ctx, "white", BindStaticRead_white, null, -2);
            duk_add_property(ctx, "yellow", BindStaticRead_yellow, null, -2);
            duk_add_property(ctx, "zero", BindStaticRead_zero, null, -2);
            duk_add_method(ctx, "set", Bind_set, -1);
            duk_add_method(ctx, "setScalar", Bind_setScalar, -1);
            duk_add_method(ctx, "setX", Bind_setX, -1);
            duk_add_method(ctx, "setY", Bind_setY, -1);
            duk_add_method(ctx, "setZ", Bind_setZ, -1);
            duk_add_method(ctx, "setW", Bind_setW, -1);
            duk_add_method(ctx, "setComponent", Bind_setComponent, -1);
            duk_add_method(ctx, "getComponent", Bind_getComponent, -1);
            duk_add_method(ctx, "clone", Bind_clone, -1);
            duk_add_method(ctx, "copy", Bind_copy, -1);
            duk_add_method(ctx, "add", Bind_add, -1);
            duk_add_method(ctx, "addScalar", Bind_addScalar, -1);
            duk_add_method(ctx, "addVectors", Bind_addVectors, -1);
            duk_add_method(ctx, "addScaledVector", Bind_addScaledVector, -1);
            duk_add_method(ctx, "sub", Bind_sub, -1);
            duk_add_method(ctx, "subScalar", Bind_subScalar, -1);
            duk_add_method(ctx, "subVectors", Bind_subVectors, -1);
            duk_add_method(ctx, "multiply", Bind_multiply, -1);
            duk_add_method(ctx, "divide", Bind_divide, -1);
            duk_add_method(ctx, "multiplyScalar", Bind_multiplyScalar, -1);
            duk_add_method(ctx, "multiplyVectors", Bind_multiplyVectors, -1);
            duk_add_method(ctx, "applyMatrix4", Bind_applyMatrix4, -1);
            duk_add_method(ctx, "divideScalar", Bind_divideScalar, -1);
            duk_add_method(ctx, "divideVectors", Bind_divideVectors, -1);
            duk_add_method(ctx, "setAxisAngleFromQuaternion", Bind_setAxisAngleFromQuaternion, -1);
            duk_add_method(ctx, "min", Bind_min, -1);
            duk_add_method(ctx, "max", Bind_max, -1);
            duk_add_method(ctx, "clamp", Bind_clamp, -1);
            duk_add_method(ctx, "clampScalar", Bind_clampScalar, -1);
            duk_add_method(ctx, "clampLength", Bind_clampLength, -1);
            duk_add_method(ctx, "floor", Bind_floor, -1);
            duk_add_method(ctx, "ceil", Bind_ceil, -1);
            duk_add_method(ctx, "round", Bind_round, -1);
            duk_add_method(ctx, "roundToZero", Bind_roundToZero, -1);
            duk_add_method(ctx, "negate", Bind_negate, -1);
            duk_add_method(ctx, "dot", Bind_dot, -1);
            duk_add_method(ctx, "lengthSq", Bind_lengthSq, -1);
            duk_add_method(ctx, "length", Bind_length, -1);
            duk_add_method(ctx, "manhattanLength", Bind_manhattanLength, -1);
            duk_add_method(ctx, "manhattanDistanceTo", Bind_manhattanDistanceTo, -1);
            duk_add_method(ctx, "normalize", Bind_normalize, -1);
            duk_add_method(ctx, "setLength", Bind_setLength, -1);
            duk_add_method(ctx, "lerp", Bind_lerp, -1);
            duk_add_method(ctx, "lerpVectors", Bind_lerpVectors, -1);
            duk_add_method(ctx, "equals", Bind_equals, -1);
            duk_add_method(ctx, "fromArray", Bind_fromArray, -1);
            duk_add_method(ctx, "toArray", Bind_toArray, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "New", BindStatic_New, -2);
            duk_add_method(ctx, "Distance", BindStatic_Distance, -2);
            duk_add_method(ctx, "Dot", BindStatic_Dot, -2);
            duk_add_method(ctx, "Lerp", BindStatic_Lerp, -2);
            duk_add_method(ctx, "LerpUnclamped", BindStatic_LerpUnclamped, -2);
            duk_add_method(ctx, "Max", BindStatic_Max, -2);
            duk_add_method(ctx, "Min", BindStatic_Min, -2);
            duk_add_method(ctx, "MoveTowards", BindStatic_MoveTowards, -2);
            duk_add_method(ctx, "Project", BindStatic_Project, -2);
            duk_add_method(ctx, "Scale", BindStatic_Scale, -2);
            duk_add_property(ctx, "magnitude", BindRead_magnitude, null, -1);
            duk_add_property(ctx, "normalized", BindRead_normalized, null, -1);
            duk_add_property(ctx, "sqrMagnitude", BindRead_sqrMagnitude, null, -1);

  
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
