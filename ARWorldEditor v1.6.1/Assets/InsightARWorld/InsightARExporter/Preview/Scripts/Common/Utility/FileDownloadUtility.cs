using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileDownloadUtility
{
    /// <summary>
    /// 生成task id
    /// </summary>
    /// <param name="url"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static int GenerateTaskId(string url, string path)
    {
        return EncodeUtility.MD5(url + path).GetHashCode();
    }
}

