//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.PhysicsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Rigidbody
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Rigidbody : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Rigidbody();
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
        public static int Bind_SetDensity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.SetDensity(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_MovePosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.MovePosition(MathConverter.ToVector3(arg0));
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_MoveRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.MoveRotation(MathConverter.ToQuaternion(arg0));
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Sleep(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.Sleep();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_IsSleeping(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.IsSleeping();
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
        public static int Bind_WakeUp(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.WakeUp();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ResetCenterOfMass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.ResetCenterOfMass();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ResetInertiaTensor(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.ResetInertiaTensor();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetRelativePointVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                var ret = self.GetRelativePointVelocity(arg0);
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
        public static int Bind_GetPointVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                var ret = self.GetPointVelocity(arg0);
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
        public static int Bind_AddForce(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        Insight.ForceMode arg3;
                        arg3 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 3);
                        self.AddForce(arg0, arg1, arg2, ForceUtility.ToMode(arg3));
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.ForceMode arg1;
                        arg1 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 1);
                        self.AddForce(MathConverter.ToVector3(arg0), ForceUtility.ToMode(arg1));
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.AddForce(MathConverter.ToVector3(arg0));
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
        public static int Bind_AddRelativeForce(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        Insight.ForceMode arg3;
                        arg3 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 3);
                        self.AddRelativeForce(arg0, arg1, arg2, ForceUtility.ToMode(arg3));
                        return 0;
                    }
                    if (argc == 3)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        self.AddRelativeForce(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.ForceMode arg1;
                        arg1 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 1);
                        self.AddRelativeForce(MathConverter.ToVector3(arg0), ForceUtility.ToMode(arg1));
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.AddRelativeForce(MathConverter.ToVector3(arg0));
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
        public static int Bind_AddTorque(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        Insight.ForceMode arg3;
                        arg3 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 3);
                        self.AddTorque(arg0, arg1, arg2, ForceUtility.ToMode(arg3));
                        return 0;
                    }
                    if (argc == 3)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        self.AddTorque(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.ForceMode arg1;
                        arg1 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 1);
                        self.AddTorque(MathConverter.ToVector3(arg0), ForceUtility.ToMode(arg1));
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.AddTorque(MathConverter.ToVector3(arg0));
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
        public static int Bind_AddRelativeTorque(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        Insight.ForceMode arg3;
                        arg3 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 3);
                        self.AddRelativeTorque(arg0, arg1, arg2, ForceUtility.ToMode(arg3));
                        return 0;
                    }
                    if (argc == 3)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        self.AddRelativeTorque(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.ForceMode arg1;
                        arg1 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 1);
                        self.AddRelativeTorque(MathConverter.ToVector3(arg0), ForceUtility.ToMode(arg1));
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.AddRelativeTorque(MathConverter.ToVector3(arg0));
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
        public static int Bind_AddForceAtPosition(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector3 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        Insight.ForceMode arg2;
                        arg2 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 2);
                        self.AddForceAtPosition(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), ForceUtility.ToMode(arg2));
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector3 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        self.AddForceAtPosition(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1));
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
        public static int Bind_AddExplosionForce(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 5)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        UnityEngine.Vector3 arg1;
                        duk_get_structvalue(ctx, 1, out arg1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        Insight.ForceMode arg4;
                        arg4 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 4);
                        self.AddExplosionForce(arg0, arg1, arg2, arg3, ForceUtility.ToMode(arg4));
                        return 0;
                    }
                    if (argc == 4)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        UnityEngine.Vector3 arg1;
                        duk_get_structvalue(ctx, 1, out arg1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        self.AddExplosionForce(arg0, arg1, arg2, arg3);
                        return 0;
                    }
                    if (argc == 3)
                    {
                        UnityEngine.Rigidbody self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        UnityEngine.Vector3 arg1;
                        duk_get_structvalue(ctx, 1, out arg1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        self.AddExplosionForce(arg0, arg1, arg2);
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
        public static int Bind_addForceAtLocalPosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.ForceMode arg2;
                arg2 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 2);
                Insight.RigidbodyExtension.addForceAtLocalPosition(self,arg0,arg1,arg2);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_addRelativeForce(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.ForceMode arg1;
                arg1 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 1);
                Insight.RigidbodyExtension.addRelativeForce(self,arg0,arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_addRelativeForceAtPosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.ForceMode arg2;
                arg2 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 2);
                Insight.RigidbodyExtension.addRelativeForceAtPosition(self,arg0,arg1,arg2);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_addRelativeForceAtLocalPosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.ForceMode arg2;
                arg2 = (Insight.ForceMode)DuktapeDLL.duk_get_int(ctx, 2);
                Insight.RigidbodyExtension.addRelativeForceAtLocalPosition(self,arg0,arg1,arg2);
                return 0;
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
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.toString(self);
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
        public static int Bind_getEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.getEnabled(self);
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
        public static int Bind_setEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool arg0;
                arg0 = DuktapeDLL.duk_get_boolean(ctx, 0);
                Insight.RigidbodyExtension.setEnabled(self, arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_gameObject(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.gameObject(self);
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
        public static int Bind_isActiveAndEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.isActiveAndEnabled(self);
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
        public static int Bind_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.name(self);
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
        public static int Bind_tag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.tag(self);
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
        public static int Bind_transform(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.RigidbodyExtension.transform(self);
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
        public static int BindRead_velocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.velocity;
                duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_velocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.velocity = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_angularVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.angularVelocity;
                duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_angularVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.angularVelocity = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_drag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.drag;
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
        public static int BindWrite_drag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.drag = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_angularDrag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.angularDrag;
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
        public static int BindWrite_angularDrag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.angularDrag = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_mass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.mass;
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
        public static int BindWrite_mass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.mass = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_useGravity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.useGravity;
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
        public static int BindWrite_useGravity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.useGravity = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_maxDepenetrationVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.maxDepenetrationVelocity;
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
        public static int BindWrite_maxDepenetrationVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.maxDepenetrationVelocity = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_isKinematic(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isKinematic;
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
        public static int BindWrite_isKinematic(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.isKinematic = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_freezeRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.freezeRotation;
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
        public static int BindWrite_freezeRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.freezeRotation = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_constraints(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.constraints;
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
        public static int BindWrite_constraints(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.RigidbodyConstraints value;
                value = (UnityEngine.RigidbodyConstraints)DuktapeDLL.duk_get_int(ctx, 0);
                self.constraints = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_collisionDetectionMode(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.collisionDetectionMode;
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
        public static int BindWrite_collisionDetectionMode(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.CollisionDetectionMode value;
                value = (UnityEngine.CollisionDetectionMode)DuktapeDLL.duk_get_int(ctx, 0);
                self.collisionDetectionMode = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_centerOfMass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.centerOfMass;
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
        public static int BindWrite_centerOfMass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 value;
                duk_get_structvalue(ctx, 0, out value);
                self.centerOfMass = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_worldCenterOfMass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.worldCenterOfMass;
                duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_inertiaTensorRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.inertiaTensorRotation;
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
        public static int BindWrite_inertiaTensorRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Quaternion value;
                duk_get_structvalue(ctx, 0, out value);
                self.inertiaTensorRotation = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_inertiaTensor(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.inertiaTensor;
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
        public static int BindWrite_inertiaTensor(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 value;
                duk_get_structvalue(ctx, 0, out value);
                self.inertiaTensor = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_detectCollisions(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.detectCollisions;
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
        public static int BindWrite_detectCollisions(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.detectCollisions = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.position;
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
        public static int BindWrite_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 value;
                duk_get_structvalue(ctx, 0, out value);
                self.position = value;
                return 0;
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
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.rotation;
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
        public static int BindWrite_rotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Quaternion value;
                duk_get_structvalue(ctx, 0, out value);
                self.rotation = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_interpolation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.interpolation;
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
        public static int BindWrite_interpolation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.RigidbodyInterpolation value;
                value = (UnityEngine.RigidbodyInterpolation)DuktapeDLL.duk_get_int(ctx, 0);
                self.interpolation = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_solverIterations(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.solverIterations;
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
        public static int BindWrite_solverIterations(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.solverIterations = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_sleepThreshold(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.sleepThreshold;
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
        public static int BindWrite_sleepThreshold(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.sleepThreshold = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_maxAngularVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.maxAngularVelocity;
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
        public static int BindWrite_maxAngularVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.maxAngularVelocity = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_solverVelocityIterations(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.solverVelocityIterations;
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
        public static int BindWrite_solverVelocityIterations(IntPtr ctx)
        {
            try
            {
                UnityEngine.Rigidbody self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.solverVelocityIterations = value;
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
            duk_begin_class(ctx, "Rigidbody", typeof(UnityEngine.Rigidbody), BindConstructor);
            duk_add_property(ctx, "angularVelocity", BindRead_angularVelocity, BindWrite_angularVelocity, -1);
            duk_add_property(ctx, "isKinematic", BindRead_isKinematic, BindWrite_isKinematic, -1);
            duk_add_property(ctx, "mass", BindRead_mass, BindWrite_mass, -1);
            duk_add_property(ctx, "sleepThreshold", BindRead_sleepThreshold, BindWrite_sleepThreshold, -1);
            duk_add_property(ctx, "useGravity", BindRead_useGravity, BindWrite_useGravity, -1);
            duk_add_property(ctx, "velocity", BindRead_velocity, BindWrite_velocity, -1);
            duk_add_property(ctx, "worldCenterOfMass", BindRead_worldCenterOfMass, null, -1);
            duk_add_method(ctx, "addForce", Bind_AddForce, -1);
            duk_add_method(ctx, "addForceAtPosition", Bind_AddForceAtPosition, -1);
            duk_add_method(ctx, "addForceAtLocalPosition", Bind_addForceAtLocalPosition, -1);
            duk_add_method(ctx, "addRelativeForce", Bind_addRelativeForce, -1);
            duk_add_method(ctx, "addRelativeForceAtLocalPosition", Bind_addRelativeForceAtLocalPosition, -1);
            duk_add_method(ctx, "addRelativeForceAtPosition", Bind_addRelativeForceAtPosition, -1);
            duk_add_method(ctx, "addRelativeTorque", Bind_AddRelativeTorque, -1);
            duk_add_method(ctx, "addTorque", Bind_AddTorque, -1);
            duk_add_method(ctx, "isSleeping", Bind_IsSleeping, -1);
            duk_add_method(ctx, "movePosition", Bind_MovePosition, -1);
            duk_add_method(ctx, "moveRotation", Bind_MoveRotation, -1);
            duk_add_method(ctx, "setDensity", Bind_SetDensity, -1);
            duk_add_method(ctx, "wakeUp", Bind_WakeUp, -1);
            duk_add_method(ctx, "sleep", Bind_Sleep, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_property(ctx, "enabled", Bind_getEnabled, Bind_setEnabled, -1);
            duk_add_property(ctx, "gameObject", Bind_gameObject, null, -1);
            duk_add_property(ctx, "isActiveAndEnabled", Bind_isActiveAndEnabled, null, -1);
            duk_add_property(ctx, "name", Bind_name, null, -1);
            duk_add_property(ctx, "tag", Bind_tag, null, -1);
            duk_add_property(ctx, "transform", Bind_transform, null, -1);   
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
