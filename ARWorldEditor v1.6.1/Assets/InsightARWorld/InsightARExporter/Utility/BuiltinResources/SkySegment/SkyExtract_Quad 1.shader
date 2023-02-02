// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EZXR/SkyExtract_Quad"
{
	Properties
	{
		_UVswitch("UVswitch", Range( 0 , 1)) = 1
		_FlipV("FlipV", Range( 0 , 1)) = 0
		_FlipU("FlipU", Range( 0 , 1)) = 1
		_Video_Input("Video_Input", 2D) = "white" {}
		_BG("BG", 2D) = "white" {}
		_MainTex("MainTex", 2D) = "white" {}
		_Strenth("Strenth", Float) = 3
		_Power("Power", Float) = 0
		_Mask_Ctrl("Mask_Ctrl", Range( 0 , 1)) = 0
		_videoV_Invert("videoV_Invert", Float) = 0
		_maskOFF("maskOFF", Range( 0 , 1)) = 0
		_skyDark("skyDark", Range( 0 , 1)) = 0.4
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite Off
		ZTest Always
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _Video_Input;
			uniform float _videoV_Invert;
			uniform half _Mask_Ctrl;
			uniform sampler2D _MainTex;
			uniform float _UVswitch;
			uniform float _FlipU;
			uniform float _FlipV;
			uniform float _Power;
			uniform sampler2D _BG;
			uniform float4 _BG_ST;
			uniform float _Strenth;
			uniform float _maskOFF;
			uniform float _skyDark;
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
#endif
				float2 uv0759 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult761 = (float2(uv0759.x , ( 1.0 - uv0759.y )));
				float2 lerpResult762 = lerp( uv0759 , appendResult761 , _videoV_Invert);
				float2 uv0744 = float4(i.ase_texcoord1.xy,0,0).xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult745 = (float2(uv0744.y , uv0744.x));
				float2 lerpResult747 = lerp( uv0744 , appendResult745 , _UVswitch);
				float2 break748 = lerpResult747;
				float lerpResult753 = lerp( break748.x , ( 1.0 - break748.x ) , _FlipU);
				float lerpResult754 = lerp( break748.y , ( 1.0 - break748.y ) , _FlipV);
				float2 appendResult755 = (float2(lerpResult753 , lerpResult754));
				float4 tex2DNode756 = tex2D( _MainTex, appendResult755 );
				float3 appendResult757 = (float3(tex2DNode756.b , tex2DNode756.g , tex2DNode756.r));
				float3 hsvTorgb588 = RGBToHSV( appendResult757 );
				float2 uv_BG = float4(i.ase_texcoord1.xy,0,0).xy * _BG_ST.xy + _BG_ST.zw;
				float4 tex2DNode585 = tex2D( _BG, uv_BG );
				float3 hsvTorgb587 = RGBToHSV( tex2DNode585.rgb );
				float temp_output_665_0 = saturate( ( ( pow( ( _Mask_Ctrl * ( 1.0 - tex2DNode756.a ) ) , _Power ) - saturate( min( ( 1.0 - pow( ( _Mask_Ctrl * ( 1.0 - tex2DNode756.a ) ) , _Power ) ) , ( ( hsvTorgb588.z - hsvTorgb587.z ) * _Strenth ) ) ) ) + saturate( min( ( ( hsvTorgb587.z - hsvTorgb588.z ) * _Strenth ) , pow( ( _Mask_Ctrl * ( 1.0 - tex2DNode756.a ) ) , _Power ) ) ) ) );
				float4 temp_cast_1 = (0.0).xxxx;
				float4 lerpResult562 = lerp( tex2DNode585 , temp_cast_1 , min( temp_output_665_0 , _skyDark ));
				
				
				finalColor = ( ( tex2D( _Video_Input, lerpResult762 ) * max( temp_output_665_0 , _maskOFF ) ) + lerpResult562 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18200
-1732;294;1262;703;65.24883;-806.5206;1.556457;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;744;-3964.058,1176.927;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;745;-3733.614,1268.118;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;746;-4011.211,1092.658;Inherit;False;Property;_UVswitch;UVswitch;0;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;747;-3587.738,1175.018;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;748;-3346.178,1170.855;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;752;-3216.556,1454.645;Inherit;False;Property;_FlipV;FlipV;1;0;Create;True;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;751;-3123.178,1355.855;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;750;-3231.826,1000.587;Inherit;False;Property;_FlipU;FlipU;2;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;749;-3112.178,1121.855;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;754;-2795.295,1297.671;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;753;-2796.89,1164.263;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;755;-2607.725,1212.972;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;756;-2400.101,1186.749;Inherit;True;Property;_MainTex;MainTex;5;0;Create;True;0;0;False;0;False;-1;e9c4642eaa083a54ab91406d8449e6ac;46a188949cfa34a8188f0720fce3dbcb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;699;-1391.181,661.7997;Half;False;Property;_Mask_Ctrl;Mask_Ctrl;9;0;Create;True;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;758;-1826.019,1076.902;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;733;-1493.618,856.3923;Inherit;False;2713.178;1137.424;抠像过程;32;585;623;587;616;589;593;603;592;584;595;640;591;639;594;596;666;665;557;560;562;588;621;732;730;731;760;763;762;765;766;768;769;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;757;-2007.72,1148.506;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;700;-1031.196,637.2358;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;585;-1486.843,1234.373;Inherit;True;Property;_BG;BG;4;0;Create;True;0;0;False;0;False;-1;None;209c0c93cc0f4455282ea92306b5951a;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;623;-911.9725,1154.519;Inherit;False;Property;_Power;Power;7;0;Create;True;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RGBToHSVNode;587;-1063.779,1361.293;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;621;-772.9722,1066.125;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RGBToHSVNode;588;-1061.277,1670.755;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RelayNode;616;-591.1489,1065.588;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;589;-745.3917,1740.816;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;593;-711.725,1656.533;Inherit;False;Property;_Strenth;Strenth;6;0;Create;True;0;0;False;0;False;3;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;603;-449.3599,1407.056;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;592;-484.6343,1718.255;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;584;-803.2924,1430.816;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;595;-431.0583,1508.444;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;640;-241.2847,1695.194;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;759;60.42719,1822.855;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;591;2.700426,1700.745;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;639;-243.1234,1425.013;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;760;285.4272,1917.855;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;594;-34.91162,1104.6;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;596;112.534,1456.962;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;763;291.4272,1712.855;Inherit;False;Property;_videoV_Invert;videoV_Invert;11;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;761;431.4272,1877.855;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;666;220.004,1243.212;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;769;468.6157,1390.193;Inherit;False;Property;_maskOFF;maskOFF;12;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;665;580.0165,1218.418;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;762;592.4272,1857.855;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;766;429.7283,1132.757;Inherit;False;Property;_skyDark;skyDark;13;0;Create;True;0;0;False;0;False;0.4;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;560;-978.7878,1276.982;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;764;644.7283,1068.314;Inherit;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;765;742.7283,1219.314;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;768;750.3345,1334.16;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;557;648.6478,1486.189;Inherit;True;Property;_Video_Input;Video_Input;3;0;Create;True;0;0;False;0;False;-1;None;46a188949cfa34a8188f0720fce3dbcb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;562;879.399,1130.811;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;767;1019.602,1472.684;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;731;-1158.888,1148.472;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;732;-1456.799,1143.607;Inherit;False;Property;_F_BG;F_BG;10;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;730;-1485.016,946.1317;Inherit;True;Property;_BG_1;BG_1;8;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;770;1133.223,1334.159;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;128;1224.656,1132.643;Float;False;True;-1;2;ASEMaterialInspector;100;1;EZXR/SkyExtract_Quad;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;7;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;745;0;744;2
WireConnection;745;1;744;1
WireConnection;747;0;744;0
WireConnection;747;1;745;0
WireConnection;747;2;746;0
WireConnection;748;0;747;0
WireConnection;751;0;748;1
WireConnection;749;0;748;0
WireConnection;754;0;748;1
WireConnection;754;1;751;0
WireConnection;754;2;752;0
WireConnection;753;0;748;0
WireConnection;753;1;749;0
WireConnection;753;2;750;0
WireConnection;755;0;753;0
WireConnection;755;1;754;0
WireConnection;756;1;755;0
WireConnection;758;0;756;4
WireConnection;757;0;756;3
WireConnection;757;1;756;2
WireConnection;757;2;756;1
WireConnection;700;0;699;0
WireConnection;700;1;758;0
WireConnection;587;0;585;0
WireConnection;621;0;700;0
WireConnection;621;1;623;0
WireConnection;588;0;757;0
WireConnection;616;0;621;0
WireConnection;589;0;588;3
WireConnection;589;1;587;3
WireConnection;603;0;616;0
WireConnection;592;0;589;0
WireConnection;592;1;593;0
WireConnection;584;0;587;3
WireConnection;584;1;588;3
WireConnection;595;0;584;0
WireConnection;595;1;593;0
WireConnection;640;0;603;0
WireConnection;640;1;592;0
WireConnection;591;0;640;0
WireConnection;639;0;595;0
WireConnection;639;1;616;0
WireConnection;760;0;759;2
WireConnection;594;0;616;0
WireConnection;594;1;591;0
WireConnection;596;0;639;0
WireConnection;761;0;759;1
WireConnection;761;1;760;0
WireConnection;666;0;594;0
WireConnection;666;1;596;0
WireConnection;665;0;666;0
WireConnection;762;0;759;0
WireConnection;762;1;761;0
WireConnection;762;2;763;0
WireConnection;560;0;585;0
WireConnection;765;0;665;0
WireConnection;765;1;766;0
WireConnection;768;0;665;0
WireConnection;768;1;769;0
WireConnection;557;1;762;0
WireConnection;562;0;560;0
WireConnection;562;1;764;0
WireConnection;562;2;765;0
WireConnection;767;0;557;0
WireConnection;767;1;768;0
WireConnection;731;0;585;0
WireConnection;731;1;730;0
WireConnection;731;2;732;0
WireConnection;770;0;767;0
WireConnection;770;1;562;0
WireConnection;128;0;770;0
ASEEND*/
//CHKSM=0BF6D2DEA5A774BBE12CDAC826CF50980EBDDE58