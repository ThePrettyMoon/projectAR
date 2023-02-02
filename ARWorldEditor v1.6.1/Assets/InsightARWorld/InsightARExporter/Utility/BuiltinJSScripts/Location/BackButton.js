function BackButton(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
BackButton.prototype = Object.assign(Object.create(Object.prototype), {
   
    OnPointerUp: function()
	{
        var _name = this.gameObject.name;
        if (_name == "backbutton") {
            if (g_GameManager)
                g_GameManager.Close();
            Fw_Event_CloseARScene();
        }
    }
});

//Return the script module
BackButton