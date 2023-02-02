using UnityEngine.Networking;

namespace ARWorldEditor
{
    /// <summary>
    /// 纹理请求
    /// </summary>
    public class HttpRequestGetTexture : HttpRequestCreateBase
    {

        public static HttpRequestGetTexture Get(string _uri)
        {
            HttpRequestGetTexture httpRequestTexture = new HttpRequestGetTexture();
            httpRequestTexture.uri = _uri;
            return httpRequestTexture;
        }

        public override void CreateWebRequest()
        {
            unityWebRequest = GenerateWebRequest();
        }

        public UnityWebRequest GenerateWebRequest()
        {
            return UnityWebRequestTexture.GetTexture(uri);
        }
    }
}
