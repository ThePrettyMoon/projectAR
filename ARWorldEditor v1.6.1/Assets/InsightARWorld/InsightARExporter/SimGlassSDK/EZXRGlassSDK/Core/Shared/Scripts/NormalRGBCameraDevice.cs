using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EZXR.Glass.Common
{
    public class NormalRGBCameraDevice
    {
        private bool hasCameraOpened = false;
        public void Open()
        {
            if (!Application.isEditor)
            {
                NativeApi.nativeOpenRGBCamera();
            }
            hasCameraOpened = true;
        }

        public void Close()
        {

        hasCameraOpened = false;
        if (!Application.isEditor)
        {
            NativeApi.nativeCloseRGBCamera();
        }
    }
    public bool getCurrentRGBImage(ref EZVIOInputImage image, float[] intrinsic)
    {
        if (hasCameraOpened)
        {
            //return NativeApi.nativeGetCurrentRGBImage(ref image);
            return NativeApi.nativeGetCurrentImage(ref image, intrinsic);
        }
        return false;
    }
    public bool getCameraIntrics(float[] inttrics)
    {
        if (hasCameraOpened)
        {
            return NativeApi.nativeGetRGBCameraIntrics(inttrics);
        }
        return false;
    }
    private partial struct NativeApi
    {
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void nativeOpenRGBCamera();

            [DllImport(NativeConsts.NativeLibrary)]
            public static extern void nativeCloseRGBCamera();

            [DllImport(NativeConsts.NativeLibrary)]
            public static extern bool nativeGetCurrentRGBImage(ref EZVIOInputImage image);


        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool nativeGetCurrentImage(ref EZVIOInputImage image, float[] intrinsic);
        

            [DllImport(NativeConsts.NativeLibrary)]
            public static extern bool nativeGetRGBCameraIntrics([Out, In][MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] float[] ptr);
        }
    }
}
