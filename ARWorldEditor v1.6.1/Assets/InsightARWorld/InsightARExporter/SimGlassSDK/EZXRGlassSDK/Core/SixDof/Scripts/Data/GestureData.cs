using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public enum AI_ENGINE_TYPE
    {
        MNN_ENGINE = 0,    ///< alibaba mnn ai engine
        SNPE_ENGINE        ///< qualcomm snpe ai engine
    }

    public enum DEVICE_TYPE
    {
        AI_ENGINE_CPU = 0,    ///< use cpu as device
        AI_ENGINE_GPU         ///< use gpu as device
    }
    /*
    /// <summary>
    /// 静态手势
    /// </summary>
    public enum STATIC_GESTURE_TYPE
    {
        FIST = 0, //握拳
        THUMB = 1,   //伸大拇指
        INDEX = 2,   //伸食指
        HLGES = 3,   //水平八字
        VGES = 4,    // 伸食指中指，yeah 手势
        OKGES = 5,  //ok手势
        PALM = 6,   //摊掌
        SOFTFIST = 7, //软握拳
        ROCKGES = 8, // 伸大拇指和小指
        VLGES = 9   //伸食指和大拇指，食指朝上
    }
    */

    /// <summary>
    /// 动态手势
    /// </summary>
    public enum GestureType
    {
        NO_HAND = -1,         ///< no 没有手
        HAND_WITHOUT_MEANING = 0,   ///< valid未定义的手势
        ENTER_GESTURE = 1,      ///< valid 静态手势 1-0/7 或者 静态手势 9-》0/7
        RETURN_GESTURE = 2,         ///< valid 静态手势 3-》0/7
        HOME_GESTURE = 3,           ///< valid 静态手势 5-》0/7
    }

    public struct BBox
    {
        public float x;    ///< top left x position
        public float y ;    ///< top left y position
        public float w ; ///< width
        public float h ; ///< height
        public float s ;    ///< objectness score
        public int c ;   ///< class id
    };
}
