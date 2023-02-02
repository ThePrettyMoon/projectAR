using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoadingManager:UnitySingleton<SceneLoadingManager>
{
    private const string TAG = "SceneLoadingManager";
    private AsyncOperation loadingOperation;    //异步加载进程，抽离处理以能获取加载进度
    private Action<float> loadingProgressAction;    //回调加载进度
    private float lastProgress; //记录加载进度
    private float currProgress; //当前加载进度

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ClearLoadingScene();

    }

    public void Update()
    {
        UpdateLoadingScene();
    }

    public  void LoadScene(string name,LoadSceneMode mode = LoadSceneMode.Single,Action callBack = null)
    {
        SceneManager.LoadScene(name, mode);
        callBack?.Invoke();
    }

    [Obsolete]
    /// <summary>
    /// unload
    /// </summary>
    /// <param name="name"></param>
    public void UnloadScene(string name)
    {
        var scenes = SceneManager.GetAllScenes();
        for(int i = 0; i < scenes.Length; i++)
        {
            if(scenes[i].name == name)
            {
                SceneManager.UnloadScene(scenes[i]);
            }
        }
    }

    [Obsolete]
    public void UnloadSceneAsync(string name)
    {
        //var scenes = SceneManager.GetAllScenes();
        //for (int i = 0; i < scenes.Length; i++)
        //{
        //    Debug.Log("scene: " + scenes[i].name);
        //    if (scenes[i].name == name)
        //    {
        //        SceneManager.UnloadSceneAsync(scenes[i]);
        //    }
        //}
        var scene = SceneManager.GetSceneByName(name);
        if(scene.isLoaded)
            SceneManager.UnloadSceneAsync(scene);
    }

    public void LoadSceneAsync(string name, LoadSceneMode mode = LoadSceneMode.Single, Action callBack = null, Action<float> progressCallback = null, bool addLoading = false, bool isLandmark = false)
    {
        StartCoroutine(LoadSceneCoroutine(name, mode, callBack, progressCallback, addLoading, isLandmark));
    }

    public  void LoadSceneAsync(string name,LoadSceneMode mode = LoadSceneMode.Single, Action callBack = null, Action<float> progressCallback = null)
    {
       LoadSceneAsync(name, mode, callBack, progressCallback, false);
    }

    IEnumerator LoadSceneCoroutine(string name ,LoadSceneMode mode ,Action callBack =null, Action<float> progressCallback = null, bool addLoading = false, bool isLandmark = false)
    {
        if (addLoading)
        {
            if (SceneManager.GetActiveScene().name != "Loading") {
                var loadasync = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Single);
                loadasync.allowSceneActivation = true;
                yield return loadasync;
            }
        }

        Scene scene = SceneManager.GetSceneByName(name);
        if (scene.IsValid()) {
            InsightDebug.Log(TAG, "unloadsceneasync: " + name);
            if (SceneManager.GetActiveScene().name == name) {
                InsightDebug.Log(TAG, "activescenename: " + name);
            }
            yield return SceneManager.UnloadSceneAsync(name);
        }
        InsightDebug.Log(TAG, "is loading a landmark: " + isLandmark);
        if (isLandmark)
        {
            yield return new WaitForSeconds(0.5f); //等待duktape加载完成
        }

        loadingOperation = SceneManager.LoadSceneAsync(name, mode);
        //AsyncOperation operation =  SceneManager.LoadSceneAsync(name, mode);
        loadingProgressAction = progressCallback;
        loadingOperation.allowSceneActivation = false;   
        yield return loadingOperation;
        callBack?.Invoke();
        //progressCallback?.Invoke(loadingOperation.progress);
        yield return null;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        InsightDebug.Log(TAG, "Load Scene: " + scene.name + "___mode: " + mode);
    }

    /// <summary>
    /// 每帧更新
    /// 回调loadingscene 进度
    /// </summary>
    private void UpdateLoadingScene() 
    {
        if (loadingProgressAction != null && loadingOperation != null)
        {
            currProgress = loadingOperation.progress;
            currProgress = currProgress >= 0.9f ? 1 : currProgress;
            var prog = Mathf.Lerp(lastProgress, currProgress, Time.deltaTime * 1);
            prog = Mathf.Ceil(prog * 100) / 100;    //保留两位有效数据
            if (lastProgress != prog)
            {
                lastProgress = prog >= 1 ? 1 : prog;
                loadingProgressAction?.Invoke(lastProgress);
            }
            if (Mathf.Approximately(prog,1.0f))
            {
                if(loadingOperation.allowSceneActivation == false)
                    loadingOperation.allowSceneActivation = true;
            }
            if (loadingOperation.isDone) {
                ClearLoadingScene();
            }
        }
    }

    /// <summary>
    /// clear
    /// </summary>
    private void ClearLoadingScene() {
        lastProgress = 0;
        currProgress = 0;
        loadingOperation = null;
        loadingProgressAction = null;
    }
}
