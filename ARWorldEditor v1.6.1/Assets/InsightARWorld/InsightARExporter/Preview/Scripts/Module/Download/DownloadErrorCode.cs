using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DownloadErrorCode 
{
    //网络异常
    NETWORK_NOT_REACHABLE = -100,
    //下载对象异常
    NETWORK_TARGET_NOT_EXITS = -101,
    // 下载文件数量异常
    NETWORK_FILE_COUNT_ERROR = -102,
    // 下载MD5值异常
    NETWORK_MD5_ERROR = -103,
    //文件下载出错
    NETWORK_FILE_DOWNLOAD_ERROR = -104,
}
