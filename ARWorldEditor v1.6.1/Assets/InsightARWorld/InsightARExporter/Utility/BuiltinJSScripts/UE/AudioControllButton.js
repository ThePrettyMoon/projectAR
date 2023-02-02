function AudioControllButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
    this.speed = 20; 
	this.rotateAngle = 0.0;
	this.isAudioPlay = false;
}
//Write prototype function here
AudioControllButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.audioTrans = Insight.GameObject.Find("root/audio").transform;
        this.audioSource = audioTrans.gameObject.getComponent("AudioSource");
    },
    // Update is called once per frame
    Update: function()
    {
        if(this.isAudioPlay)
        {
            this.rotateAngle = this.rotateAngle - Insight.Time.deltaTime * this.speed;
            if(this.rotateAngle <-360)
            {
                this.rotateAngle = this.rotateAngle + 360;
            }
            this.transform.localRotation = Insight.Quaternion.AngleAxis(this.rotateAngle, new Insight.Vector3(0,0,1));
        }
    },
    OnEnable: function()
    {
        
    },
    OnDisable: function()
    {
        this.isAudioPlay = false;
        this.audioSource.stop();
    },
    OnPointerUp: function()
    {
        if(!this.isAudioPlay)
        {
            this.PlayAudio();
        }
        else
        {
            this.PauseAudio();
        }
    },
    PlayAudio: function()
    {
        this.isAudioPlay = true;
        this.audioSource.play();
    },
    PauseAudio: function()
    {
        this.isAudioPlay = false;
        this.audioSource.pause();
    }
});

//Return the script module
AudioControllButton