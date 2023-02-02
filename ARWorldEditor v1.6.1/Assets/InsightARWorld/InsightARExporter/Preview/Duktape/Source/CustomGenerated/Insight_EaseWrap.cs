//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Ease
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS
{
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Ease : DuktapeBinding
    {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "Ease", typeof(Ease));
            duk_add_const(ctx, "Linear", (int)Ease.Linear, -2);
            duk_add_const(ctx, "InSine", (int)Ease.InSine, -2);
            duk_add_const(ctx, "OutSine", (int)Ease.OutSine, -2);
            duk_add_const(ctx, "InOutSine", (int)Ease.InOutSine, -2);
            duk_add_const(ctx, "InQuad", (int)Ease.InQuad, -2);
            duk_add_const(ctx, "OutQuad", (int)Ease.OutQuad, -2);
            duk_add_const(ctx, "InOutQuad", (int)Ease.InOutQuad, -2);
            duk_add_const(ctx, "InCubic", (int)Ease.InCubic, -2);
            duk_add_const(ctx, "OutCubic", (int)Ease.OutCubic, -2);
            duk_add_const(ctx, "InOutCubic", (int)Ease.InOutCubic, -2);
            duk_add_const(ctx, "InQuart", (int)Ease.InQuart, -2);
            duk_add_const(ctx, "OutQuart", (int)Ease.OutQuart, -2);
            duk_add_const(ctx, "InOutQuart", (int)Ease.InOutQuart, -2);
            duk_add_const(ctx, "InOutBack", (int)Ease.InOutBack, -2);
            duk_add_const(ctx, "OutBack", (int)Ease.OutBack, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
