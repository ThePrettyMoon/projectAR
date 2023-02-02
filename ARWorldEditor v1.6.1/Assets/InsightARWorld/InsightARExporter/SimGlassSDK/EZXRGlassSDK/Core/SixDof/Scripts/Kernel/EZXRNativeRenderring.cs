namespace EZXR.Glass.SixDof
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Runtime.InteropServices;
    using System;
    using System.IO;

    public class EZXRNativeRenderring
    {

        public IntPtr FrameInfoPtr;
        private IntPtr m_FrameTexturesPtr;

        public EZXRNativeRenderring()
        {
            int sizeOfFrameInfo = Marshal.SizeOf(typeof(FrameInfo));
            FrameInfoPtr = Marshal.AllocHGlobal(sizeOfFrameInfo);
        }

        ~EZXRNativeRenderring()
        {
            Marshal.FreeHGlobal(FrameInfoPtr);
        }

        public int Start()
        {
            return NativeAPI.startEZXRRenderer();
        }

        public void DoExtendedRenderring()
        {
            FrameInfo frameinfo = (FrameInfo)Marshal.PtrToStructure(FrameInfoPtr, typeof(FrameInfo));
            NativeAPI.setFrameInfo(FrameInfoPtr);
        }

        public void WriteFrameData(FrameInfo frame)
        {
            Marshal.StructureToPtr(frame, FrameInfoPtr, true);
        }

        public void Stop()
        {
            NativeAPI.stopEZXRRenderer();
        }

        private partial struct NativeAPI
        {
#if UNITY_ANDROID
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern int startEZXRRenderer();

        [DllImport(NativeConsts.NativeLibrary)]
        //public static extern void setFrameInfo(ref FrameInfo frameInfoPtr);
        public static extern void setFrameInfo(IntPtr frameInfoPtr);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void stopEZXRRenderer();
#else
            public static int startEZXRRenderer() { return -1; }

            //public static void setFrameInfo(ref FrameInfo frameInfoPtr) { }
            public static void setFrameInfo(IntPtr frameInfoPtr) { }

            public static void stopEZXRRenderer() { }
#endif
        }
    }

}