
Shader "ARBackgroundNew"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _UvTopLeftRight ("UV of top corners", Vector) = (0, 1, 1, 1)
        _UvBottomLeftRight ("UV of bottom corners", Vector) = (0 , 0, 1, 0)
    }

    // For GLES3 or GLES2 on device
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry-100" }
        Pass
        {
            ZWrite Off
            Lighting Off
            // ColorMask RGB
            // Cull Off

            GLSLPROGRAM

            #pragma only_renderers gles3 gles

            #include "UnityCG.glslinc"

            uniform vec4 _UvTopLeftRight;
            uniform vec4 _UvBottomLeftRight;

            #ifdef VERTEX

            varying vec2 textureCoord;

            void main()
            {
                vec2 uvTop = mix(_UvTopLeftRight.xy,
                                 _UvTopLeftRight.zw,
                                 gl_MultiTexCoord0.x);
                vec2 uvBottom = mix(_UvBottomLeftRight.xy,
                                    _UvBottomLeftRight.zw,
                                    gl_MultiTexCoord0.x);
                textureCoord = mix(uvTop, uvBottom, gl_MultiTexCoord0.y);

                // textureCoord = vec2(gl_MultiTexCoord0.y, 1.0f - gl_MultiTexCoord0.x);

                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                // gl_Position = ftransform();
            }

            #endif

            #ifdef FRAGMENT
            varying vec2 textureCoord;
            uniform sampler2D _MainTex;

            void main()
            {
                vec3 mainTexColor;
                mainTexColor = texture(_MainTex, textureCoord).rgb;
                // gl_FragColor = texture(_MainTex, textureCoord);
                gl_FragColor = vec4(mainTexColor, 1.0);
                // gl_FragColor = vec4(0.0,1.0,1.0, 1.0);
            }

            #endif

            ENDGLSL
        }
    }

    FallBack Off
}
