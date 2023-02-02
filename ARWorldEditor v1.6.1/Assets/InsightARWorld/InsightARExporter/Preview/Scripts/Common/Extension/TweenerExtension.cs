using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Newtonsoft.Json.Utilities;

/// <summary>
/// tweener extension 
/// </summary>
public static class TweenerExtension
{
    public static DG.Tweening.Tweener SetLoops(this Tweener tweener, int loops, LoopType loopType)
    {
        int loopTypeInt = (int)loopType;
        return tweener.SetLoops(loops, (DG.Tweening.LoopType)loopTypeInt);
    }

    public static DG.Tweening.Tweener SetEase(this Tweener tweener, Ease ease)
    {
        int easeInt = (int)ease;
        return  tweener.SetEase((DG.Tweening.Ease)easeInt);
    }

    public static DG.Tweening.Tweener SetRelative(this Tweener tweener, bool relative = true)
    {
       return tweener.SetRelative(relative);
    }
    public static void Kill(Tweener tweener)
    {
        tweener.Kill();
    }
}
