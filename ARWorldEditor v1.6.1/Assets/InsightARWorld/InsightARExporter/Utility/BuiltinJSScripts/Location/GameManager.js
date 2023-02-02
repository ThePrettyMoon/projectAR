var g_PoolManager;
var g_MapLocationController;
var g_GameStateCtrl;
var g_EventManager;
var g_MapPoiController;
var g_MapPoiEventDispater;
var g_ResManager;
var g_MapData;
var g_CalculateAngle;
var g_GameManager;

function GameManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
GameManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {
        Insight.Debug.Log("js call game manager start");
        g_PoolManager = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/PoolManager.js",0);
        g_MapLocationController = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/MapLocationController.js",0);
        g_GameStateCtrl = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/GameStateCtrl.js",0);
        g_EventManager = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/EventManager.js",0);
        g_MapPoiController = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/MapPoiController.js",0);
        g_MapPoiEventDispater = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/MapPoiEventDispatcher.js",0);
        g_ResManager = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/ResourceManager.js",0);
        g_MapData = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/GameMapData.js",0);
        g_CalculateAngle = Insight.GameObject.Find("utility").getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/CalculateArrowPosition.js",0);
        g_GameManager = this;
        this.Init();
    },
    // ondisable
    OnDisable: function()
    {
        // Exit
        this.Close();
    },
    //初始化
    Init: function()
    {
        g_PoolManager.Init();
        g_ResManager.Init();
        g_GameStateCtrl.Init();
        g_MapData.Init();
        g_MapPoiEventDispater.Init();
        
        //监听poi点击回调
        Insight.Navigation.OnDestinationChanged(this, this.onDestinationChanged);
    },
    onDestinationChanged: function(id,isNavigationState)
    {
        Insight.Debug.Log("js call on click poi handler" + id+" isNavigationState:"+isNavigationState);
        g_MapData.SetIsNavigationState(isNavigationState);
        if (g_MapData.SetNavPoiInfo(id))
            g_GameStateCtrl.ChangeState(g_GameStatus.GAME_STATUS_NAVIGATION);
    },
    // 关闭
    Close: function()
    {
        Insight.Debug.Log("js call game manager close");
        if (g_EventManager)
            g_EventManager.RemoveAllListener();

        if (g_PoolManager)
            g_PoolManager.Close();

        if (g_MapData)
            g_MapData.Close();

        if (g_GameStateCtrl)
            g_GameStateCtrl.Close();
    },
    OnDestroy: function()
    {
	    //Insight.Debug.Log("lua call game manager on destroy");
    },
    // Update is called once per frame
    Update: function()
    {

    }
});

//Return the script module
GameManager