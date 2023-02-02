using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequestPutData : HttpRequestCreateBase
{
    private string data;
    public static HttpRequestPutData Put(string _uri,string _data)
    {
        HttpRequestPutData httpRequestPutData = new HttpRequestPutData();
        httpRequestPutData.uri = _uri;
        httpRequestPutData.data = _data;
        return httpRequestPutData;
    }

    public override  void CreateWebRequest()
    {
        unityWebRequest = GenerateWebRequest();
    }

    private UnityWebRequest GenerateWebRequest()
    {
        return UnityWebRequest.Put(uri,data);
    }
}
