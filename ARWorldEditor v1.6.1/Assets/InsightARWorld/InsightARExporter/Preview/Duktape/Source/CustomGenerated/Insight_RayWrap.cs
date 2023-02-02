//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Ray
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Ray : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                UnityEngine.Vector3 arg1;
                duk_get_structvalue(ctx, 1, out arg1);
                var o = new UnityEngine.Ray(arg0, arg1);
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
        public static int Bind_GetPoint(IntPtr ctx)
        {
            try
            {
                UnityEngine.Ray self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                var ret = self.GetPoint(arg0);
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
        public static int Bind_ToString(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        UnityEngine.Ray self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_structvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        var ret = self.ToString(arg0);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        UnityEngine.Ray self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_structvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.ToString();
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
        public static int BindRead_origin(IntPtr ctx)
        {
            try
            {
                UnityEngine.Ray self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.origin;
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
        public static int BindWrite_origin(IntPtr ctx)
        {
            try
            {
                UnityEngine.Ray self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.origin = MathConverter.ToVector3(value);
                duk_rebind_this(ctx, self);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_direction(IntPtr ctx)
        {
            try
            {
                UnityEngine.Ray self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.direction;
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
        public static int BindWrite_direction(IntPtr ctx)
        {
            try
            {
                UnityEngine.Ray self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.direction =MathConverter.ToVector3(value);
                duk_rebind_this(ctx, self);
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
            duk_begin_class(ctx, "Ray", typeof(UnityEngine.Ray), BindConstructor);
            duk_add_method(ctx, "getPoint", Bind_GetPoint, -1);
            duk_add_method(ctx, "toString", Bind_ToString, -1);
            duk_add_property(ctx, "origin", BindRead_origin, BindWrite_origin, -1);
            duk_add_property(ctx, "direction", BindRead_direction, BindWrite_direction, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
