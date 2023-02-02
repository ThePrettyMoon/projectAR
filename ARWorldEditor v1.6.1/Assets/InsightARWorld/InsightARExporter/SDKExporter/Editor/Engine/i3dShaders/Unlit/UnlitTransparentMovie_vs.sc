// AUTO GENERATE OFF
//////////////////////////////////////////////////////////////////////////
// 
// --------------------------Vertex Include Begin-------------------------
// 
//////////////////////////////////////////////////////////////////////////

#ifndef INSIGHT3D_ANDROID
    #define INSIGHT3D_IOS
#endif

// You can use the buildin macro 'INSIGHT3D_ANDROID' or 'INSIGHT3D_IOS' to verify different platforms.

attribute vec3 a_position;
attribute vec2 a_texcoord0;
attribute vec2 a_texcoord1;
attribute vec4 a_normal;
attribute vec4 a_tangent;
#ifdef SKIN_SHADER
    attribute vec4 a_weight;
    attribute vec4 a_indices;
#endif
attribute vec4 a_color0;
attribute vec4 a_color1;

#define SHADER_MAX_BONE 50
uniform highp mat4 mat_bones[SHADER_MAX_BONE];

uniform vec4 u_sphere_harmonic[7]; // Ar, Ag, Ab, Br, Bg, Bb, C

uniform highp mat4 mat_ObjectToWorld;
uniform highp mat4 mat_WorldToObject;
uniform highp mat4 mat_MatrixVP;
uniform highp mat4 mat_MatrixView;
uniform highp mat4 mat_MatrixProjection;
uniform vec3 u_world_space_camera_pos;

uniform vec4 u_cos_time;
uniform vec4 u_sin_time;
uniform vec4 u_time;

uniform vec4 u_light0_pos_world;
uniform vec4 u_light0_color;

// 如果修改了本文件中的变量，请务必在UtilityShaderGenerator.SHADER_EXCLUDED_UNIFORMS中添加相应的变量名！

#ifndef _COMPATIBLE_UNITY_VS_
#define _COMPATIBLE_UNITY_VS_

#define _glesTANGENT a_tangent
#define _glesVertex vec4( a_position , 1.0 )
#define _glesNormal a_normal.xyz
#define _glesColor a_color0
#define _glesMultiTexCoord0 vec4( a_texcoord0.x, a_texcoord0.y, 0, 0 )
#define _glesMultiTexCoord1 vec4( a_texcoord1.x, a_texcoord1.y, 0, 0 )

#define unity_SHAr (u_sphere_harmonic[0])
#define unity_SHAg (u_sphere_harmonic[1])
#define unity_SHAb (u_sphere_harmonic[2])
#define unity_SHBr (u_sphere_harmonic[3])
#define unity_SHBg (u_sphere_harmonic[4])
#define unity_SHBb (u_sphere_harmonic[5])
#define unity_SHC (u_sphere_harmonic[6])

#ifndef SKIN_SHADER
    #define unity_ObjectToWorld mat_ObjectToWorld
#endif

#define unity_WorldToObject mat_WorldToObject
// defined in UnityShaderVaribles.cginc.
// w is usually 1.0, or -1.0 for odd-negative scale transforms
#define unity_WorldTransformParams vec4(0, 0, 0, 1)
#define unity_MatrixVP mat_MatrixVP
#define unity_MatrixV mat_MatrixView
#define glstate_matrix_projection  mat_MatrixProjection
#define _WorldSpaceCameraPos u_world_space_camera_pos

#define _CosTime (u_cos_time)
#define _SinTime (u_sin_time)
#define _Time (u_time.wwww)

#define _WorldSpaceLightPos0 u_light0_pos_world
#define _LightColor0 (u_light0_color.rgb)

#endif

//////////////////////////////////////////////////////////////////////////
// 
// --------------------------Vertex Include End---------------------------
// 
//////////////////////////////////////////////////////////////////////////

uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1 = vec4(0.0);
  tmpvar_1.w = 1.0;
  tmpvar_1.xyz = _glesVertex.xyz;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_1));
}


