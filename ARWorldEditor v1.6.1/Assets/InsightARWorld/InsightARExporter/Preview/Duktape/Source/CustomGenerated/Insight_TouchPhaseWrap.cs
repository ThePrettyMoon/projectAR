//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.TouchPhase
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_TouchPhase : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "TouchPhase", typeof(UnityEngine.TouchPhase));
            duk_add_const(ctx, "Began", 0, -2);
            duk_add_const(ctx, "Moved", 1, -2);
            duk_add_const(ctx, "Stationary", 2, -2);
            duk_add_const(ctx, "Ended", 3, -2);
            duk_add_const(ctx, "Canceled", 4, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
