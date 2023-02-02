// AUTO GENERATE OFF
precision highp float;
uniform sampler2D _MainTex;
varying vec4 v_texcoord0;
varying vec4 v_vertex_color0;
void main()
{
    gl_FragColor = v_vertex_color0 * texture2D(_MainTex, v_texcoord0.xy);
}

