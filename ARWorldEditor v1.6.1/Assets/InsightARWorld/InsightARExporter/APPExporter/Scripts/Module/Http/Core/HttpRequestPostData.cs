using UnityEngine.Networking;

namespace ARWorldEditor
{
    /// <summary>
    /// 封装request post form data
    /// </summary>
    public class HttpRequestPostData : HttpRequestCreateBase
    {
        private string data;

        public static HttpRequestPostData Post(string _uri,string _data)
        {
            HttpRequestPostData requestPostData = new HttpRequestPostData();
            requestPostData.uri = _uri;
            requestPostData.data = _data;
            return requestPostData;
        }

        public override  void CreateWebRequest()
        {
            unityWebRequest = GenerateWebRequest();
        }

        private UnityWebRequest GenerateWebRequest()
        {
            return UnityWebRequest.Post(uri, data);
        }
    }
}
