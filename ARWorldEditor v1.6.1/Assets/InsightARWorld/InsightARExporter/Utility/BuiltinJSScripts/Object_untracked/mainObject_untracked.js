function mainObject_untracked(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
mainObject_untracked.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

        // Fw_Event_MakeToast("main object untracked start ", 3);

        this.canvasTrans = Insight.GameObject.Find("ObjectCanvas").transform;

        // this.logTrans = this.canvasTrans.find("logText");
        // this.logText = this.logTrans.gameObject.getComponent("Text");
        // this.logTrans.gameObject.setActive(true); 
        // this.logText.text = "find logText\n";

        this.tipsPanelTrans = this.canvasTrans.find("tipsPanel");
        this.tipsTrans = this.tipsPanelTrans.find("tipsText");
        this.tipsText = this.tipsTrans.gameObject.getComponent("Text");
        this.tipsPanelTrans.gameObject.setActive(true); 
        this.tipsText.text = "试试扫描水果";

        this.InitContent();
    },

    InitContent: function() {

        this.contentTrans = this.canvasTrans.find("content");
        if (this.contentTrans && !this.contentMap) {
            this.contentMap = {
                "0_apple": this.contentTrans.find("apple"),
                "1_banana": this.contentTrans.find("banana"),
                "2_orange": this.contentTrans.find("orange"),
                "3_peach": this.contentTrans.find("peach"),
                "4_pear": this.contentTrans.find("pear"),
                "5_strawberry": this.contentTrans.find("strawberry")
            };
        }
    },
    
    // Update is called once per frame
    Update: function()
    {
        var resultIndex = 2;
        var trackingResult = Insight.Tracking.GetResultString(resultIndex);

        // this.logText.text = trackingResult;
        
        if (trackingResult != undefined && trackingResult != "") {

            var objectResult = JSON.parse(trackingResult);

            if (objectResult) {

                var status = objectResult.status;
                var clsName = objectResult.class_name;

                if (status === 1 && clsName !== "others" ) {

                    if (this.contentMap[clsName]) {

                        var target = this.contentMap[clsName];
                        target.gameObject.setActive(true);
                        this.tipsPanelTrans.gameObject.setActive(false);

                        if (this.curEnabledTarget !== target) {
                            if (this.curEnabledTarget) this.curEnabledTarget.gameObject.setActive(false);
                            this.curEnabledTarget = target;
                        }

                        return;
                    }
                }
            }        
        }

        if (this.curEnabledTarget) {
            this.tipsPanelTrans.gameObject.setActive(true);
            this.curEnabledTarget.gameObject.setActive(false);
            this.curEnabledTarget = null;
        }
    }
});

//Return the script module
mainObject_untracked