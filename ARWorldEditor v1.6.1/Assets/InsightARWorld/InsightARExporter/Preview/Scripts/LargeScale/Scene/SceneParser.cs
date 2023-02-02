using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.Video;

/// <summary>
/// 场景文件解析
/// </summary>
public class SceneParser
{
    private const string TAG = "SceneParser";
    //场景名称
    private string sceneName;
    //场景模型
    private List<SceneObject> sceneRootObjects;

    public string GetSceneName()
    {
        return this.sceneName;
    }

    /// <summary>
    /// 场景数据解析
    /// </summary>
    /// <param sceneName="jsonContent"></param>
    public void ParseScene(string sceneDirectory)
    {
        try
        {
            string scenePath = Path.Combine(sceneDirectory, ContentResPaths.SceneJsonDesc);
            if (!File.Exists(scenePath)) return;
            string jsonContent = File.ReadAllText(scenePath);

            if (string.IsNullOrEmpty(jsonContent)) return;

            JObject jObject = JObject.Parse(jsonContent);
            if (jObject != null)
            {
                this.sceneName = JObjectUtility.ParseJObjectString(jObject.SelectToken("name"));
                this.sceneRootObjects = ParseSceneObject(sceneDirectory, (JArray)jObject.SelectToken("models"));
            }
        }
        catch (Exception e)
        {
            InsightDebug.Log(TAG, "Parse Scene Exception " + e.ToString());
        }
    }

    /// <summary>
    /// 解析场景物体
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    private List<SceneObject> ParseSceneObject(string sceneDirectory, JArray jArray)
    {
        if (jArray == null ) return null;

        List<SceneObject> objects = new List<SceneObject>();
        try
        {
            for (int i = 0; i < jArray.Count; i++)
            {
                JObject jobject = (JObject)jArray[i];
                if (jobject != null)
                {
                    SceneObject sceneObject = new SceneObject();
                    sceneObject.rootDirectory = sceneDirectory;
                    sceneObject.name = JObjectUtility.ParseJObjectString(jobject.SelectToken("name"));
                    sceneObject.enable = (int)(jobject.SelectToken("enable"));
                    sceneObject.layer = JObjectUtility.ParseJObjectString(jobject.SelectToken("layer"));
                    sceneObject.tag = JObjectUtility.ParseJObjectString(jobject.SelectToken("tag"));
                    sceneObject.luaPathList = ParseLuaPath((JArray)jobject.SelectToken("luapath"));
                    sceneObject.videoPathList = ParseVideoPath((JArray)jobject.SelectToken("videopath"));
                    sceneObject.children = ParseSceneObject(sceneDirectory, (JArray)jobject.SelectToken("children"));
                    objects.Add(sceneObject);
                }
            }
        }
        catch (Exception e)
        {
            InsightDebug.Log(TAG, "Parse Scene Object Exception " + e.ToString());
        }
        return objects;
    }

    /// <summary>
    /// 解析lua script
    /// </summary>
    private string[] ParseLuaPath( JArray jArray)
    {
        if (jArray == null || jArray.Count == 0) return null;
        List<string> list = new List<string>();
        for (int i = 0; i < jArray.Count; i++)
        {
            string luaFileName = (string)jArray[i];
            string luaFilePath =luaFileName;
            list.Add(luaFilePath);
        }
        return list.ToArray();
    }

    /// <summary>
    /// parse video path
    /// </summary>
     private string[] ParseVideoPath(JArray jArray)
    {
        if (jArray == null || jArray.Count == 0) return null;
        List<string> list = new List<string>();
        for (int i = 0; i < jArray.Count; i++)
        {
            string videoFileName = (string)jArray[i];
            string luaFilePath = videoFileName;
            list.Add(luaFilePath);
        }
        return list.ToArray();
    }

    /// <summary>
    /// 解析场景
    /// </summary>
    public void ParseGameObjectComponents()
    {
        if (sceneRootObjects == null || sceneRootObjects.Count == 0) return;
        for (int i = 0; i < sceneRootObjects.Count; i++)
        {
            SceneObject sceneObject = sceneRootObjects[i];
            if (sceneObject == null) continue;
            //解析lua文件
            //ParseLuaScriptRecursive(sceneObject);
            //解析video player
            ParseVideoClipRecursive(sceneObject);

            //解析Javascript
            ParseJavaScriptRecursive(sceneObject);
        }
    }

#if false
    /// <summary>
    /// 进入场景后执行luascripts
    /// </summary>
    private void ParseLuaScriptRecursive(SceneObject sceneObject)
    {
        if (sceneObject == null) return;
        string[] luaPathList = sceneObject.luaPathList;
        if (luaPathList != null && luaPathList.Length > 0)
        {
            GameObject go = GameObjectUtility.Find(sceneObject.name);
            if (go == null) return;
            ScriptRunner[] scriptRunners = go.GetComponents<ScriptRunner>();
            if (scriptRunners != null && scriptRunners.Length > 0)
            {
                for (int i = 0; i < scriptRunners.Length; i++)
                {
                    if (i < luaPathList.Length)
                    {
                        scriptRunners[i].ParseScript(sceneObject.rootDirectory, luaPathList[i]);
                    }
                }
            }
        }

        List<SceneObject> childObjects = sceneObject.children;
        if (childObjects == null || childObjects.Count == 0) return;

        for (int j = 0; j < childObjects.Count; j++)
        {
            SceneObject childObject = childObjects[j];
            if (childObject == null) continue;
            ParseLuaScriptRecursive(childObject);
        }
    }

#endif
    /// <summary>
    /// 解析JavaScript
    /// </summary>
    /// <param name="sceneObject"></param>
    private void ParseJavaScriptRecursive(SceneObject sceneObject)
    {
        if (sceneObject == null) return;
        string[] luaPathList = sceneObject.luaPathList;
        if (luaPathList != null && luaPathList.Length > 0)
        {
            GameObject go = GameObjectUtility.Find(sceneObject.name);
            if (go == null) return;
            ScriptRunner[] scriptRunners = go.GetComponents<ScriptRunner>();
            if (scriptRunners != null && scriptRunners.Length > 0)
            {
                for (int i = 0; i < scriptRunners.Length; i++)
                {
                    if (i < luaPathList.Length)
                    {
                        if (string.IsNullOrEmpty(luaPathList[i]) || string.IsNullOrEmpty(sceneObject.rootDirectory))
                        {
                            InsightDebug.LogError(TAG, "maybe runnerscript is missing in " + go.name);
                            continue;
                        }
                        scriptRunners[i].ParseScript(sceneObject.rootDirectory, luaPathList[i]);
                    }
                }
            }
        }

        List<SceneObject> childObjects = sceneObject.children;
        if (childObjects == null || childObjects.Count == 0) return;

        for (int j = 0; j < childObjects.Count; j++)
        {
            SceneObject childObject = childObjects[j];
            if (childObject == null) continue;
            ParseJavaScriptRecursive(childObject);
        }
    }

    /// <summary>
    /// 解析videopath
    /// </summary>
    /// <param name="sceneObject"></param>
    private void ParseVideoClipRecursive(SceneObject sceneObject)
    {
        if (sceneObject == null) return;
        string[] videoPathList = sceneObject.videoPathList;
        if (videoPathList != null && videoPathList.Length > 0)
        {
            GameObject go = GameObjectUtility.Find(sceneObject.name);
            if (go == null) return;
            VideoPlayer[] videoPlayers = go.GetComponents<VideoPlayer>();
            if (videoPlayers != null && videoPlayers.Length > 0)
            {
                for (int i = 0; i < videoPlayers.Length; i++)
                {
                    if (i < videoPathList.Length)
                    {
                        VideoPlayer videoPlayer = videoPlayers[i];
                        videoPlayer.source = VideoSource.Url;
                        videoPlayer.url = Path.Combine(sceneObject.rootDirectory, videoPathList[i]);
                    }
                }
            }
        }

        List<SceneObject> childObjects = sceneObject.children;
        if (childObjects == null || childObjects.Count == 0) return;

        for (int j = 0; j < childObjects.Count; j++)
        {
            SceneObject childObject = childObjects[j];
            if (childObject == null) continue;
            ParseVideoClipRecursive(childObject);
        }
    }
}

[Serializable]
public class SceneObject
{
    public string name;
    public int enable;
    public string tag;
    public string layer;
    public List<SceneObject> children;
    public string[] luaPathList;
    public string[] videoPathList;
    public string rootDirectory;
 }


