using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using ARWorldEditor;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace ARWorldEditor
{
    public class FileDownloadManager:UnitySingleton<FileDownloadManager>
    {
        #region params
        private static object sLockObject = new object();
        private const int MAX_THREAD_COUNT = 20;

        private Queue<FileDownloadThread> mReadyList;
        private Dictionary<Thread, FileDownloadThread> mRunningList;
        private List<DownloadTask> mCompleteList;
        private List<FileDownloadThread> mErrorList;

        private const int syncRetryCount = 3;
        #endregion

        #region custom


        public FileDownloadManager()
        {
            mReadyList = new Queue<FileDownloadThread>();
            mRunningList = new Dictionary<Thread, FileDownloadThread>();
            mCompleteList = new List<DownloadTask>();
            mErrorList = new List<FileDownloadThread>();

            //https解析的设置
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;

#if UNITY_EDITOR
            //解决editor下没有update问题
            EditorApplication.update += Update;
#endif
        }

 

        /// <summary>
        /// 异步下载某个文件
        /// </summary>
        /// <param name="info"></param>
        public void DownloadAsync(DownloadTask info)
        {
            if (info == null) return;

            var downloadThread = new FileDownloadThread(info);

            lock (sLockObject)
            {
                mReadyList.Enqueue(downloadThread);
            }

            if (mRunningList.Count < MAX_THREAD_COUNT)
            {
                var thread = new Thread(ThreadLoop);
                lock (sLockObject)
                {
                    mRunningList.Add(thread, null);
                }
                thread.Start();
            }

        }

        /// <summary>
        /// 同步不会调用回调函数
        /// </summary>
        public bool DownloadSync(DownloadTask info)
        {
            if (info == null) return false;

            var downloadThread = new FileDownloadThread(info);
            try
            {//同步下载尝试三次
                int i = 0;
                while (i++ < syncRetryCount)
                {
                    downloadThread.Run();
                    if (downloadThread.State == FileDownloadState.Complete) return true;
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Error DownloadSync " + downloadThread.State + " " + downloadThread.GetDownloadTask().FileName + " " + ex.Message + " " + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 删除下载
        /// </summary>
        /// <param name="info"></param>
        public void DeleteDownload(DownloadTask info)
        {
            lock (sLockObject)
            {
                info.IsDelete = true;
            }
        }


        /// <summary>
        ///  清理所有下载
        /// </summary>
        public void ClearAllDownload()
        {
            if (mReadyList == null || mCompleteList == null
                                   || mRunningList == null) return;

            lock (sLockObject)
            {
                foreach (var taskThread in mReadyList)
                {
                    if (taskThread != null)
                    {
                        taskThread.GetDownloadTask().Canceled = true;
                        taskThread.GetDownloadTask().IsDelete = true;
                    }
                }

                foreach (var item in mRunningList)
                {
                    if (item.Value != null)
                    {
                        item.Value.GetDownloadTask().Canceled  = true;
                        item.Value.GetDownloadTask().IsDelete = true;
                    }
                }

                foreach (var task in mCompleteList)
                {
                    if (task != null)
                    {
                        task.Canceled = true;
                        task.IsDelete = true;
                    }
                }
            }
            //关闭thread
            foreach (KeyValuePair<Thread,FileDownloadThread> keyValue in mRunningList)
            {
                keyValue.Key.Abort();
            }
            mRunningList.Clear();
        }

        /// <summary>
        /// thread loop
        /// </summary>
        private void ThreadLoop()
        {
            while (true)
            {
                FileDownloadThread task = null;
                lock (sLockObject)
                {
                    if (mReadyList.Count > 0)
                    {
                        task = mReadyList.Dequeue();
                        mRunningList[Thread.CurrentThread] = task;
                        if (task != null && task.GetDownloadTask().IsDelete)
                        {//已经销毁，不提取运行，直接删除
                            mRunningList[Thread.CurrentThread] = null;
                            continue;
                        }
                    }
                }

                //已经没有需要下载的了
                if (task == null) break;

                task.Run();

                if (task.State == FileDownloadState.Complete)
                {
                    lock (sLockObject)
                    {
                        mCompleteList.Add(task.GetDownloadTask());
                        mRunningList[Thread.CurrentThread] = null;
                    }
                }
                else if (task.State == FileDownloadState.Error)
                {
                    lock (sLockObject)
                    {
                        //如果下载报错不需要重新加入readylist
                        //  mReadyList.Enqueue(task);
                        //防止失败频繁回调，只在特定次数回调
                        if (task.IsNeedErrorCall())
                            mErrorList.Add(task);
                    }
                    break;
                }
                else
                {
                    Debug.Log("Error FileDownloadState " + task.State + " " + task.GetDownloadTask().FileName);
                    break;
                }
            }

        }

        /// <summary>
        /// 更新complete
        /// </summary>
        private void UpdateComplete()
        {
            //回调完成
            if (mCompleteList.Count == 0) return;

            DownloadTask[] completeArr = null;
            lock (sLockObject)
            {
                completeArr = mCompleteList.ToArray();
                mCompleteList.Clear();
            }

            foreach (var downloadTask in completeArr)
            {
                if (downloadTask.IsDelete) continue; //已经销毁，不进行回调
                downloadTask.IsDelete = true;

                downloadTask.progressFun?.Invoke(downloadTask, downloadTask.Size, downloadTask.Size);

                if (downloadTask.completeFun != null)
                {
                    try
                    {
                        downloadTask.completeFun(downloadTask);
                        Debug.Log("download complete " + downloadTask.DownloadUrl);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(" UpdateComplete " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 更新error回调
        /// </summary>
        private void UpdateError()
        {//回调error
            if (mErrorList.Count == 0) return;

            FileDownloadThread[] errorArr = null;
            lock (sLockObject)
            {
                errorArr = mErrorList.ToArray();
                mErrorList.Clear();
            }

            foreach (var task in errorArr)
            {
                var info = task.GetDownloadTask();
                if (info.IsDelete) continue; //已经销毁，不进行回调

                if (info.errorFun != null)
                {
                    info.errorFun(info, task.ErrorCode, task.ErrorMsg);
                    task.ErrorMsg = "";
                }
            }
        }

        /// <summary>
        /// update 进度
        /// </summary>
        private void UpdateProgress()
        {
            //回调进度
            if (mRunningList.Count == 0) return;

            List<FileDownloadThread> runArr = new List<FileDownloadThread>();
            lock (sLockObject)
            {
                foreach (var task in mRunningList.Values)
                {
                    if (task != null) runArr.Add(task);
                }
            }

            foreach (var task in runArr)
            {
                var info = task.GetDownloadTask();
                if (info.IsDelete) continue; //已经销毁，不进行回调

                info.progressFun?.Invoke(info, task.CurSize, task.TotalSize);
            }
        }

        /// <summary>
        /// 线程更新
        /// </summary>
        private void UpdateThread()
        {
            if (mReadyList.Count == 0 && mRunningList.Count == 0) return;

            lock (sLockObject)
            {//关闭卡死的线程，需要将任务重新加入队列
                List<Thread> deadThreadList = new List<Thread>();
                foreach (var item in mRunningList)
                {
                    if (item.Key.IsAlive) continue;

                    if (item.Value != null) mReadyList.Enqueue(item.Value);

                    deadThreadList.Add(item.Key);
                }

                foreach (var thread in deadThreadList)
                {
                    thread.Abort();
                    mRunningList.Remove(thread);
                }
            }

            //如果没有网络，不开启新线程，旧线程会逐个关闭
            if (!NetworkUtility.IsNetworkAvaible()) return;

            if (mRunningList.Count >= MAX_THREAD_COUNT) return;
            if (mReadyList.Count > 0)
            {
                var thread = new Thread(ThreadLoop);
                lock (sLockObject)
                {
                    mRunningList.Add(thread, null);
                }
                thread.Start();
                // InsightDebug.Log(TAG, "Start New Thread " + thread.Name);
            }
        }

        /// <summary>
        /// update
        /// </summary>
        public void Update()
        {
            UpdateComplete();
            UpdateProgress();
            UpdateError();
            UpdateThread();
        }

        /// <summary>
        /// 销毁线程
        /// </summary>
        public void OnDestroy()
        {
            Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            ClearAllDownload();
        }

        /// <summary>
        /// ssl验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool RemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain, look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                        }
                    }
                }
            }
            return isOk;
        }
        #endregion
    }
}