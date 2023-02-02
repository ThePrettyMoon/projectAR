var g_RelocationTipState = {
    RELOCATION_IDLE_STATE: 0,
    RELOCATION_START_STATE: 1,
    RELOCATION_MOVE_STATE: 2,
    RELOCATION_TEXTURE_STATE: 3,
    RELOCATION_LIGHT_STATE: 4,
    RELOCATION_SUCCESS_STATE: 5
};
//跨楼层状态枚举 -处理跨楼层状态
var g_CrossState = {
    CROSS_ON_FLOOR: 0, // 位于楼层
    CROSS_ON_STAIR : 1, // 位于楼梯
    CROSS_ON_ESCALATOR : 2, // 位于自动扶梯
    CROSS_ON_ELEVATOR : 3, // 位于升降电梯
    CROSS_MAY_REACH : 4 // 可能已达到指定楼层
};
//用户导航提示枚举 -处理非跨楼层时的状态
var g_UserActionState = {
    NAV_UNKNOW : 0, // 初始化
    NAV_STRAIGHT : 1, // 前方直行n米
    NAV_LEFT : 2, // distance < set action dis ---> 左转
    NAV_RIGHT : 3, // 同上 ---> 右转
    NAV_UPSTAIRS : 4, // distance < set floor action dis ---> 上楼
    NAV_DOWNSTAIRS : 5, // 同上 ---> 下楼
    NAV_REACHING : 6, // 即将到达目的地
    NAV_REACH : 7 // 到达目的地
};
//跨楼层的类型 暂未使用
var g_PasswayType =  {
    PASSWAY_NONE : 0,
    PASSWAY_STAIR : 1,
    PASSWAY_ESCALATOR : 2,
    PASSWAY_ELEVATOR : 3
};
//楼层动作
var g_FloorActionState =  {
    FLOOR_UPSTAIRS : 0,
    FLOOR_DOWNSTAIRS : 1,
    FLOOR_SAMEFLOOR : 2
};

function GameNavigationState(gameObject) {
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
GameNavigationState.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () { },
    Enter: function () {
        this.Init();
        Insight.Debug.Log("js call Game Navigation State:Enter");
    },
    Execute: function () {
        if (!this.isEnabled) {
            return;
        }
    },
    Exit: function () {
        this.Close();
    },
    Init: function () {

        this.isEnabled = true;

        g_ResManager.navigationPanelTrans.gameObject.setActive(true);

        this.navPoiData = {};
        // 最短显示距离
        this.MINNAVDISTANCE = 1.0;

        this.NAVENDHEIGHT = -3.5;

        //如果一直是直行，显示最长路径，否则按照5米处理，尝试解决遮挡物问题
        this.ARROWSHOWDISTANCE = 5.0;

        this.startEnterUpStair = false;

        this.startEnterDownStair = false;

        this.startEnterLeft = false;

        this.startEnterRight = false;

        this.InitView();

        this.relocationTipState = g_RelocationTipState.RELOCATION_IDLE_STATE;

        this.curTipTime = 0.0;

        this.MAXTIPTIME = 1.5;

        this.curSuccessTipTime = 0.0;

        this.TRACKSUCCESSTIME = 1.0;

        //是否显示导航路径
        this.showPathArrow = true;

        this.INDICATORAREADISTANCE = 3.0;

        this.ENTERAREADISTANCE = 1.0;

        this.SHOWARROWDISTANCE = 5.0;

        this.enableArrowPath = true;

        //导航语音
        if(g_MapData.GetIsNavigationState())
        {
            //console.log("navigation init")
            g_ResManager.startAudio.play();
            this.curNaviVoice = g_ResManager.startAudio;
        }
        this.curUserAction = -1;
        this.curCrossState = -1;

        //native事件监听
        Insight.Navigation.OnNavigationEnd(this, this.onNavigationEnd);
        Insight.Navigation.OnPathDataChanged(this, this.onPathDataChanged);
    },

    onNavigationEnd: function () {
        Insight.Debug.Log("js call on navigation end");
        this.enableArrowPath = false;
        g_GameStateCtrl.ChangeState(g_GameStatus.GAME_STATUS_LOCATION);
    },

    onPathDataChanged: function (mapNavigationString) {
        //  Insight.Debug.Log("js call on path data changed " + mapNavigationString);

        if (mapNavigationString == undefined || mapNavigationString == "") {
            return;
        }

        var mapNavigationInfo = JSON.parse(mapNavigationString); //g_JSON:decode(mapNavigationString);

        if (mapNavigationInfo == undefined) {
            return;
        }

        //如果已经停止，退出绘制箭头
        if (!this.enableArrowPath) {
            return;
        }

        //如果点击规划路径而非AR导航，则不需要刷新提示
        if(g_MapData.GetIsNavigationState() == false)
        {
            return;
        }


        this.UpdateView(mapNavigationInfo);
    },

    // init view
    InitView: function () {
        var curPoint = g_MapData.GetCurrentMapPoint();
        var curPoiInfo = g_MapData.GetNavPoiInfo();

        var property = curPoiInfo.properties;
        var pointInfo = curPoiInfo.mapPoint;

        this.navPoiData = {
            id: property.id,
            name: property.name,
            position: new Insight.Vector3(
                pointInfo.realSpaceCoords[0], //Insight.Vector3.New(pointInfo.realSpaceCoords[1],
                pointInfo.realSpaceCoords[1],
                pointInfo.realSpaceCoords[2]
            )
        };
    },

    //update view
    UpdateView: function (mapNavigationInfo) {
        var userNavTips = mapNavigationInfo.userNavTips; //["turnInfoJsonBuf"];
        var mapFloorPaths = mapNavigationInfo.floorPaths; //["navPathJsonBuf"];
        var userState = mapNavigationInfo.userState;
        
        // g_ResManager.logText.text = JSON.stringify(userNavTips, null, 2);
        this.UpdateNavPath(mapFloorPaths);
        this.UpdateTurnInfoView(mapFloorPaths, userNavTips,userState);
        //this.UdpateOffLineView(mapFloorPaths,userState);

    },

    //更新偏航信息
    UdpateOffLineView: function (mapPaths, userState) {
        if (!userState.isOffRoute) {
            if (g_ResManager.navArrowImageTrans.gameObject.activeSelf) {
                g_ResManager.navArrowImageTrans.gameObject.setActive(false);
            }
            return;
        }

        if (!g_ResManager.navArrowImageTrans.gameObject.activeSelf) {
            g_ResManager.navArrowImageTrans.gameObject.setActive(true);
        }

        //先找到第一个目标点信息
        var destPosition = new Insight.Vector3(0, 0, 0);
        if (mapPaths.length > 0 && mapPaths != undefined) {
            var len = mapPaths.length;
            for (var k = 0; k < len; k++) {
                var v = mapPaths[k];
                var splitPath = v;
                if (splitPath != undefined) {
                    var mapPoints = splitPath.mapPoints;
                    if (mapPoints != undefined) {
                        //for k1,v1 in ipairs(mapPoints) do
                        for (var k1 = 0; k1 < mapPoints.length; k1++) {
                            var v1 = mapPoints[k1];
                            if (v1.isPass == 1) continue;
                            destPosition = new Insight.Vector3(v1.realSpaceCoords[0], v1.realSpaceCoords[1], v1.realSpaceCoords[2]);
                            break;
                        }
                    }
                }
            }
        }

        g_ResManager.navArrowImageRectTrans.anchoredPosition3D = g_CalculateAngle.CalculatePosition(g_ResManager.mainCamera, g_ResManager.navArrowImageTrans, destPosition, 2001, 1125, 160, 320);
        g_ResManager.navArrowImageRectTrans.localRotation = g_CalculateAngle.CalculateRotation(g_ResManager.mainCamera, destPosition);

    },

    // update nav path
    UpdateNavPath: function (mapFloorPaths) {
        if (!this.showPathArrow) {
            var childLen = g_ResManager.arrowRootTrans.childCount;
            for (i = 0; i <= childLen - 1; i++) {
                var childTrans = g_ResManager.arrowRootTrans.getChild(i);
                childTrans.gameObject.setActive(false);
            }
            return;
        }

        if (mapFloorPaths == undefined) {
            Insight.Debug.Log("nav path is nil");
            return;
        }

        var pathLen = mapFloorPaths.length; //Fw_Table_GetLength(mapNavPath.floorPath);
        if (pathLen == 0) {
            Insight.Debug.Log("floor path len is 0");
            return;
        }

        var curPoint = g_MapData.GetCurrentMapPoint();
        //当前楼层
        var curFloorId = curPoint.floorId;

        var splitPositionList = this.CalculateArrowPath(mapFloorPaths, curFloorId);
        var splitPositionLen = splitPositionList.length; //Fw_Table_GetLength(splitPositionList);
        if (splitPositionList == undefined || splitPositionLen == 0) {
            Insight.Debug.Log("split path is nil");
            return;
        }

        var childLen = g_ResManager.arrowRootTrans.childCount;
        /* for (var i = 0; i <= childLen - 1; i++) {
             var childTrans = g_ResManager.arrowRootTrans.getChild(i);
             childTrans.gameObject.setActive(false);
         }*/

        //k start at 0,
        //for k,v in ipairs(splitPositionList) do
        //for (var k in splitPositionList) {
        for (var k = 0; k < splitPositionLen - 1; k++) {
            var v = splitPositionList[k];
            if (k < childLen) {
                var childTrans = g_ResManager.arrowRootTrans.getChild(k);
                childTrans.gameObject.setActive(true);
                childTrans.position = v;
                childTrans.localScale = new Insight.Vector3(1, 1, 1);
                childTrans.lookAt(splitPositionList[k + 1], Insight.Vector3.up);
            }
        }

        //遍历一下子物体是否还需要隐藏
        if (childLen > splitPositionLen) {
            for (var k1 = splitPositionLen; k1 < childLen; k1++) {
                var childTrans = g_ResManager.arrowRootTrans.getChild(k1);
                childTrans.gameObject.setActive(false);
            }
        }
    },
    //更新导航提示
    UpdateTurnInfoView: function (mapFloorPaths, mapUserInfo,userState) {
        if (mapUserInfo == undefined||userState == undefined) {
            Insight.Debug.Log("nav turn info is nil-mapUserInfo == undefined:"+(mapUserInfo == undefined)+" userState == undefined:"+(userState == undefined));
            return;
        }
        
        //在正常的状态下判断是否是定位成功
        if (userState.userCrossState == g_CrossState.CROSS_ON_FLOOR) 
        {
            this.UpdateRelocationSuccessView();
        }

        //关闭poi接口,上下楼或者重定位中
        if (userState.userCrossState != g_CrossState.CROSS_ON_FLOOR) {
            //关闭POI标签显示
            g_MapPoiController.Pause();
            //关闭地面导航箭头
            g_ResManager.arrowRootTrans.gameObject.setActive(false)
        }else{
            //开启显示poi标签
            g_MapPoiController.Resume();
            //开启地面导航箭头
            g_ResManager.arrowRootTrans.gameObject.setActive(true)
            //关闭上下楼箭头
            g_ResManager.navDownstairArrowTrans.gameObject.setActive(false);
            g_ResManager.navUpstairArrowTrans.gameObject.setActive(false);
        }
        //console.log("mapUserInfo.userAction:"+mapUserInfo.userAction);

        //old:计算距离终点箭头显示
        // if (mapUserInfo.userAction != 9 && mapUserInfo.userAction != 0 && mapUserInfo.userAction != 7) 
        //new:非跨楼层和未到达终点的情况下计算距离终点箭头的显示
        if(userState.userCrossState == g_CrossState.CROSS_ON_FLOOR&&mapUserInfo.userAction != g_UserActionState.NAV_REACH)
        {
            var camPosition = g_ResManager.mainCameraTrans.position;
            var curPoint = g_MapData.GetCurrentMapPoint();
            var curPoiInfo = g_MapData.GetNavPoiInfo();
            var endPoint = curPoiInfo.mapPoint;
            //保证在同一层
            if (curPoint.floorId == endPoint.floorId) {
                var endDistance = this.GetNavigationEndDistance(mapFloorPaths, camPosition, endPoint.floorId);
                if (endDistance < this.SHOWARROWDISTANCE) {
                    if (!g_ResManager.navEndTrans.gameObject.activeSelf) {
                        g_ResManager.navEndTrans.gameObject.setActive(true);
                        //调整终点高度
                        var destY = endPoint.realSpaceCoords[1];
                        //console.error("End - poiHeight:"+(endPoint.realSpaceCoords[1]+g_ResManager.heightAboveGround));
                        //console.error("y:"+endPoint.realSpaceCoords[1]+" heightAboveGround:"+g_ResManager.heightAboveGround);
                        // var destY = -3.6;//c6
                        // if (endPoint.floorId == "2") {
                        //     destY = 1.0;
                        // }
                        // if (endPoint.floorId == "-2") {
                        //     destY = -1.7;
                        // }
                        g_ResManager.navEndTrans.position = new Insight.Vector3(endPoint.realSpaceCoords[0], destY, endPoint.realSpaceCoords[2]);
                    }
                }
            }
        }
        var curPoint = g_MapData.GetCurrentMapPoint();
        //当前楼层
        var curFloorId = curPoint.floorId;
        //上下楼提示
        if (userState.userCrossState != g_CrossState.CROSS_ON_FLOOR && mapFloorPaths.floorAction != g_FloorActionState.FLOOR_SAMEFLOOR)
        {
            //正在跨楼层，距离标注的上下楼点1米位置触发
            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.enterstairTipTrans.gameObject.setActive(false);
            g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);
            //打开上楼提示
            if (this.startEnterUpStair) {
                this.startEnterUpStair = false;
                g_ResManager.upstairTipTrans.gameObject.setActive(true);
                g_ResManager.navUpstairArrowTrans.gameObject.setActive(false);

                //正在上楼语音
                if (this.curUserAction != mapUserInfo.userAction) {
                    if (this.curNaviVoice) this.curNaviVoice.stop();
                    g_ResManager.upcrossingAudio.play();
                    this.curNaviVoice = g_ResManager.upcrossingAudio;
                    this.curUserAction = mapUserInfo.userAction;
                }
            }
            //打开下楼提示
            if (this.startEnterDownStair) {
                this.startEnterDownStair = false;
                g_ResManager.downstairTipTrans.gameObject.setActive(true);
                g_ResManager.navDownstairArrowTrans.gameObject.setActive(false);

                //正在下楼语音
                if (this.curUserAction != mapUserInfo.userAction) {
                    if (this.curNaviVoice) this.curNaviVoice.stop();
                    g_ResManager.downcrossingAudio.play();
                    this.curNaviVoice = g_ResManager.downcrossingAudio;
                    this.curUserAction = mapUserInfo.userAction;
                }
            }
            return;
        }
        // 重定位中或者跨楼层过程中，可能到达出口
        // old:if (mapUserInfo.userAction == 0 || mapUserInfo.userAction == 7) 
        if (userState.userCrossState != g_CrossState.CROSS_ON_FLOOR) 
        {
            g_ResManager.upstairTipTrans.gameObject.setActive(false);
            g_ResManager.downstairTipTrans.gameObject.setActive(false);

            //关闭init ok
            g_ResManager.initNavOKTrans.gameObject.setActive(false);

            //关闭上楼逻辑
            this.startEnterUpStair = false;
            g_ResManager.enterstairTipTrans.gameObject.setActive(false);
            g_ResManager.navUpstairArrowTrans.gameObject.setActive(false);
            //关闭下楼逻辑
            this.startEnterDownStair = false;
            g_ResManager.enterstairTipTrans.gameObject.setActive(false);
            g_ResManager.navDownstairArrowTrans.gameObject.setActive(false);

            //关闭转弯箭头
            this.startEnterLeft = false;
            this.startEnterRight = false;
            g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);

            this.UpdateRelocationView();

            //重定位语音
            // if (this.curUserAction != 0 && this.curUserAction != 7) 
            if(this.curCrossState == g_CrossState.CROSS_ON_FLOOR)
            {
                if (this.curNaviVoice) this.curNaviVoice.stop();
                g_ResManager.relocationAudio.play();
                this.curNaviVoice = g_ResManager.relocationAudio;
                this.curCrossState = userState.userCrossState;
            }
            return;
        }
        else
        {
            if(this.curCrossState != userState.userCrossState)
            {
                this.curCrossState = userState.userCrossState;
                //关闭上下楼导航提示箭头
                g_ResManager.navDownstairArrowTrans.gameObject.setActive(false);
                g_ResManager.navUpstairArrowTrans.gameObject.setActive(false);
            }
        }
      

        //导航状态提示（非跨楼层）
        if (mapUserInfo.userAction == g_UserActionState.NAV_STRAIGHT) /*1*/
        {
            //直行
            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.upstairTipTrans.gameObject.setActive(false);
            g_ResManager.downstairTipTrans.gameObject.setActive(false);
            g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);

            //关闭转弯箭头
            this.startEnterLeft = false;
            this.startEnterRight = false;
            g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);

            //直行语音
            if (this.curUserAction != mapUserInfo.userAction) {
                if (this.curNaviVoice) this.curNaviVoice.stop();
                g_ResManager.straightAudio.play();
                this.curNaviVoice = g_ResManager.straightAudio;
                this.curUserAction = mapUserInfo.userAction;
            }

        } 
        else if (mapUserInfo.userAction == g_UserActionState.NAV_LEFT) /*2*/
        {
            //直行后左转
            //添加左转箭头
            if (mapUserInfo.distance < this.INDICATORAREADISTANCE && mapUserInfo.distance >= this.ENTERAREADISTANCE) 
            {
                if (!this.startEnterLeft) {
                    this.startEnterLeft = true;
                    this.UpdateNavigationIndicatorPosition(
                        mapFloorPaths,
                        2,
                        g_ResManager.navRoadsignArrowTrans, curFloorId
                    );
                    g_ResManager.navRoadsignArrowTrans.gameObject.setActive(true);
                }

                //左转语音
                if (this.curUserAction != mapUserInfo.userAction) {
                    if (this.curNaviVoice) this.curNaviVoice.stop();
                    g_ResManager.leftAudio.play();
                    this.curNaviVoice = g_ResManager.leftAudio;
                    this.curUserAction = mapUserInfo.userAction;
                }

            } else {
                this.startEnterLeft = false;
                g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);
            }

            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.upstairTipTrans.gameObject.setActive(false);
            g_ResManager.downstairTipTrans.gameObject.setActive(false);

        } 
        else if (mapUserInfo.userAction == g_UserActionState.NAV_RIGHT) /*3*/
        {
            //直行后右转
            //添加右转箭头
            if (mapUserInfo.distance < this.INDICATORAREADISTANCE && mapUserInfo.distance >= this.ENTERAREADISTANCE) {
                if (!this.startEnterRight) {
                    this.startEnterRight = true;

                    this.UpdateNavigationIndicatorPosition(
                        mapFloorPaths,
                        3,
                        g_ResManager.navRoadsignArrowTrans, curFloorId
                    );
                    g_ResManager.navRoadsignArrowTrans.gameObject.setActive(true);

                }

                //右转语音
                if (this.curUserAction != mapUserInfo.userAction) {
                    if (this.curNaviVoice) this.curNaviVoice.stop();
                    g_ResManager.rightAudio.play();
                    this.curNaviVoice = g_ResManager.rightAudio;
                    this.curUserAction = mapUserInfo.userAction;
                }
            } else {
                this.startEnterRight = false;
                g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);
            }

            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.upstairTipTrans.gameObject.setActive(false);
            g_ResManager.downstairTipTrans.gameObject.setActive(false);

        } 
        else if (mapUserInfo.userAction == g_UserActionState.NAV_UPSTAIRS) /*4*/
        {
            //1-3米提示玩家
            if (mapUserInfo.distance < this.INDICATORAREADISTANCE && mapUserInfo.distance >= this.ENTERAREADISTANCE) {
                if (!this.startEnterUpStair) {
                    g_ResManager.enterstairTipTrans.gameObject.setActive(true);
                    this.startEnterUpStair = true;

                    this.UpdateNavigationIndicatorPosition(
                        mapFloorPaths,
                        4,
                        g_ResManager.navUpstairArrowTrans, curFloorId
                    );
                    g_ResManager.navUpstairArrowTrans.gameObject.setActive(true);
                }

                //直行后上楼语音
                if (this.curUserAction != mapUserInfo.userAction) {
                    if (this.curNaviVoice) this.curNaviVoice.stop();
                    g_ResManager.upstairsAudio.play();
                    this.curNaviVoice = g_ResManager.upstairsAudio;
                    this.curUserAction = mapUserInfo.userAction;
                }

            }
            //<1m关闭提示 
            else if (mapUserInfo.distance < this.ENTERAREADISTANCE) {
                g_ResManager.initNavIconTrans.gameObject.setActive(false);
                g_ResManager.upstairTipTrans.gameObject.setActive(false);
                g_ResManager.downstairTipTrans.gameObject.setActive(false);
            }
            //用户在楼梯口等徘徊时，如果重定位后，需要先关闭提示 <=> 当前为0 上一帧不为0
            if(this.curCrossState !=g_CrossState.CROSS_ON_FLOOR && userState.userCrossState == g_CrossState.CROSS_ON_FLOOR )
            {
                g_ResManager.initNavIconTrans.gameObject.setActive(false);
                g_ResManager.upstairTipTrans.gameObject.setActive(false);
                g_ResManager.downstairTipTrans.gameObject.setActive(false);
            }

        } 
        else if (mapUserInfo.userAction == g_UserActionState.NAV_DOWNSTAIRS)  /*5*/
        {
            // //直行后下楼
            if (mapUserInfo.distance < this.INDICATORAREADISTANCE && mapUserInfo.distance >= this.ENTERAREADISTANCE) {
                if (!this.startEnterDownStair) {
                    g_ResManager.enterstairTipTrans.gameObject.setActive(true);
                    this.startEnterDownStair = true;

                    this.UpdateNavigationIndicatorPosition(
                        mapFloorPaths,
                        5,
                        g_ResManager.navDownstairArrowTrans, curFloorId
                    );
                    g_ResManager.navDownstairArrowTrans.gameObject.setActive(true);
                }

                //直行后下楼语音
                if (this.curUserAction != mapUserInfo.userAction) {
                    if (this.curNaviVoice) this.curNaviVoice.stop();
                    g_ResManager.downstairsAudio.play();
                    this.curNaviVoice = g_ResManager.downstairsAudio;
                    this.curUserAction = mapUserInfo.userAction;
                }

            } else if (mapUserInfo.distance < this.ENTERAREADISTANCE) {
                g_ResManager.initNavIconTrans.gameObject.setActive(false);
                g_ResManager.upstairTipTrans.gameObject.setActive(false);
                g_ResManager.downstairTipTrans.gameObject.setActive(false);
            }

            //用户在楼梯口等徘徊时，如果重定位后，需要先关闭提示 <=> 当前为0 上一帧不为0
            if(this.curCrossState !=g_CrossState.CROSS_ON_FLOOR && userState.userCrossState == g_CrossState.CROSS_ON_FLOOR )
            {
                g_ResManager.initNavIconTrans.gameObject.setActive(false);
                g_ResManager.upstairTipTrans.gameObject.setActive(false);
                g_ResManager.downstairTipTrans.gameObject.setActive(false);
            }
        } 
        else if (mapUserInfo.userAction == g_UserActionState.NAV_REACHING)  /*6*/
        {
            //15米后直行达到目的地
            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.upstairTipTrans.gameObject.setActive(false);
            g_ResManager.downstairTipTrans.gameObject.setActive(false);

            //直行语音
            if (this.curUserAction != mapUserInfo.userAction) {
                if (this.curNaviVoice) this.curNaviVoice.stop();
                g_ResManager.straightAudio.play();
                this.curNaviVoice = g_ResManager.straightAudio;
                this.curUserAction = mapUserInfo.userAction;
            }
        } 
        else if (mapUserInfo.userAction == g_UserActionState.NAV_REACH) /*7*/
        {
            //到达目的地
            //sdk会传回这个状态，不需要处理
            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.upstairTipTrans.gameObject.setActive(false);
            g_ResManager.downstairTipTrans.gameObject.setActive(false);

            //到达目的地语音
            if (this.curUserAction != mapUserInfo.userAction) {
                if (this.curNaviVoice) this.curNaviVoice.stop();
                g_ResManager.endAudio.play();
                this.curNaviVoice = g_ResManager.endAudio;
                this.curUserAction = mapUserInfo.userAction;
            }
        }

        // angle 暂时不做处理
    },

    //更新重定位逻辑
    UpdateRelocationView: function () {
        if (this.relocationTipState == g_RelocationTipState.RELOCATION_IDLE_STATE) 
        {
            this.relocationTipState = g_RelocationTipState.RELOCATION_START_STATE;
        } 
        else if (this.relocationTipState == g_RelocationTipState.RELOCATION_START_STATE) 
        {
            this.relocationTipState = g_RelocationTipState.RELOCATION_MOVE_STATE;
            this.curTipTime = 0.0;
            g_ResManager.initNavTxt.text = "请缓慢移动手机";
            g_ResManager.initNavIconTrans.gameObject.setActive(true);
            g_ResManager.initNavSlideTrans.gameObject.setActive(true);
            g_ResManager.initNavPanelTrans.gameObject.setActive(true);

            //显示目标楼层提示
            g_ResManager.relocationTipTrans.gameObject.setActive(true);
            var curPoiInfo = g_MapData.GetNavPoiInfo();
            var endPoint = curPoiInfo.mapPoint;
            g_ResManager.relocationTipText.text =
                "请在" + endPoint.floorId + "楼扶梯口重新定位";

            //关闭路径箭头
            this.showPathArrow = false;

        } 
        else if (this.relocationTipState == g_RelocationTipState.RELOCATION_MOVE_STATE) 
        {
            if (this.curTipTime < this.MAXTIPTIME) {
                this.curTipTime = this.curTipTime + Insight.Time.deltaTime;
                g_ResManager.initNavSlideMaterial.setFloat(
                    "_FillAmount",
                    this.curTipTime / this.MAXTIPTIME
                );
            } else {
                this.relocationTipState = g_RelocationTipState.RELOCATION_TEXTURE_STATE;
                this.curTipTime = 0.0;
                g_ResManager.initNavTxt.text = "请对准纹理丰富的区域";
            }
        } else if (
            this.relocationTipState == g_RelocationTipState.RELOCATION_TEXTURE_STATE
        ) {
            if (this.curTipTime < this.MAXTIPTIME) {
                this.curTipTime = this.curTipTime + Insight.Time.deltaTime;
                g_ResManager.initNavSlideMaterial.setFloat(
                    "_FillAmount",
                    this.curTipTime / this.MAXTIPTIME
                );
            } else {
                this.relocationTipState = g_RelocationTipState.RELOCATION_LIGHT_STATE;
                this.curTipTime = 0.0;
                g_ResManager.initNavTxt.text = "请避免强光、反光等区域";
            }
        } else if (
            this.relocationTipState == g_RelocationTipState.RELOCATION_LIGHT_STATE
        ) {
            if (this.curTipTime < this.MAXTIPTIME) {
                this.curTipTime = this.curTipTime + Insight.Time.deltaTime;
                g_ResManager.initNavSlideMaterial.setFloat(
                    "_FillAmount",
                    this.curTipTime / this.MAXTIPTIME
                );
            } else {
                this.relocationTipState = g_RelocationTipState.RELOCATION_MOVE_STATE;
                this.curTipTime = 0.0;
                g_ResManager.initNavTxt.text = "请缓慢移动手机";
            }
        }
    },

    // 检查重定位是否成功
    UpdateRelocationSuccessView: function () {
        if (this.relocationTipState == g_RelocationTipState.RELOCATION_MOVE_STATE ||
            this.relocationTipState == g_RelocationTipState.RELOCATION_TEXTURE_STATE || 
            this.relocationTipState == g_RelocationTipState.RELOCATION_LIGHT_STATE) 
        {
            this.relocationTipState = g_RelocationTipState.RELOCATION_SUCCESS_STATE;

            g_ResManager.initNavIconTrans.gameObject.setActive(false);
            g_ResManager.initNavSlideTrans.gameObject.setActive(false);
            g_ResManager.relocationTipTrans.gameObject.setActive(false);
            g_ResManager.initNavOKTrans.gameObject.setActive(true);
            this.curSuccessTipTime = 0.0;
        }

        if (this.relocationTipState == g_RelocationTipState.RELOCATION_SUCCESS_STATE) {
            //定位成功
            if (this.curSuccessTipTime < this.TRACKSUCCESSTIME) {
                this.curSuccessTipTime = this.curSuccessTipTime + Insight.Time.deltaTime;
            } else {
                this.relocationTipState = g_RelocationTipState.RELOCATION_IDLE_STATE;
                this.curSuccessTipTime = 0.0;
                g_ResManager.initNavOKTrans.gameObject.setActive(false);
                g_ResManager.initNavPanelTrans.gameObject.setActive(false);
                g_ResManager.relocationTipTrans.gameObject.setActive(false);

                //打开路径箭头
                this.showPathArrow = true;

            }
        }
    },

    //计算离终点距离
    GetNavigationEndDistance: function (mapPaths, curPosition, floorId) {
        var tempDistance = 0.0;
        var firstDistance = true;
        if (mapPaths.length > 0 && mapPaths != undefined) {
            var len = mapPaths.length;
            for (var k = 0; k < len; k++) {
                var v = mapPaths[k];
                var splitPath = v;
                if (splitPath != undefined) {
                    var mapPoints = splitPath.mapPoints;
                    if (mapPoints != undefined) {
                        for (var k1 = 0; k1 < mapPoints.length; k1++) {
                            var v1 = mapPoints[k1];
                            //不处理走过的路
                            if (v1.isPass == 1) continue;
                            //考虑同一层距离
                            if (v1.floorId == floorId) {
                                var position = new Insight.Vector3(v1.realSpaceCoords[0], v1.realSpaceCoords[1], v1.realSpaceCoords[2]);
                                var betweenDist = 0.0;
                                if (firstDistance) {
                                    firstDistance = false;
                                    betweenDist = GetXZDistance(position, curPosition);
                                } else {
                                    if (k1 + 1 < mapPoints.length) {
                                        var nextPoint = mapPoints[k1 + 1];
                                        var nexPosition = new Insight.Vector3(nextPoint.realSpaceCoords[0], nextPoint.realSpaceCoords[1], nextPoint.realSpaceCoords[2]);
                                        betweenDist = GetXZDistance(position, nexPosition);
                                    }
                                }
                                tempDistance = tempDistance + betweenDist;
                            }
                        }
                    }
                }
            }
        }
        return tempDistance;
    },

    //更新箭头所在的位置
    UpdateNavigationIndicatorPosition: function (mapPaths, navAction, arrowTrans, floorId) {
        if (mapPaths.length > 0 && mapPaths != undefined) {
            var len = mapPaths.length;
            for (var k = 0; k < len; k++) {
                var v = mapPaths[k];
                var splitPath = v;
                if (splitPath != undefined) {
                    var mapPoints = splitPath.mapPoints;
                    if (mapPoints != undefined) {
                        for (var k1 = 0; k1 < mapPoints.length; k1++) {
                            var v1 = mapPoints[k1];

                            //不处理走过的路
                            if (v1.isPass == 1) continue;
                            if (v1.turnInfo.navAction == navAction && v1.floorId == floorId) {
                                if (arrowTrans != undefined) {
                                    arrowTrans.position = new Insight.Vector3(v1.realSpaceCoords[0], v1.realSpaceCoords[1], v1.realSpaceCoords[2]);
                                    /*if (k1 + 1 < mapPoints.length) {
                                        var nextPoint = mapPoints[k1 + 1];
                                        var lookAim = new Insight.Vector3(nextPoint.realSpaceCoords[0], nextPoint.realSpaceCoords[1], nextPoint.realSpaceCoords[2]);
                                        arrowTrans.lookAt(lookAim, Insight.Vector3.up);
                                        Insight.Debug.Log("js call update navigation look at next point ");
                                    }*/
                                    //  Fw_Event_MakeToast("进入右转箭头显示 " + v1.isPass + " " + navAction + " " + v1.navPointTurnInfo.navAction + " " + v1.realSpaceCoords[0] + " " + v1.realSpaceCoords[1] + " " + v1.realSpaceCoords[2] + " " + v1.rotation[0] + " " + v1.rotation[1] + " " + v1.rotation[2] + " " + v1.rotation[3], 3);
                                    arrowTrans.rotation = new Insight.Quaternion(v1.rotation[0], v1.rotation[1], v1.rotation[2], v1.rotation[3]);
                                    //Insight.Debug.Log("js call nav action " + navAction + " " + v1.rotation[0] + " " + v1.rotation[1] + " " + v1.rotation[2] + " " + v1.rotation[3]);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    },

    // 计算路径点,直接取算法数据
    CalculateArrowPath: function (splitPaths, floorId) {
        var splitPositionList = [];
        if (splitPaths == undefined) {
            return splitPositionList;
        }

        var len = splitPaths.length; //Fw_Table_GetLength(splitPaths);
        if (len == 0) {
            return splitPositionList;
        }

        //先存储所有的3d信息
        var allPositionList = []; //{};
        var index = 0;

        //for k,v in ipairs(splitPaths) do
        for (var k = 0; k < len; k++) {
            var v = splitPaths[k];
            var splitPath = v;
            if (splitPath != undefined) {
                var mapPoints = splitPath.mapPoints;
                if (mapPoints != undefined) {
                    //for k1,v1 in ipairs(mapPoints) do
                    for (var k1 = 0; k1 < mapPoints.length; k1++) {
                        var v1 = mapPoints[k1];
                        //如果已经经过不处理
                        if (v1.isPass == 1) continue;
                        //只画本层的箭头
                        if (v1.floorId == floorId) {
                            //index 从0开始
                            allPositionList[index] = new Insight.Vector3(v1.realSpaceCoords[0], v1.realSpaceCoords[1], v1.realSpaceCoords[2]);
                            index = index + 1;
                        }
                    }
                }
            }
        }
        return allPositionList;

        /*
      var allPointsLen = allPositionList.length; //Fw_Table_GetLength(allPositionList);
      if (allPointsLen < 1) {
        return splitPositionList;
      }
  
      //显示终点箭头位置
      var endPointLen = allPointsLen;
      var startPosition = allPositionList[0];
  
      // 从第二个点开始
        for (i = 1; i < endPointLen; i++) {
            var endPosition = allPositionList[i];
            while (Insight.Vector3.Distance(startPosition, endPosition) > this.MINNAVDISTANCE) {
                var endToStartPos = new Insight.Vector3();
                endToStartPos.copy(endPosition).sub(startPosition);
                var direction = new Insight.Vector3();
                direction.copy(endToStartPos).normalize();
                var tmp3d = new Insight.Vector3();
                var splitPosition = new Insight.Vector3();
                splitPosition.copy(startPosition).add(tmp3d.copy(direction).multiplyScalar(this.MINNAVDISTANCE));
                var arrowPosition = new Insight.Vector3(splitPosition.x, splitPosition.y, splitPosition.z);
                splitPositionList.push(arrowPosition);
                startPosition = splitPosition;
            }
        }
      return splitPositionList;*/
    },
    //找到所有的拐弯点
    GetTurnPointIndexList: function (positionList) {
        var len = positionList.length; //Fw_Table_GetLength(positionList);
        var turnPoints = []; //{};
        if (len < 2) {
            return turnPoints;
        }

        for (var i = 1; i < len; i++) {
            if (i + 1 < len) {
                var subPoint1 = new Insight.Vector3();
                subPoint1.copy(positionList[i]).sub(positionList[i - 1]).normalize(); // = (positionList[i] - positionList[i-1]).normalized;
                var subPoint2 = new Insight.Vector3();
                subPoint2.copy(positionList[i + 1]).sub(positionList[i]).normalize(); // = (positionList[i+1] - positionList[i]).normalized;
                if (subPoint1.dot(subPoint2) > 0.6) {
                    turnPoints.push(i);
                }
            }
        }
        return turnPoints;
    },
    //close view
    CloseView: function () {
        g_ResManager.navigationPanelTrans.gameObject.setActive(false);
        g_ResManager.navEndTrans.gameObject.setActive(false);

        //退出隐藏图标
        var childLen = g_ResManager.arrowRootTrans.childCount;
        for (i = 0; i <= childLen - 1; i++) {
            var childTrans = g_ResManager.arrowRootTrans.getChild(i);
            childTrans.gameObject.setActive(false);
        }

        g_ResManager.upstairTipTrans.gameObject.setActive(false);
        g_ResManager.downstairTipTrans.gameObject.setActive(false);
        g_ResManager.enterstairTipTrans.gameObject.setActive(false);
        g_ResManager.initNavPanelTrans.gameObject.setActive(false);
        g_ResManager.relocationTipTrans.gameObject.setActive(false);
        g_ResManager.navRoadsignArrowTrans.gameObject.setActive(false);
        g_ResManager.navUpstairArrowTrans.gameObject.setActive(false);
        g_ResManager.navDownstairArrowTrans.gameObject.setActive(false);
        g_ResManager.navArrowImageTrans.gameObject.setActive(false);

        this.relocationTipState = g_RelocationTipState.RELOCATION_IDLE_STATE;
        this.startEnterLeft = false;
        this.startEnterRight = false;
        this.startEnterDownStair = false;
        this.startEnterUpStair = false;
    },
    //close
    Close: function () {
        this.isEnabled = false;
        if (g_ResManager == undefined) {
            return;
        }

        if (g_ResManager.navigationPanelTrans == undefined) {
            return;
        }
        this.CloseView();

        Insight.Debug.Log("js call exit game navigation state");
        //不需要通知native
        //  Fw_Event_CloseNavigation();
    },
    GetState: function () {
        return g_GameStatus.GAME_STATUS_NAVIGATION;
    },
    // Update is called once per frame
    Update: function () { }
});

function GetXZDistance(startPos, endPos) {
    var betweenPos = new Insight.Vector3();
    betweenPos.copy(endPos).sub(startPos);
    return Math.sqrt(betweenPos.x * betweenPos.x + betweenPos.z * betweenPos.z);
}

//Return the script module
GameNavigationState;
