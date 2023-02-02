function FingerInteraction(gameObject)
{
    this.gameObject = gameObject;
}
/**
 * 手指交互操作，支持多指操作
 * 需要在实机上查看效果，编辑器中不支持查看
 */
FingerInteraction.prototype = Object.assign(Object.create(Object.prototype),{
    // Start is called before the first frame update
    Start: function () {
        this.camera = Insight.GameObject.Find("Main Camera");
        this.model = Insight.GameObject.Find("model");
        this.rotateAngle= 0;
        this.rotateSpeed = 0;
    },
    // Update is called once per frame
    Update: function()
    {
        //单指操作
        this.OneFingerMove();
        //双指操作
        this.TwoFingerMove();
        //旋转
        this.RotationMethod();
    },

    /////////////////////////////////实机效果
    /**
    * 单指触摸滑动，模型旋转
    * */
    OneFingerMove: function () {

        if (Insight.Input.touchCount != 1) return;

        var input1 = Insight.Input.GetTouch(0);

        if (input1.phase == "Began") {
            this.oneFingerX = Insight.Input.mousePosition.x;
        }
        
        if (input1.phase == "Moved" || input1.phase == "Ended") {
            var current = Insight.Input.mousePosition.x
            this.rotateSpeed = (this.oneFingerX - current);

            //限制最大旋转速度
            var maxSpeed = 30;
            if(this.rotateSpeed > maxSpeed) this.rotateSpeed = maxSpeed;
            else if(this.rotateSpeed < -maxSpeed) this.rotateSpeed = -maxSpeed;

            this.oneFingerX = current;
        }
    },

    /**
    * 双指触摸滑动，模型旋转
    * */
    TwoFingerMove: function () {

        if(Insight.Input.touchCount != 2) return;

        var input1 = Insight.Input.GetTouch(0);
        var input2 = Insight.Input.GetTouch(1);

        if(input1.phase == "Began"||input2.phase == "Began")
        {
            //记录第二根手指刚触摸时两指触碰点距离
            this.twoFingerDistance = input1.position.distanceTo(input2.position)
        }

        if (input1.phase == "Moved"&&input2.phase == "Moved")
        {  
            //两手指触碰点之间的距离
            var _twoFingerDistance = input1.position.distanceTo(input2.position)
            //上一帧两指触碰点之间的距离与当前帧对比，作为模型放缩的速度因子
            var speed = (_twoFingerDistance - this.twoFingerDistance)/10;
            //(双指扩张  || 双指收缩)
            if(speed >0 || speed < 0)
            {
                var targetScale = this.model.transform.localScale.addScaledVector(Insight.Vector3.one,Insight.Time.deltaTime*speed);
                var maxScaleX = 2;
                var minScaleX = 0.3;
                
                //限制放大缩小的范围
                if(targetScale.x >= maxScaleX||targetScale.x <= minScaleX) return;

                //设置大小
                this.model.transform.localScale = targetScale;
            }
            this.twoFingerDistance = _twoFingerDistance;
        }
    },

    /**
     * 平滑插值旋转方法
     * Fw_Math_Lerp来源于工具脚本 Fw_Utility.js
     * Transform:https://near.yuque.com/oixgp1/isv/zm7cuq
     * Vector3:https://near.yuque.com/oixgp1/isv/keiyqi
     * Quaternion:https://near.yuque.com/oixgp1/isv/tv3hkd
     */
    RotationMethod: function () {
        this.rotateAngle = Fw_Math_Lerp(this.rotateAngle, this.rotateAngle + this.rotateSpeed, 0.007);
        this.rotateSpeed = Fw_Math_Lerp(this.rotateSpeed, 0, 0.01);
        this.model.transform.localRotation = Insight.Quaternion.AngleAxis(this.rotateAngle, Insight.Vector3.New(0, 1, 0));
    }
});

//Return the script module
FingerInteraction