using System.Collections.Generic;
using UnityEngine.Networking;

namespace ARWorldEditor
{
    /// <summary>
    /// 封装request post form data
    /// </summary>
    public class HttpRequestPostMultiFormData : HttpRequestCreateBase
    {
        private List<IMultipartFormSection> multipartForm;

        public static HttpRequestPostMultiFormData Post(string _uri, List<IMultipartFormSection> _formData)
        {
            HttpRequestPostMultiFormData requestPostFormData = new HttpRequestPostMultiFormData();
            requestPostFormData.uri = _uri;
            requestPostFormData.multipartForm = _formData;
            return requestPostFormData;
        }

        public override  void CreateWebRequest()
        {
            unityWebRequest = GenerateWebRequest();
        }
        private UnityWebRequest GenerateWebRequest()
        {
            return UnityWebRequest.Post(uri, multipartForm);
        }
    }
}
