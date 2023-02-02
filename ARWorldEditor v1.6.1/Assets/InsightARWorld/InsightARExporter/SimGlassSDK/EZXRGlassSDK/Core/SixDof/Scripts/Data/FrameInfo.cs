namespace EZXR.Glass.SixDof
{
    using System;
    using UnityEngine;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FrameInfo
    {

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr leftTex;

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr rightTex;

        /// <summary> Values that represent the time this twc data aligned with. Unix epoch, second. </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double TwcTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public float[] Twc;           // Now，it is aligned with EZVIOResult's Twc

        /// <summary> Values that represent the time this FrameInfo data generated. Unix epoch, second. </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double frameGenTime;

        /// <summary> Values that represent the time the twc data is fetched. Unix epoch, seconds. </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double TwcFetchTime;

        public FrameInfo(IntPtr leftTex, IntPtr rightTex, float[] Twc, double TwcTime, double frameGenTime, double TwcFetchTime)
        {
            this.leftTex = leftTex;
            this.rightTex = rightTex;
            this.Twc = (float[])Twc.Clone();
            this.TwcTime = TwcTime;
            this.frameGenTime = frameGenTime;
            this.TwcFetchTime = TwcFetchTime;
        }
    }
}