//判断是否在poi场景内
var g_InPoiState = false;
var g_lastRecognitionCount = 0;
function GameLocationState(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
GameLocationState.prototype = Object.assign(Object.create(Object.prototype), {
    Awake: function()
    {

    },
    // Start is called before the first frame update
    Start: function()
    {
    },
    Enter: function()
    {
	    Insight.Debug.Log("js call Game Location State:Enter");
	    this.Init();
    },
    Execute: function()
    {
        if(!this.isEnabled)
        {
            return;
        }
        var state = this.CheckTrackState();
        this.UpdateView(state);
    },
    Exit: function()
    {
        this.Close();

        g_ResManager.locationPanelTrans.gameObject.setActive(false);
        g_ResManager.locationAudioTrans.gameObject.setActive(false);


        g_EventManager.RemoveListener(EventType.EVENT_TYPE_ENTER_POI, EnterPoiContentHandler);
        g_EventManager.RemoveListener(EventType.EVENT_TYPE_EXIT_POI, ExitPoiContentHandler);
    },

    Init: function()
    {
        this.isEnabled = true;
        this.mapEnabled = true;

      
        this.InitView();
        
        g_EventManager.AddListener(EventType.EVENT_TYPE_ENTER_POI,EnterPoiContentHandler);
        g_EventManager.AddListener(EventType.EVENT_TYPE_EXIT_POI, ExitPoiContentHandler);

        // 开启模型显示
        g_ResManager.modelRootTrans.gameObject.setActive(true);
    },
    InitView: function()
    {
        g_MapPoiController.Init();		
        g_MapLocationController.Init();
        
        //不控制mapbox视图显示
        //Fw_Event_SetMapVisibility("1");
        
        //如果在poi场景内，不能打开音乐
        if (!g_InPoiState) {
            //首次进入打开音频按钮，打开音乐，否则根据按钮状态开关音乐
            if (!g_ResManager.audioOnTrans.gameObject.activeSelf &&
                !g_ResManager.audioOffTrans.gameObject.activeSelf) {
                g_ResManager.audioOnTrans.gameObject.setActive(true);
                g_ResManager.locationAudioTrans.gameObject.setActive(true);
            } else {
                if (g_ResManager.audioOnTrans.gameObject.activeSelf) {
                    g_ResManager.locationAudioTrans.gameObject.setActive(true);
                } else {
                    g_ResManager.locationAudioTrans.gameObject.setActive(false);
                }
            }
        }
    },
    // 更新视图
    UpdateView: function(state)
    {
        if(state == 0)
        {
            if(!this.mapEnabled)
            {
               // g_ResManager.locationFloorTrans.gameObject.setActive(true);
                g_MapPoiController.Resume();
                this.mapEnabled = true;
                
                g_ResManager.downIconTrans.gameObject.setActive(false);
                g_ResManager.upIconTrans.gameObject.setActive(false);
                g_ResManager.trackLimitedTrans.gameObject.setActive(false);
            }
            g_MapLocationController.UpdateMapInfo();
        }
        else
        {
            if(state==null)
                return;
            if(this.mapEnabled)
            {
                //console.error(state)
               // g_ResManager.locationFloorTrans.gameObject.setActive(false);
                // 关闭poi显示
                g_MapPoiController.Pause();
                this.mapEnabled = false;
                if(state == 1)
                {
                    g_ResManager.downIconTrans.gameObject.setActive(true);
                    g_ResManager.upIconTrans.gameObject.setActive(false);
                    g_ResManager.trackLimitedTrans.gameObject.setActive(false);
                    g_ResManager.trackFailTrans.gameObject.setActive(false);
                    g_ResManager.errorCodeTrans.gameObject.setActive(false);
                }
                else if(state == 2)
                {
                    g_ResManager.upIconTrans.gameObject.setActive(true);
                    g_ResManager.downIconTrans.gameObject.setActive(false);
                    g_ResManager.trackLimitedTrans.gameObject.setActive(false);
                    g_ResManager.trackFailTrans.gameObject.setActive(false);
                    g_ResManager.errorCodeTrans.gameObject.setActive(false);
                }
            }
        }
    },
    OnDisable: function()
    {
        //Insight.Debug.Log("lua call Game Location State disable");
        //this.Close();
    },
    //关闭
    Close: function()
    {       
        this.isEnabled = false;  
    },
    GetState: function()
    {
	    return g_GameStatus.GAME_STATUS_LOCATION;
    },

    //检查当前是否处于用户操作异常状态
    CheckTrackState: function()
    {
        //console.error("state = "+Insight.Tracking.cloudLocationStatus+"\t totalCount = "+Insight.Tracking.cloudLocationTotalCount);
        if(Insight.Tracking.cloudLocationStatus == 1)
        {
            var upVect = new Insight.Vector3(0, 1, 0);//Insight.Vector3.New(0,1,0);
            var downVect = new Insight.Vector3(0, -1, 0);//Insight.Vector3.New(0,-1,0);
            var upDot = g_ResManager.mainCameraTrans.forward.dot(upVect);//Insight.Vector3.Dot(g_ResManager.mainCameraTrans.forward,upVect);
            var downDot = g_ResManager.mainCameraTrans.forward.dot(downVect);//Insight.Vector3.Dot(g_ResManager.mainCameraTrans.forward,downVect);
            //阈值可以调节
            if(upDot >0.9 && downDot <0)
            {
                // 手机朝着天空需要下移
                return 1;
            }
                
            if(downDot >0.9 && upDot <0 )
            {
                // 手机朝着地面需要上抬
                return 2;
            }
            //正常
            return 0;
        }
    },
    // Update is called once per frame
    Update: function()
    {

    }



});

//进入poi 内容handler
function EnterPoiContentHandler(id)
{
    if (g_ResManager.audioOnTrans.gameObject.activeSelf) {
        g_ResManager.locationAudioTrans.gameObject.setActive(false);
    }
}
// 退出poi 内容handler
function ExitPoiContentHandler(id) {
    if (g_ResManager.audioOnTrans.gameObject.activeSelf) {
        g_ResManager.locationAudioTrans.gameObject.setActive(true);
    } else {
        g_ResManager.locationAudioTrans.gameObject.setActive(false);
    }
}



//Return the script module
GameLocationState