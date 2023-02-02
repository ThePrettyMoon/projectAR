
g_TrackInitTipState={
	INIT_START_STATE = 1,
	INIT_ANIM_STATE = 2,
	INIT_OK_START_STATE = 3,
	INIT_OK_ANIM_STATE  = 4,
	INIT_OK_STATE = 5,
}

local LandmarkInitState = {}


function LandmarkInitState:New(game_object)
    if self ~= LandmarkInitState then
        return nil, "LandmarkInitState:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = LandmarkInitState})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function LandmarkInitState:Awake()
	
	
end

function LandmarkInitState:Start()
	
end

function LandmarkInitState:Enter()
	self:Init();
end

function LandmarkInitState:Execute()
	if(not self.isEnabled) then
		return;
	end
	
	if(self.curState == g_TrackInitTipState.INIT_START_STATE) then
		self:StartInitView();
		self.curState = g_TrackInitTipState.INIT_ANIM_STATE;
	elseif(self.curState == g_TrackInitTipState.INIT_ANIM_STATE) then
		--算法未成功，需要一直等待
		if(Insight.Tracking.status ~=7) then
			if(self.curInitTime <= self.INITANIMATIONINTERVAL) then
				self.curInitTime = self.curInitTime + Insight.Time.deltaTime;
				self:UpdateInitView();
			else
				self.curInitTime = 0.0;
			end
		else
			self.curInitTime = 0.0;
			self.curState = g_TrackInitTipState.INIT_OK_START_STATE;
		end
	elseif(self.curState == g_TrackInitTipState.INIT_OK_START_STATE) then
		self:StartInitOkView();
		self.curState = g_TrackInitTipState.INIT_OK_ANIM_STATE;
	elseif(self.curState == g_TrackInitTipState.INIT_OK_ANIM_STATE) then
		if(self.curSuccessTime <= self.INITSUCCESSTIME) then
			self.curSuccessTime = self.curSuccessTime + Insight.Time.deltaTime;
		else
			self.curSuccessTime = 0.0;
			self.curState = g_TrackInitTipState.INIT_OK_STATE;
			self:StopInitOkView();
		end
	elseif(self.curState == g_TrackInitTipState.INIT_OK_STATE) then
		--enter location
		g_StateCtrl:ChangeState(g_GameStatus.GAME_STATUS_TRACKING);
	end
end

function LandmarkInitState:Exit()
	self:Close();
end

function LandmarkInitState:Init()
	self.INITANIMATIONINTERVAL = 3.0;
	self.INITSUCCESSTIME = 1.0;
	self.curInitTime = 0.0;
	self.curSuccessTime = 0.0;
	self.isEnabled = true;
	
	self.curState = g_TrackInitTipState.INIT_START_STATE;
	
end

function LandmarkInitState:StartInitView()
	g_ResManager.initPanelTrans.gameObject:SetActive(true);
	g_ResManager.initIconTrans.gameObject:SetActive(true);
	g_ResManager.initSlideTrans.gameObject:SetActive(true);
	--g_ResManager.initSlideImage.fillAmount =0.0;
	g_ResManager.initSlideMaterial:SetFloat("_FillAmount",0.0);
end

function LandmarkInitState:UpdateInitView()
	--g_ResManager.initSlideImage.fillAmount = self.curInitTime / self.INITANIMATIONINTERVAL;
	g_ResManager.initSlideMaterial:SetFloat("_FillAmount",self.curInitTime / self.INITANIMATIONINTERVAL);
end


--开始init ok
function LandmarkInitState:StartInitOkView()
	g_ResManager.initIconTrans.gameObject:SetActive(false);
	g_ResManager.initSlideTrans.gameObject:SetActive(false);
	g_ResManager.initOKTrans.gameObject:SetActive(true);
end


function LandmarkInitState:StopInitOkView()
	g_ResManager.initPanelTrans.gameObject:SetActive(false);
	g_ResManager.initOKTrans.gameObject:SetActive(false);
	--g_ResManager.initSlideImage.fillAmount =0.0;
	g_ResManager.initSlideMaterial:SetFloat("_FillAmount",0.0);
end

function LandmarkInitState:Close()
	self.isEnabled = false;
	self.curInitTime = 0.0;
	self.curSuccessTime = 0.0;
end



return LandmarkInitState
