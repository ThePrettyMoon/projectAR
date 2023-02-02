using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

public static class GameExtension
{
    
    #region custom_functions



    /// <summary>
    /// Instantiate an object and add it to the specified parent.
    /// </summary>

    static public GameObject AddChild(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        #if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
        #endif
        if (go != null && parent != null)
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


    static public  double[] GetPointsFromStrings(string[] strList)
    {
        double[] pointsArray = new double[strList.Length];
        for (int i = 0; i < pointsArray.Length; i++)
        {
            double tmpValue;
            if (double.TryParse(strList[i], out tmpValue))
            {
                pointsArray[i] = tmpValue;
            }
            else
            {
                return null;
            }
        }
        return pointsArray;
    }


    static public string GetStringsFromArray(int[] points)
    {
        string strPoint = "";
        for (int i = 0; i < points.Length; i++)
        {
            strPoint += points[i].ToString();
        }
        return strPoint;
    }

    static public string GetStringsFromArray(float[] points)
    {
        string strPoint = "";
        for (int i = 0; i < points.Length; i++)
        {
            strPoint += points[i].ToString();
        }
        return strPoint;
    }


    static public  Vector2 GetVect2FromString(string s)
    {
        if (s == null)
        {
            return Vector2.zero;
        }
        string[] partName = s.Split(' ');
        float a1 = float.Parse(partName[0]);
        float a2 = float.Parse(partName[1]);
        Vector2 pos = new Vector2(a1, a2);
        return pos;
    }

    static public  string[] GetCharArrayFromString(string s)
    {
        if (s == null)
        {
            return null;
        }
        string[] partName = s.Split(' ');
        return partName;
    }

    static public string[] GetIntArrayFromStringArray(int[] icons)
    {
        if (icons == null)
        {
            return null;
        }
        string[] strs = new string[icons.Length];
        for (int i = 0; i < icons.Length; i++)
        {
            strs[i] = icons[i].ToString();
        }
        return strs;
    }

    static public int GetIndexFromString(int i, string s)
    {
        if (s == null)
        {
            return 0;
        }
        char[] cc = s.ToCharArray();
        int a = Convert.ToInt32(cc[i]) - 48;
        return a;
    }

    static public int[] GetIntArrayFromString(string s)
    {
        if (s == null)
        {
            Debug.Log("StringToInt False!");
            return null;
        }
        char[] cc = s.ToCharArray();
        int[] aa = new int[cc.Length];
        for (int i = 0; i < cc.Length; i++)
        {
            aa[i] = Convert.ToInt32(cc[i]) - 48;
        }
        return aa;
    }


    /// <summary>
    /// 去除部分物体 
    /// </summary>
    /// <returns>The box collider exception shadow receiver.</returns>
    public static Vector3 AddBoxColliderExceptionShadowReceiver(Transform parent)
    {
        Collider baseCollider = parent.GetComponent<Collider>();
        if (baseCollider != null)
        {
            return baseCollider.bounds.size;
        }

        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;

        Vector3 center = Vector3.zero;
        Renderer[] allRenders = parent.GetComponentsInChildren<Renderer>();
        List<Renderer> lst = new List<Renderer>();
        for (int i = 0; i < allRenders.Length; i++)
        {
            string renderName = allRenders[i].name.ToLower();
            if (renderName != "shadowreceiver")
            {
                lst.Add(allRenders[i]);
            } 
        }
        Renderer[] renders = lst.ToArray();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;   
        }
        center /= parent.GetComponentsInChildren<Transform>().Length; 
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);   
        }
        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;

        return bounds.size; 
    }

    /// <summary>
    /// Adds the box collider.  该函数将在父节点上添加一个boxcollider框住所有物体
    /// </summary>
    /// <param name="parent">Parent.</param>
    public static Vector3 AddBoxColliderWhenRootColliderNone(Transform parent)
    {
        Collider baseCollider = parent.GetComponent<Collider>();
        if (baseCollider != null)
        {
            return baseCollider.bounds.size;
        }

        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;
        //Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        //foreach (Collider child in colliders){
        //DestroyImmediate(child);
        //}
        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;   
        }
        center /= parent.GetComponentsInChildren<Transform>().Length; 
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);   
        }
        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;

        return bounds.size;

    }

    /// <summary>
    /// 根节点以及子物体都没有collider，再添加bocollider
    /// </summary>
    /// <returns>The box collider when collider none.</returns>
    /// <param name="parent">Parent.</param>
    public static Vector3 AddBoxColliderWhenAllCollidersNone(Transform parent)
    {
        Collider baseCollider = parent.GetComponent<Collider>();
        if (baseCollider != null)
        {
            return baseCollider.bounds.size;
        }

        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        if (colliders != null && colliders.Length > 0)
        {
            return colliders[0].bounds.size;
        }

        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;
        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;   
        }
        center /= parent.GetComponentsInChildren<Transform>().Length; 
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);   
        }
        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;

        return bounds.size;

    }

    /// <summary>
    /// 递归遍历子物体 
    /// </summary>
    /// <param name="obj">Object.</param>
    public static void FindObjects(GameObject obj)
    {   
        int i = 0;  
        while (i < obj.transform.childCount)
        {  
            Transform parent = obj.transform.GetChild(i);   
            if (parent.childCount > 0)
                FindObjects(parent.gameObject);  
            i++;  
        }  
    }

    /// <summary>
    /// 递归遍历查找根物体 
    /// </summary>
    /// <param name="obj">Object.</param>
    public static GameObject  FindRootByChild(GameObject obj)
    {   
        if (obj.transform.parent != null)
        {
            return FindRootByChild(obj.transform.parent.gameObject);
        }
        else
        {
            return obj;
        }
    }

    /// <summary>
    /// Finds the name of the child object by.
    /// </summary>
    /// <returns><c>true</c>, if child object by name was found, <c>false</c> otherwise.</returns>
    /// <param name="obj">Object.</param>
    public static bool FindChildObjectByName(GameObject obj, string childName)
    {   
        int i = 0;  
        while (i < obj.transform.childCount)
        {  
            Transform parent = obj.transform.GetChild(i);  
            if (parent.name == childName)
                return true;
            if (parent.childCount > 0)
            {
                if (FindChildObjectByName(parent.gameObject, childName))
                {
                    return true;
                }
            } 
            i++;  
        }  
        return false;
    }

    public static GameObject  GetChildObjectByName(GameObject obj, string childName)
    {   
        int i = 0;  
        while (i < obj.transform.childCount)
        {  
            Transform parent = obj.transform.GetChild(i);  
            if (parent.name == childName)
                return parent.gameObject;
            if (parent.childCount > 0)
            {
                if (FindChildObjectByName(parent.gameObject, childName))
                {
                    return  GetChildObjectByName(parent.gameObject, childName);
                }
            } 
            i++;  
        } 
        return null;
    }

    /// <summary>
    /// Finds the name of the child object by.
    /// </summary>
    /// <returns><c>true</c>, if child object by name was found, <c>false</c> otherwise.</returns>
    /// <param name="obj">Object.</param>
    public static bool FindChildObjectByGameObject(GameObject obj, GameObject childObj)
    {   
        return FindChildObjectByName(obj, childObj.name);  
    }

    /// <summary>
    /// 让模型看向相机 
    /// </summary>
    public static void LookAtMainCamera(GameObject go)
    {
        Vector3 vect = Camera.main.transform.position;
        float positionY = go.transform.position.y;
        go.transform.LookAt(new Vector3(vect.x, positionY, vect.z));
    }

    /// <summary>
    ///  根据球坐标系计算光源方向 参数都为弧度 
    /// </summary>
    /// <returns>The quaternion from sphere coordinates.</returns>
    /// <param name="azimuth">Azimuth.</param>
    /// <param name="elevation">Elevation.</param>
    public static Quaternion GetQuaternionFromSphereCoordinates(float azimuth, float elevation)
    {
        float x = Mathf.Sin(azimuth) * Mathf.Cos(elevation);
        float y = Mathf.Sin(elevation);
        float z = Mathf.Cos(azimuth) * Mathf.Cos(elevation);
        Vector3 localPositon = new Vector3(x, y, z);
        Camera mainCamera = Camera.main;
        Vector3 worldPostion = mainCamera.transform.TransformPoint(localPositon);
        Vector3 direction = mainCamera.transform.position - worldPostion;
        return  Quaternion.LookRotation(direction, Vector3.up);

    }

    /// <summary>
    /// 根据屏幕分辨率计算需要的分辨率 
    /// </summary>
    /// <param name="destWidth">Destination width.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public static void GetScreenResolution(int resolutionWidth, ref int w, ref int h)
    { 
        
        if (w > h)
        {
            float aspect = (float)h / w;
            w = resolutionWidth;
            h = (int)(resolutionWidth * aspect);
        }
        else
        {
            float aspect = (float)w / h;
            h = resolutionWidth;
            w = (int)(resolutionWidth * aspect);
        }

        w = ((int)(w + 2) / 10) * 10;
        h = ((int)(h + 2) / 10) * 10;  // 适配不同机型
    }

    /// <summary>
    /// 0 ~ 360 
    /// </summary>
    /// <returns>The angle.</returns>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public static float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? angle : 360 - angle;
    }

    /// <summary>
    /// Vectors the angle.
    /// </summary>
    /// <returns>The angle.</returns>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public static float VectorAngle(Vector3 from, Vector3 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector3.Angle(from, to);
        return cross.z > 0 ? angle : 360 - angle;
    }

    #endregion
}
