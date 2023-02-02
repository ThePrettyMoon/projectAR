using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EZXR.NET
{
    public class HttpRequestListener
    {
        public Action<BaseRequest, object> onSuccess;
        public Action<BaseRequest, string, string> onError;
        public Action<float> onUpload;
        public Action<float> onDownload;

        public HttpRequestListener(
            Action<BaseRequest, object> _onSuccess, 
            Action<BaseRequest, string, string> _onError, 
            Action<float> _onDownload = null, 
            Action<float> _onUpload = null)
        {
            onSuccess = _onSuccess;
            onError = _onError;
            onDownload = _onDownload;
            onUpload = _onUpload;
        }

    }
}
