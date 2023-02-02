using System.Collections.Generic;
using ARWorldEditor;
using UnityEngine;
using UnityEngine.Networking.Types;

namespace ARWorldEditor
{
    public class NetDataFetchManager : Singleton<NetDataFetchManager>
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="callback"></param>
        public void Login(string email, string pwd, OnOasisNetworkDataFetchCallback<LoginResponseData> callback)
        {
            LoginRequestData requestData = new LoginRequestData();
            requestData.email = email;
            requestData.pwd = EncodeUtility.MD5(pwd).ToLower();
            LoginRequest loginRequest = new LoginRequest(requestData);
            loginRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                LoginResponseData response = (LoginResponseData)obj;
                InsightConfigManager.Instance.SetToken(response.result);
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 获取我的内容列表
        /// </summary>
        /// <param name="callback"></param>
        [System.Obsolete("新版本使用 MyContents")]
        public void GetMyContents(OnOasisNetworkDataFetchCallback<GetMyContentsResponseData> callback)
        {
            long timestamp = TimeUtility.GetTimeStampMilli();
            BaseAuthRequestData authRequstData = BaseAuthRequestData.GetBaseAuthRequestData(timestamp);
            GetMyContentsRequestData requestData = new GetMyContentsRequestData(authRequstData);
            GetMyContentsRequest getMyContentsRequest = new GetMyContentsRequest(requestData);
            getMyContentsRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetMyContentsResponseData response = (GetMyContentsResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        public void MyContents(OnOasisNetworkDataFetchCallback<MyContentsResponseData> callback)
        {
            long timestamp = TimeUtility.GetTimeStampMilli();
            BaseAuthRequestData authRequstData = BaseAuthRequestData.GetBaseAuthRequestData(timestamp);
            MyContentsRequestData requestData = new MyContentsRequestData(authRequstData);
            MyContentsRequest getMyContentsRequest = new MyContentsRequest(requestData);

            getMyContentsRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                MyContentsResponseData response = (MyContentsResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 上传POI数据
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="poiData"></param>
        /// <param name="callback"></param>
        public void UploadPOIData(long contentId, string poiData, OnOasisNetworkDataFetchCallback<UploadPOIDataResponseData> callback)
        {
            UploadPOIDataRequestData requestData = new UploadPOIDataRequestData();
            requestData.contentId = contentId;
            requestData.poiData = poiData;
            UploadPOIDataRequest uploadPOIDataRequest = new UploadPOIDataRequest(requestData);
            uploadPOIDataRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                UploadPOIDataResponseData response = (UploadPOIDataResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 上传内容包
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="contentPackageId"></param>
        /// <param name="updateDes"></param>
        /// <param name="resourceList"></param>
        /// <param name="callback"></param>
        public void UploadContentPackage(long contentId, long contentPackageId, string updateDes, List<ContentPackageData> resourceList, OnOasisNetworkDataFetchCallback<UploadContentPackageResponseData> callback)
        {
            long timestamp = TimeUtility.GetTimeStampMilli();
            BaseAuthRequestData authRequstData = BaseAuthRequestData.GetBaseAuthRequestData(timestamp);
            UploadContentPackageRequestData requestData = new UploadContentPackageRequestData(authRequstData);
            requestData.contentId = contentId;
            requestData.contentPackageId = contentPackageId;
            requestData.updateDes = updateDes;
            requestData.resourceList = resourceList;
            UploadContentPackageRequest uploadContentPackageRequest = new UploadContentPackageRequest(requestData);
            uploadContentPackageRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                UploadContentPackageResponseData response = (UploadContentPackageResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 获取某个内容下所有的内容包版本信息
        /// </summary>
        public void GetContentPackageVersions(int contentId, OnOasisNetworkDataFetchCallback<GetContentPackageVersionsResponseData> callback)
        {
            long timestamp = TimeUtility.GetTimeStampMilli();
            BaseAuthRequestData authRequstData = BaseAuthRequestData.GetBaseAuthRequestData(timestamp);
            GetContentPackageVersionsRequestData requestData = new GetContentPackageVersionsRequestData(authRequstData);
            requestData.contentId = contentId;
            GetContentPackageVersionsRequest getContentPackageVersionsRequest = new GetContentPackageVersionsRequest(requestData);
            getContentPackageVersionsRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetContentPackageVersionsResponseData response = (GetContentPackageVersionsResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 获取地图资源
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="callback"></param>
        public void GetMapResources(long mapId, OnOasisNetworkDataFetchCallback<GetMapResourcesResponseData> callback)
        {
            long timestamp = TimeUtility.GetTimeStampMilli();
            BaseAuthRequestData authRequstData = BaseAuthRequestData.GetBaseAuthRequestData(timestamp);
            GetMapResourcesRequestData requestData = new GetMapResourcesRequestData(authRequstData);
            requestData.mapId = mapId;
            GetMapResourcesRequest getMapResourcesRequest = new GetMapResourcesRequest(requestData);
            getMapResourcesRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetMapResourcesResponseData response = (GetMapResourcesResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 获取Nos上传Token配置
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="callback"></param>
        public void GetNosToken(OnOasisNetworkDataFetchCallback<GetNosTokenResponseData> callback)
        {
            GetNosTokenRequestData requestData = new GetNosTokenRequestData();
            GetNosTokenRequest getNosTokenRequest = new GetNosTokenRequest(requestData);
            getNosTokenRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetNosTokenResponseData response = (GetNosTokenResponseData)obj;
                NOSConfig.SetNosBucket(response.result.bucket);
                NOSConfig.SetNosToken(response.result.token);
                NOSConfig.SetNosObject(response.result.nos_obj);
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 获取所有sdk版本信息
        /// </summary>
        /// <param name="callback"></param>
        public void GetAllSDKVersions(OnOasisNetworkDataFetchCallback<GetAllSDKVersionsResponseData> callback)
        {
            GetAllSDKVersionsRequestData requestData = new GetAllSDKVersionsRequestData();
            GetAllSDKVersionsRequest getAllSDKVersionsRequest = new GetAllSDKVersionsRequest(requestData);
            getAllSDKVersionsRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetAllSDKVersionsResponseData response = (GetAllSDKVersionsResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="callback"></param>
        public void Logout(OnOasisNetworkDataFetchCallback<LogoutResponseData> callback)
        {
            long timestamp = TimeUtility.GetTimeStampMilli();
            BaseAuthRequestData authRequstData = BaseAuthRequestData.GetBaseAuthRequestData(timestamp);
            LogoutRequestData requestData = new LogoutRequestData(authRequstData);
            LogoutRequest logoutRequest = new LogoutRequest(requestData);
            logoutRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                LogoutResponseData response = (LogoutResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 返回POI数据
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="poiData"></param>
        /// <param name="callback"></param>
        public void GetPOIData(long contentId, OnOasisNetworkDataFetchCallback<GetPoiDataResponseData> callback)
        {
            GetPoiDataRequestData requestData = new GetPoiDataRequestData();
            requestData.contentId = contentId;
            GetPoiDataRequest getPoiDataRequest = new GetPoiDataRequest(requestData);
            getPoiDataRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetPoiDataResponseData response = (GetPoiDataResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        /// <summary>
        /// 获取能进行POI操作的子内容列表
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="callback"></param>
        public void GetContentsForPOI(long contentId, int engineType, OnOasisNetworkDataFetchCallback<GetContentsForPoiResponseData> callback)
        {
            GetContentsForPoiRequestData requestData = new GetContentsForPoiRequestData();
            requestData.contentId = contentId;
            requestData.engineType = engineType;
            GetContentsForPoiRequest getContentsForPoiRequest = new GetContentsForPoiRequest(requestData);
            getContentsForPoiRequest.Query(new HttpRequestListener((BaseRequest request, object obj) =>
            {
                GetContentsForPoiResponseData response = (GetContentsForPoiResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, (BaseRequest request, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        #region 1.4.2 
        /// <summary>
        /// 给某一个内容新增一个版本
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="engineType"></param>
        /// <param name="sdkVersion"></param>
        /// <param name="callback"></param>
        public void CreateContentVersion(long contentId, int engineType,int sdkVersion,int deviceType, OnOasisNetworkDataFetchCallback<CreateContentVersionResponseData> callback)
        {
            CreateContentVersionRequestData requestData = new CreateContentVersionRequestData();
            requestData.contentId = contentId;
            requestData.engineType = engineType;
            requestData.sdkVersionId = sdkVersion;
            requestData.deviceType = deviceType;
            CreateContentVersionRequest request = new CreateContentVersionRequest(requestData);
            request.Query(new HttpRequestListener((BaseRequest baseRequest, object obj) => 
            {
                CreateContentVersionResponseData response = (CreateContentVersionResponseData)obj;
                callback.onNetworkDataSucc(response);
            }, 
            (BaseRequest baseRequest, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        public void GetAppSdkVersions(long appID,int deviceType, OnOasisNetworkDataFetchCallback<GetAppSdkVersionsResponseData> callback)
        {
            GetAppSdkVersionsRequestData requestData = new GetAppSdkVersionsRequestData();
            requestData.appId = appID;
            requestData.deviceType = deviceType;

            GetAppSdkVersionsRequest request = new GetAppSdkVersionsRequest(requestData);
            request.Query(new HttpRequestListener((BaseRequest baseRequest, object obj) =>
            {
                GetAppSdkVersionsResponseData response = (GetAppSdkVersionsResponseData)obj;
                callback.onNetworkDataSucc(response);
            },
            (BaseRequest baseRequest, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        public void GetUserInfo(OnOasisNetworkDataFetchCallback<GetUserInfoResponseData> callback)
        {
            GetUserInfoRequestData requestData = new GetUserInfoRequestData();

            GetUserInfoRequest request = new GetUserInfoRequest(requestData);
            request.Query(new HttpRequestListener((BaseRequest baseRequest, object obj) =>
            {
                GetUserInfoResponseData response = (GetUserInfoResponseData)obj;
                callback.onNetworkDataSucc(response);
            },
            (BaseRequest baseRequest, string code, string msg) =>
            {
                callback.onNetworkDataError(code, msg);
            }));
        }

        #endregion
    }
}
