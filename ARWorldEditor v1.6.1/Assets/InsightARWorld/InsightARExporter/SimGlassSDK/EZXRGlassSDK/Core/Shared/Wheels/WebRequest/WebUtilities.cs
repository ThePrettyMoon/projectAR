using UnityEngine;
using System.Collections;
using System.IO;

namespace Wheels.Unity
{
    public class WebUtilities : MonoBehaviour
    {

        #region 单例、构造
        private static WebUtilities _instance = null;
        private static readonly object SynObject = new object();

        public static WebUtilities instance
        {
            get
            {
                lock (SynObject)
                {
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.AddComponent(typeof(WebUtilities));
                        _instance = obj.GetComponent<WebUtilities>();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        // Use this for initialization
        void Start()
        {
	
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">文件网络地址</param>
        /// <param name="filePath">文件本地存储地址</param>
        public void Download(string url, string filePath)
        {
            if (File.Exists(filePath))
            {
                long lStartPos = 0; 
                FileStream fs = System.IO.File.OpenWrite(filePath); 
                lStartPos = fs.Length; 
                fs.Seek(lStartPos, SeekOrigin.Current); //移动文件流中的当前指针 
            }
        }
    }
}