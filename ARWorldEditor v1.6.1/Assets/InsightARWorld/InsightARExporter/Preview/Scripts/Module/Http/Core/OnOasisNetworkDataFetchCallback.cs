using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
