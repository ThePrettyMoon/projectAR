-- ------------------------------
-- Copyright (c) NetEase Insight
-- All rights reserved.
-- ------------------------------

local Insight3DBuildinShowOnTracking = {};

function Insight3DBuildinShowOnTracking:New( entity )
	if self ~= Insight3DBuildinShowOnTracking then return nil, "First argument must be self." end
	local new_instance = setmetatable( {} , { __metatable = {}, __index = Insight3DBuildinShowOnTracking } );
	new_instance.entity = entity;
	return new_instance;
end

function Insight3DBuildinShowOnTracking:Update()
	local status = World.Tracking.GetStatus();
	if status == 7 or status == 8 then
		self.entity:SetVisible( true );
	else
		self.entity:SetVisible( false );
	end
end

return Insight3DBuildinShowOnTracking;
