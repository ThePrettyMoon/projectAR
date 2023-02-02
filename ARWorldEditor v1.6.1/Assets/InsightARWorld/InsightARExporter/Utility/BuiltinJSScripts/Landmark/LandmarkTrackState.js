var g_lastRecognitionCount = 0;
var g_lastErrorCode = null;
var g_landmarkTrackStateInstance = null;

function LandmarkTrackState(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
LandmarkTrackState.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        g_landmarkTrackStateInstance = this;
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    Enter: function()
	{
        Insight.Debug.Log("land mark track state");
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
        g_ResManager.trackPanelTrans.gameObject.setActive(false);
        //关闭模型显示
        g_ResManager.modelRootTrans.gameObject.setActive(false);
    },
    Init: function()
    {
        this.isEnabled = true;
        this.mapEnabled = false;
        this.limitFailCount = 5;
        g_ResManager.trackPanelTrans.gameObject.setActive(true);
        
        // 开启模型显示
        g_ResManager.modelRootTrans.gameObject.setActive(true);
    },
    Close: function()
    {
        this.isEnabled = false;    
    },
    // 更新视图
    UpdateView: function(state)
    {
        if(state == 0)
        {
            if(!this.mapEnabled)
            {
                this.mapEnabled = true;
                
                g_ResManager.downIconTrans.gameObject.setActive(false);
                g_ResManager.upIconTrans.gameObject.setActive(false);
                g_ResManager.trackLimitedTrans.gameObject.setActive(false);   
                g_ResManager.trackFailTrans.gameObject.setActive(false);
                g_ResManager.errorCodeTrans.gameObject.setActive(false);
            }
        }
        else
        {
            if(state==null) return;

            if(this.mapEnabled)
            {
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
                else
                {

                }
            }
        }
    },
    //检查当前是否处于用户操作异常状态
    CheckTrackState: function()
    {
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
    }
});

//Return the script module
LandmarkTrackState