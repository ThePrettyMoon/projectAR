using ARWorldEditor;
using UnityEngine;

namespace ARWorldEditor
{
    public class HttpRequestWrap 
    {
        private const string TAG = "HttpRequestWrap";
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
            Debug.Log( "On Http Result" + resp.Text );

            ApiResponse response = JsonUtil.Deserialization(resp.Text, typeof(ApiResponse)) as ApiResponse;
            if (response == null)
            {
                mListener.onError?.Invoke(mRequest, NetworkCode.NETWORK_ERROR.ToString(), "other error,api response is null");
                return;
            }

            string code = response.Code();

            if (!code.Equals(ServerResponseCode.RESPONSE_OK))
            {
                mListener.onError?.Invoke(mRequest, code, response.Message());
                return;
            }

            var data = JsonUtil.Deserialization(resp.Text, mRequest.GetModel());
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
