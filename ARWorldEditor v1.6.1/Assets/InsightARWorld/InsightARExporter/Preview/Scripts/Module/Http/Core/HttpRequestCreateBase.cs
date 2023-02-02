using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EZXR.NET;

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
