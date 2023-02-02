function TouchIconButton(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
TouchIconButton.prototype = Object.assign(Object.create(Object.prototype), {
   
    setPoiItem:function(poiItem)
    {
        this.button = this.gameObject.getComponent("Button");  
        this.poiItem = poiItem;
    },
    OnPointerUp: function () {

        if (this.gameObject.name != "touchbutton") {
            //Fw_Event_MakeToast("属性名对应失败",1);
            return;
        }
        var poiItem = this.poiItem;
        if (poiItem == undefined || poiItem == null) {
            //Fw_Event_MakeToast("poiItem丢失",1);
            return;
        }
        if (this.button.interactable == false) {
            Fw_Event_MakeToast("按钮限制点击，请远离5米后重新靠近",1);
            return;
        }


        
        //下载未完成 需要进行提示
        if (g_MapPoiController.PublicQueryArProductLocalState(poiItem.eventId, poiItem.snapshotId) == 0) 
        {
            //Fw_Event_MakeToast("资源下载中稍后再试",3);
            return;
        }
        //----判断点击按钮时的状态
        //预览半径点击:将子事件名字改为走进看看，同时记录该点击的状态，如果进入到体验半径就直接加载子事件
        if(poiItem.currentState == g_MapPoiState.EN_STATE_PREVIEW_INVIEW)
        {
            //Fw_Event_MakeToast("预览半径点击touchbutton",1);
            if ( poiItem.isClickedTouchButton == undefined|| poiItem.isClickedTouchButton == null) {
                poiItem.isClickedTouchButton = true;
                poiItem.uiIcon.find("nametxt").gameObject.getComponent("Text").text = "走进看看";
            }
        }
        //在体验半径点击：加载子事件
        else if (poiItem.currentState == g_MapPoiState.EN_STATE_UI)
        {
            //Fw_Event_MakeToast("体验半径点击touchbutton",1);
            if (poiItem.eventId == undefined) {
                //Fw_Event_MakeToast("poiItem.eventId ==undefined",1);
                return;
            }
            if (poiItem.algMode == undefined) {
                //Fw_Event_MakeToast("poiItem.algMode ==undefined",1);
                return;
            }
            //进入状态不能进入poi
            if (poiItem.state == g_PoiDownloadState.EN_STATE_ENTER) {
                //Fw_Event_MakeToast("poiItem.state ==g_PoiDownloadState.EN_STATE_ENTER",1);
                return;
            }
            if(poiItem.algMode == "swap")
            {
                g_currentSelectItem = poiItem;
            }
            //hsh ： 改为统一流程
            if(poiItem.algMode == "overlay" || poiItem.algMode == "unchange"||poiItem.algMode == "swap")
            {
                Fw_Event_MakeToast("开始加载",1);
                // 进入poi 检查加载场景
                //var queryState = g_MapPoiController.QueryArProductLocalState(poiItem.eventId, poiItem.snapshotId)
                var queryState =  g_MapPoiController.PublicQueryArProductLocalState(poiItem.eventId, poiItem.snapshotId);
                if(queryState == 1 )
                {
                    poiItem.state = g_PoiDownloadState.EN_STATE_ENTER;
                    Fw_Event_LoadPoiData(poiItem.eventId, poiItem.snapshotId,1);
                    // send event
                    g_EventManager.SendEvent(EventType.EVENT_TYPE_ENTER_POI,poiItem.id);
                    //标识进入
                    g_InPoiState = true;               
                }
            }
            else
            {
                //Fw_Event_MakeToast("点击异常情况",1);
            }
        }
    },
});

//Return the script module
TouchIconButton