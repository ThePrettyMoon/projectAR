using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GeoData
{
#if UNITY_IOS
    public string geoFilePath;
    public GeoLevelsData geoFileContent;
#else
    /** geoJsonString 具体键值如下：
 [{
     floor : "1",
     floorName : "C6-1F",
     geoFilePath : "./geo/c6_1.json"
  }]
 */
    public string floor;
    public string floorName;
    public string geoFilePath;
#endif


}

public class GeoLevelsData {

    public int min;
    public int max;
    public List<LevelsData> levels;

}

public class LevelsData {

    [JsonProperty(propertyName:"ref")]
    public string refFloor;
    public string name;
    public float height;
    public string filePath;
}
