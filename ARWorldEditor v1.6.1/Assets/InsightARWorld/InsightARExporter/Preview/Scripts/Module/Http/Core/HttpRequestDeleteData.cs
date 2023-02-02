using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

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
