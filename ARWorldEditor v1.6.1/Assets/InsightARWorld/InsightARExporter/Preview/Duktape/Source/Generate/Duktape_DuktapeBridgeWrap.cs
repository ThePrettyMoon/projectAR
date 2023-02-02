//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Duktape.DuktapeBridge
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_DuktapeJS_Bridge : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Duktape.DuktapeBridge();
                duk_bind_native(ctx, o);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetBridge(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeBridge self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.SetBridge(arg0);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "DuktapeJS");
            duk_begin_class(ctx, "Bridge", typeof(Duktape.DuktapeBridge), BindConstructor);
            duk_add_method(ctx, "setBridge", Bind_SetBridge, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
