function BackButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
BackButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    },
    OnPointerUp: function()
    {
        Fw_Event_UnloadPoiData("98", "132", "2");
    }
});

//Return the script module
BackButton