using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorExtension
{
    public static bool isPlaying(this Animator animator)
    {
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return animator.speed > 0 && (normalizedTime - Mathf.Floor(normalizedTime) < 1.0f);
    }

    public static float getNormlizedTime(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public static bool isActiveAndEnabled(this Animator animator)
    {
        return animator.enabled;
    }
}
