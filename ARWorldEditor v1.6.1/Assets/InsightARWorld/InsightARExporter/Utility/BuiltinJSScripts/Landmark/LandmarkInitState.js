
var g_TrackInitTipState={
	"INIT_START_STATE" : 1,
	"INIT_ANIM_STATE" : 2,
	"INIT_OK_START_STATE" : 3,
	"INIT_OK_ANIM_STATE"  : 4,
	"INIT_OK_STATE" : 5
}
var g_lastErrorCode = null;
function LandmarkInitState(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
LandmarkInitState.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    Enter: function()
	{
        this.Init();
    },
    Execute: function()
    {
        if(!this.isEnabled){
		    return;
        }
	
        if(this.curState == g_TrackInitTipState.INIT_START_STATE)
        {
            this.StartInitView();
            this.curState = g_TrackInitTipState.INIT_ANIM_STATE;
        }
        else if(this.curState == g_TrackInitTipState.INIT_ANIM_STATE)
        {
            //算法未成功，需要一直等待
            if(Insight.Tracking.cloudLocationStatus !=1)
            {
                if(this.curInitTime <= this.INITANIMATIONINTERVAL)
                {
                    this.curInitTime = this.curInitTime + Insight.Time.deltaTime;
                    this.UpdateInitView();
                    var tmp = this.CheckUserCurState();
                    this.UpdateUserView(tmp);
                }
                else
                {
                    this.curInitTime = 0.0;
                }
            }
            else
            {
                this.isCloudLocSuc = true;
                this.curInitTime = 0.0;
                this.curState = g_TrackInitTipState.INIT_OK_START_STATE;
            }
        }
        else if(this.curState == g_TrackInitTipState.INIT_OK_START_STATE)
        {
            this.StartInitOkView();
            this.curState = g_TrackInitTipState.INIT_OK_ANIM_STATE;
        }
        else if(this.curState == g_TrackInitTipState.INIT_OK_ANIM_STATE)
        {
            if(this.curSuccessTime <= this.INITSUCCESSTIME){
                this.curSuccessTime = this.curSuccessTime + Insight.Time.deltaTime;
            }
            else
            {
                this.curSuccessTime = 0.0;
                this.curState = g_TrackInitTipState.INIT_OK_STATE;
                this.StopInitOkView();
            }
        }
        else if(this.curState == g_TrackInitTipState.INIT_OK_STATE)
        {
            //enter location
            g_StateCtrl.ChangeState(g_GameStatus.GAME_STATUS_TRACKING);
        }
    },
    CheckUserCurState:function()
    {
        if(Insight.Tracking.cloudLocationStatus == 1)
        {
            var upVect = new Insight.Vector3(0, 1, 0);//Insight.Vector3.New(0,1,0);
            var downVect = new Insight.Vector3(0, -1, 0);//Insight.Vector3.New(0,-1,0);
            var upDot = g_ResManager.mainCameraTrans.forward.dot(upVect);//Insight.Vector3.Dot(g_ResManager.mainCameraTrans.forward,upVect);
            var downDot = g_ResManager.mainCameraTrans.forward.dot(downVect);//Insight.Vector3.Dot(g_ResManager.mainCameraTrans.forward,downVect);
            g_lastRecognitionCount = Insight.Tracking.cloudLocationTotalCount;
            g_lastErrorCode = null;
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
        else if(Insight.Tracking.cloudLocationStatus == 18 && !this.isCloudLocSuc)//点位稀疏
        {
            return 3;
        }
        else if(Insight.Tracking.cloudLocationStatus == 16 && !this.isCloudLocSuc)//定位错误
        {
            //console.error("g_lastRecognitionCount= "+g_lastRecognitionCount);
            //如果连续云定位请求失败，则提示无法准确定位
            if(Insight.Tracking.cloudLocationTotalCount - g_lastRecognitionCount >= this.limitFailCount)
            {
                return 4;//
            }
        }
        else if(Insight.Tracking.cloudLocationStatus >= 9000&& Insight.Tracking.cloudLocationStatus != 9900 && !this.isCloudLocSuc)//服务器错误
        {
            if (g_lastErrorCode||g_lastErrorCode == Insight.Tracking.cloudLocationStatus) 
            {
                return;    
            }
            g_lastErrorCode = Insight.Tracking.cloudLocationStatus;
            g_ResManager.errorCodeText.text ="服务器开小差了，code = "+Insight.Tracking.cloudLocationStatus.toString();
            return 5;
        }
        else{
            return -1;
        }
    },
    UpdateUserView:function(state)
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
                else if(state == 3 && !this.isCloudLocSuc)//点位稀疏
                {
                    g_ResManager.trackLimitedTrans.gameObject.setActive(true);
                    g_ResManager.upIconTrans.gameObject.setActive(false);
                    g_ResManager.downIconTrans.gameObject.setActive(false);
                    g_ResManager.trackFailTrans.gameObject.setActive(false);
                    g_ResManager.errorCodeTrans.gameObject.setActive(false);
                }else if(state == 4 && !this.isCloudLocSuc)//定位失败
                {
                    g_ResManager.trackFailTrans.gameObject.setActive(true);
                    g_ResManager.trackLimitedTrans.gameObject.setActive(false);
                    g_ResManager.upIconTrans.gameObject.setActive(false);
                    g_ResManager.downIconTrans.gameObject.setActive(false);
                    g_ResManager.errorCodeTrans.gameObject.setActive(false);
                }else if(state == 5 && !this.isCloudLocSuc)
                {
                    g_ResManager.errorCodeTrans.gameObject.setActive(true);
                    g_ResManager.trackFailTrans.gameObject.setActive(false);
                    g_ResManager.trackLimitedTrans.gameObject.setActive(false);
                    g_ResManager.upIconTrans.gameObject.setActive(false);
                    g_ResManager.downIconTrans.gameObject.setActive(false);
                }
            }
            else
            {
                //有按钮控制的状态会在下一次失败时，再次提示
                if (state==4 && !this.isCloudLocSuc) 
                {
                    g_ResManager.trackFailTrans.gameObject.setActive(true);
                    if(g_ResManager.initPanelTrans.gameObject.activeSelf)
                        g_ResManager.initPanelTrans.gameObject.setActive(false);
                    g_ResManager.trackLimitedTrans.gameObject.setActive(false);
                    g_ResManager.upIconTrans.gameObject.setActive(false);
                    g_ResManager.downIconTrans.gameObject.setActive(false);
                    g_ResManager.errorCodeTrans.gameObject.setActive(false);
                }
                else if(state == 5 && !this.isCloudLocSuc)
                {
                    g_ResManager.errorCodeTrans.gameObject.setActive(true);
                    g_ResManager.trackFailTrans.gameObject.setActive(false);
                    g_ResManager.trackLimitedTrans.gameObject.setActive(false);
                    g_ResManager.upIconTrans.gameObject.setActive(false);
                    g_ResManager.downIconTrans.gameObject.setActive(false);
                }
            }
        }
    },
    Exit: function()
	{
        this.Close();
    },
    Init: function()
    {
        this.INITANIMATIONINTERVAL = 3.0;
        this.INITSUCCESSTIME = 1.0;
        this.curInitTime = 0.0;
        this.curSuccessTime = 0.0;
        this.isEnabled = true;
        this.errorTimer = 0;
        this.isCloudLocSuc = false;
        this.limitFailCount = 5;

        this.curState = g_TrackInitTipState.INIT_START_STATE;
    },
    StartInitView: function()
    {
        g_ResManager.initPanelTrans.gameObject.setActive(true);
        g_ResManager.initIconTrans.gameObject.setActive(true);
        g_ResManager.initSlideTrans.gameObject.setActive(true);
        //g_ResManager.initSlideImage.fillAmount =0.0;
        g_ResManager.initSlideMaterial.setFloat("_FillAmount",0.0);
    },
    UpdateInitView: function()
    {
        if(Insight.Tracking.cloudLocationStatus >= 9000&& Insight.Tracking.cloudLocationStatus != 9900)
        {
            if (g_ResManager.initPanelTrans.gameObject.activeSelf) {
                g_ResManager.initPanelTrans.gameObject.setActive(false);
            }
            if(this.errorTimer >= 0)
            {
                this.errorTimer -= Insight.Time.deltaTime;
                if( this.errorTimer < 0)
                {
                    if (g_lastErrorCode||g_lastErrorCode == Insight.Tracking.cloudLocationStatus) 
                    {
                        
                    }
                    else
                    {
                        g_ResManager.errorCodeText.text ="服务器开小差了，code = "+Insight.Tracking.cloudLocationStatus.toString();
                        //this.UpdateUserView(5);
                    }
                    //Fw_Event_MakeToast("服务器开小差了，code = "+Insight.Tracking.cloudLocationStatus.toString(),2);
                    this.errorTimer = 2;
                }
            }
            else{
                this.errorTimer = 0.1;
            }
        }
        else
        { 
            if(this.errorTimer >= 0)
            {
                this.errorTimer -= Insight.Time.deltaTime;
                if( this.errorTimer < 0)
                {
                    if (g_ResManager.initPanelTrans.gameObject.activeSelf == false) {
                        g_ResManager.initPanelTrans.gameObject.setActive(true);
                    }
                }
            }
        }
        //g_ResManager.initSlideImage.fillAmount = this.curInitTime / this.INITANIMATIONINTERVAL;
	    g_ResManager.initSlideMaterial.setFloat("_FillAmount",this.curInitTime / this.INITANIMATIONINTERVAL);
    },
    //开始init ok
    StartInitOkView: function()
    {
        g_ResManager.initIconTrans.gameObject.setActive(false);
        g_ResManager.initSlideTrans.gameObject.setActive(false);
        g_ResManager.initOKTrans.gameObject.setActive(true);
    },
    StopInitOkView: function()
    {
        g_ResManager.initPanelTrans.gameObject.setActive(false);
        g_ResManager.initOKTrans.gameObject.setActive(false);
        //g_ResManager.initSlideImage.fillAmount =0.0;
        g_ResManager.initSlideMaterial.setFloat("_FillAmount",0.0);
    },
    Close: function()
    {
        this.isEnabled = false;
        this.curInitTime = 0.0;
        this.curSuccessTime = 0.0;
    }
});

//Return the script module
LandmarkInitState