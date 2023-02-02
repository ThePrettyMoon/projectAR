function LandmarkIdleState(gameObject)
{
    this.transform = gameObject.transform;
    this.game_object = gameObject;
}
//Write prototype function here
LandmarkIdleState.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        
    },
    Awake: function(){

    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    Enter: function()
	{
    },
    Execute: function()
    {

    },
    Exit: function()
	{

    },
    Init: function()
    {

    },
    Close: function () {

    }
});

//Return the script module
LandmarkIdleState