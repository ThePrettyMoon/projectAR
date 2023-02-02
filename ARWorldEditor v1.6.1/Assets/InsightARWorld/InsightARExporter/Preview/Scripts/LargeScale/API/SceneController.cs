using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InsightAR.Internal;
using System;
using System.IO;

/// <summary>
/// 场景管理
/// </summary>
public class SceneController : Singleton<SceneController>
{
    #region params
    private const string TAG = "SceneController";
    private List<ProductScene> sceneList = new List<ProductScene>();

    public Action loadSceneSuccess;
    #endregion




    #region ls_sdk

    private List<LsProductSceneManager> lsProductScenes = new List<LsProductSceneManager>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cid"></param>
    /// <param name="path">场景文件路径</param>
    /// <param name="name">标记场景类型，poi表示子事件，其他为主场景</param>
    /// <param name="loadMode"></param>
    public void lsLoadScene(string cid, string path, string name = "", string sid = "0") {
        
        if (!Directory.Exists(path) || string.IsNullOrEmpty(path)){
            InsightDebug.LogError(TAG, path);
            return;
        }
        //开始加载埋点
        TrackDataManager.SetTrackData(TrackDataManager.EventID.ar_load_start);
        //解析场景
        LsProductSceneManager productScene = new LsProductSceneManager(cid, sid, path);
        string sceneName = productScene.sceneName;
        string unity3dPath = productScene.unity3dPath;

        LsProductSceneManager ps;


        

        switch (name.ToUpper()) {
            case "POI":
                //？？为什么是根据算法进行挂起或者叠加的判断
                string attachAlgPath = productScene.materialPaths.Path_Type_Alg;
                InsightAttachType attachType = InsightARManager.Instance.GetARAttach().StartAttachAR(attachAlgPath, attachAlgPath + "/assets");
                InsightDebug.Log(TAG, "attachAlgPath: " + attachAlgPath);
               
                productScene.attachType = attachType;
                ps = lsProductScenes.Find(p => p.cid == cid);
                if (ps != null) { lsProductScenes.Remove(ps); }
                lsProductScenes.Add(productScene);
                InsightDebug.Log(TAG, "poi attachType： " + attachType);
#if UNITY_EDITOR
                attachType = InsightAttachType.ATTACH_TYPE_CHANGE;
#endif

                if (attachType == InsightAttachType.ATTACH_TYPE_CHANGE) {
                    LoadSceneAsyncAction(unity3dPath, sceneName, LoadSceneMode.Single, () => {               
                        //回调内容加载成功
                        InsightAPPNative.nbSetUnityLoadingCompleted(1, IARSceneLoadingType.IARSceneLoadingTypeFullMask);
                        productScene.sceneParser.ParseGameObjectComponents();
                        productScene.sceneModel = LSProductSceneModel.SCENE_MODEL_POI_ATTACH;
                        InsightARManager.Instance.ResetLandmarkerStatus();
                        ReConfigARManager();
                    }, (progress) => {
                        InsightDebug.Log(TAG, "Loading scene progress: " + progress);
                        InsightAPPNative.nbSetUnityLoadingProgress(progress, IARSceneLoadingType.IARSceneLoadingTypeFullMask);
                    }, false);
                }
                else if (attachType == InsightAttachType.ATTACH_TYPE_NO_CHANGE || attachType == InsightAttachType.ATTACH_TYPE_ADD)
                {
                    LoadSceneAsyncAction(unity3dPath, sceneName, LoadSceneMode.Additive, () => {
                        InsightAPPNative.nbSetUnityLoadingCompleted(1, IARSceneLoadingType.IARSceneLoadingTypeFullMask);
                        productScene.sceneParser.ParseGameObjectComponents();
                        productScene.sceneModel = LSProductSceneModel.SCENE_MODEL_POI_ADD_OR_NONE;
                    }, (progress) => {
                        InsightDebug.Log(TAG, "Loading scene progress: " + progress);
                        InsightAPPNative.nbSetUnityLoadingProgress(progress, IARSceneLoadingType.IARSceneLoadingTypeFullMask);
                    }, false);
                }
                else if (attachType == InsightAttachType.ATTACH_TYPE_NONE)
                {
                    return;
                }

                break;
            default:
                productScene.sceneModel = LSProductSceneModel.SCENE_MODEL_LANMARK;
                ps = lsProductScenes.Find(p => p.cid == cid);
                if (ps != null) { lsProductScenes.Remove(ps); }
                lsProductScenes.Add(productScene);

                //缓存场景数据
                GameSceneData.Instance.SetContentId(cid);
                GameSceneData.Instance.SetContentPath(productScene.materialPaths.Path_Type_Scene);

                LoadSceneAsyncAction(unity3dPath, sceneName, LoadSceneMode.Single, () =>
                {

                    //回调内容加载成功
                    InsightAPPNative.SetAREvent(IAREventType.IAREventTypeLoadMainContentSuccess);
                    InsightAPPNative.nbSetUnityLoadingCompleted(1, IARSceneLoadingType.IARSceneLoadingTypeFullScreen);
                    InsightDebug.Log(TAG, "IAREventTypeLoadMainContentSuccess");

                    //test preload
                    //GameObject ooo = GameObject.Find("TrackingStatusText");
                    //if (ooo)
                    //{
                    //    var t = ooo.GetComponent<Text>();
                    //    t.text = "first preloading testing ";
                    //    t.color = Color.red;
                    //    ooo.transform.localScale = new Vector3(2, 2, 2);
                    //    Debug.LogError("contains a main camera");
                    //}
                    //else
                    //{
                    //    Debug.LogError("does not contain a main camera");
                    //}

                    productScene.sceneParser.ParseGameObjectComponents();

                    ConfigARManager(productScene.materialPaths.Path_Type_Alg, false);

                    loadSceneSuccess?.Invoke();

                }, (progress) =>
                {
                    InsightDebug.Log(TAG, "Loading scene progress: " + progress);
                    InsightAPPNative.nbSetUnityLoadingProgress(progress, IARSceneLoadingType.IARSceneLoadingTypeFullScreen);
                },false, true);
                break;
        }
    }

    private void LoadSceneAsyncAction(string scenePath, string sceneName, LoadSceneMode sceneMode, 
        Action onSccuss, Action<float> onProgress, bool loading = false, bool islandmark = false) {

        CustomResourceManager.Instance.LoadLevelAsync(scenePath, (UnityEngine.Object[] objs, string str) =>
        {
            SceneLoadingManager.Instance.LoadSceneAsync(sceneName, sceneMode, () =>
            {
                onSccuss?.Invoke();
                //加载成功埋点
                TrackDataManager.SetTrackData(TrackDataManager.EventID.ar_load_success);
            },
            (progress) =>
            {
                onProgress?.Invoke(progress);
            }, loading, islandmark);
        }, (string error) => {
            UnityEngine.Debug.LogError(error);
            //加载子事件失败，回撤操作
            NotifyNativeMessage.MakeToast("加载失败！");
            //加载失败埋点
            TrackDataManager.SetTrackData(TrackDataManager.EventID.ar_load_failed);
            //回调内容加载失败
            InsightAPPNative.SetAREvent(IAREventType.IAREventTypeLoadMainContentError);
        });
    }

    [Obsolete]
    public void lsUnloadPoiScene(string poiCid) {
        if (lsProductScenes.Count > 0) {
            LsProductSceneManager mainScene = lsProductScenes.Find(p => p.sceneModel == LSProductSceneModel.SCENE_MODEL_LANMARK);
            LsProductSceneManager poiScene = lsProductScenes.Find(p => p.cid == poiCid);

            if (mainScene.cid == poiCid)
            {

            }

            if (mainScene != null && poiScene != null)
            {
                //Debug.Log(mainScene.sceneName);
                //Debug.Log(poiScene.sceneName);

                //停止attach
                InsightARManager.Instance.GetARAttach().StopAttachAR();
                //清理数据
                InsightARManager.Instance.ClearData();
                Debug.Log(poiScene.attachType);

                if (poiScene.attachType == InsightAttachType.ATTACH_TYPE_CHANGE)
                {
                    LoadSceneAsyncAction(mainScene.unity3dPath, mainScene.sceneName, LoadSceneMode.Single, () =>
                    {
                        //回调内容加载成功
                        InsightAPPNative.SetAREvent(IAREventType.IAREventTypeLoadMainContentSuccess);
                        InsightAPPNative.nbSetUnityLoadingCompleted(1, IARSceneLoadingType.IARSceneLoadingTypeFullMask);
                        InsightDebug.Log(TAG, "IAREventTypeLoadMainContentSuccess");

                        mainScene.sceneParser.ParseGameObjectComponents();
                        InsightARManager.Instance.ResetLandmarkerStatus();

                        ReConfigARManager();

                        loadSceneSuccess?.Invoke();

                    }, (progress) =>
                    {
                        InsightDebug.Log(TAG, "Loading scene progress: " + progress);
                        InsightAPPNative.nbSetUnityLoadingProgress(progress, IARSceneLoadingType.IARSceneLoadingTypeFullMask);
                    });
                }
                else
                {
                    //string sceneName = poiScene.sceneName;
                    SceneLoadingManager.Instance.UnloadSceneAsync(poiScene.sceneName);
                }
                //poiScene.sceneModel = LSProductSceneModel.SCENE_NONE;
                lsProductScenes.Remove(poiScene);
            }
        }
    }

#endregion

#region custom functions

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneId"></param>
    /// <param name="arProduct"></param>
    /// //1 是室内 2室外
    /// <param name="resType"></param>
    public void LoadScene(ProductData productData,bool resetAR = false)
    {
        if (productData == null) {
            InsightDebug.LogError(TAG, "productData is null!");
            return; 
        }
        ProductScene productScene = new ProductScene(productData.GetProduct(), productData.GetProductFileRoot(),
            ProductSceneMode.PRODUCT_SCENE_MODE_MAIN);

        if (productScene == null) {
            InsightDebug.LogError(TAG, "productscene is null!");
            InsightDebug.LogError(TAG, productData.GetProductFileRoot());

            return; 
        }

        //添加loading界面
        LoadSceneAsync(productScene, LoadSceneMode.Single, () =>
         {
             if (productScene.GetMode() == ProductSceneMode.PRODUCT_SCENE_MODE_MAIN) {
                 //回调内容加载成功
                 InsightAPPNative.SetAREvent(IAREventType.IAREventTypeLoadMainContentSuccess);
                 InsightDebug.Log(TAG, "IAREventTypeLoadMainContentSuccess");
             }

             LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_LOCATION);

             ConfigARManager(productScene,resetAR);

             loadSceneSuccess?.Invoke();

         }, true);

        //add lua/js search path
        //AddSearchPath(productScene);

        ProductScene pScene = sceneList.Find(p => p.GetProductId() == productScene.GetProductId());
        if (pScene == null)
        {
            sceneList.Add(productScene);
        }
        else
        {
            sceneList.Remove(pScene);
            sceneList.Add(productScene);
        }
    }

    /// <summary>
    /// 加载poi场景
    /// </summary>
    /// <param name="arProduct"></param>
    public void LoadPoiScene(ProductData productData)
    {
        ArProduct arProduct = productData.GetProduct();
        Debug.Log("Unity Call Load Poi Scene " + arProduct.Cid);
        ProductScene productScene = new ProductScene(arProduct, productData.GetProductFileRoot(), ProductSceneMode.PRODUCT_SCENE_MODE_POI);
        if (productScene == null) return;
        string attachAlgPath = productScene.GetSceneModel().GetAlgPath();
        InsightAttachType attachType = InsightARManager.Instance.GetARAttach().StartAttachAR(attachAlgPath,
            attachAlgPath + "/assets");
        InsightDebug.Log(TAG, "attachAlgPath: " + attachAlgPath);
        productScene.SetAttachType(attachType);

        InsightDebug.Log(TAG, "Load Poi Scene " + attachType);

        //切换场景
        if (attachType == InsightAttachType.ATTACH_TYPE_CHANGE)
        {
            //导航状态，需要关闭导航
            //SceneStateID sceneState = LSGameManager.Instance.GetCurrentState();
            //if (sceneState == SceneStateID.EN_STATE_NAVIGATION)
            //{
            //    InsightAPPNative.ExitNavigation();
            //}
            
            InsightAPPNative.SetAREvent(IAREventType.IAREventTypeSceneSuspendEnter);
            //隐藏地图
            NotifyNativeMessage.SetMapVisibility(0);
            //清理POI STATE
            GameSceneData.Instance.ClearPOISenceData();
            LSGameManager.Instance.ClearListeners();
            //挂起子事件，暂不保存POI状态
            LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_POI);
            LoadSceneAsync(productScene, LoadSceneMode.Single, () =>
             {
                 ReConfigARManager();
             });

        }
        else if (attachType == InsightAttachType.ATTACH_TYPE_NO_CHANGE || attachType == InsightAttachType.ATTACH_TYPE_ADD)
        {
            LoadSceneAsync(productScene, LoadSceneMode.Additive);
        }
        else if (attachType == InsightAttachType.ATTACH_TYPE_NONE)
        {
            return;
        }
        sceneList.Add(productScene);
    }

    /// <summary>
    /// 卸载poi场景
    /// </summary>
    public void UnloadPoiScene()
    {
        if (sceneList == null || sceneList.Count == 0) return;

        ProductScene mainScene = sceneList.Find(p => p.GetMode() == ProductSceneMode.PRODUCT_SCENE_MODE_MAIN);
        ProductScene poiScene = sceneList.Find(p => p.GetMode() == ProductSceneMode.PRODUCT_SCENE_MODE_POI);
        if (poiScene != null)
        {
            //停止attach
            InsightARManager.Instance.GetARAttach().StopAttachAR();
            //清理数据
            InsightARManager.Instance.ClearData();

            InsightAttachType attachType = poiScene.GetAttachType();
            if (attachType == InsightAttachType.ATTACH_TYPE_CHANGE)
            {
                LoadSceneAsync(mainScene, LoadSceneMode.Single, () =>
                 {
                     InsightAPPNative.SetAREvent(IAREventType.IAREventTypeSceneSuspendExit);
                     if (NavigationInterface.isNavigationState)
                     {
                         ////进入导航模式，如果没有导航模式，则进入定位模式
                         LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_NAVIGATION);
                     }
                     else {
                         ////进入导航模式，如果没有导航模式，则进入定位模式
                         LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_LOCATION);
                     }
                     ReConfigARManager();
                 }); 
            }
            else
            {
                UnloadSceneAsync(poiScene);
            }
            sceneList.Remove(poiScene);
        }

    }

    /// <summary>
    /// 清空处理
    /// </summary>
    public void Close()
    {
        lsProductScenes.Clear();
        sceneList.Clear();
        //清理一下gamescene data
        GameSceneData.Instance.Clear();
        //GeoDataController.Clear();
    }

    /// <summary>
    ///  加载场景
    /// </summary>
    /// <param name="productScene"></param>
    private void LoadSceneAsync(ProductScene productScene, LoadSceneMode mode, Action callBack = null,
        bool addLoading = false)
    {
        string sceneResourcePath = productScene.GetSceneModel().GetSceneResoucePath();
        string sceneName = productScene.GetSceneModel().GetSceneName();

        if (!File.Exists(sceneResourcePath) || string.IsNullOrEmpty(sceneResourcePath))
        {
            InsightDebug.LogError(TAG, "scene resource path does not exit!");
            return;
        }
        // add loading view
        //if (addLoading) SceneLoadingManager.Instance.LoadScene("Loading");
        sceneResourcePath = sceneResourcePath.Replace("\\", "/");
        Debug.Log(sceneResourcePath);
        CustomResourceManager.Instance.LoadLevelAsync(sceneResourcePath, 
            (UnityEngine.Object[] objs, string str) =>
        {
            Debug.Log(sceneName);
            SceneLoadingManager.Instance.LoadSceneAsync(sceneName, mode, () =>
            {
                productScene.GetSceneModel().GetSceneParse().ParseGameObjectComponents();
                if (callBack != null)
                {
                    callBack();
                }
            },
            (progress) => {
                InsightDebug.Log(TAG, "Loading scene progress: " + progress);
            });
        }, (string error)=> {
            UnityEngine.Debug.LogError(error);
            //加载子事件失败，回撤操作
            NotifyNativeMessage.MakeToast("加载失败！");
        });
    }

    /// <summary>
    ///  卸载场景
    /// </summary>
    /// <param name="productScene"></param>
    private void UnloadSceneAsync(ProductScene productScene)
    {
        if (productScene == null) return;
        string sceneName = productScene.GetSceneModel().GetSceneName();
        SceneLoadingManager.Instance.UnloadSceneAsync(sceneName);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productScene"></param>
    private void AddSearchPath(ProductScene productScene)
    {
        // remove search path
        //LuaManager luaManager = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua);
        //if (luaManager != null)
        //{
        //    luaManager.AddSearchPath(productScene.GetSceneModel().GetScenePath());
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productScene"></param>
    private void RemoveSearchPath(ProductScene productScene)
    {
        // remove search path
        //LuaManager luaManager = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua);
        //if (luaManager != null)
        //{
        //    luaManager.RemoveSearchPath(productScene.GetSceneModel().GetScenePath());
        //}
    }

    /// <summary>
    /// 开启ar
    /// </summary>
    private void ConfigARManager(ProductScene productScene,bool resetAR = false)
    {
        GameObject camGO = GameObject.Find("Main Camera");
        if (camGO == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null) return;
            camGO = mainCamera.gameObject;
        }
        if (camGO == null)
        {
            camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            camGO.AddComponent<Camera>();
        }
        InsightARCamera arCamera = camGO.AddComponent<InsightARCamera>();
        InsightARManager arManager = InsightARManager.Instance;
        string algPath = productScene.GetSceneModel().GetAlgPath();
        if (resetAR)
        {
            arManager.SetUpCamera(arCamera);
            arManager.ResetAR(algPath, arCamera);
        }
        else
        {
            arManager.Init(algPath, arCamera);
        }
    }

    private void ConfigARManager(string algPath, bool resetAR = false)
    {
        GameObject camGO = GameObject.Find("Main Camera");
        if (camGO == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null) return;
            camGO = mainCamera.gameObject;
        }
        if (camGO == null)
        {
            camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            camGO.AddComponent<Camera>();
        }
        InsightARCamera arCamera = camGO.AddComponent<InsightARCamera>();
        InsightARManager arManager = InsightARManager.Instance;
        //string algPath = productScene.GetSceneModel().GetAlgPath();
        if (resetAR)
        {
            arManager.SetUpCamera(arCamera);
            arManager.ResetAR(algPath, arCamera);
        }
        else
        {
            arManager.Init(algPath, arCamera);
        }
    }

    /// <summary>
    /// 更新主相机
    /// </summary>
    private void ReConfigARManager()
    {
        GameObject camGO = GameObject.Find("Main Camera");
        if (camGO == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null) return;
            camGO = mainCamera.gameObject;
        }
        if (camGO == null)
        {
            camGO = new GameObject("Main Camera");
            camGO.tag = "MainCamera";
            camGO.AddComponent<Camera>();
        }
        InsightARCamera arCamera = camGO.AddComponent<InsightARCamera>();
        InsightARManager.Instance.SetUpCamera(arCamera);
    }

#endregion

}
