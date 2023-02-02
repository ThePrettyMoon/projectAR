//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: SampleEnum
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS__SampleEnum : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "");
            duk_begin_enum(ctx, "SampleEnum", typeof(SampleEnum));
            duk_add_const(ctx, "a", 0, -2);
            duk_add_const(ctx, "b", 1, -2);
            duk_add_const(ctx, "c", 2, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
