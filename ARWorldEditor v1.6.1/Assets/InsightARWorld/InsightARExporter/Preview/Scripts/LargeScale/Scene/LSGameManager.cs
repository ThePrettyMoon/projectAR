using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dongjian.LargeScale;
using InsightAR.Internal;
using System.Runtime.InteropServices;
using EZXR.NET;
using UnityEngine.SceneManagement;
using System.IO;

public class LSGameManager : MonoBehaviour
{
    #region params
    private const string TAG = "LSGameManager";
    private static bool sGameInited = false;
    private static LSGameManager _instance;
    public static LSGameManager Instance{
        get{return _instance;}
    }

    public delegate void OnApplicationPauseDelegate(bool paused);
    public event OnApplicationPauseDelegate onApplicationPausedEvent;

    // 存储场景信息
    private GameSceneData gameSceneData;

    private SceneEntity sceneEntity;

    public GameSceneData GetSceneData()
    {
        return gameSceneData;
    }

    public SceneEntity GetSceneEntity()
    {
        return sceneEntity;
    }

    #endregion

    #region unity functions

    private void Awake()
    {
        if (_instance == null){
            _instance = this;
        }else{
            GameObject.Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        //InitARScene();
        InitEventListeners();
    }

    private void Update()
    {
        //更新状态机
        if (sceneEntity != null && sceneEntity.GetSceneFsmSystem() != null)
        {
            sceneEntity.GetSceneFsmSystem().MachineUpdate();
        }
    }

    /// <summary>
    /// pause
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool paused)
    {
        if (onApplicationPausedEvent != null)
        {
//注意！！！
//iOS的生命周期里，从前台退到后台，从后台回到前台，后者会先执行，区别于Android平台
#if UNITY_IOS
            onApplicationPausedEvent?.Invoke(!paused);
#else
            onApplicationPausedEvent?.Invoke(paused);
#endif
        }
    }

    private void OnDestroy()
    {
        InsightDebug.Log(TAG, " On Destroy");
    }

    #endregion

    #region custom functions
    public void InitARScene()
    {
        //Debug.Log(sGameInited);

        if (sGameInited) return;
        sGameInited = true;

        if (gameSceneData == null)
        {
            gameSceneData = new GameSceneData();
        }

        if (sceneEntity == null)
        {
            sceneEntity = new SceneEntity();
            sceneEntity.InitFSM();
        }

        //InitEventListeners();

        //读取缓存数据
        InsightCacheManager.Instance.LoadCache();
        //纹理初始化
        TextureCache.Instance.Init();

        //启动lua框架
        //StartLuaFramework();
        DukTapeVMManager.Instance.Startup();

    }

    //private void StartLuaFramework()
    //{
    //    AppFacade.Instance.StartUp();
    //}

    public void ResetARScene() {
        ArProduct arProduct = GameSceneData.Instance.GetArProduct();
        ProductData productData = new ProductData();
        productData.SetSceneId(arProduct.Sid);
        productData.SetProductId(arProduct.Cid);
        productData.SetProduct(arProduct);
        productData.SetProductFileRoot(ContentResPaths.Instance.ResourcePath);
        SceneController.Instance.LoadScene(productData, true);
    }

    public void ChangeState(SceneStateID sceneId)
    {
        if (sceneEntity != null && sceneEntity.GetSceneFsmSystem() != null)
        {
            sceneEntity.GetSceneFsmSystem().ChangeState(sceneId);
        }
    }

    /// <summary>
    /// 返回当前状态
    /// </summary>
    /// <returns></returns>
    public SceneStateID GetCurrentState()
    {
        if (sceneEntity != null && sceneEntity.GetSceneFsmSystem() != null)
        {
            return (SceneStateID)sceneEntity.GetSceneFsmSystem().CurrentState().State();
        }
        return SceneStateID.EN_STATE_UNKNOWN;
    }

    //退出游戏
    public void Quit()
    {

        //退出通知
        EventManager.Instance.SendEventNotification(NotificationType.GAME_EXIT, null);
        ExitEventListeners();


        Resources.UnloadUnusedAssets();
        GC.Collect();

        //游戏退出
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    /// <summary>
    /// 退出定位和导航界面
    /// </summary>
    public void ExitARScene()
    {
        ClearListeners();

        sGameInited = false;

        SceneController.Instance.Close();

        //退出ar界面，添加这个，下次进入也会非常缓慢，可以保证
        //更新资源后，再次进入是最新的资源
        CustomResourceManager.Instance.UnloadResources();

        //停止AR
        InsightARManager.Instance.StopAR();

        gameSceneData = null;
        sceneEntity = null;



#if UNITY_ANDROID
        InsightAPPNative.SetAREvent(IAREventType.IAREventTypeExitMainContent);
        return;
#endif
        //关闭所有canvas
        Canvas[] allCanvas = GameObject.FindObjectsOfType<Canvas>();
        if (allCanvas.Length > 0) {
            foreach (Canvas c in allCanvas) {
                c.gameObject.SetActive(false);
            }
        }
        SceneLoadingManager.Instance.LoadSceneAsync("Loading", UnityEngine.SceneManagement.LoadSceneMode.Single, () =>
        {
            //ChangeState(SceneStateID.EN_STATE_LOADING);
            DukTapeVMManager.Instance.ShutDown();
            InsightAPPNative.SetAREvent(IAREventType.IAREventTypeExitMainContent);
        }, (progress) =>
        {
            InsightDebug.Log(TAG, "Loading scene progress: " + progress);
            InsightAPPNative.nbSetUnityLoadingProgress(progress, IARSceneLoadingType.IARSceneUnloadingType);
        });

    }

    public void InitEventListeners()
    {

        InsightAPPNative.navigationEvent += NaviEventManager.SearchPoiInfoCallbackHandler;
        InsightAPPNative.userEvent += UserEventManager.UserEventCallbackHandler;
    }

    public void ExitEventListeners()
    {

        InsightAPPNative.navigationEvent -= NaviEventManager.SearchPoiInfoCallbackHandler;
        InsightAPPNative.userEvent -= UserEventManager.UserEventCallbackHandler;
        InsightAPPNative.Instance.Close();

    }

    /// <summary>
    ///  清空监听
    /// </summary>
    public void ClearListeners()
    {
        NaviEventManager.ClearListeners();
        UserEventManager.ClearListeners();
    }
#endregion
}
