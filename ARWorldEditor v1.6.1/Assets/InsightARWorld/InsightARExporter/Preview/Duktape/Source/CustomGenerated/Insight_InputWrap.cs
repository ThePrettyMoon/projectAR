//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Input
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Input : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Input();
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
        public static int BindStatic_GetMouseButton(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = UnityEngine.Input.GetMouseButton(arg0);
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
        public static int BindStatic_GetMouseButtonDown(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = UnityEngine.Input.GetMouseButtonDown(arg0);
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
        public static int BindStatic_GetMouseButtonUp(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = UnityEngine.Input.GetMouseButtonUp(arg0);
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
        public static int BindStatic_GetTouch(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                if(UnityEngine.Input.touchCount > arg0)
                {
                    var ret = UnityEngine.Input.GetTouch(arg0);
                    duk_push_structvalue(ctx, ret);
                    return 1;
                }
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_mousePosition(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Input.mousePosition;
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
        public static int BindStaticRead_touchCount(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Input.touchCount;
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
            duk_begin_class(ctx, "Input", typeof(UnityEngine.Input), BindConstructor);
            duk_add_method(ctx, "GetMouseButton", BindStatic_GetMouseButton, -2);
            duk_add_method(ctx, "GetMouseButtonDown", BindStatic_GetMouseButtonDown, -2);
            duk_add_method(ctx, "GetMouseButtonUp", BindStatic_GetMouseButtonUp, -2);
            duk_add_method(ctx, "GetTouch", BindStatic_GetTouch, -2);
            duk_add_property(ctx, "mousePosition", BindStaticRead_mousePosition, null, -2);
            duk_add_property(ctx, "touchCount", BindStaticRead_touchCount, null, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
