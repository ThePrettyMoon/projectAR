var g_MapPoiState = {
    EN_STATE_UNKNOWN : -1,
    EN_STATE_OUT_OF_SIGHT : 0,   //不在视野范围内
    EN_STATE_ICON : 1,  //r3
    EN_STATE_PREVIEW_INVIEW : 2, //r2  在视野范围内
    EN_STATE_PREVIEW_OUTVIEW : 3,   //r2 不在视野范围内
    EN_STATE_UI : 4,   //r1
    EN_STATE_EXIT_POI : 5,   //离开poi区域

};
var g_MapPoiDatas = [];
//poi 高度统一,天花板是0.0
//var g_PoiHeight = -2.0;
function MapPoiEventDispatcher(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
MapPoiEventDispatcher.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.distance = 0
        this.radius3 = 0
    },
    // Update is called once per frame
    Update: function()
    {

    },
    Init: function()
    {
        g_EventManager.AddListener(EventType.EVENT_TYPE_POI_LIST,GetPoiEventHandler);
        this.enabled = true;
    },
    Close: function()
    {
        g_EventManager.RemoveListener(EventType.EVENT_TYPE_POI_LIST,GetPoiEventHandler);
        this.enabled = false;
    },
    //遍历更新
    Update: function()
    {
        if(! this.enabled )
        {
            return;
        }
        
        if(g_MapPoiDatas == undefined)
        {
            return;
        }
        
        var len = g_MapPoiDatas.length;//Fw_Table_GetLength(g_MapPoiDatas);
        if(len == 0)
        {
            return;
        }

        var curPoint = g_MapData.GetCurrentMapPoint();	
        
        for(i = 0; i < len; i ++)
        {
            var poiInfo = g_MapPoiDatas[i];
            var mainCamera = g_ResManager.mainCamera;
            if(mainCamera == undefined)
            {
                return;
            }

            var camPosition = g_ResManager.mainCameraTrans.position;
            var distance = GetXZPlaneDistance(camPosition,poiInfo.position);
            
            var inCameraView = IsWorldPositionInCameraView(mainCamera,poiInfo.position);
            if(distance > poiInfo.r3)
            {
                poiInfo.currentState = g_MapPoiState.EN_STATE_OUT_OF_SIGHT;
            }
            else if(distance >= poiInfo.r2 && distance < poiInfo.r3)
            {
                poiInfo.currentState = g_MapPoiState.EN_STATE_ICON;
            }
            else if(distance >= poiInfo.r1 && distance < poiInfo.r2)
            {
                if(inCameraView)
                {
                    poiInfo.currentState = g_MapPoiState.EN_STATE_PREVIEW_INVIEW;
                }
                else
                {
                    poiInfo.currentState = g_MapPoiState.EN_STATE_PREVIEW_OUTVIEW;
                }
            }
            else
            {
                poiInfo.currentState = g_MapPoiState.EN_STATE_UI;
            }
            
            if(poiInfo.previousState != poiInfo.currentState)
            {
                // g_EventManager.SendEvent(EventType.EVENT_TYPE_POI_EXIT, poiInfo.id, poiInfo.name, inCameraView, poiInfo.previousState, poiInfo.floorId);
                // g_EventManager.SendEvent(EventType.EVENT_TYPE_POI_ENTER, poiInfo.id, poiInfo.name, inCameraView, poiInfo.currentState, poiInfo.floorId);
                g_EventManager.SendEvent(EventType.EVENT_TYPE_POI_EXIT, poiInfo.id, poiInfo.name, inCameraView, poiInfo.previousState,poiInfo.currentState, poiInfo.floorId);
                g_EventManager.SendEvent(EventType.EVENT_TYPE_POI_ENTER, poiInfo.id, poiInfo.name, inCameraView, poiInfo.previousState,poiInfo.currentState, poiInfo.floorId);
            }
            else
            {
                //Insight.Debug.Log("Update poi " ..poiInfo.name .. " "..poiInfo.currentState .." "..distance);
                g_EventManager.SendEvent(EventType.EVENT_TYPE_POI_UPDATE, poiInfo.id, poiInfo.name, inCameraView, poiInfo.currentState, poiInfo.floorId);
            }
            
            // 离开poi区域需要多帧判断
            if(poiInfo.currentState != poiInfo.previousState && poiInfo.currentState == g_MapPoiState.EN_STATE_OUT_OF_SIGHT)
            {
                //log("exit experience " ..poiInfo.name .. " "..poiInfo.currentState .." "..tostring(inCameraView));
                g_EventManager.SendEvent(EventType.EN_STATE_EXIT_POI, poiInfo.id, poiInfo.name, inCameraView, poiInfo.currentState, poiInfo.floorId);
            }
            poiInfo.previousState = poiInfo.currentState;
        }
    }
});

//初始化poilist列表, 名称还不能相同
function GetPoiEventHandler()
{
	var mapPoiList = g_MapData.GetPoiList();
	
    if(mapPoiList == undefined ) 
    {
		return;
	}

	var len = mapPoiList.length;//Fw_Table_GetLength(mapPoiList);
	
    if(len == 0)
    {
		return;
	}


    for (i = 0; i < len; i++) {
        var curPoiInfo = mapPoiList[i];
        var property = curPoiInfo.properties;
        var pointInfo = curPoiInfo.mapPoint;
        var poiHeight = pointInfo.realSpaceCoords[1]+g_ResManager.heightAboveGround;
        // var poiHeight = -2.0;
        // console.error("pointInfo.floorId+"+pointInfo.floorId);
        // if (pointInfo.floorId == "2") { //兼容2楼处理，高度y = 0 在天花板
        //     poiHeight = 2.0;
        // }
        // if(pointInfo.floorId == "-2")
        // {
        //     poiHeight = -1.9 +1.6;
        // }
        //-只显示本楼层poi
        //if (pointInfo.floorId == curFloorId)
        // {
        //统一采用geoid 
        g_MapPoiDatas[i] = {
            id: property.id, 
            name: property.name, 
            anchor: property.x_anchor,
            r3: ParseNumberString(property.x_name_radius), 
            r2: ParseNumberString(property.x_preview_content_radius),
            r1: ParseNumberString(property.x_content_radius),
            eventId: property.x_content_id, 
            position: new Insight.Vector3(pointInfo.realSpaceCoords[0],poiHeight, pointInfo.realSpaceCoords[2]), 
            currentState: g_MapPoiState.EN_STATE_UNKNOWN,
            previousState: g_MapPoiState.EN_STATE_UNKNOWN, 
            floorId: pointInfo.floorId, 
            snapshotId: property.x_content_snapshot_id
        };
        //添加一个默认值
        if (g_MapPoiDatas[i].r3 == 0) {
            g_MapPoiDatas[i].r3 = 5;
        }
        // }
        //Insight.Debug.Log("lua call ".." id == "..tostring(g_MapPoiDatas[i].id).."r3 == "..tostring(g_MapPoiDatas[i].r3).." r2 "..tostring(g_MapPoiDatas[i].r2).." r1 "..tostring(g_MapPoiDatas[i].r1));
    }
}
// string to int
function ParseNumberString(numStr)
{
    // if(type(numStr) == "userdata")
    // {
	// 	return 0;
	// }
	
    if(typeof numStr == "number")
    {
		return numStr;
	}
	

	
    if(numStr == undefined || numStr == "")
    {
		return 0;
	}
	return tonumber(numStr);
}
//计算xz平面距离
function GetXZPlaneDistance(startPos,endPos)
{
    var betweenPos = new Insight.Vector3();
    betweenPos.copy(endPos).sub(startPos);
	return Math.sqrt(betweenPos.x * betweenPos.x + betweenPos.z * betweenPos.z);
}
// 计算是否在相机视野范围内
function IsWorldPositionInCameraView(camera,worldPosition)
{
	var viewPort = camera.worldToViewportPoint(worldPosition);
    var normDirection = new Insight.Vector3();
    normDirection.copy(worldPosition).sub(camera.transform.position);
	var dot = camera.transform.forward.dot(normDirection);
	return dot>0 && viewPort.x >=0 && viewPort.x <=1 && viewPort.y >=0 && viewPort.y <= 1;
}

//Return the script module
MapPoiEventDispatcher