/* by 小晕晕 */
using UnityEngine;
using System.Collections;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


public class FnWebRequestBase
{
    public enum RequestMode
    {
        GET,
        POST,
        PUT
    }

    /// <summary>
    /// 返回的数据
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// 请求是否失败
        /// </summary>
        public bool isError;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string error;
        /// <summary>
        /// 请求回的数据
        /// </summary>
        public byte[] data;

        public ResponseData()
        {
        }

        public ResponseData(byte[] data)
        {
            this.data = data;
        }

        public ResponseData(bool isError, string error)
        {
            this.isError = isError;
            this.error = error;
        }
    }

    /// <summary>
    /// 向Request线程传递数据用到的类
    /// </summary>
    public class Param_Bytes
    {
        public string urlKey;
        public byte[] para;
        public RequestMode requestMode;
        public GiveBackBytes callBack;
        public string identifier;
        public GiveBackLoadingProgress giveBackLoadingProgress;
        public string contentType;
        /// <summary>
        /// 文件的路径
        /// </summary>
        public string filePath;

        public Param_Bytes(string urlKey, byte[] para, RequestMode requestMode, GiveBackBytes callBack, string identifier, GiveBackLoadingProgress giveBackLoadingProgress, string contentType, string filePath)
        {
            this.urlKey = urlKey;
            this.para = para;
            this.requestMode = requestMode;
            this.callBack = callBack;
            this.identifier = identifier;
            this.giveBackLoadingProgress = giveBackLoadingProgress;
            this.contentType = contentType;
            this.filePath = filePath;
        }
    }

    /// <summary>
    /// 向HttpUploadFile线程传递数据用到的类
    /// </summary>
    class Param_HttpUploadFile
    {
        public string url;
        public string filePath;
        public string identifier;
        public string contentType;
        public Dictionary<string, string> infoDic;
        public GiveBackBytes callBack;
        public GiveBackLoadingProgress giveBackLoadingProgress;

        public Param_HttpUploadFile(string url, string filePath, string identifier, string contentType, Dictionary<string, string> infoDic, GiveBackBytes callBack, GiveBackLoadingProgress giveBackLoadingProgress)
        {
            this.url = url;
            this.filePath = filePath;
            this.identifier = identifier;
            this.contentType = contentType;
            this.infoDic = infoDic;
            this.callBack = callBack;
            this.giveBackLoadingProgress = giveBackLoadingProgress;
        }
    }

    /// <summary>
    /// 用于暂停identifier指定的请求，这里面存放所有请求用到的Identifier
    /// </summary>
    List<string> pauseList_Identifier = new List<string>();
    /// <summary>
    /// 用于暂停identifier指定的请求，这里面存放的数据与pauseList_Identifier一一对应，true为暂停
    /// </summary>
    List<bool> pauseList_Value = new List<bool>();

    /// <summary>
    /// 得到服务器返回的数据后返回给调用方，调用方如需要得到返回的数据必须实现此delegate，identifier供用户分辨出这个返回数据是谁的
    /// </summary>
    public delegate void GiveBackBytes(ResponseData responseData, string identifier);

    /// <summary>
    /// 返回当前请求的加载进度
    /// </summary>
    public delegate void GiveBackLoadingProgress(float progress, string identifier);


    public class GiveBackBytesItem
    {
        public GiveBackBytes giveBackBytes;
        public ResponseData responseData;
        public string identifier;

        public GiveBackBytesItem(GiveBackBytes giveBackBytes, ResponseData responseData, string identifier)
        {
            this.giveBackBytes = giveBackBytes;
            this.responseData = responseData;
            this.identifier = identifier;
        }
    }

    public class GiveBackLoadingProgressItem
    {
        public GiveBackLoadingProgress giveBackLoadingProgress;
        public float progress;
        public string identifier;

        public GiveBackLoadingProgressItem(GiveBackLoadingProgress giveBackLoadingProgress, float progress, string identifier)
        {
            this.giveBackLoadingProgress = giveBackLoadingProgress;
            this.progress = progress;
            this.identifier = identifier;
        }
    }

    public List<GiveBackBytesItem> giveBackBytes = new List<GiveBackBytesItem>();
    public List<GiveBackLoadingProgressItem> giveBackProgress = new List<GiveBackLoadingProgressItem>();

    protected bool threadSafe;

    /// <summary>
    /// 阻塞模式下请求网络资源（Get）
    /// </summary>
    /// <returns>返回服务端响应的内容</returns>
    /// <param name="url">请求的URL</param>
    public string CreateRequest(string url)
    {
        return UTF8Encoding.UTF8.GetString(CreateRequest(url, "", RequestMode.GET, "").data);
    }

    /// <summary>
    /// 用于https访问
    /// </summary>
    /// <returns><c>true</c>, if remote certificate validation callback was myed, <c>false</c> otherwise.</returns>
    /// <param name="sender">Sender.</param>
    /// <param name="certificate">Certificate.</param>
    /// <param name="chain">Chain.</param>
    /// <param name="sslPolicyErrors">Ssl policy errors.</param>
    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain,
        // look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    continue;
                }
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                bool chainIsValid = chain.Build((X509Certificate2)certificate);
                if (!chainIsValid)
                {
                    isOk = false;
                    break;
                }
            }
        }
        return isOk;
    }

    #region 网络请求

    /// <summary>
    /// 阻塞模式下请求网络资源
    /// </summary>
    /// <returns>返回服务端响应的内容</returns>
    /// <param name="url">请求的URL</param>
    /// <param name="param">GET或POST请求URL时要附带的参数。GET操作会将url和param直接相加，请注意param首字母应为?或其他符号；POST无要求</param>
    /// <param name="requestMode"></param>
    /// <param name="contentType">请求的Header的ContentType</param>
    public ResponseData CreateRequest(string url, string param, RequestMode requestMode, string contentType)
    {
        return Request(new Param_Bytes(url, UTF8Encoding.UTF8.GetBytes(param), requestMode, null, "", null, contentType, ""));
    }

    /// <summary>
    /// 阻塞模式下请求网络资源
    /// </summary>
    /// <returns>返回服务端响应的内容</returns>
    /// <param name="url">请求的URL</param>
    /// <param name="param">GET或POST请求URL时要附带的参数。</param>
    /// <param name="requestMode"></param>
    /// <param name="contentType">请求的Header的ContentType</param>
    public ResponseData CreateRequest(string url, byte[] param, string contentType)
    {
        return Request(new Param_Bytes(url, param, RequestMode.POST, null, "", null, contentType, ""));
    }

    /// <summary>
    /// 异步请求网络资源
    /// </summary>
    /// <param name="url">请求的URL</param>
    /// <param name="param">请求携带的参数</param>
    /// <param name="identifier">标识符，用来区分返回的内容是谁的</param>
    /// <param name="callback">将请求结果回调给调用方</param>
    /// <param name="giveBackProgress">将请求进度回调给调用方</param>
    /// <param name="requestMode">GET or POST</param>
    /// <param name="contentType">请求的ContentType</param>
    public void CreateRequestAsync(string url, string param, string identifier, GiveBackBytes callback, RequestMode requestMode)
    {
        Thread thread;
        if (requestMode == RequestMode.GET || (requestMode == RequestMode.POST && !string.IsNullOrEmpty(param)))
        {
            thread = new Thread(new ParameterizedThreadStart(RequestAsync));
            thread.Start(new Param_Bytes(url, UTF8Encoding.UTF8.GetBytes(param), requestMode, callback, identifier, null, "", ""));
            thread.Name = identifier;
        }
    }

    /// <summary>
    /// 异步请求网络资源
    /// </summary>
    /// <param name="url">请求的URL</param>
    /// <param name="param">请求携带的参数</param>
    /// <param name="identifier">标识符，用来区分返回的内容是谁的</param>
    /// <param name="callback">将请求结果回调给调用方</param>
    /// <param name="giveBackProgress">将请求进度回调给调用方</param>
    /// <param name="requestMode">GET or POST</param>
    /// <param name="contentType">请求的ContentType</param>
    public void CreateRequestAsync(string url, string param, string identifier, GiveBackBytes callback, GiveBackLoadingProgress giveBackProgress, RequestMode requestMode)
    {
        Thread thread;
        if (requestMode == RequestMode.GET || (requestMode == RequestMode.POST && !string.IsNullOrEmpty(param)))
        {
            thread = new Thread(new ParameterizedThreadStart(RequestAsync));
            thread.Start(new Param_Bytes(url, UTF8Encoding.UTF8.GetBytes(param), requestMode, callback, identifier, giveBackProgress, "", ""));
            thread.Name = identifier;
        }
    }

    /// <summary>
    /// 异步请求网络资源
    /// </summary>
    /// <param name="url">请求的URL</param>
    /// <param name="param">请求携带的参数</param>
    /// <param name="identifier">标识符，用来区分返回的内容是谁的</param>
    /// <param name="callback">将请求结果回调给调用方</param>
    /// <param name="giveBackProgress">将请求进度回调给调用方</param>
    /// <param name="requestMode">GET or POST</param>
    /// <param name="contentType">请求的ContentType</param>
    public void CreateRequestAsync(string url, string param, string identifier, GiveBackBytes callback, GiveBackLoadingProgress giveBackProgress, RequestMode requestMode, string contentType)
    {
        Thread thread;
        if (requestMode == RequestMode.GET || (requestMode == RequestMode.POST && !string.IsNullOrEmpty(param)))
        {
            thread = new Thread(new ParameterizedThreadStart(RequestAsync));
            thread.Start(new Param_Bytes(url, UTF8Encoding.UTF8.GetBytes(param), requestMode, callback, identifier, giveBackProgress, contentType, ""));
            thread.Name = identifier;
        }
    }

    /// <summary>
    /// 异步请求网络资源
    /// </summary>
    /// <param name="url">请求的URL</param>
    /// <param name="param">请求携带的参数</param>
    /// <param name="identifier">标识符，用来区分返回的内容是谁的</param>
    /// <param name="callback">将请求结果回调给调用方</param>
    /// <param name="giveBackProgress">将请求进度回调给调用方</param>
    /// <param name="requestMode">GET or POST</param>
    /// <param name="contentType">请求的ContentType</param>
    public void CreateRequestAsync(string url, byte[] param, string identifier, GiveBackBytes callback, GiveBackLoadingProgress giveBackProgress, RequestMode requestMode, string contentType)
    {
        Thread thread;
        if (requestMode == RequestMode.GET || (requestMode == RequestMode.POST && param != null))
        {
            thread = new Thread(new ParameterizedThreadStart(RequestAsync));
            thread.Start(new Param_Bytes(url, param, requestMode, callback, identifier, giveBackProgress, contentType, ""));
            thread.Name = identifier;
        }
    }

    public void RequestAsync(object _data)
    {
        Param_Bytes param_Bytes = (Param_Bytes)_data;
        Request(param_Bytes);
    }

    public ResponseData Request(Param_Bytes param_Bytes)
    {
        System.Net.WebRequest request = null;
        if (param_Bytes.requestMode == RequestMode.GET)
        {
            request = System.Net.WebRequest.Create(param_Bytes.urlKey + "?" + UTF8Encoding.UTF8.GetString(param_Bytes.para));
            request.Method = param_Bytes.requestMode.ToString();
        }
        else
        {
            request = System.Net.WebRequest.Create(param_Bytes.urlKey);
            if (string.IsNullOrEmpty(param_Bytes.contentType))
            {
                request.ContentType = "application/x-www-form-urlencoded";//不适用于POST传文件，只适用于POST传文本
            }
            else
            {
                request.ContentType = param_Bytes.contentType;
            }
            request.Method = param_Bytes.requestMode.ToString();

            Stream requestStream = null;
            try
            {
                requestStream = request.GetRequestStream();
            }
            catch (Exception e)
            {
                ResponseData responseData = new ResponseData();
                responseData.isError = true;
                responseData.error = e.ToString();
                // Debug.Log (responseData.error);
                if (param_Bytes.callBack != null)
                {
                    if (threadSafe)
                    {
                        giveBackBytes.Add(new GiveBackBytesItem(param_Bytes.callBack, responseData, param_Bytes.identifier));
                    }
                    else
                    {
                        param_Bytes.callBack(responseData, param_Bytes.identifier);
                    }
                }
                else
                {
                    return responseData;
                }
                return responseData;
            }
            if (param_Bytes.para != null && param_Bytes.para.Length > 0)
            {
                StreamWriter writer = new StreamWriter(requestStream);
                writer.BaseStream.Write(param_Bytes.para, 0, param_Bytes.para.Length);
                writer.Close();
            }
            requestStream.Close();
        }

        //用于https请求
        if (param_Bytes.urlKey.Contains("https:"))
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
        }

        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        //当前读取到的位置
        int curPos = 0;
        //缓冲区大小
        int bufferSize = 40960;
        if (response.ContentLength != -1)
        {
            byte[] responseByteArray = new byte[response.ContentLength];
            //未接收字节数
            long unReceived = response.ContentLength;

            //如果当前线程安全的进度列表中已经存在此条目则取出以用于设置最新进度
            GiveBackLoadingProgressItem progressItem = null;
            foreach (GiveBackLoadingProgressItem item in giveBackProgress)
            {
                if (item.identifier == param_Bytes.identifier)
                {
                    progressItem = item;
                    break;
                }
            }

            while (unReceived > 0)
            {
                curPos += dataStream.Read(responseByteArray, curPos, unReceived > bufferSize ? bufferSize : (int)unReceived);
                unReceived = response.ContentLength - curPos;
                if (param_Bytes.giveBackLoadingProgress != null)
                {
                    if (threadSafe)
                    {
                        if (progressItem != null)
                        {
                            progressItem.progress = (float)curPos / response.ContentLength;
                        }
                        else
                        {
                            progressItem = new GiveBackLoadingProgressItem(param_Bytes.giveBackLoadingProgress, ((float)curPos / response.ContentLength), param_Bytes.identifier);
                            giveBackProgress.Add(progressItem);
                        }
                    }
                    else
                    {
                        param_Bytes.giveBackLoadingProgress(((float)curPos / response.ContentLength), param_Bytes.identifier);
                    }
                }
            }
            dataStream.Close();
            //            string responseFromServer = Encoding.UTF8.GetString(responseByteArray3, onePair.Value.Length - 3);
            //            Debug.Log(responseFromServer);
            ResponseData responseData = new ResponseData(responseByteArray);
            if (param_Bytes.callBack != null)
            {
                if (threadSafe)
                {
                    giveBackBytes.Add(new GiveBackBytesItem(param_Bytes.callBack, responseData, param_Bytes.identifier));
                }
                else
                {
                    param_Bytes.callBack(responseData, param_Bytes.identifier);
                }
            }
            else
            {
                return responseData;
            }
        }
        else
        {
            List<byte> byteArray = new List<byte>();
            byte[] buffer = new byte[bufferSize];
            //读到的byte数量
            int receivedCount = 0;
            receivedCount = dataStream.Read(buffer, 0, bufferSize);
            while (receivedCount > 0)
            {
                byte[] tempArray = new byte[receivedCount];
                Array.Copy(buffer, tempArray, receivedCount);
                byteArray.AddRange(tempArray);
                receivedCount = dataStream.Read(buffer, 0, bufferSize);
                //                    if (giveBackProcess != null)
                //                    {
                //                        giveBackProcess(((float)curPos / response.ContentLength), data[2]);
                //                    }
            }
            dataStream.Close();
            ResponseData responseData = new ResponseData(byteArray.ToArray());
            if (param_Bytes.callBack != null)
            {
                if (threadSafe)
                {
                    giveBackBytes.Add(new GiveBackBytesItem(param_Bytes.callBack, responseData, param_Bytes.identifier));
                }
                else
                {
                    param_Bytes.callBack(responseData, param_Bytes.identifier);
                }
            }
            else
            {
                return responseData;
            }
        }

        return null;
    }

    #endregion

    #region 下载文件

    /// <summary>
    /// 每个Request创建前都应调用此处以添加一个Pause变量，用来随时执行暂停操作
    /// </summary>
    /// <param name="identifier">Identifier.</param>
    public void AddPauseState(string identifier)
    {
        //如果列表中已经存在identifier，说明此Request可能刚刚被暂停，现在是执行续传操作
        for (int i = 0; i < pauseList_Identifier.Count; i++)
        {
            if (pauseList_Identifier[i] == identifier)
            {
                pauseList_Value[i] = false;
                return;
            }
        }

        //如果列表中没有此identifier，则执行添加操作
        pauseList_Identifier.Add(identifier);
        pauseList_Value.Add(false);
    }

    /// <summary>
    /// 同步下载，以指定的RequestMode和参数para访问urlKey指定的网址，如果filePath不为空则下载数据到filePath的文件中否则以byte[]格式返回网页内容，且实时返回加载进度
    /// </summary>
    /// <returns><c>true</c>, if file was dwonloaded, <c>false</c> otherwise.</returns>
    /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
    /// <param name="identifier">唯一标识符，可以为任意值，但请确保此值全局唯一，会同加载的数据一起回调给用户，用户可通过此值区别数据归属，并可通过此值控制加载的状态</param>
    /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
    /// <param name="requestMode">请求模式，GET/POST</param>
    /// <param name="callBack">回调函数</param>
    /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
    /// <param name="filePath">文件的存储路径</param>
    /// <param name="useRange">是否启用断点续传</param>
    public bool DownloadFile(string urlKey, string identifier, string para, RequestMode requestMode, GiveBackBytes callBack, GiveBackLoadingProgress giveBackLoadingProgress, string filePath, bool useRange)
    {
        //        Debug.Log("urlKey：" + urlKey + "，  para：" + para);
        if (!string.IsNullOrEmpty(urlKey))
        {
            AddPauseState(identifier);

            string url = urlKey;
            url = (url == "" ? urlKey : url);
            string _para = (para == null ? "" : para);

            System.Net.HttpWebRequest request = null;
            if (requestMode == RequestMode.GET)
            {
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url + _para);
            }
            else if (requestMode == RequestMode.POST)
            {
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                request.Method = requestMode.ToString();

                request.ContentType = "application/x-www-form-urlencoded";//不适用于POST传文件，只适用于POST传文本
                Stream requestStream = request.GetRequestStream();
                StreamWriter writer = new StreamWriter(requestStream);
                writer.Write(_para);
                writer.Close();
                requestStream.Close();
            }

            //用于断点续传用，如果文件已经存在则将已经下载的长度赋予range，不存在此文件则为0，表示从开始下载
            int range = 0;
            FileStream fs = null;
            //如果用户指定了文件路径，则执行文件流操作
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    if (useRange)
                    {
                        fs = File.OpenWrite(filePath);
                        range = (int)fs.Length;
                        fs.Seek(range, SeekOrigin.Current); //移动文件流中的当前指针 
                        //断点续传用
                        if (range > 0)
                            request.AddRange(range); //设置Range值
                    }
                    else
                    {
                        File.Delete(filePath);
                    }
                }
                else
                {
                    range = 0;
                }
            }

            System.Net.HttpWebResponse response = null;
            //此处try为了检测是否为Range超出范围引发416错误
            try
            {
                //用于https请求
                if (url.Contains("https:"))
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
                }

                response = (System.Net.HttpWebResponse)request.GetResponse();
                if ((response.ContentLength > 0 && response.StatusCode == System.Net.HttpStatusCode.OK) || (response.ContentLength <= 0 && int.Parse(response.Headers.GetValues("Accept-Length")[0]) > 1024))
                {
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (fs == null)
                        {
                            fs = new FileStream(filePath, FileMode.Create);
                        }
                    }
                }
                else
                {
                    //总长度少于1k，收到的应该是错误提示
                    byte[] buffer = new byte[1024];
                    Stream dataStream_Error = response.GetResponseStream();
                    int length = dataStream_Error.Read(buffer, 0, 1024);
                    string errorInfo = Encoding.UTF8.GetString(buffer, 0, length);
                    Debug.LogError(errorInfo);
                    File.AppendAllText(@"C:\UnityDownloadError.txt", "url is: " + url + ", error is: " + errorInfo);
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    if (giveBackLoadingProgress != null)
                    {
                        if (threadSafe)
                        {
                            giveBackProgress.Add(new GiveBackLoadingProgressItem(giveBackLoadingProgress, -1.0f, identifier));
                        }
                        else
                        {
                            giveBackLoadingProgress(-1.0f, identifier);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }
            Stream dataStream = response.GetResponseStream();

            //找到此请求的identifier在pauseList_Identifier列表中对应的index，用这个循环找到这个index用于pauseList_Value是为了在下面的while循环中节省开销
            int index = -1;
            for (int i = 0; i < pauseList_Identifier.Count; i++)
            {
                if (pauseList_Identifier[i] == identifier)
                {
                    index = i;
                    break;
                }
            }

            //当前读取到的位置
            int curPos = 0;
            //缓冲区大小
            int bufferSize = 40960;
            if (response.ContentLength != -1)
            {
                //用于将加载的所有byte返回给用户
                byte[] responseByteArray = new byte[response.ContentLength];
                //未接收字节数
                long unReceived = response.ContentLength;
                //每次Read收到的字节数
                int receivedThisTime = 0;

                //如果用户指定了文件路径，则执行写入文件操作
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (index != -1)
                    {
                        byte[] buffer = new byte[bufferSize];

                        //如果当前线程安全的进度列表中已经存在此条目则取出以用于设置最新进度
                        GiveBackLoadingProgressItem progressItem = null;
                        foreach (GiveBackLoadingProgressItem item in giveBackProgress)
                        {
                            if (item.identifier == identifier)
                            {
                                progressItem = item;
                                break;
                            }
                        }

                        while (unReceived > 0 && !pauseList_Value[index])
                        {//如果未获取数据大于0且未被暂停则继续执行下载操作
                            receivedThisTime = dataStream.Read(buffer, 0, unReceived > bufferSize ? bufferSize : (int)unReceived);
                            curPos += receivedThisTime;

                            fs.Write(buffer, 0, receivedThisTime);

                            unReceived = response.ContentLength - curPos;
                            if (giveBackLoadingProgress != null)
                            {
                                if (threadSafe)
                                {
                                    if (progressItem != null)
                                    {
                                        progressItem.progress = ((float)(range + curPos) / (range + response.ContentLength));
                                    }
                                    else
                                    {
                                        progressItem = new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + response.ContentLength)), identifier);
                                        giveBackProgress.Add(progressItem);
                                    }
                                }
                                else
                                {
                                    giveBackLoadingProgress(((float)(range + curPos) / (range + response.ContentLength)), identifier);
                                }
                            }
                        }
                    }
                    fs.Close();
                }
                else
                {//否则返回所有byte数据给用户
                    while (unReceived > 0 && !pauseList_Value[index])
                    {
                        receivedThisTime = dataStream.Read(responseByteArray, curPos, unReceived > bufferSize ? bufferSize : (int)unReceived);
                        curPos += receivedThisTime;
                        unReceived = response.ContentLength - curPos;
                        if (giveBackLoadingProgress != null)
                        {
                            if (threadSafe)
                            {
                                giveBackProgress.Add(new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + response.ContentLength)), identifier));
                            }
                            else
                            {
                                giveBackLoadingProgress(((float)(range + curPos) / (range + response.ContentLength)), identifier);
                            }
                        }
                    }

                    //if (mainThreadSafe)
                    //{
                    //    wwwData_BackBytes.Add(new BackBytes(responseByteArray, identifier, callBack));
                    //}
                    //else
                    //{
                    if (!pauseList_Value[index])
                    {//如果被暂停了，则说明收到的数据不完整，不做输出和返回处理
                        // Debug.Log ("收到byte[]长度：" + responseByteArray.Length);
                        if (callBack != null)
                        {
                            if (threadSafe)
                            {
                                giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(responseByteArray), identifier));
                            }
                            else
                            {
                                callBack(new ResponseData(responseByteArray), identifier);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                    //}
                }
                dataStream.Close();
                return true;
            }
            else
            {
                int acceptLength = int.Parse(response.Headers.GetValues("Accept-Length")[0]);
                //如果用户指定了文件路径，则执行写入文件操作
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (index != -1)
                    {
                        // List<byte> byteArray = new List<byte> ();
                        byte[] buffer = new byte[bufferSize];
                        //读到的byte数量
                        int receivedCount = 0;
                        receivedCount = dataStream.Read(buffer, 0, bufferSize);
                        while (receivedCount > 0)
                        {
                            curPos += receivedCount;
                            fs.Write(buffer, 0, receivedCount);
                            if (giveBackLoadingProgress != null)
                            {
                                if (threadSafe)
                                {
                                    giveBackProgress.Add(new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + acceptLength)), identifier));
                                }
                                else
                                {
                                    giveBackLoadingProgress(((float)(range + curPos) / (range + acceptLength)), identifier);
                                }
                            }
                            receivedCount = dataStream.Read(buffer, 0, bufferSize);
                        }
                    }
                    fs.Close();
                }
                else
                {//否则返回所有byte数据给用户
                    List<byte> byteArray = new List<byte>();
                    byte[] buffer = new byte[bufferSize];
                    //读到的byte数量
                    int receivedCount = 0;
                    receivedCount = dataStream.Read(buffer, 0, bufferSize);
                    while (receivedCount > 0 && !pauseList_Value[index])
                    {
                        curPos += receivedCount;
                        byte[] tempArray = new byte[receivedCount];
                        Array.Copy(buffer, tempArray, receivedCount);
                        byteArray.AddRange(tempArray);
                        if (giveBackLoadingProgress != null)
                        {
                            if (threadSafe)
                            {
                                giveBackProgress.Add(new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + acceptLength)), identifier));
                            }
                            else
                            {
                                giveBackLoadingProgress(((float)(range + curPos) / (range + acceptLength)), identifier);
                            }
                        }
                        receivedCount = dataStream.Read(buffer, 0, bufferSize);
                    }
                    if (!pauseList_Value[index])
                    {//如果被暂停了，则说明收到的数据不完整，不做输出和返回处理
                        // Debug.Log ("收到byte[]长度：" + byteArray.Count);
                        if (callBack != null)
                        {
                            if (threadSafe)
                            {
                                giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(byteArray.ToArray()), identifier));
                            }
                            else
                            {
                                callBack(new ResponseData(byteArray.ToArray()), identifier);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                dataStream.Close();
                return true;
            }

        }
        return false;
    }

    /// <summary>
    /// 异步下载，以指定的RequestMode和参数para访问urlKey指定的网址，如果filePath不为空则下载数据到filePath的文件中否则以byte[]格式返回网页内容，且实时返回加载进度
    /// </summary>
    /// <param name="urlKey">此处可以直接填写url，也可以填写WebRequest_URLs中urlDic中的Key值</param>
    /// <param name="identifier">唯一标识符，可以为任意值，但请确保此值全局唯一，会同加载的数据一起回调给用户，用户可通过此值区别数据归属，并可通过此值控制加载的状态</param>
    /// <param name="para">请求带的参数，可以为空，如果是GET模式，此处的处理是参数直接追加到URL后面，建议GET模式下直接将参数写在URL中</param>
    /// <param name="requestMode">请求模式，GET/POST</param>
    /// <param name="callBack">回调函数</param>
    /// <param name="giveBackLoadingProgress">返回当前请求的加载进度</param>
    /// <param name="contentType">请求的Header中的ContentType</param>
    /// <param name="filePath">文件的存储路径（自动断点续传）</param>
    public void DownloadFileAsync(string urlKey, string identifier, string para, RequestMode requestMode, GiveBackBytes callBack, GiveBackLoadingProgress giveBackLoadingProgress, string contentType, string filePath)
    {
        //Debug.Log("urlKey：" + urlKey + "，  para：" + para);
        if (!string.IsNullOrEmpty(urlKey))
        {
            AddPauseState(identifier);

            Thread thread;
            if (requestMode == RequestMode.GET || (requestMode == RequestMode.POST && !string.IsNullOrEmpty(para)))
            {
                thread = new Thread(new ParameterizedThreadStart(WWWRequestBytes));
                Param_Bytes param_Bytes = new Param_Bytes(urlKey, UTF8Encoding.UTF8.GetBytes(para), requestMode, callBack, identifier, giveBackLoadingProgress, contentType, filePath);
                thread.Start(param_Bytes);
                thread.Name = identifier;
            }
        }
    }

    /// <summary>
    /// 以byte[]格式返回整个网页
    /// </summary>
    /// <param name="param">请求用到的参数</param>
    void WWWRequestBytes(object param)
    {
        Param_Bytes param_Bytes = (Param_Bytes)param;
        string url = param_Bytes.urlKey;
        url = (url == "" ? param_Bytes.urlKey : url);
        byte[] _para = param_Bytes.para;

        string identifier = param_Bytes.identifier;
        RequestMode requestMode = param_Bytes.requestMode;
        GiveBackBytes callBack = param_Bytes.callBack;
        GiveBackLoadingProgress giveBackLoadingProgress = param_Bytes.giveBackLoadingProgress;
        string filePath = param_Bytes.filePath;

        System.Net.HttpWebRequest request = null;
        if (requestMode == RequestMode.GET)
        {
            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url + (_para != null ? UTF8Encoding.UTF8.GetString(_para) : ""));
        }
        else if (requestMode == RequestMode.POST)
        {
            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            request.Method = requestMode.ToString();

            request.ContentType = "application/x-www-form-urlencoded";//不适用于POST传文件，只适用于POST传文本
            Stream requestStream = request.GetRequestStream();
            StreamWriter writer = new StreamWriter(requestStream);
            if (_para != null)
            {
                writer.BaseStream.Write(_para, 0, _para.Length);
            }
            writer.Close();
            requestStream.Close();
        }

        int range = 0;
        FileStream fs = null;
        //如果用户指定了文件路径，则执行文件流操作
        if (!string.IsNullOrEmpty(param_Bytes.filePath))
        {
            if (File.Exists(param_Bytes.filePath))
            {
                fs = File.OpenWrite(param_Bytes.filePath);
                range = (int)fs.Length;
                fs.Seek(range, SeekOrigin.Current); //移动文件流中的当前指针 
                //断点续传用
                if (range > 0)
                    request.AddRange(range); //设置Range值
            }
            else
            {
                range = 0;
            }
        }

        System.Net.HttpWebResponse response = null;
        //此处try为了检测是否为Range超出范围引发416错误
        try
        {
            //用于https请求
            if (url.Contains("https:"))
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
            }

            response = (System.Net.HttpWebResponse)request.GetResponse();
            if (response.ContentLength > 200 || (response.ContentLength <= 0 && int.Parse(response.Headers.GetValues("Accept-Length")[0]) > 200))
            {
                if (!string.IsNullOrEmpty(param_Bytes.filePath))
                {
                    if (fs == null)
                    {
                        fs = new FileStream(param_Bytes.filePath, FileMode.Create);
                    }
                }
            }
            else
            {
                //总长度少于200，收到的应该是错误提示，这里就认为没有长度小于200的fn
                byte[] buffer = new byte[1024];
                Stream dataStream_Error = response.GetResponseStream();
                int length = dataStream_Error.Read(buffer, 0, 1024);
                Debug.LogError(Encoding.UTF8.GetString(buffer, 0, length));
                if (fs != null)
                {
                    fs.Close();
                }
                if (giveBackLoadingProgress != null)
                {
                    if (threadSafe)
                    {
                        giveBackProgress.Add(new GiveBackLoadingProgressItem(giveBackLoadingProgress, -1.0f, identifier));
                    }
                    else
                    {
                        giveBackLoadingProgress(-1.0f, identifier);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
            if (fs != null)
            {
                fs.Close();
            }
            if (giveBackLoadingProgress != null)
            {
                if (threadSafe)
                {
                    giveBackProgress.Add(new GiveBackLoadingProgressItem(giveBackLoadingProgress, -1.0f, identifier));
                }
                else
                {
                    giveBackLoadingProgress(-1.0f, identifier);
                }
            }
            return;
        }
        Stream dataStream = response.GetResponseStream();
        //找到此请求的identifier在pauseList_Identifier列表中对应的index，用这个循环找到这个index用于pauseList_Value是为了在下面的while循环中节省开销
        int index = -1;
        for (int i = 0; i < pauseList_Identifier.Count; i++)
        {
            if (pauseList_Identifier[i] == identifier)
            {
                index = i;
                break;
            }
        }

        //当前读取到的位置
        int curPos = 0;
        //缓冲区大小
        int bufferSize = 40960;
        if (response.ContentLength != -1)
        {
            //用于将加载的所有byte返回给用户
            byte[] responseByteArray = new byte[response.ContentLength];
            //未接收字节数
            long unReceived = response.ContentLength;
            //每次Read收到的字节数
            int receivedThisTime = 0;

            //如果用户指定了文件路径，则执行写入文件操作
            if (!string.IsNullOrEmpty(filePath))
            {
                if (index != -1)
                {
                    byte[] buffer = new byte[bufferSize];

                    //如果当前线程安全的进度列表中已经存在此条目则取出以用于设置最新进度
                    GiveBackLoadingProgressItem progressItem = null;
                    foreach (GiveBackLoadingProgressItem item in giveBackProgress)
                    {
                        if (item.identifier == identifier)
                        {
                            progressItem = item;
                            break;
                        }
                    }

                    while (unReceived > 0 && !pauseList_Value[index])
                    {//如果未获取数据大于0且未被暂停则继续执行下载操作
                        receivedThisTime = dataStream.Read(buffer, 0, unReceived > bufferSize ? bufferSize : (int)unReceived);
                        curPos += receivedThisTime;

                        fs.Write(buffer, 0, receivedThisTime);

                        unReceived = response.ContentLength - curPos;
                        if (giveBackLoadingProgress != null)
                        {
                            if (threadSafe)
                            {
                                if (progressItem != null)
                                {
                                    progressItem.progress = (float)(range + curPos) / (range + response.ContentLength);
                                }
                                else
                                {
                                    progressItem = new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + response.ContentLength)), identifier);
                                    giveBackProgress.Add(progressItem);
                                }
                            }
                            else
                            {
                                giveBackLoadingProgress(((float)(range + curPos) / (range + response.ContentLength)), identifier);
                            }
                        }
                    }
                }
                fs.Close();
            }
            else
            {//否则返回所有byte数据给用户
                //如果当前线程安全的进度列表中已经存在此条目则取出以用于设置最新进度
                GiveBackLoadingProgressItem progressItem = null;
                foreach (GiveBackLoadingProgressItem item in giveBackProgress)
                {
                    if (item.identifier == identifier)
                    {
                        progressItem = item;
                        break;
                    }
                }

                while (unReceived > 0 && !pauseList_Value[index])
                {
                    receivedThisTime = dataStream.Read(responseByteArray, curPos, unReceived > bufferSize ? bufferSize : (int)unReceived);
                    curPos += receivedThisTime;
                    unReceived = response.ContentLength - curPos;
                    if (giveBackLoadingProgress != null)
                    {
                        if (threadSafe)
                        {
                            if (progressItem != null)
                            {
                                progressItem.progress = (float)(range + curPos) / (range + response.ContentLength);
                            }
                            else
                            {
                                progressItem = new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + response.ContentLength)), identifier);
                                giveBackProgress.Add(progressItem);
                            }
                        }
                        else
                        {
                            giveBackLoadingProgress(((float)(range + curPos) / (range + response.ContentLength)), identifier);
                        }
                    }
                }

                //if (mainThreadSafe)
                //{
                //    wwwData_BackBytes.Add(new BackBytes(responseByteArray, identifier, callBack));
                //}
                //else
                //{
                if (!pauseList_Value[index])
                {//如果被暂停了，则说明收到的数据不完整，不做输出和返回处理
                    // Debug.Log ("收到byte[]长度：" + responseByteArray.Length);
                    if (callBack != null)
                    {
                        if (threadSafe)
                        {
                            giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(responseByteArray), identifier));
                        }
                        else
                        {
                            callBack(new ResponseData(responseByteArray), identifier);
                        }
                    }
                }
                //}
            }
            dataStream.Close();
        }
        else
        {
            int acceptLength = int.Parse(response.Headers.GetValues("Accept-Length")[0]);
            //如果用户指定了文件路径，则执行写入文件操作
            if (!string.IsNullOrEmpty(filePath))
            {
                if (index != -1)
                {
                    byte[] buffer = new byte[bufferSize];
                    //读到的byte数量
                    int receivedCount = 0;
                    receivedCount = dataStream.Read(buffer, 0, bufferSize);

                    //如果当前线程安全的进度列表中已经存在此条目则取出以用于设置最新进度
                    GiveBackLoadingProgressItem progressItem = null;
                    foreach (GiveBackLoadingProgressItem item in giveBackProgress)
                    {
                        if (item.identifier == identifier)
                        {
                            progressItem = item;
                            break;
                        }
                    }

                    while (receivedCount > 0)
                    {
                        curPos += receivedCount;
                        fs.Write(buffer, 0, receivedCount);
                        if (giveBackLoadingProgress != null)
                        {
                            if (threadSafe)
                            {
                                if (progressItem != null)
                                {
                                    progressItem.progress = (float)(range + curPos) / (range + acceptLength);
                                }
                                else
                                {
                                    progressItem = new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + acceptLength)), identifier);
                                    giveBackProgress.Add(progressItem);
                                }
                            }
                            else
                            {
                                giveBackLoadingProgress(((float)(range + curPos) / (range + acceptLength)), identifier);
                            }
                        }
                        receivedCount = dataStream.Read(buffer, 0, bufferSize);
                    }
                }
                fs.Close();
            }
            else
            {//否则返回所有byte数据给用户
                List<byte> byteArray = new List<byte>();
                byte[] buffer = new byte[bufferSize];
                //读到的byte数量
                int receivedCount = 0;
                receivedCount = dataStream.Read(buffer, 0, bufferSize);

                //如果当前线程安全的进度列表中已经存在此条目则取出以用于设置最新进度
                GiveBackLoadingProgressItem progressItem = null;
                foreach (GiveBackLoadingProgressItem item in giveBackProgress)
                {
                    if (item.identifier == identifier)
                    {
                        progressItem = item;
                        break;
                    }
                }

                while (receivedCount > 0 && !pauseList_Value[index])
                {
                    curPos += receivedCount;
                    byte[] tempArray = new byte[receivedCount];
                    Array.Copy(buffer, tempArray, receivedCount);
                    byteArray.AddRange(tempArray);
                    if (giveBackLoadingProgress != null)
                    {
                        if (threadSafe)
                        {
                            if (progressItem != null)
                            {
                                progressItem.progress = (float)(range + curPos) / (range + acceptLength);
                            }
                            else
                            {
                                progressItem = new GiveBackLoadingProgressItem(giveBackLoadingProgress, ((float)(range + curPos) / (range + acceptLength)), identifier);
                                giveBackProgress.Add(progressItem);
                            }
                        }
                        else
                        {
                            giveBackLoadingProgress(((float)(range + curPos) / (range + acceptLength)), identifier);
                        }
                    }
                    receivedCount = dataStream.Read(buffer, 0, bufferSize);
                }
                if (!pauseList_Value[index])
                {//如果被暂停了，则说明收到的数据不完整，不做输出和返回处理
                    // Debug.Log ("收到byte[]长度：" + byteArray.Count);
                    if (callBack != null)
                    {
                        if (threadSafe)
                        {
                            giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(byteArray.ToArray()), identifier));
                        }
                        else
                        {
                            callBack(new ResponseData(byteArray.ToArray()), identifier);
                        }
                    }
                }
            }
            dataStream.Close();
        }
        //        //用于将加载的所有byte返回给用户
        //        byte[] responseByteArray = new byte[response.ContentLength];
        //        //当前读取到的位置
        //        int curPos = 0;
        //        //缓冲区大小
        //        int bufferSize = 40960;
        //        //未接收字节数
        //        long unReceived = response.ContentLength;
        //        //每次Read收到的字节数
        //        int receivedThisTime = 0;
        //
        //        //找到此请求的identifier在pauseList_Identifier列表中对应的index，用这个循环找到这个index用于pauseList_Value是为了在下面的while循环中节省开销
        //        int index = -1;
        //        for (int i = 0; i < pauseList_Identifier.Count; i++)
        //        {
        //            if (pauseList_Identifier[i] == param_Bytes.identifier)
        //            {
        //                index = i;
        //                break;
        //            }
        //        }
        //
        //        //如果用户指定了文件路径，则执行写入文件操作
        //        if (!string.IsNullOrEmpty(param_Bytes.filePath))
        //        {
        //            if (index != -1)
        //            {
        //                byte[] buffer = new byte[bufferSize];
        //                while (unReceived > 0 && !pauseList_Value[index])//如果未获取数据大于0且未被暂停则继续执行下载操作
        //                {
        //                    receivedThisTime = dataStream.Read(buffer, 0, unReceived > bufferSize ? bufferSize : (int)unReceived);
        //                    curPos += receivedThisTime;
        //
        //                    fs.Write(buffer, 0, receivedThisTime);
        //
        //                    unReceived = response.ContentLength - curPos;
        //                    if (giveBackLoadingProgress != null)
        //                    {
        //                        giveBackLoadingProgress(((float)(range + curPos) / (range + response.ContentLength)), identifier);
        //                    }
        //                }
        //            }
        //            fs.Close();
        //        }
        //        else//否则返回所有byte数据给用户
        //        {
        //            while (unReceived > 0 && !pauseList_Value[index])
        //            {
        //                receivedThisTime = dataStream.Read(responseByteArray, curPos, unReceived > bufferSize ? bufferSize : (int)unReceived);
        //                curPos += receivedThisTime;
        //                unReceived = response.ContentLength - curPos;
        //                if (giveBackLoadingProgress != null)
        //                {
        //                    giveBackLoadingProgress(((float)(range + curPos) / (range + response.ContentLength)), identifier);
        //                }
        //            }
        //
        //            //if (mainThreadSafe)
        //            //{
        //            //    wwwData_BackBytes.Add(new BackBytes(responseByteArray, identifier, callBack));
        //            //}
        //            //else
        //            //{
        //            if (!pauseList_Value[index])//如果被暂停了，则说明收到的数据不完整，不做输出和返回处理
        //            {
        //                Debug.Log("收到byte[]长度：" + responseByteArray.Length);
        //                if (callBack != null)
        //                {
        //                    callBack(responseByteArray, identifier);
        //                }
        //            }
        //            //}
        //        }
        //        dataStream.Close();
    }

    #endregion

    /// <summary>
    /// 上传文件到服务器
    /// </summary>
    /// <param name="url">上传文件的url</param>
    /// <param name="fileName">要上传的文件的名字</param>
    /// <param name="filePath">要上传的文件的路径</param>
    /// <param name="identifier">文件唯一标识符，此处为文件名字</param>
    /// <param name="contentType">要上传的文件类型，通常为"multipart/form-data"</param>
    /// <param name="infoDic">要上传的参数们</param>
    /// <param name="giveBackLoadingProgress">得到当前文件的上传进度</param>
    /// <returns></returns>
    public bool HttpUploadFile(string url, string fileName, string filePath, string identifier, string contentType, Dictionary<string, string> infoDic, GiveBackBytes callBack, GiveBackLoadingProgress giveBackLoadingProgress)
    {
        // Debug.Log (string.Format ("Uploading {0} to {1}", filePath, url));
        string boundary = "---------------------------" + System.DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        Stream rs = wr.GetRequestStream();

        if (infoDic != null && infoDic.Count > 0)
        {
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in infoDic.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, infoDic[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
        }

        rs.Write(boundarybytes, 0, boundarybytes.Length);
        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string header = string.Format(headerTemplate, "file", fileName, contentType);
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        rs.Write(headerbytes, 0, headerbytes.Length);

        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        long fileLength = fileStream.Length;
        byte[] buffer = new byte[40960];
        int bytesRead = 0;
        long curLength = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            curLength += bytesRead;
            rs.Write(buffer, 0, bytesRead);
            if (giveBackLoadingProgress != null)
            {
                giveBackLoadingProgress(((float)curLength / fileLength), identifier);
            }
        }
        fileStream.Close();

        byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);
        rs.Close();

        System.Net.WebResponse wresp = null;
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            string response = reader2.ReadToEnd();
            if (response.Contains("Failed") || response.Contains("exceeds the limit of"))
            {
                Debug.LogError(string.Format(response));
                if (callBack != null)
                {
                    callBack(new ResponseData(true, response), identifier);
                }
            }
            else
            {
                Debug.Log(string.Format("File uploaded, Server response is: {0}", response));
                if (callBack != null)
                {
                    callBack(new ResponseData(UTF8Encoding.UTF8.GetBytes(response)), identifier);
                }
                return true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error uploading file：" + ex.ToString());
            if (callBack != null)
            {
                callBack(new ResponseData(true, ex.ToString()), identifier);
            }
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
            }
        }
        finally
        {
            wr = null;
        }
        return false;
    }

    /// <summary>
    /// 上传文件到服务器
    /// </summary>
    /// <param name="url">上传文件的url</param>
    /// <param name="fileName">要上传的文件的名字</param>
    /// <param name="fileData">要上传的文件的原始数据</param>
    /// <param name="identifier">文件唯一标识符，此处为文件名字</param>
    /// <param name="contentType">要上传的文件类型，通常为"multipart/form-data"</param>
    /// <param name="infoDic">要上传的参数们</param>
    /// <param name="giveBackLoadingProgress">得到当前文件的上传进度</param>
    /// <returns></returns>
    public bool HttpUploadFile(string url, string fileName, byte[] fileData, string identifier, string contentType, Dictionary<string, string> infoDic, GiveBackBytes callBack, GiveBackLoadingProgress giveBackLoadingProgress)
    {
        // Debug.Log (string.Format ("Uploading {0} to {1}", filePath, url));
        string boundary = "---------------------------" + System.DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        Stream rs = wr.GetRequestStream();

        if (infoDic != null && infoDic.Count > 0)
        {
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in infoDic.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, infoDic[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
        }

        rs.Write(boundarybytes, 0, boundarybytes.Length);
        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string header = string.Format(headerTemplate, "file", fileName, contentType);
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        rs.Write(headerbytes, 0, headerbytes.Length);

        MemoryStream memoryStream = new MemoryStream(fileData);
        long fileLength = memoryStream.Length;
        byte[] buffer = new byte[40960];
        int bytesRead = 0;
        long curLength = 0;
        while ((bytesRead = memoryStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            curLength += bytesRead;
            rs.Write(buffer, 0, bytesRead);
            if (giveBackLoadingProgress != null)
            {
                giveBackLoadingProgress(((float)curLength / fileLength), identifier);
            }
        }
        memoryStream.Close();

        byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);
        rs.Close();

        System.Net.WebResponse wresp = null;
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            string response = reader2.ReadToEnd();
            if (response.Contains("Failed") || response.Contains("exceeds the limit of"))
            {
                Debug.LogError(string.Format(response));
                if (callBack != null)
                {
                    callBack(new ResponseData(true, response), identifier);
                }
            }
            else
            {
                Debug.Log(string.Format("File uploaded, Server response is: {0}", response));
                if (callBack != null)
                {
                    callBack(new ResponseData(UTF8Encoding.UTF8.GetBytes(response)), identifier);
                }
                return true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error uploading file：" + ex.ToString());
            if (callBack != null)
            {
                callBack(new ResponseData(true, ex.ToString()), identifier);
            }
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
            }
        }
        finally
        {
            wr = null;
        }
        return false;
    }


    /// <summary>
    /// 异步上传文件到服务器
    /// </summary>
    /// <param name="url">上传文件的url</param>
    /// <param name="filePath">要上传的文件的路径</param>
    /// <param name="identifier">文件唯一标识符，此处为文件名字</param>
    /// <param name="contentType">要上传的文件类型，通常为"multipart/form-data"</param>
    /// <param name="infoDic">要上传的参数们</param>
    /// <param name="giveBackLoadingProgress">得到当前文件的上传进度</param>
    public void HttpUploadFileAsync(string url, string filePath, string identifier, string contentType, Dictionary<string, string> infoDic, GiveBackBytes callBack, GiveBackLoadingProgress giveBackLoadingProgress)
    {
        Thread thread;
        if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(filePath))
        {
            thread = new Thread(new ParameterizedThreadStart(Thread_HttpUploadFile));
            thread.Start(new Param_HttpUploadFile(url, filePath, identifier, contentType, infoDic, callBack, giveBackLoadingProgress));
            thread.Name = identifier;
        }
    }

    void Thread_HttpUploadFile(object param)
    {
        Param_HttpUploadFile _param = (Param_HttpUploadFile)param;
        string url = _param.url;//上传文件的url
        string filePath = _param.filePath;//要上传的文件的路径
        string identifier = _param.identifier;//文件唯一标识符
        string contentType = _param.contentType;//要上传的文件类型，通常为"multipart/form-data"
        Dictionary<string, string> infoDic = _param.infoDic;//要上传的参数们
        GiveBackBytes callBack = _param.callBack;//上传成功后的回调
        // GiveBackLoadingProgress giveBackLoadingProgress = _param.giveBackLoadingProgress;//回传上传进度
        long fileLength = new FileInfo(filePath).Length;//得到要上传的文件的大小

        // Debug.Log (string.Format ("Uploading {0} to {1}", filePath, url));
        string boundary = "---------------------------" + System.DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        Stream rs = wr.GetRequestStream();

        if (infoDic != null && infoDic.Count > 0)
        {
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (infoDic != null)
            {
                foreach (string key in infoDic.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, infoDic[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
        }

        rs.Write(boundarybytes, 0, boundarybytes.Length);
        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string header = string.Format(headerTemplate, "file", filePath, contentType);
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        rs.Write(headerbytes, 0, headerbytes.Length);

        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[40960];
        int bytesRead = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            rs.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();

        byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);
        rs.Close();

        System.Net.WebResponse wresp = null;
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            string response = reader2.ReadToEnd();
            if (response.Contains("Failed") || response.Contains("exceeds the limit of"))
            {
                Debug.LogError(string.Format(response));
                if (callBack != null)
                {
                    if (threadSafe)
                    {
                        giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(true, response), identifier));
                    }
                    else
                    {
                        callBack(new ResponseData(true, response), identifier);
                    }

                }
            }
            else
            {
                if (callBack != null)
                {
                    if (threadSafe)
                    {
                        giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(UTF8Encoding.UTF8.GetBytes(response)), identifier));
                    }
                    else
                    {
                        callBack(new ResponseData(UTF8Encoding.UTF8.GetBytes(response)), identifier);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            if (callBack != null)
            {
                if (threadSafe)
                {
                    giveBackBytes.Add(new GiveBackBytesItem(callBack, new ResponseData(true, ex.ToString()), identifier));
                }
                else
                {
                    callBack(new ResponseData(true, ex.ToString()), identifier);
                }
            }
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
            }
        }
        finally
        {
            wr = null;
        }
        //        return false;
    }
}