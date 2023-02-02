function SetActiveByModel(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
SetActiveByModel.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.canvasTran=Insight.GameObject.Find("UIRoot").transform;
        this.setactiveButton = this.canvasTran.find("Btn_SetActive").gameObject.getComponent("Button",0);
        this.role = this.canvasTran.find("BG/toolman").gameObject;
        this.img_show = this.setactiveButton.transform.find("Img_Show").gameObject;
        this.img_hide = this.setactiveButton.transform.find("Img_Hide").gameObject;
        this.text = this.setactiveButton.transform.find("Text").gameObject.getComponent("Text");
        Insight.Button.AddClick(this.setactiveButton,this,this.SetStateAnimation);
        this.SetBtnState(false,true);
        this.text.text = "隐藏";
    },
    // Update is called once per frame
    Update: function()
    {
        
    },
    SetStateAnimation:function()
    {
        if(this.role.activeSelf)
        {
            this.role.setActive(false);
            this.SetBtnState(true,false);
            this.text.text = "显示";
        }
        else
        {
            this.role.setActive(true);
            this.SetBtnState(false,true);
            this.text.text = "隐藏";
        }
    },
    SetBtnState(obj1,obj2)
    {
        this.img_show.setActive(obj1);
        this.img_hide.setActive(obj2);
    }
});

//Return the script module
SetActiveByModel