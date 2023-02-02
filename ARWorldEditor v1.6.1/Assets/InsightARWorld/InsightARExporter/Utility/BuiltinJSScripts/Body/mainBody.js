function mainBody(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}

//Write prototype function here
mainBody.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function() {

        // Fw_Event_MakeToast("main body start ", 3);
        // Insight.Debug.Log("js call start gesture ");

        this.canvasTrans = Insight.GameObject.Find("BodyCanvas").transform;

        // this.logTrans = this.canvasTrans.find("logText");
        // this.logText = this.logTrans.gameObject.getComponent("Text");
        // this.logTrans.gameObject.setActive(true); 
        // this.logText.text = "find logText\n";

        this.tipsPanelTrans = this.canvasTrans.find("tipsPanel");
        this.tipsTrans = this.tipsPanelTrans.find("tipsText");
        this.tipsText = this.tipsTrans.gameObject.getComponent("Text");
        this.tipsPanelTrans.gameObject.setActive(true); 
        this.tipsText.text = "摆个姿势扫描试试";
        
        this.InitContent();

        this.curEnabledTargets = [];
    },

    InitContent: function() {

        this.contentTrans = this.canvasTrans.find("content");

        if (this.contentTrans && !this.contentMap) {

            this.contentMap = {
                1: this.contentTrans.find("leftpush"),
                2: this.contentTrans.find("rightpush"),
                4: this.contentTrans.find("heart"),
                5: this.contentTrans.find("leftcall"),
                6: this.contentTrans.find("rightcall"),
                7: this.contentTrans.find("rightbow"),
                8: this.contentTrans.find("leftbow")
            };
        }
    },

    // Update is called once per frame
    Update: function() {

        var resultIndex = 6;
        var trackingResult = Insight.Tracking.GetResultString(resultIndex);

        // this.logText.text = "get trackingResult:\n" + trackingResult;
        
        if (trackingResult != undefined && trackingResult != "") {
        
            var bodyResult = JSON.parse(trackingResult);

            if (bodyResult) {

                var output = "";
                var status = bodyResult.status;
                var num = bodyResult.person_nums;

                if (status === 1 && num > 0) {

                    this.SetTargetsStatus(false);

                    var pose = bodyResult.pose;
                    var poseCount = pose.length;
                    if (poseCount > 2) poseCount = 2;

                    for (var j = 0; j < poseCount; j++) {
                        var currentPose = pose[j];
                        var actionID = currentPose.action_id;
                        // var rect = currentPose.bbox;

                        if (this.contentMap[actionID]) {
                            var target = this.contentMap[actionID];
                            this.curEnabledTargets.push(target);
                        }
                    }

                    if (this.curEnabledTargets && this.curEnabledTargets.length > 0) {
                        this.tipsPanelTrans.gameObject.setActive(false);
                        this.SetTargetsStatus(true);
                        return;
                    }
                }
            }
        }

        this.tipsPanelTrans.gameObject.setActive(true);
        this.SetTargetsStatus(false);
    },

    SetTargetsStatus: function(status) {

        if (this.curEnabledTargets && this.curEnabledTargets.length > 0) {
            var anchoredPosY = (this.curEnabledTargets.length - 1) * 60;
            for (var i = 0; i < this.curEnabledTargets.length; i++) {
                if (this.curEnabledTargets[i]) {
                    this.curEnabledTargets[i].gameObject.setActive(status);

                    if (status) {
                        var rectTrans = this.curEnabledTargets[i].gameObject.getComponent("RectTransform");
                        rectTrans.anchoredPosition3D = new Insight.Vector3(0, anchoredPosY - i * 120, 0);  
                    }
                }
            }

            if (!status) this.curEnabledTargets = [];
        }
    }
});

//Return the script module
mainBody