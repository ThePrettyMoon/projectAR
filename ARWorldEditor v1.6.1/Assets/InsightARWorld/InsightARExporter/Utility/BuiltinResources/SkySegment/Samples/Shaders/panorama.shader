Shader "i3dEngine/Panorama"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		// _SkyMask("SkyMask", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
	    LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
		Cull Off

		// CGINCLUDE
		// #pragma target 3.0
		// ENDCG
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
			};

			uniform sampler2D _MainTex;
			// uniform sampler2D _SkyMask;
			
			v2f vert ( appdata v )
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;

                // v2f o;
				// o.vertex = v.vertex;
				// o.vertex.xy *= 2;
				// o.uv = v.uv;
				// // o.uv.y = 1.0 - o.uv.y;
                // o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				// return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float3 WorldPosition = i.worldPos;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(WorldPosition);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 break35 = ase_worldViewDir;
				float2 appendResult15 = (float2(atan2( break35.x , break35.z ) , asin( break35.y )));
				float2 appendResult46 = (float2(3.14159265359f , ( 3.14159265359f * 0.5 )));
				float2 uv = ((( appendResult15 / appendResult46 ) + 1.0 ) * 0.5 );
				finalColor = tex2D( _MainTex, float2(uv.x, 1.0-uv.y));
                // finalColor.a = 1.0 - tex2D( _SkyMask, i.uv).r;
				return finalColor;

                // fixed4 finalColor;
                // finalColor = tex2D( _MainTex, i.uv);
                // // finalColor.a = 1.0 - tex2D( _SkyMask, i.uv).r;
				// return finalColor;
			}
			ENDCG
		}
	}
}