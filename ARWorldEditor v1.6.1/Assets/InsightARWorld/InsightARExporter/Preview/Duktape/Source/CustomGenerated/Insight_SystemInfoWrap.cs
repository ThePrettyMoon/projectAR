//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.SystemInfo
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_SystemInfo : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Insight.SystemInfo();
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
        public static int BindStatic_getEngineMajorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfoExtension.getEngineMajorVersion();
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
        public static int BindStatic_getEngineMinorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfoExtension.getEngineMinorVersion();
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
        public static int BindStatic_getEngineType(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfoExtension.getEngineType();
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
        public static int BindStatic_getOperatingSystemMajorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfoExtension.getOperatingSystemMajorVersion();
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
        public static int BindStatic_getOperatingSystemMinorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfoExtension.getOperatingSystemMinorVersion();
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
        public static int BindStatic_getOperatingSystemType(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfoExtension.getOperatingSystemType();
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
        public static int BindStatic_GetType(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Insight.SystemInfo.GetType(arg0);
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
        public static int BindStatic_GetValue(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Insight.SystemInfo.GetValue(arg0);
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
        public static int BindStaticRead_engineMajorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfo.engineMajorVersion;
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
        public static int BindStaticRead_engineMinorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfo.engineMinorVersion;
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
        public static int BindStaticRead_engineType(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfo.engineType;
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
        public static int BindStaticRead_operatingSystemMajorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfo.operatingSystemMajorVersion;
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
        public static int BindStaticRead_operatingSystemMinorVersion(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfo.operatingSystemMinorVersion;
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
        public static int BindStaticRead_operatingSystemType(IntPtr ctx)
        {
            try
            {
                var ret = Insight.SystemInfo.operatingSystemType;
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
            duk_begin_class(ctx, "SystemInfo", typeof(Insight.SystemInfo), BindConstructor);
            duk_add_method(ctx, "GetType", BindStatic_GetType, -2);
            duk_add_method(ctx, "GetValue", BindStatic_GetValue, -2);
            duk_add_property(ctx, "engineMajorVersion", BindStatic_getEngineMajorVersion, null, -2);
            duk_add_property(ctx, "engineMinorVersion", BindStatic_getEngineMinorVersion, null, -2);
            duk_add_property(ctx, "engineType", BindStatic_getEngineType, null, -2);
            duk_add_property(ctx, "operatingSystemMajorVersion", BindStatic_getOperatingSystemMajorVersion, null, -2);
            duk_add_property(ctx, "operatingSystemMinorVersion", BindStatic_getOperatingSystemMinorVersion, null, -2);
            duk_add_property(ctx, "operatingSystemType", BindStatic_getOperatingSystemType, null, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
