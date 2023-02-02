function CalculateArrowPosition(gameObject)
{
    this.gameObject = gameObject;
    this.transform = gameObject.transform;
}
//Write prototype function here
CalculateArrowPosition.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    // Start: function(){

    // },
    // // Update is called once per frame
    // Update: function()
    // {

    // },
    // calculate rotation
    CalculateRotation: function(camera,stuffPosition)
    {
        var angle = this.CalculateAngle(camera,stuffPosition);
        return Insight.Quaternion.AngleAxis(angle,new Insight.Vector3(0, 0, 1));
    },
    // CalculatePosition
    CalculatePosition: function(camera,trans,stuffPosition,screenHeight,screenWidth,marginH,marginV)
    {
        var angle = this.CalculateAngle(camera,stuffPosition);
        var cosA = Math.cos(Fw_Math_Rad(1) * angle);
        var sinA = Math.sin(Fw_Math_Rad(1) * angle);
        
        var posZ = trans.gameObject.getComponent("RectTransform").anchoredPosition3D.z;
        return new Insight.Vector3((screenWidth - marginH)/2.0*cosA,(screenHeight - marginV)/2.0 * sinA,posZ);
    },
    // calculate angle
    CalculateAngle: function(camera,stuffPosition)
    {
        var camTrans = camera.transform;
        var stuffOffset = new Insight.Vector3();
        stuffOffset.copy(stuffPosition).sub(camTrans.position);

        var camRight = camTrans.right;
        var camUp = camTrans.up;
        var camForward = camTrans.forward;
        
        var cosRight = camRight.dot(stuffOffset)/stuffOffset.magnitude;//Insight.Vector3.Dot(camRight,stuffOffset)/stuffOffset.magnitude;
        var cosUp = camUp.dot(stuffOffset)/stuffOffset.magnitude;//Insight.Vector3.Dot(camUp,stuffOffset)/stuffOffset.magnitude;
        
        var length = Math.sqrt(cosRight * cosRight + cosUp * cosUp);
        
        if(length < 0.001)
        {
            return 0.0;
        }
        
        var cosA = cosRight/length;
        var sinA = cosUp /length;
        
        var angle = 0;
        
        if(sinA >=0 && cosA >= 0)
        {
            angle = Fw_Math_Deg(1)*Math.asin(sinA);
        }
        else if(sinA>=0 && cosA<0)
        {
            angle = Fw_Math_Deg(1) * Math.acos(cosA);
        }
        else if(sinA<0 && cosA <0)
        {
            angle = 180 - Fw_Math_Deg(1) * Math.asin(sinA);
        }
        else
        {
            angle = Fw_Math_Deg(1) * Math.asin(sinA);
        }
        return angle;
    }
});

//Return the script module
CalculateArrowPosition