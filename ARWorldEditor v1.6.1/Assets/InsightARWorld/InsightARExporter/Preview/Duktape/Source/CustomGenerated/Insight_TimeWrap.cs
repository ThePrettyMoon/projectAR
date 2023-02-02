//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Time
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Time : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Time();
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
        public static int BindStaticRead_time(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Time.time;
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
        public static int BindStaticRead_deltaTime(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Time.deltaTime;
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
        public static int BindStaticRead_unscaledTime(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Time.unscaledTime;
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
        public static int BindStaticRead_unscaledDeltaTime(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Time.unscaledDeltaTime;
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
        public static int BindStaticRead_timeScale(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Time.timeScale;
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
        public static int BindStaticWrite_timeScale(IntPtr ctx)
        {
            try
            {
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                UnityEngine.Time.timeScale = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_frameCount(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Time.frameCount;
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
        public static int BindStatic_GetAbsoluteTime(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Time.GetAbsoluteTime();
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
        public static int BindStatic_GetDateTime(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Time.GetDateTime();
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
        public static int BindStatic_GetDateTimeByMillisecond(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Insight.Time.GetDateTimeByMillisecond(arg0);
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
        public static int BindStatic_GetSecondByDateTime(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Time.GetSecondByDateTime();
                DuktapeDLL.duk_push_number(ctx, ret);
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
            duk_begin_class(ctx, "Time", typeof(UnityEngine.Time), BindConstructor);
            duk_add_property(ctx, "deltaTime", BindStaticRead_deltaTime, null, -2);
            duk_add_property(ctx, "frameCount", BindStaticRead_frameCount, null, -2);
            duk_add_property(ctx, "time", BindStaticRead_time, null, -2);
            duk_add_property(ctx, "timeScale", BindStaticRead_timeScale, BindStaticWrite_timeScale, -2);
            duk_add_property(ctx, "unscaledDeltaTime", BindStaticRead_unscaledDeltaTime, null, -2);
            duk_add_property(ctx, "unscaledTime", BindStaticRead_unscaledTime, null, -2);
            duk_add_method(ctx, "GetAbsoluteTime", BindStatic_GetAbsoluteTime, -2);
            duk_add_method(ctx, "GetDateTime", BindStatic_GetDateTime, -2);
            duk_add_method(ctx, "GetDateTimeByMillisecond", BindStatic_GetDateTimeByMillisecond, -2);
            duk_add_method(ctx, "GetSecondByDateTime", BindStatic_GetSecondByDateTime, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
