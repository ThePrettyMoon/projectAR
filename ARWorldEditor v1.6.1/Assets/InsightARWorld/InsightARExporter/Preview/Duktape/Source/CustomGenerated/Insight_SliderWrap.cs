//#if UNITY_ANDROID
// Assembly: UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.UI.Slider
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Slider : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetValueWithoutNotify(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.SetValueWithoutNotify(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Rebuild(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.CanvasUpdate arg0;
                arg0 = (UnityEngine.UI.CanvasUpdate)DuktapeDLL.duk_get_int(ctx, 0);
                self.Rebuild(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_LayoutComplete(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.LayoutComplete();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GraphicUpdateComplete(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.GraphicUpdateComplete();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnPointerDown(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.PointerEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnPointerDown(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnDrag(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.PointerEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnDrag(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnMove(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.AxisEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnMove(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_FindSelectableOnLeft(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.FindSelectableOnLeft();
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
        public static int Bind_FindSelectableOnRight(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.FindSelectableOnRight();
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
        public static int Bind_FindSelectableOnUp(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.FindSelectableOnUp();
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
        public static int Bind_FindSelectableOnDown(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.FindSelectableOnDown();
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
        public static int Bind_OnInitializePotentialDrag(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.PointerEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnInitializePotentialDrag(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetDirection(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.Slider.Direction arg0;
                arg0 = (UnityEngine.UI.Slider.Direction)DuktapeDLL.duk_get_int(ctx, 0);
                bool arg1;
                arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                self.SetDirection(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_fillRect(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.fillRect;
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
        public static int BindWrite_fillRect(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.RectTransform value;
                duk_get_classvalue(ctx, 0, out value);
                self.fillRect = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_handleRect(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.handleRect;
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
        public static int BindWrite_handleRect(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.RectTransform value;
                duk_get_classvalue(ctx, 0, out value);
                self.handleRect = value;
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
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.direction;
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
        public static int BindWrite_direction(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.Slider.Direction value;
                value = (UnityEngine.UI.Slider.Direction)DuktapeDLL.duk_get_int(ctx, 0);
                self.direction = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_minValue(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.minValue;
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
        public static int BindWrite_minValue(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.minValue = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_maxValue(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.maxValue;
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
        public static int BindWrite_maxValue(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.maxValue = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_wholeNumbers(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.wholeNumbers;
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
        public static int BindWrite_wholeNumbers(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.wholeNumbers = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_value(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.value;
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
        public static int BindWrite_value(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.value = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_normalizedValue(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.normalizedValue;
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
        public static int BindWrite_normalizedValue(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float value;
                value = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.normalizedValue = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_OnValueChanged(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Slider self;
                duk_get_classvalue(ctx, 0, out self);
                DuktapeObject moduleObject, funcObject;
                duk_get_classvalue(ctx, 1, out moduleObject);
                duk_get_classvalue(ctx, 2, out funcObject);
                self.onValueChanged.AddListener((float value) =>
                {
                    IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                    DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObject.heapPtr, value);
                });
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
            duk_begin_class(ctx, "Slider", typeof(UnityEngine.UI.Slider), object_private_ctor);
            //property
            duk_add_property(ctx, "fillRect", BindRead_fillRect, BindWrite_fillRect, -1);
            duk_add_property(ctx, "handleRect", BindRead_handleRect, BindWrite_handleRect, -1);
            duk_add_property(ctx, "direction", BindRead_direction, BindWrite_direction, -1);
            duk_add_property(ctx, "minValue", BindRead_minValue, BindWrite_minValue, -1);
            duk_add_property(ctx, "maxValue", BindRead_maxValue, BindWrite_maxValue, -1);
            duk_add_property(ctx, "wholeNumbers", BindRead_wholeNumbers, BindWrite_wholeNumbers, -1);
            duk_add_property(ctx, "value", BindRead_value, BindWrite_value, -1);
            duk_add_property(ctx, "normalizedValue", BindRead_normalizedValue, BindWrite_normalizedValue, -1);
            //method
            duk_add_method(ctx, "setValueWithoutNotify", Bind_SetValueWithoutNotify, -1);
            duk_add_method(ctx, "setDirection", Bind_SetDirection, -1);

            //static
            duk_add_method(ctx, "OnValueChanged", BindStatic_OnValueChanged, -2);


            //duk_add_method(ctx, "setValueWithoutNotify", Bind_SetValueWithoutNotify, -1);
            //duk_add_method(ctx, "rebuild", Bind_Rebuild, -1);
            //duk_add_method(ctx, "layoutComplete", Bind_LayoutComplete, -1);
            //duk_add_method(ctx, "graphicUpdateComplete", Bind_GraphicUpdateComplete, -1);
            //duk_add_method(ctx, "onPointerDown", Bind_OnPointerDown, -1);
            //duk_add_method(ctx, "onDrag", Bind_OnDrag, -1);
            //duk_add_method(ctx, "onMove", Bind_OnMove, -1);
            //duk_add_method(ctx, "findSelectableOnLeft", Bind_FindSelectableOnLeft, -1);
            //duk_add_method(ctx, "findSelectableOnRight", Bind_FindSelectableOnRight, -1);
            //duk_add_method(ctx, "findSelectableOnUp", Bind_FindSelectableOnUp, -1);
            //duk_add_method(ctx, "findSelectableOnDown", Bind_FindSelectableOnDown, -1);
            //duk_add_method(ctx, "onInitializePotentialDrag", Bind_OnInitializePotentialDrag, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
