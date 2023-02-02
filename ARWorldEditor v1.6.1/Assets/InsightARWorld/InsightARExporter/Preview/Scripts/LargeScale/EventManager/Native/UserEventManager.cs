using InsightAR.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserEventManager
{
    private const string TAG = "UserEventManager";

    public static Action<int> onUserCaptureEvent;

    public static Action onUserExitNaviEvent;

    /// <summary>
    /// native 返回的事件类型
    /// </summary>
    /// <param name="type"></param>
    public static void UserEventCallbackHandler(IUserEventType type)
    {

        InsightDebug.Log(TAG, " User Event Type = " + type + "/" + (int)type);

        if (type == IUserEventType.IUserEventTypeExit)
        {
            LSGameManager.Instance.ExitARScene();
        }
        else if (type == IUserEventType.IUserEventTypeNaviStop)
        {
            NavigationInterface.isNavigationState = false;
            //通知内容
            onUserExitNaviEvent?.Invoke();
            NaviSceneManager.Instance.EndNavi();//通知导航算法
            //返回location
            LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_LOCATION);
        }
        else if (type == IUserEventType.IUserEventTypeRecordStart)
        {
            ReplayCamManager.Instance.StartRecording();
        }
        else if (type == IUserEventType.IUserEventTypeRecordStop)
        {
            ReplayCamManager.Instance.StopRecording();
        }
        else if (type == IUserEventType.IUserEventTypeRecordFail)
        {

        }
        else if (type == IUserEventType.IUserEventTypeTakePhoto)
        {
            string path = CaptureUtility.TakePhoto();
            InsightAPPNative.TakePhoto(path);
        }

        //loglevel
        else if (type == IUserEventType.IUserEventTypeLogLevelVerbose) {
            InsightDebug.sEnableLogLevel = (LogLevel)type;

        }
        else if (type == IUserEventType.IUserEventTypeLogLevelDebug)
        {
            InsightDebug.sEnableLogLevel = (LogLevel)type;

        }
        else if (type == IUserEventType.IUserEventTypeLogLevelWarn)
        {
            InsightDebug.sEnableLogLevel = (LogLevel)type;

        }
        else if (type == IUserEventType.IUserEventTypeLogLevelError)
        {
            InsightDebug.sEnableLogLevel = (LogLevel)type;

        }
        else if (type == IUserEventType.IUserEventTypeLogLevelNone)
        {
            InsightDebug.sEnableLogLevel = (LogLevel)type;

        }

        onUserCaptureEvent?.Invoke((int)type);
    }


    public static void ClearListeners() {
        onUserCaptureEvent = null;
        onUserExitNaviEvent = null;
    }

}
