using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 下载场景大文件
/// </summary>
public class DownloadFileFetchManager : UnitySingleton<DownloadFileFetchManager>
{
    private const string TAG = "DownloadFileFetchManager";

    /// <summary>
    /// download product
    /// </summary>
    /// <param name="product"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    public void DownloadProduct(ArProduct product, Action<string, string> onError, Action onSuccess, Action<float> onProgress)
    {
        StartCoroutine(downloadProductAndUnzipCoroutine(product, onError, onSuccess, onProgress));
    }

    /// <summary>
    /// 下载和解压
    /// </summary>
    /// <param name="url"></param>
    /// <param name="fileName"></param>
    /// <param name="pid"></param>
    /// <param name="materialType"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    public void DownloadResourcesAndUnzipByUrl(string url, string fileName, string pid, string materialType, Action<int, string> onError,
    Action onSuccess, Action<float> onProgress)
    {
        string downloadFilePath = ConstPath.RootDirectory() + "/" + ConstPath.ProductDirectory() + "/" + pid + "/" + materialType + "/" + fileName;
        string tempFilePath = ConstPath.RootDirectory() + "/" + ConstPath.TempDirectory() + "/" + pid + "/" + materialType + "/" + fileName;
        StartCoroutine(downloadCoroutine(url, downloadFilePath, tempFilePath, fileName, onError, () =>
        {
            //unzip
            UnzipAndDeleteZipFile(downloadFilePath);
            onSuccess?.Invoke();
        }, onProgress));
    }

    /// <summary>
    /// 下载普通数据
    /// </summary>
    /// <param name="baseData"></param>
    /// <param name="url"></param>
    /// <param name="pid"></param>
    /// <param name="keyId"></param>
    /// <param name="fileName"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    public void downloadAndUnzip(BaseDbData baseData, string url, int pid, int keyId, string fileName, Action<int, string> onError,
Action onSuccess, Action<float> onProgress)
    {
        StartCoroutine(downloadAndUnzipCoroutine(baseData, url, pid, keyId, fileName, onError,
            onSuccess, onProgress));
    }

    /// <summary>
    /// 下载普通数据协程
    /// </summary>
    /// <param name="baseData"></param>
    /// <param name="url"></param>
    /// <param name="pid"></param>
    /// <param name="keyId"></param>
    /// <param name="fileName"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    /// <returns></returns>
    private IEnumerator downloadAndUnzipCoroutine(BaseDbData baseData, string url, int pid, int keyId, string fileName, Action<int, string> onError,
    Action onSuccess, Action<float> onProgress)
    {
        if (baseData == null)
        {
            onError?.Invoke((int)DownloadErrorCode.NETWORK_TARGET_NOT_EXITS, "Download Target Not Exits");
            yield break;
        }
        string relativeDownloadDirectory = Path.Combine(ConstPath.ProductDirectory(), pid.ToString(), keyId.ToString());
        string downloadDirectory = Path.Combine(ConstPath.RootDirectory(), relativeDownloadDirectory);

        string relativeTempDirectory = Path.Combine(ConstPath.TempDirectory(), pid.ToString(), keyId.ToString());
        string tempProductDirectory = Path.Combine(ConstPath.RootDirectory(), relativeTempDirectory);

        baseData.DownloadPath = relativeDownloadDirectory;

        if (Directory.Exists(downloadDirectory))
        {
            FileUtility.DirectoryDeleteRF(downloadDirectory);
        }
        Directory.CreateDirectory(downloadDirectory);
        string downloadFilePath = downloadDirectory + "/" + fileName;
        string tempFilePath = tempProductDirectory + "/" + fileName;

        yield return downloadCoroutine(url, downloadFilePath, tempFilePath, fileName
               , (int code, string msg) =>
               {
                   InsightDebug.Log(TAG, "Download Error " + code + "msg " + msg);
                   baseData.DownloadState = DownloadState.ERROR;
                   //更新进度
                   InsightCacheManager.Instance.AddOrUpdate(baseData);
                   onError?.Invoke(code, msg);

               }, () =>
               {
                   baseData.DownloadState = DownloadState.FINISHED;
                   baseData.UnzipState = UnZipState.START;
                   UnzipAndDeleteZipFile(downloadFilePath);

                   baseData.DownloadProgress = 100;
                   baseData.UnzipState = UnZipState.FINISHED;
                   InsightCacheManager.Instance.AddOrUpdate(baseData);
                   //写入内存
                   InsightCacheManager.Instance.WriteToDisk();
                   onSuccess?.Invoke();

               }, (float progress) =>
               {
                   baseData.DownloadState = DownloadState.RUNNING;
                   baseData.DownloadProgress = (int)(progress * 100);
                   InsightCacheManager.Instance.AddOrUpdate(baseData);
                   onProgress?.Invoke(progress);
               });
    }

    /// <summary>
    /// 根据product id 下载arproduct
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    private IEnumerator downloadProductAndUnzipCoroutine(ArProduct arProduct, Action<string, string> onError,
    Action onSuccess, Action<float> onProgress)
    {
        if (arProduct == null)
        {
            onError?.Invoke(DownloadErrorCode.NETWORK_TARGET_NOT_EXITS.ToString(), "Download Target Not Exits");
            yield break;
        }

        List<ProductMaterial> productMaterials = arProduct.ProductMaterials;
        if (productMaterials == null || productMaterials.Count == 0) 
            yield break;
        string pid = arProduct.Cid.ToString();

        string relativeProductPath = Path.Combine(ConstPath.ProductDirectory(), pid);
        string downloadProductDirectory = Path.Combine(ConstPath.RootDirectory(), relativeProductPath);

        string relativeTempPath = Path.Combine(ConstPath.TempDirectory(), pid);
        string tempProductDirectory = Path.Combine(ConstPath.RootDirectory(), relativeTempPath);

        //存储相对路径
        arProduct.DownloadPath = relativeProductPath;
        //如果本地文件已经存在，需要先删除目录
        if (Directory.Exists(downloadProductDirectory))
        {
            FileUtility.DirectoryDeleteRF(downloadProductDirectory);
        }

        Directory.CreateDirectory(downloadProductDirectory);

        int successMaterialCount = 0;
        int materialsCount = productMaterials.Count;

        arProduct.DownloadState = DownloadState.START;
        InsightCacheManager.Instance.AddOrUpdate(arProduct);
        for (int i = 0; i < materialsCount; i++)
        {
            ProductMaterial productMaterial = productMaterials[i];
            string url = productMaterial.Url;
            string materialType = productMaterial.Type.ToString();
            string fileName = productMaterial.Md5 + ".zip";

            string relativeMaterialPath = Path.Combine(relativeProductPath, materialType);
            string downloadMaterialDirectory = Path.Combine(ConstPath.RootDirectory(), relativeMaterialPath);
            Directory.CreateDirectory(downloadMaterialDirectory);
            productMaterial.DownloadPath = relativeMaterialPath;

            string downloadFilePath = downloadMaterialDirectory + "/" + fileName;
            string tempFilePath = tempProductDirectory + "/" + materialType + "/" + fileName;

            yield return downloadCoroutine(url, downloadFilePath, tempFilePath, fileName
                , (int code, string msg) =>
                {
                    InsightDebug.Log(TAG, "Download Material Error " + code + "msg " + msg);
                    productMaterial.DownloadState = DownloadState.ERROR;
                    arProduct.DownloadState = DownloadState.ERROR;
                    InsightCacheManager.Instance.AddOrUpdate(arProduct);

                }, () =>
                {
                    productMaterial.DownloadState = DownloadState.FINISHED;
                    productMaterial.UnzipState = UnZipState.START;
                    UnzipAndDeleteZipFile(downloadFilePath);
                    productMaterial.UnzipState = UnZipState.FINISHED;
                    InsightCacheManager.Instance.AddOrUpdate(arProduct);

                    successMaterialCount++;

                }, (float progress) =>
                {
                    arProduct.DownloadState = DownloadState.RUNNING;
                    productMaterial.DownloadState = DownloadState.RUNNING;
                    productMaterial.DownloadProgress = (int)(progress * 100);

                    InsightCacheManager.Instance.AddOrUpdate(arProduct);
                });
        }

        if (successMaterialCount == materialsCount)
        {
            arProduct.DownloadProgress = 100;
            arProduct.DownloadState = DownloadState.FINISHED;
            arProduct.UnzipState = UnZipState.FINISHED;
            InsightCacheManager.Instance.AddOrUpdate(arProduct);
            //写入内存
            InsightCacheManager.Instance.WriteToDisk();
            onSuccess?.Invoke();
        }
        else
        {
            onError(DownloadErrorCode.NETWORK_FILE_COUNT_ERROR.ToString(), "Download File Count Error");
        }
    }



    /// <summary>
    /// 下载协程
    /// </summary>
    /// <param name="url"></param>
    /// <param name="downloadPath"></param>
    /// <param name="fileName"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    /// <returns></returns>
    IEnumerator downloadCoroutine(string url, string downloadPath, string tempFilePath, string fileName = null, Action<int, string> onError = null,
        Action onSuccess = null, Action<float> onProgress = null)
    {
        if (string.IsNullOrEmpty(fileName)) fileName = GetDownloadFileNameByUrl(url);
        DownloadTask downloadTask = new DownloadTask();
        downloadTask.DownloadUrl = url;
        downloadTask.SavePath = downloadPath;
        downloadTask.TempPath = tempFilePath;
        downloadTask.FileName = fileName;

        bool isDownloadFinish = false;
        downloadTask.AddListener(new DownloadCompleteCallBack((DownloadTask task) =>
        {
            isDownloadFinish = true;
            onSuccess?.Invoke();

        }), new DownloadProgressCallBack((DownloadTask task, long curSize, long totalSize) =>
        {
            //每个事件的progress
            if (onProgress != null)
            {
                float progress = 0.0f;
                if (totalSize != 0)
                {
                    progress = (float)curSize / totalSize;
                }
                onProgress(progress);
            }
        }), new DownloadErrorCallBack((DownloadTask task, int code, string msg) =>
        {
            isDownloadFinish = true;
            onError?.Invoke(code, msg);
        }));
        FileDownloadManager.Instance.DownloadAsync(downloadTask);

        //一直等待返回执行结果
        while (!isDownloadFinish)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 从url获取下载文件名称
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private string GetDownloadFileNameByUrl(string url)
    {
        int positionSeparator = url.LastIndexOf(Constants.URL_PATH_SEPARATOR);
        int positionParams = url.IndexOf(Constants.URL_PARAMS_SEPARATOR);
        if (positionParams != -1 && positionParams <= positionSeparator) return null;
        return url.Substring(positionSeparator + 1, positionParams == -1 ? url.Length : positionParams - positionSeparator - 1);
    }

    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="savePath"></param>
    private void UnzipAndDeleteZipFile(string zipFilePath)
    {
        if (!zipFilePath.EndsWith(".zip")) return;
        string unzipDirectory = Path.GetDirectoryName(zipFilePath);
        UnzipFile.Unzip(zipFilePath, unzipDirectory);
        //解压后删除本地zip 文件
        File.Delete(zipFilePath);
    }
}
