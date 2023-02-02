//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.RectInt
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_RectInt : DuktapeBinding {
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
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        int arg3;
                        arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                        var o = new UnityEngine.RectInt(arg0, arg1, arg2, arg3);
                        duk_bind_native(ctx, o);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Vector2Int arg0;
                        duk_get_structvalue(ctx, 0, out arg0);
                        UnityEngine.Vector2Int arg1;
                        duk_get_structvalue(ctx, 1, out arg1);
                        var o = new UnityEngine.RectInt(arg0, arg1);
                        duk_bind_native(ctx, o);
                        return 0;
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
        public static int Bind_SetMinMax(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector2 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector2 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                self.SetMinMax(new UnityEngine.Vector2Int((int)arg0.x, (int)arg0.y), new UnityEngine.Vector2Int((int)arg1.x, (int)arg1.y));
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
        public static int Bind_ClampToBounds(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.RectInt arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                self.ClampToBounds(arg0);
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
        public static int Bind_Contains(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector2 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                var ret = self.Contains(new UnityEngine.Vector2Int((int)arg0.x, (int)arg0.y));
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
        public static int Bind_ToString(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_x(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.x;
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
        public static int BindWrite_x(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.x = value;
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
        public static int BindRead_y(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.y;
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
        public static int BindWrite_y(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.y = value;
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
        public static int BindRead_center(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.center;
                duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                return 1;
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
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.min;
                duk_push_structvalue(ctx, ret);
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
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector2Int value;
                duk_get_structvalue(ctx, 0, out value);
                self.min = value;
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
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.max;
                duk_push_structvalue(ctx, ret);
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
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector2Int value;
                duk_get_structvalue(ctx, 0, out value);
                self.max = value;
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
        public static int BindRead_width(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.width;
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
        public static int BindWrite_width(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.width = value;
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
        public static int BindRead_height(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.height;
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
        public static int BindWrite_height(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.height = value;
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
        public static int BindRead_xMin(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.xMin;
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
        public static int BindWrite_xMin(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.xMin = value;
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
        public static int BindRead_yMin(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.yMin;
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
        public static int BindWrite_yMin(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.yMin = value;
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
        public static int BindRead_xMax(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.xMax;
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
        public static int BindWrite_xMax(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.xMax = value;
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
        public static int BindRead_yMax(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.yMax;
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
        public static int BindWrite_yMax(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.yMax = value;
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
        public static int BindRead_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.position;
                duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector2 value;
                duk_get_classvalue(ctx, 0, out value);
                self.position = new UnityEngine.Vector2Int((int)value.x,(int)value.y);
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
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.size;
                duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
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
                UnityEngine.RectInt self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector2 value;
                duk_get_classvalue(ctx, 0, out value);
                self.size = new UnityEngine.Vector2Int((int)value.x, (int)value.y); 
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
            duk_begin_class(ctx, "RectInt", typeof(UnityEngine.RectInt), BindConstructor);
            duk_add_property(ctx, "center", BindRead_center, null, -1);
            duk_add_property(ctx, "height", BindRead_height, BindWrite_height, -1);
            duk_add_property(ctx, "max", BindRead_max, BindWrite_max, -1);
            duk_add_property(ctx, "min", BindRead_min, BindWrite_min, -1);
            duk_add_property(ctx, "position", BindRead_position, BindWrite_position, -1);
            duk_add_property(ctx, "size", BindRead_size, BindWrite_size, -1);
            duk_add_property(ctx, "width", BindRead_width, BindWrite_width, -1);
            duk_add_property(ctx, "x", BindRead_x, BindWrite_x, -1);
            duk_add_property(ctx, "xMax", BindRead_xMax, BindWrite_xMax, -1);
            duk_add_property(ctx, "xMin", BindRead_xMin, BindWrite_xMin, -1);
            duk_add_property(ctx, "y", BindRead_y, BindWrite_y, -1);
            duk_add_property(ctx, "yMax", BindRead_yMax, BindWrite_yMax, -1);
            duk_add_property(ctx, "yMin", BindRead_yMin, BindWrite_yMin, -1);
            duk_add_method(ctx, "toString", Bind_ToString, -1);
            duk_add_method(ctx, "contains", Bind_Contains, -1);
            duk_add_method(ctx, "clampToBounds", Bind_ClampToBounds, -1);
            duk_add_method(ctx, "setMinMax", Bind_SetMinMax, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
