// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "i3dEngine/Standard Specular"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_BumpMap("BumpMap", 2D) = "bump" {}
		_BumpScale("BumpScale", Float) = 1
		_SpecGlossMap("SpecGlossMap", 2D) = "white" {}
		_GlossMapScale("GlossMapScale", Range( 0 , 1)) = 0.5
		_OcclusionMap("OcclusionMap", 2D) = "white" {}
		_OcclusionStrength("OcclusionStrength", Range( 0 , 1)) = 1
		_EmissionMap("EmissionMap", 2D) = "black" {}
		_EmissionColor("EmissionColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
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
			o.Albedo = ( _Color * tex2D( _MainTex, uv_MainTex ) ).rgb;
			float2 uv_EmissionMap = i.uv_texcoord * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
			o.Emission = ( tex2D( _EmissionMap, uv_EmissionMap ) * _EmissionColor ).xyz;
			float2 uv_SpecGlossMap = i.uv_texcoord * _SpecGlossMap_ST.xy + _SpecGlossMap_ST.zw;
			float4 tex2DNode36 = tex2D( _SpecGlossMap, uv_SpecGlossMap );
			o.Specular = tex2DNode36.rgb;
			o.Smoothness = ( tex2DNode36.a * _GlossMapScale );
			float2 uv_OcclusionMap = i.uv_texcoord * _OcclusionMap_ST.xy + _OcclusionMap_ST.zw;
			o.Occlusion = ( ( tex2D( _OcclusionMap, uv_OcclusionMap ).g * _OcclusionStrength ) + ( 1.0 - _OcclusionStrength ) );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}