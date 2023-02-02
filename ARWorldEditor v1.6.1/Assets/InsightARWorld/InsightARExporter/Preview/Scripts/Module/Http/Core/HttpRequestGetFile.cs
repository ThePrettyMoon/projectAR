using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class HttpRequestGetFile : HttpRequestCreateBase
{
    private string downloadFilePath;


    public static HttpRequestGetFile Get(string _uri,string _downloadPath)
    {
        HttpRequestGetFile httpRequestFile = new HttpRequestGetFile();
        httpRequestFile.uri = _uri;
        httpRequestFile.downloadFilePath = _downloadPath;
        return httpRequestFile;
    }


    public override void CreateWebRequest()
    {
        unityWebRequest = GenerateWebRequest();
    }

    private UnityWebRequest GenerateWebRequest()
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);
        unityWebRequest.downloadHandler = new DownloadHandlerFile(downloadFilePath);
        return unityWebRequest;
    }
}
