var g_getPoiList = false;
var g_setMapboxOn = false;
function MapLocationController(gameObject) {
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
MapLocationController.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function () {

    },
    // Update is called once per frame
    Update: function () {

    },
    OnDisable: function () {

    },
    OnDestroy: function () {

    },
    // init
    Init: function () {
        this.isMapEnabled = true;
        this.QueryMapLocation();
    },
    // update
    UpdateMapInfo: function () {
        if (!this.isMapEnabled) {
            return;
        }

        if (!g_getPoiList) {
            this.QueryPoiList();
        }
    },
    Close: function () {
        this.isMapEnabled = false;
    },

    //query mapPoint
    QueryMapLocation: function () {
        Insight.Navigation.OnLocationChanged(this, this.onLocationChanged);
    },

    //query mappoint callback
    onLocationChanged: function (mapPointStr) {
        if (mapPointStr == undefined || mapPointStr == "") {
            Insight.Debug.Log("query map point is nil");
            return;
        }

        //Insight.Debug.Log("js call query mappoint " + mapPointStr);
        var mapPoint = JSON.parse(mapPointStr);
        g_MapData.SetCurrentMapPoint(mapPoint);
    },
    //query poilist
    QueryPoiList: function () {
        var curMappoint = g_MapData.GetCurrentMapPoint();

        if (curMappoint == undefined || curMappoint.geoCoordsPosition == undefined) {
            return;
        }

        var poiInfoString = Insight.Navigation.QueryMapPoiList();

        if (poiInfoString == undefined || poiInfoString == "") {
            Insight.Debug.Log("js call query poiInfoList length fail");
            return;
        }

        Insight.Debug.Log("js call query poilist " + poiInfoString);
        var mapPoiInfos = JSON.parse(poiInfoString);

        if (mapPoiInfos == undefined) {
            return;
        }

        g_MapData.SetPoiList(mapPoiInfos.mapPoILists);
        g_getPoiList = true;

        g_EventManager.SendEvent(EventType.EVENT_TYPE_POI_LIST);
    },
    Close: function () {
        this.isMapEnabled = false;
    }
});

//Return the script module
MapLocationController