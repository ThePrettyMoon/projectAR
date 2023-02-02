function AnimationPlay(gameObject)
{
    this.gameObject = gameObject;
}

AnimationPlay.prototype = Object.assign(Object.create(Object.prototype), {

    Start: function(){
        //首先找到场景中人物的Animator组件，通过这个组件我们将人物动画进行一个控制
        this.animator=Insight.GameObject.Find("UIRoot/BG/toolman").getComponent("Animator",0);
        this.animator.speed = 0;
        //找到几个控制的button
        this.canvasTran=Insight.GameObject.Find("UIRoot").transform;
        this.playButton=this.canvasTran.find("Btns/Btn_Play").gameObject.getComponent("Button",0);
        this.img_play = this.playButton.transform.find("Img_Play").gameObject;
        this.img_pause = this.playButton.transform.find("Img_Pause").gameObject;
        this.text = this.playButton.transform.find("Text").gameObject.getComponent("Text");
        this.stopButton=this.canvasTran.find("Btns/Btn_Stop").gameObject.getComponent("Button",0);
        this.resetButton=this.canvasTran.find("Btns/Btn_RePlay").gameObject.getComponent("Button",0);
        
        //使用AddClick()函数来给button添加事件，第一个参数为button，第三个参数为回调函数
        Insight.Button.AddClick(this.playButton,this,this.PlayAnimation);
        Insight.Button.AddClick(this.stopButton,this,this.StopAnimation);
        Insight.Button.AddClick(this.resetButton,this,this.ResetAnimation);
    },

    Update: function()
    {
        
    },

    //VideoPlayer组件的API函数：play()播放;time是当前播放时间属性，设置为0即暂停 1即继续   停止就是利用play()方法 第三个参数0重新开始并暂停从而达到一个停止的效果 重新开始跟第停止一样;
    PlayAnimation:function()
    {
        if(this.img_play.activeSelf)
        {
            this.text.text = "暂停";
            this.animator.speed = 1;
            this.SetState(false,true);
        }
        else
        {
            this.text.text = "播放";
            this.animator.speed = 0;
            this.SetState(true,false);
        }
    },
    StopAnimation:function()
    {
        this.SetState(true,false);
        this.animator.play("Walk", 0, 0);
        this.animator.speed = 0;
    },

    ResetAnimation:function()
    {
        this.animator.speed = 1;
        this.animator.play("Walk", 0, 0);
    },
    SetState:function(obj1,obj2)
    {
        this.img_play.setActive(obj1);
        this.img_pause.setActive(obj2);
    }
});

AnimationPlay