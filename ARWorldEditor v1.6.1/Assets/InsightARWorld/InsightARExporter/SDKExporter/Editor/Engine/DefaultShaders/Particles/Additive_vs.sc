// AUTO GENERATE OFF
attribute vec3 a_position;
attribute vec2 a_texcoord0;
attribute vec4 a_color0;

uniform highp mat4 mat_ObjectToWorld;
uniform highp mat4 mat_MatrixVP;
varying vec4 v_texcoord0;
varying vec4 v_vertex_color0;

void main()
{
gl_Position = mat_MatrixVP * mat_ObjectToWorld * vec4(a_position, 1);
v_texcoord0 = vec4( a_texcoord0 , 0,1 );
//v_texcoord0.y = 1.0 - a_texcoord0.y;
v_vertex_color0 = a_color0;
}

