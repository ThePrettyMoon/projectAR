using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StructWrap
{
    public static Vector4 NewVector4(float px, float py, float pz, float pw)
    {
        return new Vector4(px, py, pz, pw);
    }

    public static Vector3 NewVector3(float px, float py, float pz)
    {
        return new Vector3(px, py, pz);
    }

    public static Vector2 NewVector2(float px, float py)
    {
        return new Vector2(px, py);
    }
}

