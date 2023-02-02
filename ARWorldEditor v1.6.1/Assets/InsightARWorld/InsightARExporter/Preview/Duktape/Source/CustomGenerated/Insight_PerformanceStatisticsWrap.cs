//#if UNITY_STANDALONE_OSX
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.PerformanceStatistics
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_PerformanceStatistics : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Insight.PerformanceStatistics();
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
        public static int BindStatic_GetFPS(IntPtr ctx)
        {
            try
            {
                var ret = Insight.PerformanceStatistics.GetFPS();
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
        public static int BindStatic_GetFrameTime(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetFrameTime(arg0);
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
        public static int BindStatic_GetUpdateTime(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetUpdateTime(arg0);
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
        public static int BindStatic_GetRenderTime(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetRenderTime(arg0);
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
        public static int BindStatic_GetPhysicsTime(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetPhysicsTime(arg0);
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
        public static int BindStatic_GetGPUTime(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetGPUTime(arg0);
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
        public static int BindStatic_GetDrawCallCount(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetDrawCallCount(arg0);
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
        public static int BindStatic_GetTriangleCount(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.PerformanceStatistics.GetTriangleCount(arg0);
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
        public static int BindStatic_GetCostyScript(IntPtr ctx)
        {
            try
            {
                var ret = Insight.PerformanceStatistics.GetCostyScript();
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
        public static int BindStatic_GetCostyScriptTime(IntPtr ctx)
        {
            try
            {
                var ret = Insight.PerformanceStatistics.GetCostyScriptTime();
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
            duk_begin_class(ctx, "PerformanceStatistics", typeof(Insight.PerformanceStatistics), BindConstructor);
            duk_add_method(ctx, "GetFPS", BindStatic_GetFPS, -2);
            duk_add_method(ctx, "GetFrameTime", BindStatic_GetFrameTime, -2);
            duk_add_method(ctx, "GetUpdateTime", BindStatic_GetUpdateTime, -2);
            duk_add_method(ctx, "GetRenderTime", BindStatic_GetRenderTime, -2);
            duk_add_method(ctx, "GetPhysicsTime", BindStatic_GetPhysicsTime, -2);
            duk_add_method(ctx, "GetGPUTime", BindStatic_GetGPUTime, -2);
            duk_add_method(ctx, "GetDrawCallCount", BindStatic_GetDrawCallCount, -2);
            duk_add_method(ctx, "GetTriangleCount", BindStatic_GetTriangleCount, -2);
            duk_add_method(ctx, "GetCostyScript", BindStatic_GetCostyScript, -2);
            duk_add_method(ctx, "GetCostyScriptTime", BindStatic_GetCostyScriptTime, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
