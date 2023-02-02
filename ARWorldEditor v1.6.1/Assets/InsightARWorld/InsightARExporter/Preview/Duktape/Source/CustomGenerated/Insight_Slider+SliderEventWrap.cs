#if UNITY_ANDROID
// Assembly: UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.UI.Slider+SliderEvent
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_UI_Slider_SliderEvent : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.UI.Slider.SliderEvent();
                duk_bind_native(ctx, o);
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
            duk_begin_class(ctx, "SliderEvent", typeof(UnityEngine.UI.Slider.SliderEvent), BindConstructor);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
#endif
