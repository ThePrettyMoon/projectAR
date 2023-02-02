using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// 场景控制器
    /// </summary>
    public interface ISceneController:IState
    {

    }

    /// <summary>
    /// 场景状态枚举
    /// </summary>
    public enum SceneStateID
    {
        EN_STATE_UNKNOWN = -1,   //未知状态
        EN_STATE_LOADING = 0,  //资源加载中
        //EN_STATE_CHECKLOGIN = 0,  //检查登陆状态
        //EN_STATE_LOGIN = 1,    //登陆状态
        EN_STATE_LOCATION = 1, //场景定位
        EN_STATE_NAVIGATION = 2, //场景导航
        //EN_STATE_PRODUCTLIST = 4, //展示产品列表
        //EN_STATE_XIXIPRODUCTLIST = 5, //西溪产品列表
        //EN_STATE_DOWNLOADCHAPTER = 6, // 下载chapter
        EN_STATE_POI = 3,  //POI状态
        //EN_STATE_VERIFY_CODE = 8, //核销码

    }
