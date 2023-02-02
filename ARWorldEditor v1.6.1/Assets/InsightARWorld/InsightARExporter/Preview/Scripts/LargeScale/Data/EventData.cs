using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 内容层返回的poi消息
/// </summary>
[Serializable]
public class PoiEventData{
    public string cid;
    public string sid;
    //0 只下载 1下载并进入
    public int activetype;
}

/// <summary>
/// 内容层返回的map信息
/// </summary>
[Serializable]
public class MapVisibleEventData
{
    // "0-隐藏 1-显示"
    public string visibility;
}

[Serializable]
public class MakeToastEventData
{
    // 1 显示文字提示 2 显示进度条（咱不实现） 0 隐藏显示
    public string command;
    // 显示时间，默认3秒
    public string time;
    // 0 -1 ,command =2 生效
    public string progress;
    //文字提示
    public string text;
}
