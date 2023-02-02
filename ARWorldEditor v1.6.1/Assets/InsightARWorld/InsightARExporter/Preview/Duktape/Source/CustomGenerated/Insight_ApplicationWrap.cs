//#if UNITY_IOS
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Application
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Application : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Application();
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
        public static int BindStatic_ResetGlobal(IntPtr ctx)
        {
            try
            {
                ApplicationExtension.ResetGlobal();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_NetworkState(IntPtr ctx)
        {
            try
            {
                var ret = ApplicationExtension.NetworkState;
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
        public static int BindStaticRead_RuntimePlatform(IntPtr ctx)
        {
            try
            {
                var ret = ApplicationExtension.RuntimePlatform.ToString();
                DuktapeDLL.duk_push_string(ctx,ret);
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
            duk_begin_class(ctx, "Application", typeof(UnityEngine.Application), BindConstructor);
            duk_add_method(ctx, "ResetGlobal", BindStatic_ResetGlobal, -2);
            duk_add_method(ctx, "OpenUrl", BindStatic_OpenUrl, -2);
            duk_add_property(ctx, "NetworkState", BindStaticRead_NetworkState, null, -2);
            duk_add_property(ctx, "RuntimePlatform", BindStaticRead_RuntimePlatform, null, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        private static int BindStatic_OpenUrl(IntPtr ctx)
        {
            try
            {
                string str="";
                str = DuktapeDLL.duk_get_string(ctx, 0);
                ApplicationExtension.OpenUrl(str);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
    }
}
//#endif
