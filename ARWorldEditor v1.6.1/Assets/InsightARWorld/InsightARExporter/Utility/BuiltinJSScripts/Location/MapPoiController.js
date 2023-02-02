var g_PoiDownloadState = {
    EN_STATE_UNKNOWN : -1,
    EN_STATE_START : 0, //开始下载
    EN_STATE_DOWNLOADING : 1, //下载中
    EN_STATE_FINISH : 2,   //下载完成
    EN_STATE_ENTER : 3,  // 进入poi
    EN_STATE_EXIT : 4,   //退出poi
}
var g_MapPoiItems = [];
var g_EnablePoiView = false;
var g_currentSelectItem;
//记录文件下载状态
var g_ProductStatus = [];

function MapPoiController(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
/*
    poiItem.isClickedTouchButton 
    该属性针对touchbutton按钮，已经点击过同时又进入了体验半径，此时无需再点击，直接加载资源
 */
MapPoiController.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {

    },
    PublicQueryArProductLocalState:function(cid, sid)
    {
        return QueryArProductLocalState(cid, sid)
    },
    // Update is called once per frame
    Update: function () {

    },
    //切后台回来或者退出挂起场景返回触发
    OnApplicationResume: function () {
        Insight.Debug.Log("js call on application resume");
        //退出poi内容
        if (g_currentSelectItem != undefined) {
            if (g_currentSelectItem.eventId != undefined) {
                if (g_currentSelectItem.state == g_PoiDownloadState.EN_STATE_ENTER && g_currentSelectItem.algMode == "swap") {
                    Insight.Debug.Log("js call unload poi scene " + g_currentSelectItem.eventId);
                    g_currentSelectItem.state = g_PoiDownloadState.EN_STATE_EXIT;
                    g_EventManager.SendEvent(EventType.EVENT_TYPE_EXIT_POI, g_currentSelectItem.id);
                    //标识退出
                    g_InPoiState = false;
                }
            }
        }
    },
    // 采用MapPoiController["EnterPoiState"] 参数会不断发生变化，原因待查
    // 全局函数正常
    Init: function () {
        g_EnablePoiView = true;

        g_EventManager.AddListener(EventType.EVENT_TYPE_POI_ENTER, EnterPoiViewHandler);
        g_EventManager.AddListener(EventType.EVENT_TYPE_POI_UPDATE, UpdatePoiViewHandler);
        g_EventManager.AddListener(EventType.EVENT_TYPE_POI_EXIT, ExitPoiViewHandler);

        g_ResManager.poiPanelTrans.gameObject.setActive(true);
        Insight.Navigation.OnPoiStatusDataChanged(this, this.onPoiStatusDataChanged);
    },
    onPoiStatusDataChanged: function (poiStatusStr) {
        Insight.Debug.Log("js call on poi status data changed " + poiStatusStr);
        if (poiStatusStr == undefined || poiStatusStr == "") {
            return;
        }

        var poiStatusData = JSON.parse(poiStatusStr); //g_JSON:decode(mapNavigationString);

        if (poiStatusData == undefined) {
            return;
        }

        var poiProductItem = FindPoiItemById(g_ProductStatus, poiStatusData.cid);
       

        if (poiProductItem == undefined) {
            g_ProductStatus.push({ 
                id: poiStatusData.cid, 
                downloadResult: poiStatusData.downloadResult, 
                needUpdate:poiStatusData.needUpdate,
                msg:poiStatusData.msg
            });
        } else {
            poiProductItem.id = poiStatusData.cid;
            poiProductItem.downloadResult = poiStatusData.downloadResult;
            poiProductItem.needUpdate = poiStatusData.needUpdate;
            poiProductItem.msg = poiStatusData.msg;
        }
    },

    //隐藏显示
    Pause: function () {
        g_EnablePoiView = false;
        //关闭poipanel
        g_ResManager.poiPanelTrans.gameObject.setActive(false);

        //Fw_Event_MakeToast("POI显示暂停", 2);

        //for key,value in pairs(g_MapPoiItems) do
        for (var key in g_MapPoiItems) {
            var value = g_MapPoiItems[key];
            if (value != undefined) {
                if (value.uiIcon != undefined) {
                    value.uiIcon.gameObject.setActive(false);
                }

                if (value.previewMissIcon != undefined) {
                    value.previewMissIcon.gameObject.setActive(false);
                }

                if (value.closerIcon != undefined) {
                    value.closerIcon.gameObject.setActive(false);
                }

                if (value.interactionPanel != undefined) {
                    value.interactionPanel.gameObject.setActive(false);
                }

                if (value.previewModel != undefined) {
                    value.previewModel.gameObject.setActive(false);
                }
            }
        }
    },

    // 继续显示
    Resume: function () {
        g_EnablePoiView = true;
        g_ResManager.poiPanelTrans.gameObject.setActive(true);
      //  Fw_Event_MakeToast("POI显示继续", 2);
    },
    OnDisable: function () {
        // Exit
        this.Close();
    },
    // close
    Close: function () {
        g_EventManager.RemoveListener(EventType.EVENT_TYPE_POI_ENTER, EnterPoiViewHandler);
        g_EventManager.RemoveListener(EventType.EVENT_TYPE_POI_UPDATE, UpdatePoiViewHandler);
        g_EventManager.RemoveListener(EventType.EVENT_TYPE_POI_EXIT, ExitPoiViewHandler);

        g_EnablePoiView = false;
        if (g_MapPoiItems.length == 0) {
            return;
        }

        //资源回收
        g_MapPoiItems = [];
        g_EnablePoiView = false;
    }
});


function ExitPoiViewHandler(id, name, inView, previousState,currentState, floorId) {
    var curPoint = g_MapData.GetCurrentMapPoint();
    if (curPoint == undefined) {
        return;
    }

    if (g_MapPoiItems == undefined || g_MapPoiItems.length == 0) {
        return;
    }
    var poiItem = FindPoiItemById(g_MapPoiItems, id);
    if (poiItem == undefined) {
        return;
    }


    //Insight.Debug.Log("js call exitPoiView poi view " + id + "  " + name + "  " + "  " + tostring(inView) + "  " + state);
    ExitPoiView(poiItem, inView, previousState,currentState);
}

//查询文件下载状态
function QueryArProductLocalState(cid, sid) {
    Insight.Debug.Log("js call query product state enter " + cid + " " + sid);
    var queryLocalState = 0;
    var queryItem = FindPoiItemById(g_ProductStatus, cid);
    if (queryItem != undefined) {
        //已下载且不需要更新
        if (queryItem.needUpdate == "0" && queryItem.downloadResult == "1") {
            queryLocalState = 1;
        }
        if(queryItem.needUpdate == "-1")
        {
            Fw_Event_MakeToast(queryItem.msg, 2);
        }
        Insight.Debug.Log("js call query product state defined " + cid + " " + sid);
    } else {
        //Fw_Event_MakeToast("内容未加载 cid = "+cid, 2);
        Insight.Debug.Log("js call query product state undefined " );
    }
    return queryLocalState;
}

    //返回poilist info
function GetPoiListHandler() {
   // Insight.Debug.Log("js call get poi list handler ");
    var mapPoiList = g_MapData.GetPoiList();

    if (mapPoiList == undefined) {
        return;
    }

    var len = mapPoiList.length;//Fw_Table_GetLength(mapPoiList);

    if (len == 0) {
        return;
    }

    for (var i = 0; i < len; i++) {
        var mapPoiInfo = mapPoiList[i];
        var pointInfo = mapPoiInfo.mapPoint;
        var property = mapPoiInfo.properties;
        var poiHeight = pointInfo.realSpaceCoords[1]+g_ResManager.heightAboveGround;
        // console.log("pointInfo.floorId+"+pointInfo.floorId+" poiHeight:"+(pointInfo.realSpaceCoords[1]+g_ResManager.heightAboveGround));
        // console.log("y:"+pointInfo.realSpaceCoords[1]+" heightAboveGround:"+g_ResManager.heightAboveGround);
        // if (pointInfo.floorId == "2") { //兼容2楼处理，高度y = 0 在天花板
        //     poiHeight = 2.0;
        // }
        // if(pointInfo.floorId == "-2")
        // {
        //     poiHeight = -1.9+1.6;
        // }
        //统一用geo id  alg mode "swap"  挂起， "overlay"  叠加  "unchange" 不变  "unsupport" 不支持
        g_MapPoiItems[i] = {
            id: property.id, 
            name: property.name, 
            position: new Insight.Vector3(pointInfo.realSpaceCoords[0],poiHeight, pointInfo.realSpaceCoords[2]), 
            uiIcon: undefined, 
            previewModel: undefined, 
            previewMissIcon: undefined, 
            closerIcon: undefined, 
            interactionPanel: undefined, 
            eventId: property.x_content_id,
            state: g_PoiDownloadState.EN_STATE_START, 
            algMode: property.x_content_alg_mode, 
            snapshotId: property.x_content_snapshot_id,
            virtual:property.x_virtual,
        };
      
        if (g_MapPoiItems[i].snapshotId == undefined) {
            g_MapPoiItems[i].snapshotId = "0";
        }
    }
}

    // enter 
function EnterPoiViewHandler(id, name, inView, previousState, currentState,floorId) {

    var curPoint = g_MapData.GetCurrentMapPoint();
    if (curPoint == undefined) {
        return;
    }

    if (g_MapPoiItems == undefined || g_MapPoiItems.length == 0) {
        GetPoiListHandler();
        return;
    }

    var poiItem = FindPoiItemById(g_MapPoiItems, id);
    if (poiItem == undefined) {
        return;
    }

    //是否已经初始化poi.uiIcon 涉及后续的场景下载
    //避免在信息半径外初始（保证在信息半径中可见）
    if(poiItem.uiIcon == undefined&&currentState != g_MapPoiState.EN_STATE_OUT_OF_SIGHT)
    {
        //Fw_Event_MakeToast("state = "+currentState,1);
        PoiItemUIInit(poiItem)
    }

    //如果不在同一楼层或者关闭显示，需要隐藏UI
    if (curPoint.floorId != floorId || !g_EnablePoiView) {
        if (poiItem.uiIcon != undefined) {
            poiItem.uiIcon.gameObject.setActive(false);
        }

        if (poiItem.closerIcon != undefined) {
            poiItem.closerIcon.gameObject.setActive(false);
        }
        return;
    }

    //Insight.Debug.Log("js call enter poi view " + id + "  " + name + "  " + "  " + tostring(inView) + "  " + state);
    EnterPoiView(poiItem, inView, previousState,currentState);

}



//update 
function UpdatePoiViewHandler( id,name,inView,state,floorId)
{
    var curPoint = g_MapData.GetCurrentMapPoint();
    if (curPoint == undefined) {
        return;
    }

    if(g_MapPoiItems == undefined || g_MapPoiItems.length == 0)
    {
		GetPoiListHandler();
		return;
	}
	var poiItem = FindPoiItemById(g_MapPoiItems,id);
    if(poiItem == undefined)
    {
		return;
    }

    // if(poiItem.uiIcon != undefined && state != g_MapPoiState.EN_STATE_OUT_OF_SIGHT)
    // {
    //     if( poiItem.uiIcon.gameObject.activeSelf == false)  poiItem.uiIcon.gameObject.setActive(true);
    // }

    if (curPoint.floorId != floorId || !g_EnablePoiView) {
        if (poiItem.uiIcon != undefined) {
            poiItem.uiIcon.gameObject.setActive(false);
        }

        if (poiItem.closerIcon != undefined) {
            poiItem.closerIcon.gameObject.setActive(false);
        }
        return;
    }
   
	
	UpdatePoiView(poiItem,inView,state);
}


function PoiItemUIInit(poiItem)
{
    poiItem.uiIcon = g_PoolManager.Spawn("poiUI");
    if(poiItem.uiIcon == undefined)
    {
        return;
    }
    //是否是虚拟点位,显示对应的图片
    var isVirtualPoint = poiItem.virtual == "true";
    poiItem.uiIcon.transform.find("virtualpoint").gameObject.setActive(isVirtualPoint);
    poiItem.uiIcon.transform.find("physicpoint").gameObject.setActive(!isVirtualPoint);
    //虚拟点位新增 点击进入子事件的交互逻辑
    if (isVirtualPoint && poiItem.touchButtonSc == undefined) {
        poiItem.touchButtonSc = poiItem.uiIcon.find("touchbutton").gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/TouchIconButton.js");
        poiItem.touchButtonSc.setPoiItem(poiItem);
        poiItem.touchButtonSc.button.interactable = false
    }
    poiItem.uiIcon.gameObject.setActive(true);
    //设置显示名字
    var poiName = Fw_MixCharacters_GetCountString(poiItem.name,6);
    poiItem.uiIcon.find("nametxt").gameObject.getComponent("Text").text = poiName;
    poiItem.uiIcon.gameObject.getComponent("RectTransform").anchoredPosition3D = poiItem.position;
    //开始下载资源
    if(poiItem.eventId !=undefined)
    {
        var queryState = QueryArProductLocalState(poiItem.eventId, poiItem.snapshotId);
        //只有第一次进入才会下载,修改为只要没有下载完成，就继续调用
        if( queryState == 0)
        {
            //先检查是否有无网络，如果没有不能进入下载
            var networkState = Insight.Application.NetworkState;
            if(networkState == 0 || networkState == -1)
            {
                Fw_Event_MakeToast("网络异常，请检查网络",3);
                return;
            }
            Insight.Debug.Log("js call download poi scene " + poiItem.eventId);
            Fw_Event_LoadPoiData(poiItem.eventId, poiItem.snapshotId, 0);
            poiItem.state = g_PoiDownloadState.EN_STATE_DOWNLOADING;
        }
    }
}
//enter
function EnterPoiView(poiItem,inCameraView,previousState,currentState)
{
	//Insight.Debug.Log("js call enter poi view "+poiItem.id+"  "+poiItem.name+"  "+state+" poi " +poiItem.state);
    poiItem.currentState = currentState;
    if(currentState == g_MapPoiState.EN_STATE_ICON)
    {
      
    }
    else if(currentState == g_MapPoiState.EN_STATE_PREVIEW_INVIEW)
    {
        //从信息半径->浏览半径 按钮可点击
        if( previousState == g_MapPoiState.EN_STATE_ICON && poiItem.touchButtonSc != null && poiItem.touchButtonSc.button !=null)
        { 
            //Fw_Event_MakeToast("poiItem.touchButtonSc.button.interactable = true" +poiItem.name ,1);
            poiItem.touchButtonSc.button.interactable = true;
        }

        //从信息半径->浏览半径时，有提示
        if (previousState == g_MapPoiState.EN_STATE_ICON) {
            Fw_Event_MakeToast("附近有可体验事件,点击卡片开启体验" + "\nname = " +poiItem.name ,5);
        }
    }
    else if(currentState == g_MapPoiState.EN_STATE_PREVIEW_OUTVIEW)
    {
         //从信息半径->浏览半径时，有提示
         if (previousState == g_MapPoiState.EN_STATE_ICON) {
            Fw_Event_MakeToast("附近有可体验事件,点击卡片开启体验" + "\nname = " +poiItem.name ,5);
        }
    }
    else if(currentState == g_MapPoiState.EN_STATE_UI)
    {
		//进入状态不能进入poi
        if(poiItem.eventId ==undefined || poiItem.algMode == undefined||poiItem.state == g_PoiDownloadState.EN_STATE_ENTER) 
            return;

        //如果是默认算法则直接显示
        if(poiItem.algMode == "unchange")
        {
            // 进入poi 检查加载场景
            var queryState = QueryArProductLocalState(poiItem.eventId, poiItem.snapshotId);
            if(queryState == 1 )
            {
                poiItem.state = g_PoiDownloadState.EN_STATE_ENTER;
                Fw_Event_LoadPoiData(poiItem.eventId, poiItem.snapshotId,1);
                // send event
                g_EventManager.SendEvent(EventType.EVENT_TYPE_ENTER_POI,poiItem.id);
                //标识进入
                g_InPoiState = true;      
                poiItem.uiIcon.gameObject.setActive(false);
                g_ResManager.world_canvasTrans.gameObject.setActive(false);
            }
        }
        //如果是挂起算法或者叠加算法，需要点击才可以进入场景中
        else if (poiItem.algMode == "overlay" || poiItem.algMode == "swap") 
        {
            //保证只有在点击后才可加载
            if (poiItem.isClickedTouchButton == undefined || poiItem.isClickedTouchButton == null)
                return;
            if(poiItem.algMode == "swap")
            {
                g_currentSelectItem = poiItem;
            }
            //重置状态
            poiItem.uiIcon.gameObject.setActive(true)
            poiItem.isClickedTouchButton = null;
            //进入poi 检查加载场景
            var queryState = QueryArProductLocalState(poiItem.eventId, poiItem.snapshotId);
            if(queryState == 1 )
            {
                poiItem.state = g_PoiDownloadState.EN_STATE_ENTER;
                Fw_Event_LoadPoiData(poiItem.eventId, poiItem.snapshotId,1);
                // send event
                g_EventManager.SendEvent(EventType.EVENT_TYPE_ENTER_POI,poiItem.id);
                //标识进入
                g_InPoiState = true;     
                if(poiItem.algMode == "overlay") 
                {
                    poiItem.uiIcon.gameObject.setActive(false)
                    //叠加需要隐藏poi卡片
                    g_ResManager.world_canvasTrans.gameObject.setActive(false);
                }      
            }
        }
	}
    else if(currentState == g_MapPoiState.EN_STATE_OUT_OF_SIGHT)
    {
		//超出poi范围,资源回收
        if(poiItem.uiIcon!=undefined)
        {
            g_PoolManager.DeSpawn("poiUI",poiItem.uiIcon);
            if (poiItem.touchButtonSc != undefined) {
                poiItem.touchButtonSc.setPoiItem(null)
                poiItem.touchButtonSc = undefined;
            }
            poiItem.uiIcon = undefined;
        }
		
        if(poiItem.closerIcon !=undefined)
        {
			g_PoolManager.DeSpawn("poiCloser",poiItem.closerIcon);
			poiItem.closerIcon = undefined;
		}
		
        if(poiItem.interactionPanel !=undefined)
        {
			g_PoolManager.DeSpawn("poiInteraction",poiItem.interactionPanel);
			poiItem.interactionPanel = undefined;
		}
		
        if(poiItem.previewModel !=undefined)
        {
			g_PoolManager.DeSpawn("poiPreview",poiItem.previewModel);
			poiItem.previewModel = undefined;
		}
		
		//退出poi内容
        if(poiItem.eventId !=undefined)
        {
			//如果是切换状态不需要发送退出消息
            //Fw_Event_MakeToast("离开信息半径" + "\nname = " +poiItem.name +" \nstate = "+poiItem.state +"\nswap = "+poiItem.algMode ,1);
            if(poiItem.state == g_PoiDownloadState.EN_STATE_ENTER && poiItem.algMode != "swap")
            {
				Insight.Debug.Log("js call unload poi scene " + poiItem.eventId);
                poiItem.state = g_PoiDownloadState.EN_STATE_EXIT;
                Fw_Event_UnloadPoiData(poiItem.eventId, poiItem.snapshotId, "0");
				g_EventManager.SendEvent(EventType.EVENT_TYPE_EXIT_POI,poiItem.id);
				//标识退出
				g_InPoiState = false;
                g_ResManager.world_canvasTrans.gameObject.setActive(true);
			}
		}
	}
}
//update 
function UpdatePoiView(poiItem, inCameraView, state) {
    if (state == g_MapPoiState.EN_STATE_ICON) {
        if(poiItem.state != g_PoiDownloadState.EN_STATE_ENTER)
        {
            if (poiItem.uiIcon != undefined) {
                var isInFront = GetUIIsInCameraFront(g_ResManager.mainCamera, poiItem.position);
                if (isInFront) {
                    if (!poiItem.uiIcon.gameObject.activeSelf) {
                        poiItem.uiIcon.gameObject.getComponent("RectTransform").anchoredPosition3D = poiItem.position;
                        poiItem.uiIcon.gameObject.setActive(true);
                    }
                    SetUIWorldRotationToLookAtCamera(poiItem.uiIcon, g_ResManager.mainCameraTrans.position);
                }
            }
        }
    }
    else if (state == g_MapPoiState.EN_STATE_PREVIEW_INVIEW) {
        //如果是进入状态，不能重复设置,避免一开始就位于半径内，无法点击icon的问题
        if (poiItem.state != g_PoiDownloadState.EN_STATE_ENTER) {
            if (poiItem.touchButtonSc!= undefined && poiItem.touchButtonSc.button.interactable == false) {
                poiItem.touchButtonSc.button.interactable = true;
            }

            if (poiItem.uiIcon != undefined) {
                var isInFront = GetUIIsInCameraFront(g_ResManager.mainCamera, poiItem.position);
                if (isInFront) {
                    if (!poiItem.uiIcon.gameObject.activeSelf) {
                        poiItem.uiIcon.gameObject.getComponent("RectTransform").anchoredPosition3D = poiItem.position;
                        poiItem.uiIcon.gameObject.setActive(true);
                    }
                    SetUIWorldRotationToLookAtCamera(poiItem.uiIcon, g_ResManager.mainCameraTrans.position);
                }
            }
        }

      
    }
    else if (state == g_MapPoiState.EN_STATE_PREVIEW_OUTVIEW) {
          //如果是进入状态，不能重复设置，避免一开始就位于半径内，无法点击icon的问题
          if (poiItem.state != g_PoiDownloadState.EN_STATE_ENTER) {
            if (poiItem.touchButtonSc!= undefined && poiItem.touchButtonSc.button.interactable == false) {
                poiItem.touchButtonSc.button.interactable = true;
            }
            if (poiItem.uiIcon != undefined) {
                var isInFront = GetUIIsInCameraFront(g_ResManager.mainCamera, poiItem.position);
                if (isInFront) {
                    if (!poiItem.uiIcon.gameObject.activeSelf) {
                        poiItem.uiIcon.gameObject.getComponent("RectTransform").anchoredPosition3D = poiItem.position;
                        poiItem.uiIcon.gameObject.setActive(true);
                    }
                    SetUIWorldRotationToLookAtCamera(poiItem.uiIcon, g_ResManager.mainCameraTrans.position);
                }
            }
        }
    }
    else if (state == g_MapPoiState.EN_STATE_UI) {
         //如果是进入状态，不能重复设置，避免一开始就位于半径内，无法点击icon的问题
         if (poiItem.state != g_PoiDownloadState.EN_STATE_ENTER) {
            if (poiItem.touchButtonSc!= undefined && poiItem.touchButtonSc.button.interactable == false) {
                poiItem.touchButtonSc.button.interactable = true;
            }
        }
    }
    else if (state == g_MapPoiState.EN_STATE_OUT_OF_SIGHT) {
    }

}
//exit 退出上一个状态 准备切换到当前状态
function ExitPoiView(poiItem,inCameraView,previousState,currentState)
{
	//Insight.Debug.Log("lua call exit poi view "..poiItem.id.."  "..poiItem.name.."  "..state.." poi " ..poiItem.state);
    if(previousState == g_MapPoiState.EN_STATE_ICON)
    {
        //从信息半径退出到poi外 会隐藏icon显示
        if(currentState ==g_MapPoiState.EN_STATE_OUT_OF_SIGHT && poiItem.uiIcon != undefined&&poiItem.uiIcon != null)
        {
			poiItem.uiIcon.gameObject.setActive(false);
        }   
       
       
    }
    else if(previousState == g_MapPoiState.EN_STATE_PREVIEW_INVIEW)
    { 
        if(currentState == g_MapPoiState.EN_STATE_ICON&&poiItem.touchButtonSc != undefined && poiItem.touchButtonSc!=null)
        {
            poiItem.touchButtonSc.button.interactable = false;
        }
         //按钮状态重置
         if(poiItem.virtual == "true" && currentState != g_MapPoiState.EN_STATE_UI && poiItem.uiIcon!= undefined)
         {
             var poiName = Fw_MixCharacters_GetCountString(poiItem.name,6);
             poiItem.uiIcon.find("nametxt").gameObject.getComponent("Text").text = poiName;
             poiItem.isClickedTouchButton = null;
         }
    }
    else if(previousState == g_MapPoiState.EN_STATE_PREVIEW_OUTVIEW)
    {
        if(currentState == g_MapPoiState.EN_STATE_ICON&&poiItem.touchButtonSc!= undefined && poiItem.touchButtonSc!=null)
        {
            poiItem.touchButtonSc.button.interactable = false;
        }
         //按钮状态重置
         if(poiItem.virtual == "true" && currentState != g_MapPoiState.EN_STATE_UI && poiItem.uiIcon!= undefined) 
         {
            var poiName = Fw_MixCharacters_GetCountString(poiItem.name,6);
            poiItem.uiIcon.find("nametxt").gameObject.getComponent("Text").text = poiName;
            poiItem.isClickedTouchButton = null;
         }
    }
    else if(previousState == g_MapPoiState.EN_STATE_UI)
    {
       
    }
    else if(previousState == g_MapPoiState.EN_STATE_OUT_OF_SIGHT)
    {

    }
}
//find poi item by id
function FindPoiItemById(searchTable,id)
{
    //for key,value in pairs(searchTable) do
    for(var key in searchTable)
    {
        var value = searchTable[key];
        if(value !=undefined)
        {
            if(value.id == id)
            {
				return value;
			}
		}
	}
	return undefined;
}
//设置ui position
function SetUIWorldPositionToScreenPosition(camera,uiTrans, worldPosition)
{
    if(camera == undefined || uiTrans == undefined)
    {
		return;
	}
	var screenPosition = camera.worldToScreenPoint(worldPosition);
	uiTrans.gameObject.getComponent("RectTransform").anchoredPosition3D = screenPosition;
}
//判断是否在相机前面
function GetUIIsInCameraFront(camera,worldPosition)
{
	var viewPort = camera.worldToViewportPoint(worldPosition);
    var normDirection = new Insight.Vector3();
    normDirection.copy(worldPosition).sub(camera.transform.position);
	return camera.transform.forward.dot(normDirection) >0;
}

//计算miss图标位置
function SetUIMissPositionAndRotation(camera, trans, stuffPosition) {
    if (trans == undefined) {
        return;
    }

    var rectTrans = trans.gameObject.getComponent("RectTransform");
    rectTrans.anchoredPosition3D = g_CalculateAngle.CalculatePosition(camera, trans, stuffPosition, 2001, 1125, 160, 320);
    rectTrans.localRotation = g_CalculateAngle.CalculateRotation(camera, stuffPosition);
    //trans.anchoredPosition3D = g_CalculateAngle.CalculatePosition(camera,trans,stuffPosition,2001,1125,160,320);
    //trans.localRotation = g_CalculateAngle.CalculateRotation(camera,stuffPosition);
}

//设置ui position
function SetUIWorldRotationToLookAtCamera(uiTrans, worldPosition) {
    if(uiTrans == undefined)
    {
		return;
	}

    var uiTransPos = uiTrans.position;
    var correctPos = new Insight.Vector3(2 * uiTransPos.x - worldPosition.x, 2 * uiTransPos.y - worldPosition.y, 2 * uiTransPos.z - worldPosition.z);
    uiTrans.lookAt(correctPos, Insight.Vector3.up);
}

//Return the script module
MapPoiController