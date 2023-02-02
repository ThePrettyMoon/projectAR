//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.OperatingSystemType
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_OperatingSystemType : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "OperatingSystemType", typeof(Insight.OperatingSystemType));
            duk_add_const(ctx, "Other", 0, -2);
            duk_add_const(ctx, "MacOSX", 1, -2);
            duk_add_const(ctx, "Windows", 2, -2);
            duk_add_const(ctx, "Linux", 3, -2);
            duk_add_const(ctx, "Android", 4, -2);
            duk_add_const(ctx, "iOS", 5, -2);
            duk_add_const(ctx, "Web", 6, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
