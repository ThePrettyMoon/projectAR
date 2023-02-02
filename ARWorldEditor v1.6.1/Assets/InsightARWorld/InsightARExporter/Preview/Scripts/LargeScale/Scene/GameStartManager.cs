using AOT;
using InsightAR.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStartManager : MonoBehaviour
{
    private static string TAG = "GameStartManager";

    private static GameStartManager _instance;

    public static GameStartManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        //InsightDebug.Log(TAG," Awake");


    }

    private void Start() {

   

    }

    private void OnEnable()
    {
        //InsightDebug.Log(TAG," OnEnable");
        InsightAPPNative.arLocalPathEvent += ContentStartManager.lsContentLoaderBridge;
        InsightAPPNative.Instance.Init();

        LsHttpNetWorkWithNative.Instance.Init();    //注册SDK层的网络回调
        LsDownloadWithNative.Instance.Init();
    }

    private void Update()
    {
        //InsightAPPNative.Instance.UpdateARResourceLocalPath();
    }

    private void OnDisable()
    {
        //InsightDebug.Log(TAG," OnDisable");
        InsightAPPNative.arLocalPathEvent -= ContentStartManager.lsContentLoaderBridge;
        LsHttpNetWorkWithNative.Instance.Close();
        LsDownloadWithNative.Instance.Close();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (GUI.Button(new Rect(400, 100, 100, 100), "startar")){
            InsightDebug.Log(TAG, "StartAR");

            var scenePath = Application.dataPath.Replace(@"/Assets", @"/EditorProductor/223");
            var sceneName = "Art_working_Bake_ios.unity3d";
            //var localPath = "{\"localPath\":\"" + scenePath + "\",\"sceneName\":\"" + sceneName + "\",\"cid\":\"" + "223" + "\",\"loadMode\":\"0\"}";
            var localPath = "{" +
                "\"localPath\":\"" + scenePath + "\"," +
                "\"loadMode\":\"0\"," +
                "\"cid\":\"" + "223" + "\"," +
                "\"sceneName\":\"" + sceneName + "\"," +
                "\"content\":{" +
                    "\"promptBeforeDownload\":false," +
                    "\"province\":\"浙江省\"," +
                    "\"centroid\":{\"latitude\":30.244357977580211,\"longitude\":120.22881912288413}," +
                    "\"orientation\":1," +
                    "\"cid\":631," +
                    "\"coverImageUrl\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/b1264196b12479633ce18735b8dca766.png\"," +
                    "\"address\":\"钱江世纪公园C6\"," +
                    "\"materials\":[{\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20210728164109035_t0iloz2N.zip\",\"md5\":\"364dd480f0e9741cb178512902a1c5f9\",\"type\":10001,\"size\":10797040,\"mid\":7078},{\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20210719140439782_1Ta7xqFq.zip\",\"md5\":\"ab5905b95fa7dc855f97a17cf8b149a2\",\"type\":10002,\"size\":15827884,\"mid\":6954},{\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20210125173646559_2H4NQ3Wp.zip\",\"md5\":\"49a2327f58452c2692e7f75ef07ac097\",\"type\":10003,\"size\":14587887,\"mid\":1357},{\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/69e39551-81f7-4743-8a42-e5b9ca7bc2f8.zip\",\"md5\":\"6ac5df323ae115d580af79864dec4620\",\"type\":10008,\"size\":30829,\"mid\":6449},{\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20210518202426923_vSyVVVJV.zip\",\"md5\":\"5e64bbcf4750643eae023689f32f5f32\",\"type\":10009,\"size\":61010,\"mid\":5143}]," +
                    "\"totalSize\":41304650," +
                    "\"sid\":2284," +
                    "\"name\":\"导航主场景\"," +
                    "\"distance\":-1," +
                    "\"cloudRelocUrl\":\"https:\\/\\/reloc-gw.easexr.com\\/api\\/alg\\/cloud\\/aw\\/reloc\\/proxy?routeApp=parkc&map=c6\"," +
                    "\"sarPid\":0," +
                    "\"tags\":[]," +
                    "\"district\":\"萧山区\"," +
                    "\"navEnabled\":true," +
                    "\"aro_path_type\":0," +
                    "\"updateTime\":1627560763000," +
                    "\"city\":\"杭州市\"}" +
                "}";
            InsightAPPNative.ARContentStartCallback(localPath);

        }

        if (GUI.Button(new Rect(400, 200, 100, 100), "exitar"))
        {
            LSGameManager.Instance.ExitARScene();
            InsightDebug.Log(TAG, "ExitAR");
        }

        if (GUI.Button(new Rect(100, 400, 100, 100), "ClickPOI"))
        {
            NaviEventManager.SearchPoiInfoCallbackHandler(INavigationType.INavigationTypeNavi, "");
            InsightDebug.Log(TAG, "ClickPOI");
        }

        if (GUI.Button(new Rect(100, 500, 100, 100), "Location"))
        {
            NaviSceneManager.onNavi2DPoseUpdate(IntPtr.Zero, IntPtr.Zero);
            InsightDebug.Log(TAG, "Location");
        }

        if (GUI.Button(new Rect(100, 600, 100, 100), "Navigation"))
        {
            NaviSceneManager.onNaviUpdate(IntPtr.Zero, 0, IntPtr.Zero);
            InsightDebug.Log(TAG, "Navigation");
        }

        if (GUI.Button(new Rect(100, 800, 100, 100), "LoadPOI"))
        {
            //EventExtension.Happen(1, 1, 10008, "");
            //InsightDebug.Log(TAG, "LoadPOI");

            var scenePath = Application.dataPath.Replace(@"/Assets", @"/EditorDebug/Product/434");
            var sceneName = "POI";
            var localPath = "{\"localPath\":\"" + scenePath + "\",\"sceneName\":\"" + sceneName + "\",\"cid\":\"" + "434" + "\",\"loadMode\":\"0\"}";

            InsightAPPNative.ARContentStartCallback(localPath);
        }

        if (GUI.Button(new Rect(100, 900, 100, 100), "UnloadPOI"))
        {
            EventExtension.Happen(1, 1, 10009, "");
            InsightDebug.Log(TAG, "UnloadPOI");
        }

        if (GUI.Button(new Rect(100, 1100, 100, 100), "StartRecord"))
        {
            UserEventManager.UserEventCallbackHandler(IUserEventType.IUserEventTypeRecordStart);
            InsightDebug.Log(TAG, "StartRecord");
        }
        if (GUI.Button(new Rect(200, 1100, 100, 100), "ExitRecord"))
        {
            UserEventManager.UserEventCallbackHandler(IUserEventType.IUserEventTypeRecordStop);
            InsightDebug.Log(TAG, "ExitRecord");
        }

        if (GUI.Button(new Rect(100, 1200, 100, 100), "TakePhoto"))
        {
            UserEventManager.UserEventCallbackHandler(IUserEventType.IUserEventTypeTakePhoto);
            InsightDebug.Log(TAG, "TakePhoto");
        }
    }
#endif
}
