using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
  
/* EZVIOState：VIO跟踪状态量，反映当前算法的跟踪质量，主要用来交互
* -----------------------------------------------------------------------------
* EZVIOCameraState_Initing
*     跟踪状态默认值，只有系统启动时是这个状态，会立刻切换到Detecting.
* EZCameraState_Init_Fail
*     算法初始化失败，通常由错误的初始化方式造成.
* EZVIOCameraState_Detecting
*     算法正在进行初始化.
* EZVIOCameraState_Tracking
*     算法正常运行的状态.
* EZVIOCameraState_Track_Limited
*     算法跟踪质量较低，但还能继续运行.
* EZVIOCameraState_Track_Fail
*     算法经过跟踪质量很差，需要强制复位.*/
    public enum EZVIOStateReason
    {
        EZVIOCameraStateReason_None = 0,
        EZVIOCameraStateReason_AssetFilesError = 1,
        EZVIOCameraStateReason_CameraDataError = 2,
        EZVIOCameraStateReason_IMUDataError = 3,
        EZVIOCameraStateReason_LowLight = 4,
        EZVIOCameraStateReason_ExcessiveMotion = 5,
        EZVIOCameraStateReason_InsufficientFeatures = 6,
    }
}
