//#if UNITY_ANDROID
// Assembly: UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.UI.Selectable
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Selectable : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_IsInteractable(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.IsInteractable();
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
        public static int Bind_FindSelectable(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                var ret = self.FindSelectable(arg0);
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
        public static int Bind_FindSelectableOnLeft(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
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
                UnityEngine.UI.Selectable self;
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
                UnityEngine.UI.Selectable self;
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
                UnityEngine.UI.Selectable self;
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
        public static int Bind_OnMove(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
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
        public static int Bind_OnPointerDown(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
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
        public static int Bind_OnPointerUp(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.PointerEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnPointerUp(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnPointerEnter(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.PointerEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnPointerEnter(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnPointerExit(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.PointerEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnPointerExit(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnSelect(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.BaseEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnSelect(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_OnDeselect(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.EventSystems.BaseEventData arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.OnDeselect(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Select(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                self.Select();
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_AllSelectablesNoAlloc(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable[] arg0;
                duk_get_classvalue_array(ctx, 0, out arg0);
                var ret = UnityEngine.UI.Selectable.AllSelectablesNoAlloc(arg0);
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
        public static int BindStaticRead_allSelectablesArray(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.UI.Selectable.allSelectablesArray;
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
        public static int BindStaticRead_allSelectableCount(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.UI.Selectable.allSelectableCount;
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
        public static int BindRead_navigation(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.navigation;
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
        public static int BindWrite_navigation(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.Navigation value;
                duk_get_structvalue(ctx, 0, out value);
                self.navigation = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_transition(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.transition;
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
        public static int BindWrite_transition(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.Selectable.Transition value;
                value = (UnityEngine.UI.Selectable.Transition)DuktapeDLL.duk_get_int(ctx, 0);
                self.transition = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_colors(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.colors;
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
        public static int BindWrite_colors(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.ColorBlock value;
                duk_get_structvalue(ctx, 0, out value);
                self.colors = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_spriteState(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.spriteState;
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
        public static int BindWrite_spriteState(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.SpriteState value;
                duk_get_structvalue(ctx, 0, out value);
                self.spriteState = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_animationTriggers(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.animationTriggers;
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
        public static int BindWrite_animationTriggers(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.AnimationTriggers value;
                duk_get_classvalue(ctx, 0, out value);
                self.animationTriggers = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_targetGraphic(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.targetGraphic;
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
        public static int BindWrite_targetGraphic(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.Graphic value;
                duk_get_classvalue(ctx, 0, out value);
                self.targetGraphic = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_interactable(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.interactable;
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
        public static int BindWrite_interactable(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.interactable = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_image(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.image;
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
        public static int BindWrite_image(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.UI.Image value;
                duk_get_classvalue(ctx, 0, out value);
                self.image = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_animator(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Selectable self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.animator;
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
            duk_begin_class(ctx, "Selectable", typeof(UnityEngine.UI.Selectable), object_private_ctor);
            duk_add_method(ctx, "isInteractable", Bind_IsInteractable, -1);
            duk_add_method(ctx, "findSelectable", Bind_FindSelectable, -1);
            duk_add_method(ctx, "findSelectableOnLeft", Bind_FindSelectableOnLeft, -1);
            duk_add_method(ctx, "findSelectableOnRight", Bind_FindSelectableOnRight, -1);
            duk_add_method(ctx, "findSelectableOnUp", Bind_FindSelectableOnUp, -1);
            duk_add_method(ctx, "findSelectableOnDown", Bind_FindSelectableOnDown, -1);
            duk_add_method(ctx, "onMove", Bind_OnMove, -1);
            duk_add_method(ctx, "onPointerDown", Bind_OnPointerDown, -1);
            duk_add_method(ctx, "onPointerUp", Bind_OnPointerUp, -1);
            duk_add_method(ctx, "onPointerEnter", Bind_OnPointerEnter, -1);
            duk_add_method(ctx, "onPointerExit", Bind_OnPointerExit, -1);
            duk_add_method(ctx, "onSelect", Bind_OnSelect, -1);
            duk_add_method(ctx, "onDeselect", Bind_OnDeselect, -1);
            duk_add_method(ctx, "select", Bind_Select, -1);
            duk_add_method(ctx, "AllSelectablesNoAlloc", BindStatic_AllSelectablesNoAlloc, -2);
            duk_add_property(ctx, "allSelectablesArray", BindStaticRead_allSelectablesArray, null, -2);
            duk_add_property(ctx, "allSelectableCount", BindStaticRead_allSelectableCount, null, -2);
            duk_add_property(ctx, "navigation", BindRead_navigation, BindWrite_navigation, -1);
            duk_add_property(ctx, "transition", BindRead_transition, BindWrite_transition, -1);
            duk_add_property(ctx, "colors", BindRead_colors, BindWrite_colors, -1);
            duk_add_property(ctx, "spriteState", BindRead_spriteState, BindWrite_spriteState, -1);
            duk_add_property(ctx, "animationTriggers", BindRead_animationTriggers, BindWrite_animationTriggers, -1);
            duk_add_property(ctx, "targetGraphic", BindRead_targetGraphic, BindWrite_targetGraphic, -1);
            duk_add_property(ctx, "interactable", BindRead_interactable, BindWrite_interactable, -1);
            duk_add_property(ctx, "image", BindRead_image, BindWrite_image, -1);
            duk_add_property(ctx, "animator", BindRead_animator, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
