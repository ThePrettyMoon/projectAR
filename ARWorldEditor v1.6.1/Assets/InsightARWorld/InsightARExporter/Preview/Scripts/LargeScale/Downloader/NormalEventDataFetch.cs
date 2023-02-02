using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
///  普通事件下载
/// </summary>
public class NormalEventDataFetch : Singleton<NormalEventDataFetch>
{
    /// <summary>
    /// 根据id 下载
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    public void DownloadProductById(string contentId, Action<string, string> onError,
   Action<ArProduct, DownloadProductState> onSuccess, Action<float> onProgress)
    {
        ArProduct localProduct = QueryLocalProduct(contentId);
        //先检查是否有网络,没有网络采用本地数据
        if (NetworkUtility.IsNetworkAvaible())
        {
            NetDataFetchManager.Instance.QuerySubContent(int.Parse(contentId), new OnOasisNetworkDataFetchCallback<SubContentResponseData>(
                (SubContentResponseData subContent) =>
                {
                    ArProduct arProduct = SubArProduct.Invert(subContent.Content);
                    bool needUpdate = CheckIfNeedDownload(localProduct, arProduct);
                    if (needUpdate)
                    {
                        GameSceneData.Instance.UpdatePOINeedUpdate(contentId, POINeedUpdateData.NEEDUPDATE);
                        DownloadProduct(arProduct, onError, onSuccess, onProgress);
                    }
                    else
                    {
                        if (localProduct != null)
                        {
                            GameSceneData.Instance.UpdatePOINeedUpdate(contentId, POINeedUpdateData.NO_NEEDUPDATE);
                            onSuccess(localProduct, DownloadProductState.DOWNLOAD_STATE_FROM_LOCAL);
                        }
                    }
                }, (string code, string msg) =>
                {
                    //获取arproduct 出错
                    onError?.Invoke(code, msg);
                }));
        }
        else
        {
            if (localProduct != null)
            {
                GameSceneData.Instance.UpdatePOINeedUpdate(contentId, POINeedUpdateData.NO_NEEDUPDATE);
                onSuccess(localProduct, DownloadProductState.DOWNLOAD_STATE_FROM_LOCAL);
            }
            else {
                onError(DownloadErrorCode.NETWORK_NOT_REACHABLE.ToString(), "Network Not Reachable");
            }
        }
    }

    /// <summary>
    /// 检查远程是否有数据更新
    /// </summary>
    /// <param name="product"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    /// <param name="networkToast"></param>
    public void CheckAndDownloadProduct(ArProduct product, Action<string, string> onError,
 Action<ArProduct, DownloadProductState> onSuccess, Action<float> onProgress, bool networkToast = false)
    {
        if (NetworkUtility.IsNetworkAvaible())
        {
            NetDataFetchManager.Instance.QuerySubContent(product.Cid, new OnOasisNetworkDataFetchCallback<SubContentResponseData>(
                (SubContentResponseData subContent) =>
            {
                ArProduct arProduct = SubArProduct.Invert(subContent.Content);
                DownloadProduct(arProduct, onError, onSuccess, onProgress,networkToast);
            }, (string code, string msg) =>
            {
                //获取arproduct 出错
                onError?.Invoke(code, msg);
            }));
        }
        else
        {
            DownloadProduct(product, onError, onSuccess, onProgress,networkToast);
        }
    }

    /// <summary>
    /// 下载product，先判断是否已经下载
    /// </summary>
    /// <param name="product"></param>
    /// <param name="onError"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onProgress"></param>
    /// <param name="networkToast"></param>
    private void DownloadProduct(ArProduct product, Action<string, string> onError,
 Action<ArProduct, DownloadProductState> onSuccess, Action<float> onProgress,bool networkToast = false)
    {
        int pid = product.Cid;
        ArProduct localProduct = InsightCacheManager.Instance.Query<ArProduct>(pid);

        //检查是否需要更新
        if (!CheckIfNeedDownload(localProduct, product))
        {
            onSuccess?.Invoke(localProduct, DownloadProductState.DOWNLOAD_STATE_FROM_LOCAL);
            return;
        }

        //下载前先删除本地缓存
        if (localProduct != null)
        {
            string localDownloadPath = Path.Combine(ConstPath.RootDirectory(), localProduct.DownloadPath);
            if (Directory.Exists(localDownloadPath))
            {
                FileUtility.DirectoryDeleteRF(localDownloadPath);
            }
        }

        //如果没有网络给出提示
        if(networkToast && !NetworkUtility.IsNetworkAvaible())
        {
            NotifyNativeMessage.MakeToast("网络异常，请检查网络连接");
        }

        DownloadFileFetchManager.Instance.DownloadProduct(product, (string code, string msg) =>
        {
            onError?.Invoke(code, msg);
        }, () =>
        {
            onSuccess?.Invoke(product, DownloadProductState.DOWNLOAD_STATE_FROM_SERVER);

        }, (float progress) =>
         {
             onProgress?.Invoke(progress);
             product.DownloadProgress = (int)(progress * 100);
         });
    }

    /// <summary>
    /// 检查是否需要重新下载
    /// </summary>
    private bool CheckIfNeedDownload(ArProduct localProduct,ArProduct arProduct)
    {
        //判断本地product
        if (localProduct == null) return true;

        // 是否是下载解压完成状态
        if (!localProduct.IsValid()) return true;

        //判断更新时间是否一致
        if (localProduct.UpdateTime != arProduct.UpdateTime) return true;

        //判断本地是否存在对应文件目录
        string localPath = Path.Combine(ConstPath.RootDirectory(), localProduct.DownloadPath);
        if (!Directory.Exists(localPath)) return true;

        //可以进一步check文件大小和md5
        return false;
    }

    /// <summary>
    /// 检查本地pid是否存在
    /// </summary>
    /// <param name="pid"></param>
    /// <returns></returns>
    private ArProduct QueryLocalProduct(string pid)
    {
        ArProduct localProduct = InsightCacheManager.Instance.Query<ArProduct>(pid);
        if (localProduct == null) return null;
        // 是否是下载解压完成状态
        if (!localProduct.IsValid()) return null;

        //判断本地是否存在对应文件目录
        string localPath = Path.Combine(ConstPath.RootDirectory(), localProduct.DownloadPath);
        if (!Directory.Exists(localPath)) return null;

        return localProduct;
    }
}
