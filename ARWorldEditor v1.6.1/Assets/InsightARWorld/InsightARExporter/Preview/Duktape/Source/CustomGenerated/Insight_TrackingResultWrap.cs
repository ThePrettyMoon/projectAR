//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.TrackingResult
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_TrackingResult : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new TrackingResult();
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
        public static int BindRead_point(IntPtr ctx)
        {
            try
            {
                TrackingResult self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.point;
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
        public static int BindWrite_point(IntPtr ctx)
        {
            try
            {
                TrackingResult self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.point = MathConverter.ToVector3( value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_tracked(IntPtr ctx)
        {
            try
            {
                TrackingResult self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.tracked;
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
        public static int BindWrite_tracked(IntPtr ctx)
        {
            try
            {
                TrackingResult self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.tracked = value;
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
            duk_begin_class(ctx, "TrackingResult", typeof(TrackingResult), BindConstructor);
            duk_add_property(ctx, "point", BindRead_point, null, -1);
            duk_add_property(ctx, "tracked", BindRead_tracked, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
