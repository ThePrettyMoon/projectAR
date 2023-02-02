using Duktape;
using System;

namespace DuktapeJS
{
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Invoke : DuktapeBinding
    {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new Insight.InvokeManager();
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
        public static int BindStatic_Invoke(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                int arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                var ret = Insight.InvokeManager.Invoke(arg0, arg1, arg2);
                DuktapeDLL.duk_push_number(ctx, ret);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_InvokeRepeating(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Duktape.DuktapeObject arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                float arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                var ret = Insight.InvokeManager.InvokeRepeating(arg0, arg1, arg2, arg3);
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_CancelInvoke(IntPtr ctx)
        {
            try
            {
                int arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                Insight.InvokeManager.CancelInvoke(arg0);
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
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "InvokeManager", typeof(Insight.InvokeManager), BindConstructor);
            duk_add_method(ctx, "Invoke", BindStatic_Invoke, -2);
            duk_add_method(ctx, "InvokeRepeating", BindStatic_InvokeRepeating, -2);
            duk_add_method(ctx, "CancelInvoke", BindStatic_CancelInvoke, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}