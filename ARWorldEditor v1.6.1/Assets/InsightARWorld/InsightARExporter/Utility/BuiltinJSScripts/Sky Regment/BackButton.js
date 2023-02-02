function BackButton(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
BackButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    OnPointerUp: function()
	{
        var _name = this.gameObject.name;
        if (_name == "backbutton") {
            Fw_Event_CloseARScene();
        }
    }
});

//Return the script module
BackButton