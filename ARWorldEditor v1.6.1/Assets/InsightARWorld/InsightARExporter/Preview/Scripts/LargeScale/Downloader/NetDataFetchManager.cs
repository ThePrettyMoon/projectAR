using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using EZXR.NET;
using System;
using System.IO;

/// <summary>
///  大场景业务数据类
/// </summary>
public class NetDataFetchManager:Singleton<NetDataFetchManager>
{
    #region params
    private const string TAG = "NetDataFetchManager";
    #endregion

    #region 一期
    /// <summary>
    /// 获取设备AR支持情况
    /// </summary>
    /// <param name="callback"></param>
    public void CheckARSupport( OnOasisNetworkDataFetchCallback<DeviceFeatureResponseData> callback)
    {
        AW_ClientInfo clientInfo = ClientInfos.GetClientInfo();
        DeviceFeatureRequest deviceRequest = new DeviceFeatureRequest(clientInfo);
        deviceRequest.Query(new HttpRequestListener(
            (BaseRequest request, object obj) =>
        {
            DeviceFeatureResponseData response = (DeviceFeatureResponseData)obj;
            callback.onNetworkDataSucc(response);
        }, (BaseRequest request, string code, string msg) =>
        {
            callback.onNetworkDataError(code, msg);
        }));
    }

    /// <summary>
    /// 登陆功能
    /// </summary>
    /// <param name="appKey"></param>
    /// <param name="appSecret"></param>
    /// <param name="packageName"></param>
    /// <param name="callback"></param>
    public void Login(string appKey, string appSecret, string packageName, int platformId, OnOasisNetworkDataFetchCallback<LoginResponseData> callback)
    {
        LoginUserData loginUser = new LoginUserData(appKey, appSecret, packageName, platformId);
        LoginRequest loginRequest = new LoginRequest(loginUser);
        loginRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            callback.onNetworkDataSucc((LoginResponseData)obj);
        }, (BaseRequest request, string code, string msg) =>
        {
            callback.onNetworkDataError(code, msg);
        }));
    }

    /// <summary>
    /// 请求时间戳
    /// </summary>
    public void QueryTimestamp(OnOasisNetworkDataFetchCallback<TimestampResponseData> callback)
    {
        TimestampRequest timestampRequest = new TimestampRequest();
        timestampRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((TimestampResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// SDK配置
    /// </summary>
    public void QuerySDKConfig(OnOasisNetworkDataFetchCallback<SDKConfigResponseData> callback)
    {
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        SDKConfigRequestData data = new SDKConfigRequestData(aw_ClientInfo);
        SDKConfigRequest sdkConfigRequest = new SDKConfigRequest(data, appKey, appSecret, token);
        sdkConfigRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((SDKConfigResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 获取动态so（匿名）（仅安卓）
    /// </summary>
    public void QuerySDKResources(string abi, OnOasisNetworkDataFetchCallback<SDKResourcesResponseData> callback)
    {
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        SDKResourcesRequestData data = new SDKResourcesRequestData(abi, aw_ClientInfo);
        SDKResourcesRequest sdkResourcesRequest = new SDKResourcesRequest(data);
        sdkResourcesRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((SDKResourcesResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 查询子内容
    /// </summary>
    /// <param name="contentId"></param>
    public void QuerySubContent(int contentId, OnOasisNetworkDataFetchCallback<SubContentResponseData> callback)
    {
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        SubContentRequestData data = new SubContentRequestData(contentId, aw_ClientInfo);
        SubContentRequest subContentRequest = new SubContentRequest(data, appKey, appSecret, token);
        subContentRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                var result = (SubContentResponseData)obj;
                callback.onNetworkDataSucc(result);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 查询子内容快照
    /// </summary>
    /// <param name="contentId"></param>
    public void QuerySubContentSnapshot(int contentId, int sceneId, OnOasisNetworkDataFetchCallback<SubContentSnapshotResponseData> callback)
    {
        long time = TimeUtility.GetTimeStampMilli();
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        SubContentSnapshotRequestData data = new SubContentSnapshotRequestData(contentId, sceneId, aw_ClientInfo);
        SubContentSnapshotRequest subContentSnapshotRequest = new SubContentSnapshotRequest(data, appKey, appSecret, token);
        subContentSnapshotRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((SubContentSnapshotResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 查询主内容
    /// </summary>
    /// <param name="contentId"></param>
    public void QueryLandmarker(int contentId, OnOasisNetworkDataFetchCallback<LandmarkerResponseData> callback)
    {
        long time = TimeUtility.GetTimeStampMilli();
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        LandmarkerRequestData data = new LandmarkerRequestData(contentId, aw_ClientInfo);
        LandmarkerRequest landmarkerRequest = new LandmarkerRequest(data, appKey, appSecret, token);
        landmarkerRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((LandmarkerResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 查询主内容列表
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <param name="coordType"></param>
    /// <param name="maxDistance"></param>
    /// <param name="tagIds"></param>
    /// <param name="callback"></param>
    public void QueryLandmarkers(long lat, long lng, int coordType, double maxDistance, string tagIds, OnOasisNetworkDataFetchCallback<LandmarkersResponseData> callback)
    {
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        LandmarkersRequestData data = new LandmarkersRequestData(lat, lng, coordType, maxDistance, tagIds, aw_ClientInfo);
        LandmarkersRequest landmarkerRequest = new LandmarkersRequest(data, appKey, appSecret, token);
        landmarkerRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((LandmarkersResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 查询（周围）主内容列表
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <param name="coordType"></param>
    /// <param name="callback"></param>
    public void QueryLandmarkersNearby(double lat, double lng, int coordType, OnOasisNetworkDataFetchCallback<LandmarkersNearbyResponseData> callback)
    {
        long time = TimeUtility.GetTimeStampMilli();
        LandmarkersNearbyRequestData data = new LandmarkersNearbyRequestData();
        data.latitude = lat;
        data.longitude = lng;
        data.coordType = coordType;
        data.client = ClientInfos.GetClientInfo();

        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        LandmarkersNearbyRequest nearbyScenesRequest = new LandmarkersNearbyRequest(data, appKey, appSecret, token);
        nearbyScenesRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            LandmarkersNearbyResponseData response = (LandmarkersNearbyResponseData)obj;
            if (callback != null)
            {
                callback.onNetworkDataSucc(response);
                //todo


            }
        }, (BaseRequest request, string code, string msg) =>
        {
            InsightDebug.Log(TAG, "Query Neary Scenes Error code = " + code + " msg = " + msg);
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 查询主内容列表（根据标签查询）
    /// </summary>
    /// <param name="contentId"></param>
    /// <param name="dataType"></param>
    public void QueryLandmarkersByTags(string tagIds, OnOasisNetworkDataFetchCallback<LandmarkersByTagsResponseData> callback)
    {
        long time = TimeUtility.GetTimeStampMilli();
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        LandmarkersByTagsRequestData data = new LandmarkersByTagsRequestData(tagIds, aw_ClientInfo);
        LandmarkersByTagsRequest landmarkersByTagsRequest = new LandmarkersByTagsRequest(data, appKey, appSecret, token);
        landmarkersByTagsRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((LandmarkersByTagsResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }


    /// <summary>
    /// 查询内容（扫描二维码）
    /// </summary>
    /// <param name="contentId"></param>
    /// <param name="dataType"></param>
    public void QueryContentsByQRCode(string qrCode, OnOasisNetworkDataFetchCallback<ContentsByQRCodeResponseData> callback)
    {
        AW_ClientInfo aw_ClientInfo = ClientInfos.GetClientInfo();
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        ContentsByQRCodeRequestData data = new ContentsByQRCodeRequestData(qrCode, aw_ClientInfo);
        ContentsByQRCodeRequest contentsByQRCodeRequest = new ContentsByQRCodeRequest(data, appKey, appSecret, token);
        contentsByQRCodeRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((ContentsByQRCodeResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 请求代理
    /// </summary>
    public void QueryProxy(string requestId, string requestUrlParms, string requestHeaders, 
        string requestBody, string requestExtensions, OnOasisNetworkDataFetchCallback<ProxyResponseData> callback)
    {
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
        string token = InsightConfigManager.Instance.GetToken();
        ProxyRequestData data = new ProxyRequestData(requestId, requestUrlParms, requestHeaders, requestBody, requestExtensions);
        ProxyRequest proxyRequest = new ProxyRequest(data, appKey, appSecret, token);
        proxyRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataSucc((ProxyResponseData)obj);
            }
        }, (BaseRequest request, string code, string msg) =>
        {
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));
    }

    /// <summary>
    /// 云端重定位更新
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="sceneId"></param>
    /// <param name="pId"></param>
    /// <param name="callback"></param>
    public void QueryCloudLoc(string imageBase64Data, string protoBase64Data, string cloudUrl,
        OnOasisNetworkDataFetchCallback<CloudResponseData> callback)
    {
        long time = TimeUtility.GetTimeStampMilli();
        CloudRequestData data = new CloudRequestData();
        data.alg.imageEncodingData = imageBase64Data;
        data.alg.protobufEncodingData = protoBase64Data;
        string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
        string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();

        CloudRequest cloudRequest = new CloudRequest(data, time, appKey, appSecret);

        cloudRequest.ParseUrl(cloudUrl);

        //加上网络状态判断，如果网络不好，给出toast提示
        //先检查网络状态
        if (!NetworkUtility.IsNetworkAvaible())
        {
            NotifyNativeMessage.MakeToast("网络异常，请检查网络状态");
            return;
        }

;        cloudRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
        {
            CloudResponseData response = (CloudResponseData)obj;
            if (callback != null)
            {
                callback.onNetworkDataSucc(response);
            }

        }, (BaseRequest request, string code, string msg) =>
        {
            InsightDebug.Log(TAG, "Query Cloud Location Error code = " + code + " msg = " + msg);
            if (callback != null)
            {
                callback.onNetworkDataError(code, msg);
            }
        }));

    }
    #endregion
}
