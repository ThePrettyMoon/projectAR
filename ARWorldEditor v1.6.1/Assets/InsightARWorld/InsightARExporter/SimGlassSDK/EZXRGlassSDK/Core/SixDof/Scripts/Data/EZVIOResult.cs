using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EZVIOResult
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] camTransform;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] Twc;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] quaternion;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] translation;
        public double timestamp;
        public double pose_quality;
        public EZVIOState vioState;
        public EZVIOStateReason vioReason;
    }
}
