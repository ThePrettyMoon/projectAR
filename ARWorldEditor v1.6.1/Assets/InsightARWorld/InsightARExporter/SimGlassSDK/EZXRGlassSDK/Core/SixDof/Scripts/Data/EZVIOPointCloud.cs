using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System;

namespace EZXR.Glass.SixDof
{
    public struct EZVIOPointCloud
    {
        public bool is_valid;
        public long num_points;      // total number of map points in this point cloud
        public IntPtr points;     // head pointer for this point cloud
        public MapPoint3D[] points3d;
    }
}
