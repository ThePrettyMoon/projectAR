// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_freshnelpower("freshnel power", Range( 0 , 5)) = 0
		[HDR]_Color("Color", Color) = (1,1,1,0)
		_WaveTex("Wave Tex", 2D) = "white" {}
		_Uspeed("U-speed", Float) = 0
		_Vspeed("V-speed", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float3 viewDir;
			half3 worldNormal;
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform half _freshnelpower;
		uniform half4 _Color;
		uniform sampler2D _WaveTex;
		SamplerState sampler_WaveTex;
		uniform half _Uspeed;
		uniform half _Vspeed;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half3 ase_worldNormal = i.worldNormal;
			half dotResult42 = dot( i.viewDir , ase_worldNormal );
			half temp_output_46_0 = pow( ( 1.0 - saturate( abs( dotResult42 ) ) ) , _freshnelpower );
			half2 appendResult57 = (half2(_Uspeed , _Vspeed));
			half2 panner53 = ( 1.0 * _Time.y * appendResult57 + i.uv_texcoord);
			half4 tex2DNode52 = tex2D( _WaveTex, panner53 );
			o.Emission = ( temp_output_46_0 * i.vertexColor * _Color * tex2DNode52.r ).rgb;
			o.Alpha = ( temp_output_46_0 * i.vertexColor.a * _Color * tex2DNode52.r ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18703
0;6;1920;1013;1838.737;745.3278;1;True;False
Node;AmplifyShaderEditor.WorldNormalVector;40;-1434.707,-185.259;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;39;-1412.167,-342.6833;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;42;-1210.961,-273.094;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1321.556,-660.3704;Inherit;False;Property;_Uspeed;U-speed;4;0;Create;True;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;45;-962.2513,-251.149;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1326.556,-557.3704;Inherit;False;Property;_Vspeed;V-speed;5;0;Create;True;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;44;-823.2664,-257.4186;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-1291.556,-876.3704;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;57;-1125.556,-646.3704;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-779.6808,-143.8047;Inherit;False;Property;_freshnelpower;freshnel power;1;0;Create;True;0;0;False;0;False;0;4.9;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;53;-953.556,-774.3704;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;58;-613.928,-257.296;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;49;523.4619,-415.9201;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-660.556,-802.3704;Inherit;True;Property;_WaveTex;Wave Tex;3;0;Create;True;0;0;False;0;False;-1;None;992028d320100274481cb8bc69bab36a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;48;-639.0378,-558.7198;Inherit;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;False;0;False;1,1,1,0;0,0.7806989,12.90052,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;46;-391.0022,-290.5652;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-67.03784,-232.7198;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-128.0378,-566.7198;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;38;191.7644,-495.1576;Half;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;New Amplify Shader;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;42;0;39;0
WireConnection;42;1;40;0
WireConnection;45;0;42;0
WireConnection;44;0;45;0
WireConnection;57;0;55;0
WireConnection;57;1;56;0
WireConnection;53;0;54;0
WireConnection;53;2;57;0
WireConnection;58;0;44;0
WireConnection;52;1;53;0
WireConnection;46;0;58;0
WireConnection;46;1;47;0
WireConnection;51;0;46;0
WireConnection;51;1;49;4
WireConnection;51;2;48;0
WireConnection;51;3;52;1
WireConnection;50;0;46;0
WireConnection;50;1;49;0
WireConnection;50;2;48;0
WireConnection;50;3;52;1
WireConnection;38;2;50;0
WireConnection;38;9;51;0
ASEEND*/
//CHKSM=FE2CC7822F38398027661C0F3644855C85DE92C3