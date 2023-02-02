using UnityEngine;

public class LocalMapInfo : ScriptableObject
{
  public double WorldLatitude;
  public double WorldLongitude;
  public Matrix4x4 LocalToWorldMatrix;

}
