function CancelButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
CancelButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    // Start: function(){

    // },
    // Awake: function(){

    // },
    // OnEnable: function()
    // {

    // },
    // Update is called once per frame
    //Update: function()
    //{

    //},
    OnPointerUp: function()
    {
	    g_EventManager.SendEvent(EventType.EVENT_TYPE_CLICK_POI_CANCEL);
    }
});

//Return the script module
CancelButton