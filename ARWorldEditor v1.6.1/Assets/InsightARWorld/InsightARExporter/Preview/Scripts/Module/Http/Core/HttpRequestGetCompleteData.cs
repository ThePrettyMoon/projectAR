using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using EZXR.NET;
using UnityEngine.Networking;

public class HttpRequestGetCompleteData : HttpRequestCreateBase
{
    private Dictionary<string, string> headers;
    private int timeout;
    private bool useHttpContinue;

    public static HttpRequestGetCompleteData Get(string _uri, Dictionary<string, string> _headers, int _timeout,
        bool _useHttpContinue)
    {
        HttpRequestGetCompleteData httpRequestGetCompleteData = new HttpRequestGetCompleteData();
        httpRequestGetCompleteData.uri = _uri;
        httpRequestGetCompleteData.headers = _headers;
        httpRequestGetCompleteData.timeout = _timeout;
        httpRequestGetCompleteData.useHttpContinue = _useHttpContinue;
        return httpRequestGetCompleteData;
    }

    public override void CreateWebRequest()
    {
        unityWebRequest = GenerateWebRequest();
    }

    private UnityWebRequest GenerateWebRequest()
    {

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);
        // support https
        unityWebRequest.certificateHandler = new AcceptAllCertificatesSigned();

        if (headers != null && headers.Count > 0)
        {
            foreach (var header in headers)
            {
                unityWebRequest.SetRequestHeader(header.Key, header.Value);
            }
        }
        unityWebRequest.timeout = timeout;
        unityWebRequest.useHttpContinue = useHttpContinue;
        return unityWebRequest;
    }

}
