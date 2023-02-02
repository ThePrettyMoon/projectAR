using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARWorldEditor
{
    public class TextureCache : Singleton<TextureCache>
    {
        #region params
        private const string TAG = "TextureCache";
        //存储所有的textureInfo
        private static List<TextureInfo> cachedTextures;

        //默认最大2G
        private const uint MAXSTORAGESIZE = 2 * 1024;

        private static float textureStorageSize = 0.0f;

        #endregion


        #region custom functions
        public void Init()
        {
            cachedTextures = new List<TextureInfo>();
        }

        /// <summary>
        /// 返回texture
        /// </summary>
        /// <param name="url"></param>
        /// 都用本地path 作为hashcode
        /// <returns></returns>
        public Texture2D GetTexture(string hashCode)
        {
            if (cachedTextures == null || cachedTextures.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < cachedTextures.Count; i++)
            {
                if (cachedTextures[i].hashCode == hashCode)
                {
                    return cachedTextures[i].texture;
                }
            }
            return null;
        }

        /// <summary>
        /// add texture
        /// </summary>
        /// <param name="hashCode"></param>
        /// <param name="url"></param>
        /// <param name="texture"></param>
        public void AddTexture(string hashCode, string url, Texture2D texture)
        {
            if (cachedTextures == null) cachedTextures = new List<TextureInfo>();
            for (int i = 0; i < cachedTextures.Count; i++)
            {
                if (cachedTextures[i].hashCode == hashCode)
                {
                    Debug.Log("texture " + url + " already exits!");
                    return;
                }
            }

            TextureInfo textureInfo;

            while (textureStorageSize >= MAXSTORAGESIZE)
            {
                float textureSize = cachedTextures[0].size;
                textureStorageSize -= textureSize;
                ClearTextureInfo(cachedTextures[0]);
                cachedTextures.RemoveAt(0);
            }

            textureInfo = new TextureInfo();
            textureInfo.size = (float)(4 * texture.width * texture.height / 1024.0f / 1024.0f);
            textureInfo.width = (uint)texture.width;
            textureInfo.height = (uint)texture.height;
            textureInfo.texture = texture;
            textureInfo.hashCode = hashCode;
            // textureInfo.sprite =  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));  //需要评估必要性

            cachedTextures.Add(textureInfo);
            textureStorageSize += textureInfo.size;
        }

        /// <summary>
        /// clear texture info
        /// </summary>
        /// <param name="textureInfo"></param>
        private void ClearTextureInfo(TextureInfo textureInfo)
        {
            if (textureInfo == null) return;

            if (textureInfo.texture != null)
            {
                UnityEngine.Object.Destroy(textureInfo.texture);
            }
            if (textureInfo.sprite != null)
            {
                UnityEngine.Object.Destroy(textureInfo.sprite);
            }
        }

        /// <summary>
        /// close
        /// </summary>
        public void Close()
        {
            if (cachedTextures != null)
            {
                for (int i = 0; i < cachedTextures.Count; i++)
                {
                    ClearTextureInfo(cachedTextures[i]);
                }
                cachedTextures.Clear();
            }

            textureStorageSize = 0;
        }

        #endregion

    }

    /// <summary>
    ///  纹理信息
    /// </summary>
    public class TextureInfo
    {
        public string hashCode;
        public float size;
        public uint width;
        public uint height;
        public Texture2D texture;
        public Sprite sprite;
    }
}