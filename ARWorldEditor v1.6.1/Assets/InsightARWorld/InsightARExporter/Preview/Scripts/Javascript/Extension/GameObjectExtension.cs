using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameObjectExtension
{
    static public T AddMissingComponent<T>(this GameObject go) where T : Component
    {
#if UNITY_FLASH
        object comp = go.GetComponent<T>();
#else
        T comp = go.GetComponent<T>();
#endif
        if (comp == null)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                RegisterUndo(go, "Add " + typeof(T));
#endif
            comp = go.AddComponent<T>();
        }
#if UNITY_FLASH
#else
        return comp;
#endif
    }

    static private void RegisterUndo(UnityEngine.Object obj, string name)
    {
#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(obj, name);
        SetDirty(obj);
#endif
    }

    static private void SetDirty(UnityEngine.Object obj)
    {
#if UNITY_EDITOR
        if (obj)
        {
            UnityEditor.EditorUtility.SetDirty(obj);
        }
#endif
    }

    /// <summary>
    /// set layer
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="name"></param>
    /// <param name="includeChildren"></param>
    public static void SetLayer(this Transform trans, string name, bool includeChildren = false)
    {
        SetLayer(trans.gameObject, name, includeChildren);
    }

    /// <summary>
    /// set layer
    /// </summary>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="includeChildren"></param>
    public static void SetLayer(this GameObject go, string name, bool includeChildren = false)
    {
        go.layer = LayerMask.NameToLayer(name);
        if (!includeChildren)
            return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>())
        {
            trans.gameObject.layer = LayerMask.NameToLayer(name);
        }
    }
    public static void LookAtPosition(this Transform trans, Vector3 pos, float y)
    {
        Vector3 vect = new Vector3(pos.x, y, pos.z);
        trans.LookAt(vect);
    }

    public static void LookAtPosition(this GameObject go, Vector3 pos)
    {
        LookAtPosition(go.transform, pos);
    }

    public static void LookAtPosition(this Transform trans, Vector3 pos)
    {
        trans.LookAt(pos);
    }

    public static GameObject getParent(this GameObject go)
    {
        return go.transform.parent.gameObject;
    }

    public static GameObject[] getChildren(this GameObject go)
    {
        return go.GetComponentsInChildren<GameObject>();
    }

    public static void InstantiateFromFile(string modelUrl, Action<GameObject> onSuccess)
    {
        InstantiateFromFile(modelUrl, null, onSuccess);
    }

    public static void InstantiateFromFile(string modelUrl, Transform parent, Action<GameObject> onSuccess)
    {
        //todo 
    }

    /// <summary>
    /// 是否对该GameObject上的Mesh进行视锥体剔除，默认为true。
    /// </summary>
    public static bool frustumCulled(this GameObject go)
    {
        return true;
    }

    public static void frustumCulled(this GameObject go, bool active)
    {

    }



}
