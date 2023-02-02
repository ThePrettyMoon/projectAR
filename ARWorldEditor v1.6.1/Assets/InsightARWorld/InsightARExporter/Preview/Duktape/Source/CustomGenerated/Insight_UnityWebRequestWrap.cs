//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.UnityWebRequestModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Networking.UnityWebRequest
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_UnityWebRequest : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(UnityEngine.Networking.DownloadHandler), typeof(UnityEngine.Networking.UploadHandler)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            UnityEngine.Networking.DownloadHandler arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            UnityEngine.Networking.UploadHandler arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            var o = new UnityEngine.Networking.UnityWebRequest(arg0, arg1, arg2, arg3);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Uri), typeof(string), typeof(UnityEngine.Networking.DownloadHandler), typeof(UnityEngine.Networking.UploadHandler)))
                        {
                            System.Uri arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            UnityEngine.Networking.DownloadHandler arg2;
                            duk_get_classvalue(ctx, 2, out arg2);
                            UnityEngine.Networking.UploadHandler arg3;
                            duk_get_classvalue(ctx, 3, out arg3);
                            var o = new UnityEngine.Networking.UnityWebRequest(arg0, arg1, arg2, arg3);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            var o = new UnityEngine.Networking.UnityWebRequest(arg0, arg1);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Uri), typeof(string)))
                        {
                            System.Uri arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            var o = new UnityEngine.Networking.UnityWebRequest(arg0, arg1);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var o = new UnityEngine.Networking.UnityWebRequest(arg0);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Uri)))
                        {
                            System.Uri arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var o = new UnityEngine.Networking.UnityWebRequest(arg0);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 0)
                    {
                        var o = new UnityEngine.Networking.UnityWebRequest();
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                } while (false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Dispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.Dispose();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SendWebRequest(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.SendWebRequest();
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Abort(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.Abort();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetRequestHeader(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.GetRequestHeader(arg0);
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetRequestHeader(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                self.SetRequestHeader(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetResponseHeader(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.GetResponseHeader(arg0);
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetResponseHeaders(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetResponseHeaders();
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_ClearCookieCache(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        System.Uri arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Networking.UnityWebRequest.ClearCookieCache(arg0);
                        return 0;
                    }
                    if (argc == 0)
                    {
                        UnityEngine.Networking.UnityWebRequest.ClearCookieCache();
                        return 0;
                    }
                } while (false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Get(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);

                if (argc == 1)
                {

                    string arg0;
                    arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                    var ret = UnityEngine.Networking.UnityWebRequest.Get(arg0);
                    duk_push_classvalue(ctx, ret);
                    return 1;
                }
                if (argc == 2)
                {
                    string arg0;
                    arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                    string arg1;
                    arg1 = DuktapeDLL.duk_get_string(ctx, 0);
                    //params 不兼容
                    var ret = UnityEngine.Networking.UnityWebRequest.Get(arg0);
                    duk_push_classvalue(ctx, ret);
                    return 1;
                }
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Post(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                if (argc == 2)
                {
                    string arg0;
                    arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                    string arg1;
                    arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                    var ret = UnityEngine.Networking.UnityWebRequest.Post(arg0, arg1);
                    duk_push_classvalue(ctx, ret);
                    return 1;
                }     
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_EscapeURL(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.Text.Encoding arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = UnityEngine.Networking.UnityWebRequest.EscapeURL(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = UnityEngine.Networking.UnityWebRequest.EscapeURL(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_UnEscapeURL(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        System.Text.Encoding arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var ret = UnityEngine.Networking.UnityWebRequest.UnEscapeURL(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = UnityEngine.Networking.UnityWebRequest.UnEscapeURL(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_SerializeFormSections(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                byte[] arg1;
                duk_get_primitive_array(ctx, 1, out arg1);
                var ret = UnityEngine.Networking.UnityWebRequest.SerializeFormSections(arg0, arg1);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_GenerateBoundary(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Networking.UnityWebRequest.GenerateBoundary();
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_SerializeSimpleForm(IntPtr ctx)
        {
            try
            {
                System.Collections.Generic.Dictionary<string, string> arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = UnityEngine.Networking.UnityWebRequest.SerializeSimpleForm(arg0);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_disposeCertificateHandlerOnDispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.disposeCertificateHandlerOnDispose;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_disposeCertificateHandlerOnDispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.disposeCertificateHandlerOnDispose = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_disposeDownloadHandlerOnDispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.disposeDownloadHandlerOnDispose;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_disposeDownloadHandlerOnDispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.disposeDownloadHandlerOnDispose = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_disposeUploadHandlerOnDispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.disposeUploadHandlerOnDispose;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_disposeUploadHandlerOnDispose(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.disposeUploadHandlerOnDispose = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_method(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.method;
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_method(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string value;
                value = DuktapeDLL.duk_get_string(ctx, 0);
                self.method = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_error(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.error;
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_useHttpContinue(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.useHttpContinue;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_useHttpContinue(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.useHttpContinue = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_url(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.url;
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_url(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string value;
                value = DuktapeDLL.duk_get_string(ctx, 0);
                self.url = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_uri(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.uri;
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_uri(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                System.Uri value;
                duk_get_classvalue(ctx, 0, out value);
                self.uri = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_responseCode(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.responseCode;
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
        public static int BindRead_uploadProgress(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.uploadProgress;
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
        public static int BindRead_isModifiable(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isModifiable;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_isDone(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isDone;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_isNetworkError(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isNetworkError;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_isHttpError(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isHttpError;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_downloadProgress(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.downloadProgress;
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
        public static int BindRead_uploadedBytes(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.uploadedBytes;
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
        public static int BindRead_downloadedBytes(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.downloadedBytes;
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
        public static int BindRead_redirectLimit(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.redirectLimit;
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
        public static int BindWrite_redirectLimit(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.redirectLimit = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_chunkedTransfer(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.chunkedTransfer;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_chunkedTransfer(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.chunkedTransfer = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_uploadHandler(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.uploadHandler;
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_uploadHandler(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Networking.UploadHandler value;
                duk_get_classvalue(ctx, 0, out value);
                self.uploadHandler = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_downloadHandler(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                //返回string
                var ret = self.downloadHandler.text;
                duk_push_primitive(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_downloadHandler(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Networking.DownloadHandler value;
                duk_get_classvalue(ctx, 0, out value);
                self.downloadHandler = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_certificateHandler(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.certificateHandler;
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_certificateHandler(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Networking.CertificateHandler value;
                duk_get_classvalue(ctx, 0, out value);
                self.certificateHandler = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_timeout(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.timeout;
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
        public static int BindWrite_timeout(IntPtr ctx)
        {
            try
            {
                UnityEngine.Networking.UnityWebRequest self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.timeout = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Send(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                if (argc == 6)
                {
                    string arg0;
                    arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                    string arg1;
                    arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                    string arg2;
                    arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                    string arg3;
                    arg3 = DuktapeDLL.duk_get_string(ctx, 3);
                    Duktape.DuktapeObject arg4;
                    duk_get_classvalue(ctx, 4, out arg4);
                    Duktape.DuktapeObject arg5;
                    duk_get_classvalue(ctx, 5, out arg5);
                    //Duktape.DuktapeObject arg0;
                    //duk_get_classvalue(ctx, 0, out arg0);
                    //Duktape.DuktapeObject arg1;
                    //duk_get_classvalue(ctx, 1, out arg1);
                    //InsightDebug.Log("httpback", arg0);
                    //InsightDebug.Log("httpback", arg1);
                    //InsightDebug.Log("httpback", arg2);
                    //InsightDebug.Log("httpback", arg3);
                    //InsightDebug.Log("httpback", arg4.ToString());
                    //InsightDebug.Log("httpback", arg5.ToString());
                    LsHttpNetWorkWithNative.Instance.lsNetworkRequest(arg0, arg1, arg2, arg3, arg4, arg5);
                    return 0;
                }
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DownloadFile(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                if (argc == 6)
                {
                    string arg0;
                    arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                    string arg1;
                    arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                    Duktape.DuktapeObject arg2;
                    duk_get_classvalue(ctx, 2, out arg2);
                    Duktape.DuktapeObject arg3;
                    duk_get_classvalue(ctx, 3, out arg3);
                    Duktape.DuktapeObject arg4;
                    duk_get_classvalue(ctx, 4, out arg4);
                    Duktape.DuktapeObject arg5;
                    duk_get_classvalue(ctx, 5, out arg5);
                    LsDownloadWithNative.Instance.lsDowndloadRequest(arg0, arg1, arg2, arg3, arg4, arg5);
                    return 0;
                }
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
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
            duk_begin_class(ctx, "UnityWebRequest", typeof(UnityEngine.Networking.UnityWebRequest), BindConstructor);
            duk_add_method(ctx, "Get", BindStatic_Get, -2);
            duk_add_method(ctx, "Post", BindStatic_Post, -2);
            //内容层调用网络，通过NATIVE实现
            //Insight.UnityWebRequest.Send(url,method,header,body, <func>Callback)
            duk_add_method(ctx, "Send", BindStatic_Send, -2);
            //内容下载内容
            duk_add_method(ctx, "DownloadFile", BindStatic_DownloadFile, -2);
            duk_add_property(ctx, "error", BindRead_error, null, -1);
            duk_add_property(ctx, "isDone", BindRead_isDone, null, -1);
            duk_add_property(ctx, "isHttpError", BindRead_isHttpError, null, -1);         
            duk_add_property(ctx, "isNetworkError", BindRead_isNetworkError, null, -1);
            duk_add_property(ctx, "uri", BindRead_uri, BindWrite_uri, -1);
            duk_add_property(ctx, "url", BindRead_url, BindWrite_url, -1);
            duk_add_property(ctx, "method", BindRead_method, BindWrite_method, -1);
            duk_add_property(ctx, "responseCode", BindRead_responseCode, null, -1);
            duk_add_property(ctx, "downloadedBytes", BindRead_downloadedBytes, null, -1); 
            duk_add_property(ctx, "downloadHandler", BindRead_downloadHandler, BindWrite_downloadHandler, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
