using EZXR.Glass.SixDof;
using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUIRenderer : MonoBehaviour
{
    public Camera phoneUICamera;


    private void Start()
    {
        if (Display.displays.Length > 1)
        {
            phoneUICamera.SetTargetBuffers(Display.displays[0].colorBuffer, Display.displays[0].depthBuffer);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}