// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Simple_Refraction"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Refraction("Refraction", Range( 0 , 1)) = 0.05
		_Lighten("Lighten", Range( 0 , 2)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		GrabPass{ }
		CGPROGRAM
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		SamplerState sampler_TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Lighten;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _Refraction;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode4 = tex2D( _TextureSample0, uv_TextureSample0 );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor1 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ase_grabScreenPosNorm + ( tex2DNode4.r * _Refraction * i.vertexColor.a ) ).xy);
			o.Emission = ( ( tex2DNode4.r * _Lighten * i.vertexColor.a ) + screenColor1 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18703
-1080;439;1080;658;1375.087;-109.3353;1;True;False
Node;AmplifyShaderEditor.SamplerNode;4;-1159.6,204;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;74f632725fdf11d4aa0b05d8fe04f729;6a9ec07f676bdd545bf6885f0df63c86;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1135.278,400.4939;Inherit;False;Property;_Refraction;Refraction;1;0;Create;True;0;0;False;0;False;0.05;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;14;-1053.087,530.3353;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;2;-913,5;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-840.2776,279.4939;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;-636,172;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-804.7311,433.3259;Inherit;False;Property;_Lighten;Lighten;2;0;Create;True;0;0;False;0;False;1;0.2;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;1;-496,164;Inherit;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;False;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-477.1311,364.4259;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-289.9311,333.2259;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-171.7,114.8;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Simple_Refraction;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;7;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;4;1
WireConnection;9;1;10;0
WireConnection;9;2;14;4
WireConnection;3;0;2;0
WireConnection;3;1;9;0
WireConnection;1;0;3;0
WireConnection;11;0;4;1
WireConnection;11;1;13;0
WireConnection;11;2;14;4
WireConnection;12;0;11;0
WireConnection;12;1;1;0
WireConnection;0;2;12;0
ASEEND*/
//CHKSM=FFBC065D5366D674B1D2222B4D73357ECFB031EC