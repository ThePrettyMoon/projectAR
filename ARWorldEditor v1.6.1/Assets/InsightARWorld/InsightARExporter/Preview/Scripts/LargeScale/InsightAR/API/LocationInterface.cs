using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.IO;
using InsightAR.Internal;
using AOT;

public class LocationInterface
{
    public const string TAG = "LocationInterface";
    private static LocationInterface m_Interface;
    public static LocationInterface GetInstance()
    {
        if (m_Interface == null) { m_Interface = new LocationInterface(); }
        return m_Interface;
    }

    public static Action<string> onLocationChanged;
    public static Action<Navi2DPoseCallbackData> navi2DPoseCallbackAction;
    private static List<Navi2DPoseCallbackData> navi2DPoseCallbackDatas;

    private static bool LocationState = false;
    private static bool NaviInitedState;


    public static bool GetLocationState() {
        return LocationState;
    }
    public static void EnterLocation() {

        LocationState = true;
        NaviInitedState = false;

        navi2DPoseCallbackAction += OnNavi2DPoseCallback;

        navi2DPoseCallbackDatas = new List<Navi2DPoseCallbackData>();

        InsightDebug.Log(TAG, " enter location");

    }

    public static bool InitAllNavis() {

        if (!GameSceneData.Instance.GetNaviEnabled()) {
            return false;
        }

        string naviRoot = ContentResPaths.Instance.GetNaviRoot();
        string geoRoot = ContentResPaths.Instance.GetGeoRoot();

        InsightDebug.Log(TAG, NaviSceneManager.Instance.CheckNavigatorStatus().ToString());
        InsightDebug.Log(TAG, ContentResPaths.Instance.GetNaviRoot());
        InsightDebug.Log(TAG, ContentResPaths.Instance.GetGeoRoot());

        if (!(Directory.Exists(naviRoot) && Directory.Exists(geoRoot)))
        {
            InsightDebug.LogError(TAG, "navifile does not exist: " + naviRoot);
            InsightDebug.LogError(TAG, "geofile does not exist: " + geoRoot);
            return false;
        }

        NaviSceneManager.Instance.InitNavi(
               ContentResPaths.Instance.GetNaviRoot(),
               ContentResPaths.Instance.GetGeoRoot()
               );
        NaviSceneManager.Instance.SetNaviCallback();
        NaviSceneManager.Instance.SetNavi2DPoseCallback();
        NaviSceneManager.Instance.ConvertPose(null, null);

        return true;
    }

    public static void UpdatePoiInfoList() {

        //set visiblity
        if (GameSceneData.Instance.GetNaviEnabled())
        {
            //set poi lists
            int NaviType = 1;
            string poiInfos = InsightARManager.Instance.QueryMapPoiList(NaviType);
            GameSceneData.Instance.SetPoiInfos(poiInfos);

            NotifyNativeMessage.SetPoiList(GameSceneData.Instance.GetPoiInfos());
            //init visible
            NotifyNativeMessage.SetMapVisibility(1);
        }

    }

    public static void UpdateLocation() {

        if (LocationState == false)
            return;

        if (NaviInitedState == false)
        {
            if (!GameSceneData.Instance.GetNaviEnabled())
            {
                return;
            }
            //Debug.Log("CheckNavigatorStatus: " + NaviSceneManager.Instance.CheckNavigatorStatus());
            //if (NaviSceneManager.Instance.CheckNavigatorStatus() == 0)
            //{
            //    if (InitAllNavis()) {
            //        NaviInitedState = true;
            //        UpdatePoiInfoList();
            //        InsightDebug.Log(TAG, "NaviInitedState: " + NaviInitedState);
            //    }
            //}
            //else {
            //    return;
            //}
        }

        updateNavi2DPoseCallbacks();

    }

    public static void EndLocation() {

        LocationState = false;

        onLocationChanged = null;

        navi2DPoseCallbackAction -= OnNavi2DPoseCallback;
 

        if (navi2DPoseCallbackDatas != null)
            navi2DPoseCallbackDatas.Clear();

    }

    public static void AddNavi2DPoseCallback(string posedata) {
#if UNITY_EDITOR
        OnNavi2DPoseCallback(new Navi2DPoseCallbackData
        {
            navidata = "{\"osmId\":0,\"floorId\":\"1\",\"cameraState\":0,\"timestamp\":\"0\",\"geoCoordsPosition\":[120.22884721, 30.24438370],\"geoCoordsAngle\":269.16363525}"
        }); ;
        return;
#else
        navi2DPoseCallbackDatas.Add(new Navi2DPoseCallbackData
        {
            navidata = posedata
        });
#endif
    }

    private static void OnNavi2DPoseCallback(Navi2DPoseCallbackData navi2DPoseCallbackData)
    {
        var data = navi2DPoseCallbackData.navidata;
        //Debug.Log("navidata: " + data);
        //update to native
        InsightAPPNative.SetCurrentLocation(data);
        //expose to content
        onLocationChanged?.Invoke(data);
    }

    private static void updateNavi2DPoseCallbacks()
    {
        var length = navi2DPoseCallbackDatas.Count;
        for (int i = 0; i < length; i++)
        {
            Navi2DPoseCallbackData navi2DPoseCallbackData = navi2DPoseCallbackDatas[i];
            if (navi2DPoseCallbackAction != null)
                navi2DPoseCallbackAction(navi2DPoseCallbackData);
        }
        navi2DPoseCallbackDatas.Clear();

    }
}
