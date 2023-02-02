/*using Mapbox.Unity.Map;
using Mapbox.Utils;*/
using UnityEditor;
using UnityEngine;

public class MapMenu
{
    [MenuItem("CONTEXT/Transform/GenLocalMapInfo")]
    private static void GenMapMatrix(MenuCommand command)
    {
      /*  Transform context = (Transform)command.context;
        GameObject gameObject = ((Component)context).gameObject;
        string[] strArray = ((AbstractMap)GameObject.Find("Map").GetComponent<AbstractMap>()).Options.locationOptions.latitudeLongitude.Split(',');
        Vector2d vector2d = new Vector2d(double.Parse(strArray[0]), double.Parse(strArray[1]));
        Debug.LogFormat("originLatitudeLongitude {0} {1}", new object[2]
        {
            vector2d.x,
            vector2d.y
        });
        LocalMapInfo instance = (LocalMapInfo)ScriptableObject.CreateInstance<LocalMapInfo>();
        instance.WorldLatitude = vector2d.x;
        instance.WorldLongitude = vector2d.y;
        instance.LocalToWorldMatrix = context.localToWorldMatrix;
        AssetDatabase.CreateAsset((Object)instance, string.Format("Assets/InsightARWorld/{0}.asset", (object)((Object)gameObject).name));
        */
    }
}
