using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第一个为设定的设备朝向
/// 第二个为当前玩家的设备朝向 
/// </summary>
public enum ScreenDeviceOrientation
{
    UnChanged = 0,
    PortraitToLandScape = 1,
    PortraitToReverseLandScape = 2,
    PortraitToReversePortrait = 3,
    LandScapeToPortrait = 4,
    LandScapeToReversePortrait = 5,
    LandScapeToReverseLandScape = 6
}
