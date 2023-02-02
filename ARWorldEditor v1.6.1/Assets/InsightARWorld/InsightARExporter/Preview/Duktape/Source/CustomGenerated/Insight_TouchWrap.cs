//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Touch
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Touch : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Touch();
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
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                UnityEngine.Touch self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.TouchExtension.toString(self);
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
        public static int BindRead_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.Touch self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret =MathConverter.FromVector3( self.position);
                var vectPosition = new Insight.Vector2(ret.x, ret.y);
                //·µ»Øvector2
                duk_push_classvalue(ctx, vectPosition);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_deltaPosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Touch self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = MathConverter.FromVector2(self.deltaPosition);
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
        public static int BindRead_phase(IntPtr ctx)
        {
            try
            {
                UnityEngine.Touch self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.phase.ToString();
                DuktapeDLL.duk_push_string(ctx, ret);
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
            duk_begin_class(ctx, "Touch", typeof(UnityEngine.Touch), BindConstructor);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_property(ctx, "position", BindRead_position, null, -1);
            duk_add_property(ctx, "deltaPosition", BindRead_deltaPosition, null, -1);
            duk_add_property(ctx, "phase", BindRead_phase, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
