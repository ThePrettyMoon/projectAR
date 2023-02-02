function BackButton(gameObject)
{
    this.transform = gameObject.transform;
    this.gameObject = gameObject;
}
//Write prototype function here
BackButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    OnPointerUp: function()
	{
        var _name = this.gameObject.name;
        if (_name == "backbutton") {
            if (g_LandmarkManager)
                g_LandmarkManager.Close();
            Fw_Event_CloseARScene();
        }
    }
});

//Return the script module
BackButton