//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: GB`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS__String : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new GB<string>();
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
        public static int Bind_Foo(IntPtr ctx)
        {
            try
            {
                GB<string> self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                self.Foo(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "");
            duk_begin_class(ctx, "GB_String", typeof(GB<string>), BindConstructor);
            duk_add_method(ctx, "foo", Bind_Foo, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
