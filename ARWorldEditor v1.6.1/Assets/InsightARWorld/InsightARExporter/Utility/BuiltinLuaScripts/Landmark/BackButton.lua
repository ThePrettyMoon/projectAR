

local BackButton = {}


function BackButton:New(game_object)
    if self ~= BackButton then
        return nil, "BackButton:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = BackButton})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function BackButton:Awake()
	
	
end

function BackButton:Start()
	
end

function BackButton:OnPointerUp()
	local _name = self.gameObject.name;
	if _name == "backbutton" then
		g_LandmarkManager:Close();
		Fw_Event_CloseARScene();
	end
end

return BackButton
