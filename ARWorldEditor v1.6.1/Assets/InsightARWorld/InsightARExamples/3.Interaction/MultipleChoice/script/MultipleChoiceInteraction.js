function MultipleChoiceInteraction(gameObject) {
    this.className = "MultipleChoiceInteraction";
    this.gameObject = gameObject;
}
//Write prototype function here
MultipleChoiceInteraction.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {

        //单选=false  多选=true
        this.isMultiChoose = false;

        console.log(Insight.Application.RuntimePlatform);


        this.canvasTrans = Insight.GameObject.Find("Canvas").transform;

        //链表的使用
        this.buttonList =
            [
                this.canvasTrans.find("answer/an1/Background").gameObject.getComponent("Button"),
                this.canvasTrans.find("answer/an2/Background").gameObject.getComponent("Button"),
                this.canvasTrans.find("answer/an3/Background").gameObject.getComponent("Button"),
                this.canvasTrans.find("answer/an4/Background").gameObject.getComponent("Button")
            ]
        this.buttonImgList =
            [
                this.buttonList[0].gameObject.getComponent("Image"),
                this.buttonList[1].gameObject.getComponent("Image"),
                this.buttonList[2].gameObject.getComponent("Image"),
                this.buttonList[3].gameObject.getComponent("Image"),
            ]
        this.okButton = this.canvasTrans.find("ok/Background").gameObject.getComponent("Button"),
            this.resetButton = this.canvasTrans.find("reset/Background").gameObject.getComponent("Button"),

            Insight.Button.AddClick(this.buttonList[0], this, this.ChooseA);
        Insight.Button.AddClick(this.buttonList[1], this, this.ChooseB);
        Insight.Button.AddClick(this.buttonList[2], this, this.ChooseC);
        Insight.Button.AddClick(this.buttonList[3], this, this.ChooseD);
        Insight.Button.AddClick(this.okButton, this, this.OK);
        Insight.Button.AddClick(this.resetButton, this, this.Reset);

        this.cube = Insight.GameObject.Find("Cube").transform;


        this.chooseList = [];
    },
    Update: function () {
        console.log(this.cube);
        var v = new Insight.Vector4(this.cube.rotation.x, this.cube.rotation.y, this.cube.rotation.z, this.cube.rotation.w);
        console.log();
        var rotation = Insight.Quaternion.Euler(0, 10, 0);
        var v2 = new Insight.Vector4(rotation.x, rotation.y, rotation.z, rotation.w);
        var v3 = v.multiply(v2);
        console.log(v3.x + '' + v3.y+" "+ v3.z+" "+ v3.w);
        this.cube.rotation = new Insight.Quaternion(v3.x,v3.y,v3.z,v3.w);
    },
    ChooseA: function () {
        console.log("选择A");
        this.CheckMultiChoose(0);
    },
    ChooseB: function () {
        console.log("选择B");
        this.CheckMultiChoose(1);
    },
    ChooseC: function () {
        console.log("选择C");
        this.CheckMultiChoose(2);
    },
    ChooseD: function () {
        console.log("选择D");
        this.CheckMultiChoose(3);
    },

    CheckMultiChoose: function (index) {
        //如果是单选
        if (!this.isMultiChoose) {
            //单选成功
            console.log("单选成功：" + index);
        }
        else {

            //indexOf：https://www.w3school.com.cn/jsref/jsref_indexof_array.asp
            //indexOf() 方法在数组中搜索指定项目，并返回其位置。
            //搜索将从指定位置开始，如果未指定开始位置，则从头开始，并在数组末尾结束搜索。
            //如果未找到该项目，则 indexOf() 返回 - 1。

            //这里只有没选择的才加入，已选择的就不再选择了
            if (this.chooseList.indexOf(index) == -1)
            {
                console.log("多选成功：" + index);
                //加入表中
                this.chooseList.push(index);
                //高亮按钮
                Insight.DOTween.DOFade(this.buttonImgList[index], 0.4, 0.3);
            }
        }
    },
    OK: function () {
        for (var i = 0; i < this.chooseList.length; i++) {
            console.log("选择了："+this.chooseList[i])
        }
    },
    Reset: function () {
        console.log("Reset ~~");
        //清空已选择的链表
        this.chooseList = [];

        //重置所有高亮效果
        for (var i = 0; i < this.buttonImgList.length; i++) {
            Insight.DOTween.DOFade(this.buttonImgList[i], 0.1, 0);
        }
    }
});

// Debugger Methods Begin 
var SafeFunctor = SafeFunctor || function (func) { return function () { try { return func.apply(this, arguments) } catch (e) { var msg = "ClassName: " + this.className + ", LineNo: " + (e.lineNumber || e.line) + ", Msg: " + e.message + "\r\nStackTrace: " + (e.stackTrace || e.stack); console.error(msg) } } };
var SafeWrapper = SafeWrapper || function (classObject) { var o = classObject.prototype; for (var key in o) { var func = o[key]; if (typeof (func) === 'function') { o[key] = SafeFunctor(func) } } };
SafeWrapper(MultipleChoiceInteraction);
// Debugger Methods End
//Return the script module
MultipleChoiceInteraction