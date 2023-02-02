function light(gameObject) {
    this.gameObject = gameObject;
}
//Write prototype function here
light.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {
        //var str = Insight.Navigation.GetLightEstimate();
        //var obj = JSON.parse(str);
        //console.log(str);

        this.isFirstTrackSuccess = false;
        this.light = Insight.GameObject.Find("Sunlight").getComponent("Light");
        this.rootTrans = Insight.GameObject.Find("World").transform;
        this.canvas = Insight.GameObject.Find("HintController").transform;
        this.modelTrans = this.rootTrans.find("Earth_Opaque");

        this.modelTrans.transform.position = new Insight.Vector3(-13.42, -3.759, 4.36);
        //trans
        this.Cloud_LowPoly = this.modelTrans.find("Cloud_LowPoly");
        this.HeightLinesAndArrows = this.modelTrans.find("HeightLinesAndArrows");
        this.Height_LowPly = this.HeightLinesAndArrows.find("Height_LowPly");
        this.Arrow_Twoside = this.HeightLinesAndArrows.find("Arrow_Twoside");
        this.Arrow_Inside = this.Arrow_Twoside.find("Arrow_Inside");
        this.Arrow_Outside = this.Arrow_Twoside.find("Arrow_Outside");

        //mat
        this.cloudMat = this.Cloud_LowPoly.gameObject.getComponent("MeshRenderer").material;
        this.HeightLowPolyMat = this.Height_LowPly.gameObject.getComponent("MeshRenderer").material;
        this.Arrow_InsideMat = this.Arrow_Inside.gameObject.getComponent("MeshRenderer").material;
        this.Arrow_OutsideMat = this.Arrow_Outside.gameObject.getComponent("MeshRenderer").material;

        //value
        this.Height_LowPlyOpacity = 0;
        this.Height_LowPlyOffset = 0;
        this.Height_LowPlyStepTest = 0;



        this.cloudMat.setFloat("_TimeOffset", 0);
        this.HeightLowPolyMat.setFloat("_TimeOffset", 0);
        this.Arrow_InsideMat.setFloat("_TimeOffset", 0);
        this.Arrow_OutsideMat.setFloat("_TimeOffset", 0);

        this.Arrow_InsideMatColor1 = this.Arrow_InsideMat.getColor("_Color_1");
        this.Arrow_InsideMatColor2 = this.Arrow_InsideMat.getColor("_Color_2");
        this.Arrow_OutsideMattColor1 = this.Arrow_OutsideMat.getColor("_Color_1");
        this.Arrow_OutsideMatColor2 = this.Arrow_OutsideMat.getColor("_Color_2");

        this.Arrow_InsideMat.setColor("_Color_1", this.Arrow_InsideMatColor1.clone().setW(0));
        this.Arrow_InsideMat.setColor("_Color_2", this.Arrow_InsideMatColor2.clone().setW(0));
        this.Arrow_OutsideMat.setColor("_Color_1", this.Arrow_OutsideMattColor1.clone().setW(0));
        this.Arrow_OutsideMat.setColor("_Color_2", this.Arrow_OutsideMatColor2.clone().setW(0));


        this.lightValue = this.canvas.find("lightValue").gameObject.getComponent("Text");
        this.lightValue.text = "reset";

        this.earthTime = 12.0;

    },
    // Update is called once per frame
    Update: function () {

        if (Insight.Tracking.cloudLocationStatus == 1) {
            if (this.isFirstTrackSuccess == false) {
                this.isFirstTrackSuccess = true;
                this.StartAnim();
            }
        }
        //if (Insight.Tracking.status == 7) {
        //    if (this.isFirstTrackSuccess == false) {
        //        this.isFirstTrackSuccess = true;
        //        this.StartAnim();
        //    }
        //    if (!this.modelTrans.gameObject.activeSelf) {
        //        this.modelTrans.gameObject.setActive(true);
        //    }
        //}
        //else {
        //    if (this.modelTrans.gameObject.activeSelf) {
        //        this.modelTrans.gameObject.setActive(false);
        //    }
        //}
        var time = Insight.Time.time;


        this.cloudMat.setFloat("_TimeOffset", time);
        this.HeightLowPolyMat.setFloat("_TimeOffset", time);
        this.Arrow_InsideMat.setFloat("_TimeOffset", time);
        this.Arrow_OutsideMat.setFloat("_TimeOffset", time);
        this.modelTrans.rotate(0, 10 * Insight.Time.deltaTime, 0, true);

        var str = Insight.Navigation.GetLightEstimate();
        console.log("str:" + str);
        if (str != null) {
            try {
                //var st = parseFloat(str);
                ////var obj = JSON.parse(str);
                //console.log("obj:" + st);
                //Insight.Debug.Log("st:" + st.toString())
                ////console.log("tt:"+obj["illuminIntensity"])
                this.illuminIntensity = str;
                ////console.log("this.illuminIntensity :" + this.illuminIntensity);
                var value = this.illuminIntensity / 1000;
                this.illuminIntensity = value > 1 ? 1 : value;


                this.light.intensity = this.illuminIntensity * 2;
                console.log("this.light.intensity:" + this.light.intensity);
                this.lightValue.text = this.light.intensity + "";
            } catch (e) {
                console.error(e)
            }


        }
    },
    StartAnim: function () {
        Insight.DOTween.DOLocalMove(this.modelTrans, Insight.Vector3.New(-13.42, -3.227, 4.36), this.earthTime).setEase(Insight.Ease.InOutBack);
        Insight.InvokeManager.Invoke(this, function () {
            Insight.DOTween.DOLocalMove(this.modelTrans, Insight.Vector3.New(-13.42, -3.273, 4.36), 4.0).setEase(Insight.Ease.InOutSine).setLoops(-1, Insight.LoopType.Yoyo);
        }, this.earthTime);
        Insight.InvokeManager.Invoke(this, function () {
            Insight.DOTween.DOFloat(this.HeightLowPolyMat, 1, "_Opacity", 4).setEase(Insight.Ease.InOutSine);
            Insight.DOTween.DOFloat(this.HeightLowPolyMat, 1, "_Range", 4).setEase(Insight.Ease.InOutSine);
        }, this.earthTime / 2)
        Insight.InvokeManager.Invoke(this, function () {
            Insight.DOTween.DOColor(this.Arrow_InsideMat, this.Arrow_InsideMatColor1, "_Color_1", 4).setEase(Insight.Ease.InOutSine);
            Insight.DOTween.DOColor(this.Arrow_InsideMat, this.Arrow_InsideMatColor2, "_Color_2", 4).setEase(Insight.Ease.InOutSine);
            Insight.DOTween.DOColor(this.Arrow_OutsideMat, this.Arrow_OutsideMattColor1, "_Color_1", 4).setEase(Insight.Ease.InOutSine);
            Insight.DOTween.DOColor(this.Arrow_OutsideMat, this.Arrow_OutsideMatColor2, "_Color_2", 4).setEase(Insight.Ease.InOutSine);
        }, this.earthTime);
    },

});

//Return the script module
light