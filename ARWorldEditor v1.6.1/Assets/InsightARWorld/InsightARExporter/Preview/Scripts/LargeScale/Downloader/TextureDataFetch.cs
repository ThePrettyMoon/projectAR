using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using EZXR.NET;

    /// <summary>
    /// 纹理数据下载
    /// </summary>
    public class TextureDataFetch : Singleton<TextureDataFetch>
    {
        private const string TAG = "TextureDataFetch";

        public void GetLocalTexture(string path, Action<Texture2D> onSuccess = null,Action<string ,string> onError =null)
        {
            InsightDebug.Log(TAG, "Get Local Texture path == " + path);

            string hashCode = path.GetHashCode().ToString();
            Texture2D textureCached = TextureCache.Instance.GetTexture(hashCode);
            if (textureCached != null)
            {
                if (onSuccess != null)
                {
                    onSuccess(textureCached);
                }
                return;
            }

            if (!File.Exists(path))
            {
                InsightDebug.LogError(TAG, "File " + path + " Not Exits!");
                return;
            }

            HttpManager.Instance.GetTexture("file:///" + path, onSuccess, onError,null,null);
        }

        public void GetTexture(string uri, Action<Texture2D> onSuccess = null, Action<string, string> onError = null)
        {
           // InsightDebug.Log(TAG, "Get Texture path == " + uri);

            string hashCode = uri.GetHashCode().ToString();
            Texture2D textureCached = TextureCache.Instance.GetTexture(hashCode);
            if (textureCached != null)
            {
                if (onSuccess != null)
                {
                    onSuccess(textureCached);
                }
                return;
            }

            HttpManager.Instance.GetTexture(uri, onSuccess, onError, null, null);
        }
    }

