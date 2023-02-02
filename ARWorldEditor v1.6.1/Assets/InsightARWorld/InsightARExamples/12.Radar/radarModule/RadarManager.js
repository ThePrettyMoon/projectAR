var g_radarManger;
function RadarManager(gameObject)
{
    this.gameObject = gameObject;
}
/**
 * 使用指南：
 * 1.首先在一个空的主场景中将radarModule中的radarPanel拖入HintController作为其子节点。拖入RadarCamera到场景中（无父节点）。拖入Zone到场景中
 * 2.Hierarchy中，找到Zone节点下的mini节点，并设置Layer为：radar
 * 3.Hierarchy中，RadarCamera节点下的camera设置culling mask为radar
 * 4.Hierarchy中，找到Manager节点并添加ScriptRunner,并将radarModule中RadarManager.js脚本放入。
 * 5.修改RadarManager中Start:
 *    g_radarManger = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExamples/12.Radar/radarModule/RadarManager.js",0);
 * 将路径修改为当前脚本所在路径
 *    g_radarManger = this.gameObject.getComponent("Assets/InsightARWorld/······/RadarManager.js",0);
 * 表现：
 * 通过移动、旋转MainCamera节点，查看雷达效果
 */


//Write prototype function here
RadarManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        g_radarManger = this.gameObject.getComponent("Assets/InsightARWorld/InsightARExamples/12.Radar/radarModule/RadarManager.js",0);
        g_radarManger.Init();
    },
    Init:function(){
        this.trackList= [];
        this.trackStatu = false;
        this.acDir = new Insight.Vector2(0,0);
        this.cameraForward = new Insight.Vector2(0,0);

        //UI和使用节点
        this.radarPanel =Insight.GameObject.Find("HintController").transform.find("radarPanel");
        this.radarCameraTrans = Insight.GameObject.Find("RadarCamera").transform;
        this.target =  this.radarPanel.find("target");

        //测试代码
        //加入并打开
        this.AddTrackNode(Insight.GameObject.Find("World").transform.find("Zone"),this.target.gameObject.getComponent("RectTransform"));
        this.OpenRadar();
    },
    //增加雷达追踪
    AddTrackNode:function(trans,transUI)
    {
        for (var index = 0; index < this.trackList.length; index++) {
            const element = this.trackList[index];
            //有重复
            if (trans == element[0]) {
                return;
            }
        }
        //无重复
        var array = [trans,transUI]
        this.trackList.push(array);
    },
    //开启雷达监测
    OpenRadar()
    {
        this.trackStatu = true;
        this.radarPanel.gameObject.setActive(true)
    },
    //关闭雷达监测
    CloseRadar()
    {
        this.trackStatu = false;
        this.radarPanel.gameObject.setActive(false)
    },
    // Update is called once per frame
    Update: function()
    {
        if (this.trackStatu) {
            var cameraPosition = g_ResManager.mainCameraTrans.position;
            var cameraForward = g_ResManager.mainCameraTrans.forward;
            //雷达相机跟随
            this.radarCameraTrans.position = cameraPosition.clone().setY(cameraPosition.y+10);
            this.radarCameraTrans.forward = cameraForward.clone().setY(0)

            for (var index = 0; index < this.trackList.length; index++) {
                const elementList = this.trackList[index];
                //雷达图节点 transform
                var transCalcu = elementList[0];
                //雷达图节点 UI recttransform
                var transUI  = elementList[1];
                //计算距离，超过 5m 取消显示UI
                var distance = Insight.Vector3.Distance(transCalcu.transform.position,cameraPosition);
                var isShow = distance >= 5;
                transUI.gameObject.setActive(isShow)
                //在距离外显示指向箭头
                if (isShow) {
                    var dir = transCalcu.transform.position.sub(cameraPosition);
                    this.acDir.set(dir.x,dir.z)
                    this.cameraForward.set(cameraForward.x,cameraForward.z);

                    var angle = Insight.Vector2.SignedAngle(this.cameraForward, this.acDir);
                    var signed = angle / 180 * Math.PI;
                    var x = -Math.sin(signed)*200;
                    var y = Math.cos(signed)*200;
    
                    this.acDir.set(x,y);
                    transUI.anchoredPosition =  this.acDir;
                    transUI.transform.rotation = Insight.Quaternion.AngleAxis(signed,0,0,1);
                }
            }
        }
    }
});

//Return the script module
RadarManager