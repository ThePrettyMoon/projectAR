using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtility 
{
    /// <summary>
    /// 朝向相机，不弯腰
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="camera"></param>
    /// <param name="y"></param>
    public static void LookAtXZPlane(Transform trans, Camera camera,bool useTargetHeight = true)
    {
        Vector3 worldPosition = camera.transform.position;
        if (useTargetHeight)
        {
            worldPosition.y = trans.position.y;
        }
        trans.LookAt(worldPosition);
    }

    /// <summary>
    /// 克隆物体
    /// </summary>
    /// <param name="cloneTrans"></param>
    /// <param name="parent"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static Transform Clone(Transform prefabTrans, Transform parent, string layer)
    {
        if (prefabTrans == null) return null;
        GameObject prefab = GameObject.Instantiate(prefabTrans.gameObject);
        prefab.name = prefab.name.Replace("(Clone)", "");
        Transform trans = prefab.transform;
        trans.SetParent(parent);
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
        trans.localScale = Vector3.one;
        trans.SetLayer(layer, true);
        return trans;
    }

    /// <summary>
    /// 从resources 目录加载transform
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="layer"></param>
    /// <param name="includeChildren"></param>
    /// <returns></returns>
    public static Transform Clone(string path, Transform parent, Vector3 position,
Quaternion rotation, string layer, bool includeChildren = true)
    {
        GameObject go = CustomResourceManager.Instance.LoadResources<GameObject>(path);
        if (go == null) return null;
        GameObject prefab = GameObject.Instantiate(go);
        prefab.name = prefab.name.Replace("(Clone)", "");
        Transform trans = prefab.transform;
        trans.SetParent(parent);
        trans.position = position;
        trans.rotation = rotation;
        trans.localScale = Vector3.one;
        trans.SetLayer(layer, includeChildren);
        trans.gameObject.SetActive(true);
        return trans;
    }
}
