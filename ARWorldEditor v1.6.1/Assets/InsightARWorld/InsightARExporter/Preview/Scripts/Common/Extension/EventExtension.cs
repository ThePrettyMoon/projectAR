using Duktape;
using InsightAR.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 截屏参数，满足多种截屏需求，仅对unity sdk 有效
/// </summary>
public class CaptureParams {
    public string cameraname;   //相机名称
    public string rawimagename; //rawimage， 存储相机截图
}

public class ParamTable
{
    public string url;
    public class type
    {
        public int urlType;
        public string title;
        public string param1;
    }
}
public class ShareParams {
    /// <summary>
    /// 1:文本
    /// 2:图片
    /// 3:音频
    /// 4:视频
    /// 5:链接
    /// </summary>
    public int type;   
    public string title;    //标题
    public string desc;     //文本分享内容
    public string identity;
    public string url;
    public string music;
    public string video;
    public string screenshot;
    public string image;    //logo路径"/ShareLogo/ShareLogo.png"
    public string imagepath;
    /// <summary>
    ///-- 1:分享图片-加框-加码
    ///-- 2:分享图片-不加框-加码
    ///-- 3:分享图片-内嵌码
    ///-- 0:分享图片-不处理
    /// </summary>
    public int imageformat;
}

public static class EventExtension
{
    private const string TAG = "EventExtension";
    /// <summary>
    /// 兼容SDK中通知上层应用发生事件
    /// </summary>
    /// <param name="identity">Identity.</param>
    /// <param name="version">Version.</param>
    /// <param name="type">Type.</param>
    /// <param name="desc">Desc.</param>
    public static void Happen(int identity, int version, int type, string desc)
    {
        switch (type)
        {
            case 100: //runscript
            case 115: //埋点
                InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
                break;
            case 108: //跳转链接
#if UNITY_EDITOR_WIN
                ParamTable tmp = JsonUtil.Deserialization<ParamTable>(desc);
                //ApplicationExtension.OpenUrl(tmp.url);
                System.Diagnostics.Process.Start("IExplore.exe", tmp.url);
#elif UNITY_ANDROID || UNITY_IOS
                InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
#endif
                break;
            case 101: //退出
                OnExitHandler(desc);
                break;
            case 102: //重启算法和应用
                OnReloadARHandler(desc);
                break;
            case 110: //截屏
                if (string.IsNullOrEmpty(desc)) {
                    string path = CaptureUtility.TakePhoto();
                    desc = "{\"imagepath\":\"" + path + "\"}";
                    InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
                    break;
                }
                CaptureParams captureParams = JsonUtil.Deserialization<CaptureParams>(desc);
                if (!string.IsNullOrEmpty(captureParams.rawimagename) && !string.IsNullOrEmpty(captureParams.cameraname))
                {
                    CaptureUtility.TakePhoto(captureParams.cameraname, captureParams.rawimagename);
                    //do nothing to native
                    //set the rt in rawimage
                }
                else { 
                    if(!string.IsNullOrEmpty(captureParams.cameraname))
                    {
                        string path = CaptureUtility.TakePhoto(captureParams.cameraname);
                        desc = "{\"imagepath\":\"" + path + "\"}";
                        InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
                    }
                }
                break;
            case 111: //分享
                ShareParams shareParams = JsonUtil.Deserialization<ShareParams>(desc);
                if (shareParams.type == 2)
                {
                    shareParams.imagepath = CaptureUtility.TakePhoto();
                    if (string.IsNullOrEmpty(shareParams.imagepath))
                    {
                        NotifyNativeMessage.MakeToast("分享图片失败！");
                        return;
                    }
                }
                else if (shareParams.type == 4) {
                    shareParams.video = InsightNativeRecord.recordedVideoPath;
                    if (string.IsNullOrEmpty(shareParams.video)) {
                        NotifyNativeMessage.MakeToast("分享视频失败！");
                        return;
                    }
                }
                desc = JsonUtil.Serialize(shareParams);
                InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
                break;
            case 112:
                //唤起录屏
                ReplayCamManager.Instance.StartRecording();
                break;
            case 113:
                //结束录屏
                ReplayCamManager.Instance.StopRecording();
                break;
            case 10005: //maketoast
                OnMakeToastHandler(desc);
                break;
            case 10006: //弹对话框
                //InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
                //break;
            case 10007: //震动
                InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
                break;
            case 10008: //下载或者加载poi内容
                //OnDownloadPoiContent(desc);
                //break;
                InsightDebug.Log(TAG, "On Download Poi Content " + desc);
                InsightAPPNative.ar3DEventMessage(identity, version, type, desc);
#if UNITY_EDITOR
                PoiEventData poiEventData = JsonUtil.Deserialization<PoiEventData>(desc);
                if (poiEventData.activetype == 0)
                {
                    //download poi
                    POIStatusData poiStatusData = new POIStatusData();
                    poiStatusData.downloadResult = "1";
                    poiStatusData.cid = poiEventData.cid;
                    poiStatusData.NeedUpdate = "0";
                    InsightAPPNative.PoiStatusCallback(JsonUtil.Serialize(poiStatusData));
                }
                else {
                    //load poi
                    var scenePath = Application.dataPath.Replace(@"/Assets", @"/EditorDebug/Product/434");
                    var sceneName = "POI";
                    var localPath = "{\"localPath\":\"" + scenePath + "\",\"sceneName\":\"" + sceneName + "\",\"cid\":\"" + "434" + "\",\"loadMode\":\"0\"}";
                    InsightAPPNative.ARContentStartCallback(localPath);
                }
#endif
                break;
            case 10009: //卸载poi内容
                OnUnloadPoiContentHandler(desc);
                break;
            case 10010:  //退出导航
                OnExitNavigationHandler(desc);
                break;
            case 10011:  // 设置地图可见性
                OnMapVisibleHandler(desc);
                break;

            //视频相关
            case 10020:  //内容唤起Native录屏或拍照
                OnRecordOrPhotoActiveHandler(desc);
                break;
            //视频相关
            case 10021:  //内容停止Native录屏或拍照
                OnRecordOrPhotoCloseHandler(desc);
                break;

        }
    }


    public static void HappenCallback(DuktapeObject moduleObject, DuktapeObject funcObjcet) {

        InsightAPPNative.arDoScriptEvent = null;
        InsightAPPNative.arDoScriptEvent += (string script) =>
        {
            InsightDebug.Log(TAG, "script: " + script);
            IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
            DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr, script);
        };

    }

    private static void OnReloadARHandler(string desc)
    {
        LSGameManager.Instance.ResetARScene();
    }

    /// <summary>
    /// 退出处理
    /// </summary>
    /// <param name="jsonStr"></param>
    private static void OnExitHandler(string jsonStr)
    {
        LSGameManager.Instance.ExitARScene();
    }

    /// <summary>
    /// 下载或者加载poi内容
    /// </summary>
    /// <param name="jsonStr"></param>
    private static void OnDownloadPoiContent(string jsonStr)
    {
        InsightDebug.Log(TAG, "On Download Poi Content " + jsonStr);
#if UNITY_EDITOR
        PoiEventData poiEventDataTemp = new PoiEventData
        {
            cid = "630",
            sid = "0",
            activetype = 1
        };
        jsonStr = JsonUtil.Serialize(poiEventDataTemp);
#endif

        PoiEventData poiEventData = JsonUtil.Deserialization<PoiEventData>(jsonStr);

        InsightDebug.Log(TAG, "On Download Poi Content " + jsonStr);

        //检查是否在下载中，如果在下载中，不处理
        int cid = StringUtility.ParseInt(poiEventData.cid);
        string contnetId = poiEventData.cid;
        ArProduct localProduct = InsightCacheManager.Instance.Query<ArProduct>(cid);
        if (localProduct != null && (localProduct.DownloadState == DownloadState.RUNNING || localProduct.UnzipState == UnZipState.RUNNING))
        {
            InsightDebug.Log(TAG, "pid == " + contnetId + " is downloading or unziping");
            GameSceneData.Instance.UpdatePOIDownloadResult(contnetId, POIDownloadResult.DOWNLOAD_DOING_OR_UNDO);
            return;
        }
        //给定初始状态
        GameSceneData.Instance.UpdatePOINeedUpdate(contnetId, POINeedUpdateData.NEEDUPDATE);
        NormalEventDataFetch.Instance.DownloadProductById(poiEventData.cid, (string code, string msg) =>
        {
            GameSceneData.Instance.UpdatePOIDownloadResult(contnetId, POIDownloadResult.DOWNLOAD_FAILED);
            InsightDebug.Log(TAG, "pid == " + poiEventData.cid + " code " + code + " msg " + msg);
            NotifyNativeMessage.MakeToast("请求内容异常！");
        }, (ArProduct product, DownloadProductState state) =>
        {
            GameSceneData.Instance.UpdatePOIDownloadResult(contnetId, POIDownloadResult.DOWNLOAD_DONE);
            //下载完进入场景
            if (poiEventData.activetype == 1)
            {
                ProductData productData = new ProductData();
                productData.SetSceneId(product.Sid);
                productData.SetProductId(product.Cid);
                productData.SetProduct(product);
                productData.SetProductFileRoot(Path.Combine(ConstPath.RootDirectory(), ConstPath.ProductDirectory(), product.Cid.ToString()));
                SceneController.Instance.LoadPoiScene(productData);
            }
            else
            {
                GameSceneData.Instance.GetProductList().AddOrUpdateProduct(product);
            }
        }, (float progress) => {
            Debug.Log(cid + ": " + progress);
        });
    }

    /// <summary>
    /// 处理退出poi内容消息
    /// </summary>
    /// <param name="jsonStr"></param>
    private static void OnUnloadPoiContentHandler(string jsonStr)
    {
        Debug.Log("Unity Call Unload Poi Scene " + jsonStr);
        PoiEventData poiEventData = JsonUtil.Deserialization<PoiEventData>(jsonStr);
        //SceneController.Instance.UnloadPoiScene();
#if UNITY_EDITOR
        poiEventData.cid = "434";
#endif
        SceneController.Instance.lsUnloadPoiScene(poiEventData.cid);
    }

    /// <summary>
    /// 退出导航消息(来自内容)
    /// </summary>
    /// <param name="jsonStr"></param>
    private static void OnExitNavigationHandler(string jsonStr)
    {
        //InsightAPPNative.ExitNavigation();//通知native
        NaviSceneManager.Instance.EndNavi();//通知导航算法

        //返回location
        LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_LOCATION);
    }

    /// <summary>
    /// 处理地图信息
    /// </summary>
    /// <param name="jsonStr"></param>
    private static void OnMapVisibleHandler(string jsonStr)
    {
        MapVisibleEventData mapVisibleEventData = JsonUtil.Deserialization<MapVisibleEventData>(jsonStr);
        int visible = StringUtility.ParseInt(mapVisibleEventData.visibility);
        NotifyNativeMessage.SetMapVisibility(visible);
    }

    /// <summary>
    /// 弹框提示
    /// </summary>
    /// <param name="jsonStr"></param>
    private static void OnMakeToastHandler(string jsonStr)
    {
        MakeToastEventData makeToastEventData = JsonUtil.Deserialization<MakeToastEventData>(jsonStr);
        int during = StringUtility.ParseInt(makeToastEventData.time);
        if (makeToastEventData.command == "1")
        {
            NotifyNativeMessage.MakeToast(makeToastEventData.text, during);
        }
    }


    private static void OnRecordOrPhotoActiveHandler(string jsonStr) {

        //todo
        InsightAPPNative.SetAREvent(IAREventType.IAREventTypeEnableRecordOrPhoto);
    }

    private static void OnRecordOrPhotoCloseHandler(string jsonStr)
    {
        //todo
        InsightAPPNative.SetAREvent(IAREventType.IAREventTypeDisenableRecordOrPhoto);
    }
    
}
