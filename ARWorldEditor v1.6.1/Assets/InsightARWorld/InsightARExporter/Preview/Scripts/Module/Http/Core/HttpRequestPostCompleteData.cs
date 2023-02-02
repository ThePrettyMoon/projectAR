using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Text;
using EZXR.NET;

/// <summary>
/// post请求
/// </summary>
public class HttpRequestPostCompleteData : HttpRequestCreateBase
{
    private byte[] bytes;
    private string contentType;
    private Dictionary<string, string> headers;
    private int timeout;
    private bool useHttpContinue;

    public static HttpRequestPostCompleteData Post(string _uri, byte[] _bytes, string _contentType, Dictionary<string, string> _headers)
    {
        return Post(_uri, _bytes, _contentType, _headers, 10);
    }

    public static HttpRequestPostCompleteData Post(string _uri, byte[] _bytes, string _contentType)
    {
        return Post(_uri, _bytes, _contentType, null, 10);
    }

    public static HttpRequestPostCompleteData Post(string _uri, byte[] _bytes, string _contentType, Dictionary<string, string> _headers,
        int _timeout, bool _useHttpContinue = true)
    {
        HttpRequestPostCompleteData httpRequestPostData = new HttpRequestPostCompleteData();
        httpRequestPostData.uri = _uri;
        httpRequestPostData.bytes = _bytes;
        httpRequestPostData.contentType = _contentType;
        httpRequestPostData.headers = _headers;
        httpRequestPostData.timeout = _timeout;
        httpRequestPostData.useHttpContinue = _useHttpContinue;
        return httpRequestPostData;
    }


    public override void CreateWebRequest()
    {
        unityWebRequest = GenerateWebRequest();
    }

    private UnityWebRequest GenerateWebRequest()
    {
        var unityWebRequest = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bytes)
            {
                contentType = contentType
            },
            downloadHandler = new DownloadHandlerBuffer(),
            //support https
            certificateHandler = new AcceptAllCertificatesSigned()
        };
        if (headers != null && headers.Count > 0)
        {
            foreach (var header in headers)
            {
                unityWebRequest.SetRequestHeader(header.Key, header.Value);
            }
        }
        unityWebRequest.useHttpContinue = useHttpContinue;
        unityWebRequest.timeout = timeout;
        return unityWebRequest;
    }
}
