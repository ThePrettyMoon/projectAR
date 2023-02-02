-- ------------------------------
-- Copyright (c) NetEase Insight
-- All rights reserved.

-- Framework(Fw_): Lua层的拓展框架
-- 提供常用的变量和Lua方法。
-- 涵盖算法状态、消息交互、中文字符串处理、安全的表操作、AR常用数学计算

-- AR Script 标准版本

-- ------------------------------
 

--------------------------------------------------
-- Constructor

local InsightExtension = {};

function InsightExtension:New( game_object )
	if self ~= InsightExtension then
		return nil, "First argument must be self."
	end
	local new_instance = setmetatable( {} , { __metatable = {}, __index = InsightExtension } );
	new_instance.transform = game_object.transform;
	new_instance.game_object = game_object.transform.gameObject;

	return new_instance;
end

--------------------------------------------------
-- Life Cycle

-- Init SomeThing For Global Usage
function InsightExtension:Awake()

	-- Init The Print Entity
	 Fw_PrintToTextEntity = Insight.GameObject.Find( "Fw_PrintToTextEntity"  );

end

-- Life Cycle
--------------------------------------------------


--------------------------------------------------
-- Global Variables


-- AR Checking Status
-- 不允许修改
Fw_ARState_Uninitialized        = 0;
Fw_ARState_Initing              = 1;
Fw_ARState_Init_OK              = 2;
Fw_ARState_Init_Fail            = 3;
Fw_ARState_Detecting            = 4;
Fw_ARState_Detect_OK            = 5;
Fw_ARState_Detect_Fail          = 6;
Fw_ARState_Tracking             = 7;
Fw_ARState_Track_Limited        = 8;
Fw_ARState_Track_Lost           = 9;
Fw_ARState_Track_Fail           = 10;
Fw_ARState_Track_Stop           = 11;


-- AR Tracking Tpye 使用哪种算法在tracking
-- Set:不允许修改
Fw_ARTrackingTpye_IMU  = 10;
Fw_ARTrackingTpye_VO   = 11;
Fw_ARTrackingTpye_VIO  = 12;



-- Hint for Tracking Limited reason enum
-- Set: 不允许修改

Fw_TrackingLimitedReason_ReasonNone = 0.0;
Fw_TrackingLimitedReason_LowLight = 1.0;
Fw_TrackingLimitedReason_ExcessiveMotion = 2.0;
Fw_TrackingLimitedReason_InsufficientFeatures = 3.0;


--------------------------------------------------------------------------
-- Global Functions

-----------------------------------------------
-- AR Events



-- Functions for Interact With APP
-- methodName : without ()
-- params : if no params, then set nil
-- result : 1 (Succeed) , 0 (Failed)
-- 告知APP该RunScript已经执行
function Fw_Event_SendRunScriptCallback( methodName, params, result )
	
	if params then
		Insight.Event.Happen( 1 , 1 , 500 , methodName .."(\"" .. params .. "\")".."___"..result );
	else
		Insight.Event.Happen( 1 , 1 , 500 , methodName .."()".."___"..result );
	end
end

function Fw_Event_SendRunScript( methodName, params )
	
	if params then
		Insight.Event.Happen( 1 , 1 , 100 , methodName .. "___" .. params );
	else
		Insight.Event.Happen( 1 , 1 , 100 , methodName);
	end

	print("[test event] send message : " .. methodName ); --test
end

function Fw_Event_CloseARScene()
	
	Insight.Event.Happen( 1 , 1 , 101 , nil);
end

--make toast
-- msg 显示的内容
-- during 显示时间
function Fw_Event_MakeToast(msg,during)
	local paramTable = {command="1",time = tostring(during),progress = "0",text = msg };
	if(g_JSON == nil) then
		g_JSON = dofile( "Assets/InsightARWorld/Art/Scripts/Util/Fw_json.lua" );
	end
	local jsonStr = g_JSON:encode(paramTable);
	Insight.Event.Happen( 1 , 1 , 10005 , jsonStr);
end

--load poi product
-- local paramTable ={"eventid " = "3707"};
--  未下载，启动下载，下载完成，进入poi内容
function Fw_Event_LoadPoiData(id,aType)
	local paramTable = {eventid=id,activetype =aType };
	if(g_JSON == nil) then
		g_JSON = dofile( "Assets/InsightARWorld/Art/Scripts/Util/Fw_json.lua" );
	end
	--Insight.Debug.Log("lua "..tostring(id));
	local jsonStr = g_JSON:encode(paramTable);
	Insight.Event.Happen( 1 , 1 , 10008 , jsonStr);
end

--卸载 poi product
-- 0 是叠加状态，2 是挂起状态
-- local paramTable ={"eventid " = "3707","eventtype"= "0"};
function Fw_Event_UnloadPoiData(id,etype)
	local paramTable = {eventid=id,eventtype =etype };
	if(g_JSON == nil) then
		g_JSON = dofile( "Assets/InsightARWorld/Art/Scripts/Util/Fw_json.lua" );
	end
	local jsonStr = g_JSON:encode(paramTable);
	Insight.Event.Happen( 1 , 1 , 10009 , jsonStr);
end

--stop navigation
function Fw_Event_CloseNavigation()
	
	Insight.Event.Happen( 1 , 1 , 10010 , nil);
end

-- local paramTable  = {
  --          visibility = “0-隐藏 1-显示”
--};
function Fw_Event_SetMapVisibility(visible)
	local paramTable = {visibility=visible};
	if(g_JSON == nil) then
		g_JSON = dofile( "Assets/InsightARWorld/Art/Scripts/Util/Fw_json.lua" );
	end
	local jsonStr = g_JSON:encode(paramTable);
	
	Insight.Event.Happen( 1 , 1 , 10011 , jsonStr);
end



function Fw_Event_ReloadARProduct()
	Insight.Event.Happen( 1, 1, 102, nil);
end

function Fw_Event_OpenURL( url, jsonStr )
	
	if url and jsonStr then
		Insight.Event.Happen( 1 , 1 , 108 , url .. "___" .. jsonStr );
		print("[test event] send message : " .. url .. " and json = " .. jsonStr ); --test

	else
		print("[test event] Error : OpenURL params incorrect " ); --test

	end
end

--type___title___description___url___logoImagePatch
--type : 1 text; 2 image; 3 music; 4 video; 5 url
function Fw_Event_Share(shareType, title, description, url, logoImagePath)
	
	local typeStr, titleStr, descriptionStr, urlStr  = " "," "," "," ";
	local logoImagePathStr = "/ShareLogo/ShareLogo.png";
	if shareType then
		typeStr = shareType;
	end
	if title then
		titleStr = title;
	end
	if description then
		descriptionStr = description;
	end
	if url then
		urlStr = url;
	end
	if logoImagePath then
		logoImagePathStr = logoImagePath;
	end
	Insight.Event.Happen( 1, 1, 111, typeStr.. "___" .. titleStr .. "___" .. descriptionStr .. "___" .. urlStr .. "___" .. logoImagePathStr );
end


-----------------------------------------------
-- Rotate Model Towards Camera

function Fw_RotateGameObjectTowardsCamera( goToRotate , alsoRotateY )

	if not goToRotate then
		Insight.Debug.Log( "Error: Fw_RotateGameObjectTowardsAnother : goToRotate is nil" )
		return
	end

	local camera = Insight.GameObject.Find( "Main Camera"  );
	if not camera then
		Insight.Debug.Log( "Error: Fw_RotateGameObjectTowardsAnother : can not find a camera with name : Main Camera" )
		return
	end

	local yRotateRatio = 1.0;
	if not alsoRotateY then
		yRotateRatio = 0.0;
	end

	local targetPos = Insight.Vector3.New( camera.transform.position.x , 
										goToRotate.transform.position.y + yRotateRatio * ( camera.transform.position.y - goToRotate.transform.position.y ) ,
										camera.transform.position.z  )  ;
	goToRotate.transform:LookAt( targetPos, Insight.Vector3.up  )

end


--------------------------------------------------
-- Place Stuff to Real World With Screen point( screen center default )
-- Return: true -- Place Success ; false -- Place failed

function  Fw_PlaceStuffToWorldWithScreenPoint( ref, target, screenPoint2d, OnTrackingFailed )


	-- Get the world position
	-- local viewport = Insight.Camera.Main():GetCamera():GetViewport();
	-- local viewsize = Insight.Vector2.New( viewport:W() , viewport:H() );
	local viewsize = Insight.Vector2.New( Insight.Screen.Width() , Insight.Screen.Height() );
	local crossScreenPos = Insight.Vector2.New( viewsize.x / 2.0, viewsize.y / 2.0 );

	if screenPoint2d then

		crossScreenPos = screenPoint2d;
	end

	-- print( "[g_PlaceStuffToWorld] crossScreenPos for Raycasting " .. crossScreenPos.x .. " , " .. crossScreenPos.y ); -- crossScreePos
	-- print( "[g_PlaceStuffToWorld] Tracking Status: " .. Insight.Tracking.GetStatus() ); -- test the tracking status

	local tracking_result = Insight.Tracking.Raycasting( crossScreenPos.x , crossScreenPos.y );

	local point = tracking_result:Point();
	print( "[g_PlaceStuffToWorld] Tracking Result: " .. (tracking_result:Tracked() and "true" or "false")
		 	.. " (" .. point.x .. "," .. point.y .. "," .. point.z .. ")" );

	if tracking_result:Tracked() then

		-- Show Target
		-- target:SetVisible( true );
		target:SetActive( true  );

		-- Place the target to viewport center pos in world
		-- target:SetLocalTranslation( point );   --( self.cross:GetLocalTranslation());
		target.transform.localPosition = point;

		--print( "[g_PlaceStuffToWorld] tracked position : " .. point.x .. ", " ..point.y .. ", " ..point.z  );

		return true;

	else

		if OnTrackingFailed then

			OnTrackingFailed(ref);
		else
			--print(" Warning : g_PlaceStuffToWorld -- OnTrackingFailed is nil !!!" );

		end

		return false;
	end

end

-- 由屏幕中心发出射线射向现实空间，获取改射线击中现实空间中识别出的环境位置。（返回的结果会考虑环境位置距离相机的最小距离和最大距离。返回的点不会超过这两个距离）
function Fw_GetPositionForPlacingModel( minDistance, maxDistance )

	local viewsize = Insight.Vector2.New( Insight.Screen.Width() , Insight.Screen.Height() );
	local crossScreenPos = Insight.Vector2.New( viewsize.x / 2.0, viewsize.y / 2.0 );

	local tracking_result = Insight.Tracking.Raycasting( crossScreenPos.x , crossScreenPos.y );
	local point = tracking_result.point;
	local camPos = Insight.GameObject.Find( "Main Camera"  ).transform.localPosition; -- Insight.Camera.Main():GetLocalTranslation();
	print( "[Fw_GetPositionForPlacingModel] Tracking Result: " .. (tracking_result.tracked and "true" or "false")
		 	.. " (" .. point.x .. "," .. point.y .. "," .. point.z .. ")" );	

	if tracking_result.tracked then

		local distance = ( point - camPos ).magnitude;
		-- If point is not in correct region
		--Insight.Debug.Log("[test place] Camera Position:  ".. camPos.x .. ", " .. camPos.y .. ", " ..camPos.z .. "\n"); --test
		if distance < minDistance then
			point = camPos + Insight.GameObject.Find( "Main Camera" ).transform.forward * minDistance;
			Insight.Debug.Log("[test] place to defualt distance for out of range \n"); --test
		end
	else
		point = camPos + Insight.GameObject.Find( "Main Camera" ).transform.forward * maxDistance;
		Insight.Debug.Log("[test] place to defualt distance for Tracked false \n"); --test
	end

	return point;
	

end

--------------------------------------------------
-- Switch Tool

function  Fw_Swap( a, b )
	return b, a;
end



--------------------------------------------------
-- 中文字符串的处理


function Fw_ChineseCharacters_StringToTable(s)
    local tb = {}

    --[[
    UTF8的编码规则：
    1. 字符的第一个字节范围： 0x00—0x7F(0-127),或者 0xC2—0xF4(194-244); UTF8 是兼容 ascii 的，所以 0~127 就和 ascii 完全一致
    2. 0xC0, 0xC1,0xF5—0xFF(192, 193 和 245-255)不会出现在UTF8编码中
    3. 0x80—0xBF(128-191)只会出现在第二个及随后的编码中(针对多字节编码，如汉字)
    ]]
    for utfChar in string.gmatch(s, "[%z\1-\127\194-\244][\128-\191]*") do
        table.insert(tb, utfChar)
    end

    return tb
end

function Fw_ChineseCharacters_GetUTFLen(s)
    local sTable = Fw_ChineseCharacters_StringToTable(s)

    local len = 0
    local charLen = 0

    for i=1,#sTable do
        local utfCharLen = string.len(sTable[i])
        if utfCharLen > 1 then -- 长度大于1的就认为是中文
            charLen = 2
        else
            charLen = 1
        end

        len = len + charLen
    end

    return len
end

-- get string length,包含中文或者英文字符
function Fw_MixCharacters_GetUTFLen(s)
    local sTable = Fw_ChineseCharacters_StringToTable(s)

    local len = 0
    local charLen = 0

    for i=1,#sTable do
        local utfCharLen = string.len(sTable[i])
        len = len + utfCharLen
    end

    return len
end

--字符count
function Fw_MixCharacters_GetUTFCount(s)
    local sTable = Fw_ChineseCharacters_StringToTable(s)

    local count = 0
    local charLen = 0

    for i=1,#sTable do
        local utfCharLen = string.len(sTable[i])
        count = count + 1;
    end

    return count
end

-- string
function Fw_MixCharacters_GetUTFLenWithCount(s, count)
    local sTable = Fw_ChineseCharacters_StringToTable(s)

    local len = 0
    local charLen = 0
    local isLimited = (count >= 0)

    for i=1,#sTable do
        local utfCharLen = string.len(sTable[i])

        len = len + utfCharLen

        if isLimited then
            count = count - 1;
            if count <= 0 then
                break
            end
        end
    end
    return len
end


function Fw_ChineseCharacters_GetUTFLenWithCount(s, count)
    local sTable = Fw_ChineseCharacters_StringToTable(s)

    local len = 0
    local charLen = 0
    local isLimited = (count >= 0)

    for i=1,#sTable do
        local utfCharLen = string.len(sTable[i])
        if utfCharLen > 1 then -- 长度大于1的就认为是中文
            charLen = 2
        else
            charLen = 1
        end

        len = len + utfCharLen

        if isLimited then
            count = count - charLen
            if count <= 0 then
                break
            end
        end
    end
    return len
end

function Fw_ChineseCharacters_GetMaxLenString(s, maxLen)
    local len = Fw_ChineseCharacters_GetUTFLen(s)

    local dstString = s
    -- 超长，裁剪，加...
    if len > maxLen then
        dstString = string.sub(s, 1, Fw_ChineseCharacters_GetUTFLenWithCount(s, maxLen))
        dstString = dstString.."..."
    end

    return dstString
end

--5个字符，包含汉字或者英文
function Fw_MixCharacters_GetCountString(s, maxCount)
    local len = Fw_MixCharacters_GetUTFLen(s);
	local count = Fw_MixCharacters_GetUTFCount(s);
	
    local dstString = s
    -- 超长，裁剪，加...
    if len > maxCount then
        dstString = string.sub(s, 1, Fw_MixCharacters_GetUTFLenWithCount(s, maxCount))
		--只有大于最大maxcount 才会加上...
		if(count > maxCount) then
			dstString = dstString.."..."
		end
    end

    return dstString
end

-- 这里起止index中，截取的长度 = 2 * ( 汉字个数 + 汉字标点 ) + 1 * ( 英文字符 )
function Fw_ChineseCharacters_SubString(s, startIndex, endIndex)

	local start = 1;
	if startIndex ~= 1 then
		start = Fw_ChineseCharacters_GetUTFLenWithCount(s, startIndex - 1) + 1;
	end
	local dstString = string.sub(s,start, Fw_ChineseCharacters_GetUTFLenWithCount(s, endIndex)  );
	return dstString
end


--------------------------------------------------
-- SetText相关
-- by Cyl

Fw_TextAlignment_Left = 1;    -- 靠左对齐
Fw_TextAlignment_Center = 2;  -- 左右居中
Fw_TextAlignment_Right = 4;   -- 靠右对齐
Fw_TextAlignment_Top = 8;     -- 靠上对齐
Fw_TextAlignment_Middle = 16; -- 上下居中
Fw_TextAlignment_Bottom = 32; -- 靠下对齐

Fw_TextDirection_Normal = 0;       -- 符合现代书写习惯的正常方向，即横向，从左到右
Fw_TextDirection_LeftToRight = 1;  -- 竖向，从左到右
Fw_TextDirection_RightToLeft = 2;  -- 竖向，从右到左

-- 文字转贴图
-- 用于将输入的字符串显示在贴图上
-- 目前还不能修改文字颜色，生成的文字贴图是黑底白字（黑色部分会被剔除为透明），如果要反转成白底黑字（并剔除白色部分）可以修改shader，参考天灯demo
-- Params: texture -- 通过 entity 的 GetRenderer():FindProperty( propName , index );方法获取用于显示文字的贴图
--         text -- 要显示的字符串
--         texture_w, texture_h -- 生成的文字贴图的大小（像素值），与材质上赋的原始贴图大小无关
--         padding_left, padding_right, padding_top, padding_bottom -- 显示文字的区域距离贴图左、右、上、下边缘的距离（像素值）
--         font_style -- 字体名称
--         font_width, font_height -- 文字的宽和高（像素值）
--         char_stride, line_stride -- 字间距和行间距（像素值）经过试验，字间距建议为0, 其他值会稍微有点偏差
--         direction -- 文字方向，即 Fw_TextDirection_Normal，Fw_TextDirection_LeftToRight，或 Fw_TextDirection_RightToLeft
--         alignment -- 文字对齐方式。Fw_TextAlignment中六中对齐方式可以通过加法组合使用，比如靠左居中：Fw_TextAlignment_Left + Fw_TextAlignment_Middle
-- Author: Cyl

function Fw_SetTextToTexture( texture, text, 
							texture_w, texture_h, 
							padding_left, padding_right, padding_top, padding_bottom, 
							font_style, font_width, font_height, 
							char_stride, line_stride, 
							direction, alignment )
	local char_str = 0;
	local line_str = 0;
	if direction == Fw_TextDirection_Normal then
		line_str = line_stride --+ font_height;
		if char_stride ~= 0 then                   -- 字间距设为0时，会自动调整字间距匹配font_width
			char_str = char_stride --+ font_width;
		end
	else
		line_str = line_stride -- + font_width;
		if char_stride ~= 0 then
			char_str = char_stride -- + font_height;
		end
	end
	
	texture:SetText(  text
                        , texture_w, texture_h
                        , padding_left, padding_top, texture_w - padding_right, texture_h - padding_bottom
                        , font_style, font_width, font_height, 0
                        , char_str, line_str
                        , direction , alignment );

end

-- Example
-- function SetTextTest:Start( )
-- 	self._text_texture = self.entity:GetRenderer():FindProperty( "_text_texture" , 0 );
-- 	if self._text_texture ~= nil then
-- 		Insight.Debug.Log( "Lua TextToTexture Start: Found property " .. self._text_texture:GetName() .. "\n" );

-- 		local font_name = "PingFangSC-Regular"; -- font for iOS
-- 		if Insight.OS.Name() == "Android" then
-- 			font_name = "DroidSans";
-- 		end
-- 		local text = "你好！你好！\n哈哈哈哈哈\nA你好！\n心想事成,新年快乐。\n啦啦啦啦";
-- 		Fw_SetTextToTexture(self._text_texture, text,
-- 						   512, 512,
-- 						   50, 50, 50, 50,
-- 						   font_name, 30, 30,
-- 						   0, 30,
-- 						   Fw_TextDirection_Normal, Fw_TextAlignment_Center + Fw_TextAlignment_Middle );
-- 	end
-- end


-------------------------------------
-- Table Operation


function Fw_Table_GetLength(T)
	
	if T then	
		
	  local count = 0
	  for _ in pairs(T) do count = count + 1 end
	  return count

	else
		Insight.Debug.Log("Warning: Fw_Table_GetLength: Table is nil \n");
		return 0 ;
	end

end

function Fw_Table_Clear(T)

	if T then
		for k in pairs (T) do
    		T[k] = nil
		end
	else
		Insight.Debug.Log("Warning: Fw_Table_Clear: Table is nil \n");
	end
end

function Fw_Table_ClearAndDestroyItem(T)
	
	if T then

		for i = #T,1,-1 do	
			Insight.GameObject.Destroy(T[i]);
			table.remove( T, i);
		end

	else
		Insight.Debug.Log("Warning: Fw_Table_ClearAndDestroyItem: Table is nil \n");
	end
end


-------------------------------------------------------------------
-- Fw_Math



function Fw_Math_Point3To4(v)
  return Insight.Vector4.New(v.x,v.y,v.z,1);
end

function Fw_Math_Point4To3(v)
  local v4 = v;
  v4 = v4/v4:W();
  return Insight.Vector3.New(v4.x,v4.y,v4.z);
end

function Fw_Math_Vector3To4(v)
  return Insight.Vector4.New(v.x,v.y,v.z,0);
end

function Fw_Math_Vector4To3(v)
  return Insight.Vector3.New(v.x,v.y,v.z);
end

-- 使用4x4矩阵对三维点进行坐标变换
function Fw_Math_TransPoint(m,v)
  local v4 = Fw_Math_Point3To4(v);
  v4 = m*v4;
  return Fw_Math_Point4To3(v4);
end

-- 使用4x4矩阵对三维向量进行坐标变换
function Fw_Math_TransVector(m,v)
  local v4 = Fw_Math_Vector3To4(v);
  v4 = m*v4;
  return Fw_Math_Vector4To3(v4);
end



-- 计算两个向量的叉积
function Fw_Math_CorssVec3( v1, v2 )

	return Insight.Vector3.New( v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x );

end

-- 计算一个二维点是否在一个矩形内部
-- args: Vector2:point, Vector2:rectCenter, Vector2:rectScaleXY
function  Fw_Math_IsPointInRect( point , rectCenter, rectScaleXY  )
	
	local minX = rectCenter.x - rectScaleXY.x;
	local maxX = rectCenter.x + rectScaleXY.x;
	local minY = rectCenter.y - rectScaleXY.y;
	local maxY = rectCenter.y + rectScaleXY.y;

	if( point.x >= minX and point.x <= maxX  and point.y >= minY and point.y <= maxY ) then
		return true;
	else
		return false;
	end

end

-- Lerp
function  Fw_Math_Lerp(a, b, t)

	if type(a) == "number" and  type(b) == "number"  and  type(t) == "number"  then 
		return a + (b - a) * ( math.max( 0, math.min( t, 1.0 ) ) );
	else
		print("[Fw_Math_Lerp] error -- Some params are not number!");
		return 0;
	end
end

-- Lerp for Vector3
function  Fw_Math_LerpVector3(a, b, t)

	local lerpX = Fw_Math_Lerp( a.x, b.x, t );
	local lerpY = Fw_Math_Lerp( a.y, b.y, t );
	local lerpZ = Fw_Math_Lerp( a.z, b.z, t );

	return Insight.Vector3.New( lerpX, lerpY, lerpZ ) ;
end



-- 根据一个带单位的字符串，返回相应数值（以米为单位）
-- 如果字符串不带单位，则将该字符串作为数值返回
-- 若字符串中数字部分夹杂其他字符，则返回0
-- 支持单位: nm, um, mm, cm, m, km
-- Params: string
-- Return: number
function Fw_Math_HowManyMetersFromString(s)
	
	local n = 0
	if s then

		if string.find(s,"nm") then

			 local originN = Fw_Math_ToNumberFromStringWithUnit( s, "nm" )
			 n = originN / 1000000000.0
		
		elseif string.find(s,"um") then

			 local originN = Fw_Math_ToNumberFromStringWithUnit( s, "um" )
			 n = originN / 1000000.0
		
		elseif string.find(s,"mm") then

			 local originN = Fw_Math_ToNumberFromStringWithUnit( s, "mm" )
			 n = originN / 1000.0
		
		elseif string.find(s,"cm") then

			local originN = Fw_Math_ToNumberFromStringWithUnit( s, "cm" )
			n = originN /100.0

		elseif string.find(s,"km") then

			local originN = Fw_Math_ToNumberFromStringWithUnit( s, "km" )
			n = originN * 1000.0

		elseif string.find(s,"m") then

			local originN = Fw_Math_ToNumberFromStringWithUnit( s, "m" )
			n = originN
		else
			local originN = Fw_Math_ToNumberFromStringWithUnit( s, "" )
			n = originN
		end

	end

	return n
end

-- 根据一个带单位的字符串，返回相应的数值
-- 如果数字中夹杂其他字符（除.之外），将返回0
-- Params: s string, u string( the unit of measurement)
-- Return: number
function Fw_Math_ToNumberFromStringWithUnit( s, u )
	local n = 0
	local sn = string.gsub(s, u, "")
	if sn then
		n = tonumber(sn) or 0
	end
	return n
end

-- Fw_Math
-------------------------------------------------------------------


-------------------------------------------------------------------
-- Log

-- 作为用来在屏幕上输出日志的Text Entity
Fw_PrintToTextEntity = nil;

--将log打印到Prefab: Fw_PrintToTextEntity 上，并自动在原来的log内容上新起一行再显示( 局限：1. 不要每帧都打印，否则会很影响性能 2. 在Start()中使用有时会无法执行 )
function Fw_PrintToText( log )

	if Fw_PrintToTextEntity then
		if Fw_PrintToTextEntity:GetComponent( "Text", 0  ).text then
			local oldLog = Fw_PrintToTextEntity:GetComponent( "Text", 0  ).text;
			oldLog = oldLog .. "\n" .. log;
			Fw_PrintToTextEntity:GetComponent( "Text", 0  ).text = oldLog;
		else
			Insight.Debug.Log("Warning:  Fw_PrintToText -- the name \"Fw_PrintToTextEntity\" is token by something else  \n")	
		end
	else
		--Insight.Debug.Log("Warning:  Fw_PrintToText -- Please Put the Fw_PrintToTextEntity Prefab In the UI Canvas for log \n");
	end 
end

-- Log
-------------------------------------------------------------------


-------------------------------------------------------------------
-- Stack Table

-- Lua实现的一个Stack

-- Usage Example:
-- stack = Fw_Stack:Create()
-- stack:Push("a","b","c")
-- stack:PrintAll()
-- print( "stack:Peek() " .. stack:Peek() )
-- stack:Pop(2)
-- stack:PrintAll()
-- print( "stack:GetCount() " .. stack:GetCount() )

Fw_Stack = {}

-- Create a Table with Fw_Stack functions
function Fw_Stack:Create()

  -- Fw_Stack table
  local t = {}
  -- entry table,用于存放Push入的元素
  t._et = {}

  -- push a value on to the Fw_Stack
  function t:Push(...)
    if ... then
      local targs = {...}
      -- add values
      for _,v in ipairs(targs) do
        table.insert(self._et, v)
      end
    end
  end

  -- pop a value from the Fw_Stack
  function t:Pop(num)

    -- get num values from Fw_Stack
    local num = num or 1

    -- return table
    local entries = {}

    -- get values into entries
    for i = 1, num do
      -- get last entry
      if #self._et ~= 0 then
        table.insert(entries, self._et[#self._et])
        -- remove last value
        table.remove(self._et)
      else
        break
      end
    end
    -- return unpacked entries
	-- return unpack(entries) 我们的解析器无法解析unpack()方法,但可以使用 table.unpack()
	--for i,v in ipairs(entries) do  return v end   
	return table.unpack(entries)


  end

  function t:Clear()
	if #self._et ~= 0 then
		t:Pop( #self._et )
	end
  end

  -- get entries
  function t:GetCount()
    return #self._et
  end

  -- get Top Element
  function t:Peek()
	if #self._et ~= 0 then
		return self._et[#self._et]
	else
		return nil
	end
  end

  -- return all values to an table
  function t:ToArrayTable( arrayTable )
	arrayTable = {}
	if arrayTable then
		for i = 1, #self._et, 1 do
			if #self._et ~= 0 then
			  table.insert(arrayTable, self._et[i])
			else
			  break
			end
		end
	end

  end

  -- print all values
  function t:PrintAll()
    for i,v in pairs(self._et) do
      print(i, v)
    end
  end

  return t
end

-- Stack Table
---------------------------------------------------------------------


-------------------------

return InsightExtension;
