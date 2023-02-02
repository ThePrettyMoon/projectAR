using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTween
{
    public static Tweener DOColor(Material mat, Vector4 colorTo, float duration)
    {
        Color color = MathUtility.Vector4ToColor(colorTo);
        return  mat.DOColor(color, duration);
    }

    public static Tweener DOColor(Material mat, Vector4 colorTo, string pro_name, float duration)
    {
        Color color = MathUtility.Vector4ToColor(colorTo);
        return mat.DOColor(color, pro_name, duration);
    }
    public static Tweener DOFade(UnityEngine.UI.Image gp, float fadeValue, float duration)
    {
        return gp.DOFade(fadeValue, duration);
    }

    public static Tweener DOVector(Material mat, Vector4 vectorTo, string pro_name, float duration)
    {
        return  mat.DOVector(vectorTo, pro_name, duration); 
    }

    public static Tweener DOFloat(Material mat, float floatTo, string pro_name, float duration)
    {
        return mat.DOFloat(floatTo, pro_name, duration);
    }

    public static Tweener DOMove(Transform trans, Vector3 moveTo, float duration)
    {
        return  trans.DOMove(moveTo, duration);
    }

    public static Tweener DOLocalMove(Transform trans, Vector3 moveTo, float duration)
    {
        return trans.DOLocalMove(moveTo, duration);
    }

    public static Tweener DORotate(Transform trans, Vector3 rotateTo, float duration)
    {
        return trans.DORotate(rotateTo, duration);
    }

    public static Tweener DOLocalRotate(Transform trans, Vector3 rotateTo, float duration)
    {
        return trans.DOLocalRotate(rotateTo, duration);
    }

    /// <summary>
    ///  DOScale already tweens a Transform's localScale because the only other option is lossyScale which can't be changed 
    /// </summary>
    /// <returns>The local scale.</returns>
    /// <param name="trans">Trans.</param>
    /// <param name="scaleTo">Scale to.</param>
    /// <param name="duration">Duration.</param>
    public static Tweener DOLocalScale(Transform trans, Vector3 scaleTo, float duration)
    {
        return trans.DOScale(scaleTo, duration);
    }
    public static Tweener DOText(UnityEngine.UI.Text tt, string text, float duration)
    {
        return tt.DOText(text, duration);
    }
}
