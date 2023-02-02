function mainHand(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}

//Write prototype function here
mainHand.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function() {

        // Fw_Event_MakeToast("main behaviour start ", 3);
        // Insight.Debug.Log("js call start gesture ");

        this.canvasTrans = Insight.GameObject.Find("HandCanvas").transform;

        // this.logTrans = this.canvasTrans.find("logText");
        // this.logText = this.logTrans.gameObject.getComponent("Text");
        // this.logTrans.gameObject.setActive(true); 
        // this.logText.text = "find logText\n";

        this.tipsPanelTrans = this.canvasTrans.find("tipsPanel");
        this.tipsTrans = this.tipsPanelTrans.find("tipsText");
        this.tipsText = this.tipsTrans.gameObject.getComponent("Text");
        this.tipsPanelTrans.gameObject.setActive(true); 
        this.tipsText.text = "做个手势扫描试试";
        
        this.InitContent();
    },

    InitContent: function() {

        this.contentTrans = this.canvasTrans.find("content");

        if (this.contentTrans && !this.contentMap) {

            this.contentMap = {
                "fist": this.contentTrans.find("fist"),
                "thumb": this.contentTrans.find("thumb"),
                "lges": this.contentTrans.find("lges"),
                "vges": this.contentTrans.find("vges"),
                "okges": this.contentTrans.find("okges"),
                "palm": this.contentTrans.find("palm"),
                "sheart": this.contentTrans.find("sheart"),
                "rockges": this.contentTrans.find("rockges"),
                "dheart": this.contentTrans.find("dheart"),
                "index": this.contentTrans.find("index")
            };
        }
    },

    // Update is called once per frame
    Update: function() {

        var resultIndex = 1;
        var trackingResult = Insight.Tracking.GetResultString(resultIndex);

        if (trackingResult != undefined && trackingResult != "") {

            var gestureResult = JSON.parse(trackingResult);

            if (gestureResult) {

                var output = "";
                var status = gestureResult.status;
                var num = gestureResult.gesture_num;

                if (status === 1 && num > 0) {
                    var gestures = gestureResult.gestures;
                    var currentGesture = gestures[0];
                    var clsName = currentGesture.class_name;
                    var rect = currentGesture.rect;

                    // output = "get gestureResult: " + clsName + " " + rect + "\nScreen: " + Insight.Screen.width + " " + Insight.Screen.height+ " " + Insight.Screen.ImageWidth() + " " + Insight.Screen.ImageHeight();

                    if (this.contentMap[clsName]) {

                        var target = this.contentMap[clsName];
                        this.UpdateTargetRect(target, rect);
                        this.tipsPanelTrans.gameObject.setActive(false);

                        if (this.curEnabledTarget !== target) {
                            if (this.curEnabledTarget) this.curEnabledTarget.gameObject.setActive(false);
                            this.curEnabledTarget = target;
                        }

                        // this.logText.text = output;
                        return;
                    }
                }
                // else {
                //     this.logText.text = "get exceptional gestureResult:\nstatus="+ status + ", gesture_num=" + num;
                // }
            }
        }

        if (this.curEnabledTarget) {
            this.tipsPanelTrans.gameObject.setActive(true);
            this.curEnabledTarget.gameObject.setActive(false);
            this.curEnabledTarget = null;
        }
    },

    UpdateTargetRect: function(trans, rectStr) {

        var rect_split = rectStr.split(" ");
        var rect1x = parseFloat(rect_split[0]);
        var rect1y = parseFloat(rect_split[1]);
        var rect2x = parseFloat(rect_split[2]) + rect1x;
        var rect2y = parseFloat(rect_split[3]) + rect1y;

        var imageWidth = Insight.Screen.ImageHeight();  // 960 or 720
        var imageHeight = Insight.Screen.ImageWidth();  // 1280
		var imageAspect = imageWidth/imageHeight;
		var screenWidth = Insight.Screen.width;
		var screenHeight = Insight.Screen.height;
		var screenAspect = screenWidth/screenHeight;
		var ratio = screenAspect/imageAspect;
		// if(ratio <1) {
		// 	rect1x = ((rect1x/imageWidth -0.5)*ratio + 0.5)*screenWidth;
		// 	rect2x = ((rect2x/imageWidth -0.5)*ratio + 0.5)*screenWidth;
		// 	rect1y = (1.0-rect1y/imageHeight) * screenHeight;
		// 	rect2y = (1.0-rect2y/imageHeight) * screenHeight;            
        // }
		// else {
		// 	rect1x = rect1x/imageWidth*screenWidth;
		// 	rect2x = rect2x/imageWidth*screenWidth;
		// 	rect1y = (-(rect1y/imageHeight -0.5)*ratio + 0.5)*screenHeight;
		// 	rect2y = (-(rect2y/imageHeight -0.5)*ratio + 0.5)*screenHeight;		
        // }

        if (rect1x > 0 && rect2x < imageWidth && rect1y > 0 && rect2y < imageHeight) {

            rect1x = rect1x / imageWidth * screenWidth;
            rect2x = rect2x / imageWidth * screenWidth;
            rect1y = (1.0 - rect1y / imageHeight) * screenHeight;
            rect2y = (1.0 - rect2y / imageHeight) * screenHeight;

            var positionX = (rect1x + rect2x) / 2.0;
            var positionY = (rect1y + rect2y) / 2.0;

            var rectTrans = trans.gameObject.getComponent("RectTransform");
            rectTrans.anchoredPosition3D = new Insight.Vector3(positionX, positionY, 0);  
            trans.gameObject.setActive(true);
        }
        else {
            trans.gameObject.setActive(false);
        }
    }
});

//Return the script module
mainHand