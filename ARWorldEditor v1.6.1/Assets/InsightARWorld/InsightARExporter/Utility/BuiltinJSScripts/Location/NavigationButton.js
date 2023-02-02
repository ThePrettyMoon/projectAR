function NavigationButton(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
NavigationButton.prototype = Object.assign(Object.create(Object.prototype), {
    Awake: function(){

    },
    // Start is called before the first frame update
    Start: function(){

    },
    // Update is called once per frame
    Update: function()
    {

    }
});

//Return the script module
NavigationButton