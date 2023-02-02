using System.Collections;

namespace ARWorldEditor
{
    public interface IHttpBaseRequest 
    {
        void Abort();
        IEnumerator Send();

        void Dispose();
    }
}