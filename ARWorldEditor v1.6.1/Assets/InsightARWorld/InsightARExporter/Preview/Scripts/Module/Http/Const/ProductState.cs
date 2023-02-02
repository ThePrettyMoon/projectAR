using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 记录arproduct 状态
/// </summary>
public enum ProductState
{
    TYPE_NO_ALG = 0,
    // 本地已经存在数据，不需要更新
    STATE_LOCAL = 1,
    // 需要更新及重新下载
    STATE_NEED_DOWNLOAD = 2,

    STATE_NEED_UNZIP = 3
}
