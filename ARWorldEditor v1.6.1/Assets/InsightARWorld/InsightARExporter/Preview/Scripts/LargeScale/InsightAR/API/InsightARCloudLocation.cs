using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;
using System;
using System.Runtime.InteropServices;

/// <summary>
/// 云端重定位算法
/// </summary>
public class InsightARCloudLocation
{
    #region
    private const string TAG = "InsightARCloudLocation";
    public int CloudLocAlgCode = 16;//初始值未定位失败，1表示成功
    public string CloudLocAlgReason = "";
    public long CloudLocTotalCount = 0;
    public long CloudSuccessCount=0;
    #endregion

    #region custon functions

    /// <summary>
    /// 添加重定位监听
    /// </summary>
    public void AddListener()
    {
        //InsightARInterface.cloudLocAction += OnCloudLocHandler;
        InsightARInterface.cloudLocAction += OnCloudLocNativeRequestHandler;
        InsightAPPNative.arCloudLocEvent += OnCloudLocNativeCallbackHandler;

        CloudLocTotalCount = 0;
        CloudLocAlgReason = "";
        CloudLocAlgCode = 0;
        CloudSuccessCount = 0;
    }

    /// <summary>
    /// 移除重定位监听
    /// </summary>
    public void RemoverListener()
    {
        //InsightARInterface.cloudLocAction -= OnCloudLocHandler;
        InsightARInterface.cloudLocAction -= OnCloudLocNativeRequestHandler;
        InsightAPPNative.arCloudLocEvent -= OnCloudLocNativeCallbackHandler;

        CloudLocTotalCount = 0;
        CloudLocAlgReason = "";
        CloudLocAlgCode = 0;
        CloudSuccessCount = 0;
    }

    /// <summary>
    /// 底层算法发起云端重定位请求
    /// </summary>
    /// <param name="requestData"></param>
    private void OnCloudLocHandler(InsightARCloudLocRequestImpl requestData)
    {
        InsightDebug.Log(TAG, "Cloud Loc Request ");
        CloudLocRequest(requestData);
    }

    /// <summary>
    /// 发起云定位请求
    /// </summary>
    /// <param name="requestData"></param>
    private void OnCloudLocNativeRequestHandler(InsightARCloudLocRequestImpl requestData)
    {
        double timestamp = 0;
        string imageBase64 = requestData.jpgStr;
        string protoBase64 = requestData.requestInfoStr;
        InsightAPPNative.CloudLocationRequest(timestamp, imageBase64, protoBase64);
        InsightDebug.Log(TAG, "cloud request to native");

        CloudLocTotalCount++;

        if (CloudLocTotalCount == 1)
        {
            //开始云定位埋点
            TrackDataManager.SetTrackData(TrackDataManager.EventID.ar_int_start);
        }
    }

    /// <summary>
    /// 处理云定位回掉
    /// </summary>
    /// <param name="resultData"></param>
    private void OnCloudLocNativeCallbackHandler(InsightCloudRequestResult resultData)
    {
        InsightDebug.Log(TAG, resultData.algCode.ToString());
        //服务器或者客户端报错会+10000，客户端报错是>-400
        CloudLocAlgCode = resultData.algCode;
        if (CloudLocAlgCode >= 9000) {
            InsightDebug.Log(TAG, resultData.algResult.ToString());
            CloudLocAlgReason = resultData.algResult;
            return;
        }
        CloudLocAlgReason = "";
        CloudLocResult(resultData.algResult);
        InsightDebug.Log(TAG, "cloud response from native");

        //云定位成功计数
        if(CloudLocAlgCode==1)
        {
            CloudSuccessCount++;
            if(CloudSuccessCount==1)
            {
                //初次云定位成功埋点
                TrackDataManager.SetTrackData(TrackDataManager.EventID.ar_int_success);
            }
        }
    }

//#if UNITY_IOS
    //private void OnCloudLocNativeCallbackHandler(InsightARCloudLocResult resultData)
    //{
    //    InsightDebug.Log(TAG, resultData.resultInfoPtr.ToString());

    //    CloudLocResult(resultData);

    //    InsightDebug.Log(TAG, "cloud response from native");
    //}

    //private void OnCloudLocNativeRequestHandler(InsightARCloudLocRequest requestData)
    //{
    //    InsightAPPNative.CloudLocationRequest(requestData);
    //    InsightDebug.Log(TAG, "cloud request to native");
    //}
//#elif UNITY_ANDROID
    //private void OnCloudLocNativeCallbackHandler(string responseData)
    //{
    //    InsightDebug.Log(TAG, responseData);

    //    CloudLocResult(responseData);

    //    InsightDebug.Log(TAG, "cloud response from native");
    //}

    //private void OnCloudLocNativeRequestHandler(InsightARCloudLocRequestImpl requestData) 
    //{
    //    double timestamp = 0;
    //    string imageBase64 = requestData.jpgStr;
    //    string protoBase64 = requestData.requestInfoStr;
    //    InsightAPPNative.CloudLocationRequest(timestamp, imageBase64, protoBase64);
    //    InsightDebug.Log(TAG, "cloud request to native");
    //}
//#endif

    /// <summary>
    /// 云端重定位请求
    /// </summary>
    /// <param name="requestData"></param>
    private void CloudLocRequest(InsightARCloudLocRequestImpl requestData)
    {
        string imageBase64 = requestData.jpgStr;
        string protoBase64 = requestData.requestInfoStr;

        string cloudUrl = InsightConfigManager.Instance.GetMainContentCloudUrl();

        NetDataFetchManager.Instance.QueryCloudLoc(imageBase64, protoBase64, cloudUrl, new OnOasisNetworkDataFetchCallback<CloudResponseData>(
         (CloudResponseData data) =>
         {
             CloudLocResult(data.protobufEncodingData);
         }, (string code, string msg) =>
         {
             CloudLocAlgCode = int.Parse( code );
             CloudLocAlgReason = msg;
             InsightDebug.Log(TAG, "Query Cloud Error: code == " + code + " msg == " + msg);
         }));
    }

    /// <summary>
    /// 处理云端重定位服务器返回的结果
    /// </summary>
    /// <param name="requestData"></param>
    /// <param name="protoData"></param>
    private void CloudLocResult(string protoData)
    {
        InsightARCloudLocResult insightARCloudLocResult = new InsightARCloudLocResult();
        InsightARCloudLocResultMeta insightARCloudLocResultMeta = new InsightARCloudLocResultMeta();
        insightARCloudLocResultMeta.timestamp = 0.0;
        insightARCloudLocResultMeta.status = 0;
        insightARCloudLocResult.meta = insightARCloudLocResultMeta;
        try {
            //base64 解码
            byte[] buffer = Convert.FromBase64String(protoData);
            int length = buffer.Length;
            IntPtr resultPtr = Marshal.AllocHGlobal(length);
            Marshal.Copy(buffer, 0, resultPtr, length);
            insightARCloudLocResult.resultInfoPtr = resultPtr;
            insightARCloudLocResult.resultLength = length;

            //同步调用
            InsightARNative.iarlsOnCloudLocalizedNative(insightARCloudLocResult);

            //供底层算法调用之后，释放指针
            Marshal.FreeHGlobal(resultPtr);
        }
        catch (FormatException exp) {
            InsightDebug.Log(TAG, "Format Error: " + exp);
        }
       
    }

    private void CloudLocResult(InsightARCloudLocResult resultData)
    {
        //同步调用
        InsightARNative.iarlsOnCloudLocalizedNative(resultData);
    }

    #endregion
}
