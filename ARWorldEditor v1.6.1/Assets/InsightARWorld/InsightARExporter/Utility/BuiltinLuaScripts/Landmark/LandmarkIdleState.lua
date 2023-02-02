


local LandmarkIdleState = {}


function LandmarkIdleState:New(game_object)
    if self ~= LandmarkIdleState then
        return nil, "LandmarkIdleState:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = LandmarkIdleState})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function LandmarkIdleState:Awake()
	
	
end

function LandmarkIdleState:Start()
	
end

function LandmarkIdleState:Enter()
	
end

function LandmarkIdleState:Execute()

end

function LandmarkIdleState:Exit()
	
end

function LandmarkIdleState:Init()

end



return LandmarkIdleState
