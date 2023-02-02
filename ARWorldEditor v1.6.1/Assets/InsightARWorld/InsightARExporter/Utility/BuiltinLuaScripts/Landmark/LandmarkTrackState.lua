
local LandmarkTrackState = {}


function LandmarkTrackState:New(game_object)
    if self ~= LandmarkTrackState then
        return nil, "LandmarkTrackState:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = LandmarkTrackState})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function LandmarkTrackState:Awake()
	
	
end

function LandmarkTrackState:Start()
	
end

function LandmarkTrackState:Enter()
	Insight.Debug.Log("land mark track state");
	self:Init();
end

function LandmarkTrackState:Execute()
	if(not self.isEnabled) then
		return;
	end
	
	local state = self:CheckTrackState();
	self:UpdateView(state);
end

function LandmarkTrackState:Exit()
	self:Close();
end

function LandmarkTrackState:Init()
	self.isEnabled = true;
	g_ResManager.trackPanelTrans.gameObject:SetActive(true);
	
	-- 开启模型显示
	g_ResManager.modelRootTrans.gameObject:SetActive(true);
	-- g_ResManager.jinyujiTrans.gameObject:SetActive(true);
	-- g_ResManager.datiangouTrans.gameObject:SetActive(true);
	-- g_ResManager.yijinzhentianTrans.gameObject:SetActive(true);
	-- g_ResManager.jinlongTrans.gameObject:SetActive(true);
	-- g_ResManager.balloonTrans.gameObject:SetActive(true);
	-- g_ResManager.landmarkTrans.gameObject:SetActive(true);
	-- g_ResManager.axisTrans.gameObject:SetActive(true);
	-- g_ResManager.maskTrans.gameObject:SetActive(true);
end


function LandmarkTrackState:Close()
	self.isEnabled = false;
	g_ResManager.trackPanelTrans.gameObject:SetActive(false);
	--关闭模型显示
	g_ResManager.modelRootTrans.gameObject:SetActive(false);
	-- g_ResManager.jinyujiTrans.gameObject:SetActive(false);
	-- g_ResManager.datiangouTrans.gameObject:SetActive(false);
	-- g_ResManager.yijinzhentianTrans.gameObject:SetActive(false);
	-- g_ResManager.jinlongTrans.gameObject:SetActive(false);
	-- g_ResManager.balloonTrans.gameObject:SetActive(false);
	-- g_ResManager.landmarkTrans.gameObject:SetActive(false);
	-- g_ResManager.axisTrans.gameObject:SetActive(false);
	-- g_ResManager.maskTrans.gameObject:SetActive(false);
end

-- 更新视图
function LandmarkTrackState:UpdateView(state)
	if(state == 0) then
		if(not self.mapEnabled) then
			self.mapEnabled = true;
			
			g_ResManager.downIconTrans.gameObject:SetActive(false);
			g_ResManager.upIconTrans.gameObject:SetActive(false);
			g_ResManager.trackLimitedTrans.gameObject:SetActive(false);
			
		end
	else
		if(self.mapEnabled) then
			self.mapEnabled = false;

			if(state == 1) then
				g_ResManager.downIconTrans.gameObject:SetActive(true);
			elseif(state == 2) then
				g_ResManager.upIconTrans.gameObject:SetActive(true);
			elseif(state == 3) then
				g_ResManager.trackLimitedTrans.gameObject:SetActive(true);
			end
		end
	end
end

--检查当前是否处于用户操作异常状态
function LandmarkTrackState:CheckTrackState()
	if(Insight.Tracking.status == 7) then
		local upVect = Insight.Vector3.New(0,1,0);
		local downVect = Insight.Vector3.New(0,-1,0);
		local upDot = Insight.Vector3.Dot(g_ResManager.mainCameraTrans.forward,upVect);
		local downDot = Insight.Vector3.Dot(g_ResManager.mainCameraTrans.forward,downVect);
			--阈值可以调节
		if(upDot >0.9 and downDot <0)then
			-- 手机朝着天空需要下移
			return 1;
		end
			
		if(downDot >0.9 and upDot <0 ) then
			-- 手机朝着地面需要上抬
			return 2;
		end
		--正常
		return 0;
	else
		--算法状态异常
		return 3;
	end
end

return LandmarkTrackState
