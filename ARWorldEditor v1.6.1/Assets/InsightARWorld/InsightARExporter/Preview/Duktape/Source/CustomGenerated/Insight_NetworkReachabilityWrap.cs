//#if UNITY_IOS
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.NetworkReachability
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_NetworkReachability : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_enum(ctx, "NetworkReachability", typeof(UnityEngine.NetworkReachability));
            duk_add_const(ctx, "NotReachable", 0, -2);
            duk_add_const(ctx, "ReachableViaCarrierDataNetwork", 1, -2);
            duk_add_const(ctx, "ReachableViaLocalAreaNetwork", 2, -2);
            duk_end_enum(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
