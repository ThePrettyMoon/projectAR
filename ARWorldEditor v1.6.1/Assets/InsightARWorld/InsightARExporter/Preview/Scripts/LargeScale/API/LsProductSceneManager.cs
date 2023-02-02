using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum LSProductSceneModel { 
    SCENE_NONE = 0,
    SCENE_MODEL_LANMARK = 1,
    SCENE_MODEL_POI_ADD_OR_NONE = 2,
    SCENE_MODEL_POI_ATTACH = 4,
}

public struct MaterialPaths{

    public string Path_Type_Scene;
    public string Path_Type_Alg;
    public string Path_Type_Map;
    public string Path_Type_Geo;
    public string Path_Type_Navi;

}

public class LsProductSceneManager
{
    private LsProductSceneManager() { }

    public LsProductSceneManager(string contentId, string snapshotId, string path) {

        rootPath = path;
        cid = contentId;
        sid = snapshotId;
        materialPaths = new MaterialPaths
        {
            Path_Type_Scene = Path.Combine(rootPath, ContentResPaths.SceneFileDesc),
            Path_Type_Alg   = Path.Combine(rootPath, ContentResPaths.AlgFileDesc),
            Path_Type_Map   = Path.Combine(rootPath, ContentResPaths.MapFileDesc),
            Path_Type_Geo   = Path.Combine(rootPath, ContentResPaths.GeoFileDesc),
            Path_Type_Navi  = Path.Combine(rootPath, ContentResPaths.NavFileDesc),
        };
        
        scenePath = materialPaths.Path_Type_Scene;
        sceneParser = new SceneParser();
        sceneParser.ParseScene(scenePath);
        sceneName = sceneParser.GetSceneName();

        unity3dPath = Path.Combine(scenePath, sceneName + ".unity3d");
    }

    //包含10001-10009的文件夹名
    public string rootPath
    {
        get; set;
    }

    //unity3D文件夹名
    public string unity3dPath
    {
        get; set;
    }

    //10001的文件夹名
    public string scenePath
    {
        get; set;
    }

    public string sceneName
    {
        get; set;
    }

    public string cid {
        get;set;
    }

    public string sid
    {
        get; set;
    }

    /// <summary>
    /// 资源的文件路径：10001~10009
    /// </summary>
    public MaterialPaths materialPaths {
        get;set;
    }


    public SceneParser sceneParser
    {
        get;set;
    }

    public LSProductSceneModel sceneModel {
        get;set;
    }

    public InsightAttachType attachType
    {
        get; set;
    }
}
