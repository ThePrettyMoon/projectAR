using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConstPath
{
    /// <summary>
    /// root 根目录
    /// </summary>
    /// <returns></returns>
    public static string RootDirectory()
    {
#if UNITY_EDITOR
        return Application.dataPath ;
#else
       return Application.persistentDataPath;
#endif
    }

    /// <summary>
    /// 返回根目录
    /// </summary>
    /// <returns></returns>
    public static string ProductDirectory()
    {
#if UNITY_EDITOR
        return "../EditorDebug/Product";
#else
       return "Product";
#endif
    }

    /// <summary>
    /// 临时temp 路径
    /// </summary>
    /// <returns></returns>
    public static string TempDirectory()
    {
#if UNITY_EDITOR
        return "../EditorDebug/Temp";
#else
       return "Temp";
#endif
    }

    /// <summary>
    /// 临时temp 路径
    /// </summary>
    /// <returns></returns>
    public static string CacheRootDirectory()
    {
#if UNITY_EDITOR
        return  "../EditorDebug/Cache";
#else
       return "Cache";
#endif
    }
}
