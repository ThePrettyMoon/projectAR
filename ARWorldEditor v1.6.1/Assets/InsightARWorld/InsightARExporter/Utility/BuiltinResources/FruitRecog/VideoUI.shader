// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "i3dEngine/VideoUI"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Transform ("Scaled", vector) = (0.0, 0.0, 1.0, 1.0)
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100	
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Transform;

			float4 _ScanlineDirection;
			float4x4 _TimewarpBegin;
			float4x4 _TimewarpEnd;
			float4x4 _FinalTransform;

			v2f vert ( appdata v )
			{
				v2f o;
				fixed2 fullscreen = v.vertex.xy * 2;
				o.vertex = float4(fullscreen.x * _Transform.z, fullscreen.y * _Transform.w , 1.0, 1.0);	
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				o.uv.y = 1.0 - o.uv.y;
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				// sample the texture
				fixed3 yuv = tex2D(_MainTex, i.uv).xyz;
				fixed4 col = fixed4( yuv , 1.0 );
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
	
	
}
