//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.LogoPosition
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_LogoPosition : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "LogoPosition", typeof(LogoPosition));
            duk_add_const(ctx, "TL0", 0, -2);
            duk_add_const(ctx, "TM0", 1, -2);
            duk_add_const(ctx, "TR0", 2, -2);
            duk_add_const(ctx, "BL0", 3, -2);
            duk_add_const(ctx, "BM0", 4, -2);
            duk_add_const(ctx, "BR0", 5, -2);
            duk_add_const(ctx, "TL1", 6, -2);
            duk_add_const(ctx, "TM1", 7, -2);
            duk_add_const(ctx, "TR1", 8, -2);
            duk_add_const(ctx, "BL1", 9, -2);
            duk_add_const(ctx, "BM1", 10, -2);
            duk_add_const(ctx, "BR1", 11, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
