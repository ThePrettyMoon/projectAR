function ResourceManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}

//=========================
//POI模式下的ResourceManager
//=========================

ResourceManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    },
    //初始化
    Init: function()
    {
        this.canvasTrans = Insight.GameObject.Find("HintController").transform;
        this.mainCamera =  Insight.GameObject.Find("Main Camera").getComponent("Camera");
        this.mainCameraTrans = this.mainCamera.transform;
        
        // init panel
        this.initPanelTrans = this.canvasTrans.find("initpanel");
        this.initIconTrans = this.initPanelTrans.find("initicon");
        this.initSlideTrans = this.initPanelTrans.find("initslide");
        this.initSlideImage = this.initSlideTrans.gameObject.getComponent("Image");
        this.initSlideMaterial = this.initSlideImage.material;
        this.initOKTrans = this.initPanelTrans.find("initok");

        this.modelRootTrans = Insight.GameObject.Find("World").transform;
        this.poiTrans = this.modelRootTrans.find("Land/POI");
        if(this.poiTrans !=null)
        {
            this.heightAboveGround = this.poiTrans.find("heightabovegroundroot").position.y;
            this.poiTrans.gameObject.setActive(false)
        }

        //location
        this.locationPanelTrans = this.canvasTrans.find("locationpanel");
        this.locationFloorTrans = this.locationPanelTrans.find("floortip");
        this.locationAudioTrans = Insight.GameObject.Find("bgSource").transform;
        this.locationAudioSource = this.locationAudioTrans.gameObject.getComponent("AudioSource");
        this.upIconTrans = this.locationPanelTrans.find("upicon");
        this.downIconTrans = this.locationPanelTrans.find("downicon");
        this.trackLimitedTrans = this.locationPanelTrans.find("tracklimitedicon");
        this.trackFailTrans = this.locationPanelTrans.find("trackfailicon");
        this.trackFailButton = this.trackFailTrans.find("trackfailbutton").gameObject.getComponent("Button");
        this.errorCodeTrans = this.locationPanelTrans.find("errorcodeicon");
        this.errorCodeButton = this.errorCodeTrans.find("errorcodebutton").gameObject.getComponent("Button");
        this.errorCodeText = this.errorCodeTrans.find("errorcodetext").gameObject.getComponent("Text");


        this.audioOnTrans = this.locationPanelTrans.find("audiobuttonon");
        this.audioOffTrans = this.locationPanelTrans.find("audiobuttonoff");
        
        
        //poi panel
        this.poiPanelTrans = this.canvasTrans.find("poipanel");
        this.world_canvasTrans = Insight.GameObject.Find("WorldCanvas").transform;
        this.world_poiPanelTrans = this.world_canvasTrans.find("poipanel");

        this.poiInfoTransList = [];
        //兼容sdk，不好处理instantiate，先预存40个,存储poi显示图标的列表
        for(var i= 0; i<40; i++)
        {
            var infoPanelName = "poiinfopanel"+" ("+i.toString()+")";
            // this.poiInfoTransList[i] = this.poiPanelTrans.find(infoPanelName);
            this.poiInfoTransList[i] = this.world_poiPanelTrans.find(infoPanelName);
            //存入缓存池
            g_PoolManager.DeSpawn("poiUI",this.poiInfoTransList[i]);
        }
        
        //存储miss 图标
        this.poiMissTransList = [];
        for(var i= 0; i<5; i++)
        {
            var missPanel = "missTipPanel"+" ("+i.toString()+")";
            // this.poiMissTransList[i] = this.poiPanelTrans.find(missPanel);
            this.poiMissTransList[i] = this.world_poiPanelTrans.find(missPanel);
            // 存入缓存池
            g_PoolManager.DeSpawn("poiMiss",this.poiMissTransList[i]);
        }
        
        //存储closer 图标
        this.poiCloserTransList = [];
        for(var i= 0; i < 5; i++)
        {
            var closerPanel = "closerpanel"+" ("+i.toString()+")";
            // this.poiCloserTransList[i] = this.poiPanelTrans.find(closerPanel);
            this.poiCloserTransList[i] = this.world_poiPanelTrans.find(closerPanel);
            // 存入缓存池
            g_PoolManager.DeSpawn("poiCloser",this.poiCloserTransList[i]);
        }
        
        //交互面板
        this.poiInteractionList = [];
        for(var i = 0; i < 5; i ++)
        {
            var interactionPanel = "interactionpanel"+" ("+i.toString()+")";
            this.poiInteractionList[i] = this.poiPanelTrans.find(interactionPanel);
            g_PoolManager.DeSpawn("poiInteraction",this.poiInteractionList[i]);
        }
        
        // //preview model
        // this.previewRootTrans = Insight.GameObject.Find("previewRoot").transform;
        // this.poiPreviewList = [];
        // for(var i = 0; i < 5; i ++)
        // {
        //     var previewModel = "cube"+" ("+i.toString()+")";
        //     this.poiPreviewList[i] = this.previewRootTrans.find(previewModel);
        //     g_PoolManager.DeSpawn("poiPreview",this.poiPreviewList[i]);
        // }

        
        //// navigation 
        // this.navigationPanelTrans = this.canvasTrans.find("navigationpanel");
        // this.upstairTipTrans = this.navigationPanelTrans.find("upstairimage");
        // this.downstairTipTrans = this.navigationPanelTrans.find("downstairimage");
    
        // //箭头偏航提示
        // this.arrowTrans = this.navigationPanelTrans.find("arrowimage");
        // this.navRootTrans = Insight.GameObject.Find("naviroot").transform;
        // this.arrowRootTrans = this.navRootTrans.find("arrows").transform;
        // this.navEndTrans = this.navRootTrans.find("navend").transform;
        // this.navUpstairArrowTrans = this.navRootTrans.find("navupstairs").transform;
        // this.navRoadsignArrowTrans = this.navRootTrans.find("navturn").transform;
        // this.navDownstairArrowTrans = this.navRootTrans.find("navdownstairs").transform;
        // this.enterstairTipTrans = this.navigationPanelTrans.find("enterstairtip").transform;
        // this.relocationTipTrans = this.navigationPanelTrans.find("relocationtip").transform;
        // this.relocationTipText = this.navigationPanelTrans.find("relocationtip/Text").gameObject.getComponent("Text");
        // this.navArrowImageTrans = this.navigationPanelTrans.find("arrowimage").transform;
        // this.navArrowImageRectTrans = this.navArrowImageTrans.gameObject.getComponent("RectTransform");

        //navigation tip
        // this.initNavPanelTrans = this.navigationPanelTrans.find("initpanel");
        // this.initNavIconTrans = this.initNavPanelTrans.find("initicon");
        // this.initNavTxt = this.initNavPanelTrans.find("initicon/Text").gameObject.getComponent("Text");
        // this.initNavSlideTrans = this.initNavPanelTrans.find("initslide");
        // this.initNavSlideImage = this.initNavSlideTrans.gameObject.getComponent("Image");
        // this.initNavSlideMaterial = this.initNavSlideImage.material;
        // this.initNavOKTrans = this.initNavPanelTrans.find("initok");
        //this.navAlgTxt = this.navigationPanelTrans.find("navalgtxt").gameObject.getComponent("Text");

        //navigation voice
        // this.naviVoiceTrans = this.navRootTrans.find("voicetips").transform;
        // this.startAudio = this.naviVoiceTrans.find("start").gameObject.getComponent("AudioSource");
        // this.relocationAudio = this.naviVoiceTrans.find("relocation").gameObject.getComponent("AudioSource");
        // this.straightAudio = this.naviVoiceTrans.find("straight").gameObject.getComponent("AudioSource");
        // this.leftAudio = this.naviVoiceTrans.find("left").gameObject.getComponent("AudioSource");
        // this.rightAudio = this.naviVoiceTrans.find("right").gameObject.getComponent("AudioSource");
        // this.upstairsAudio = this.naviVoiceTrans.find("upstairs").gameObject.getComponent("AudioSource");
        // this.downstairsAudio = this.naviVoiceTrans.find("downstairs").gameObject.getComponent("AudioSource");
        // this.upcrossingAudio = this.naviVoiceTrans.find("upcrossing").gameObject.getComponent("AudioSource");
        // this.downcrossingAudio = this.naviVoiceTrans.find("downcrossing").gameObject.getComponent("AudioSource");
        // this.endAudio = this.naviVoiceTrans.find("end").gameObject.getComponent("AudioSource");

        // this.logText = this.canvasTrans.find("logText").gameObject.getComponent("Text");
    },
    OnDisable: function()
    {
        //Insight.Debug.Log("lua call ResourceManager On Disable");
       // this = undefined;
    }
});

//Return the script module
ResourceManager