using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 录屏管理，支持多相机
/// </summary>
public class RecordManager : UnitySingleton<RecordManager>
{
    #region params
    private const string TAG = "RecordManager";
    private const long MINCAPTUREINTERVAL = 33;
    private int mVideoWidth;
    private int mVideoHeight;
    private Texture2D mVideoTexture;
    private long mLastCaptureTime;
    private bool mRecording;
    InsightARCaptureRecordResult mRecordResult;

    public bool IsRecording
    {
        get
        {
            return mRecording;
        }
    }

    #endregion

    #region unity functions
    private void OnEnable()
    {
        InsightNativeRecord.recordCaptureEvent += OnRecordCallback;

        LSGameManager.Instance.onApplicationPausedEvent += OnApplicationPauseEventHandler;

        mRecording = false;
        mRecordResult = InsightARCaptureRecordResult.IARRecordNotInited;
    }


    void Update()
    {
        if (!mRecording) return;
        UpdateCaptureState();
    }

    public void OnDisable()
    {
        StopRecord();
        InsightNativeRecord.recordCaptureEvent -= OnRecordCallback;
        LSGameManager.Instance.onApplicationPausedEvent -= OnApplicationPauseEventHandler;
    }

    public void OnDestroy()
    {

    }
    #endregion

    #region custom functions
    /// <summary>
    /// start record
    /// </summary>
    public void StartRecord()
    {
        if (mRecording) return;
        InitRecord();
    }

    /// <summary>
    /// stop record
    /// </summary>
    public void StopRecord()
    {
        if (!mRecording) return;
        InsightNativeRecord.StopRecord();
        InsightDebug.Log(TAG, "Stop Record");
        mRecording = false;
        mRecordResult = InsightARCaptureRecordResult.IARRecordNotInited;
    }

    /// <summary>
    /// 处理录屏状态
    /// </summary>
    private void UpdateCaptureState()
    {
        if (mRecordResult == InsightARCaptureRecordResult.IARRecordSaveSuccess)
        {
            CaptureSaveSuccess();
        }
        else if (mRecordResult == InsightARCaptureRecordResult.IARRecordNotInited ||
           mRecordResult == InsightARCaptureRecordResult.IARRecordInitFail ||
           mRecordResult == InsightARCaptureRecordResult.InsightARRecordAuthorizationStatusDenied)
        {
            CaptureFail();
        }
        else if (mRecordResult == InsightARCaptureRecordResult.IARRecordStart)
        {
            CaptureFrame();
        }
    }

    /// <summary>
    /// init record
    /// </summary>
    private void InitRecord()
    {
        InsightDebug.Log(TAG, "Init Record ");

        mVideoWidth = Screen.width;
        mVideoHeight = Screen.height;

        if (mVideoTexture == null)
            mVideoTexture = new Texture2D(mVideoWidth, mVideoHeight, TextureFormat.RGBA32, false, true);

        string recordPath = GetRecordFilePath();
        InsightNativeRecord.InitRecord(recordPath, mVideoWidth, mVideoHeight);

        //默认录屏成功
        InitRecordSuccess();
    }

    /// <summary>
    /// init record success
    /// </summary>
    private void InitRecordSuccess()
    {
        mRecordResult = InsightARCaptureRecordResult.IARRecordInitSuccess;
        mRecording = true;
        InsightNativeRecord.StartRecord();
        InsightDebug.Log(TAG, "Start Record");
    }

    /// <summary>
    /// 录屏保存成功
    /// </summary>
    private void CaptureSaveSuccess()
    {

    }

    /// <summary>
    /// 录屏失败处理
    /// </summary>
    private void CaptureFail()
    {
        if (mRecording) mRecording = false;

    }

    /// <summary>
    /// get record file path
    /// </summary>
    /// <returns></returns>
    private string GetRecordFilePath()
    {
        string recordDirectory = Path.Combine(Application.persistentDataPath, "Video");

        if (!Directory.Exists(recordDirectory))
        {
            Directory.CreateDirectory(recordDirectory);
        }

        return recordDirectory + "/" + "insight_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".mp4";
    }

    /// <summary>
    /// on record callback
    /// </summary>
    /// <param name="result"></param>
    private void OnRecordCallback(InsightARCaptureRecordResult result)
    {
        mRecordResult = result;
        InsightDebug.Log(TAG, "On Record Callback " + result);
    }

    /// <summary>
    /// 进入后台需要断开录屏
    /// </summary>
    /// <param name="paused"></param>
    private void OnApplicationPauseEventHandler(bool paused)
    {
        InsightDebug.Log(TAG, "On Application Paused " + paused);
        //安卓可以在退到后台之前通知unity，iOS单独处理
//#if UNITY_IOS
        if (paused) StopRecord();
//#endif
    }

    /// <summary>
    /// 更新相机画面
    /// </summary>
    private void CaptureFrame()
    {
        CaptureVideoTexture(mVideoWidth, mVideoHeight);

        long currentTime = TimeUtility.GetTimeStampMilli();
        if (currentTime - mLastCaptureTime > MINCAPTUREINTERVAL)
        {
            long nanoTime = TimeUtility.GetTimeStampNano();
            InsightNativeRecord.UpdateRecord(mVideoTexture.GetNativeTexturePtr(), nanoTime);
            mLastCaptureTime = TimeUtility.GetTimeStampMilli();
        }
    }

    /// <summary>
    /// Capture 视频纹理
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    private void CaptureVideoTexture(int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height,24,RenderTextureFormat.ARGB32);
        RenderTexture.active = rt;

        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.gameObject.tag == "MainCamera") cam.targetTexture = rt;
            cam.Render();
            cam.targetTexture = null;
        }

        mVideoTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        mVideoTexture.Apply(false);

        RenderTexture.active = null;
        Destroy(rt);
    }

#endregion
}
