using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UObject = UnityEngine.Object;

public class CustomResourceManager : UnitySingleton<CustomResourceManager>
{
    public const string TAG = "CustomResourceManager";
    public class AssetInfo
    {
        public string path;
        public UObject asset;
    }

    #region resources 加载
    private Dictionary<Type, List<AssetInfo>> loadedResourceAssets = new Dictionary<Type, List<AssetInfo>>();

    /// <summary>
    /// 从resources目录加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T LoadResources<T>(string path, string name = "") where T : UnityEngine.Object
    {
        List<AssetInfo> assetInfos = null;
        if (loadedResourceAssets.TryGetValue(typeof(T), out assetInfos))
        {
            for (int i = 0; i < assetInfos.Count; i++)
            {
                if (assetInfos[i].path == path)
                {
                    return assetInfos[i].asset as T;
                }
            }
        }

        T asset = Resources.Load<T>(path);
        AssetInfo assetInfo = new AssetInfo();
        assetInfo.path = path;
        assetInfo.asset = asset;
        if (assetInfos == null || assetInfos.Count == 0)
        {
            assetInfos = new List<AssetInfo>();
            assetInfos.Add(assetInfo);
            loadedResourceAssets.Add(typeof(T), assetInfos);
        }
        else
        {
            assetInfos.Add(assetInfo);
        }
        return asset;
    }

    /// <summary>
    /// 清空资源素材
    /// </summary>
    public void DestroyResourceAssets()
    {
        var Enumetor = loadedResourceAssets.GetEnumerator();
        while (Enumetor.MoveNext())
        {
            List<AssetInfo> assetInfos = Enumetor.Current.Value;
            foreach (AssetInfo assetInfo in assetInfos)
            {
                GameObject.DestroyImmediate(assetInfo.asset, true);
            }
        }

        loadedResourceAssets.Clear();
        Resources.UnloadUnusedAssets();
    }

    #endregion

    #region assetbundle同步加载  慎用
    private Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();

    public T LoadAsset<T>(string path, string name) where T : UnityEngine.Object
    {
        AssetBundle assetBundle = LoadAssetBundle(path);
        if (assetBundle == null) return null;
        return assetBundle.LoadAsset<T>(name);
    }

    private AssetBundle LoadAssetBundle(string path)
    {
        AssetBundle assetBundle = null;
        if (!bundles.TryGetValue(path, out assetBundle))
        {
            assetBundle = AssetBundle.LoadFromFile(path);
            bundles.Add(path, assetBundle);
        }
        return assetBundle;
    }

    /// <summary>
    /// 销毁同步加载的bundles
    /// </summary>
    public void UnloadBundles()
    {
        List<AssetBundle> lstBundles = new List<AssetBundle>();
        foreach (var ab in bundles.Values)
        {
            lstBundles.Add(ab);
        }

        lstBundles.ForEach(p =>
        {
            p.Unload(false);
        });
        bundles.Clear();
    }
    #endregion

    #region assetbundle 异步加载 推荐
    Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();
    //存储保存的assetbundle
    Dictionary<string, AssetBundleInfo> loadedAssetBundles = new Dictionary<string, AssetBundleInfo>();
    // 存储请求
    Dictionary<string, List<LoadAssetRequest>> loadAssetRequests = new Dictionary<string, List<LoadAssetRequest>>();

    class LoadAssetRequest
    {
        public Type assetType;
        public string[] assetNames;
        public bool bScene;
        public Action<UnityEngine.Object[], string> sharpFunc;
    }

    public class AssetBundleInfo
    {
        public AssetBundle m_AssetBundle;
        public int m_ReferenceCount = 0;

        public AssetBundleInfo(AssetBundle assetBundle)
        {
            m_AssetBundle = assetBundle;
            m_ReferenceCount = 0;
        }
    }

    private void OnDestroy()
    {
        UnloadResources();
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="path"></param>
    public void LoadLevelAsync(string path, Action<UnityEngine.Object[], string> action = null, Action<string> onError = null)
    {
        LoadAssetAsync<UObject>(path, null, true, action, onError);
    }

    /// <summary>
    /// 加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="assetNames"></param>
    /// <param name="action"></param>
    public void LoadAssetAsync<T>(string path, string assetNames, Action<UnityEngine.Object[], string> action = null, Action<string> onError = null) where T : UnityEngine.Object
    {
        LoadAssetAsync<UObject>(path, new string[] { assetNames }, false, action, onError);
    }

    /// <summary>
    /// 加载多个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="assetNames"></param>
    /// <param name="action"></param>
    public void LoadAssetAsync<T>(string path, string[] assetNames, Action<UnityEngine.Object[], string> action = null, Action<string> onError = null) where T : UnityEngine.Object
    {
        LoadAssetAsync<UObject>(path, assetNames, false, action, onError);
    }

    /// <summary>
    ///默认从本地路径异步加载，包含场景加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name=""></param>
    /// <returns></returns>
    void LoadAssetAsync<T>(string path, string[] assetNames, bool bScene = false, Action<UnityEngine.Object[], string> action = null, Action<string> onError = null) where T : UnityEngine.Object
    {
        LoadAssetRequest loadAssetRequest = new LoadAssetRequest();
        loadAssetRequest.assetNames = assetNames;
        loadAssetRequest.assetType = typeof(T);
        loadAssetRequest.sharpFunc = action;
        loadAssetRequest.bScene = bScene;

        string abName = PathUtility.GetFileNameFromPath(path);
        Debug.Log(abName);
        List<LoadAssetRequest> request = null;
        if (loadAssetRequests.TryGetValue(abName, out request))
        {
            request.Add(loadAssetRequest);
        }
        else
        {
            request = new List<LoadAssetRequest>();
            request.Add(loadAssetRequest);
            loadAssetRequests.Add(abName, request);
            StartCoroutine(StartLoadAssetAync<T>(path, abName, onError));
        }
    }

    /// <summary>
    /// 加载资源协程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    IEnumerator StartLoadAssetAync<T>(string path, string abName, Action<string> onError = null) where T : UnityEngine.Object
    {
        Debug.Log("assetBundleInfo: " + abName);
        AssetBundleInfo assetBundleInfo = TryGetLoadedAssetBundle(abName);
        //Debug.Log("assetBundleInfo: " + assetBundleInfo.ToString());
        if (assetBundleInfo == null)
        {
            yield return StartCoroutine(TryLoadAssetBundle(path, abName));
            assetBundleInfo = loadedAssetBundles[abName];
        }

        if (assetBundleInfo == null)
        {
            string error = "maybe content is exported via error opereationsystem platform or unity editor version";
            loadAssetRequests.Remove(abName);
            onError?.Invoke(error);
            yield break;
        }
        if (assetBundleInfo.m_AssetBundle == null) {
            string error = "maybe content is exported via error opereationsystem platform or unity editor version";
            loadAssetRequests.Remove(abName);
            onError?.Invoke(error);
            yield break;
        }

        List<LoadAssetRequest> lst;
        if (!loadAssetRequests.TryGetValue(abName, out lst))
        {
            string error = "maybe content is exported via error opereationsystem platform or unity editor version";
            loadAssetRequests.Remove(abName);
            onError?.Invoke(error);
            yield break;
        }

        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundle assetBundle = assetBundleInfo.m_AssetBundle;
            List<UnityEngine.Object> results = new List<UnityEngine.Object>();
            //判断是否是scene assetbundle
            if (!assetBundle.isStreamedSceneAssetBundle)
            {
                AssetBundleRequest request = assetBundle.LoadAssetAsync(name, typeof(T));
                yield return request;

                string[] assetNames = lst[i].assetNames;
                for (int j = 0; j < assetNames.Length; j++)
                {
                    if (!lst[i].bScene)
                    {
                        AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync(assetNames[j], lst[i].assetType);
                        yield return assetBundleRequest;
                        results.Add(assetBundleRequest.asset);
                    }
                }
            }
            if (lst[i].sharpFunc != null)
            {
                lst[i].sharpFunc(results.ToArray(), abName);
                lst[i].sharpFunc = null;
            }
            assetBundleInfo.m_ReferenceCount++;           
        }
        loadAssetRequests.Remove(abName);

    }

    /// <summary>
    /// 返回assetbundleinfo
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    AssetBundleInfo TryGetLoadedAssetBundle(string abName)
    {
        AssetBundleInfo assetBundleInfo;
        loadedAssetBundles.TryGetValue(abName, out assetBundleInfo);
        if (assetBundleInfo == null) return null;

        //加入assetbundle 依赖判断
        string[] dependencies = null;
        if (!m_Dependencies.TryGetValue(abName, out dependencies))
            return assetBundleInfo;

        foreach (var dependency in m_Dependencies)
        {
            AssetBundleInfo dependencyBundleInfo;
            if (!loadedAssetBundles.TryGetValue(abName, out dependencyBundleInfo))
                return null;
        }

        return assetBundleInfo;
    }

    /// <summary>
    /// 加载assetbundle 协程
    /// </summary>
    /// <param name="path"></param>
    /// <param name="abName"></param>
    /// <returns></returns>
    IEnumerator TryLoadAssetBundle(string path, string abName)
    {
        AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(path);
        yield return assetBundleCreateRequest;
        AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;
        AssetBundleInfo assetBundleInfo = new AssetBundleInfo(assetBundle);
        loadedAssetBundles.Add(abName, assetBundleInfo);
    }


    /// <summary>
    /// 内部卸载assetbundle
    /// 如果内部有依赖，减掉引用索引
    /// </summary>
    /// <param name="abName"></param>
    void UnloadAssetBundleInternal(string abName, bool thorough = false)
    {
        AssetBundleInfo assetBundleInfo = TryGetLoadedAssetBundle(abName);
        if (assetBundleInfo == null) return;

        if (--assetBundleInfo.m_ReferenceCount <= 0)
        {
            //如果当前AB处于Async Loading过程中，卸载会崩溃，只减去引用计数即可
            if (loadAssetRequests.ContainsKey(abName)) return;

            assetBundleInfo.m_AssetBundle.Unload(thorough);
            loadedAssetBundles.Remove(abName);
            InsightDebug.Log(TAG, "AssetBunlde " + abName + " has been unloaded successfully" );
        }
    }

    /// <summary>
    /// 卸载依赖
    /// </summary>
    /// <param name="abName"></param>
    void UnloadDependencies(string abName, bool bThorough = false)
    {
        string[] dependencies;
        if (!m_Dependencies.TryGetValue(abName, out dependencies)) return;

        foreach (var dependency in dependencies)
        {
            UnloadAssetBundleInternal(dependency, bThorough);
        }
        m_Dependencies.Remove(abName);
    }


    /// <summary>
    /// 卸载assetbundle ，供外部调用
    /// </summary>
    /// <param name="abName"></param>
    public void UnloadAssetBundle(string abName, bool bThorough = false)
    {
        UnloadAssetBundleInternal(abName, bThorough);
        UnloadDependencies(abName, bThorough);
    }

    /// <summary>
    /// 卸载ab
    /// </summary>
    /// <param name="abName"></param>
    public void Unload(string abName)
    {
        if (loadedAssetBundles == null || loadedAssetBundles.Count == 0) return;
        AssetBundleInfo assetBundleInfo;
        loadedAssetBundles.TryGetValue(abName, out assetBundleInfo);
        if (assetBundleInfo == null) return;
        loadedAssetBundles.Remove(abName);
    }

    /// <summary>
    /// 清理资源
    /// </summary>
    public void UnloadResources()
    {
        if (loadedAssetBundles == null || loadedAssetBundles.Count == 0) return;
        List<string> list = new List<string>();
        foreach (KeyValuePair<string, AssetBundleInfo> kv in loadedAssetBundles)
        {
            string key = kv.Key;
            list.Add(key);
        }
        for(int i = 0; i < list.Count; i++)
        {
            UnloadAssetBundle(list[i], true);
        }
        //loadedAssetBundles.Clear(); //避免某些情况ab没有卸载问题，不能清空
        list.Clear();
    }
    #endregion
}

