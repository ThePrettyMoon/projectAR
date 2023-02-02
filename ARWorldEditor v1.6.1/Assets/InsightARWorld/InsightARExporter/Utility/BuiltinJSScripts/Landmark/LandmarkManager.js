var g_FwUtility;
var g_ResManager;
var g_StateCtrl;
var g_LandmarkManager;

function LandmarkManager(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
LandmarkManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        Insight.Debug.Log("landmarkmanager start");
        g_FwUtility = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Util/Fw_Utility.js",0);
        g_ResManager = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Landmark/LandmarkResource.js",0);
        g_StateCtrl = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Landmark/LandmarkStateCtrl.js",0);
        g_LandmarkManager = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Landmark/LandmarkManager.js",0);
        this.Init();
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    Init: function()
    {
        g_ResManager.Init();
        g_StateCtrl.Init();
    },
    OnDisable: function()
    {
	    //this.Close();
    },
    //关闭
    Close: function()
    {
        //g_StateCtrl:ChangeState(g_GameStatus.GAME_STATUS_IDLE);
        if (g_StateCtrl)
            g_StateCtrl.Close();
    }

});

//Return the script module
LandmarkManager