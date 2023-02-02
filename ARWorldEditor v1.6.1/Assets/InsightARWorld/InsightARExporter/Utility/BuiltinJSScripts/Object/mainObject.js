function mainObject(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
mainObject.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

        this.canvasTrans = Insight.GameObject.Find("Canvas").transform;

        // this.logTrans = this.canvasTrans.find("logText");
        // this.logText = this.logTrans.gameObject.getComponent("Text");
        // this.logTrans.gameObject.setActive(true); 
        // this.logText.text = "find logText\n";

        this.dragonball = Insight.GameObject.Find("dragonball").transform; 
        this.dragonball.gameObject.setActive(false);

        this.tipsPanelTrans = this.canvasTrans.find("tipsPanel");
        this.tipsTrans = this.tipsPanelTrans.find("tipsText");
        this.tipsText = this.tipsTrans.gameObject.getComponent("Text");
        this.tipsPanelTrans.gameObject.setActive(true); 
        this.tipsText.text = "请扫描七龙珠模型";

        this.curStatus = 1;
    },

    // Update is called once per frame
    Update: function()
    {
        if (Insight.Tracking.status === 7) {

            if (this.curStatus && this.curStatus !== 7) {

                this.dragonball.gameObject.setActive(true);
                this.tipsPanelTrans.gameObject.setActive(false); 

                this.curStatus = 7;
            }
        }
        else {

            if (this.curStatus && this.curStatus !== 1) {

                this.dragonball.gameObject.setActive(false);
                this.tipsPanelTrans.gameObject.setActive(true); 

                this.curStatus = 1;
            }
        }
    }
});

//Return the script module
mainObject