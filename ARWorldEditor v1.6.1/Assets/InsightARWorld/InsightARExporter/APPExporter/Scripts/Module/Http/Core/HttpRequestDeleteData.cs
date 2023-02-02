using UnityEngine.Networking;

namespace ARWorldEditor
{
    public class HttpRequestDeleteData : HttpRequestCreateBase
    {
        public static HttpRequestDeleteData Delete(string _uri)
        {
            HttpRequestDeleteData httpRequestDeleteData = new HttpRequestDeleteData();
            httpRequestDeleteData.uri = _uri;
            return httpRequestDeleteData;
        }

        public override void CreateWebRequest()
        {
            unityWebRequest = GenerateWebRequest();
        }

        private UnityWebRequest GenerateWebRequest()
        {
            return UnityWebRequest.Delete(uri);
        }
    }
}
