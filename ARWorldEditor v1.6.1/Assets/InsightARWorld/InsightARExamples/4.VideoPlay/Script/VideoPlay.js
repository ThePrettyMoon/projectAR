function VideoPlay(gameObject)
{
    this.gameObject = gameObject;
}

VideoPlay.prototype = Object.assign(Object.create(Object.prototype), {

    Start: function(){
        //首先找到场景中的VideoPlayer组件，通过这个组件我们将视频渲染到物体材质上，这里我们将材质的shader设置成Unlit/Transparent,支持透明通道，为了方便演示将物体放在MainCamera下，大场景模板中一般作为World子物体
        this.video_3d=Insight.GameObject.Find("MainCamera/VideoQuad").getComponent("VideoPlayer",0);
        this.video_2d=Insight.GameObject.Find("Canvas/Video").getComponent("VideoPlayer",0);
        //找到几个控制的button
        this.canvasTran=Insight.GameObject.Find("Canvas").transform;
        this.playButton=this.canvasTran.find("PlayButton").gameObject.getComponent("Button",0);
        this.pauseButton=this.canvasTran.find("PauseButton").gameObject.getComponent("Button",0);
        this.stopButton=this.canvasTran.find("StopButton").gameObject.getComponent("Button",0);
        this.resetButton=this.canvasTran.find("ResetButton").gameObject.getComponent("Button",0);
        //使用AddClick()函数来给button添加事件，第一个参数为button，第三个参数为回调函数
        Insight.Button.AddClick(this.playButton,this,this.PlayVideo);
        Insight.Button.AddClick(this.pauseButton,this,this.PauseVideo);
        Insight.Button.AddClick(this.stopButton,this,this.StopVideo);
        Insight.Button.AddClick(this.resetButton,this,this.ResetVideo);
    },

    Update: function()
    {

    },

    //VideoPlayer组件的API函数：play()播放;stop()停止;pause()暂停;time是当前播放时间属性，设置为0即从头播放;
    PlayVideo:function()
    {
        this.video_3d.play();
        this.video_2d.play();
        this.playButton.gameObject.setActive(false);
        this.pauseButton.gameObject.setActive(true);
    },

    PauseVideo:function()
    {
        this.video_3d.pause();
        this.video_2d.pause();
        this.playButton.gameObject.setActive(true);
        this.pauseButton.gameObject.setActive(false);
    },

    StopVideo:function()
    {
        this.video_3d.stop();
        this.video_2d.stop();
        this.playButton.gameObject.setActive(true);
        this.pauseButton.gameObject.setActive(false);
    },

    ResetVideo:function()
    {
        this.video_3d.time=0;
        this.video_2d.time=0;
    }
});

VideoPlay