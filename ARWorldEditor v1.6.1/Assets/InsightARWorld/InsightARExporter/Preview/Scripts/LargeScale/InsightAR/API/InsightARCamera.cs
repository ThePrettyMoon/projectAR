using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

/// <summary>
/// ar camera 组件
/// </summary>
public class InsightARCamera:MonoBehaviour
{
    private Camera cam;

    public Camera GetCamera()
    {
        return cam;
    }

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    public void OnPreRender()
    {
        //if (InsightARManager.Instance.isRunning())
        //{
        //    InsightARManager.Instance.GetARInterface().UpdateARBackground();
        //}
    }

}
