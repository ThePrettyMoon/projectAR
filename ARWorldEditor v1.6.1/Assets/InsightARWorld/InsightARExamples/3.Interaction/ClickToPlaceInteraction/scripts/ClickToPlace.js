function ClickToPlace(gameObject) {
    this.className = "ClickToPlace";
    this.gameObject = gameObject;
}
//Write prototype function here
ClickToPlace.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {
        //节点
        this.cameraTrans = Insight.GameObject.Find("Main Camera").transform;
        this.camera = this.cameraTrans.gameObject.getComponent("Camera");

        this.worldTrans = Insight.GameObject.Find("World").transform;
        this.placeObject = this.worldTrans.find("placeObject");
    },
    // Update is called once per frame
    Update: function () {
        //点击交互
        if (Insight.Input.GetMouseButtonDown(0)) {
            var ray = this.camera.screenPointToRay(Insight.Input.mousePosition);
            var raycasthit = new Insight.RaycastHit();
            var isHitted = Insight.Physics.Raycast(ray.origin, ray.direction, raycasthit, 50, 4294967295);
            if (isHitted) {
                this.placeObject.gameObject.setActive(true)
                //do somthing
                this.target = raycasthit.transform.gameObject;
                console.log(this.target.name);
                var hitPoint = raycasthit.point;
                console.log("碰撞点坐标" + hitPoint.x + " " + hitPoint.y + " " + hitPoint.z)
                this.placeObject.position = hitPoint;
                //向上移动1个单位后的位置 可调整
                var pos = this.placeObject.position.clone().add(new Insight.Vector3(0, 1, 0))

                //悬浮动画
                //上下浮动动画
                //注意 初始的位置 可以通过创建一个父节点，把当前节点拽入父节点下，改变父节点名字为placeObject后，进行调整
                 //5秒时间 ，以YOYO方式循环，动画插值函数使用=4的方式 ，更多动画插值函数：https://near.yuque.com/oixgp1/isv/zxdgrf
                Insight.DOTween.DOMove(this.placeObject, pos, 5).setLoops(-1, Insight.LoopType.Yoyo).setEase(4);//Sin函数方式
            }
        }
    }
});

// Debugger Methods Begin
var SafeFunctor = SafeFunctor || function (func) { return function () { try { return func.apply(this, arguments) } catch (e) { var msg = "ClassName: " + this.className + ", LineNo: " + (e.lineNumber || e.line) + ", Msg: " + e.message + "\r\nStackTrace: " + (e.stackTrace || e.stack); console.error(msg) } } };
var SafeWrapper = SafeWrapper || function (classObject) { var o = classObject.prototype; for (var key in o) { var func = o[key]; if (typeof (func) === 'function') { o[key] = SafeFunctor(func) } } };
SafeWrapper(ClickToPlace);
// Debugger Methods End
//Return the script module
ClickToPlace