// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Examples/Earth/Height_Flow_ClampTest"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color0("Color 0", Color) = (1,0.5441177,0.5441177,0)
		_Color1("Color 1", Color) = (0,0.2965517,1,0)
		_Step("Step", Float) = 10
		_Opacity("Opacity", Range( 0 , 1)) = 1
		_Range("Range", Range( 0 , 1)) = 1
		_TimeOffset("TimeOffset", Float) = 0
		_Step_Test("Step_Test", Range( 0 , 1)) = 0
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform float4 _Color0;
		uniform float _Step;
		uniform float _Range;
		uniform float _TimeOffset;
		uniform float4 _Color1;
		uniform float _Opacity;
		uniform float _Step_Test;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float temp_output_26_0 = ( i.vertexColor.r * _Step * _Range );
			float temp_output_80_0 = ( _Step * i.vertexColor.b * _Range );
			float temp_output_102_0 = frac( ( temp_output_26_0 + temp_output_80_0 + _TimeOffset ) );
			float temp_output_29_0 = ( 1.0 / _Step );
			float temp_output_30_0 = ( floor( ( temp_output_102_0 + temp_output_26_0 ) ) * temp_output_29_0 );
			float clampResult76 = clamp( ( temp_output_30_0 * 5.0 ) , 0.0 , 1.0 );
			float4 clampResult74 = clamp( ( _Color0 * clampResult76 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float temp_output_78_0 = ( floor( ( temp_output_80_0 + temp_output_102_0 ) ) * temp_output_29_0 );
			float clampResult82 = clamp( ( temp_output_78_0 * 5.0 ) , 0.0 , 1.0 );
			float4 clampResult85 = clamp( ( _Color1 * clampResult82 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float clampResult108 = clamp( ( ( max( temp_output_30_0 , temp_output_78_0 ) + ( 1.0 - step( temp_output_102_0 , 0.96 ) ) ) * (-1.0 + (max( clampResult76 , clampResult82 ) - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
			o.Emission = ( max( clampResult74 , clampResult85 ) * ( 1.0 + clampResult108 + clampResult108 ) ).rgb;
			o.Alpha = ( clampResult108 * _Opacity * ( 1.0 - step( clampResult108 , _Step_Test ) ) );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
0;45;1782;1035;570.0005;200.7476;1.104199;True;True
Node;AmplifyShaderEditor.RangedFloatNode;27;-745.587,259.4877;Float;False;Property;_Step;Step;2;0;10;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;146;-853.5466,344.1817;Float;False;Property;_Range;Range;4;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;11;-760.197,-10.14287;Float;False;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;147;-527.655,513.9648;Float;False;Property;_TimeOffset;TimeOffset;5;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-498.9857,45.26822;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-512.525,319.3879;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;120;-309.4566,420.5174;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.FractNode;102;-180.6582,456.106;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;144;-348.6283,2.569885;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;145;-356.6283,258.5699;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;28;-734.3358,175.9039;Float;False;Constant;_Float0;Float 0;3;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.FloorOpNode;79;-243.0841,256.355;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.FloorOpNode;25;-227.3341,8.633957;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;29;-493.9618,151.4725;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;116;-113.3136,163.3422;Float;False;Constant;_Float1;Float 1;3;0;5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-104.0922,58.94059;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-106.0106,254.4602;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;53.9978,63.677;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;10.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;55.62378,225.6895;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;10.0;False;1;FLOAT
Node;AmplifyShaderEditor.StepOpNode;129;12.1814,431.7969;Float;False;2;0;FLOAT;0.96;False;1;FLOAT;0.96;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;82;201.7262,195.6505;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;76;202.2455,61.45723;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;111;184.6168,448.4941;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMaxOp;130;386.7386,111.7969;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMaxOp;132;216.7386,320.7969;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;133;501.4772,109.7943;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;-1.0;False;4;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;84;375.5167,320.7196;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;668.8791,114.6531;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;17;134.7158,-147.0109;Float;False;Property;_Color1;Color 1;1;0;0,0.2965517,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;14;103.4623,-351.7437;Float;False;Property;_Color0;Color 0;0;0;1,0.5441177,0.5441177,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;108;812.2308,91.38481;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;400.3696,-194.0191;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;399.0958,-327.25;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;150;716.0723,396.28;Float;False;Property;_Step_Test;Step_Test;6;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;85;566.6993,-193.9649;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;114;811.8744,-3.717572;Float;False;Constant;_Float2;Float 2;3;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;74;570.3869,-339.141;Float;False;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR
Node;AmplifyShaderEditor.StepOpNode;149;1004.833,321.4808;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;135;1008.515,156.7489;Float;False;Property;_Opacity;Opacity;3;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;151;1198.219,360.2568;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;115;990.5193,25.28975;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMaxOp;131;776.7386,-266.2031;Float;False;2;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;143;1301.379,133.9896;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;141;-534.6185,627.0676;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;1160.119,-103.41;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;10;1552.394,-99.90784;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Earth/Height_Flow_ClampTest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;0;11;1
WireConnection;26;1;27;0
WireConnection;26;2;146;0
WireConnection;80;0;27;0
WireConnection;80;1;11;3
WireConnection;80;2;146;0
WireConnection;120;0;26;0
WireConnection;120;1;80;0
WireConnection;120;2;147;0
WireConnection;102;0;120;0
WireConnection;144;0;102;0
WireConnection;144;1;26;0
WireConnection;145;0;80;0
WireConnection;145;1;102;0
WireConnection;79;0;145;0
WireConnection;25;0;144;0
WireConnection;29;0;28;0
WireConnection;29;1;27;0
WireConnection;30;0;25;0
WireConnection;30;1;29;0
WireConnection;78;0;79;0
WireConnection;78;1;29;0
WireConnection;75;0;30;0
WireConnection;75;1;116;0
WireConnection;81;0;78;0
WireConnection;81;1;116;0
WireConnection;129;0;102;0
WireConnection;82;0;81;0
WireConnection;76;0;75;0
WireConnection;111;0;129;0
WireConnection;130;0;76;0
WireConnection;130;1;82;0
WireConnection;132;0;30;0
WireConnection;132;1;78;0
WireConnection;133;0;130;0
WireConnection;84;0;132;0
WireConnection;84;1;111;0
WireConnection;126;0;84;0
WireConnection;126;1;133;0
WireConnection;108;0;126;0
WireConnection;23;0;17;0
WireConnection;23;1;82;0
WireConnection;22;0;14;0
WireConnection;22;1;76;0
WireConnection;85;0;23;0
WireConnection;74;0;22;0
WireConnection;149;0;108;0
WireConnection;149;1;150;0
WireConnection;151;0;149;0
WireConnection;115;0;114;0
WireConnection;115;1;108;0
WireConnection;115;2;108;0
WireConnection;131;0;74;0
WireConnection;131;1;85;0
WireConnection;143;0;108;0
WireConnection;143;1;135;0
WireConnection;143;2;151;0
WireConnection;113;0;131;0
WireConnection;113;1;115;0
WireConnection;10;2;113;0
WireConnection;10;9;143;0
ASEEND*/
//CHKSM=9131427E42BA97C277BD67C5F7E73E1543C3FD5C