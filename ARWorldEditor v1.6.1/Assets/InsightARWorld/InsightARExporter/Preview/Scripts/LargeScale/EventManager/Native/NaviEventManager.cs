using InsightAR.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviEventManager
{
    private const string TAG = "NaviEventManager";

    public static Action<string> onDestnationEvent;

    public static void SearchPoiInfoCallbackHandler(INavigationType type, string input)
    {
        InsightDebug.Log(TAG, "Native Search Poi Info Handler " + type + " " + input);
#if UNITY_EDITOR
        input = "{\"mapPoint\":{\"direction2DYaw\":0,\"realSpaceCoords\":[0.22967482,-2.7999999999999998,7.1925127199999999],\"isPass\":false,\"floorId\":\"1\",\"geographicCoords\":[120.22892177999999,30.24438237]},\"osmIdentifier\":\"-101333\",\"identifierMap\":\"\",\"propertiesSum\":15,\"properties\":{\"id\":\"ce03ddf57e05bf488762e6fec8b8a813\",\"x_content_id\":\"262\",\"type\":\"POI\",\"x_content_type\":\"ar_product\",\"x_content_radius\":\"2\",\"x_popular\":\"3\",\"direction\":\"10.8\",\"x_anchor\":\"yes\",\"name\":\"模型-默认算法\",\"height\":\"0.8\",\"x_preview_content_radius\":\"3\",\"level\":\"1\",\"x_name_radius\":\"5\"}}";
#endif

        GameSceneData.Instance.SetNaviPoiInput(input);
        GameSceneData.Instance.SetNavigationType(type);
        string geoId = "";
        //todo
        //传递给内容为geoId
        JObject jObject = JObject.Parse(input);
        if (jObject != null)
        {
            JObject jObjectContent = (JObject)jObject.SelectToken("properties");
            geoId = jObjectContent.SelectToken("id").ToString();
            if (!string.IsNullOrEmpty(geoId))
            {
                InsightDebug.Log(TAG, "geoid " + geoId);
                onDestnationEvent?.Invoke(geoId);
            }
        }
        else {
            InsightDebug.Log(TAG,  "poiinfo is wrong syntx" );
        }

        if (type == INavigationType.INavigationTypeNavi)
        {
            NavigationInterface.isNavigationState = true;
        }
        else
        {
            NavigationInterface.isNavigationState = false;
        }

        //进入导航状态
        LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_NAVIGATION);
    }

    public static void ClearListeners()
    {
        onDestnationEvent = null;
    }
}
