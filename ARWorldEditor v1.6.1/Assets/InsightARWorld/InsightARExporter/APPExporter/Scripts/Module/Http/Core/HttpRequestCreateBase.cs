using UnityEngine.Networking;

namespace ARWorldEditor
{
    public class HttpRequestCreateBase :IHttpRequestCreate
    {
        public UnityWebRequest unityWebRequest;
        public string uri;


        public void Dispose()
        {
            if (unityWebRequest != null) unityWebRequest.Dispose();
        }

        public UnityWebRequest GetWebRequest()
        {
            return unityWebRequest;
        }

        public virtual void CreateWebRequest()
        {
  
        }
    }
}
