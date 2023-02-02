function EventBackButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
EventBackButton.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
    },
    // Update is called once per frame
    Update: function()
    {
    },
    OnPointerUp: function()
    {
        this.cid = "0";
        this.sid = "0";
        if (g_currentSelectItem != undefined) {
            if (g_currentSelectItem.eventId != undefined) {
                this.cid = g_currentSelectItem.eventId;
            }

            if (g_currentSelectItem.snapshotId != undefined) {
                this.sid = g_currentSelectItem.snapshotId;
            }
        }
        Fw_Event_UnloadPoiData(this.cid, this.sid, "2");
    }
});

//Return the script module
EventBackButton