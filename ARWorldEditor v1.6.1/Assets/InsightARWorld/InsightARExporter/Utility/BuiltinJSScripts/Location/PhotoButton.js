function PhotoButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
PhotoButton.prototype = Object.assign(Object.create(Object.prototype), {
    Awake: function(){
        Insight.Debug.Log("Hello JS:Awake");
    },
    // Start is called before the first frame update
    Start: function(){
        Insight.Debug.Log("Hello:Start");
    },
    // Update is called once per frame
    Update: function()
    {

    }
});

//Return the script module
PhotoButton