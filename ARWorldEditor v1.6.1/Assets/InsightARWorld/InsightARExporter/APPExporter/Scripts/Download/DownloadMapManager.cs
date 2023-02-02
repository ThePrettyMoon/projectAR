using System;
using System.Collections;
using System.IO;
using ARWorldEditor;
using UnityEngine;

namespace ARWorldEditor
{
    //文件下载
    public class DownloadMapManager : UnitySingleton<DownloadMapManager>
    {
        /// <summary>
        /// 下载地图资源
        /// </summary>
        /// <param name="map"></param>
        /// <param name="onError"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onProgress"></param>
        public void DownloadMap(MapResourcesResultData map, Action<string, string> onError,
            Action<MapResourcesResultData, DownloadProductState> onSuccess, Action<string, float> onProgress)
        {
            int mapId = (int)map.mapId;
            MapResourcesResultData localMap = InsightCacheManager.Instance.Query<MapResourcesResultData>(mapId);

            if (!CheckIfNeedDownload(localMap, map))
            {
                onSuccess?.Invoke(localMap, DownloadProductState.DOWNLOAD_STATE_FROM_LOCAL);
                return;
            }

            if (!NetworkUtility.IsNetworkAvaible())
            {
                Debug.Log("当前无网络，请检查网络");
                onError?.Invoke(DownloadErrorCode.NETWORK_NOT_REACHABLE.ToString(), "当前无网络，请检查网络");
                return;
            }

            StartDownloadMap(map, localMap, (string code, string msg) =>
             {
                 onError?.Invoke(code, msg);
             }, () =>
             {
                 onSuccess?.Invoke(map, DownloadProductState.DOWNLOAD_STATE_FROM_SERVER);

             }, (string fileName, float progress) =>
             {
                 onProgress?.Invoke(fileName, progress);
                 map.DownloadProgress = (int)(progress * 100);
             });
        }

        /// <summary>
        /// 下载地图资源
        /// </summary>
        /// <param name="map"></param>
        /// <param name="localMap"></param>
        /// <param name="onError"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onProgress"></param>
        private void StartDownloadMap(MapResourcesResultData map, MapResourcesResultData localMap, Action<string, string> onError,
Action onSuccess, Action<string, float> onProgress)
        {
#if UNITY_EDITOR
            EditorCoroutines.StartCoroutine(downloadMapAndUnzipCoroutine(map, localMap, onError, onSuccess, onProgress), this);
#else
            StartCoroutine(downloadMapAndUnzipCoroutine(map, localMap, onError, onSuccess, onProgress));
#endif
           
        }


        /// <summary>
        /// 下载地图协程
        /// </summary>
        /// <param name="map"></param>
        /// <param name="localMap"></param>
        /// <param name="onError"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onProgress"></param>
        /// <returns></returns>
        private IEnumerator downloadMapAndUnzipCoroutine(MapResourcesResultData map, MapResourcesResultData localMap, Action<string, string> onError,
  Action onSuccess, Action<string, float> onProgress)
        {
            if (map == null)
            {
                onError?.Invoke(DownloadErrorCode.NETWORK_FILE_DOWNLOAD_ERROR.ToString(), "Download Target Not Exits!");
                yield break;
            }

            string downloadMapDir = Path.Combine(ConfigGlobal.MAP_PATH, map.mapId.ToString());
            string downloadTempMapDir = Path.Combine(ConfigGlobal.MAP_TEMP_PATH, map.mapId.ToString());

            if (Directory.Exists(downloadMapDir))
            {
                Directory.CreateDirectory(downloadMapDir);
            }

            map.DownloadPath = downloadMapDir;

            map.DownloadState = DownloadState.START;
            InsightCacheManager.Instance.AddOrUpdate(map);
            int successMapCount = 0;

            for (int i = 0; i < map.resourceList.Count; i++)
            {
                MapResourcesData subMap = map.resourceList[i];

                if (subMap == null) continue;
                MapResourcesData localSubMap = null;
                if (localMap != null && localMap.resourceList != null)
                {
                    localSubMap = localMap.resourceList.Find(p => p.type == subMap.type);
                }

                string downloadSubMapDir = Path.Combine(downloadMapDir, subMap.type.ToString());
                string downloadSubMapTempDir = Path.Combine(downloadTempMapDir, subMap.type.ToString());

                if (localSubMap != null)
                {
                    if (localSubMap.gmtModified != subMap.gmtModified)
                    {
                        if (Directory.Exists(downloadSubMapDir))
                        {
                            FileUtility.DirectoryDeleteRF(downloadSubMapDir);
                        }
                    }
                    else // 如果本地时间一致，本地文件夹存在，不需要更新
                    {
                        if (Directory.Exists(downloadSubMapDir))
                        {
                            successMapCount++;
                            continue;
                        }
                    }
                }
                //如果本地子地图存在，需要先删除
                if (Directory.Exists(downloadSubMapDir)) FileUtility.DirectoryDeleteRF(downloadSubMapDir);

                Directory.CreateDirectory(downloadSubMapDir);

                if (!Directory.Exists(downloadSubMapTempDir))
                {
                    Directory.CreateDirectory(downloadSubMapTempDir);
                }


                string url = subMap.resourceUrl;

                string fileName = GetDownloadFileNameByUrl(subMap.resourceUrl);

                string downloadSubmapFilePath = Path.Combine(downloadSubMapDir, fileName);
                string downloadSubmapTempFilePath = Path.Combine(downloadSubMapTempDir, fileName);

                Debug.Log("export file name " + fileName + " " + url);
                IEnumerator coroutine = downloadCoroutine(url, downloadSubmapFilePath, downloadSubmapTempFilePath, fileName
              , (int code, string msg) =>
              {
                  map.DownloadState = DownloadState.ERROR;
                  InsightCacheManager.Instance.AddOrUpdate(map);

              }, () =>
              {
                  UnzipAndDeleteZipFile(downloadSubmapFilePath);
                  InsightCacheManager.Instance.AddOrUpdate(map);
                  successMapCount++;

              }, (string fileDesc, float progress) =>
              {
                  map.DownloadState = DownloadState.RUNNING;
                  InsightCacheManager.Instance.AddOrUpdate(map);
                  onProgress(fileDesc, progress);
              });
#if UNITY_EDITOR
                yield return EditorCoroutines.StartCoroutine(coroutine, this);
#else
                yield return StartCoroutine(coroutine);
#endif
            }

            //所有文件已经下载完毕
            if (successMapCount == map.resourceList.Count)
            {
                map.DownloadProgress = 100;
                map.DownloadState = DownloadState.FINISHED;
                map.UnzipState = UnZipState.FINISHED;
                InsightCacheManager.Instance.AddOrUpdate(map);
                //写入内存
                InsightCacheManager.Instance.WriteToDisk();
                onSuccess?.Invoke();
            }
            else
            {
                onError(DownloadErrorCode.NETWORK_FILE_COUNT_ERROR.ToString(), "download map error");
            }
        }

        /// <summary>
        /// 下载地图协程
        /// </summary>
        /// <param name="url"></param>
        /// <param name="downloadPath"></param>
        /// <param name="tempFilePath"></param>
        /// <param name="fileName"></param>
        /// <param name="onError"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onProgress"></param>
        /// <returns></returns>
        IEnumerator downloadCoroutine(string url, string downloadPath, string tempFilePath, string fileName = null, Action<int, string> onError = null,
    Action onSuccess = null, Action<string, float> onProgress = null)
        {
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
                    onProgress(task.FileName, progress);
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
        /// url返回名称
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetDownloadFileNameByUrl(string url)
        {
            int positionSeparator = url.LastIndexOf(Constants.URL_PATH_SEPARATOR);
            int positionParams = url.IndexOf(Constants.URL_PARAMS_SEPARATOR);
            if (positionParams != -1 && positionParams <= positionSeparator) return null;
            int length = url.Length;
            return url.Substring(positionSeparator + 1, positionParams == -1 ? length - positionSeparator - 1 : positionParams - positionSeparator - 1);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="savePath"></param>
        private void UnzipAndDeleteZipFile(string zipFilePath)
        {
            if (!zipFilePath.EndsWith(".zip")) return;
            string unzipDirectory = Path.GetDirectoryName(zipFilePath);
            ARWorldEditor.ZipUtility.Unzip(zipFilePath, unzipDirectory);
            //解压后删除本地zip 文件
            File.Delete(zipFilePath);
        }


        /// <summary>
        /// 检查文件是否需要更新
        /// </summary>
        /// <param name="localMap"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private bool CheckIfNeedDownload(MapResourcesResultData localMap, MapResourcesResultData map)
        {
            if (localMap == null) return true;
            if (localMap.gmtModified != map.gmtModified) return true;
            if (!Directory.Exists(localMap.DownloadPath)) return true;
            //检查一下子文件夹是否被删除，如果删除需要重新下载
            if (localMap.resourceList != null)
            {
                for (int i = 0; i < localMap.resourceList.Count; i++)
                {
                    MapResourcesData subMap = localMap.resourceList[i];
                    if (subMap != null)
                    {
                        string subMapDirectory = Path.Combine(localMap.DownloadPath, subMap.type);
                        if (!Directory.Exists(subMapDirectory)) return true;
                    }
                }
            }
            return false;
        }
    }
}
