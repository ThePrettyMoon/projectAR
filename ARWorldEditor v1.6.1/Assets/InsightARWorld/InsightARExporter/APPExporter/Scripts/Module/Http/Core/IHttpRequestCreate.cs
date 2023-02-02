using UnityEngine.Networking;

namespace ARWorldEditor
{
    public interface IHttpRequestCreate
    {
        UnityWebRequest GetWebRequest();

        void CreateWebRequest();

        void Dispose();
    }
}
