function DefaultManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
DefaultManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.rootTrans = Insight.GameObject.Find("root").transform;
        this.modelTrans = this.rootTrans.find("qiannv_daoke_G");
       // this.audioSource = this.modelTrans.find("AudioSource").gameObject.getComponent("AudioSource");
    },
    // Update is called once per frame
    Update: function()
    {
        if(Insight.Tracking.status ==7 )
        {
            if(! this.modelTrans.gameObject.activeSelf)
            {
                this.modelTrans.gameObject.setActive(true);
               // this.audioSource.play();
            }
        }
        else
        {
            if(this.modelTrans.gameObject.activeSelf)
            {
                this.modelTrans.gameObject.setActive(false);
              //  this.audioSource.pause();
            }
        }
    }
});

//Return the script module
DefaultManager