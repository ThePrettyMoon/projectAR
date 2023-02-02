//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Bounds
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Bounds : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                var o = new UnityEngine.Bounds(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1));
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
        public static int Bind_SetMinMax(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                self.SetMinMax(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1));
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
        public static int Bind_IntersectRay(IntPtr ctx)
        {
            try
            {

                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Ray arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                var ret = self.IntersectRay(arg0);
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
        public static int Bind_Contains(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.Contains(MathConverter.ToVector3(arg0));
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
        public static int BindRead_center(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.center;
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
        public static int BindWrite_center(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.center = MathConverter.ToVector3(value);
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
        public static int BindRead_size(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.size;
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
        public static int BindWrite_size(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.size =MathConverter.ToVector3(value);
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
        public static int BindRead_extents(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.extents;
                duk_push_classvalue(ctx,MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_extents(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.extents = MathConverter.ToVector3(value);
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
        public static int BindRead_min(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.min;
                duk_push_classvalue(ctx,MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_min(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.min = MathConverter.ToVector3(value);
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
        public static int BindRead_max(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.max;
                duk_push_classvalue(ctx,MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_max(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.max = MathConverter.ToVector3(value);
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
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                UnityEngine.Bounds self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ToString();
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
            duk_begin_class(ctx, "Bounds", typeof(UnityEngine.Bounds), BindConstructor);
            duk_add_property(ctx, "center", BindRead_center, BindWrite_center, -1);
            duk_add_property(ctx, "extents", BindRead_extents, BindWrite_extents, -1);
            duk_add_property(ctx, "max", BindRead_max, BindWrite_max, -1);
            duk_add_property(ctx, "min", BindRead_min, BindWrite_min, -1);
            duk_add_property(ctx, "size", BindRead_size, BindWrite_size, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "contains", Bind_Contains, -1);
            duk_add_method(ctx, "intersectRay", Bind_IntersectRay, -1);
            duk_add_method(ctx, "setMinMax", Bind_SetMinMax, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
