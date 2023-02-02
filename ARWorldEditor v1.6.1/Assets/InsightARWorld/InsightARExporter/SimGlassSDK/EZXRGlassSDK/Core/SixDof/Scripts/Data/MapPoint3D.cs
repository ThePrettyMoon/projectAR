using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public struct MapPoint3D
    {
        public long id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] worldPosition;
    }
}
