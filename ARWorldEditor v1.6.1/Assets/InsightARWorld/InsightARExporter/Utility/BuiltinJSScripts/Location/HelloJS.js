function HelloJS(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
    Insight.Debug.Log("Hello JS:New"+gameObject.name);
}
//Write prototype function here
HelloJS.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        Insight.Debug.Log("Hello:Start");
    },
    // Update is called once per frame
    Awake: function()
    {
        Insight.Debug.Log("Hello JS :Awake 123 456");
    }
});

//Return the script module
HelloJS