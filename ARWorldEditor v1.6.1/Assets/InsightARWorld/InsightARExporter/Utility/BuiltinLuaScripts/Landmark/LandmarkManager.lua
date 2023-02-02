

local LandmarkManager = {}


function LandmarkManager:New(game_object)
    if self ~= LandmarkManager then
        return nil, "LandmarkManager:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = LandmarkManager})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function LandmarkManager:Awake()

	
end

function LandmarkManager:Start()
	Insight.Debug.Log("landmarkmanager start");
	g_FwUtility = self.gameObject:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Util/Fw_Utility.lua",0);
	g_ResManager = self.gameObject:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkResource.lua",0);
	g_StateCtrl = self.gameObject:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkStateCtrl.lua",0);
	g_LandmarkManager = self.gameObject:GetComponent("Assets/InsightARWorld/InsightARExporter/Utility/BuiltinLuaScripts/Landmark/LandmarkManager.lua",0);
	self:Init();
end

function LandmarkManager:Init()
	g_ResManager:Init();
	g_StateCtrl:Init();
end

function LandmarkManager:OnDisable()
	self:Close();
end

--关闭
function LandmarkManager:Close()
	--g_StateCtrl:ChangeState(g_GameStatus.GAME_STATUS_IDLE);
end

return LandmarkManager
