// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Examples/Earth/Flowmap_Opacity"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Cube_Color("Cube_Color", Color) = (1,1,1,1)
		_Emission("Emission", Range( 0 , 10)) = 0
		_Texture("Texture", 2D) = "white" {}
		_Flow_UV("Flow_UV", 2D) = "white" {}
		_TimeOffset("TimeOffset", Float) = 0
		_ClipHeight("ClipHeight", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float2 texcoord_0;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _Texture;
		uniform sampler2D _Flow_UV;
		uniform float4 _Flow_UV_ST;
		uniform float _TimeOffset;
		uniform float4 _Cube_Color;
		uniform float _Emission;
		uniform float _ClipHeight;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Flow_UV = i.uv_texcoord * _Flow_UV_ST.xy + _Flow_UV_ST.zw;
			float4 tex2DNode142 = tex2D( _Flow_UV, uv_Flow_UV );
			float2 appendResult146 = (float2(tex2DNode142.r , tex2DNode142.g));
			float temp_output_70_0 = ( _TimeOffset * 0.2 );
			float temp_output_65_0 = frac( temp_output_70_0 );
			float2 lerpResult95 = lerp( appendResult146 , i.texcoord_0 , temp_output_65_0);
			float temp_output_77_0 = frac( ( temp_output_70_0 + ( ( 1.0 / 0.2 ) / 2.0 ) ) );
			float2 lerpResult99 = lerp( appendResult146 , i.texcoord_0 , temp_output_77_0);
			float4 temp_output_128_0 = ( ( tex2D( _Texture, lerpResult95 ) * ( abs( ( temp_output_77_0 - 0.5 ) ) * 2.0 ) ) + ( tex2D( _Texture, lerpResult99 ) * ( abs( ( temp_output_65_0 - 0.5 ) ) * 2.0 ) ) );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			float dotResult134 = dot( ase_worldlightDir , i.worldNormal );
			float clampResult139 = clamp( dotResult134 , 0.2 , 1.0 );
			o.Emission = max( ( temp_output_128_0 * _Cube_Color * ( clampResult139 * _Emission ) ) , _Cube_Color ).rgb;
			float componentMask147 = temp_output_128_0.x;
			float lerpResult44 = lerp( componentMask147 , 1.0 , _Cube_Color.a);
			o.Alpha = (( ase_worldPos.y > _ClipHeight ) ? lerpResult44 :  0.0 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

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
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
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
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
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
79;184;1656;891;2347.552;347.1231;2.090857;True;True
Node;AmplifyShaderEditor.CommentaryNode;131;-1994.198,273.9999;Float;False;881.6998;431.9004;Comment;10;72;73;74;75;76;70;71;77;65;148;时间交替遮罩;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-1917.349,436.8081;Float;False;Constant;_Float1;Float 1;4;0;0.2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;73;-1917.011,528.9571;Float;False;Constant;_Float2;Float 2;4;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;74;-1734.599,489.3004;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;148;-1921.505,341.7287;Float;False;Property;_TimeOffset;TimeOffset;4;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;75;-1753.898,590.9005;Float;False;Constant;_Float3;Float 3;4;0;2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-1593.598,342.5001;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.2;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;76;-1583.298,466.8002;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-1413.798,425.4005;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;119;-1041.001,554.0997;Float;False;Constant;_Float4;Float 4;4;0;0.5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.FractNode;77;-1266.5,417.7003;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;142;-1733.034,-261.3599;Float;True;Property;_Flow_UV;Flow_UV;3;0;Assets/Res/Earth/Textures/flow_Base.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.FractNode;65;-1272.398,324.0001;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleSubtractOpNode;123;-879.4,625.8791;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleSubtractOpNode;118;-872.6006,423.1998;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;86;-1642.374,-24.26061;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DynamicAppendNode;146;-1127.733,-206.1601;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.AbsOpNode;121;-722.8006,421.9997;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;95;-915.4988,-163.3001;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0,0,0;False;2;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.LerpOp;99;-923.3987,104.6;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0,0,0;False;2;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.AbsOpNode;125;-725.7,628.5792;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;122;-744.3008,526.8998;Float;False;Constant;_Float5;Float 5;4;0;2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;56;-643.7996,-93.50006;Float;True;Property;_Texture;Texture;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;133;-431.535,-310.6606;Float;False;1;0;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.WorldNormalVector;135;-380.1349,-232.1604;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;-547.5005,469.4;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;2.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-546.3001,572.1793;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;2.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;145;-649.233,127.1399;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;None;True;0;False;white;Auto;False;Instance;56;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-243.8002,275.8994;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.DotProductOpNode;134;-180.0342,-288.0604;Float;False;2;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;129;-244.1,172.3994;Float;False;2;2;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ClampOpNode;139;-61.23413,-287.5606;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.2;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;49;-137.0005,-75.63214;Float;False;Property;_Emission;Emission;1;0;0;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;128;-83.74548,219.6465;Float;False;2;2;0;FLOAT4;0.0;False;1;COLOR;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;138;144.2648,-192.2609;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;24;-135.5,33.40002;Float;False;Property;_Cube_Color;Cube_Color;0;0;1,1,1,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;147;105.5634,264.1393;Float;False;True;False;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;45;168.899,353.8;Float;False;Constant;_Float0;Float 0;2;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;270.3402,-22.59985;Float;False;3;3;0;FLOAT4;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.LerpOp;44;399.1989,200.9002;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;149;135.5237,619.9304;Float;False;Property;_ClipHeight;ClipHeight;5;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;151;113.5237,446.9304;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;66;-1940.408,160.3914;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMaxOp;154;439.3966,12.55804;Float;False;2;0;FLOAT4;0.0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.TFHCCompareGreater;150;445.5237,404.9304;Float;False;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;579.4996,19.19986;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Earth/Flowmap_Opacity;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Transparent;0.5;True;True;0;False;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;SrcAlpha;OneMinusSrcAlpha;2;SrcAlpha;OneMinusSrcAlpha;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;74;0;73;0
WireConnection;74;1;72;0
WireConnection;70;0;148;0
WireConnection;70;1;72;0
WireConnection;76;0;74;0
WireConnection;76;1;75;0
WireConnection;71;0;70;0
WireConnection;71;1;76;0
WireConnection;77;0;71;0
WireConnection;65;0;70;0
WireConnection;123;0;65;0
WireConnection;123;1;119;0
WireConnection;118;0;77;0
WireConnection;118;1;119;0
WireConnection;146;0;142;1
WireConnection;146;1;142;2
WireConnection;121;0;118;0
WireConnection;95;0;146;0
WireConnection;95;1;86;0
WireConnection;95;2;65;0
WireConnection;99;0;146;0
WireConnection;99;1;86;0
WireConnection;99;2;77;0
WireConnection;125;0;123;0
WireConnection;56;1;95;0
WireConnection;120;0;121;0
WireConnection;120;1;122;0
WireConnection;126;0;125;0
WireConnection;126;1;122;0
WireConnection;145;1;99;0
WireConnection;130;0;145;0
WireConnection;130;1;126;0
WireConnection;134;0;133;0
WireConnection;134;1;135;0
WireConnection;129;0;56;0
WireConnection;129;1;120;0
WireConnection;139;0;134;0
WireConnection;128;0;129;0
WireConnection;128;1;130;0
WireConnection;138;0;139;0
WireConnection;138;1;49;0
WireConnection;147;0;128;0
WireConnection;23;0;128;0
WireConnection;23;1;24;0
WireConnection;23;2;138;0
WireConnection;44;0;147;0
WireConnection;44;1;45;0
WireConnection;44;2;24;4
WireConnection;154;0;23;0
WireConnection;154;1;24;0
WireConnection;150;0;151;2
WireConnection;150;1;149;0
WireConnection;150;2;44;0
WireConnection;0;2;154;0
WireConnection;0;9;150;0
ASEEND*/
//CHKSM=B1760E372A714D461EE2D10262A142E8D5BFED9E