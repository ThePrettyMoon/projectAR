-- ------------------------------
-- Copyright (c) NetEase Insight
-- All rights reserved.
-- ------------------------------
local cors = {};

function WaitForSeconds(sec)
	local begin = Insight.Time.time;
	while( Insight.Time.time - begin < sec ) do
		coroutine.yield();
	end
end

function WaitUntil(cond)
	while( false == cond() ) do
		coroutine.yield();
	end
end

function StartCoroutine(func)
	local id = coroutine.create(func);
	cors[id] = id;
	coroutine.resume( id );
	return id;
end

function StopCoroutine(id)
	cors[id] = nil;
end

local Insight3DBuildinCoroutine = {};

function Insight3DBuildinCoroutine:New( game_object )
	if self ~= Insight3DBuildinCoroutine then return nil, "First argument must be self." end
	local new_instance = setmetatable( {} , { __metatable = {}, __index = Insight3DBuildinCoroutine } );
	return new_instance;
end

function Insight3DBuildinCoroutine:Update()
	for id,co in pairs(cors) do
		local cont = coroutine.resume( id );
		if( false == cont )then
			cors[id] = nil;
		end
	end
end

return Insight3DBuildinCoroutine;
