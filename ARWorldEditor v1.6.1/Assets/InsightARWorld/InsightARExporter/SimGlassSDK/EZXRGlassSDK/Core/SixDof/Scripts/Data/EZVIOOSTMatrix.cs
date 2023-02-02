using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public struct OSTMatrices
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] T_TrackCam_Head;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] T_RightEye_Head;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] T_LeftEye_Head;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] T_RGB_Head;
    }
}
