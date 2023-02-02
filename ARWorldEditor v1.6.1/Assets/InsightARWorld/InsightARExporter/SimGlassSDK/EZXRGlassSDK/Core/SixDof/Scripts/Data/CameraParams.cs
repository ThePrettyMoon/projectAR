using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public struct CamParams
    {
        public int width;
        public int height;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public double[] fov;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] leftProjection;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] rightProjection;
    }
}
