//雷达原理就是在相机和追踪的区域上加标记，把标记的层级设为Map，而雷达相机设为只能渲染Map层级
//将标记的位置和旋转设置好
//设置箭头标记，并控制箭头的出现
//
function MapManager(gameObject)
{
    this.gameObject = gameObject;
}

MapManager.prototype = Object.assign(Object.create(Object.prototype), {

    Start: function(){
        this.canvasTran=Insight.GameObject.Find("HintController").transform;
        this.mainCameraObject=Insight.GameObject.Find("Main Camera");
        this.mainCameraPoint=Insight.GameObject.Find("MainCameraMapPoint");
        this.tmpPos=new Insight.Vector3(0,0,0);
        this.mapCameraObject=Insight.GameObject.Find("MapCamera");
        this.mapCamera=this.mapCameraObject.getComponent("Camera");
        this.gameObjectPoint=Insight.GameObject.Find("World/Point");
        this.arrow=Insight.GameObject.Find("MapArrow");
    },

    Update: function()
    { 
        
        this.tmpPos=new Insight.Vector3(0,this.mainCameraObject.transform.rotation.eulerAngles.y,0);
        this.mainCameraPoint.transform.rotation=Insight.Quaternion.Euler(this.tmpPos);
        this.mapCameraObject.transform.position=new Insight.Vector3(this.mainCameraObject.transform.position.x,10,this.mainCameraObject.transform.position.z);
        this.gameObjectPoint.transform.position=new Insight.Vector3(this.gameObjectPoint.transform.position.x,0,this.gameObjectPoint.transform.position.z);
        this.mainCameraPoint.transform.position=new Insight.Vector3(this.mainCameraObject.transform.position.x,0,this.mainCameraObject.transform.position.z);
        this.arrow.transform.position=this.mainCameraPoint.transform.position;
        if(Insight.Vector3.Distance(this.gameObjectPoint.transform.position,this.mainCameraPoint.transform.position)>5.5)
        {
            this.arrow.transform.lookAt(this.gameObjectPoint.transform.position);
            this.arrow.setActive(true);
        }
        else
        {
            this.arrow.setActive(false);
        }
    }
});

MapManager