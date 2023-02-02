function RepeatButton(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
RepeatButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    OnPointerUp: function()
	{
        var _name = this.gameObject.name;
        
        if (_name == g_ResManager.trackFailButton.gameObject.name) 
        {
            //多次识别失败
            g_lastRecognitionCount = Insight.Tracking.cloudLocationTotalCount;
            g_ResManager.trackFailTrans.gameObject.setActive(false);
            if(!g_ResManager.initPanelTrans.gameObject.activeSelf && Insight.Tracking.cloudLocationStatus!=1)
                        g_ResManager.initPanelTrans.gameObject.setActive(true);
        }
        else if(_name ==g_ResManager.errorCodeButton.gameObject.name)
        {
            //服务器errorcode 开小差
            g_ResManager.errorCodeTrans.gameObject.setActive(false);
        }
    }
});

//Return the script module
RepeatButton