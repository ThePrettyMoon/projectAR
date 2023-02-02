function RecognitionManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here

RecognitionManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.rootTrans = Insight.GameObject.Find("root").transform;
        this.modelTrans = this.rootTrans.find("qiannv_huashi_B");
        //初始时隐藏
        this.modelTrans.gameObject.setActive(false)
        //识别回调注册
        Insight.Tracking.OnRecongnizedTarget(this,this.OnRecongnizedTarget);
        //识别图片的名字
        this.pictureName="arworld_20210712093620602_Z2kTmq1i";


        /*识别使用的算法类型
        "InsightARClassifiedTypeQRCode"
        "InsightARClassifiedTypeARCode"
        "InsightARClassifiedType2dImage"
        "InsightARClassifiedTypeObject"
        "InsightARClassifiedTypeFace"
        "InsightARClassifiedTypeGesture"
        "InsightARClassifiedTypeBody"
        "InsightARClassifiedTypeFaceUnity"
        */ 
        this.arType = "InsightARClassifiedType2dImage";
    },

    OnRecongnizedTarget: function( type, result ){
        Insight.Debug.Log("js OnRecongnizedTarget " + type + ", " + result);

        if(type != this.arType)
        {
            Fw_Event_MakeToast("算法未对应，请检查arType是否与平台算法配置一致。" ,1);
            return;
        }
        if (result != this.pictureName) {
            Fw_Event_MakeToast("识别图片与目标图片不一致" ,1);
            return;
        }

        if(!this.modelTrans.gameObject.activeSelf)
        {
            this.modelTrans.gameObject.setActive(true)
        }
    },
    
});
RecognitionManager