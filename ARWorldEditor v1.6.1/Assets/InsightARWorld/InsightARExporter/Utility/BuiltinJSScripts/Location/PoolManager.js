function PoolManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
//缓存池
PoolManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    },
    //初始化
    Init: function()
    {
        this.poolList = [];
        // 设置最大缓存物体个数
        this.MAXPOOLCOUNT = 50;
        
        //缓存的根物体
        this.rootTrans = Insight.GameObject.Find("rootTrans").transform;
    },
    //从spwan 拿物体
    Spawn: function(poolType)
    {
        if(this.poolList[poolType] == undefined )
        {
            Insight.Debug.Log("spawn object does not exits ");
            return undefined;
        }
        
        var len = this.poolList[poolType].length;//Fw_Table_GetLength(self.poolList[poolType]);
        if(len ==0)
        {
            Insight.Debug.Log("spawn type"+poolType+" is empty");
            return;
        }
        
        var prefabTrans = this.poolList[poolType][1];
        //Insight.Debug.Log("pool manager spawn "+prefabTrans.name);
        // table移除最后一个物体 wzy: 这里是删除了table里面最前面的元素？那对呀array就是删除头部元素了
        // table.remove(self.poolList[poolType],1);
        this.poolList[poolType].shift();
        return prefabTrans;
    },
    // 回收物体
    DeSpawn: function(poolType,prefabTrans)
    {
        // 先重置prefab参数
        if(prefabTrans == undefined)
        {
            Insight.Debug.Log("js call poolmanager despawn transform is nil");
            return ;
        }
        
        if(this.poolList[poolType] != undefined)
        {
            //if(#self.poolList[poolType] >self.MAXPOOLCOUNT) then
            if(this.poolList[poolType].length > this.MAXPOOLCOUNT)
            {
                Insight.Debug.Log("js call exceede max despawn object count" );
                return;
            }
        }
        if(prefabTrans!=undefined && prefabTrans.gameObject!=undefined)
        {
            prefabTrans.gameObject.setActive(false);
        }
        
        //不设置父物体
        //prefabTrans.parent = self.rootTrans;
        
        if(this.poolList[poolType] == undefined || this.poolList[poolType].length == 0)
        {
            this.poolList[poolType] = [prefabTrans];
        }
        else
        {
            this.poolList[poolType].push(prefabTrans);
            //table.insert(self.poolList[poolType],prefabTrans);
        }
        
    },
    Close: function()
    {
        //需要考虑是否删除缓存池
        this.poolList = [];
    }
});

//Return the script module
PoolManager