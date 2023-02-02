#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace ARWorldEditor
{
    public  class LocalSceneCacheManager:Singleton<LocalSceneCacheManager>
    {
        private const string SCENE_TAG = "LocalSceneCacheManager";
        private static string Enviroment
        {
            get
            {
#if DEBUG_TEST
                return "DEBUG_TEST";
#else
            return "";
#endif
            }
        }

        #region 缓存曾经发布过的场景
        private static LocalSceneCache localSceneCache;
        private static string targetPath = "";
        private static string version = "";
        private static string id = "";
        private static Dictionary<string, LocalSceneCacheData> localSceneCacheDic
        {
            get
            {
                if (localSceneCache == null)
                {
                    localSceneCache = new LocalSceneCache();
                    ReadCacheFromLocal();
                }
                return localSceneCache.localSceneCacheDic;
            }
        }
        public bool LoadFromCache(string _id, string devicesType, string _version)
        {
            string key = Enviroment + _id + devicesType;
            InsightDebug.Log(SCENE_TAG, "load scene : " + key);
            if (localSceneCacheDic.ContainsKey(key))
            {
                var curSceneCache = localSceneCacheDic[key];

                if (!EditorUtility.DisplayDialog("提示", "是否加载本地缓存场景？", "确定", "取消"))
                {
                    return false;
                }
                targetPath = curSceneCache.scenePath;
                version = _version;
                id = _id;
                EditorApplication.update += UpdateMethod;
                return true;
            }
            return false;
        }
        static void UpdateMethod()
        {
            EditorApplication.update -= UpdateMethod;
            var curScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            if (curScene.path == targetPath)
            {
                //if (curScene.name.Contains("scene_" + id + "_"))
                //{
                //    string name = "scene_" + id + "_" + 13;
                //    Rename(curScene, name);
                //}
                //Debug.Log("相同场景");
                return;
            }
            try
            {
                var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(targetPath);
            }
            catch (System.Exception e)
            {
                EditorUtility.DisplayDialog("Error", e.Message + "/n本地缓存可能已被清理", "确认");
                throw;
            }

            //if (scene.name.Contains("scene_"+id+"_"))
            //{

            //    string name = "scene_" + id +"_" + version;
            //    Rename(scene, name);
            //}

        }
        static void Rename(UnityEngine.SceneManagement.Scene scene, string name)
        {
            if (Path.GetExtension(scene.path) != "")//判断路径是否为空
            {
                AssetDatabase.RenameAsset(scene.path, name);
            }
        }
        public void SaveToDic(string _id, string _sceneName, string _version, string devicesType, string _path)
        {
            string key = Enviroment + _id + devicesType;
            if (localSceneCacheDic.ContainsKey(key))
            {
                int currentVersion = int.Parse(_version.Substring(1));
                int resVersion = int.Parse(localSceneCacheDic[key].version.Substring(1));
                //判断版本
                if (currentVersion >= resVersion)
                {
                    localSceneCacheDic[key].version = _version;
                    localSceneCacheDic[key].sceneName = _sceneName;
                    localSceneCacheDic[key].scenePath = _path;
                    SaveChacheToLocal();
                }
                return;
            }
            localSceneCacheDic.Add(key, new LocalSceneCacheData() { contentID = _id, sceneName = _sceneName, version = _version, scenePath = _path });
            SaveChacheToLocal();
        }
        private static void ReadCacheFromLocal()
        {
            if (!UnityEditor.EditorPrefs.HasKey(Enviroment + SCENE_TAG))
            {
                //InsightDebug.LogError(TAG, "EditorPrefs not has key");
                return;
            }
            if (localSceneCache == null)
            {
                InsightDebug.LogError(SCENE_TAG, "localSceneCache null");
                return;
            }
            localSceneCache = JsonUtil.Deserialization<LocalSceneCache>(UnityEditor.EditorPrefs.GetString(Enviroment + SCENE_TAG));
        }
        private static void SaveChacheToLocal()
        {
            if (localSceneCache == null)
            {
                InsightDebug.LogError(SCENE_TAG, "localSceneCache null");
                return;
            }
            UnityEditor.EditorPrefs.SetString(Enviroment + SCENE_TAG, JsonUtil.Serialize(localSceneCache));
        }
        public class LocalSceneCacheData
        {
            public string contentID;//场景内容ID
            public string sceneName;//发布时场景的名字
            public string version;//1
            public string scenePath;//Asset/xxx/xxx.unity
        }
        public class LocalSceneCache
        {
            public Dictionary<string, LocalSceneCacheData> localSceneCacheDic = new Dictionary<string, LocalSceneCacheData>();
        }
        #endregion

        #region 缓存发布时勾选的选项-主要是mobile端
        private const string PLATFORM_TAG = "PlatformCache";
        private static LocalSceneExportPlatformsCache localSceneExportPlatformsCache;
        private static Dictionary<string, localSceneExportPlatformData> localSceneExportPlatformsCacheDic
        {
            get
            {
                if (localSceneExportPlatformsCache == null)
                {
                    localSceneExportPlatformsCache = new LocalSceneExportPlatformsCache();
                    ReadExportPlatformFromLocal();
                }
                return localSceneExportPlatformsCache.localSceneExportPlatformCacheDic;
            }
        }
        public class localSceneExportPlatformData
        {
            public bool iosToggle;
            public bool androidToggle;
        }
        public class LocalSceneExportPlatformsCache
        {
            public Dictionary<string, localSceneExportPlatformData> localSceneExportPlatformCacheDic = new Dictionary<string, localSceneExportPlatformData>();
        }
        private static void ReadExportPlatformFromLocal()
        {
            if (!UnityEditor.EditorPrefs.HasKey(Enviroment + PLATFORM_TAG))
            {
                //InsightDebug.LogError(TAG, "EditorPrefs not has key");
                return;
            }
            if (localSceneExportPlatformsCache == null)
            {
                InsightDebug.LogError(PLATFORM_TAG, "localSceneCache null");
                return;
            }
            localSceneExportPlatformsCache = JsonUtil.Deserialization<LocalSceneExportPlatformsCache>(UnityEditor.EditorPrefs.GetString(Enviroment + PLATFORM_TAG));
        }
        private static void SavePlatformChacheToLocal()
        {
            if (localSceneCache == null)
            {
                InsightDebug.LogError(PLATFORM_TAG, "localSceneExportPlatformsCache null");
                return;
            }
            UnityEditor.EditorPrefs.SetString(Enviroment + PLATFORM_TAG, JsonUtil.Serialize(localSceneExportPlatformsCache));
        }
        public bool LoadPlatformFromCache(string _id,out localSceneExportPlatformData data)
        {
            string key = Enviroment + _id ;
            data = null;
            if (localSceneExportPlatformsCacheDic.ContainsKey(key))
            {
                var curCache = localSceneExportPlatformsCacheDic[key];
                Debug.LogError("LoadPlatformFromCache:"+ curCache.androidToggle+" - "+ curCache.iosToggle);
                data = curCache;
                return true;
            }
            return false;
        }
        public void SaveToPlatformDic(string _id,bool iosToggle,bool androidToggle)
        {
            string key = Enviroment + _id ;
            if (localSceneExportPlatformsCacheDic.ContainsKey(key))
            {
                localSceneExportPlatformsCacheDic[key].iosToggle = iosToggle;
                localSceneExportPlatformsCacheDic[key].androidToggle = androidToggle;
                
                return;
            }
            localSceneExportPlatformsCacheDic.Add(key, new localSceneExportPlatformData() { iosToggle = iosToggle, androidToggle = androidToggle }) ;
            SavePlatformChacheToLocal();
        }
    }
    #endregion
}
#endif