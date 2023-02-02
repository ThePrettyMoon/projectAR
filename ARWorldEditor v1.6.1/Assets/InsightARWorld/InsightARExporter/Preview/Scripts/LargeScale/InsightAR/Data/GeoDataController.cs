using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GeoDataController
{

    public static void SetAndInitGeoData()
    {

        string scenePath = ContentResPaths.Instance.GetGeoFilePath();
        if (!File.Exists(scenePath)) return;
        string jsonContent = File.ReadAllText(scenePath);
        GeoLevelsData levelsData = new GeoLevelsData();
        JObject geojObject = JObject.Parse(jsonContent);
        if (geojObject != null)
        {
            levelsData.min = int.Parse(geojObject.SelectToken("min").ToString());
            levelsData.max = int.Parse(geojObject.SelectToken("max").ToString());
            var objLevels = (JArray)geojObject.SelectToken("levels");
            levelsData.levels = new List<LevelsData>();
            for (int i = 0; i < objLevels.Count; i++)
            {
                LevelsData l = new LevelsData
                {
                    refFloor = objLevels[i].SelectToken("ref").ToString(),
                    name = JObjectUtility.ParseJObjectString(objLevels[i].SelectToken("name")),
                    height = float.Parse(objLevels[i].SelectToken("height").ToString()),
                };
                l.filePath = Path.Combine(ContentResPaths.Instance.GetGeoRoot(), l.refFloor, ContentResPaths.GeoFloorJsonDesc);
                //Debug.Log("test " + l.filePath);
                levelsData.levels.Add(l);
            }
        }
        InsightConfigManager.Instance.SetGeoLevelsData(levelsData);
    }

    public static void Clear() {

        InsightConfigManager.Instance.SetGeoLevelsData(null);

    }
}
