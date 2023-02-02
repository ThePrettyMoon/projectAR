function UIInteraction(gameObject)
{
    this.gameObject = gameObject;
}
//Write prototype function here
UIInteraction.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {
        var canvas = Insight.GameObject.Find("Canvas").transform;

        /**
         * Text 
         * **/
        this.textContent = canvas.find("text/content").gameObject.getComponent("Text");

        /**
         * InputField
         * https://near.yuque.com/arworld/pgywq3/fksly6#InputField
         * **/
        this.inputField = canvas.find("inputfieldtext/InputField").gameObject.getComponent("InputField");
        Insight.InputField.OnValueChanged(this.inputField, this, this.onValueChanged);

        /**
         * Button
         * **/
        this.button = canvas.find("button").gameObject.getComponent("Button");
        Insight.Button.AddClick(this.button, this, this.OnButtonClick);

        /**
         * Slider
         * **/
        this.slider = canvas.find("slider").gameObject.getComponent("Slider");
        this.sliderValue = canvas.find("slider/value").gameObject.getComponent("Text");
        Insight.Slider.OnValueChanged(this.slider, this, function (value) {
            //console.log("滑动值" + value)
            this.sliderValue.text = "value:"+value;
        })
    },
    // Update is called once per frame
    Update: function()
    {

    },

    /**
     * InputField 内容发生改变时 回调事件
     * @param {"当前输入框中的内容"} content
     */
    onValueChanged: function (content) {
        console.log("onValueChanged content:" + content);
        this.textContent.text = content;
    },

    /**
     * Button点击事件
     * */
    OnButtonClick: function () {
        console.log("OnButtonClick");
        this.inputField.interactable = !this.inputField.interactable;
    },

    /**
     * Slider 滑动值变化时的回调
     * @param {"滑动值"} content
     */
    OnSliderValueChanged: function (content) {
        console.log("OnSliderValueChanged content:" + content);
    },





});

//Return the script module
UIInteraction