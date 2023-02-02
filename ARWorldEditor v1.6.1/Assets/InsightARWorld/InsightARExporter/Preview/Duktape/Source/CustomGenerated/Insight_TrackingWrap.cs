//#if UNITY_IOS
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Tracking
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Tracking : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Tracking();
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
        public static int BindStatic_EstimateIllumination(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Tracking.EstimateIllumination(arg0);
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
        public static int BindStatic_QuadGetCenter(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Tracking.QuadGetCenter(arg0);
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
        public static int BindStatic_QuadGetRotation(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Tracking.QuadGetRotation(arg0);
                duk_push_classvalue(ctx, MathConverter.FromQuaternion(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_QuadGetName(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Tracking.QuadGetName(arg0);
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
        public static int BindStatic_QuadGetScale(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Tracking.QuadGetScale(arg0);
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
        public static int BindStatic_QuadGetValid(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Tracking.QuadGetValid(arg0);
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
        public static int BindStatic_Raycasting(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                var ret = Tracking.Raycasting(arg0, arg1);
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
        public static int BindStaticRead_faceResultJsonString(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.faceResultJsonString;
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
        public static int BindStaticRead_gestureResultJsonString(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.gestureResultJsonString;
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
        public static int BindStaticRead_quadCount(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.quadCount;
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
        public static int BindStaticRead_reason(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.reason;
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
        public static int BindStaticRead_status(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.status;
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
        public static int BindStaticRead_cloudLocationStatus(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.cloudLocationStatus;
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
        public static int BindStaticRead_cloudLocationReason(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.cloudLocationReason;
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
        public static int BindStaticRead_cloudLocationTotalCount(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.cloudLocationTotalCount;
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
        public static int BindStaticRead_type(IntPtr ctx)
        {
            try
            {
                var ret = Tracking.type;
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
        public static int BindStatic_GetTrackingResult(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = (int)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = Tracking.GetResultString(arg0);
                duk_push_primitive(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_RequestOnceCloudLocation(IntPtr ctx)
        {
            try
            {
                Tracking.RequestOnceLocNative();
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_OnRecongnizedTarget(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Tracking.OnRecongnizedTarget(arg0, arg1);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_GetCurrentAREngine(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Tracking.GetCurrentAREngine();
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
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "Tracking", typeof(Tracking), BindConstructor);    
            duk_add_property(ctx, "faceResultJsonString", BindStaticRead_faceResultJsonString, null, -2);
            duk_add_property(ctx, "gestureResultJsonString", BindStaticRead_gestureResultJsonString, null, -2);
            duk_add_property(ctx, "quadCount", BindStaticRead_quadCount, null, -2);
            duk_add_property(ctx, "reason", BindStaticRead_reason, null, -2);
            duk_add_property(ctx, "status", BindStaticRead_status, null, -2);
            duk_add_property(ctx, "cloudLocationStatus", BindStaticRead_cloudLocationStatus, null, -2);
            duk_add_property(ctx, "cloudLocationReason", BindStaticRead_cloudLocationReason, null, -2);
            duk_add_property(ctx, "cloudLocationTotalCount", BindStaticRead_cloudLocationTotalCount, null, -2);
            duk_add_property(ctx, "type", BindStaticRead_type, null, -2);
            duk_add_method(ctx, "EstimateIllumination", BindStatic_EstimateIllumination, -2);
            duk_add_method(ctx, "QuadGetCenter", BindStatic_QuadGetCenter, -2);
            duk_add_method(ctx, "QuadGetRotation", BindStatic_QuadGetRotation, -2);
            duk_add_method(ctx, "QuadGetName", BindStatic_QuadGetName, -2);
            duk_add_method(ctx, "QuadGetScale", BindStatic_QuadGetScale, -2);
            duk_add_method(ctx, "QuadGetValid", BindStatic_QuadGetValid, -2);
            duk_add_method(ctx, "Raycasting", BindStatic_Raycasting, -2);
            duk_add_method(ctx, "GetResultString", BindStatic_GetTrackingResult, -2);
            duk_add_method(ctx, "RequestOnceCloudLocation", BindStatic_RequestOnceCloudLocation, -2);
            duk_add_method(ctx, "OnRecongnizedTarget", BindStatic_OnRecongnizedTarget, -2);
            duk_add_method(ctx, "GetCurrentAREngine", BindStatic_GetCurrentAREngine, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
