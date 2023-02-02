using System;

namespace ARWorldEditor
{
    public class HttpRequestListener
    {
        private const string REQUEST_OK = "000000";

        public Action<BaseRequest, object> onSuccess;
        public Action<BaseRequest, string, string> onError;
        public  Action<float> onUpload;
        public Action<float> onDownload;

        public HttpRequestListener(Action<BaseRequest, object> _onSuccess, Action<BaseRequest, string, string> _onError
            , Action<float> _onDownload=null, Action<float> _onUpload=null)
        {
            onSuccess = _onSuccess;
            onError = _onError;
            onDownload = _onDownload;
            onUpload = _onUpload;
        }

    }
}
