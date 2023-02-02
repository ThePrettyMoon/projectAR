var EventType={
    EVENT_TYPE_POI_ENTER : 0,
    EVENT_TYPE_POI_UPDATE : 1,
    EVENT_TYPE_POI_EXIT : 2,
    EVENT_TYPE_POI_LIST : 3,
    EVENT_TYPE_TRACK_SUCCESS : 4,
    EVENT_TYPE_TRACK_FAIL : 5,
    EVENT_TYPE_TRACK_UP : 6,
    EVENT_TYPE_TRACK_DOWN : 7,
    EVENT_TYPE_ENTER_POI : 8,
    EVENT_TYPE_EXIT_POI : 9,
    EVENT_TYPE_CLICK_ENTER_POI : 10, // 选择开始游戏
    EVENT_TYPE_CLICK_POI_CANCEL : 11, //选择取消游戏
    EVENT_TYPE_CLICK_POI_CONFIRM : 12 //选择进入游戏
 };

 var g_funcList = [];

function EventManager(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
EventManager.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    },
    //添加监听
    AddListener: function(eventType,func)
    {
       // Insight.Debug.Log("js call add listener " + eventType);
        if(eventType == undefined || func == undefined)
        {
            return;
        }
        if(g_funcList[eventType]==undefined)
        {
            var a= [];
            a.push(func);
            // table.insert(a,func);
            g_funcList[eventType]=a;
        }
        else
        {
            g_funcList[eventType].push(func);
            //table.insert(g_funcList[eventType],func);
        }
    },
    //移除事件
    RemoveListener: function(eventType,func)
    {
       // Insight.Debug.Log("js call remove listener " + eventType);
        if(eventType==undefined || func==undefined)
        {
            Insight.Debug.Log("eventType is nil or func is nil");
            return;
        }
        var a=g_funcList[eventType];
        if(a!=undefined)
        {
            for(var k = 0; k < a.length; k ++)
            {
                if(a[k] == func)
                {
                    a[k] = undefined;
                }
            }
            // for k,v in pairs(a) do
            //     if(v==func)then
            //         a[k]=nil
            //     end
            // end
        }
    },
    //派发事件 wzy: 有待验证
    SendEvent: function (eventType) {
        var args = [].slice.call(arguments);
        // Insight.Debug.Log("js call send eventType0 " + eventType + " " + args.length);
        if (eventType != undefined) {
            var a = g_funcList[eventType];
            if (a != undefined) {
                for (var k = 0; k < a.length; k++) {
                    var v = a[k];
                    if (args.length == 1) {
                        v();
                    }
                    else if (args.length == 2) {
                        v(args[1]);
                    }
                    else if (args.length == 3) {
                        v(args[1], args[2]);
                    }
                    else if (args.length == 4) {
                        v(args[1], args[2], args[3]);
                    } else if (args.length == 5) {
                        v(args[1], args[2], args[3], args[4]);
                    }
                    else if (args.length == 6) {
                        v(args[1], args[2], args[3], args[4], args[5]);
                    }else if(args.length == 7)
                    {
                        v(args[1], args[2], args[3], args[4], args[5], args[6]);
                    } 
                    else {
                        Insight.Debug.Log("ERROR, can not deal more than 7 args");
                    }
                    //v(info);

                }
                // for k,v in pairs(a) do
                //     v(...)
                // end
            }
        }
    },
    //移除所有的listener
    RemoveAllListener: function()
    {
        g_funcList = [];
    }
});


//Return the script module
EventManager