Shader "Unlit/Ring"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

        _OuterRadius("Outer Radius", Range(0, 0.5)) = 0.5
        _InnerRadius("Inner Radius", Range(0, 0.5)) = 0.2
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" "IngnoreProjector" = "True" "RenderType" = "Transparent" }

            Cull Off
            ZWrite Off
            //ZTest Off
            Lighting Off
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                //#pragma target 2.0

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    float2 texcoord  : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _Color;
                float4 _MainTex_ST;

                fixed _OuterRadius;
                fixed _InnerRadius;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                    //o.color = v.color * _Color;
                    return o;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    float4 color = (tex2D(_MainTex, IN.texcoord)) * _Color;

                    float dis = distance(IN.texcoord, float2(0.5, 0.5));
                    color.a = step(_InnerRadius, dis) * step(dis, _OuterRadius);

                    return color;
                }
            ENDCG
            }
        }
}

