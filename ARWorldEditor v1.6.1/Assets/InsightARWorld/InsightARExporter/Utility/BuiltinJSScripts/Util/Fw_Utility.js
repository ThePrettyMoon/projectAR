// ------------------------------
// Copyright (c) NetEase Insight
// All rights reserved.

// Framework(Fw_): JS层的拓展框架
// 提供常用的变量和JS方法。
// 涵盖算法状态、消息交互、中文字符串处理、安全的表操作、AR常用数学计算

// AR Script 标准版本

// ------------------------------
 

//--------------------------------------------------
// Constructor

function InsightExtension(gameObject)
{
    this.transform = gameObject.transform;
    this.game_object = gameObject;
}
//Write prototype function here
InsightExtension.prototype = Object.assign(Object.create(Object.prototype), {
    // Start is called before the first frame update
    Start: function(){
        
    },
    //--------------------------------------------------
    // Life Cycle

    // Init SomeThing For Global Usage
    Awake: function(){
        // Init The Print Entity
        Fw_PrintToTextEntity = Insight.GameObject.Find( "Fw_PrintToTextEntity"  );
    },
    // Update is called once per frame
    Update: function()
    {
        
    }
});

// Life Cycle
//--------------------------------------------------


//--------------------------------------------------
// Global Variables


// AR Checking Status
// 不允许修改
var Fw_ARState_Uninitialized        = 0;
var Fw_ARState_Initing              = 1;
var Fw_ARState_Init_OK              = 2;
var Fw_ARState_Init_Fail            = 3;
var Fw_ARState_Detecting            = 4;
var Fw_ARState_Detect_OK            = 5;
var Fw_ARState_Detect_Fail          = 6;
var Fw_ARState_Tracking             = 7;
var Fw_ARState_Track_Limited        = 8;
var Fw_ARState_Track_Lost           = 9;
var Fw_ARState_Track_Fail           = 10;
var Fw_ARState_Track_Stop           = 11;


// AR Tracking Tpye 使用哪种算法在tracking
// Set:不允许修改
var Fw_ARTrackingTpye_IMU  = 10;
var Fw_ARTrackingTpye_VO   = 11;
var Fw_ARTrackingTpye_VIO  = 12;



// Hint for Tracking Limited reason enum
// Set: 不允许修改

var Fw_TrackingLimitedReason_ReasonNone = 0.0;
var Fw_TrackingLimitedReason_LowLight = 1.0;
var Fw_TrackingLimitedReason_ExcessiveMotion = 2.0;
var Fw_TrackingLimitedReason_InsufficientFeatures = 3.0;


//--------------------------------------------------------------------------
// Global Functions

//-----------------------------------------------
// AR Events

// Functions for Interact With APP
// methodName : without ()
// params : if no params, then set nil
// result : 1 (Succeed) , 0 (Failed)
// 告知APP该RunScript已经执行
function Fw_Event_SendRunScriptCallback( methodName, params, result )
{	
    if(params)
    {
        Insight.Event.Happen( 1 , 1 , 500 , methodName + "(\"" + params + "\")" + "___" + result );
    }
    else
    {
		Insight.Event.Happen( 1 , 1 , 500 , methodName  + "()" + "___" + result );
	}
}
function Fw_Event_SendRunScript( methodName, params )
{	
    if(params)
    {
		Insight.Event.Happen( 1 , 1 , 100 , methodName + "___" + params );
    }else{
		Insight.Event.Happen( 1 , 1 , 100 , methodName);
	}

	print("[test event] send message : " + methodName ); //test
}
function Fw_Event_CloseARScene()
{	
	Insight.Event.Happen( 1 , 1 , 101 , "");
}
//make toast
// msg 显示的内容
// during 显示时间
function Fw_Event_MakeToast(msg,during)
{
	var paramTable = {command:"1",time : tostring(during),progress : "0",text : msg };
	var jsonStr = JSON.stringify(paramTable);
	Insight.Event.Happen( 1 , 1 , 10005 , jsonStr);
}
//load poi product
// local paramTable ={"eventid " = "3707"};
//  未下载，启动下载，下载完成，进入poi内容
function Fw_Event_LoadPoiData(contentid,snapshotid,aType)
{
	if (snapshotid == undefined) {
		snapshotid = "0";
	}

	var paramTable = { cid: contentid, sid: snapshotid, activetype: aType };
	//Insight.Debug.Log("js call  " + tostring(id) + " " + tostring(aType));
	var jsonStr = JSON.stringify(paramTable);//g_JSON:encode(paramTable);
	Insight.Event.Happen( 1 , 1 , 10008 , jsonStr);
}
//卸载 poi product
// 0 是叠加状态，2 是挂起状态
// local paramTable ={"eventid " = "3707","eventtype"= "0"};
function Fw_Event_UnloadPoiData(contentid,snapshotid,etype)
{
	if (snapshotid == undefined) {
		snapshotid = "0";
	}

	var paramTable = { cid: contentid, sid: snapshotid, eventtype: etype };
	var jsonStr = JSON.stringify(paramTable);
	Insight.Event.Happen( 1 , 1 , 10009 , jsonStr);
}
//stop navigation
function Fw_Event_CloseNavigation()
{	
	Insight.Event.Happen( 1 , 1 , 10010 , "");
}
// local paramTable  = {
//          visibility = “0-隐藏 1-显示”
//};
function Fw_Event_SetMapVisibility(visible)
{
    var paramTable = {visibility:visible};
    var jsonStr = JSON.stringify(paramTable);
    
    Insight.Event.Happen( 1 , 1 , 10011 , jsonStr);
}
function Fw_Event_ReloadARProduct()
{
	Insight.Event.Happen( 1, 1, 102, "");
}

function Fw_Event_OpenURL( url, jsonStr )
{	
    if(url && jsonStr)
    {
		Insight.Event.Happen( 1 , 1 , 108 , url + "___" + jsonStr );
		print("[test event] send message : " + url + " and json = " + jsonStr ); //test

    }else{
		print("[test event] Error : OpenURL params incorrect " ); //test

	}
}
//type___title___description___url___logoImagePatch
//type : 1 text; 2 image; 3 music; 4 video; 5 url
function Fw_Event_Share(shareType, title, description, url, logoImagePath)
{
	var typeStr = ""; var titleStr = ""; var descriptionStr = ""; var urlStr  = " ";
	var logoImagePathStr = "/ShareLogo/ShareLogo.png";
	if(shareType){
		typeStr = shareType;
    }
	if(title){
		titleStr = title;
    }
	if(description) {
		descriptionStr = description;
    }
	if(url){
		urlStr = url;
    }
	if(logoImagePath){
		logoImagePathStr = logoImagePath;
    }
	Insight.Event.Happen( 1, 1, 111, typeStr +  "___" + titleStr + "___" + descriptionStr + "___" + urlStr + "___" + logoImagePathStr );
}

//-----------------------------------------------
// Rotate Model Towards Camera

function Fw_RotateGameObjectTowardsCamera( goToRotate , alsoRotateY )
{
	if(!goToRotate){
		Insight.Debug.Log( "Error: Fw_RotateGameObjectTowardsAnother : goToRotate is nil" )
		return
    }

	var camera = Insight.GameObject.Find( "Main Camera"  );
	if(!camera){
		Insight.Debug.Log( "Error: Fw_RotateGameObjectTowardsAnother : can not find a camera with name : Main Camera" )
		return
    }

	var yRotateRatio = 1.0;
	if(!alsoRotateY){
		yRotateRatio = 0.0;
    }

	var targetPos = new Insight.Vector3( camera.transform.position.x , 
										goToRotate.transform.position.y + yRotateRatio * ( camera.transform.position.y - goToRotate.transform.position.y ) ,
										camera.transform.position.z  )  ;
	goToRotate.transform.lookAt( targetPos, Insight.Vector3.up  )

}

//--------------------------------------------------
// Place Stuff to Real World With Screen point( screen center default )
// Return: true -- Place Success ; false -- Place failed

function  Fw_PlaceStuffToWorldWithScreenPoint( ref, target, screenPoint2d, OnTrackingFailed )
{

	// Get the world position
	// local viewport = Insight.Camera.Main():GetCamera():GetViewport();
	// local viewsize = Insight.Vector2.New( viewport:W() , viewport:H() );
	var viewsize = new Insight.Vector2( Insight.Screen.width , Insight.Screen.height );
	var crossScreenPos = new Insight.Vector2( viewsize.x / 2.0, viewsize.y / 2.0 );

	if(screenPoint2d){

		crossScreenPos = screenPoint2d;
    }

	// print( "[g_PlaceStuffToWorld] crossScreenPos for Raycasting " .. crossScreenPos.x .. " , " .. crossScreenPos.y ); -- crossScreePos
	// print( "[g_PlaceStuffToWorld] Tracking Status: " .. Insight.Tracking.GetStatus() ); -- test the tracking status

	var tracking_result = Insight.Tracking.Raycasting( crossScreenPos.x , crossScreenPos.y );

    var point = tracking_result.point;
    var strtmp;
    if(tracking_result.tracked) strtmp = "true"; else strtmp = "false";
	print( "[g_PlaceStuffToWorld] Tracking Result: " + strtmp
		 	+ " (" + point.x + "," + point.y + "," + point.z + ")" );

	if(tracking_result.tracked) {

		// Show Target
		// target:SetVisible( true );
		target.setActive( true  );

		// Place the target to viewport center pos in world
		// target:SetLocalTranslation( point );   --( self.cross:GetLocalTranslation());
		target.transform.localPosition = point;

		//print( "[g_PlaceStuffToWorld] tracked position : " + point.x + ", " + point.y + ", " + point.z  );

		return true;

    }else{

		if(OnTrackingFailed){

			OnTrackingFailed(ref);
        }else{
			//print(" Warning : g_PlaceStuffToWorld -- OnTrackingFailed is nil !!!" );

		}

		return false;
	}

}
// 由屏幕中心发出射线射向现实空间，获取改射线击中现实空间中识别出的环境位置。（返回的结果会考虑环境位置距离相机的最小距离和最大距离。返回的点不会超过这两个距离）
function Fw_GetPositionForPlacingModel( minDistance, maxDistance )
{
	var viewsize = new Insight.Vector2( Insight.Screen.width , Insight.Screen.height );
	var crossScreenPos = new Insight.Vector2( viewsize.x / 2.0, viewsize.y / 2.0 );

	var tracking_result = Insight.Tracking.Raycasting( crossScreenPos.x , crossScreenPos.y );
	var point = tracking_result.point;
    var camPos = Insight.GameObject.Find( "Main Camera"  ).transform.localPosition; // Insight.Camera.Main():GetLocalTranslation();
    var strtmp;
    if(tracking_result.tracked) strtmp = "true"; else strtmp = "false";
	print( "[Fw_GetPositionForPlacingModel] Tracking Result: " + strtmp
		 	+ " (" + point.x + "," + point.y + "," + point.z + ")" );	

	if(tracking_result.tracked){
        var v3tmp = new Insight.Vector3();
		var distance = (v3tmp.copy(point).sub(camPos)).length;
		// If point is not in correct region
		//Insight.Debug.Log("[test place] Camera Position:  ".. camPos.x .. ", " .. camPos.y .. ", " ..camPos.z .. "\n"); --test
		if(distance < minDistance){
			point.copy(v3tmp.copy(camPos).add(Insight.GameObject.Find( "Main Camera" ).transform.forward.multiplyScalar(minDistance)));// = camPos + Insight.GameObject.Find( "Main Camera" ).transform.forward * minDistance;
			Insight.Debug.Log("[test] place to defualt distance for out of range \n"); //test
        }
    }else{
		point.copy(v3tmp.copy(camPos).add(Insight.GameObject.Find( "Main Camera" ).transform.forward.multiplyScalar(maxDistance)));// = camPos + Insight.GameObject.Find( "Main Camera" ).transform.forward * maxDistance;
		Insight.Debug.Log("[test] place to defualt distance for Tracked false \n"); //test
	}

	return point;

}

//--------------------------------------------------
// Switch Tool

// function  Fw_Swap( a, b )
// {
// 	return b, a;
// }

//--------------------------------------------------
// 中文字符串的处理
//wzy - todo 不知道具体需求

function Fw_ChineseCharacters_StringToTable(s)
{
    var reg = /[^\u0000-\u00FF]/g;
    var characters = s.match(reg);
    var utfChar;
    var tb = new Array();// = {}
    for(utfChar in characters)
    {
        tb.push(utfChar);
    }
    return tb
}

// function Fw_ChineseCharacters_GetUTFLen(s)
//     local sTable = Fw_ChineseCharacters_StringToTable(s)

//     local len = 0
//     local charLen = 0

//     for i=1,#sTable do
//         local utfCharLen = string.len(sTable[i])
//         if utfCharLen > 1 then -- 长度大于1的就认为是中文
//             charLen = 2
//         else
//             charLen = 1
//         end

//         len = len + charLen
//     end

//     return len
// end

// -- get string length,包含中文或者英文字符
// function Fw_MixCharacters_GetUTFLen(s)
//     local sTable = Fw_ChineseCharacters_StringToTable(s)

//     local len = 0
//     local charLen = 0

//     for i=1,#sTable do
//         local utfCharLen = string.len(sTable[i])
//         len = len + utfCharLen
//     end

//     return len
// end

// --字符count
// function Fw_MixCharacters_GetUTFCount(s)
//     local sTable = Fw_ChineseCharacters_StringToTable(s)

//     local count = 0
//     local charLen = 0

//     for i=1,#sTable do
//         local utfCharLen = string.len(sTable[i])
//         count = count + 1;
//     end

//     return count
// end

// -- string
// function Fw_MixCharacters_GetUTFLenWithCount(s, count)
//     local sTable = Fw_ChineseCharacters_StringToTable(s)

//     local len = 0
//     local charLen = 0
//     local isLimited = (count >= 0)

//     for i=1,#sTable do
//         local utfCharLen = string.len(sTable[i])

//         len = len + utfCharLen

//         if isLimited then
//             count = count - 1;
//             if count <= 0 then
//                 break
//             end
//         end
//     end
//     return len
// end


// function Fw_ChineseCharacters_GetUTFLenWithCount(s, count)
//     local sTable = Fw_ChineseCharacters_StringToTable(s)

//     local len = 0
//     local charLen = 0
//     local isLimited = (count >= 0)

//     for i=1,#sTable do
//         local utfCharLen = string.len(sTable[i])
//         if utfCharLen > 1 then -- 长度大于1的就认为是中文
//             charLen = 2
//         else
//             charLen = 1
//         end

//         len = len + utfCharLen

//         if isLimited then
//             count = count - charLen
//             if count <= 0 then
//                 break
//             end
//         end
//     end
//     return len
// end

// function Fw_ChineseCharacters_GetMaxLenString(s, maxLen)
//     local len = Fw_ChineseCharacters_GetUTFLen(s)

//     local dstString = s
//     -- 超长，裁剪，加...
//     if len > maxLen then
//         dstString = string.sub(s, 1, Fw_ChineseCharacters_GetUTFLenWithCount(s, maxLen))
//         dstString = dstString.."..."
//     end

//     return dstString
// end

// --5个字符，包含汉字或者英文
function Fw_MixCharacters_GetCountString(str, n)
{
	if (str.replace(/[\u4e00-\u9fa5]/g, "**").length <= n) {
		return str;
	}
	else {
		var len = 0;
		var tmpStr = "";
		//字符个数，包含汉字或者英文
		var count = 0;
		for (var i = 0; i < str.length; i++) {
			if (/[\u4e00-\u9fa5]/.test(str[i])) {
				len += 2;
				count++;
			}
			else {
				len += 1;
				count++;
			}
			if (count > n) {
				break;
			}
			else {
				tmpStr += str[i];
			}
		}
		return tmpStr + " ...";
	}
}
// --5个字符，包含汉字或者英文
// function Fw_MixCharacters_GetCountString(s, maxCount)
//     local len = Fw_MixCharacters_GetUTFLen(s);
// 	local count = Fw_MixCharacters_GetUTFCount(s);
	
//     local dstString = s
//     -- 超长，裁剪，加...
//     if len > maxCount then
//         dstString = string.sub(s, 1, Fw_MixCharacters_GetUTFLenWithCount(s, maxCount))
// 		--只有大于最大maxcount 才会加上...
// 		if(count > maxCount) then
// 			dstString = dstString.."..."
// 		end
//     end

//     return dstString
// end

// -- 这里起止index中，截取的长度 = 2 * ( 汉字个数 + 汉字标点 ) + 1 * ( 英文字符 )
// function Fw_ChineseCharacters_SubString(s, startIndex, endIndex)

// 	local start = 1;
// 	if startIndex ~= 1 then
// 		start = Fw_ChineseCharacters_GetUTFLenWithCount(s, startIndex - 1) + 1;
// 	end
// 	local dstString = string.sub(s,start, Fw_ChineseCharacters_GetUTFLenWithCount(s, endIndex)  );
// 	return dstString
// end

//--------------------------------------------------
// SetText相关
// by Cyl

var Fw_TextAlignment_Left = 1;    // 靠左对齐
var Fw_TextAlignment_Center = 2;  // 左右居中
var Fw_TextAlignment_Right = 4;   // 靠右对齐
var Fw_TextAlignment_Top = 8;     // 靠上对齐
var Fw_TextAlignment_Middle = 16; // 上下居中
var Fw_TextAlignment_Bottom = 32; // 靠下对齐

var Fw_TextDirection_Normal = 0;       // 符合现代书写习惯的正常方向，即横向，从左到右
var Fw_TextDirection_LeftToRight = 1;  // 竖向，从左到右
var Fw_TextDirection_RightToLeft = 2;  // 竖向，从右到左

// 文字转贴图
// 用于将输入的字符串显示在贴图上
// 目前还不能修改文字颜色，生成的文字贴图是黑底白字（黑色部分会被剔除为透明），如果要反转成白底黑字（并剔除白色部分）可以修改shader，参考天灯demo
// Params: texture -- 通过 entity 的 GetRenderer():FindProperty( propName , index );方法获取用于显示文字的贴图
//         text -- 要显示的字符串
//         texture_w, texture_h -- 生成的文字贴图的大小（像素值），与材质上赋的原始贴图大小无关
//         padding_left, padding_right, padding_top, padding_bottom -- 显示文字的区域距离贴图左、右、上、下边缘的距离（像素值）
//         font_style -- 字体名称
//         font_width, font_height -- 文字的宽和高（像素值）
//         char_stride, line_stride -- 字间距和行间距（像素值）经过试验，字间距建议为0, 其他值会稍微有点偏差
//         direction -- 文字方向，即 Fw_TextDirection_Normal，Fw_TextDirection_LeftToRight，或 Fw_TextDirection_RightToLeft
//         alignment -- 文字对齐方式。Fw_TextAlignment中六中对齐方式可以通过加法组合使用，比如靠左居中：Fw_TextAlignment_Left + Fw_TextAlignment_Middle
// Author: Cyl

function Fw_SetTextToTexture( texture, text, 
    texture_w, texture_h, 
    padding_left, padding_right, padding_top, padding_bottom, 
    font_style, font_width, font_height, 
    char_stride, line_stride, 
    direction, alignment )
    {
        var char_str = 0;
        var line_str = 0;
        if(direction ==  Fw_TextDirection_Normal){
            line_str = line_stride //+ font_height;
            if (char_stride != 0){                   //-- 字间距设为0时，会自动调整字间距匹配font_width
                char_str = char_stride //+ font_width;
            }
        }
        else
        {
            line_str = line_stride // + font_width;
            if(char_stride != 0){
                char_str = char_stride // + font_height;
            }
        }

        texture.setText(  text
        , texture_w, texture_h
        , padding_left, padding_top, texture_w - padding_right, texture_h - padding_bottom
        , font_style, font_width, font_height, 0
        , char_str, line_str
        , direction , alignment );

}

// function Fw_Table_GetLength(T)
	
// 	if T then	
		
// 	  local count = 0
// 	  for _ in pairs(T) do count = count + 1 end
// 	  return count

// 	else
// 		Insight.Debug.Log("Warning: Fw_Table_GetLength: Table is nil \n");
// 		return 0 ;
// 	end

// end

// function Fw_Table_Clear(T)

// 	if T then
// 		for k in pairs (T) do
//     		T[k] = nil
// 		end
// 	else
// 		Insight.Debug.Log("Warning: Fw_Table_Clear: Table is nil \n");
// 	end
// end
// function Fw_Table_ClearAndDestroyItem(T)
	
// 	if T then

// 		for i = #T,1,-1 do	
// 			Insight.GameObject.Destroy(T[i]);
// 			table.remove( T, i);
// 		end

// 	else
// 		Insight.Debug.Log("Warning: Fw_Table_ClearAndDestroyItem: Table is nil \n");
// 	end
// end

//-------------------------------------------------------------------
//-- Fw_Math



function Fw_Math_Point3To4(v)
{
	return new Insight.Vector4(v.x,v.y,v.z,1);
}

function Fw_Math_Point4To3(v)
{
  var v4 = new Vector4();
  v4.copy(v);
  v4.divideScalar( v4.w );
  return new Insight.Vector3(v4.x,v4.y,v4.z);
}

function Fw_Math_Vector3To4(v)
{ 
  return new Insight.Vector4(v.x,v.y,v.z,0);
}

function Fw_Math_Vector4To3(v)
{
  return new Insight.Vector3(v.x,v.y,v.z);
}

// 使用4x4矩阵对三维点进行坐标变换
function Fw_Math_TransPoint(m,v)
{
  var v4 = Fw_Math_Point3To4(v);
  v4 = MultiplyVector4(m, v4);//m*v4;
  return Fw_Math_Point4To3(v4);
}

// 使用4x4矩阵对三维向量进行坐标变换
function Fw_Math_TransVector(m,v)
{
  var v4 = Fw_Math_Vector3To4(v);
  v4 = MultiplyVector4(m, v4);//m*v4;
  return Fw_Math_Vector4To3(v4);
}
// 计算两个向量的叉积
function Fw_Math_CorssVec3( v1, v2 )
{
	return new Insight.Vector3( v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x );
}

// 计算一个二维点是否在一个矩形内部
// args: Vector2:point, Vector2:rectCenter, Vector2:rectScaleXY
function  Fw_Math_IsPointInRect( point , rectCenter, rectScaleXY  )
{
	var minX = rectCenter.x - rectScaleXY.x;
	var maxX = rectCenter.x + rectScaleXY.x;
	var minY = rectCenter.y - rectScaleXY.y;
	var maxY = rectCenter.y + rectScaleXY.y;

	if( point.x >= minX && point.x <= maxX  && point.y >= minY && point.y <= maxY ){
		return true;
	}else{
		return false;
	}
}
// Lerp
function  Fw_Math_Lerp(a, b, t)
{
	if (typeof a == "number" &&  typeof b == "number"  &&  typeof t == "number")
	{ 
		return a + (b - a) * ( Math.max( 0, Math.min( t, 1.0 ) ) );
	}else{
		print("[Fw_Math_Lerp] error -- Some params are not number!");
		return 0;
	}
}

// Lerp for Vector3
function  Fw_Math_LerpVector3(a, b, t)
{
	var lerpX = Fw_Math_Lerp( a.x, b.x, t );
	var lerpY = Fw_Math_Lerp( a.y, b.y, t );
	var lerpZ = Fw_Math_Lerp( a.z, b.z, t );

	return new Insight.Vector3( lerpX, lerpY, lerpZ ) ;
}

// 根据一个带单位的字符串，返回相应数值（以米为单位）
// 如果字符串不带单位，则将该字符串作为数值返回
// 若字符串中数字部分夹杂其他字符，则返回0
// 支持单位: nm, um, mm, cm, m, km
// Params: string
// Return: number
function Fw_Math_HowManyMetersFromString(s)
{	
	var n = 0
	if (s)
	{
		//if string.find(s,"nm") then
		if(s.include('nm'))
		{
			 var originN = Fw_Math_ToNumberFromStringWithUnit( s, "nm" )
			 n = originN / 1000000000.0
		}
		else if(s.include('um')) //string.find(s,"um") then
		{
			 var originN = Fw_Math_ToNumberFromStringWithUnit( s, "um" )
			 n = originN / 1000000.0
		}
		else if(s.include('mm')) //string.find(s,"mm") then
		{
			 var originN = Fw_Math_ToNumberFromStringWithUnit( s, "mm" )
			 n = originN / 1000.0
		}
		else if(s.include('cm')) //string.find(s,"cm") then
		{
			var originN = Fw_Math_ToNumberFromStringWithUnit( s, "cm" )
			n = originN /100.0
		}		
		else if(s.include('km')) //string.find(s,"km") then
		{
			var originN = Fw_Math_ToNumberFromStringWithUnit( s, "km" )
			n = originN * 1000.0
		}
		else if(s.include('m')) //string.find(s,"m") then
		{
			var originN = Fw_Math_ToNumberFromStringWithUnit( s, "m" )
			n = originN
		}
		else
		{
			var originN = Fw_Math_ToNumberFromStringWithUnit( s, "" )
			n = originN
		}
	}

	return n
}

// 根据一个带单位的字符串，返回相应的数值
// 如果数字中夹杂其他字符（除.之外），将返回0
// Params: s string, u string( the unit of measurement)
// Return: number
function Fw_Math_ToNumberFromStringWithUnit( s, u )
{
	var n = 0
	var sn = s.replace('/'+ u + '/g', "");//string.gsub(s, u, "")
	if (sn){
		n = tonumber(sn);
		if(n)
			return n;
		else return 0;
	}
	return n
}
// Fw_Math
//-------------------------------------------------------------------


//-------------------------------------------------------------------
// Log

// 作为用来在屏幕上输出日志的Text Entity
var Fw_PrintToTextEntity = undefined;

//将log打印到Prefab: Fw_PrintToTextEntity 上，并自动在原来的log内容上新起一行再显示( 局限：1. 不要每帧都打印，否则会很影响性能 2. 在Start()中使用有时会无法执行 )
function Fw_PrintToText( log )
{
	if(Fw_PrintToTextEntity)
	{
		if (Fw_PrintToTextEntity.getComponent( "Text", 0  ).text)
		{
			var oldLog = Fw_PrintToTextEntity.getComponent( "Text", 0  ).text;
			oldLog = oldLog + "\n" + log;
			Fw_PrintToTextEntity.getComponent( "Text", 0  ).text = oldLog;
		}
		else
		{
			Insight.Debug.Log("Warning:  Fw_PrintToText -- the name \"Fw_PrintToTextEntity\" is token by something else  \n")	
		}
	}
	else
	{
		//Insight.Debug.Log("Warning:  Fw_PrintToText -- Please Put the Fw_PrintToTextEntity Prefab In the UI Canvas for log \n");
	} 
}

function Fw_Math_Rad(degrees)
{
    var pi = Math.PI;
    return degrees * (pi/180);
}
function Fw_Math_Deg(radians)
{
	var pi = Math.PI;
	return radians * (180 / pi);
}
//wzy-todo 实现stack的操作

//Return the script module
InsightExtension