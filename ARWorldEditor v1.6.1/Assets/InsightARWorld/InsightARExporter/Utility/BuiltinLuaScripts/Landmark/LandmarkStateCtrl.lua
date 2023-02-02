g_GameStatus={
   GAME_STATUS_UNKNOWN = 0,
   GAME_STATUS_IDLE = 1,
   GAME_STATUS_TRACK_INIT = 2,
   GAME_STATUS_TRACKING = 3,
}

local LandmarkStateCtrl = {}

g_GameState = {}
g_CurGameState = nil;
function LandmarkStateCtrl:New(game_object)
    if self ~= LandmarkStateCtrl then
        return nil, "LandmarkStateCtrl:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = LandmarkStateCtrl})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function LandmarkStateCtrl:Awake()
	
end

function LandmarkStateCtrl:Start()
	
end

function LandmarkStateCtrl:Update()
	if(g_CurGameState) then
		g_CurGameState:Execute();
	end
end

function LandmarkStateCtrl:Init()
	g_GameState = {};
	local mainState = Insight.GameObject.Find( "mainState");
	g_GameState[#g_GameState + 1] = mainState:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkIdleState.lua", 0  );
	g_GameState[#g_GameState + 1] = mainState:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkInitState.lua", 0  );
	g_GameState[#g_GameState + 1] = mainState:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkTrackState.lua", 0  );
	--初始化进入init 状态
	self:ChangeState(g_GameStatus.GAME_STATUS_TRACK_INIT);
end


function LandmarkStateCtrl:ChangeState(status)
	if(g_CurGameState) then
		g_CurGameState:Exit();
	end
	g_CurGameState = g_GameState[status];
	g_CurGameState:Enter();
end



return LandmarkStateCtrl
