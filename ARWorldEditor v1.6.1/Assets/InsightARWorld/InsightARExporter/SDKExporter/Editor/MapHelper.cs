
using UnityEngine;

public class MapHelper
{
    public static void LocalPosToLatitudeLongitude(Transform transform, LocalMapInfo localMapInfo)
    {
        Vector3 vector3 = ((Matrix4x4)@localMapInfo.LocalToWorldMatrix).MultiplyPoint(transform.position);
        double worldLatitude = localMapInfo.WorldLatitude;
        double worldLongitude = localMapInfo.WorldLongitude;
        Debug.LogFormat("pointLatitudeLongitude {0} {1}", new object[2]
        {
      (object) (worldLatitude + (double) vector3.z * 8.99E-06),//reality distance to Latitude
      (object) (worldLongitude + (double) vector3.x * 1.141E-05)//reality distance to Longitude
        });
    }
}
