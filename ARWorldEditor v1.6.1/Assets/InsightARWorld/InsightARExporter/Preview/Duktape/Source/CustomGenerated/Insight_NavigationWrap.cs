//#if UNITY_IOS
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Navigation
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Navigation : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Insight.Navigation();
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
        public static int BindStatic_OnLocationChanged(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Navigation.OnLocationChanged(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_OnDestinationChanged(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Navigation.OnDestinationChanged(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_OnPathDataChanged(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Navigation.OnPathDataChanged(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_OnNavigationEnd(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Navigation.OnNavigationEnd(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_OnPoiStatusDataChanged(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                Insight.Navigation.OnPoiStatusDataChanged(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_QueryArProductLocalState(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Insight.Navigation.QueryArProductLocalState(arg0);
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
        public static int BindStatic_QueryMapPoiList(IntPtr ctx)
        {
            try
            {
                var ret = Insight.Navigation.QueryMapPoiList();
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
        public static int BindStatic_GetLightEstimate(IntPtr ctx)
        {
            try
            {
                var result = Insight.Navigation.GetLightEstimate();
                DuktapeDLL.duk_push_string(ctx, result);
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
            duk_begin_class(ctx, "Navigation", typeof(Insight.Navigation), BindConstructor);
            duk_add_method(ctx, "OnLocationChanged", BindStatic_OnLocationChanged, -2);
            duk_add_method(ctx, "OnDestinationChanged", BindStatic_OnDestinationChanged, -2);
            duk_add_method(ctx, "OnPathDataChanged", BindStatic_OnPathDataChanged, -2);
            duk_add_method(ctx, "OnNavigationEnd", BindStatic_OnNavigationEnd, -2);
            duk_add_method(ctx, "OnPoiStatusDataChanged", BindStatic_OnPoiStatusDataChanged, -2);
            duk_add_method(ctx, "QueryArProductLocalState", BindStatic_QueryArProductLocalState, -2);
            duk_add_method(ctx, "QueryMapPoiList", BindStatic_QueryMapPoiList, -2);
            duk_add_method(ctx, "GetLightEstimate", BindStatic_GetLightEstimate, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
