using AOT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duktape;
using System;

/// <summary>
/// js下载回调
/// </summary>
public struct DownloadForContent
{
    public DuktapeObject duktapeObj;    //js脚本
    public DuktapeObject successObj;    //成功回调
    public DuktapeObject progressObj;   //进度回调（暂时不提供到内容层，此数据不可靠）
    public DuktapeObject failObj;       //失败回调
}
/// <summary>
/// 下载状态
/// </summary>
public enum DOWNLOADSTATUS
{
    isFailed = 0,
    isDownloading = 1,
    isDownloaded = 2
}

public class LsDownloadWithNative : Singleton<LsDownloadWithNative>
{
    private static string TAG = "LsDownloadWithNative";

    private class DownloadTask
    {
        public string url;
        public string path;
        //标记是SDK请求或者内容请求
        public bool isSDKRequest;
        //记录下载状态
        public DOWNLOADSTATUS downloadStatus;
        //SDK执行
        public Action<string> onSuccess;
        public Action<float> onProgress;
        public Action<string, string> onFail;
        //内容执行
        public DownloadForContent downloadForContent;
    }

    private static Dictionary<string, DownloadTask> TaskData;

    InsightAPPNative.IDownloadSuccessCallback DownloadSuccess;
    InsightAPPNative.IDownloadProgressCallback DownloadProgress;
    InsightAPPNative.IDownloadFailCallback DownloadFail;

    public void Init()
    {

        TaskData = new Dictionary<string, DownloadTask>();
        DownloadSuccess = lsDownloadSuccessCallback;
        DownloadProgress = lsDownloadProgressCallback;
        DownloadFail = lsDownloadFailCallback;
        //注册回调到native接口
        InsightAPPNative.SetDownloadFileCallback(DownloadSuccess, DownloadProgress, DownloadFail);
    }

    public void Close()
    {

        TaskData.Clear();
        DownloadSuccess = null;
        DownloadProgress = null;
        DownloadFail = null;
    }

    /// <summary>
    /// SDK下载请求
    /// </summary>
    /// <param name="downloadUrl"></param>
    /// <param name="downloadPath"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    /// <param name="onFail"></param>
    public void lsDowndloadRequest(string downloadUrl, string downloadPath,
        Action<string> onSuccess,
        Action<float> onProgress = null,
        Action<string, string> onFail = null)
    {

        if (string.IsNullOrEmpty(downloadUrl))
        {
            InsightDebug.LogError(TAG, "download url is null or empty!");
            return;
        }
        if (onSuccess == null)
        {
            InsightDebug.LogError(TAG, "download success callback is null or empty!");
            return;
        }

        // create unique id
        string nonce_userdata = EncodeUtility.MD5(downloadUrl + downloadPath);
        TaskData.TryGetValue(nonce_userdata, out DownloadTask task);
        bool needDownload = false;
        if (task == null)
        {
            needDownload = true;
        }
        else
        {
            if (task.downloadStatus == DOWNLOADSTATUS.isFailed)
            {
                needDownload = true;
            }
            else
            {
                InsightDebug.Log(TAG, "download task has been being downloading or finished -> " + downloadUrl);
            }
        }

        if (needDownload)
        {
            if (task == null)
                TaskData.Add(nonce_userdata, new DownloadTask
                {
                    url = downloadUrl,
                    path = downloadPath,
                    isSDKRequest = true,
                    downloadStatus = DOWNLOADSTATUS.isDownloading,
                    onSuccess = onSuccess,
                    onProgress = onProgress,
                    onFail = onFail
                });

            InsightAPPNative.DownloadFile(downloadUrl, downloadPath, nonce_userdata);
#if UNITY_EDITOR
            onProgress?.Invoke(99);
            onProgress?.Invoke(100);
            onProgress?.Invoke(100);
            onProgress?.Invoke(100);
            onSuccess?.Invoke(downloadPath);
            //onFail?.Invoke("resultCode", "resultDesc");
#endif
        }
    }

    /// <summary>
    /// 内容下载请求
    /// </summary>
    /// <param name="downloadUrl"></param>
    /// <param name="downloadPath"></param>
    /// <param name="duktapeObject"></param>
    /// <param name="successObject"></param>
    /// <param name="progressObject"></param>
    /// <param name="failObject"></param>
    public void lsDowndloadRequest(string downloadUrl, string downloadPath,
        DuktapeObject duktapeObject,
        DuktapeObject successObject,
        DuktapeObject progressObject,
        DuktapeObject failObject)
    {
        if (string.IsNullOrEmpty(downloadUrl))
        {
            InsightDebug.LogError(TAG, "download url is null or empty!");
            return;
        }
        if (successObject == null)
        {
            InsightDebug.LogError(TAG, "download success callback is null or empty!");
            return;
        }
        if (!string.IsNullOrEmpty(downloadPath))
        {
            downloadPath = PathUtils.Combine(GameSceneData.Instance.GetContentPath(), downloadPath);
            InsightDebug.Log(TAG, "downloadPath: " + downloadPath);
        }

        // create unique id
        string nonce_userdata = EncodeUtility.MD5(downloadUrl + downloadPath);
        bool needDownload = false;
        TaskData.TryGetValue(nonce_userdata, out DownloadTask task);
        if (task == null)
        {
            needDownload = true;
        }
        else
        {
            if (task.downloadStatus == DOWNLOADSTATUS.isFailed)
            {
                needDownload = true;
            }
            else
            {
                InsightDebug.Log(TAG, "download task has been being downloading or finished -> " + downloadUrl);
            }
        }

        if (needDownload)
        {
            if (task == null)
                TaskData.Add(nonce_userdata, new DownloadTask
                {
                    url = downloadUrl,
                    path = downloadPath,
                    isSDKRequest = false,
                    downloadStatus = DOWNLOADSTATUS.isDownloading,
                    downloadForContent = new DownloadForContent
                    {
                        duktapeObj = duktapeObject,
                        successObj = successObject,
                        progressObj = progressObject,
                        failObj = failObject
                    }
                });

            InsightAPPNative.DownloadFile(downloadUrl, downloadPath, nonce_userdata);
#if UNITY_EDITOR
            lsDownloadProgressCallback(downloadUrl, 1, nonce_userdata);
            lsDownloadProgressCallback(downloadUrl, 2, nonce_userdata);
            lsDownloadProgressCallback(downloadUrl, 47, nonce_userdata);
            lsDownloadProgressCallback(downloadUrl, 100, nonce_userdata);
            lsDownloadSuccessCallback(downloadUrl, downloadPath, nonce_userdata);
            //lsDownloadFailCallback(downloadUrl, "resultCode", "resultDesc", nonce_userdata);
#endif
        }
    }

    /// <summary>
    /// 下载成功回调
    /// </summary>
    /// <param name="str"></param>
    [MonoPInvokeCallback(typeof(InsightAPPNative.IDownloadSuccessCallback))]
    public static void lsDownloadSuccessCallback(string url, string path, string userdata)
    {
        var copyUrl = url;
        var copyPath = path;
        var copyUserData = userdata;

        Debug.Log("download success: url:" + copyUrl + "  path:" + copyPath + "  userdata:" + copyUserData);
        TaskData.TryGetValue(copyUserData, out DownloadTask task);
        if (task != null)
        {
            task.downloadStatus = DOWNLOADSTATUS.isDownloaded;
            if (task.isSDKRequest)
            {
                task.onSuccess?.Invoke(copyPath);
            }
            else
            {
                if (task.downloadForContent.duktapeObj != null && task.downloadForContent.successObj != null)
                {
                    var result = copyPath;
                    IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                    DuktapeUtility.CallMethod(context, task.downloadForContent.duktapeObj.heapPtr, task.downloadForContent.successObj.heapPtr, result);
                }
            }
        }
    }

    /// <summary>
    /// 下载进度回调
    /// </summary>
    /// <param name="str"></param>
    [MonoPInvokeCallback(typeof(InsightAPPNative.IDownloadSuccessCallback))]
    public static void lsDownloadProgressCallback(string url, float progress, string userdata)
    {
        var copyUrl = url;
        var copyProgress = progress;
        var copyUserData = userdata;

        Debug.Log("download progress: url:" + copyUrl + "  progress:" + copyProgress + "  userdata:" + copyUserData);
        TaskData.TryGetValue(copyUserData, out DownloadTask task);
        if (task != null)
        {
            if (task.isSDKRequest)
            {
                task.onProgress?.Invoke(copyProgress);
            }
            else
            {
                if (task.downloadForContent.duktapeObj != null && task.downloadForContent.progressObj != null)
                {
                    var result = copyProgress.ToString();
                    IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                    DuktapeUtility.CallMethod(context, task.downloadForContent.duktapeObj.heapPtr, task.downloadForContent.progressObj.heapPtr, result);
                }
            }
        }

    }

    /// <summary>
    /// 下载失败回调
    /// </summary>
    /// <param name="str"></param>
    [MonoPInvokeCallback(typeof(InsightAPPNative.IDownloadSuccessCallback))]
    public static void lsDownloadFailCallback(string url, string resultCode, string resultDesc, string userdata)
    {
        var copyUrl = url;
        var copyResultCode = resultCode;
        var copyResultDesc = resultDesc;
        var copyUserData = userdata;

        Debug.Log("download fail: url:" + copyUrl + "  resultCode:" + copyResultCode + "  resultDesc:" + copyResultDesc + "  userdata:" + copyUserData);
        TaskData.TryGetValue(copyUserData, out DownloadTask task);
        if (task != null)
        {
            task.downloadStatus = DOWNLOADSTATUS.isFailed;
            if (task.isSDKRequest)
            {
                task.onFail?.Invoke(copyResultCode, copyResultDesc);
            }
            else
            {
                if (task.downloadForContent.duktapeObj != null && task.downloadForContent.failObj != null)
                {
                    string result = "{\"resultCode\":\"" + copyResultCode + "\",\"resultDesc\":\"" + copyResultDesc + "\"}";
                    IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                    DuktapeUtility.CallMethod(context, task.downloadForContent.duktapeObj.heapPtr, task.downloadForContent.failObj.heapPtr, result);
                }
            }
        }
    }



    //todo
    //暂停下载
    //重复下载（done,一次正常的体验过程同一个对象仅支持下载一次）
    //更新下载
}
