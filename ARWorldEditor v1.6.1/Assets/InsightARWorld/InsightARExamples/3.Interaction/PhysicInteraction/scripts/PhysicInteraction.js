function PhysicInteraction(gameObject)
{
    this.gameObject = gameObject;
}
//
PhysicInteraction.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {
        console.log("Start");
        //射线碰撞检测案例 
        //参考文档 https://near.yuque.com/oixgp1/isv/fo6nsp
        /**
         * 1.需要一个相机，和一个带有collider 碰撞组件的cube
         * 2.在scene中点击某个有碰撞体的cube,可以在unity console中看到输出
         * */
        this.cube_ray = Insight.GameObject.Find("cube_ray");
        this.camera = Insight.GameObject.Find("Main Camera").getComponent("Camera");

        //Trigger碰撞案例 
        /**
          * 1.需要一个有rigidbody组件以及collider 碰撞体组件的cube
          * 2.一个带有collider 碰撞组件的cube
          * 3.至少一个碰撞组件上勾选is trigger
          * 4.有一个物体有js脚本，并且其中有OnTriggerEnter()方法
          * */
        this.cube_trigger1 = Insight.GameObject.Find("cube3");
        this.cube_trigger2 = Insight.GameObject.Find("cube4");
        this.InitTriggerTest();
    },
    // Update is called once per frame
    Update: function()
    {
        this.TouchInteraction();
    },
    //****************************Example:判断射线穿过碰撞体
    TouchInteraction: function () {

        if (Insight.Input.GetMouseButtonDown(0)) {
            var rayCastHit = new Insight.RaycastHit();
            var ray = this.camera.screenPointToRay(Insight.Input.mousePosition)
            var isHitted = Insight.Physics.Raycast(ray.origin, ray.direction, rayCastHit, 1000.0, 4294967295);
            if (isHitted) {
                Insight.Debug.Log( " test hitted : " + isHitted.toString()+ " `` "+  rayCastHit.collider.name + "\t" +rayCastHit.point); 
            }
        }
    },

    //****************************Example:物体碰撞测试
    InitTriggerTest: function () {
        //让物体1移动，并进行碰撞
        Insight.DOTween.DOLocalMove(this.cube_trigger1.transform, Insight.Vector3.New(3.32, 0.0, -4.01), 1.0).setLoops(-1, Insight.LoopType.Restart).setEase(Insight.Ease.Line);
    },
    //碰撞事件回调
    OnTriggerEnter: function (collider) {

       //注意使用Debug方式输出节点时会返回空，推荐使用console的方式
        Insight.Debug.Log(collider);
        console.log(collider);
    },

});

//Return the script module
PhysicInteraction