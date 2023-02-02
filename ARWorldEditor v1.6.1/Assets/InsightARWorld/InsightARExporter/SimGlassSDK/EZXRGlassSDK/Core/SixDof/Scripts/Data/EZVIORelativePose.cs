using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    [StructLayout(LayoutKind.Sequential/*, Pack = 1*/)]
    public struct EZVIORelativePose
    {
        /// <summary>
        /// 是否重定位成功
        /// </summary>
        //[MarshalAs(UnmanagedType.I1)]
        public bool is_reloc_ok;
        /// <summary>
        /// 重定位成功后，计算得到的vio坐标系到地图坐标系的转换矩阵
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public double[] T_vio_to_map;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] T_w_m_u3d;
        /// <summary>
        /// camera坐标系到imu坐标系的转换
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public double[] T_bc;
    }
}