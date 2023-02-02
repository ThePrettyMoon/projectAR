using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ARWorldEditor
{
    public class FileDownloadThread
    {
        #region params
        private const int oneReadLen = 16 * 1024;       // 一次读取长度 16384 = 16*kb
        private const int Md5ReadLen = 16 * 1024;       // 一次读取长度 16384 = 16*kb
        private const int ReadWriteTimeOut = 10 * 1000;  // 超时等待时间
        private const int WaitTimeOut = 10 * 1000;       // 超时等待时间
        DownloadTask downloadTask;

        private long mCurSize = 0;
        private long mTotalSize = 0;
        private FileDownloadState mState = FileDownloadState.None;
        private int mTryCount = 0; //尝试次数
        private string mErrorMsg = "";
        private int mErrorCode;

        public FileDownloadState State
        {
            get
            {
                return mState;
            }
            set
            {
                mState = value;
            }
        }

        public DownloadTask GetDownloadTask()
        {
            return downloadTask;
        }

        public string ErrorMsg
        {
            get
            {
                return mErrorMsg;
            }
            set
            {
                mErrorMsg = value;
            }
        }

        public int ErrorCode
        {
            get
            {
                return mErrorCode;
            }
            set
            {
                mErrorCode = value;
            }
        }

        public long CurSize
        {
            get
            {
                return mCurSize;
            }
            set
            {
                mCurSize = value;
            }
        }

        public long TotalSize
        {
            get
            {
                return mTotalSize;
            }
            set
            {
                mTotalSize = value;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        public FileDownloadThread(DownloadTask info)
        {
            downloadTask = info;
        }

        /// <summary>
        /// 防止失败频繁回调，只在特定次数回调
        /// </summary>
        public bool IsNeedErrorCall()
        {
            if (mTryCount == 3
                || mTryCount == 10
                || mTryCount == 100)
                return true;

            return false;
        }

        /// <summary>
        /// run download thread
        /// </summary>
        public void Run()
        {
            mTryCount++;
            if (mTryCount > 1)
            {
                Debug.Log(" Retry Count " + (mTryCount - 1));
            }

            mState = FileDownloadState.ResetSize;
            if (!ResetSize()) return;

            mState = FileDownloadState.Downloading;
            if (!Download()) return;

            mState = FileDownloadState.CheckMD5;
            if (!CheckMd5()) //校验失败，重下一次
            {
                mState = FileDownloadState.Downloading;
                if (!Download()) return;

                mState = FileDownloadState.CheckMD5;
                if (!CheckMd5()) return; //两次都失败，文件有问题
            }
            mState = FileDownloadState.Complete;
        }

        /// <summary>
        /// 重新设置长度
        /// </summary>
        /// <returns></returns>
        private bool ResetSize()
        {
            if (downloadTask.Size <= 0)
            {
                downloadTask.Size = GetWebFileSize(downloadTask.DownloadUrl);
                if (downloadTask.Size == 0) return false;
            }

            mCurSize = 0;
            mTotalSize = downloadTask.Size;

            return true;
        }

        /// <summary>
        /// 检查md5值
        /// </summary>
        /// <returns></returns>
        private bool CheckMd5()
        {
            if (string.IsNullOrEmpty(downloadTask.MD5)) return true; //不做校验，默认成功

            string md5 = GetMD5HashFromFile(downloadTask.SavePath);

            if (md5 != downloadTask.MD5)
            {
                File.Delete(downloadTask.SavePath);
                Debug.Log( " 文件MD5校验出错：" + downloadTask.FileName);
                mState = FileDownloadState.Error;
                mErrorMsg = "Network MD5 Error ";
                mErrorCode = (int)DownloadErrorCode.NETWORK_MD5_ERROR;
                return false;
            }

            return true;
        }

        /// <summary>
        ///  下载逻辑
        /// </summary>
        /// <returns></returns>
        public bool Download()
        {
            //打开上次下载的文件
            long startPos = 0;
            string tempFile = downloadTask.TempPath + ".temp";
            FileStream fileStream = null;
            if (File.Exists(downloadTask.SavePath))
            {
                //文件已存在，跳过
                Debug.Log(" File Exists " + downloadTask.SavePath);
                mCurSize = downloadTask.Size;
                return true;
            }
            else if (File.Exists(tempFile))
            {
                Debug.Log(" Temp File Exists " + tempFile);
                fileStream = File.OpenWrite(tempFile);
                startPos = fileStream.Length;
                fileStream.Seek(startPos, SeekOrigin.Current); //移动文件流中的当前指针

                //文件已经下载完，没改名字，结束
                if (startPos == downloadTask.Size)
                {
                    fileStream.Flush();
                    fileStream.Close();
                    fileStream = null;
                    if (File.Exists(downloadTask.SavePath)) File.Delete(downloadTask.SavePath);
                    File.Move(tempFile, downloadTask.SavePath);

                    mCurSize = startPos;
                    return true;
                }
            }
            else
            {
                Debug.Log(" Download File " + tempFile);
                string direName = Path.GetDirectoryName(tempFile);
                if (!Directory.Exists(direName)) Directory.CreateDirectory(direName);
                fileStream = new FileStream(tempFile, FileMode.Create);
            }

            // 下载逻辑
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responStream = null;
            try
            {
                request = HttpWebRequest.Create(downloadTask.DownloadUrl) as HttpWebRequest;
                request.ReadWriteTimeout = ReadWriteTimeOut;
                request.Timeout = WaitTimeOut;
                request.KeepAlive = true;
                //设置Range值，断点续传
                if (startPos > 0) request.AddRange(startPos);

                //向服务器请求，获得服务器回应数据流
                response = (HttpWebResponse)request.GetResponse();

                responStream = response.GetResponseStream();
                // 断线重连状态下，这里返回的是内容剩余长度
                long totalSize = response.ContentLength;
                long curSize = startPos;
                //判断是否已经全部下载完成
                if (curSize - startPos == totalSize)
                {
                    fileStream.Flush();
                    fileStream.Close();
                    fileStream = null;
                    if (File.Exists(downloadTask.SavePath)) File.Delete(downloadTask.SavePath);
                    File.Move(tempFile, downloadTask.SavePath);

                    mCurSize = (int)curSize;
                }
                else
                {
                    byte[] bytes = new byte[oneReadLen];
                    int readSize = responStream.Read(bytes, 0, oneReadLen); // 读取第一份数据
                    //是否下载完成标志位
                    bool downloadComplete = false;

                    while (readSize > 0 && !downloadTask.Canceled && !downloadTask.Paused)
                    {
                        fileStream.Write(bytes, 0, readSize);       // 将下载到的数据写入临时文件
                        curSize += readSize;

                        // 判断是否下载完成
                        // 下载完成将temp文件，改成正式文件，断线续传需要考虑起始大小
                        if (curSize - startPos == totalSize)
                        {
                            fileStream.Flush();
                            fileStream.Close();
                            fileStream = null;
                            if (File.Exists(downloadTask.SavePath)) File.Delete(downloadTask.SavePath);
                            File.Move(tempFile, downloadTask.SavePath);

                            downloadComplete = true;
                        }

                        // 回调一下
                        mCurSize = curSize;
                        // 往下继续读取
                        readSize = responStream.Read(bytes, 0, oneReadLen);
                    }

                    if (readSize > 0 && (downloadTask.Canceled || downloadTask.Paused))
                    {
                        if (downloadTask.Paused)
                        {
                            mState = FileDownloadState.Pause;
                        }
                        else if (downloadTask.Canceled)
                        {
                            mState = FileDownloadState.Cancel;
                        }
                    }

                    //没有下载完成抛出异常
                    if (!downloadComplete)
                    {
                        throw new Exception("Download Stop UnExpected ");
                    }
                }
            }
            catch (Exception ex)
            {
                //下载失败，不删除临时文件
                if (fileStream != null) { fileStream.Flush(); fileStream.Close(); fileStream = null; }

                Debug.Log(" Catch Download Error：" + ex.Message);
                mState = FileDownloadState.Error;
                mErrorMsg = "Download Error " + ex.Message;
                mErrorCode = (int)DownloadErrorCode.NETWORK_FILE_DOWNLOAD_ERROR;
            }
            finally
            {
                if (fileStream != null) { fileStream.Flush(); fileStream.Close(); fileStream = null; }
                if (responStream != null) { responStream.Close(); responStream = null; }
                if (response != null) { response.Close(); response.Dispose(); }
                if (request != null) { request.Abort(); request = null; }
                Debug.Log(" Download Catch Finally ");
            }

            if (mState == FileDownloadState.Error) return false;
            return true;
        }

        /// <summary>
        /// 返回url长度
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private long GetWebFileSize(string url)
        {
            HttpWebRequest request = null;
            WebResponse response = null;
            long length = 0;
            try
            {
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = WaitTimeOut;
                request.ReadWriteTimeout = ReadWriteTimeOut;
                //向服务器请求，获得服务器回应数据流
                response = request.GetResponse();
                length = response.ContentLength;
            }
            catch (WebException e)
            {
                Debug.Log("获取文件长度出错：" + e.Message);
                mState = FileDownloadState.Error;
                mErrorMsg = "Request File Length Error " + e.Message;
            }
            finally
            {
                if (response != null) { response.Close(); response = null; }
                if (request != null) { request.Abort(); request = null; }
            }
            return length;
        }

        /// <summary>
        /// 计算文件md5值
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetMD5HashFromFile(string fileName)
        {
            byte[] buffer = new byte[Md5ReadLen];
            int readLength = 0;//每次读取长度  
            var output = new byte[Md5ReadLen];

            using (Stream inputStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider())
                {
                    while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //计算MD5  
                        hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                    }
                    //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)  
                    hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                    byte[] retVal = hashAlgorithm.Hash;

                    StringBuilder sb = new StringBuilder(32);
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }

                    hashAlgorithm.Clear();
                    inputStream.Close();
                    return sb.ToString();
                }
            }
        }
    }
}
