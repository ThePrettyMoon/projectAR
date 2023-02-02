//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.Dotween
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;
using Insight;

namespace DuktapeJS
{
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_Dotween : DuktapeBinding
    {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new DOTween();
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
        public static int BindStatic_DOColor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Material arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector4 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        string arg2;
                        arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        DG.Tweening.Tweener tweener = DOTween.DOColor(arg0, MathConverter.ToVector4(arg1), arg2, arg3);
                        duk_push_classvalue(ctx, tweener);
                        return 1;
                    }
                    if (argc == 3)
                    {
                        UnityEngine.Material arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector4 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        DG.Tweening.Tweener tweener = DOTween.DOColor(arg0, MathConverter.ToVector4(arg1), arg2);
                        duk_push_classvalue(ctx, tweener);
                        return 1;
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
        public static int BindStatic_DOFade(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Image arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DOFade(arg0, arg1, arg2);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOVector(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector4 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                string arg2;
                arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                float arg3;
                arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                DG.Tweening.Tweener tweener = DOTween.DOVector(arg0, MathConverter.ToVector4(arg1), arg2, arg3);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOFloat(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                string arg2;
                arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                float arg3;
                arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                DG.Tweening.Tweener tweener = DOTween.DOFloat(arg0, arg1, arg2, arg3);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOMove(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DOMove(arg0, MathConverter.ToVector3(arg1), arg2);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOLocalMove(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DOLocalMove(arg0, MathConverter.ToVector3(arg1), arg2);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DORotate(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DORotate(arg0, MathConverter.ToVector3(arg1), arg2);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOLocalRotate(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DOLocalRotate(arg0, MathConverter.ToVector3(arg1), arg2);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOLocalScale(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DOLocalScale(arg0, MathConverter.ToVector3(arg1), arg2);
                duk_push_classvalue(ctx, tweener);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_DOText(IntPtr ctx)
        {
            try
            {
                UnityEngine.UI.Text arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                DG.Tweening.Tweener tweener = DOTween.DOText(arg0, arg1, arg2);
                duk_push_classvalue(ctx, tweener);
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
            duk_begin_class(ctx, "DOTween", typeof(DOTween), BindConstructor);
            duk_add_method(ctx, "DOColor", BindStatic_DOColor, -2);
            duk_add_method(ctx, "DOFade", BindStatic_DOFade, -2);
            duk_add_method(ctx, "DOVector", BindStatic_DOVector, -2);
            duk_add_method(ctx, "DOFloat", BindStatic_DOFloat, -2);
            duk_add_method(ctx, "DOMove", BindStatic_DOMove, -2);
            duk_add_method(ctx, "DOLocalMove", BindStatic_DOLocalMove, -2);
            duk_add_method(ctx, "DORotate", BindStatic_DORotate, -2);
            duk_add_method(ctx, "DOLocalRotate", BindStatic_DOLocalRotate, -2);
            duk_add_method(ctx, "DOLocalScale", BindStatic_DOLocalScale, -2);
            duk_add_method(ctx, "DOText", BindStatic_DOText, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
