using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum IAREventType
{
    IAREventTypeSceneSuspendEnter = 100,        // 进入挂起
    IAREventTypeSceneSuspendExit = 101,         // 退出挂起
    IAREventTypeSceneAttachEnter = 103,         // 进入叠加
    IAREventTypeSceneAttachExit = 104,          // 退出叠加
    IAREventTypeSceneUnchangedEnter = 105,      // 进入unchange，保留吧，暂时用不到
    IAREventTypeSceneUnchangedExit = 106,       // 退出unchange，保留吧，暂时用不到

    IAREventTypeLoadMainContentSuccess  = 201,  //成功加载主内容
    IAREventTypeExitMainContent         = 202,  //退出主内容
    IAREventTypeLoadMainContentError    = 203,  //加载主内容异常

    IAREventTypeEnableRecordOrPhoto = 301,      //内容唤起拍照录屏
    IAREventTypeDisenableRecordOrPhoto = 302,   //内容停止拍照录屏

    
};


/// <summary>
/// 权限状态，iOS 新增【用户未选择】状态
/// </summary>
public enum IAuthorizationStatus
{
    IPermissionStatusNotDetermined = 0,
    IPermissionStatusDenied = 1,
    IPermissionStatusAuthorized = 2,
};


public enum IAuthorizationType
{
    IPermissionTypeCamera = 1,
    IPermissionTypeAudio = 2,
    IPermissionTypeAlbum = 3,
    IPermissionTypeGPS = 4,
};

/// 文件类型
public enum IMediaType
{
    IMediaTypeImage = 1,
    IMediaTypeVideo = 3,
};


public enum IImageUploadState
{
    IUploadStart = 0,
    IUploading = 1,
    IUploadSuccess = 2,
    IUploadFailed = 3
};

public struct ImageUploadState
{
    IImageUploadState state;
    int progress;
    IntPtr nosAddress;
};

public enum IGPSResultStatus
{
    IGPSResultStateSuccess = 0,
    IGPSResultStateGPSOff = 1,
    IGPSResultStateError = 2
}

[Serializable]
public struct IGPSResult
{
    public IGPSResultStatus status;
    public double latitude;
    public double longitude;

    /// <summary>
    /// tostring
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "GPSResult state = " + status.ToString() + " lat = " + latitude + " lng = " + longitude;
    }
}

public enum IUserEventType
{
    IUserEventTypeExit = 0,

    IUserEventTypeNaviStop = 100,       //退出导航

    IUserEventTypeRecordSuccess = 200,    // 录屏成功
    IUserEventTypeRecordFail = 201,       // 录屏失败
    IUserEventTypeRecordStart = 202,      // 开始录屏
    IUserEventTypeRecordStop = 203,       // 结束录屏
    IUserEventTypeTakePhoto = 204,        // 拍照
    IUSerEventTypeRecordExit = 205,       // 退出录屏

    IUserEventTypeAudioPause = 300,   //音频暂停
    IUserEventTypeAudioResume = 301,  //音频恢复
    IUserEventTypeVideoPause = 303,   //视频暂停
    IUserEventTypeVideoResume = 304,  //视频恢复

    /// Log Level
    IUserEventTypeLogLevelVerbose = 400,
    IUserEventTypeLogLevelDebug = 401,
    IUserEventTypeLogLevelWarn = 402,
    IUserEventTypeLogLevelError = 403,
    IUserEventTypeLogLevelNone = 404,
}

/// <summary>
/// 导航类型
/// </summary>
public enum INavigationType
{
    INavigationTypePlan = 0, // 路径规划
    INavigationTypeNavi = 1, // 路径导航
};

/// <summary>
/// 权限回调
/// </summary>
public struct PermissionResult
{
    public IAuthorizationType type;
    public IAuthorizationStatus status;

    public PermissionResult(IAuthorizationType type,IAuthorizationStatus status)
    {
        this.type = type;
        this.status = status;
    }
}

/// <summary>
/// 导航回调
/// </summary>
public struct NavigationResult
{
    public INavigationType type;
    public string poiId;

    public NavigationResult(INavigationType type,string poiId)
    {
        this.type = type;
        this.poiId = poiId;
    }
}

/// 外部导航的出行方式
public enum IExternalNavMode
{
    IExternalNavModeWalking = 0, // 步行
    IExternalNavModeTransit = 1, // 公共交通
    IExternalNavModeRiding = 2, // 骑行
    IExternalNavModeDriving = 3, // 驾车
};


/// AR 场景类型
public enum IARSceneLoadingType
{
    IARSceneLoadingTypeFullScreen = 0,      // 全屏替换，不透明
    IARSceneLoadingTypeFullMask = 1,        // 子场景POI
    IARSceneUnloadingType = 2
};
