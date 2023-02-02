var g_GameStatus={
    GAME_STATUS_UNKNOWN : 0,
    GAME_STATUS_TRACK_INIT : 1,
    GAME_STATUS_LOCATION : 2,
    GAME_STATUS_NAVIGATION : 3,
    GAME_STATUS_IDLE : 4,
 };
var g_GameState;
var g_CurGameState = undefined;
function GameStateCtrl(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}

//=========================
//POI模式下的GameStateCtrl 
//=========================

GameStateCtrl.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

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
        g_GameState = [];
        var mainState = Insight.GameObject.Find( "mainState");
        //wzy: 这里从1开始是因为0是unknonw，没有脚本处理
        g_GameState[g_GameState.length + 1] = mainState.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/GameTrackInitState.js", 0  );
        g_GameState[g_GameState.length ] = mainState.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/GameLocationState.js", 0  );
        g_GameState[g_GameState.length ] = mainState.getComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinJSScripts/Location/GameIdleState.js", 0  );
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
        if(g_CurGameState)
        {
            g_CurGameState.Enter();
        }
    },
    //返回状态
    GetState: function()
    {
        if(g_CurGameState)
        {
            return g_CurGameState.GetState();
        }
        return g_GameStatus.GAME_STATUS_UNKNOWN;
    },
    Close: function () {
        if (g_CurGameState) {
            g_CurGameState.Close();
        }
    }
});

//Return the script module
GameStateCtrl