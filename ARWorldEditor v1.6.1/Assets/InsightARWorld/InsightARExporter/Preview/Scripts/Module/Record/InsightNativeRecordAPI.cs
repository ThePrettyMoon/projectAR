using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public static class InsightNativeRecordAPI 
{
#if UNITY_EDITOR
    public static void iarInitRecord(string path, int width, int height, InsightNativeRecord.IARRecordCaptureRecordCallback callback) { }

    public static void iarStartRecording() { }

    public static void iarStopRecording() { }

    public static void iarWriteBytePixels(int textureId,long timestamp) { }
#else
#if UNITY_ANDROID
    public const string dllName = "UnityCallbackNative";

     [DllImport(dllName)]
    public static extern void iarWriteBytePixels(int textureId, long timestamp);
#elif UNITY_IOS
    public const string dllName = "__Internal";

    [DllImport(dllName)]
    public static extern void iarWriteBytePixels(IntPtr texturePtr, long timestamp);
#endif
    [DllImport(dllName)]
    public static extern void iarInitRecord(string path, int width, int height, InsightNativeRecord.IARRecordCaptureRecordCallback callback);

    [DllImport(dllName)]
    public static extern void iarStartRecording();

    [DllImport(dllName)]
    public static extern void iarStopRecording();

#endif

}
