using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Entity 状态ID
public enum StateID
{
    // 系统中不存在状态
    NullStateID = 0,
    //出生状态
    Birth = 1,
    // 玩家准备状态
    Prepare = 2,
    // 开始状态
    Start = 3,
    // 运行状态
    Run = 4,
    // 停止状态
    Finish = 5

}
