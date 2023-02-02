using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ContentResPaths:Singleton<ContentResPaths>
{

    public const string SceneJsonDesc    = "scenedesc.json";
    public const string GeoJsonDesc      = "levels.json";

    public const string GeoFloorJsonDesc = "geojson.json";

    public const string SceneFileDesc   = "10001";
    public const string AlgFileDesc     = "10002";
    public const string MapFileDesc     = "10003";
    public const string GeoFileDesc     = "10008";
    public const string NavFileDesc     = "10009";

    private string resourcePath;

    [JsonIgnore]
    public string ResourcePath {
        get {
            return resourcePath;
        }
        set {
            resourcePath = value;
        }
    }

    public string GetGeoRoot()
    {
#if UNITY_IOS
        var path = Path.Combine(ResourcePath, GeoFileDesc);
        return path.Replace("\\", "/");
#else
        return Path.Combine(ResourcePath, GeoFileDesc);
#endif
    }

    public string GetNaviRoot()
    {
#if UNITY_IOS
        var path = Path.Combine(ResourcePath, NavFileDesc);
        return path.Replace("\\", "/");
#else
        return Path.Combine(ResourcePath, NavFileDesc);
#endif
    }


    public string GetGeoFilePath() {

#if UNITY_IOS
        var path = Path.Combine(GetGeoRoot(), GeoJsonDesc);
        return path.Replace("\\", "/");
#else
        return Path.Combine(GetGeoRoot(), GeoJsonDesc);
#endif
    }


}