using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOT;
using System;

public class InsightNativeRecord
{
    public delegate void IARRecordCaptureRecordCallback(InsightARCaptureRecordResult result);
    public static event IARRecordCaptureRecordCallback recordCaptureEvent;
    public static string recordedVideoPath = string.Empty;  //分享视频时调用

    private static string videoPath = string.Empty;

    /// <summary>
    /// 初始化录屏
    /// </summary>
    /// <param name="path"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void InitRecord(string path, int width, int height)
    {
        InsightNativeRecordAPI.iarInitRecord(path, width, height, RecordCaptureCallback);
        recordedVideoPath = string.Empty;
        videoPath = path;
    }

    /// <summary>
    /// 开始录屏
    /// </summary>
    /// <param name="type"></param>
    public static void StartRecord()
    {
        InsightNativeRecordAPI.iarStartRecording();
    }

    /// <summary>
    /// 录屏更新，时间戳为纳秒
    /// </summary>
    /// <param name="textureId"></param>
    /// <param name="type"></param>
    /// <param name="timestamp"></param>
    public static void UpdateRecord(IntPtr texturePtr,long timestamp)
    {
#if UNITY_EDITOR
        InsightNativeRecordAPI.iarWriteBytePixels(texturePtr.ToInt32(), timestamp);
#elif UNITY_ANDROID
        InsightNativeRecordAPI.iarWriteBytePixels(texturePtr.ToInt32(), timestamp);
#elif UNITY_IOS
        InsightNativeRecordAPI.iarWriteBytePixels(texturePtr, timestamp);
#endif
    }

    /// <summary>
    /// 停止录屏
    /// </summary>
    /// <param name="type"></param>
    public static void StopRecord()
    {
        InsightNativeRecordAPI.iarStopRecording();
        recordedVideoPath = videoPath;
    }

    /// <summary>
    /// event
    /// </summary>
    /// <param name="result"></param>
    [MonoPInvokeCallback(typeof(IARRecordCaptureRecordCallback))]
    static void RecordCaptureCallback(InsightARCaptureRecordResult result)
    {
        if (recordCaptureEvent != null)
        {
            recordCaptureEvent(result);
        }
    }
}
