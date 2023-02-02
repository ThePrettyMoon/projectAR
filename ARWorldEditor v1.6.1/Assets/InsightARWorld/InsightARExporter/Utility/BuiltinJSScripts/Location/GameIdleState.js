function GameIdleState(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
GameIdleState.prototype = Object.assign(Object.create(Object.prototype), {
    Awake: function()
    {

    },
    // Start is called before the first frame update
    Start: function(){

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
    GetState: function()
    {
	    return g_GameStatus.GAME_STATUS_IDLE;
    },
    Close: function () {

    }

});

//Return the script module
GameIdleState