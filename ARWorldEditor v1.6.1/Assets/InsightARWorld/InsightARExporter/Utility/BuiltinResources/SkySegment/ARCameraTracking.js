function BackButton(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
BackButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.mainCam = Insight.GameObject.Find("Main Camera").transform;
        this.panoCam = Insight.GameObject.Find("PanoramaCamera").transform;
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    OnPointerUp: function()
	{
        
    }
});

//Return the script module
BackButton