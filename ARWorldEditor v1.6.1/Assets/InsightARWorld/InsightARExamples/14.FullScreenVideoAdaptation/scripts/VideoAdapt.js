function VideoAdapt(gameObject)
{
    this.className = "VideoAdapt";
    this.gameObject = gameObject;
}
//Write prototype function here
VideoAdapt.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        this.videoRect = Insight.GameObject.Find("Canvas/RawImage").getComponent("RectTransform");
        this.videoPlayer = this.videoRect.gameObject.getComponent("VideoPlayer");
    },
    // Update is called once per frame
    Update: function()
    {
        /**
         * 该全屏视频方案 假设视频是横版视频
         * 该全屏方案 基于视频宽高比率不变的条件进行 对屏幕分辨率适配
         * */
        this.screenWidth = Insight.Screen.width;
        this.screenHeight = Insight.Screen.height;
        this.videoWidth = this.videoPlayer.width;
        this.videoHeight = this.videoPlayer.height


        //视频宽对比 竖屏高
        this.widthRate = this.videoWidth / this.screenHeight;
        //视频高对比 竖屏宽
        this.heightRate = this.videoHeight / this.screenWidth;
       


        //如果视频宽大于屏幕横屏宽，但视频高低于横屏屏幕高-》基于横屏宽度比率需要缩放-》此时确保横屏时 视频的高度不会超过屏幕高，且比率适配
        if (this.widthRate > 1 && this.heightRate < 1) {
            this.acWidth = this.videoWidth / this.widthRate;
            this.acHeight = this.videoHeight / this.widthRate;
        }
        //如果视频高大于横屏屏幕高，但视频宽低于横屏屏幕宽-》基于横屏高度比率需要缩放-》此时确保高不会超过屏幕高，且比率适配
        else if (this.heightRate > 1 && this.widthRate < 1) {
            this.acWidth = this.videoWidth / this.heightRate;
            this.acHeight = this.videoHeight / this.heightRate;
        }
        //如果视频宽等于屏幕高，且视频高等于屏幕宽
        else if (this.heightRate == 1 && this.widthRate == 1) {
            this.acWidth = this.screenHeight;
            this.acHeight = this.screenWidth;
        }
        //如果视频宽度大于屏幕横屏宽度，且视频高度大于屏幕横屏高-》需要判断基于视频的哪一边做适配
        else if (this.widthRate > 1 && this.heightRate > 1) {
            //视频宽高比率，宽度较大，当同等比例情况下， 宽度容易扩大出范围，所以此时需要以宽为基准调整
            if (this.widthRate > this.heightRate) {
                this.acWidth = this.videoWidth / this.widthRate
                this.acHeight = this.videoHeight / this.widthRate;
            }
            //视频宽高比率，高度更高，当同等比例情况下， 高度容易扩大出范围，所以此时需要以高为基准调整
            else if (this.widthRate < this.heightRate) {
                this.acWidth = this.videoWidth / this.heightRate
                this.acHeight = this.videoHeight / this.heightRate;
            }
            //缩放相同情况下，直接取一侧就行
            else {
                this.acWidth = this.videoWidth / this.heightRate
                this.acHeight = this.videoHeight / this.heightRate;
            }
        }
        else if (this.widthRate < 1 && this.heightRate < 1)
        {
            //视频宽高比率，宽度较大，当同等比例情况下， 宽度容易扩大出范围，所以此时需要以宽为基准调整
            if (this.widthRate > this.heightRate)
            {
                this.acWidth = this.videoWidth / this.widthRate
                this.acHeight = this.videoHeight / this.widthRate;
            }
            //视频宽高比率，高度更高，当同等比例情况下， 高度容易扩大出范围，所以此时需要以高为基准调整
            else if (this.widthRate < this.heightRate)
            {
                this.acWidth = this.videoWidth / this.heightRate
                this.acHeight = this.videoHeight / this.heightRate;
            }
            //缩放相同情况下，直接取一侧就行
            else
            {
                this.acWidth = this.videoWidth / this.heightRate
                this.acHeight = this.videoHeight / this.heightRate;
            }
        }
        this.videoRect.sizeDelta = new Insight.Vector2(this.acWidth, this.acHeight);
    }
});

// Debugger Methods Begin
var SafeFunctor=SafeFunctor||function(func){return function(){try{return func.apply(this,arguments)}catch(e){var msg="ClassName: "+this.className+", LineNo: "+(e.lineNumber||e.line)+", Msg: "+e.message+"\r\nStackTrace: "+(e.stackTrace||e.stack);console.error(msg)}}};
var SafeWrapper=SafeWrapper||function(classObject){var o=classObject.prototype;for(var key in o){var func=o[key];if(typeof(func)==='function'){o[key]=SafeFunctor(func)}}};
SafeWrapper(VideoAdapt);
// Debugger Methods End
//Return the script module
VideoAdapt