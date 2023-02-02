using System;
using ARWorldEditor;
using UnityEngine;

namespace ARWorldEditor{
    public class MapResources
    {
        public static void DownloadMaps(long mapId, Action<GetMapResourcesResponseData> onSuccess, Action<string,float> onProgress, Action<string,string> OnError)
        {

            InsightCacheManager.Instance.LoadCache();
            ARWorldEditor.NetDataFetchManager.Instance.GetMapResources(mapId, new OnOasisNetworkDataFetchCallback<GetMapResourcesResponseData>(
                (GetMapResourcesResponseData response) =>
                {
                    OnDownloadSuccess(mapId, response,onSuccess,onProgress,OnError);
                }, (string code, string msg) =>
                 {
                     OnDownloadFail(code, msg, OnError);
                 }));

        }

        private static void OnDownloadSuccess(long mapId, GetMapResourcesResponseData response, Action<GetMapResourcesResponseData> onSuccess
            ,Action<string,float> onProgress,Action<string,string> onError)
        {
            response.result.mapId = mapId;
            DownloadMapManager.Instance.DownloadMap(response.result, (string code, string msg) =>
              {
                  Debug.Log("download map error " + code + " " + msg);
                  onError?.Invoke(code,msg);
              }, (MapResourcesResultData map, ARWorldEditor.DownloadProductState downloadState) =>
               {
                  // Debug.Log("download map success " + map.mapId);
                   onSuccess?.Invoke(response);
               }, (string fileName,float progress) =>
              {
                  //Debug.Log("downloang progress " + progress);
                  onProgress?.Invoke(fileName, progress);
              });
        }

        private static void OnDownloadFail(string code,string msg,Action<string,string> onError)
        {
            Debug.Log("download map error " + code + msg);
            onError?.Invoke(code,msg);
        }
    }
}
