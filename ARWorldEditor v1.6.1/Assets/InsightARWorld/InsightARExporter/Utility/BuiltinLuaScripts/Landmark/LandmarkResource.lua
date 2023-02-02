

local LandmarkResource = {}


function LandmarkResource:New(game_object)
    if self ~= LandmarkResource then
        return nil, "LandmarkResource:New is nil"
    end
    local newinstance = setmetatable({}, {__metatable = {}, __index = LandmarkResource})
    newinstance.transform = game_object.transform
    transform = game_object.transform
    newinstance.gameObject = game_object
    gameObject = game_object
    return newinstance
end

function LandmarkResource:Awake()
	
	
end

function LandmarkResource:Start()
	
end

-- init
function LandmarkResource:Init()
	self.canvasTrans = Insight.GameObject.Find("HintController").transform;
	self.mainCamera =  Insight.GameObject.Find("Main Camera"):GetComponent("Camera");
	self.mainCameraTrans = self.mainCamera.transform;
	
	-- init panel
	self.initPanelTrans = self.canvasTrans:Find("initpanel");
	self.initIconTrans = self.initPanelTrans:Find("initicon");
	self.initSlideTrans = self.initPanelTrans:Find("initslide");
	self.initSlideImage = self.initSlideTrans.gameObject:GetComponent("Image");
	self.initSlideMaterial = self.initSlideImage.material;
	self.initOKTrans = self.initPanelTrans:Find("initok");
	
	-- model
	self.modelRootTrans = Insight.GameObject.Find("World").transform;
	-- self.jinyujiTrans = self.modelRootTrans:Find("JINYUJI");
	-- self.datiangouTrans = self.modelRootTrans:Find("DATIANGOU");
	-- self.yijinzhentianTrans = self.modelRootTrans:Find("YIJINZHENTIAN");
	-- self.jinlongTrans = self.modelRootTrans:Find("jinlong");
	-- self.balloonTrans = self.modelRootTrans:Find("fire_balloons");
	-- self.landmarkTrans = self.modelRootTrans:Find("LandMark_C11");
	-- self.axisTrans = self.modelRootTrans:Find("Axis");
	-- self.maskTrans = self.modelRootTrans:Find("C11_Mask");
	
	--trackpanel
	self.trackPanelTrans = self.canvasTrans:Find("trackpanel");
	self.upIconTrans = self.trackPanelTrans:Find("upicon");
	self.downIconTrans = self.trackPanelTrans:Find("downicon");
	self.trackLimitedTrans = self.trackPanelTrans:Find("tracklimitedicon");
end

return LandmarkResource
