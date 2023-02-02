function StopNavButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
StopNavButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    },
    OnPointerUp: function()
    {
        var _name = this.gameObject.name;
        //Insight.Debug.Log("On Point Up "+"Stop Nav Button");
        if(_name == "stopbutton")
        {
            g_GameStateCtrl.ChangeState(g_GameStatus.GAME_STATUS_LOCATION);
        }
    }
});

//Return the script module
StopNavButton