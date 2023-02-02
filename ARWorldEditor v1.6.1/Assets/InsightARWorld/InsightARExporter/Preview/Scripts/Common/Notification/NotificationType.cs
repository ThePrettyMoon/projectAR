using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通知类型
/// </summary>
public enum NotificationType
{
    //other
    MODEL_INSTANCE,
    TRANSLATE,
    SCALE,
    ROTATE,
    TOUCH_END,

    // 添加其他数据信息
    MAP_STOP_NAVIGATION,  //停止导航
    BODY_RECOGNITION,
    PHOTOWALL_CHOOSE_CONFIRM,  //确认选择
    PHOTOWALL_CHOOSE_CANCEL,   //取消确认
    PHOTOWALL_REPLACE_CONFIRM, // 确认替换
    PHOTOWALL_REPLACE_CANCEL,  //取消替换
    PHOTOWALL_CLOSE_DETAILINFO,  // 关闭详情
    POI_START_GAME,      //POI_开始游戏
    POI_GET_LIST,         //获取POI数据

    //游戏退出
    GAME_EXIT,
    //网络异常
    NETWORK_ERROR
}
