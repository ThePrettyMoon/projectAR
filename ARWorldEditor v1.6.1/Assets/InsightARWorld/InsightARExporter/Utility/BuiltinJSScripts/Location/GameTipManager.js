function GameTipManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
GameTipManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    }
});

//Return the script module
GameTipManager