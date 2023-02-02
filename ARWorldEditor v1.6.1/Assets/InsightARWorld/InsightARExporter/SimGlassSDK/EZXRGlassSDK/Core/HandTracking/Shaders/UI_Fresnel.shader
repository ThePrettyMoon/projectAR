// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "UI_Fresnel"
{
	Properties
	{
		[HDR]_Glow("Glow", Color) = (0,0.8571169,1,0)
		_Color0("Color 0", Color) = (0.4851764,0,0.5660378,0)
		_color1("color1", Color) = (0,0.07041359,1,0)
		_GlowPow("GlowPow", Float) = 3
		_MidOpacity("MidOpacity", Float) = 0
		_TimeScale("TimeScale", Float) = 0
		_Flash("Flash", Float) = 0
		_Opacity("Opacity", Range( 0 , 1)) = 1

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Overlay" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
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
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float3 ase_normal : NORMAL;
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

			uniform float _TimeScale;
			uniform float _Flash;
			uniform float _MidOpacity;
			uniform float _GlowPow;
			uniform float4 _Color0;
			uniform float4 _color1;
			uniform float4 _Glow;
			uniform float _Opacity;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord1.xyz = ase_worldNormal;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.w = 0;
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
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(WorldPosition);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = i.ase_texcoord1.xyz;
				float mulTime18 = _Time.y * _TimeScale;
				float fresnelNdotV1 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode1 = ( ( ( abs( ( frac( mulTime18 ) - 0.5 ) ) * _Flash ) + _MidOpacity ) + 1.0 * pow( 1.0 - fresnelNdotV1, _GlowPow ) );
				float temp_output_23_0 = saturate( fresnelNode1 );
				float4 lerpResult8 = lerp( _Color0 , _color1 , ( ( ase_worldNormal.y + 1.0 ) * 0.5 ));
				float4 lerpResult5 = lerp( lerpResult8 , _Glow , temp_output_23_0);
				
				
				finalColor = ( temp_output_23_0 * lerpResult5 * _Opacity );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18703
607;192;1080;852;960.1655;388.0752;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;20;-1503.768,-269.203;Inherit;False;Property;_TimeScale;TimeScale;5;0;Create;True;0;0;False;0;False;0;1.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;18;-1358.768,-269.203;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;24;-1199.984,-275.8891;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1200.206,-191.7325;Inherit;False;Constant;_Float3;Float 3;6;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;25;-1076.984,-267.8891;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-964.6577,-168.7552;Inherit;False;Property;_Flash;Flash;6;0;Create;True;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;27;-946.5062,-269.0325;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;7;-1187.35,-18.89307;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;16;-942.9938,-66.11444;Inherit;False;Property;_MidOpacity;MidOpacity;4;0;Create;True;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-813.6577,-260.7552;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1122.481,157.1069;Inherit;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-480.5431,-119.6146;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-965.4814,189.1069;Inherit;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-765.7457,234.4924;Inherit;False;Property;_GlowPow;GlowPow;3;0;Create;True;0;0;False;0;False;3;3.86;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-974.4814,86.10693;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;1;-581,-12;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;-0.16;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-693,-652;Inherit;False;Property;_Color0;Color 0;1;0;Create;True;0;0;False;0;False;0.4851764,0,0.5660378,0;0.8120155,0,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;-692.4814,-478.8931;Inherit;False;Property;_color1;color1;2;0;Create;True;0;0;False;0;False;0,0.07041359,1,0;0,0.2810159,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-855.4814,89.10693;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;23;-296.5431,-61.61456;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-703,-330;Inherit;False;Property;_Glow;Glow;0;1;[HDR];Create;True;0;0;False;0;False;0,0.8571169,1,0;0.5803922,1.372549,1.498039,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;8;-411.4814,-331.8931;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;5;-140,-248;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-416.1655,189.9248;Inherit;False;Property;_Opacity;Opacity;7;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-95,-36;Inherit;True;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;3;139,-22;Float;False;True;-1;2;ASEMaterialInspector;100;1;UI_Fresnel;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;0;False;-1;True;0;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Overlay=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;18;0;20;0
WireConnection;24;0;18;0
WireConnection;25;0;24;0
WireConnection;25;1;26;0
WireConnection;27;0;25;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;21;0;28;0
WireConnection;21;1;16;0
WireConnection;10;0;7;2
WireConnection;10;1;11;0
WireConnection;1;0;7;0
WireConnection;1;1;21;0
WireConnection;1;3;15;0
WireConnection;12;0;10;0
WireConnection;12;1;13;0
WireConnection;23;0;1;0
WireConnection;8;0;4;0
WireConnection;8;1;14;0
WireConnection;8;2;12;0
WireConnection;5;0;8;0
WireConnection;5;1;6;0
WireConnection;5;2;23;0
WireConnection;2;0;23;0
WireConnection;2;1;5;0
WireConnection;2;2;30;0
WireConnection;3;0;2;0
ASEEND*/
//CHKSM=41768F2310F5E1E8F78100709B53C2E6F1B67C8E