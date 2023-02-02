function AudioButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
AudioButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    // Start: function(){

    // },
    // // Update is called once per frame
    // Update: function()
    // {

    // },
    // OnEnable: function()
    // {
    // },
    OnPointerUp: function () {
        var objName = this.gameObject.name;
        if (objName == "audiobuttonon") {
            g_ResManager.audioOnTrans.gameObject.setActive(false);
            g_ResManager.audioOffTrans.gameObject.setActive(true);
            //g_ResManager.locationAudioSource.volume = 0.0;
            g_ResManager.locationAudioTrans.gameObject.setActive(false);
        } else if (objName == "audiobuttonoff") {
            g_ResManager.audioOnTrans.gameObject.setActive(true);
            g_ResManager.audioOffTrans.gameObject.setActive(false);
           // g_ResManager.locationAudioSource.volume = 1.0;
            g_ResManager.locationAudioTrans.gameObject.setActive(true);
        }

    },
    OnDisable: function()
    {
      //  g_ResManager.locationAudioSource.stop();
    }
});

AudioButton
//Return the script module
