//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.TextFlag
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_TextFlag : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "TextFlag", typeof(Insight.TextFlag));
            duk_add_const(ctx, "RICH_TEXT", 1, -2);
            duk_add_const(ctx, "AUTO_WARP", 2, -2);
            duk_add_const(ctx, "BOLD", 256, -2);
            duk_add_const(ctx, "ITALIC", 512, -2);
            duk_add_const(ctx, "UNDERLINE", 1024, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
