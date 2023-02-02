using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EZXR.NET
{
    public class HttpRequestWrap 
    {
        private const string TAG = "HttpRequestWrap";
        private const string REQUEST_OK = "00000000";
        private BaseRequest mRequest;
        HttpRequestListener mListener;

        public HttpRequestWrap(BaseRequest _request,HttpRequestListener _listener)
        {
            mRequest = _request;
            mListener = _listener;
        }

        /// <summary>
        /// 结果数据解析
        /// </summary>
        /// <param name="resp"></param>
        public void OnResult(HttpResponse resp)
        {
            InsightDebug.Log(TAG, "On Http Result" + resp.Text );
            BaseResponseData bResponse = JsonUtil.Deserialization(resp.Text, typeof(BaseResponseData)) as BaseResponseData;
            if (bResponse == null)
            {
                mListener.onError?.Invoke(mRequest, NetworkCode.NETWORK_ERROR.ToString(), "other error,api response is null");
                return;
            }

            string code = bResponse.GetCode();
            if (string.IsNullOrEmpty(code))
            {
                mListener.onError?.Invoke(mRequest, code, bResponse.GetMsg());
                return;
            }

            if (!code.Equals(REQUEST_OK))
            {
                mListener.onError?.Invoke(mRequest, code, bResponse.GetMsg());
                return;
            }
            //InsightDebug.Log(TAG, "result: " + bResponse.GetResult());
            //InsightDebug.Log(TAG, "result: " + mRequest.GetModel().ToString());
            //InsightDebug.Log(TAG, "result: " + JsonUtil.Serialize(bResponse.GetResult()));
            var data = JsonUtil.Deserialization(JsonUtil.Serialize(bResponse.GetResult()), mRequest.GetModel());
            InsightDebug.Log(TAG, JsonUtil.Serialize(data));
            mListener.onSuccess?.Invoke(mRequest, data);
        }

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="e"></param>
        /// <param name="resp"></param>
        public void OnError(string code, HttpResponse resp)
        {
            mListener.onError?.Invoke(mRequest, code, "请求异常");
        }

        /// <summary>
        /// 上传进度
        /// </summary>
        /// <param name="progress"></param>
        public void OnUpload(float progress)
        {
            mListener.onUpload?.Invoke(progress);
        }

        /// <summary>
        /// 下载进度
        /// </summary>
        /// <param name="progress"></param>
        public void OnDownload(float progress)
        {
            mListener.onDownload?.Invoke(progress);
        }
    }
}
