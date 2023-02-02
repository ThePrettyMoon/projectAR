function EnterPoiButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
EnterPoiButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    // Start: function(){

    // },
    // Awake: function(){

    // },
    // OnEnable: function()
    // {

    // },
    // // Update is called once per frame
    // Update: function()
    // {

    // },
    OnPointerUp: function()
    {
	    //enter poi
	    g_EventManager.SendEvent(EventType.EVENT_TYPE_CLICK_ENTER_POI);
    }
});

//Return the script module
EnterPoiButton