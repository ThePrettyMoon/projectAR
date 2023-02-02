//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Camera
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    using UnityEngine;

    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Camera : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Camera();
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
        public static int Bind_WorldToScreenPoint(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Camera.MonoOrStereoscopicEye arg1;
                        arg1 = (UnityEngine.Camera.MonoOrStereoscopicEye)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.WorldToScreenPoint(MathConverter.ToVector3(arg0), arg1);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.WorldToScreenPoint(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
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
        public static int Bind_WorldToViewportPoint(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Camera.MonoOrStereoscopicEye arg1;
                        arg1 = (UnityEngine.Camera.MonoOrStereoscopicEye)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.WorldToViewportPoint(MathConverter.ToVector3(arg0), arg1);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.WorldToViewportPoint(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
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
        public static int Bind_ViewportToWorldPoint(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Camera.MonoOrStereoscopicEye arg1;
                        arg1 = (UnityEngine.Camera.MonoOrStereoscopicEye)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.ViewportToWorldPoint(MathConverter.ToVector3(arg0), arg1);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ViewportToWorldPoint(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
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
        public static int Bind_ScreenToWorldPoint(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Camera.MonoOrStereoscopicEye arg1;
                        arg1 = (UnityEngine.Camera.MonoOrStereoscopicEye)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.ScreenToWorldPoint(MathConverter.ToVector3(arg0), arg1);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ScreenToWorldPoint(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
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
        public static int Bind_ScreenToViewportPoint(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.ScreenToViewportPoint(MathConverter.ToVector3(arg0));
                duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ViewportToScreenPoint(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.ViewportToScreenPoint(MathConverter.ToVector3(arg0));
                duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ViewportPointToRay(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Camera.MonoOrStereoscopicEye arg1;
                        arg1 = (UnityEngine.Camera.MonoOrStereoscopicEye)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.ViewportPointToRay(MathConverter.ToVector3(arg0), arg1);
                        duk_push_structvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ViewportPointToRay(MathConverter.ToVector3(arg0));
                        duk_push_structvalue(ctx, ret);
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
        public static int Bind_ScreenPointToRay(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        UnityEngine.Camera.MonoOrStereoscopicEye arg1;
                        arg1 = (UnityEngine.Camera.MonoOrStereoscopicEye)DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.ScreenPointToRay(MathConverter.ToVector3(arg0), arg1);
                        duk_push_structvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Camera self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.ScreenPointToRay(MathConverter.ToVector3(arg0));
                        duk_push_structvalue(ctx, ret);
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
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.toString(self);
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
        public static int Bind_getPostEffectMaterial(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = Insight.CameraExtension.getPostEffectMaterial(self, arg0);
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
        public static int Bind_estimationSHIntensity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.estimationSHIntensity(self);
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
        public static int Bind_worldToCameraMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.worldToCameraMatrix(self);
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
        public static int Bind_getEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.getEnabled(self);
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
        public static int Bind_setEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool arg0;
                arg0 = DuktapeDLL.duk_get_boolean(ctx, 0);
                Insight.CameraExtension.setEnabled(self, arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_gameObject(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.gameObject(self);
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
        public static int Bind_isActiveAndEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.isActiveAndEnabled(self);
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
        public static int Bind_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.name(self);
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
        public static int Bind_tag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.tag(self);
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
        public static int Bind_transform(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.CameraExtension.transform(self);
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
        public static int BindRead_nearClipPlane(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.nearClipPlane;
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
        public static int BindRead_farClipPlane(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.farClipPlane;
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
        public static int BindRead_fieldOfView(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.fieldOfView;
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
        public static int BindRead_renderingPath(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.renderingPath;
                DuktapeDLL.duk_push_int(ctx, (int)ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_orthographic(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.orthographic;
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
        public static int BindRead_depth(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.depth;
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
        public static int BindRead_aspect(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.aspect;
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
        public static int BindWrite_aspect(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.aspect = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_cullingMask(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.cullingMask;
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
        public static int BindWrite_cullingMask(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.cullingMask = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_rect(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.rect; //return insight.vector4
                var vect = new Insight.Vector4(ret.x, ret.y, ret.width, ret.height);
                duk_push_classvalue(ctx, vect);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_pixelRect(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.pixelRect;
                Insight.Vector4 vect = new Insight.Vector4(ret.x, ret.y, ret.width,ret.height);
                duk_push_classvalue(ctx,vect);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_pixelWidth(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.pixelWidth;
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
        public static int BindRead_pixelHeight(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.pixelHeight;
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
        public static int BindRead_cameraToWorldMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.cameraToWorldMatrix;
                duk_push_classvalue(ctx, MathConverter.FromMatrix4x4(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_worldToCameraMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.worldToCameraMatrix;
                duk_push_classvalue(ctx, MathConverter.FromMatrix4x4(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_projectionMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.projectionMatrix;
                duk_push_classvalue(ctx, MathConverter.FromMatrix4x4(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_save(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                int arg2;
                arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                int arg3;
                arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                int arg4;
                arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                Insight.CameraExtension.save(self,arg0, arg1, arg2, arg3, arg4);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_CalculateObliqueMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Camera self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = MathConverter.FromMatrix4x4(self.CalculateObliqueMatrix(MathConverter.ToVector4( arg0)));
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
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "Camera", typeof(UnityEngine.Camera), BindConstructor);
            duk_add_property(ctx, "aspect", BindRead_aspect, BindWrite_aspect, -1);
            duk_add_property(ctx, "cameraToWorldMatrix", BindRead_cameraToWorldMatrix, null, -1);
            duk_add_property(ctx, "cullingMask", BindRead_cullingMask, BindWrite_cullingMask, -1);
            duk_add_property(ctx, "depth", BindRead_depth, null, -1);
            duk_add_property(ctx, "estimationSHIntensity", Bind_estimationSHIntensity, null, -1);
            duk_add_property(ctx, "farClipPlane", BindRead_farClipPlane, null, -1);
            duk_add_property(ctx, "fieldOfView", BindRead_fieldOfView, null, -1);
            duk_add_property(ctx, "nearClipPlane", BindRead_nearClipPlane, null, -1);
            duk_add_property(ctx, "orthographic", BindRead_orthographic, null, -1);
            duk_add_property(ctx, "pixelHeight", BindRead_pixelHeight, null, -1);
            duk_add_property(ctx, "pixelRect", BindRead_pixelRect, null, -1);
            duk_add_property(ctx, "pixelWidth", BindRead_pixelWidth, null, -1);
            duk_add_property(ctx, "projectionMatrix", BindRead_projectionMatrix, null, -1);
            duk_add_property(ctx, "rect", BindRead_rect, null, -1);
            duk_add_property(ctx, "worldToCameraMatrix", BindRead_worldToCameraMatrix, null, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "getPostEffectMaterial", Bind_getPostEffectMaterial, -1);
            duk_add_method(ctx, "screenPointToRay", Bind_ScreenPointToRay, -1);
            duk_add_method(ctx, "screenToViewportPoint", Bind_ScreenToViewportPoint, -1);
            duk_add_method(ctx, "viewportPointToRay", Bind_ViewportPointToRay, -1);
            duk_add_method(ctx, "viewportToScreenPoint", Bind_ViewportToScreenPoint, -1);
            duk_add_method(ctx, "viewportToWorldPoint", Bind_ViewportToWorldPoint, -1);
            duk_add_method(ctx, "worldToScreenPoint", Bind_WorldToScreenPoint, -1);
            duk_add_method(ctx, "save", Bind_save, -1);
            duk_add_method(ctx, "calculateObliqueMatrix", Bind_CalculateObliqueMatrix, -1);
            duk_add_method(ctx, "worldToViewportPoint", Bind_WorldToViewportPoint, -1);
            duk_add_method(ctx, "screenToWorldPoint", Bind_ScreenToWorldPoint, -1);
            duk_add_property(ctx, "enabled", Bind_getEnabled, Bind_setEnabled, -1);
            duk_add_property(ctx, "gameObject", Bind_gameObject, null, -1);
            duk_add_property(ctx, "isActiveAndEnabled", Bind_isActiveAndEnabled, null, -1);
            duk_add_property(ctx, "name", Bind_name, null, -1);
            duk_add_property(ctx, "tag", Bind_tag, null, -1);
            duk_add_property(ctx, "transform", Bind_transform, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
