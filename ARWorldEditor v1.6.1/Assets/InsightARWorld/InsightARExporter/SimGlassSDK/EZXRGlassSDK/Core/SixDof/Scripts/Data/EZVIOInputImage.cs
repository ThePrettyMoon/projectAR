using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace EZXR.Glass.SixDof
{
    public struct EZVIOInputImage
    {
        public IntPtr pImg0; //left image pointer 640x400
        public IntPtr pImg1; //right image pointer 640x400
        public IntPtr fullImg;//full image pointer 1280x400

        public EZVIOImagePointerType imgPtrType;
        public EZVIOImageFormat imgFormat;
        public EZVIOImageRes imgRes;

        public double timestamp;
    }

    public struct EZVIOImageRes
    {
        public float width;
        public float height;
    }


    public enum EZVIOImagePointerType
    {
        EZVIOImagePointerType_RawData, //please use this one
        EZVIOImagePointerType_iOS_CVPixelRef,
    }

    public enum EZVIOImageFormat
    {
        EZVIOImageFormat_YUV420v = 0, //be used in iOS
        EZVIOImageFormat_YUV420f = 1, //be used in iOS
        EZVIOImageFormat_BGRA = 2, //be used in iOS
        EZVIOImageFormat_YUV420A = 3, //YUV_420_888_Android (YYYYYYYY UUVV)
        EZVIOImageFormat_RGB,//RGB_888
    }
}
