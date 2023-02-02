using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOT;
using System.Threading;
using System;
using System.Runtime.InteropServices;
using InsightAR.Internal;

/// <summary>
/// 和native通信bridge
/// </summary>
public class InsightAPPNative:UnitySingleton<InsightAPPNative>
{
    #region params
    private const string TAG = "InsightAPPNative";

    public delegate void IPermissionStateCallback(IAuthorizationType type, IAuthorizationStatus status);

    public delegate void IUploadCallback(ImageUploadState state);

    public delegate void IPickImageCallback(string path);

    public delegate void INavigationCallback(INavigationType type, string poiId);

    public delegate void IGPSCallback(IGPSResult result);

    public delegate void IUserEventCallback(IUserEventType type);

    public delegate void IScannedQRCodeCallback(string scannedQRCode);

    public delegate void IHttpNetworkCallback(string userdata, string resulCode, string resultMsg, string response);

    public delegate void IDownloadSuccessCallback(string downloadUrl, string downloadPath, string userData);

    public delegate void IDownloadProgressCallback(string downloadUrl, float progress, string userData);

    public delegate void IDownloadFailCallback(string downloadUrl, string resultCode, string resultDesc, string userData);
    //ls sdk zucker
    //云定位
    public delegate void ICloudLocationCallback(int algCode, string requestBackCode);
    //runscript callback
    public delegate void IDoScriptCallback(string script);
    //poistatus callback
    public delegate void IPoiStatusCallback(string script);

    //zucker
    public delegate void IARResourceLocalPathCallback(string locaARPathData);
    public delegate void IARNaviCallback(string navidata, int type);
    public delegate void IARNavi2DPoseCallback(string navidata);

    public static event IPermissionStateCallback permissionStateEvent;

    public static event IUploadCallback uploadImageEvent;

    public static event IPickImageCallback pickImageEvent;

    public static event INavigationCallback navigationEvent;

    public static event IGPSCallback gpsEvent;

    public static event IUserEventCallback userEvent;

    public static event IScannedQRCodeCallback scanQRCodeEvent;

    public static event IHttpNetworkCallback NetworkEvent;
    //zucker
    public static event IARResourceLocalPathCallback arLocalPathEvent;
    public static event IARNaviCallback arNaviEvent;
    public static event IARNavi2DPoseCallback arNavi2DPoseEvent;

    public static Action<InsightCloudRequestResult> arCloudLocEvent;

    public static Action<string> arDoScriptEvent;

    private static List<PermissionResult> listPermissionResult;
    private static List<NavigationResult> listNavigationResult;
    //private static List<IGPSResult> listGPSResult;
    private static List<IUserEventType> listUserEventResult;
    private static List<ImageUploadState> listImageUpdateResult;
    private static List<string> listPickImageResult;
    private static List<string> listScanQRCodeResult;
    private static List<InsightCloudRequestResult> listCloudRequestResult;
    private static List<string> listDoScriptResult;
    private static List<string> listPoiStatusResult;

    #endregion

    #region unity functions
    private void Update()
    {
        UpdatePermissionState();
        UpdateNavigationState();
        //UpdateUploadImageState();
        UpdateUserEventState();
        //UpdateGPSState();
        //UpdatePickImageState();
        UpdateCloudLocRequest();
        UpdateDoScript();
        UpdatePoiStatus();
    }

    /// <summary>
    /// 更新权限接口
    /// 每次消费一条数据
    /// </summary>
    private void UpdatePermissionState()
    {
        int length = listPermissionResult.Count;
        if (length > 0)
        {
            PermissionResult result = listPermissionResult[0];
            if (permissionStateEvent != null)
                permissionStateEvent(result.type, result.status);
            listPermissionResult.RemoveAt(0);
        }
    }

    /// <summary>
    /// 回调点击poi导航
    /// </summary>
    private void UpdateNavigationState()
    {
        int length = listNavigationResult.Count;
        if (length > 0)
        {
            NavigationResult result = listNavigationResult[0];
            if (navigationEvent != null)
                navigationEvent(result.type, result.poiId);
            listNavigationResult.RemoveAt(0);
        }
    }

    /// <summary>
    ///  上传图像接口图像回调
    /// </summary>
    private void UpdateUploadImageState()
    {
        int length = listImageUpdateResult.Count;
        if (length > 0)
        {
            ImageUploadState state = listImageUpdateResult[0];
            if (uploadImageEvent != null)
                uploadImageEvent(state);
            listImageUpdateResult.RemoveAt(0);
        }
    }

    /// <summary>
    /// update user event
    /// </summary>
    private void UpdateUserEventState()
    {
        int length = listUserEventResult.Count;
        if (length > 0)
        {
            IUserEventType type = listUserEventResult[0];
            if (userEvent != null)
                userEvent(type);
            listUserEventResult.RemoveAt(0);
        }
    }

    /// <summary>
    /// update gps event
    /// </summary>
    //private void UpdateGPSState()
    //{
    //    int length = listGPSResult.Count;
    //    if (length > 0)
    //    {
    //        IGPSResult result = listGPSResult[0];
    //        if (gpsEvent != null)
    //            gpsEvent(result);
    //        listGPSResult.RemoveAt(0);
    //    }
    //}

    /// <summary>
    /// update pick image event
    /// </summary>
    private void UpdatePickImageState()
    {
        int length = listPickImageResult.Count;
        if (length > 0)
        {
            string path = listPickImageResult[0];
            if (pickImageEvent != null)
                pickImageEvent(path);
            listPickImageResult.RemoveAt(0);
        }
    }

    private void UpdateQRCodeResult()
    {
        int length = listScanQRCodeResult.Count;
        if (length > 0)
        {
            string qrCodeString = listScanQRCodeResult[0];
            if (scanQRCodeEvent != null)
                scanQRCodeEvent(qrCodeString);
            listScanQRCodeResult.RemoveAt(0); ;
        }
    }

    public void UpdateCloudLocRequest()
    {
        int length = listCloudRequestResult.Count;
        if (length > 0)
        {
            var result = listCloudRequestResult[0];
            if (arCloudLocEvent != null)
                arCloudLocEvent(result);
            listCloudRequestResult.RemoveAt(0);
        }
    }

    public void UpdateDoScript()
    {
        int length = listDoScriptResult.Count;
        if (length > 0)
        {
            var script = listDoScriptResult[0];
            if (arDoScriptEvent != null)
                arDoScriptEvent(script);
            listDoScriptResult.RemoveAt(0);
        }
    }

    public void UpdatePoiStatus()
    {
        int length = listPoiStatusResult.Count;
        if (length > 0)
        {
            var script = listPoiStatusResult[0];
            if (Insight.Navigation.moduleObject_poiStatus != null &&
                Insight.Navigation.funcObjcet_poiStatus != null) {
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context,
                    Insight.Navigation.moduleObject_poiStatus.heapPtr,
                    Insight.Navigation.funcObjcet_poiStatus.heapPtr, script);
            }

            listPoiStatusResult.RemoveAt(0);
        }
    }
    #endregion

    #region custom functions
    /// <summary>
    /// 初始化
    /// </summary>
    public  void Init()
    {
        listPermissionResult = new List<PermissionResult>();
        listNavigationResult = new List<NavigationResult>();
        //listGPSResult = new List<IGPSResult>();
        listImageUpdateResult = new List<ImageUploadState>();
        listPickImageResult = new List<string>();
        listUserEventResult = new List<IUserEventType>();
        listScanQRCodeResult = new List<string>();
        listCloudRequestResult = new List<InsightCloudRequestResult>();
        listDoScriptResult = new List<string>();
        listPoiStatusResult = new List<string>();

    InsightAPPNativeAPI.nbInit(
            PermissionStateCallback,
            //UploadImageCallback,
            //PickImageCallback,
            NavigationCallback,
            //GPSCallback,
            UserEventCallback,
            //ScanQRCodeCallback,
            ARContentStartCallback,
            CloudLocRequestCallback,
            DoScriptCallback,
            PoiStatusCallback
            );
    }

    public void Close()
    {
        listPermissionResult.Clear();
        listNavigationResult.Clear();
        //listGPSResult.Clear();
        listImageUpdateResult.Clear(); 
        listPickImageResult.Clear();
        listUserEventResult.Clear();
        listCloudRequestResult.Clear();
        listDoScriptResult.Clear();
        listPoiStatusResult.Clear();
        Release();
    }

    /// <summary>
    /// 返回权限是否已经授权
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IAuthorizationStatus GetPermissionAccess(IAuthorizationType type)
    {
        return InsightAPPNativeAPI.nbGetAuthorizationAccess(type);
    }

    /// <summary>
    /// 请求权限
    /// </summary>
    /// <param name="type"></param>
    public static void RequestPermissionAccess(IAuthorizationType type)
    {
       // InsightDebug.Log(TAG, "Request Permission Access " + type);
        InsightAPPNativeAPI.nbRequestAuthorizationAccess(type);

#if UNITY_EDITOR
        //PermissionStateCallback(type, IAuthorizationStatus.IPermissionStatusAuthorized);
#endif
    }

    /// <summary>
    /// 上传图片到nos
    /// </summary>
    /// <param name="path"></param>
    /// <param name="token"></param>
    /// <param name="nosObject"></param>
    /// <param name="bucket"></param>
    public static void UploadImageToNos(string path, string token, string nosObject, string bucket)
    {
        InsightAPPNativeAPI.nbUploadImageToNos(path, token, nosObject, bucket);
    }

    /// <summary>
    /// 保存图片和视频文件
    /// type 1 图片，3 是视频
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="type"></param>
    public static void SaveMediaFile(string filePath, IMediaType type)
    {
        InsightAPPNativeAPI.nbSaveMediaFile(filePath, (int)type);
    }

    /// <summary>
    /// 判断是否是wifi环境
    /// </summary>
    /// <returns></returns>
    public static bool IsWifi()
    {
        return InsightAPPNativeAPI.nbIsWifi();
    }

    /// <summary>
    /// 判断network是否可用
    /// </summary>
    /// <returns></returns>
    public static bool IsNetworkAvaible()
    {
        return InsightAPPNativeAPI.nbIsNetworkAvailable();
    }

    /// <summary>
    /// 进入设置界面
    /// </summary>
    public static void GotoSetting()
    {
        InsightAPPNativeAPI.nbGoSettings();
    }

    /// <summary>
    /// 从相册选择图片
    /// </summary>
    public static void PickImageFromAlbum()
    {
        InsightAPPNativeAPI.nbPickImageFromAlbum();
    }

    /// <summary>
    /// 获取gps位置
    /// </summary>
    public static void QueryLocationGPS()
    {
        InsightAPPNativeAPI.nbGetCurrentGPS();
        #if UNITY_EDITOR
        IGPSResult point = new IGPSResult();
        point.latitude = 30.2449830000;
        point.longitude = 130.2365650000;
        point.status = IGPSResultStatus.IGPSResultStateSuccess;
        GPSCallback(point);
        #endif
    }

    /// <summary>
    /// /// 加载地图资源。 geoJsonString：楼层相关信息，json串，floor：默认楼层
    /** geoJsonString 具体键值如下：
     [
      {
         floor : "1",
         floorName : "C6-1F",
         geoFilePath : "./geo/c6_1.json"
      }
     ]
     */
    ///floor 初始所在楼层
    /// </summary>
    /// <param name="geoPath"></param>
    public static void LoadMapData(string geoJsonString,int floor)
    {
        InsightAPPNativeAPI.nbLoad2DMapData(geoJsonString, floor);
    }

    /// <summary>
    /// 设置mapbox位置
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="direction"></param>
    public static void SetCurrentLocation(string locJsonString)
    {
        InsightAPPNativeAPI.nbUpdate2DMapLocation(locJsonString);
    }

    /// <summary>
    /// mapbox poi list
    /// </summary>
    /// <param name="poiList"></param>
    public static void SetPoiList(string poiStr)
    {
        InsightAPPNativeAPI.nbSet2DMapPoiList(poiStr);
    }

    /// <summary>
    /// unity->native 查询的路径点信息
    /// </summary>
    /// <param name="poiInfo"></param>
    public static void SetCurrentNaviPath(INavigationType type, string jsonStr)
    {
        InsightAPPNativeAPI.nbUpdate2DMapNaviPath(type, jsonStr);
    }

    /// <summary>
    ///通知当前poi场景类型
    /// </summary>
    /// <param name="eventType"></param>
    public static void SetAREvent(IAREventType eventType)
    {
        InsightAPPNativeAPI.nbSetAREvent(eventType);
    }

    /// <summary>
    /// 退出导航
    /// </summary>
    public static void ExitNavigation()
    {
        InsightDebug.Log(TAG, "Exit Navigation ");
        InsightAPPNativeAPI.nbExit2DNavigation();
    }

    /// <summary>
    /// 设置mapbox是否隐藏
    /// </summary>
    /// <param name="enable"></param>
    public static void SetMapUIVisibility(int enable)
    {
        InsightAPPNativeAPI.nbSet2DMapVisibility(enable);
    }

    /// <summary>
    /// 判断是否是底部有白色滑动条
    /// </summary>
    /// <returns></returns>
    public static bool IsBottomBarSeries()
    {
        return InsightAPPNativeAPI.nbIsBottomBarSeries();
    }

    /// <summary>
    /// 添加toast
    /// </summary>
    /// <param name="text"></param>
    /// <param name="during"></param>
    public static void MakeToast(string text, int during)
    {
        InsightDebug.Log(TAG, "Unity Call Make Toast " + text);
        InsightAPPNativeAPI.nbMakeToast(text, during);
    }

    /// <summary>
    /// 截屏
    /// </summary>
    /// <param name="path"></param>
    public static void TakePhoto(string path)
    {
        InsightAPPNativeAPI.nbTakePhoto(path);
    }

    /// <summary>
    /// release
    /// </summary>
    private static void Release()
    {
        InsightAPPNativeAPI.nbRelease();
    }


    public static void OpenQRCodeScan()
    {
        InsightAPPNativeAPI.nbOpenQRCodeScan();

#if UNITY_EDITOR
        //a9QOVdCY,E0GeCZ0V,wsamVpwf,vEZfYGcn,NEF59H1w,yDcFIYG1,hRaA9zFm,jQzXZv1H
        ScanQRCodeCallback("yzLYmUrK");
#endif
    }

    public static void StopQRCodeScan()
    {
        InsightAPPNativeAPI.nbStopQRCodeScan();
    }

    /// <summary>
    /// 默认为true
    /// </summary>
    /// <param name="scanning"></param>
    public static void SetScaning(bool scanning)
    {
        InsightAPPNativeAPI.nbSetScannnig(scanning);
    }

    public static void Vibrate()
    {
        InsightAPPNativeAPI.nbVibrate();
    }

    public static void SetCaputureUIVisible(int visible)
    {
        InsightAPPNativeAPI.nbSetRecordViewVisibility(visible);
    }

    /// <summary>
    ///  通知native 核销码失效
    /// </summary>
    public static void InvalidQRCode()
    {
        InsightAPPNativeAPI.nbInvalidQRCode();
    }

    public static void CloudLocationRequest(double timestamp, string jpgStr, string protoBufferStr) {
        InsightAPPNativeAPI.nbCloudLocationRequest(timestamp, jpgStr, protoBufferStr);
#if UNITY_EDITOR

#endif
    }

    /// <summary>
    /// 与APP交互
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="version"></param>
    /// <param name="type"></param>
    /// <param name="desc"></param>
    public static void ar3DEventMessage(int identity, int version, int type, string desc) {
        InsightDebug.Log(TAG, "3devent message: " + desc);
        InsightAPPNativeAPI.nb3DEventMessage(identity, version, type, desc);
#if UNITY_EDITOR
        if (type == 100) {
            DoScriptCallback("callback: " + desc);
        }
#endif
    }

    public static void webRequest(string url, string method, string header, string body, string user_data) {

        //Debug.Log("url: " + url + "\nheader: " + header + "\nbody: " + body);
        InsightAPPNativeAPI.nbRequest(url, method, header, body, user_data);
    }

    public static void SetWebRequestCallback(IHttpNetworkCallback networkCallback) {

        if (networkCallback == null) {
            InsightDebug.LogError(TAG, "http callback set null");
            return;
        }
        InsightAPPNativeAPI.nbSetRequestCallback(networkCallback);
    }

    public static void DownloadFile(string url, string downloadPath, string user_data)
    {
        InsightAPPNativeAPI.nbDownloadFile(url, downloadPath, user_data);
    }

    public static void SetDownloadFileCallback(IDownloadSuccessCallback onSuccess, IDownloadProgressCallback onProgress, IDownloadFailCallback onFail)
    {

        if (onSuccess == null || onFail == null)
        {
            InsightDebug.LogError(TAG, "download success or fail callback set null");
            return;
        }
        InsightAPPNativeAPI.nbSetDownloadCallback(onSuccess, onProgress, onFail);
    }

#if UNITY_ANDROID
    public static void SetCurrentAREngineType(AREngines_Type type)
    {
        InsightAPPNativeAPI.nbSetCurrentAREngineType((int)type);
    }
#endif

    public static void nbSetUnityLoadingProgress(double progress, IARSceneLoadingType type)
    {
        InsightDebug.Log(TAG, progress.ToString());
        string user_data = EncodeUtility.MD5(TimeUtility.GetTimeStampMilli() + progress.ToString());
        InsightAPPNativeAPI.nbSetUnityLoadingProgress(user_data, progress, type);
    }

    public static void nbSetUnityLoadingCompleted(double delay_to_open, IARSceneLoadingType type)
    {
        string user_data = EncodeUtility.MD5(TimeUtility.GetTimeStampMilli() + delay_to_open.ToString());
        InsightDebug.Log("Loading scene complete: ", delay_to_open.ToString());
#if UNITY_IOS
        InsightAPPNativeAPI.nbSetUnityLoadingCompleted(user_data, delay_to_open, type);
#elif UNITY_ANDROID
        InsightAPPNativeAPI.nbSetUnityLoadingCompleted(user_data, 0, type);
#endif
    }

    /// <summary>
    /// 调用外部导航
    /// </summary>
    /// <param name="destName"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="navMode"></param>
    public static void CallExternalNavi(string destName,double latitude,double longitude,IExternalNavMode navMode)
    {
        Debug.Log("Unity Call Call External Navi " + destName + " " + latitude + " " + longitude + " " + navMode);
        InsightAPPNativeAPI.nbCallExternalNavi(destName, latitude, longitude, navMode);
    }

    /// <summary>
    /// 返回设备名称
    /// </summary>
    /// <returns></returns>
    public static string GetDeviceModel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        IntPtr intPtr = InsightAPPNativeAPI.nbGetDeviceModel();
        string deviceName = Marshal.PtrToStringAnsi(intPtr);
        Marshal.FreeHGlobal(intPtr);
        return deviceName;
#else
        return SystemInfo.deviceModel;
#endif
    }
#endregion

#region callback
    /// <summary>
    /// 请求权限回调
    /// </summary>
    /// <param name="type"></param>
    /// <param name="status"></param>
    [MonoPInvokeCallback(typeof(IPermissionStateCallback))]
    public static void PermissionStateCallback(IAuthorizationType type, IAuthorizationStatus status)
    {
        var copyType = type;
        var copyStatus = status;
        PermissionResult permissionResult = new PermissionResult(copyType, copyStatus);
        listPermissionResult.Add(permissionResult);
    }

    /// <summary>
    /// 请求上传nos回调
    /// </summary>
    /// <param name="state"></param>
    [MonoPInvokeCallback(typeof(IUploadCallback))]
   public static void UploadImageCallback(ImageUploadState state)
    {
        var copyState = state;
        listImageUpdateResult.Add(copyState);
    }

    /// <summary>
    /// 请求相册选择照片回调
    /// </summary>
    /// <param name="path"></param>
    [MonoPInvokeCallback(typeof(IPickImageCallback))]
   public static void PickImageCallback(string path)
    {
        var copyPath = path;
        listPickImageResult.Add(copyPath);
    }

    /// <summary>
    /// 导航回调
    /// </summary>
    /// <param name="poiName"></param>
    [MonoPInvokeCallback(typeof(INavigationCallback))]
    public static void NavigationCallback(INavigationType type, string poiId)
    {
        var copyType = type;
        var copyPoiId = poiId;
        NavigationResult navigationResult = new NavigationResult(copyType, copyPoiId);
        listNavigationResult.Add(navigationResult);
    }


    /// <summary>
    /// gps 回调
    /// </summary>
    /// <param name="poiName"></param>
    [MonoPInvokeCallback(typeof(IGPSCallback))]
    public  static void GPSCallback(IGPSResult result)
    {
        //var copyResult = result;
        //listGPSResult.Add(copyResult);
    }

    /// <summary>
    /// native event 回调
    /// </summary>
    /// <param name="poiName"></param>
    [MonoPInvokeCallback(typeof(IUserEventCallback))]
    public static void UserEventCallback(IUserEventType type)
    {
        var copyType = type;
        listUserEventResult.Add(copyType);
    }

    /// <summary>
    /// 二维码扫描回调
    /// </summary>
    /// <param name="str"></param>
    [MonoPInvokeCallback(typeof(IScannedQRCodeCallback))]
    public static void ScanQRCodeCallback(string str)
    {
        var copyStr = str;
        listScanQRCodeResult.Add(copyStr);
    }

    /// <summary>
    /// AR内容本地路径
    /// 启动AR！！！
    /// </summary>
    /// <param name="config"></param>
    [MonoPInvokeCallback(typeof(IARResourceLocalPathCallback))]
    public static void ARContentStartCallback(string config)
    {
        LSGameManager.Instance.InitARScene();
        var copyConfig = config;
        ContentStartManager.lsContentLoaderBridge(copyConfig);
    }

    /// <summary>
    /// AR内容本地路径回调
    /// </summary>
    /// <param name="str"></param>
    [MonoPInvokeCallback(typeof(ICloudLocationCallback))]
    public static void CloudLocRequestCallback(int code, string result)
    {
        var copyCode = code;
        var copyResult = result;
        InsightCloudRequestResult restult = new InsightCloudRequestResult()
        {
            algCode = copyCode,
            algResult = copyResult
        };
        listCloudRequestResult.Add(restult);
    }

    /// <summary>
    /// rundcript回调
    /// </summary>
    /// <param name="str"></param>
    [MonoPInvokeCallback(typeof(IDoScriptCallback))]
    public static void DoScriptCallback(string script)
    {
        string copyScript = script;
        listDoScriptResult.Add(copyScript);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poistatus"></param>
    [MonoPInvokeCallback(typeof(IPoiStatusCallback))]
    public static void PoiStatusCallback(string poistatus)
    {
        string poiStatusJsonStr = poistatus;
        listPoiStatusResult.Add(poiStatusJsonStr);
        //IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
        //DuktapeUtility.CallMethod(context, 
        //    Insight.Navigation.moduleObject_poiStatus.heapPtr, 
        //    Insight.Navigation.funcObjcet_poiStatus.heapPtr, poiStatusJsonStr);

    }
#endregion

}
