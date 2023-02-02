using System;
using System.IO;
using ARWorldEditor;
using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    /// 纹理数据下载
    /// </summary>
    public class TextureDataFetch : Singleton<TextureDataFetch>
    {
        private const string TAG = "TextureDataFetch";

        public void GetLocalTexture(string path, Action<Texture2D> onSuccess = null,Action<string ,string> onError =null)
        {
            Debug.Log(" Get Local Texture path == " + path);

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
                Debug.LogError("File " + path + " Not Exits!");
                return;
            }

            HttpManager.Instance.GetTexture("file:///" + path, onSuccess, onError,null,null);
        }

        public void GetTexture(string uri, Action<Texture2D> onSuccess = null, Action<string, string> onError = null)
        {
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
}

