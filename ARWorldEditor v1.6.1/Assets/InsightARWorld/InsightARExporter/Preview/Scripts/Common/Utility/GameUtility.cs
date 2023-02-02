using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtility 
{
    static public void Destroy(UnityEngine.Object obj)
    {
        if (obj)
        {
            if (obj is Transform)
            {
                Transform t = (obj as Transform);
                GameObject go = t.gameObject;

                if (Application.isPlaying)
                {
                    t.parent = null;
                    UnityEngine.Object.Destroy(go);
                }
                else
                    UnityEngine.Object.DestroyImmediate(go);
            }
            else if (obj is GameObject)
            {
                GameObject go = obj as GameObject;
                Transform t = go.transform;

                if (Application.isPlaying)
                {
                    t.parent = null;
                    UnityEngine.Object.Destroy(go);
                }
                else
                    UnityEngine.Object.DestroyImmediate(go);
            }
            else if (Application.isPlaying)
                UnityEngine.Object.Destroy(obj);
            else
                UnityEngine.Object.DestroyImmediate(obj);
        }
    }
}
