//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Quaternion
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Quaternion : DuktapeBinding {
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
                        var o = new Insight.Quaternion(arg0, arg1, arg2, arg3);
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                    if (argc == 0)
                    {
                        var o = new Insight.Quaternion();
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
        public static int Bind_identity(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.identity();
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
        public static int Bind_set(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
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
        public static int Bind_setX(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
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
        public static int Bind_setFromAxisAngle(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.setFromAxisAngle(arg0, arg1);
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
        public static int Bind_setFromRotationMatrix(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Matrix4x4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.setFromRotationMatrix(arg0);
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
        public static int Bind_setFromToRotation(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.setFromToRotation(arg0, arg1);
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
        public static int Bind_setFromUnitVectors(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.setFromUnitVectors(arg0, arg1);
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
        public static int Bind_angleTo(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.angleTo(arg0);
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
        public static int Bind_rotateTowards(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.rotateTowards(arg0, arg1);
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
        public static int Bind_inverse(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.inverse();
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
        public static int Bind_conjugate(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.conjugate();
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
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
        public static int Bind_normalize(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
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
        public static int Bind_multiply(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
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
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
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
        public static int Bind_multiplyQuaternions(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = self.multiplyQuaternions(arg0, arg1);
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
        public static int Bind_slerp(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                var ret = self.slerp(arg0, arg1);
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
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
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
        public static int Bind_pow(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.pow(arg0);
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
        public static int Bind_fromArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        Insight.Quaternion self;
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
                        Insight.Quaternion self;
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
                        Insight.Quaternion self;
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
                        Insight.Quaternion self;
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
                        Insight.Quaternion self;
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
                Insight.Quaternion self;
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
        public static int Bind_setFromRotation(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                self.setFromRotation(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_setLookRotation(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        Insight.Quaternion self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector3 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = self.setLookRotation(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Quaternion self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.setLookRotation(arg0);
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
        public static int Bind_toAngleAxis(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);

                DuktapeDLL.duk_push_this(ctx);
                float arg0;
                Insight.Vector3 arg1;
                self.toAngleAxis(out arg0, out arg1);
                duk_push_primitive(ctx, arg0);
                DuktapeDLL.duk_put_prop_string(ctx, -2, "angle");
                duk_push_classvalue(ctx, arg1);
                DuktapeDLL.duk_put_prop_string(ctx, -2, "axis");
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
                var ret = Insight.Quaternion.New();
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
        public static int BindStatic_Slerp(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        Insight.Quaternion arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Quaternion arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        Insight.Quaternion arg2;
                        duk_get_classvalue(ctx, 2, out arg2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        var ret = Insight.Quaternion.Slerp(arg0, arg1, arg2, arg3);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        Insight.Quaternion arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Quaternion arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = Insight.Quaternion.Slerp(arg0, arg1, arg2);
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
        public static int BindStatic_SlerpFlat(IntPtr ctx)
        {
            try
            {
                float[] arg0;
                duk_get_primitive_array(ctx, 0, out arg0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                float[] arg2;
                duk_get_primitive_array(ctx, 2, out arg2);
                int arg3;
                arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                float[] arg4;
                duk_get_primitive_array(ctx, 4, out arg4);
                int arg5;
                arg5 = DuktapeDLL.duk_get_int(ctx, 5);
                float arg6;
                arg6 = (float)DuktapeDLL.duk_get_number(ctx, 6);
                var ret = Insight.Quaternion.SlerpFlat(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
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
        public static int BindStatic_multiplyVector3(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Quaternion.multiplyVector3(arg0, arg1);
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
        public static int BindStatic_Angle(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Quaternion.Angle(arg0, arg1);
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
        public static int BindStatic_AngleAxis(IntPtr ctx)
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
                        var ret = Insight.Quaternion.AngleAxis(arg0, arg1, arg2, arg3);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        Insight.Vector3 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = Insight.Quaternion.AngleAxis(arg0, arg1);
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
        public static int BindStatic_Dot(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Quaternion.Dot(arg0, arg1);
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
        public static int BindStatic_Euler(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = Insight.Quaternion.Euler(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = Insight.Quaternion.Euler(arg0);
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
        public static int BindStatic_FromToRotation(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var ret = Insight.Quaternion.FromToRotation(arg0, arg1);
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
        public static int BindStatic_Lerp(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Quaternion.Lerp(arg0, arg1, arg2);
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
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Quaternion.LerpUnclamped(arg0, arg1, arg2);
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
        public static int BindStatic_SlerpUnclamped(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Quaternion.SlerpUnclamped(arg0, arg1, arg2);
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
        public static int BindStatic_LookRotation(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector3 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = Insight.Quaternion.LookRotation(arg0, arg1);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = Insight.Quaternion.LookRotation(arg0);
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
        public static int BindStatic_RotateTowards(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Quaternion arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                var ret = Insight.Quaternion.RotateTowards(arg0, arg1, arg2);
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
                Insight.Quaternion self;
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_eulerAngles(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.eulerAngles;
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
        public static int BindRead__x(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self._x;
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
        public static int BindWrite__x(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self._x = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead__y(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self._y;
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
        public static int BindWrite__y(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self._y = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead__z(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self._z;
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
        public static int BindWrite__z(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self._z = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead__w(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self._w;
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
        public static int BindWrite__w(IntPtr ctx)
        {
            try
            {
                Insight.Quaternion self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self._w = value;
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
            duk_begin_class(ctx, "Quaternion", typeof(Insight.Quaternion), BindConstructor);
            duk_add_property(ctx, "eulerAngles", BindRead_eulerAngles, null, -1);
            duk_add_property(ctx, "x", BindRead_x, BindWrite_x, -1);
            duk_add_property(ctx, "y", BindRead_y, BindWrite_y, -1);
            duk_add_property(ctx, "z", BindRead_z, BindWrite_z, -1);
            duk_add_property(ctx, "w", BindRead_w, BindWrite_w, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "identity", Bind_identity, -1);
            duk_add_method(ctx, "set", Bind_set, -1);
            duk_add_method(ctx, "setX", Bind_setX, -1);
            duk_add_method(ctx, "setY", Bind_setY, -1);
            duk_add_method(ctx, "setZ", Bind_setZ, -1);
            duk_add_method(ctx, "setW", Bind_setW, -1);
            duk_add_method(ctx, "setComponent", Bind_setComponent, -1);
            duk_add_method(ctx, "getComponent", Bind_getComponent, -1);
            duk_add_method(ctx, "clone", Bind_clone, -1);
            duk_add_method(ctx, "copy", Bind_copy, -1);
            duk_add_method(ctx, "setFromAxisAngle", Bind_setFromAxisAngle, -1);
            duk_add_method(ctx, "setFromRotationMatrix", Bind_setFromRotationMatrix, -1);
            duk_add_method(ctx, "setFromToRotation", Bind_setFromToRotation, -1);
            duk_add_method(ctx, "setFromUnitVectors", Bind_setFromUnitVectors, -1);
            duk_add_method(ctx, "angleTo", Bind_angleTo, -1);
            duk_add_method(ctx, "rotateTowards", Bind_rotateTowards, -1);
            duk_add_method(ctx, "inverse", Bind_inverse, -1);
            duk_add_method(ctx, "conjugate", Bind_conjugate, -1);
            duk_add_method(ctx, "dot", Bind_dot, -1);
            duk_add_method(ctx, "lengthSq", Bind_lengthSq, -1);
            duk_add_method(ctx, "length", Bind_length, -1);
            duk_add_method(ctx, "normalize", Bind_normalize, -1);
            duk_add_method(ctx, "multiply", Bind_multiply, -1);
            duk_add_method(ctx, "premultiply", Bind_premultiply, -1);
            duk_add_method(ctx, "multiplyQuaternions", Bind_multiplyQuaternions, -1);
            duk_add_method(ctx, "slerp", Bind_slerp, -1);
            duk_add_method(ctx, "equals", Bind_equals, -1);
            duk_add_method(ctx, "pow", Bind_pow, -1);
            duk_add_method(ctx, "fromArray", Bind_fromArray, -1);
            duk_add_method(ctx, "toArray", Bind_toArray, -1);         
            duk_add_method(ctx, "setFromRotation", Bind_setFromRotation, -1);
            duk_add_method(ctx, "setLookRotation", Bind_setLookRotation, -1);
            duk_add_method(ctx, "toAngleAxis", Bind_toAngleAxis, -1);
            duk_add_method(ctx, "New", BindStatic_New, -2);          
            duk_add_method(ctx, "multiplyVector3", BindStatic_multiplyVector3, -2);
            duk_add_method(ctx, "Angle", BindStatic_Angle, -2);
            duk_add_method(ctx, "AngleAxis", BindStatic_AngleAxis, -2);
            duk_add_method(ctx, "Dot", BindStatic_Dot, -2);
            duk_add_method(ctx, "Euler", BindStatic_Euler, -2);
            duk_add_method(ctx, "FromToRotation", BindStatic_FromToRotation, -2);
            duk_add_method(ctx, "Lerp", BindStatic_Lerp, -2);
            duk_add_method(ctx, "LerpUnclamped", BindStatic_LerpUnclamped, -2);      
            duk_add_method(ctx, "LookRotation", BindStatic_LookRotation, -2);
            duk_add_method(ctx, "RotateTowards", BindStatic_RotateTowards, -2);
            duk_add_method(ctx, "Slerp", BindStatic_Slerp, -2);
            duk_add_method(ctx, "SlerpUnclamped", BindStatic_SlerpUnclamped, -2);
            duk_add_method(ctx, "SlerpFlat", BindStatic_SlerpFlat, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
