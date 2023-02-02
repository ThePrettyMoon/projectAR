using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InsightAR.Internal;
using System;
using System.IO;

public static class TrackDataManager
{
    private static readonly string sdkType = "unitysdk";
    public enum EventID
    {
        ar_load_start = 0,
        ar_load_success = 1,
        ar_load_failed = 2,
        ar_int_start = 4,
        ar_int_success = 8,
        ar_Cloudpositioning_time = 16
    };

    private static Dictionary<string, string> eventDic = new Dictionary<string, string>
    {
        {"ar_load_start","加载" },
        {"ar_load_success","加载"},
        {"ar_load_failed","加载"},
        {"ar_int_start","启动"},
        {"ar_int_success","初次云定位成功时间戳"},
        {"ar_Cloudpositioning_time","初次云定位时长"}
    };

    public static void SetTrackData(EventID eventID)
    {
        Debug.LogError("输入eventID为："+ eventID.ToString());
        string event_id = eventID.ToString();
        eventDic.TryGetValue(event_id, out string event_value);
        if (string.IsNullOrEmpty(event_value))
        {
            Debug.LogError("输入eventID为空");
            return;
        }
        string jsonStr = "{\"eventID\":\"" + event_id + "\",\"category\":\"" + event_value + "\",\"sdkType\":\"" + sdkType + "\"}";

        //开始加载埋点
        EventExtension.Happen(1, 1, 115, jsonStr);
    }
}
