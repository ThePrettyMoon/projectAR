var g_CurGameState;
var g_GameState;
var g_GameStatus={
    "GAME_STATUS_UNKNOWN" : 0,
    "GAME_STATUS_IDLE" : 1,
    "GAME_STATUS_TRACK_INIT" : 2,
    "GAME_STATUS_TRACKING" : 3
 }
 
function LandmarkStateCtrl(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
LandmarkStateCtrl.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        if(g_CurGameState)
        {
		    g_CurGameState.Execute();
        }
    },
    Init: function()
    {
        //g_GameState = {};
        g_GameState = new Array(4);
        var mainState = Insight.GameObject.Find( "mainState");
        // g_GameState[#g_GameState + 1] = mainState:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkIdleState.lua", 0  );
        // g_GameState[#g_GameState + 1] = mainState:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkInitState.lua", 0  );
        // g_GameState[#g_GameState + 1] = mainState:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkTrackState.lua", 0  );
        g_GameState[1] = mainState.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Landmark/LandmarkIdleState.js", 0  );
        g_GameState[2] = mainState.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Landmark/LandmarkInitState.js", 0  );
        g_GameState[3] = mainState.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Landmark/LandmarkTrackState.js", 0  );
        //初始化进入init 状态
        this.ChangeState(g_GameStatus.GAME_STATUS_TRACK_INIT);
    },
    ChangeState: function(status)
    {
        if(g_CurGameState)
        {
            g_CurGameState.Exit();
        }
        g_CurGameState = g_GameState[status];
        g_CurGameState.Enter();
    },
    Close: function () {
        if (g_CurGameState) {
            g_CurGameState.Close();
        }
    }
});

//Return the script module
LandmarkStateCtrl