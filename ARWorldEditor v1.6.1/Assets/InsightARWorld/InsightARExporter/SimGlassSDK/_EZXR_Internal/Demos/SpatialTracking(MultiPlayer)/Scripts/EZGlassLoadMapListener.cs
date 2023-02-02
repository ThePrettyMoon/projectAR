using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZGlassLoadMapListener : AndroidJavaProxy
{
    Action<int, string> callback;
    public EZGlassLoadMapListener(Action<int, string> callback) : base("com.ezxr.ezglassarsdk.callback.EZGlassLoadMapListener")
    {
        this.callback = callback;
    }

    /// <summary>
    /// 下载成功：code返回0，msg返回本地存储路径
    /// 下载失败：code返回非0
    /// </summary>
    /// <param name="code"></param>
    /// <param name="msg"></param>
    public void onFinish(int code, string msg)
    {
        callback(code, msg);
    }
}
