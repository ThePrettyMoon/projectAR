using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ARWorldEditor
{
    public class HttpResponse
    {
        public float progress;

        public long StatusCode
        {
            get;
            set;
        }

        public byte[] Bytes
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }

        public Texture Texture
        {
            get;
            set;
        }

        public Dictionary<string,string> ResponseHeaders
        {
            get;
            set;
        }

        public bool IsSuccessful
        {
            get; set;
        }

        public string Url
        {
            get;set;
        }

        public static HttpResponse CreateResponse(UnityWebRequest unityWebRequest)
        {
            return new HttpResponse
            {
                Url = unityWebRequest.url,
                Bytes = unityWebRequest.downloadHandler?.data,
                Text = unityWebRequest.downloadHandler?.text,
                IsSuccessful = !unityWebRequest.isHttpError && !unityWebRequest.isNetworkError,
                Error = unityWebRequest.error,
                StatusCode = unityWebRequest.responseCode,
                ResponseHeaders = unityWebRequest.GetResponseHeaders(),
                Texture = (unityWebRequest.downloadHandler as DownloadHandlerTexture)?.texture
            };
        }
    }
}
