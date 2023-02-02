using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DownloadState
{
    // 未下载
    NONE = 0,

    // 下载开始
    START = 1,

    //下载停止
    STOP = 2,

    // 下载暂停
    PAUSE = 3,

    // 下载错误

    ERROR = 4,

    //下载结束
    FINISHED = 5,

    // 下载中
    RUNNING = 6,

    // 等待下载
    WAITING = 7,

    // 下载被去除

    REMOVED = 8
}
