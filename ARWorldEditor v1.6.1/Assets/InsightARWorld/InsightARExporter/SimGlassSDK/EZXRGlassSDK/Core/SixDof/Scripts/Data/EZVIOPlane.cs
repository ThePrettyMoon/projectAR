using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public struct EZVIOPlane
    {
        public int id;              // identifier of this detected plane                        [used in Unity]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] center;        // plane center in world frame
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] normal;        // rotation part of transform, presented as quaternion
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] transform;  // transform a point from world frame to plane frame
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] rotation;        // rotation part of transform, presented as quaternion
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] transform_unity;  // transform a point from world frame to plane frame   [used in Unity]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public float[] rect;       // vertices of minimum bounding rectangle                    [used in Unity]
}
}
