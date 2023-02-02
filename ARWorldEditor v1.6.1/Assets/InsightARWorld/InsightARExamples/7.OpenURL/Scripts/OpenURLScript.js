function OpenURLScript(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
OpenURLScript.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
         this.canvasTran=Insight.GameObject.Find("UIRoot").transform;
         this.openBtn = this.canvasTran.find("BG/InputPoints/Btn_Open").gameObject.getComponent("Button",0);
         this.inputField = this.canvasTran.find("BG/InputPoints/InputField").gameObject.getComponent("InputField");
         Insight.Button.AddClick(this.openBtn,this,this.OpenURL);
    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    OpenURL:function()
    {
        this.paramTable = {
            url: this.inputField.text,
            type: {
                urlType: 1,
                title: "NeteaseAR",
                param1: "showBrandInfo"
            }
        };
        this.encodedJson = JSON.stringify( this.paramTable );
        //  let match2 = /^((http|https):\/\/)?(([A-Za-z0-9]+-[A-Za-z0-9]+|[A-Za-z0-9]+)\.)+([A-Za-z]+)[/\?\:]?.*$/;
        //  let vol2 = match2.test(this.inptuField.text);
        Insight.Event.Happen(1,1,108,this.encodedJson);
    }

});

//Return the script module
OpenURLScript