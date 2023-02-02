using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using NatSuite.Recorders.Internal;

public class ReplayCamManager : UnitySingleton<ReplayCamManager>
{
    private const string TAG = "ReplayCamManager";

    private IMediaRecorder recorder;
    private CameraInput cameraInput;
    private AudioInput audioInput;
    private AudioListener audioListener;

    private int videoWidth = 720;
    private int videoHeight = 1080;
    private int frameRate = 30;
    private int sampleRate = 44100;
    private int channelCount = 2;
    private bool isRunning = false;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        LSGameManager.Instance.onApplicationPausedEvent += OnApplicationPauseEventHandler;
    }

    public void OnDisable()
    {
        LSGameManager.Instance.onApplicationPausedEvent -= OnApplicationPauseEventHandler;
    }

    private void OnDestroy()
    {

    }

    /// <summary>
    /// 进入后台，停止录屏
    /// </summary>
    /// <param name="paused"></param>
    private void OnApplicationPauseEventHandler(bool paused)
    {
        InsightDebug.Log(TAG, "On Application Paused " + paused);
        if (paused) StopRecording();
    }

    public void StartRecording()
    {
        // Start recording
        var clock = new RealtimeClock();

        //audio listener 也需要重新初始化
        audioListener = FindObjectOfType<AudioListener>();
        if (audioListener == null) audioListener = Camera.main.gameObject.AddComponent<AudioListener>();
        string recordPath = Utility.GetPath(@".mp4");

        sampleRate = AudioSettings.outputSampleRate;
        channelCount = (int)AudioSettings.speakerMode;

        //默认安卓720x1280 ，iOS改成1080x1920
        var shortScreen = Screen.width >= Screen.height ? Screen.height : Screen.width; //短边
        var longScreen = Screen.width >= Screen.height ? Screen.width : Screen.height;  //长边
        var isPortriat = Screen.height > Screen.width;
#if UNITY_IOS
        //videoWidth = Screen.width;
        //videoHeight = Screen.height;
        videoWidth = shortScreen >= 1080 ? 1080 : shortScreen;
#else
        videoWidth = shortScreen >= 720 ? 720 : shortScreen;
#endif
        //需要根据背景进行适配
        float screenRatio = (float)longScreen / (float)shortScreen;
        videoHeight = (int)(videoWidth * screenRatio);

        //确保为偶数
        videoWidth = videoWidth % 2 == 0 ? videoWidth : videoWidth + 1;
        videoHeight = videoHeight % 2 == 0 ? videoHeight : videoHeight + 1;
        Debug.Log("unity call video width " + videoWidth + " " + videoHeight);
        Debug.Log("unity call Screen width " + Screen.width + " " + Screen.height);

        if(isPortriat)
            recorder = new MP4Recorder(recordPath, videoWidth, videoHeight, frameRate, sampleRate, channelCount);
        else
            recorder = new MP4Recorder(recordPath, videoHeight, videoWidth, frameRate, sampleRate, channelCount);

        // Create recording inputs
        cameraInput = new CameraInput(recorder, clock, Camera.main);
        audioInput = new AudioInput(recorder, clock, audioListener);

        //native
        InsightNativeRecord.InitRecord(recordPath, videoWidth, videoHeight);
        InsightNativeRecord.StartRecord();

        isRunning = true;
    }

    public async void StopRecording()
    {
        if (!isRunning) return;

        // Stop recording
        audioInput?.Dispose();
        cameraInput.Dispose();
        var path = await recorder.FinishWriting();
        // Playback recording
        Debug.Log(string.Format("unity call save recording path {0}", path));
        InsightNativeRecord.StopRecord();

        isRunning = false;
    }


}
