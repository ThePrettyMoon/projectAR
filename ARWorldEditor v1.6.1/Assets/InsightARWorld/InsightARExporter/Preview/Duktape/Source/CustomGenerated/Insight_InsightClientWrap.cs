//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.InsightClient
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_InsightClient : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                bool arg2;
                arg2 = DuktapeDLL.duk_get_boolean(ctx, 2);
                var o = new Insight.InsightClient(arg0, arg1, arg2);
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
        public static int Bind_disconnect(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.disconnect();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_send(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                self.send(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_tryRecv(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.tryRecv();
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_sendInitUserContextRequest(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                string arg2;
                arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                string arg3;
                arg3 = DuktapeDLL.duk_get_string(ctx, 3);
                int arg4;
                arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                int arg5;
                arg5 = DuktapeDLL.duk_get_int(ctx, 5);
                int arg6;
                arg6 = DuktapeDLL.duk_get_int(ctx, 6);
                var ret = self.sendInitUserContextRequest(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_sendRestoreUserContextRequest(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                var ret = self.sendRestoreUserContextRequest(arg0, arg1);
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_sendClientHeartbeatRequest(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.sendClientHeartbeatRequest(arg0);
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_getStatus(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.InsightClientExtension.getStatus(self);
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_status(IntPtr ctx)
        {
            try
            {
                Insight.InsightClient self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.status;
                DuktapeDLL.duk_push_int(ctx, ret);
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
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "InsightClient", typeof(Insight.InsightClient), BindConstructor);
            duk_add_method(ctx, "disconnect", Bind_disconnect, -1);
            duk_add_method(ctx, "send", Bind_send, -1);
            duk_add_method(ctx, "tryRecv", Bind_tryRecv, -1);
            duk_add_method(ctx, "sendInitUserContextRequest", Bind_sendInitUserContextRequest, -1);
            duk_add_method(ctx, "sendRestoreUserContextRequest", Bind_sendRestoreUserContextRequest, -1);
            duk_add_method(ctx, "sendClientHeartbeatRequest", Bind_sendClientHeartbeatRequest, -1);
            duk_add_property(ctx, "status", Bind_getStatus, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
