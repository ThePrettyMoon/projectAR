// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NavMesh"
{
	Properties
	{
		_Color0("Color 0", Color) = (1,1,1,0)
		_Color1("Color 1", Color) = (0,0.2935211,0.735849,0)
		_S1("S1", Range( 0 , 1)) = 0
		_S2("S2", Range( 0 , 1)) = 1
		_Emission1("Emission1", Color) = (1,0.7603418,0,0)
		_Emission2("Emission2", Color) = (1,0.7603418,0,0)
		_TimeScale("TimeScale", Float) = 1
		_Offset("Offset", Vector) = (0,0,0.1,0)
		_TimeOffset("TimeOffset", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform float3 _Offset;
		uniform float _TimeScale;
		uniform float _TimeOffset;
		uniform float4 _Color0;
		uniform float4 _Color1;
		uniform float4 _Emission1;
		uniform float4 _Emission2;
		uniform float _S1;
		uniform float _S2;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime30 = _Time.y * _TimeScale;
			v.vertex.xyz += ( _Offset * sin( ( mulTime30 + _TimeOffset ) ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult3 = lerp( _Color0 , _Color1 , i.vertexColor.r);
			o.Albedo = lerpResult3.rgb;
			float4 lerpResult21 = lerp( _Emission1 , _Emission2 , i.vertexColor.r);
			o.Emission = lerpResult21.rgb;
			float lerpResult10 = lerp( _S1 , _S2 , i.vertexColor.r);
			o.Smoothness = lerpResult10;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17400
704;179;1066;747;938.1282;-335.5843;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;31;-670.2852,691.2903;Inherit;False;Property;_TimeScale;TimeScale;6;0;Create;True;0;0;False;0;1;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-513.1282,835.5842;Inherit;False;Property;_TimeOffset;TimeOffset;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;30;-510.2856,690.2903;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-338.1282,785.5842;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;29;-280.2856,695.2903;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-511,15;Inherit;False;Property;_S2;S2;3;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;28;-322.7971,545.424;Inherit;False;Property;_Offset;Offset;7;0;Create;True;0;0;False;0;0,0,0.1;0.002,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;22;-457.1867,379.7479;Inherit;False;Property;_Emission2;Emission2;5;0;Create;True;0;0;False;0;1,0.7603418,0,0;0,0.2404628,0.3396226,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;4;-438,-331;Inherit;False;Property;_Color0;Color 0;0;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-449.8,-169.6;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;0,0.2935211,0.735849,0;0,0.2935211,0.735849,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-509,99;Inherit;False;Property;_S1;S1;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;-441.1621,189.4578;Inherit;False;Property;_Emission1;Emission1;4;0;Create;True;0;0;False;0;1,0.7603418,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;2;-808.5743,-153.8338;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;3;-166,-124;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;10;-178,17;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;21;-184.9012,252.411;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-145.797,637.424;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;357.9,5.800001;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;NavMesh;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;31;0
WireConnection;32;0;30;0
WireConnection;32;1;33;0
WireConnection;29;0;32;0
WireConnection;3;0;4;0
WireConnection;3;1;5;0
WireConnection;3;2;2;1
WireConnection;10;0;12;0
WireConnection;10;1;11;0
WireConnection;10;2;2;1
WireConnection;21;0;18;0
WireConnection;21;1;22;0
WireConnection;21;2;2;1
WireConnection;27;0;28;0
WireConnection;27;1;29;0
WireConnection;0;0;3;0
WireConnection;0;2;21;0
WireConnection;0;4;10;0
WireConnection;0;11;27;0
ASEEND*/
//CHKSM=E6541758D560748EAFFCB18783605FFB7967B655








