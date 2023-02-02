using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// 设置transform 可见性
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="enabled"></param>
    public static void SetVisible(this Transform trans,bool enabled)
    {
        if (trans != null)
        {
            if (enabled && !trans.gameObject.activeSelf)
            {
                trans.gameObject.SetActive(enabled);
            }

            if (!enabled && trans.gameObject.activeSelf)
            {
                trans.gameObject.SetActive(enabled);
            }
        }
    }
}
