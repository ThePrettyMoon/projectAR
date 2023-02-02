using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duktape;
using System;
using Dongjian.LargeScale;
using System.IO;
using InsightAR.Internal;

namespace Insight
{
    public class Navigation
    {

        public static DuktapeObject moduleObject_poiStatus;
        public static DuktapeObject funcObjcet_poiStatus;

        /// <summary>
        //注册OnLocationChanged方法，当Location改变时，回调该方法
        //Parameters
        //module_instance 脚本本身实例
        //func 回调的js方法
        //方法传入参数：string - Location信息，为json格式
        /// </summary>
        /// <param name="moduleInstance"></param>
        /// <param name="funcObjcet"></param>
        public static void OnLocationChanged(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            LocationInterface.onLocationChanged = null;
            LocationInterface.onLocationChanged += (string navidata) =>
            {
               // Debug.Log("user event to js location data: " + navidata);
                string locationJsonStr = navidata;
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr, locationJsonStr);
            };


        }

        /// <summary>
        /// 注册OnDestinationChanged方法，当Destination改变时，回调该方法
        /// </summary>
        /// <param name="moduleObject"></param>
        /// <param name="funcObjcet"></param>
        public static void OnDestinationChanged(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            NaviEventManager.onDestnationEvent = null;
            NaviEventManager.onDestnationEvent += (string geoId) =>
            {
                string destinationJsonStr = geoId;
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr, destinationJsonStr);
            };
        }

        /// <summary>
        /// 注册OnPathDataChanged方法，当PathData改变时，回调该方法
        /// </summary>
        /// <param name="moduleObject"></param>
        /// <param name="funcObjcet"></param>
        public static void OnPathDataChanged(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            NavigationInterface.onNavigationChanged = null;
            NavigationInterface.onNavigationChanged += (string navidata, int type) => {
                string pathDataJsonStr = navidata;
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr, pathDataJsonStr);
            };

        }

        /// <summary>
        /// 注册OnNavigationEnd方法，当通知NavigationEnd时，回调该方法
        /// </summary>
        /// <param name="moduleObject"></param>
        /// <param name="funcObjcet"></param>
        public static void OnNavigationEnd(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            UserEventManager.onUserExitNaviEvent = null;
            UserEventManager.onUserExitNaviEvent += () => {
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr);
            };
        }

        /// <summary>
        /// 注册OnPoiStatusDataChanged方法，当PoiStatusData改变时，回调该方法
        /// </summary>
        /// <param name="moduleObject"></param>
        /// <param name="funcObjcet"></param>
        public static void OnPoiStatusDataChanged(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            GameSceneData.Instance.onPOIStatusChanged = null;
            //GameSceneData.Instance.onPOIStatusChanged += (string poiStatusData) =>
            //{
            //    string poiStatusJsonStr = poiStatusData;
            //    IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
            //    DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr, poiStatusJsonStr);
            //};
            moduleObject_poiStatus = moduleObject;
            funcObjcet_poiStatus = funcObjcet;
        }

        /// <summary>
        /// 请求AR Local状态信息。返回值为number。
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static int QueryArProductLocalState(string pid)
        {
            ArProduct localProduct = InsightCacheManager.Instance.Query<ArProduct>(pid);

            //判断本地product是否为空
            if (localProduct == null) return 0;

            //判断本地是否存在对应文件
            string localPath = Path.Combine(ConstPath.RootDirectory(), localProduct.DownloadPath);
            if (!Directory.Exists(localPath)) return 0;

            //判断下载状态,如果没有解压完成，认为下载未完成
            if (localProduct.UnzipState != UnZipState.FINISHED) return 0;

            return 1;
        }

        /// <summary>
        /// 请求地图的所有poi列表，返回值为json格式。
        /// </summary>
        /// <returns></returns>
        public static string  QueryMapPoiList()
        {
            int NaviType = 1;
            return InsightARManager.Instance.QueryMapPoiList(NaviType);
        }

        /// <summary>
        /// 查询路径
        /// </summary>
        public static string QueryNaviPath(string input, int naviType)
        {
            return InsightARManager.Instance.QueryNavigationPath(input, naviType);
        }

        public static string GetLightEstimate()
        {
            bool isRes = NavigatorNative.isLightEstimateEnabled();
            NavigatorNative.InsightARLightEstimateResult res;
            if (isRes)
            {
                res = NavigatorNative.iarGetLightEstimate();
                Debug.LogFormat("IARAPI-Light intensity: {0}  tempa: {1} \n", res.illuminIntensity, res.illuminTemperature);
                return JsonUtil.Serialize(res);
            }
            return null;
        }
    }
}
