using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    public interface OnHttpCallback 
    {
        void OnResult(string result, IHttpBaseRequest request);

        void OnError(Exception e, IHttpBaseRequest request);
    }
}
