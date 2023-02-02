function GameMapData(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
GameMapData.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    //初始化
    Init:function()
    {
        //self.mapPoiList = {};
        this.mapPoiList = [];
        this.curMapPoint = undefined;
        this.curMapInfo = undefined;
        this.curNavPoiInfo = undefined;
    },
    //返回poilist
    GetPoiList:function()
    {
        return this.mapPoiList;
    },
    //返回当前所在点
    GetCurrentMapPoint:function()
    {
        return this.curMapPoint;
    },
    //返回map信息
    GetCurrentMapInfo:function()
    {
        return this.curMapInfo;
    },
    //设置poilist
    SetPoiList:function(poiList)
    {
        this.mapPoiList = poiList;
    },
    //设置当前mappoint
    SetCurrentMapPoint:function(mapPoint)
    {
        this.curMapPoint = mapPoint;
    },
    //设置map info
    SetCurrentMapInfo:function(mapInfo)
    {
        this.curMapInfo = mapInfo;
    },
    //设置当前导航的poi点
    SetNavPoiInfo:function(id)
    {
        var len = this.mapPoiList.length;//Fw_Table_GetLength(self.mapPoiList);

        print("js call set nav poi info lenght " + len+" "+ id);
        if(len == 0)
        {
            return false;
        }	
        
        for(var i=0; i < len; i ++)
        {
            var poiInfo = this.mapPoiList[i];
            if(poiInfo != undefined)
            {
                var property = poiInfo.properties;
                if(property.id == id)
                {
                    this.curNavPoiInfo = poiInfo;
                    return true;
                }
            }
        }
        return false;
    },
    GetNavPoiInfo: function()
    {
	    return this.curNavPoiInfo;
    },
    SetIsNavigationState:function(isNavigationState)
    {
        this.isNavigationState = isNavigationState;
    },
    GetIsNavigationState:function()
    {
        return this.isNavigationState;
    },
    Close: function()
    {
        this.mapPoiList = [];
        this.curMapPoint = undefined;
        this.curMapInfo = undefined;
        this.curNavPoiInfo = [];
        this.isNavigationState = true;
    },
    // Update is called once per frame
    Update: function()
    {

    }
});

//Return the script module
GameMapData