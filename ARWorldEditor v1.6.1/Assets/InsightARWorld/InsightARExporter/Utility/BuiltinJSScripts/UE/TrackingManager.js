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
    },
    // Update is called once per frame
    Update: function()
    {
        if(Insight.Tracking.status ==7 )
        {
            if(! this.modelTrans.gameObject.activeSelf)
            {
                this.modelTrans.gameObject.setActive(true);
            }
        }
        else
        {
            if(this.modelTrans.gameObject.activeSelf)
            {
                this.modelTrans.gameObject.setActive(false);
            }
        }
    }
});

//Return the script module
RecognitionManager