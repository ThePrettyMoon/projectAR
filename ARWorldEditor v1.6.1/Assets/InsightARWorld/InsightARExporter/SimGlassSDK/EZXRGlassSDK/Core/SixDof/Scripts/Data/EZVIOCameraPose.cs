using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public class EZVIOCamPos
    {
        //OpenGL coordinate
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        float[] quaternion_opengl;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        float[] center_opengl;
    }
}
