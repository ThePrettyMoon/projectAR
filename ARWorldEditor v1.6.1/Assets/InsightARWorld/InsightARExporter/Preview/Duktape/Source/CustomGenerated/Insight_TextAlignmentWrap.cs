//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.TextAlignment
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_TextAlignment : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "TextAlignment", typeof(Insight.TextAlignment));
            duk_add_const(ctx, "LEFT", 1, -2);
            duk_add_const(ctx, "H_MID", 2, -2);
            duk_add_const(ctx, "RIGHT", 4, -2);
            duk_add_const(ctx, "TOP", 8, -2);
            duk_add_const(ctx, "V_MID", 16, -2);
            duk_add_const(ctx, "BOTTOM", 32, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
