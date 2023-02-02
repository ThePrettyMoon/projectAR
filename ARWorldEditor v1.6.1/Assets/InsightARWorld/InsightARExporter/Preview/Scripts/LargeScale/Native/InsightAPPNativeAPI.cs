using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using InsightAR.Internal;

public static class InsightAPPNativeAPI
{
#if UNITY_EDITOR
    public static void nbInit(
        InsightAPPNative.IPermissionStateCallback onPermission,
        //InsightAPPNative.IUploadCallback onUpload,
        //InsightAPPNative.IPickImageCallback onPick,
        InsightAPPNative.INavigationCallback onNavigation,
        //InsightAPPNative.IGPSCallback onGPS,
        InsightAPPNative.IUserEventCallback onUserEvent,
        //InsightAPPNative.IScannedQRCodeCallback onQRCode,
        InsightAPPNative.IARResourceLocalPathCallback onLocalPath,
        InsightAPPNative.ICloudLocationCallback onCloudLocRequest,
        InsightAPPNative.IDoScriptCallback onDoScriptCallback,
        InsightAPPNative.IPoiStatusCallback onPoiStatusCallback
        )
    { }

    public static IAuthorizationStatus nbGetAuthorizationAccess(IAuthorizationType type) { return IAuthorizationStatus.IPermissionStatusDenied; }

    public static void nbRequestAuthorizationAccess(IAuthorizationType type) { }


    #region ----通用接口-----
    public static bool nbIsWifi() { return true; }

    public static bool nbIsNetworkAvailable() { return true; }

    public static void nbGoSettings() { }

    public static bool nbIsBottomBarSeries() { return false; }

    public static void nbMakeToast(string text, int during) { }

    public static void nbVibrate() { }

    public static void nbGetCurrentGPS() { }

    public static void nbPickImageFromAlbum() { }

    public static void nbSaveMediaFile(string filePath, int type) { }

    public static void nbTakePhoto(string path) { }

    public static void nbUploadImageToNos(string absolutePath, string token, string nosObject, string bucket) { }
    #endregion

    // 加载地图资源。 geoJsonString：楼层相关信息，json串，floor：默认楼层
    /** geoJsonString 具体键值如下：
     [{
         floor : "1",
         floorName : "C6-1F",
         geoFilePath : "./geo/c6_1.json"
      }]
     */
    public static void nbLoad2DMapData(string geoJsonString, int floor) { }
    // 设置当前位置。 floor：所在楼层
    public static void nbUpdate2DMapLocation(string locJsonString) { }
    public static void nbSet2DMapPoiList(string poiListInfo) { }
    // 更新导航路径
    public static void nbUpdate2DMapNaviPath(INavigationType type, string poiInfo) { }
    // 结束2D导航
    public static void nbExit2DNavigation() { }
    // 设置2D地图页面是否显示
    public static void nbSet2DMapVisibility(int enable) { }
    // for Android only
    public static void nbRelease() { }
    // 增加内容事件
    public static void nbSetAREvent(IAREventType type) { }


    //西溪二维码

    /// open To Scan, default full Screen, will callback via [ARLUnityMessager shared].scannedQRCodeCallback, set block using nbSetScannedQRCodeCallback API in UnityPluginAPI.h
    public static void nbOpenQRCodeScan() { }

    /// Close and stop Scanning
    public static void nbStopQRCodeScan() { }

    /// default is YES, can stop scanning when Camera is Open
    public static void nbSetScannnig(bool scanning) { }



    /// <summary>
    /// 控制录屏是否显示的接口
    /// </summary>
    /// <param name="visible"></param>
    public static void nbSetRecordViewVisibility(int visible) { }

    // 通知native 核销码失效
    public static void nbInvalidQRCode() { }

    //云定位接口-android
    public static void nbCloudLocationRequest(double timestamp, string jpgStr, string protoBufferStr) { }
    //云定位接口-ios
    public static void nbCloudLocationRequest(InsightARCloudLocRequest requestImpl) { }

    public static void nb3DEventMessage(int identity, int version, int type, string desc) { }

    /// <summary>
    /// 和APP or 上层的网路请求交互
    /// </summary>
    /// <param name="url"></param>
    /// <param name="header"></param>
    /// <param name="body"></param>
    /// <param name="contentType"></param>
    /// <param name="userData"></param>
    /// <param name="networkCallback"></param>
    public static void nbRequest(string url, string method, string header, string body, string userData) { }

    public static void nbSetRequestCallback(InsightAPPNative.IHttpNetworkCallback networkCallback) { }
    public static void nbDownloadFile(string downladUrl, string downloadPath, string userData) { }
    public static void nbSetDownloadCallback(
        InsightAPPNative.IDownloadSuccessCallback successCallback,
        InsightAPPNative.IDownloadProgressCallback progressCallback,
        InsightAPPNative.IDownloadFailCallback failedCallback)
    { }

    /**
     unity 加载进度 - 加载中
     user_data: 内容自定义参数
     progress: loading进度
     */
    public static void nbSetUnityLoadingProgress(string user_data, double progress, IARSceneLoadingType type){}

    /**
     unity 进度加载 - 加载完成
     user_data: 内容自定义参数，传负值则默认0.5s（SDK决定一个默认值）
     progress: loading进度
     */
    public static void nbSetUnityLoadingCompleted(string user_data, double delay_to_open, IARSceneLoadingType type) { }

    //报告算法类型
    public static void nbSetCurrentAREngineType(int arEngineType) { }

    public static void nbCallExternalNavi(string destName, double destLatitude, double destLongitude, IExternalNavMode naviMode) { }
#else
#if UNITY_ANDROID
    const string dllName = "UnityCallbackNative";

    public static bool nbIsBottomBarSeries() { return false; }

    [DllImport(dllName)]
    public static extern IntPtr nbGetDeviceModel();
    //云定位接口
    [DllImport(dllName)]
    public static extern void nbCloudLocationRequest(double timestamp, string jpgStr, string protoBufferStr);

    //报告算法类型
    [DllImport(dllName)]
    public static extern void nbSetCurrentAREngineType(int arEngineType);
#elif UNITY_IOS
    const string dllName = "__Internal";

    [DllImport(dllName)]
    public static extern bool nbIsBottomBarSeries();

    ////云定位接口
    //[DllImport(dllName)]
    //public static extern void nbCloudLocationRequest(InsightARCloudLocRequest requestImpl);
    //云定位接口
    [DllImport(dllName)]
    public static extern void nbCloudLocationRequest(double timestamp, string jpgStr, string protoBufferStr);

#endif

    [DllImport(dllName)]
    public static extern void nbInit(
        InsightAPPNative.IPermissionStateCallback onPermission,
        //InsightAPPNative.IUploadCallback onUpload,
        //InsightAPPNative.IPickImageCallback onPick,
        InsightAPPNative.INavigationCallback onNavigation,
        //InsightAPPNative.IGPSCallback onGPS,
        InsightAPPNative.IUserEventCallback onUserEvent,
        //InsightAPPNative.IScannedQRCodeCallback onQRCode,
        InsightAPPNative.IARResourceLocalPathCallback onLocalPath,
        InsightAPPNative.ICloudLocationCallback onCloudLocRequest,
        InsightAPPNative.IDoScriptCallback onDoScriptCallback,
        InsightAPPNative.IPoiStatusCallback onPoiStatusCallback
        );

    [DllImport(dllName)]
    public static extern void nbRequestAuthorizationAccess(IAuthorizationType type);

    [DllImport(dllName)]
    public static extern IAuthorizationStatus nbGetAuthorizationAccess(IAuthorizationType type);

    [DllImport(dllName)]
    public static extern void nbUploadImageToNos(string absolutePath, string token, string nosObject, string bucket);

    [DllImport(dllName)]
    public static extern void nbSaveMediaFile(string filePath, int type);

    [DllImport(dllName)]
    public static extern bool nbIsWifi();

    [DllImport(dllName)]
    public static extern bool nbIsNetworkAvailable();

    [DllImport(dllName)]
    public static extern void nbGoSettings();

    [DllImport(dllName)]
    public static extern void nbPickImageFromAlbum();

    [DllImport(dllName)]
    public static extern void nbGetCurrentGPS();

    [DllImport(dllName)]
    public static extern void nbLoad2DMapData(string geoJsonString, int floor);

    [DllImport(dllName)]
    public static extern void nbUpdate2DMapLocation(string locJsonString);

    [DllImport(dllName)]
    public static extern void nbSet2DMapPoiList(string poiListInfo);

    [DllImport(dllName)]
    public static extern void nbUpdate2DMapNaviPath(INavigationType type,string poiInfo);

    [DllImport(dllName)]
    public static extern void nbSetAREvent(IAREventType type);


    [DllImport(dllName)]
    public static extern void nbExit2DNavigation();

    [DllImport(dllName)]
    public static extern void nbSet2DMapVisibility(int enable);

    [DllImport(dllName)]
    public static extern void nbMakeToast(string text, int during);

    [DllImport(dllName)]
    public static extern void nbTakePhoto(string path);

    [DllImport(dllName)]
    public static extern void nbRelease();

    [DllImport(dllName)]
    public static extern void nbOpenQRCodeScan();

    [DllImport(dllName)]
    public static extern void nbStopQRCodeScan() ;

    [DllImport(dllName)]
    public static extern void nbSetScannnig(bool scanning) ;

    [DllImport(dllName)]
    public static extern void nbVibrate() ;

    [DllImport(dllName)]
    public static extern void nbSetRecordViewVisibility(int visible) ;

    [DllImport(dllName)]
    public static extern void nbInvalidQRCode();

    [DllImport(dllName)]
    public static extern void nbCallExternalNavi(string destName, double destLatitude, double destLongitude, IExternalNavMode naviMode);

    [DllImport(dllName)]
    public static extern void nb3DEventMessage(int identity, int version, int type, string desc);

    [DllImport(dllName)]
    public static extern void nbRequest(string url,string method, string header, string body, string userData);

    [DllImport(dllName)]
    public static extern void nbSetRequestCallback(InsightAPPNative.IHttpNetworkCallback networkCallback);

    [DllImport(dllName)]
    public static extern void nbDownloadFile(string downladUrl, string downloadPath, string userData);

    [DllImport(dllName)]
    public static extern void nbSetUnityLoadingProgress(string user_data, double progress, IARSceneLoadingType type);

    [DllImport(dllName)]
    public static extern void nbSetUnityLoadingCompleted(string user_data, double delay_to_open, IARSceneLoadingType type);

    [DllImport(dllName)]
    public static extern void nbSetDownloadCallback(
        InsightAPPNative.IDownloadSuccessCallback successCallback,
        InsightAPPNative.IDownloadProgressCallback progressCallback,
        InsightAPPNative.IDownloadFailCallback failedCallback);
#endif
}
