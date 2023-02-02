function LandmarkResource(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
LandmarkResource.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
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
        
        // model
        this.modelRootTrans = Insight.GameObject.Find("World").transform;
        
        //trackpanel
        this.trackPanelTrans = this.canvasTrans.find("trackpanel");
        this.upIconTrans = this.trackPanelTrans.find("upicon");
        this.downIconTrans = this.trackPanelTrans.find("downicon");
        this.trackLimitedTrans = this.trackPanelTrans.find("tracklimitedicon");
        this.trackFailTrans = this.trackPanelTrans.find("trackfailicon");
        this.trackFailButton = this.trackFailTrans.find("trackfailbutton").gameObject.getComponent("Button");
        this.errorCodeTrans = this.trackPanelTrans.find("errorcodeicon");
        this.errorCodeButton = this.errorCodeTrans.find("errorcodebutton").gameObject.getComponent("Button");
        this.errorCodeText = this.errorCodeTrans.find("errorcodetext").gameObject.getComponent("Text");
    }
});

//Return the script module
LandmarkResource