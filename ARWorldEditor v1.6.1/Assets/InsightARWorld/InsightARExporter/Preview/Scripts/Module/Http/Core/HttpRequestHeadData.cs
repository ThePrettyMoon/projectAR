using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class HttpRequestHeadData : HttpRequestCreateBase
{

    public static HttpRequestHeadData Head(string _uri)
    {
        HttpRequestHeadData httpRequestHeadData = new HttpRequestHeadData();
        httpRequestHeadData.uri = _uri;
        return httpRequestHeadData;
    }

    public override  void CreateWebRequest()
    {
        unityWebRequest = GenerateWebRequest();
    }

    private UnityWebRequest GenerateWebRequest()
    {
        return UnityWebRequest.Head(uri);
    }
}
