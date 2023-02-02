using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;


public static class GameObjectUtility
{
    /// <summary>
    /// get or create prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="name"></param>
    /// <param name="position"></param>
    /// <param name="root"></param>
    /// <returns></returns>
    public static GameObject GetOrCreatePrefab(GameObject prefab, string name, Vector3 position, Transform root = null)
    {
        return GetOrCreatePrefab(prefab, name, position, Quaternion.identity, root);
    }

    /// <summary>
    /// get or creat prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="name"></param>
    /// <param name="position"></param>
    /// <param name="quaternion"></param>
    /// <param name="root"></param>
    /// <returns></returns>
    public static GameObject GetOrCreatePrefab(GameObject prefab, string name, Vector3 position, Quaternion quaternion, Transform root = null)
    {
        GameObject go = GameObject.Instantiate(prefab);
        if (root != null)
        {
            go.transform.parent = root;
        }
        go.name = name;
        go.transform.position = position;
        go.transform.rotation = quaternion;
        go.transform.localScale = Vector3.one;
        return go;
    }

    /// <summary>
    ///找到所有物体 
    /// </summary>
    /// <param name="includeDisable">If set to <c>true</c> include disable.</param>
    public static GameObject Find(string name, bool includeInactive= true )
    {
        GameObject go = GameObject.Find(name);
        if (go != null)
            return go;

        GameObject[] objs = includeInactive ? Resources.FindObjectsOfTypeAll<GameObject>() : GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objs)
        {
            //名称一致
            if (obj.name == name)
            {
                return obj;
            }

            //层级名称一致
            if (obj.GetHierarchyName() == name)
            {
                return obj;
            }

            //子物体
            Transform trans = obj.transform.Find(name);
            if (trans != null)
            {
                return trans.gameObject;
            }
        }

        return null;
    }

    /// <summary>
    /// Add a new child game object.
    /// </summary>

    static public GameObject AddChild(GameObject parent)
    {
        return AddChild(parent, true);
    }

    /// <summary>
    /// Add a new child game object.
    /// </summary>

    static public GameObject AddChild(GameObject parent, bool undo)
    {
        GameObject go = new GameObject();
#if UNITY_EDITOR
        if (undo)
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    public static string GetHierarchyName(this GameObject go)
    {
        string object_string = go.name;
        Transform parent = go.transform.parent;
        while (parent != null)
        {
            object_string = parent.name + "/" + object_string;
            parent = parent.parent;
        }
        return object_string;
    }
}
