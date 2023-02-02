//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.UI.ColorBlock
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_ColorBlock : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.UI.ColorBlock();
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
        public static int Bind_Equals(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(object)))
                        {
                            UnityEngine.UI.ColorBlock self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_structvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            object arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.Equals(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.UI.ColorBlock)))
                        {
                            UnityEngine.UI.ColorBlock self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_structvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            UnityEngine.UI.ColorBlock arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            var ret = self.Equals(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        break;
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
        public static int Bind_GetHashCode(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.GetHashCode();
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
        public static int Bind_tostring(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = ColorBlockExtension.toString(self);
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
        public static int BindRead_normalColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.normalColor;
                var color = new Insight.Vector4(ret.r, ret.b, ret.b, ret.a);
                duk_push_classvalue(ctx, color);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_normalColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 value;
                duk_get_classvalue(ctx, 0, out value);
                self.normalColor =new UnityEngine.Color( value.x,value.y,value.z,value.w);
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
        public static int BindRead_highlightedColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.highlightedColor;
                var color = new Insight.Vector4(ret.r, ret.g, ret.b, ret.a);
                duk_push_classvalue(ctx, color);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_highlightedColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 value;
                duk_get_classvalue(ctx, 0, out value);
                self.highlightedColor = new UnityEngine.Color(value.x,value.y,value.z,value.w);
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
        public static int BindRead_pressedColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.pressedColor;
                var color = new Insight.Vector4(ret.r, ret.g, ret.b, ret.a);
                duk_push_classvalue(ctx, color);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_pressedColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 value;
                duk_get_classvalue(ctx, 0, out value);
                self.pressedColor = new UnityEngine.Color( value.x,value.y,value.z,value.w);
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
        public static int BindRead_disabledColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.disabledColor;
                //Insight.Vector4
                var color = new Insight.Vector4(ret.r, ret.g, ret.b, ret.a);
                duk_push_classvalue(ctx, color);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_disabledColor(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 value;
                duk_get_classvalue(ctx, 0, out value);
                self.disabledColor = new UnityEngine.Color(value.x,value.y,value.z,value.w);
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
        public static int BindRead_colorMultiplier(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.colorMultiplier;
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
        public static int BindWrite_colorMultiplier(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.colorMultiplier = value;
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
        public static int BindRead_fadeDuration(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.fadeDuration;
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
        public static int BindWrite_fadeDuration(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.ColorBlock self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_structvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.fadeDuration = value;
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
        public static int BindStaticRead_defaultColorBlock(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.UI.ColorBlock.defaultColorBlock;
                duk_push_structvalue(ctx, ret);
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
            duk_begin_class(ctx, "ColorBlock", typeof(UnityEngine.UI.ColorBlock), BindConstructor);
            duk_add_property(ctx, "colorMultiplier", BindRead_colorMultiplier, BindWrite_colorMultiplier, -1);
            duk_add_property(ctx, "disabledColor", BindRead_disabledColor, BindWrite_disabledColor, -1);
            duk_add_property(ctx, "fadeDuration", BindRead_fadeDuration, BindWrite_fadeDuration, -1);
            duk_add_property(ctx, "highlightedColor", BindRead_highlightedColor, BindWrite_highlightedColor, -1);
            duk_add_property(ctx, "normalColor", BindRead_normalColor, BindWrite_normalColor, -1);
            duk_add_property(ctx, "pressedColor", BindRead_pressedColor, BindWrite_pressedColor, -1);
            duk_add_method(ctx, "toString", Bind_tostring, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
