// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Examples/Earth/Unlit_UVPanner"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Arrow_Weather("Arrow_Weather", 2D) = "white" {}
		_Color_main("Color_main", Color) = (1,1,1,1)
		_Speed("Speed", Float) = 0
		_Color_2("Color_2", Color) = (0.2279412,0.7124748,1,0)
		_Color_1("Color_1", Color) = (1,0.2647059,0.2647059,0)
		_TimeOffset("TimeOffset", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float2 texcoord_0;
		};

		uniform float4 _Color_main;
		uniform sampler2D _Arrow_Weather;
		uniform float _TimeOffset;
		uniform float _Speed;
		uniform float4 _Arrow_Weather_ST;
		uniform float4 _Color_1;
		uniform float4 _Color_2;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Arrow_Weather = i.uv_texcoord * _Arrow_Weather_ST.xy + _Arrow_Weather_ST.zw;
			float4 tex2DNode1 = tex2D( _Arrow_Weather, (abs( uv_Arrow_Weather+( _TimeOffset * _Speed ) * float2(1,0 ))) );
			float4 lerpResult12 = lerp( _Color_1 , _Color_2 , (-1.0 + (i.texcoord_0.x - 0.0) * (2.0 - -1.0) / (1.0 - 0.0)));
			o.Emission = ( _Color_main * tex2DNode1.r * lerpResult12 ).rgb;
			float componentMask28 = lerpResult12.a;
			float clampResult29 = clamp( componentMask28 , 0.0 , 1.0 );
			o.Alpha = ( clampResult29 * tex2DNode1.r );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

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
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
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
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
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
Version=12001
12;200;1853;899;2540.682;282.0181;1.556934;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-2641.099,-113.2997;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;31;-1315.375,322.0718;Float;False;Property;_TimeOffset;TimeOffset;5;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;11;-1294.65,424.1372;Float;False;Property;_Speed;Speed;2;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;13;-1569.798,-436.8997;Float;False;Property;_Color_1;Color_1;4;0;1,0.2647059,0.2647059,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;30;-2190.096,53.0989;Float;False;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;-1.0;False;4;FLOAT;2.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;14;-1569.098,-223.5996;Float;False;Property;_Color_2;Color_2;3;0;0.2279412,0.7124748,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1589.003,-43.60007;Float;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;12;-1133.898,-213.9997;Float;False;3;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1102.801,292.1;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.PannerNode;5;-975.5001,163.3001;Float;False;1;0;2;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.ComponentMaskNode;28;-789.9978,-281.0005;Float;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;3;-662.4999,-56.20004;Float;False;Property;_Color_main;Color_main;1;0;1,1,1,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;29;-854.9979,671.8994;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-691.4997,144.4;Float;True;Property;_Arrow_Weather;Arrow_Weather;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-206.1002,41.99997;Float;False;3;3;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleTimeNode;9;-1555.381,267.7458;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-243.9979,300.0995;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;Earth/Unlit_UVPanner;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Off;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;2;SrcAlpha;OneMinusSrcAlpha;2;SrcAlpha;OneMinusSrcAlpha;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;15;1
WireConnection;12;0;13;0
WireConnection;12;1;14;0
WireConnection;12;2;30;0
WireConnection;10;0;31;0
WireConnection;10;1;11;0
WireConnection;5;0;6;0
WireConnection;5;1;10;0
WireConnection;28;0;12;0
WireConnection;29;0;28;0
WireConnection;1;1;5;0
WireConnection;2;0;3;0
WireConnection;2;1;1;1
WireConnection;2;2;12;0
WireConnection;27;0;29;0
WireConnection;27;1;1;1
WireConnection;0;2;2;0
WireConnection;0;9;27;0
ASEEND*/
//CHKSM=964BD9EC4D1D83F31C2AB8796B5BC7250F84E11B