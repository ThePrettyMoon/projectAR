// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "i3dEngine/Standard Specular Transparent"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_BumpMap("BumpMap", 2D) = "bump" {}
		_BumpScale("BumpScale", Float) = 1
		_SpecGlossMap("SpecGlossMap", 2D) = "black" {}
		_GlossMapScale("GlossMapScale", Range( 0 , 2)) = 0.5
		_OcclusionMap("OcclusionMap", 2D) = "white" {}
		_OcclusionStrength("OcclusionStrength", Range( 0 , 1)) = 1
		_EmissionMap("EmissionMap", 2D) = "black" {}
		_EmissionColor("EmissionColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform half _BumpScale;
		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform half4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _EmissionMap;
		uniform float4 _EmissionMap_ST;
		uniform half4 _EmissionColor;
		uniform sampler2D _SpecGlossMap;
		uniform float4 _SpecGlossMap_ST;
		uniform half _GlossMapScale;
		uniform sampler2D _OcclusionMap;
		uniform float4 _OcclusionMap_ST;
		uniform half _OcclusionStrength;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			o.Normal = ( _BumpScale * UnpackNormal( tex2D( _BumpMap, uv_BumpMap ) ) );
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode14 = tex2D( _MainTex, uv_MainTex );
			o.Albedo = ( _Color * tex2DNode14 ).rgb;
			float2 uv_EmissionMap = i.uv_texcoord * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
			o.Emission = ( tex2D( _EmissionMap, uv_EmissionMap ) * _EmissionColor ).xyz;
			float2 uv_SpecGlossMap = i.uv_texcoord * _SpecGlossMap_ST.xy + _SpecGlossMap_ST.zw;
			float4 tex2DNode36 = tex2D( _SpecGlossMap, uv_SpecGlossMap );
			o.Specular = tex2DNode36.rgb;
			o.Smoothness = ( tex2DNode36.a * _GlossMapScale );
			float2 uv_OcclusionMap = i.uv_texcoord * _OcclusionMap_ST.xy + _OcclusionMap_ST.zw;
			o.Occlusion = ( ( tex2D( _OcclusionMap, uv_OcclusionMap ).g * _OcclusionStrength ) + ( 1.0 - _OcclusionStrength ) );
			o.Alpha = ( _Color.a * tex2DNode14.a );

			// o.Albedo *= o.Alpha;
			// float oneMinusReflectivity = 1 - max (max (o.Specular.r, o.Specular.g), o.Specular.b);
			// o.Alpha = 1 - oneMinusReflectivity + o.Alpha * oneMinusReflectivity;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardSpecular alpha:premul keepalpha fullforwardshadows 

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
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : SV_POSITION
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandardSpecular o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandardSpecular, o )
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
}