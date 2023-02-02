using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Wheels.Unity
{
    /// <summary>
    /// 得到服务器返回的数据后返回给调用方，调用方如需要得到返回的数据必须实现此delegate，identifier供用户分辨出这个返回数据是谁的
    /// </summary>
    public delegate void GiveBackString(string data, string identifier, long statusCode = 200);
    /// <summary>
    /// 得到服务器返回的数据后返回给调用方，调用方如需要得到返回的数据必须实现此delegate，identifier供用户分辨出这个返回数据是谁的
    /// </summary>
    public delegate void GiveBackStringArray(string[] data, string[] identifier, long statusCode = 200);
    /// <summary>
    /// 得到服务器返回的数据后返回给调用方，调用方如需要得到返回的数据必须实现此delegate，identifier供用户分辨出这个返回数据是谁的
    /// </summary>
    public delegate void GiveBackBytes(byte[] data, string identifier, long statusCode = 200);
    /// <summary>
    /// 得到服务器返回的数据后返回给调用方，调用方如需要得到返回的数据必须实现此delegate，identifier供用户分辨出这个返回数据是谁的
    /// </summary>
    public delegate void GiveBackBytesArray(byte[][] data, string[] identifier, long[] statusCode);
    /// <summary>
    /// 返回当前请求的加载进度
    /// </summary>
    public delegate void GiveBackLoadingProgress(UnityEngine.Networking.UnityWebRequest unityWebRequest, string identifier);

    public enum RequestMode
    {
        GET,
        POST
    }

    public partial class UnityWebRequest : MonoBehaviour
    {
        #region 单例、构造
        private static UnityWebRequest _instance = null;
        private static readonly object SynObject = new object();

        public static UnityWebRequest Instance
        {
            get
            {
                lock (SynObject)
                {
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("UnityWebRequest");
                        obj.AddComponent(typeof(UnityWebRequest));
                        _instance = obj.GetComponent<UnityWebRequest>();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        /// <summary>
        /// 本次Request用的header
        /// </summary>
        Dictionary<string, string> headerDic = new Dictionary<string, string>();

        public class BackString
        {
            public string data;
            public string identifier;
            public GiveBackString del_GiveBack;

            public BackString(string data, string identifier, GiveBackString del_GiveBack)
            {
                this.data = data;
                this.identifier = identifier;
                this.del_GiveBack = del_GiveBack;
            }
        }

        public class BackStringArray
        {
            public string[] data;
            public string[] identifiers;
            public GiveBackStringArray del_GiveBack;

            public BackStringArray(string[] data, string[] identifiers, GiveBackStringArray del_GiveBack)
            {
                this.data = data;
                this.identifiers = identifiers;
                this.del_GiveBack = del_GiveBack;
            }
        }

        public class BackBytes
        {
            public byte[] data;
            public string identifier;
            public GiveBackBytes del_GiveBack;

            public BackBytes(byte[] data, string identifier, GiveBackBytes del_GiveBack)
            {
                this.data = data;
                this.identifier = identifier;
                this.del_GiveBack = del_GiveBack;
            }
        }

        public class BackBytesArray
        {
            public byte[][] data;
            public string[] identifiers;
            public GiveBackBytesArray del_GiveBack;

            public BackBytesArray(byte[][] data, string[] identifiers, GiveBackBytesArray del_GiveBack)
            {
                this.data = data;
                this.identifiers = identifiers;
                this.del_GiveBack = del_GiveBack;
            }
        }

        /// <summary>
        /// 用于所有文件上传完毕后的回调（除了请求的时候传入的回调，这个回调也是会被无条件回调的）
        /// </summary>
        public GiveBackString fileUploaded;


        #region 单请求
        /// <summary>
        /// 设置本次Request用的header，如果有多对header要设置，请多次调用
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public void SetRequestHeader(string key, string value)
        {
            try
            {
                headerDic.Add(key, value);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        #region WWWForm表单
        /// <summary>
        /// 发送请求（POST）
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数</param>
        public void Create(string urlKey, WWWForm para)
        {
            GiveBackString callBack = null;
            Create(urlKey, para, callBack, "", null);
        }

        /// <summary>
        /// 发送请求（POST）
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数</param>
        /// <param name="identifier">请求标识符，identifier供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public void Create(string urlKey, WWWForm para, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            GiveBackString callBack = null;
            Create(urlKey, para, callBack, identifier, giveBackLoadingProgress);
        }

        /// <summary>
        /// 发送请求（POST）
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        public void Create(string urlKey, WWWForm para, GiveBackString callBack, string identifier)
        {
            StartCoroutine(WWWRequest(urlKey, para, callBack, identifier, null));
        }

        public void Create(string urlKey, WWWForm para, GiveBackBytes callBack, string identifier)
        {
            StartCoroutine(WWWRequest(urlKey, para, callBack, identifier, null));
        }

        /// <summary>
        /// 发送请求（POST）
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public void Create(string urlKey, WWWForm para, GiveBackString callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            StartCoroutine(WWWRequest(urlKey, para, callBack, identifier, giveBackLoadingProgress));
        }

        public void Create(string urlKey, WWWForm para, GiveBackBytes callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            StartCoroutine(WWWRequest(urlKey, para, callBack, identifier, giveBackLoadingProgress));
        }
        #endregion

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        public void Create(string urlKey, RequestMode requestMode)
        {
            GiveBackString callBack = null;
            Create(urlKey, "", requestMode, callBack, "", null);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public void Create(string urlKey, RequestMode requestMode, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            GiveBackString callBack = null;
            Create(urlKey, "", requestMode, callBack, identifier, giveBackLoadingProgress);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        public void Create(string urlKey, RequestMode requestMode, GiveBackString callBack, string identifier)
        {
            Create(urlKey, "", requestMode, callBack, identifier, null);
        }

        public void Create(string urlKey, RequestMode requestMode, GiveBackBytes callBack, string identifier)
        {
            Create(urlKey, "", requestMode, callBack, identifier, null);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public void Create(string urlKey, RequestMode requestMode, GiveBackString callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            Create(urlKey, "", requestMode, callBack, identifier, giveBackLoadingProgress);
        }

        public void Create(string urlKey, RequestMode requestMode, GiveBackBytes callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            Create(urlKey, "", requestMode, callBack, identifier, giveBackLoadingProgress);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        public void Create(string urlKey, string para, RequestMode requestMode)
        {
            GiveBackString callBack = null;
            Create(urlKey, para, requestMode, callBack, "", null);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public void Create(string urlKey, string para, RequestMode requestMode, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            GiveBackString callBack = null;
            Create(urlKey, para, requestMode, callBack, identifier, giveBackLoadingProgress);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        public void Create(string urlKey, string para, RequestMode requestMode, GiveBackString callBack, string identifier)
        {
            Debug.Log("urlKey：" + urlKey + "，  para：" + para);
            if (!string.IsNullOrEmpty(urlKey))
            {
                StartCoroutine(WWWRequest(urlKey, para, requestMode, callBack, identifier, null));
            }
        }

        public void Create(string urlKey, string para, RequestMode requestMode, GiveBackBytes callBack, string identifier)
        {
            Debug.Log("urlKey：" + urlKey + "，  para：" + para);
            if (!string.IsNullOrEmpty(urlKey))
            {
                StartCoroutine(WWWRequest(urlKey, para, requestMode, callBack, identifier, null));
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="callBack">回调函数</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public void Create(string urlKey, string para, RequestMode requestMode, GiveBackString callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            Debug.Log("urlKey：" + urlKey + "，  para：" + para);
            if (!string.IsNullOrEmpty(urlKey))
            {
                StartCoroutine(WWWRequest(urlKey, para, requestMode, callBack, identifier, giveBackLoadingProgress));
            }
        }

        public void Create(string urlKey, string para, RequestMode requestMode, GiveBackBytes callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            Debug.Log("urlKey：" + urlKey + "，  para：" + para);
            if (!string.IsNullOrEmpty(urlKey))
            {
                StartCoroutine(WWWRequest(urlKey, para, requestMode, callBack, identifier, giveBackLoadingProgress));
            }
        }


        /// <summary>
        /// 请求一个string
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="urlKey">从WebRequest_URLs中的urlDic中取URL用，对应urlDic中的Key值</param>
        /// <param name="para">要传递的参数.</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="callBack">回调</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public IEnumerator WWWRequest(string urlKey, string para, RequestMode requestMode, GiveBackString callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            string url = GetURL(urlKey);
            url = (url == "" ? urlKey : url);
            string _para = (para == null ? "" : para);

            switch (requestMode)
            {
                case RequestMode.GET:
                    using (UnityEngine.Networking.UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Get(url + _para))
                    {
                        if (giveBackLoadingProgress != null)
                        {
                            giveBackLoadingProgress(unityWebRequest, identifier);
                        }
                        yield return unityWebRequest.Send();

                        if (unityWebRequest.isNetworkError)
                        {
                            Debug.Log(unityWebRequest.error);
                        }
                        else
                        {
                            Debug.Log("Response: " + unityWebRequest.downloadHandler.text + "  Address：" + url);
                            if (callBack != null)
                            {
                                callBack(unityWebRequest.downloadHandler.text, identifier, unityWebRequest.responseCode);
                            }
                        }
                    }
                    break;
                case RequestMode.POST:
                    using (UnityEngine.Networking.UnityWebRequest unityWebRequest = new UnityEngine.Networking.UnityWebRequest(url))
                    {
                        if (giveBackLoadingProgress != null)
                        {
                            giveBackLoadingProgress(unityWebRequest, identifier);
                        }
                        unityWebRequest.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(para));
                        unityWebRequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                        unityWebRequest.method = UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST;
                        if (headerDic.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> itm in headerDic)
                            {
                                unityWebRequest.SetRequestHeader(itm.Key, itm.Value);
                            }
                            headerDic.Clear();
                        }
                        Debug.Log("Content-Type：" + unityWebRequest.GetRequestHeader("Content-Type"));
                        yield return unityWebRequest.Send();

                        if (unityWebRequest.isNetworkError)
                        {
                            Debug.Log(unityWebRequest.error);
                        }
                        else
                        {
                            Debug.Log("Response: " + unityWebRequest.downloadHandler.text + "  Address：" + url);
                            if (fileUploaded != null)
                            {
                                fileUploaded(unityWebRequest.downloadHandler.text, identifier);
                            }
                            if (callBack != null)
                            {
                                callBack(unityWebRequest.downloadHandler.text, identifier, unityWebRequest.responseCode);
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 适用于键值对类型参数的POST请求
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="urlKey">从WebRequest_URLs中的urlDic中取URL用，对应urlDic中的Key值</param>
        /// <param name="para">要传递的参数</param>
        /// <param name="callBack">回调</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        public IEnumerator WWWRequest(string urlKey, WWWForm para, GiveBackString callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            string url = GetURL(urlKey);
            url = (url == "" ? urlKey : url);

            using (UnityEngine.Networking.UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Post(url, para))
            {
                if (giveBackLoadingProgress != null)
                {
                    giveBackLoadingProgress(unityWebRequest, identifier);
                }

                unityWebRequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                if (headerDic.Count > 0)
                {
                    foreach (KeyValuePair<string, string> itm in headerDic)
                    {
                        unityWebRequest.SetRequestHeader(itm.Key, itm.Value);
                    }
                    headerDic.Clear();
                }
                yield return unityWebRequest.Send();

                if (unityWebRequest.isNetworkError)
                {
                    Debug.Log(unityWebRequest.error);
                }
                else
                {
                    Debug.Log("Response: " + unityWebRequest.downloadHandler.text + "  Address：" + url);
                    if (fileUploaded != null)
                    {
                        fileUploaded(unityWebRequest.downloadHandler.text, identifier);
                    }
                    if (callBack != null)
                    {
                        callBack(unityWebRequest.downloadHandler.text, identifier, unityWebRequest.responseCode);
                    }
                }
            }
        }

        /// <summary>
        /// 请求一个byte[]
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="urlKey">从WebRequest_URLs中的urlDic中取URL用，对应urlDic中的Key值</param>
        /// <param name="para">要传递的参数</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        /// <param name="callBack">回调</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        IEnumerator WWWRequest(string urlKey, string para, RequestMode requestMode, GiveBackBytes callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            string url = GetURL(urlKey);
            url = (url == "" ? urlKey : url);
            string _para = (para == null ? "" : para);

            switch (requestMode)
            {
                case RequestMode.GET:
                    using (UnityEngine.Networking.UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Get(url + _para))
                    {
                        if (giveBackLoadingProgress != null)
                        {
                            giveBackLoadingProgress(unityWebRequest, identifier);
                        }
                        yield return unityWebRequest.Send();

                        if (unityWebRequest.isNetworkError)
                        {
                            Debug.Log(unityWebRequest.error);
                        }
                        else
                        {
                            Debug.Log("Request complete!");
                            if (callBack != null)
                            {
                                callBack(unityWebRequest.downloadHandler.data, identifier, unityWebRequest.responseCode);
                            }
                        }
                    }
                    break;
                case RequestMode.POST:
                    using (UnityEngine.Networking.UnityWebRequest unityWebRequest = new UnityEngine.Networking.UnityWebRequest(url))
                    {
                        if (giveBackLoadingProgress != null)
                        {
                            giveBackLoadingProgress(unityWebRequest, identifier);
                        }
                        unityWebRequest.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(para));
                        unityWebRequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                        unityWebRequest.method = UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST;
                        if (headerDic.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> itm in headerDic)
                            {
                                unityWebRequest.SetRequestHeader(itm.Key, itm.Value);
                            }
                            headerDic.Clear();
                        }
                        yield return unityWebRequest.Send();

                        if (unityWebRequest.isNetworkError)
                        {
                            Debug.Log(unityWebRequest.error);
                        }
                        else
                        {
                            Debug.Log("Request complete!");
                            if (callBack != null)
                            {
                                callBack(unityWebRequest.downloadHandler.data, identifier, unityWebRequest.responseCode);
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 适用于键值对类型参数的POST请求
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="urlKey">从WebRequest_URLs中的urlDic中取URL用，对应urlDic中的Key值</param>
        /// <param name="para">要传递的参数</param>
        /// <param name="callBack">回调</param>
        /// <param name="identifier">请求标识符，供用户分辨出这个返回数据是谁的</param>
        /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
        IEnumerator WWWRequest(string urlKey, WWWForm para, GiveBackBytes callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            string url = GetURL(urlKey);
            url = (url == "" ? urlKey : url);

            using (UnityEngine.Networking.UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Post(url, para))
            {
                if (giveBackLoadingProgress != null)
                {
                    giveBackLoadingProgress(unityWebRequest, identifier);
                }
                unityWebRequest.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(para.data);
                unityWebRequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                unityWebRequest.method = UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST;
                if (headerDic.Count > 0)
                {
                    foreach (KeyValuePair<string, string> itm in headerDic)
                    {
                        unityWebRequest.SetRequestHeader(itm.Key, itm.Value);
                    }
                    headerDic.Clear();
                }
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.isNetworkError)
                {
                    Debug.Log(unityWebRequest.error);
                }
                else
                {
                    Debug.Log("Request complete!");
                    if (callBack != null)
                    {
                        callBack(unityWebRequest.downloadHandler.data, identifier, unityWebRequest.responseCode);
                    }
                }
            }
        }
        #endregion


        #region 多请求
        public class OneGroup
        {
            public string urlKey;
            public string para;
            public RequestMode requestMode;
            public string identifier;

            public OneGroup(string _urlKey, string _para, RequestMode _requestMode, string _identifier)
            {
                urlKey = _urlKey;
                para = _para;
                requestMode = _requestMode;
                identifier = _identifier;
            }
        }

        List<OneGroup> requestsDic = new List<OneGroup>();

        /// <summary>
        /// 添加请求（POST）
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数</param>
        public void Add(string urlKey, WWWForm para, string identifier)
        {

        }

        /// <summary>
        /// 添加请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        public void Add(string urlKey, RequestMode requestMode, string identifier)
        {
            Add(urlKey, "", requestMode, identifier);
        }

        /// <summary>
        /// 添加请求
        /// </summary>
        /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
        /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
        /// <param name="requestMode">请求模式，GET/POST</param>
        public void Add(string urlKey, string para, RequestMode requestMode, string identifier)
        {
            requestsDic.Add(new OneGroup(urlKey, para, requestMode, identifier));
        }

        /// <summary>
        /// 清空所有已经Add进来的请求
        /// </summary>
        public void Clear()
        {
            requestsDic.Clear();
        }

        /// <summary>
        /// 一次性执行所有已经Add进来的请求，并在所有请求处理完毕后回调callBack
        /// </summary>
        /// <param name="callBack">Call back.</param>
        public void CreateBatch(GiveBackStringArray callBack)
        {
            if (requestsDic != null)
            {
                StartCoroutine(BatchRequest(callBack, requestsDic.ToArray()));
            }
        }

        /// <summary>
        /// 一次性执行所有已经Add进来的请求，并在所有请求处理完毕后回调callBack
        /// </summary>
        /// <param name="callBack">Call back.</param>
        public void CreateBatch(GiveBackBytesArray callBack)
        {
            if (requestsDic != null)
            {
                StartCoroutine(BatchRequest(callBack, requestsDic.ToArray()));
            }
        }

        /// <summary>
        /// 创建多个request（GiveBackStringArray）
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="callBack">回调</param>
        IEnumerator BatchRequest(GiveBackStringArray callBack, OneGroup[] requestsArray)
        {
            string[] identifiers = new string[requestsArray.Length];
            string[] tempData = new string[requestsArray.Length];

            for (int i = 0; i < requestsArray.Length; i++)
            {
                identifiers[i] = requestsArray[i].identifier;
                string url = GetURL(requestsArray[i].urlKey);
                url = (url == "" ? requestsArray[i].urlKey : url);
                string _para = (requestsArray[i].para == null ? "" : requestsArray[i].para);

                switch (requestsArray[i].requestMode)
                {
                    case RequestMode.GET:
                        using (UnityEngine.Networking.UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Get(url + _para))
                        {
                            yield return unityWebRequest.Send();

                            if (unityWebRequest.isNetworkError)
                            {
                                Debug.Log(unityWebRequest.error);
                            }
                            else
                            {
                                Debug.Log("Response: " + unityWebRequest.downloadHandler.text + "  Address：" + url);
                                tempData[i] = unityWebRequest.downloadHandler.text;
                            }
                        }
                        break;
                    case RequestMode.POST:
                        using (UnityEngine.Networking.UnityWebRequest unityWebRequest = new UnityEngine.Networking.UnityWebRequest(url))
                        {
                            unityWebRequest.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(requestsArray[i].para));
                            unityWebRequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                            unityWebRequest.method = UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST;
                            if (headerDic.Count > 0)
                            {
                                foreach (KeyValuePair<string, string> itm in headerDic)
                                {
                                    unityWebRequest.SetRequestHeader(itm.Key, itm.Value);
                                }
                                headerDic.Clear();
                            }
                            Debug.Log("Content-Type：" + unityWebRequest.GetRequestHeader("Content-Type"));
                            yield return unityWebRequest.Send();

                            if (unityWebRequest.isNetworkError)
                            {
                                Debug.Log(unityWebRequest.error);
                            }
                            else
                            {
                                Debug.Log("Response: " + unityWebRequest.downloadHandler.text + "  Address：" + url);
                                tempData[i] = unityWebRequest.downloadHandler.text;
                            }
                        }
                        break;
                }
            }
            Clear();
            if (callBack != null)
            {
                callBack(tempData, identifiers);
            }
        }

        /// <summary>
        /// 创建多个request（GiveBackBytesArray）
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="callBack">回调</param>
        IEnumerator BatchRequest(GiveBackBytesArray callBack, OneGroup[] requestsArray)
        {
            string[] identifiers = new string[requestsArray.Length];
            byte[][] tempData = new byte[requestsArray.Length][];
            long[] statusCodes = new long[requestsArray.Length];

            for (int i = 0; i < requestsArray.Length; i++)
            {
                identifiers[i] = requestsArray[i].identifier;
                string url = GetURL(requestsArray[i].urlKey);
                url = (url == "" ? requestsArray[i].urlKey : url);
                string _para = (requestsArray[i].para == null ? "" : requestsArray[i].para);

                switch (requestsArray[i].requestMode)
                {
                    case RequestMode.GET:
                        using (UnityEngine.Networking.UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Get(url + _para))
                        {
                            yield return unityWebRequest.Send();

                            if (unityWebRequest.isNetworkError)
                            {
                                Debug.Log(unityWebRequest.error);
                            }
                            else
                            {
                                tempData[i] = new byte[unityWebRequest.downloadHandler.data.Length];
                                tempData[i] = unityWebRequest.downloadHandler.data;
                                statusCodes[i] = unityWebRequest.responseCode;
                            }
                        }
                        break;
                    case RequestMode.POST:
                        using (UnityEngine.Networking.UnityWebRequest unityWebRequest = new UnityEngine.Networking.UnityWebRequest(url))
                        {
                            unityWebRequest.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(requestsArray[i].para));
                            unityWebRequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                            unityWebRequest.method = UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST;
                            if (headerDic.Count > 0)
                            {
                                foreach (KeyValuePair<string, string> itm in headerDic)
                                {
                                    unityWebRequest.SetRequestHeader(itm.Key, itm.Value);
                                }
                                headerDic.Clear();
                            }
                            Debug.Log("Content-Type：" + unityWebRequest.GetRequestHeader("Content-Type"));
                            yield return unityWebRequest.Send();

                            if (unityWebRequest.isNetworkError)
                            {
                                Debug.Log(unityWebRequest.error);
                            }
                            else
                            {
                                Debug.Log("Respon Bytes Length: " + unityWebRequest.downloadHandler.data.Length + "  Address：" + url);
                                tempData[i] = new byte[unityWebRequest.downloadHandler.data.Length];
                                tempData[i] = unityWebRequest.downloadHandler.data;
                                statusCodes[i] = unityWebRequest.responseCode;
                            }
                        }
                        break;
                }
            }
            Clear();
            if (callBack != null)
            {
                callBack(tempData, identifiers, statusCodes);
            }
        }
        #endregion
    }
}