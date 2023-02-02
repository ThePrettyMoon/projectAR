// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Examples/Earth/Earth_Arise"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_MetallicGlossMap("MetallicGlossMap", 2D) = "white" {}
		_Color1("Color 1", Color) = (0,0,0,0)
		_Color0("Color 0", Color) = (0,0,0,0)
		_Power("Power", Float) = 0
		_BumpMap("BumpMap", 2D) = "bump" {}
		_Color("Color", Color) = (1,1,1,1)
		_ClipHeight("ClipHeight", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha , SrcAlpha OneMinusSrcAlpha
		BlendOp Add , Add
		CGINCLUDE
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
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Color0;
		uniform float4 _Color1;
		uniform float _Power;
		uniform sampler2D _MetallicGlossMap;
		uniform float4 _MetallicGlossMap_ST;
		uniform float _ClipHeight;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			o.Normal = UnpackNormal( tex2D( _BumpMap, uv_BumpMap ) );
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			o.Albedo = ( _Color * tex2D( _MainTex, uv_MainTex ) ).rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNDotV4 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode4 = ( 0 + 1 * pow( 1.0 - fresnelNDotV4, _Power ) );
			float4 lerpResult7 = lerp( _Color0 , _Color1 , fresnelNode4);
			o.Emission = lerpResult7.rgb;
			float2 uv_MetallicGlossMap = i.uv_texcoord * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
			o.Smoothness = tex2D( _MetallicGlossMap, uv_MetallicGlossMap ).r;
			o.Alpha = (( ase_worldPos.y > _ClipHeight ) ? 1 :  0 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

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
			#include "HLSLSupport.cginc"
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
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
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
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
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
Version=15001
44;45;1920;1035;1105.8;727.0021;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;8;-968.8998,-170.7;Float;False;Property;_Power;Power;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-456.8998,-482.4572;Float;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-408.3376,-291.1137;Float;False;Property;_Color0;Color 0;4;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-109.8842,623.8329;Float;False;Property;_ClipHeight;ClipHeight;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;4;-744.8998,-244.7;Float;True;World;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;8.12;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-304.1036,-674.4247;Float;False;Property;_Color;Color;7;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-411.8862,-98.59641;Float;False;Property;_Color1;Color 1;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;13;-137.3422,392.0086;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;21.27675,-548.837;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-465.9661,142.8136;Float;True;Property;_MetallicGlossMap;MetallicGlossMap;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCCompareGreater;14;83.05034,401.1301;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;7;-65.89978,-257.7;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-481.4613,458.0537;Float;True;Property;_BumpMap;BumpMap;6;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;299.5269,-260.268;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Examples/Earth/Earth_Arise;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;2;SrcAlpha;OneMinusSrcAlpha;2;SrcAlpha;OneMinusSrcAlpha;Add;Add;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;3;8;0
WireConnection;17;0;15;0
WireConnection;17;1;1;0
WireConnection;14;0;13;2
WireConnection;14;1;18;0
WireConnection;7;0;5;0
WireConnection;7;1;6;0
WireConnection;7;2;4;0
WireConnection;0;0;17;0
WireConnection;0;1;12;0
WireConnection;0;2;7;0
WireConnection;0;4;3;1
WireConnection;0;9;14;0
ASEEND*/
//CHKSM=4E6907CEC8DB1F6C294C4371A34478F12D6E6E11