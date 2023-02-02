using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.NET;
using Duktape;

/// <summary>
/// 缓存内容层的网络回调的注册
/// </summary>
public struct NetworkForContent {
    public DuktapeObject duktapeObj;    //脚本
    public DuktapeObject methodObj;     //方法
}

public class LsHttpNetWorkWithNative:Singleton<LsHttpNetWorkWithNative>
{

    private static string TAG = "LsHttpNetWorkWithNative";
    private const string REQUEST_OK = "00000000";
    private const string NATIVE_HTTP_OK = "200";

    /// <summary>
    /// 网络请求结构
    /// </summary>
    private class NetworkData
    {
        public string userdata; //string
        public long timestamp;  //long,时间戳
        /// <summary>
        /// 标记是否是SDK的请求，如是，需求进行结果解析后使用，如不是直接暴露给内容
        /// </summary>
        public bool isSdkRequest;
        //SDK请求参数
        public BaseRequest mRequest = null;
        public HttpRequestListener mListener = null;
        //内容请求
        public NetworkForContent dukContent = new NetworkForContent();
    }
    /// <summary>
    /// 请求缓冲池
    /// </summary>
    private static Dictionary<string, NetworkData> listNetworkData;
    private static Dictionary<string, List<string[]>> listCurrentDataTemp;
    /// <summary>
    /// 大场景网络请求返回方法
    /// </summary>
    InsightAPPNative.IHttpNetworkCallback networkCallback;

    /// <summary>
    /// 
    /// </summary>
    public void Init() {
        listNetworkData = new Dictionary<string, NetworkData>();
        listCurrentDataTemp = new Dictionary<string, List<string[]>>();
        networkCallback = lsNetworkResultResponse;
        //确定网络回调方法
        InsightAPPNative.SetWebRequestCallback(networkCallback);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Close() {
        listNetworkData.Clear();
        listCurrentDataTemp.Clear();
        networkCallback = null;
    }

    /// <summary>
    /// SDK层网络请求
    /// </summary>
    /// <param name="baseRequest"></param>
    /// <param name="requestListener"></param>
    public void lsNetworkRequest(BaseRequest baseRequest, HttpRequestListener requestListener)
    {
        BaseHttpJsonBodyCreate bodyCreate = new BaseHttpJsonBodyCreate(baseRequest);
        string url = bodyCreate.Url();
        string header = bodyCreate.strHeaders();
        string body = bodyCreate.Body();
        //string contentType = bodyCreate.ContentType();
        string method = baseRequest.GetMethod();

        // create unique id
        string http_userdata = EncodeUtility.MD5(url + header + body);
        var http_timestamp = TimeUtility.GetTimeStampMilli();
        NetworkData data = new NetworkData
        {
            userdata = http_userdata + http_timestamp.ToString(),
            timestamp = http_timestamp,
            isSdkRequest = true,
            mRequest = baseRequest,
            mListener = requestListener
        };

        listNetworkData.TryGetValue(data.userdata, out NetworkData datas);
        if (datas != null)
            listNetworkData[data.userdata] = data;
        else
            listNetworkData.Add(data.userdata,  data );

        InsightAPPNative.webRequest(url, method, header, body, data.userdata);

    }

    /// <summary>
    /// 内容层网络请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="header"></param>
    /// <param name="body"></param>
    /// <param name="moduleObject"></param>
    /// <param name="funcObjcet"></param>
    public void lsNetworkRequest(string url, string method, string header, string body,
        DuktapeObject moduleObject, DuktapeObject funcObjcet)
    {
        //InsightDebug.Log("httpback", url);
        //InsightDebug.Log("httpback", string.IsNullOrEmpty(url).ToString());
        if (string.IsNullOrEmpty(url))
            return;
        if (string.IsNullOrEmpty(method))
            method = "POST";

        // create unique id
        string http_userdata = EncodeUtility.MD5(url + header + body);
        var http_timestamp = TimeUtility.GetTimeStampMilli();

        NetworkData data = new NetworkData
        {
            userdata = http_userdata + http_timestamp.ToString(),
            timestamp = http_timestamp,
            isSdkRequest = false,
            dukContent = new NetworkForContent {
                duktapeObj = moduleObject,
                methodObj = funcObjcet
            }
        };

        listNetworkData.TryGetValue(data.userdata, out NetworkData datas);
        if (datas != null)
            listNetworkData[data.userdata] = data;
        else
            listNetworkData.Add(data.userdata,  data );

        InsightAPPNative.webRequest(url, method, header, body, data.userdata);
#if UNITY_EDITOR
        LsHttpNetWorkWithNative.lsNetworkResultResponse(data.userdata, "200", "no msg", "response");
#endif
    }


    /// <summary>
    /// 大场景网络请求，native返回
    /// 注意：超时或网络异常都由客户端处理
    /// </summary>
    /// <param name="responseResult"></param>
    [MonoPInvokeCallback(typeof(InsightAPPNative.IHttpNetworkCallback))]
    public static void lsNetworkResultResponse(string userdata, string resultCode, string resultMsg, string responseResult)
    {
        string copyUserdata = userdata;
        string copyResultCode = resultCode;
        string copyResultMsg = resultMsg;
        string copyResponseResult = responseResult;

        Debug.Log("network userdata: " + copyUserdata + "\nresponseResult: " + copyResponseResult);

        listNetworkData.TryGetValue(copyUserdata, out NetworkData DataTemp);
        
        if (DataTemp != null) {
            // do response
            //OnResultWrap(responseResult, DataTemp[0].mRequest, DataTemp[0].mListener);
            ReParseHttpResponse(copyResultCode, copyResultMsg, copyResponseResult, DataTemp);
            listNetworkData.Remove(copyUserdata);
        }
        else {
            InsightDebug.Log(TAG, "http response back but excutor is missing");
        }
    }

    private static void ReParseHttpResponse(string resultCode, string resultMsg, string responseResult, NetworkData data) {

        if (data.isSdkRequest)
        {
            OnResultWrap(responseResult, data.mRequest, data.mListener);
        }
        else {
            if (resultCode != NATIVE_HTTP_OK)
            {
                responseResult = "{\"resultCode\":\"" + resultCode + "\",\"resultMsg\":\"" + resultMsg + "\",\"responseResult\":\"" + responseResult + "\"}";
            }
            OnResultWrap(responseResult, data.dukContent);
        }

    }

    private static void OnResultWrap(string result, BaseRequest baseRequest, HttpRequestListener requestListener) {

        InsightDebug.Log(TAG, "On Http Result" + result);
        BaseResponseData bResponse = JsonUtil.Deserialization(result, typeof(BaseResponseData)) as BaseResponseData;
        if (bResponse == null)
        {
            requestListener.onError?.Invoke(baseRequest, NetworkCode.NETWORK_ERROR.ToString(), "other error,api response is null");
            return;
        }

        string code = bResponse.GetCode();
        if (string.IsNullOrEmpty(code))
        {
            requestListener.onError?.Invoke(baseRequest, code, bResponse.GetMsg());
            return;
        }

        if (!code.Equals(REQUEST_OK))
        {
            requestListener.onError?.Invoke(baseRequest, code, bResponse.GetMsg());
            return;
        }
        //InsightDebug.Log(TAG, "result: " + bResponse.GetResult());
        //InsightDebug.Log(TAG, "result: " + mRequest.GetModel().ToString());
        //InsightDebug.Log(TAG, "result: " + JsonUtil.Serialize(bResponse.GetResult()));
        var data = JsonUtil.Deserialization(JsonUtil.Serialize(bResponse.GetResult()), baseRequest.GetModel());
        //InsightDebug.Log(TAG, JsonUtil.Serialize(data));
        requestListener.onSuccess?.Invoke(baseRequest, data);

    }

    private static void OnResultWrap(string result, NetworkForContent conetent)
    {
        if (conetent.methodObj == null || conetent.duktapeObj == null) {
            InsightDebug.LogError(TAG, "content http response callback is null");
            return;
        }
        IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
        DuktapeUtility.CallMethod(context, conetent.duktapeObj.heapPtr, conetent.methodObj.heapPtr, result);
    }

}
