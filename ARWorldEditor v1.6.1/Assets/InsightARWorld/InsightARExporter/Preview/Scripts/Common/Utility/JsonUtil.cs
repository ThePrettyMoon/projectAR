using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 防止和json utility命名冲突
/// </summary>
public class JsonUtil
{
    private const string TAG = "JsonUtil";

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static string Serialize(object src)
    {
        if (src == null) return null;
        return JsonConvert.SerializeObject(src);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="jsonStr"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object Deserialization(string jsonStr)
    {
        if (string.IsNullOrEmpty(jsonStr)) return null;
        return JsonConvert.DeserializeObject(jsonStr);
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonStr"></param>
    /// <returns></returns>
    public static object Deserialization(string jsonStr, System.Type type)
    {
        if (string.IsNullOrEmpty(jsonStr)) return null;
        return JsonConvert.DeserializeObject(jsonStr, type);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonStr"></param>
    /// <returns></returns>
    public static T Deserialization<T>(string jsonStr)
    {
        if (string.IsNullOrEmpty(jsonStr)) return default(T);
        return JsonConvert.DeserializeObject<T>(jsonStr);
    }

}

