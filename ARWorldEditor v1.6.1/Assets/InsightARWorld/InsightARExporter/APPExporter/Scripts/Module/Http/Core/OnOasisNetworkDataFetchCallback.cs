using System;

namespace ARWorldEditor
{
    public class OnOasisNetworkDataFetchCallback<T>
    {
        public Action<T> onNetworkDataSucc;
        public Action<string, string> onNetworkDataError;

        public OnOasisNetworkDataFetchCallback(Action<T> onSuccess, Action<string, string> onError)
        {
            onNetworkDataError = onError;
            onNetworkDataSucc = onSuccess;
        }

    }
}
