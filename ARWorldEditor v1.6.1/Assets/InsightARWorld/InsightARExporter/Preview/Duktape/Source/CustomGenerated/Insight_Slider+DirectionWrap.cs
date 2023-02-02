#if UNITY_ANDROID
// Assembly: UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.UI.Slider+Direction
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_UI_Slider_Direction : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "Direction", typeof(UnityEngine.UI.Slider.Direction));
            duk_add_const(ctx, "LeftToRight", 0, -2);
            duk_add_const(ctx, "RightToLeft", 1, -2);
            duk_add_const(ctx, "BottomToTop", 2, -2);
            duk_add_const(ctx, "TopToBottom", 3, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
#endif
