function AudioPlay(gameObject)
{
    this.gameObject = gameObject;
}

AudioPlay.prototype = Object.assign(Object.create(Object.prototype), {

    Start: function(){
        //UI父节点
        this.canvasTrans = Insight.GameObject.Find("UIRoot").transform;
        //Button组件
        this.playButton = this.canvasTrans.find("Button_Play").gameObject.getComponent("Button");
        this.stopButton = this.canvasTrans.find("Button_Stop").gameObject.getComponent("Button");
        this.nextButton = this.canvasTrans.find("Button_Next").gameObject.getComponent("Button");
        //Image组件
        this.playImg = this.playButton.transform.find("Image_Play").gameObject.getComponent("Image");
        this.pauseImg = this.playButton.transform.find("Image_Pause").gameObject.getComponent("Image");
        this.noteImg = Insight.GameObject.Find("Image_Note").getComponent("Image");
        //Slider组件
        this.volumeSlider = this.canvasTrans.find("Slider_Volume").gameObject.getComponent("Slider");
        //Text组件
        this.playText = this.playButton.transform.find("Text_Play").gameObject.getComponent("Text");
        this.volumeText = this.volumeSlider.transform.find("Text_Volume").gameObject.getComponent("Text");

        //音频组件父节点
        this.audioTrans = Insight.GameObject.Find("AudioRoot").transform;
        this.audioBgm = [];
        this.audioLength = 2;
        for(var i = 0; i < this.audioLength; i++)
        {
            var audioName = "as_bgm"+i.toString();
            this.audioBgm[i] = Insight.GameObject.Find(audioName).getComponent("AudioSource");
        }
        
        //添加点击事件
        Insight.Button.AddClick(this.playButton, this, this.ClickPlayAudio);
        Insight.Button.AddClick(this.stopButton, this, this.ClickStopAudio);
        Insight.Button.AddClick(this.nextButton, this, this.ClickNextAudio);
        //滑动条
        Insight.Slider.OnValueChanged(this.volumeSlider, this, this.SetAudioVolue)

        //当前播放序列
        this.curIndex = 0;
    },

    Update: function()
    {

    },

    //设置音频音量
    SetAudioVolue: function(value)
    {
        this.audioBgm[this.curIndex].volume = value;
        this.volumeText.text = "音量："+ this.audioBgm[this.curIndex].volume.toFixed(2);
    },
    //播放音频
    ClickPlayAudio:function()
    {
        if(this.audioBgm[this.curIndex].isPlaying)
        {
            this.ResetPlayText(false);
            this.audioBgm[this.curIndex].pause();
        }
        else
        {
            this.audioBgm[this.curIndex].play();
            this.ResetPlayText(true);
        }
    },
    //重置播放文本
    ResetPlayText: function(status)
    {
        this.noteImg.enabled = status;
        this.playImg.enabled = !status;
        this.pauseImg.enabled = status;
        if(status)
            this.playText.text = "暂停";
        else
            this.playText.text = "播放";
    },
    //停止播放音频
    ClickStopAudio:function()
    {
        this.audioBgm[this.curIndex].stop();
        this.ResetPlayText(false);
    },
    //播放下一首音频
    ClickNextAudio:function()
    {
        //歌曲正在播放，先停止
        if(this.audioBgm[this.curIndex].isPlaying)
        {
            this.audioBgm[this.curIndex].stop();
            this.ResetPlayText(false);
        }
        //序列+1
        if(this.curIndex < this.audioLength - 1)
            this.curIndex = this.curIndex + 1;
        else
            this.curIndex = 0;
        this.audioBgm[this.curIndex].play();
        this.ResetPlayText(true);
    }
});

AudioPlay