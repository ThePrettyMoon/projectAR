// AUTO GENERATE OFF

//////////////////////////////////////////////////////////////////////////
// 
// --------------------------Vertex Include Begin-------------------------
// 
//////////////////////////////////////////////////////////////////////////

#ifdef __ANDROID__
    #define INSIGHT3D_ANDROID
#else
    #define INSIGHT3D_IOS
#endif
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

varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
uniform lowp vec4 _Color;
uniform highp vec4 _MainTex_ST;
void main ()
{
  lowp vec3 worldNormal_1 = vec3(0.0);
  mediump vec3 tmpvar_2 = vec3(0.0);
  mediump vec4 tmpvar_3 = vec4(0.0);
  highp vec2 tmpvar_4 = vec2(0.0);
  highp vec4 tmpvar_5 = vec4(0.0);
  tmpvar_5.zw = _glesVertex.zw;
  lowp vec4 tmpvar_6 = vec4(0.0);
  tmpvar_5.xy = _glesVertex.xy;
  tmpvar_6 = (_glesColor * _Color) ;
  tmpvar_3 = tmpvar_6;
  highp vec4 tmpvar_7 = vec4(0.0);
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = tmpvar_5.xyz;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = unity_WorldToObject[0].xyz;
  tmpvar_8[1] = unity_WorldToObject[1].xyz;
  tmpvar_8[2] = unity_WorldToObject[2].xyz;
  highp vec3 tmpvar_9 = vec3(0.0);
  tmpvar_9 = normalize((_glesNormal * tmpvar_8));
  worldNormal_1 = tmpvar_9;
  tmpvar_2 = worldNormal_1;
  mediump vec3 normal_10 = vec3(0.0);
  normal_10 = worldNormal_1;
  mediump vec4 tmpvar_11 = vec4(0.0);
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = normal_10;
  mediump vec3 res_12 = vec3(0.0);
  mediump vec3 x_13 = vec3(0.0);
  x_13.x = dot (unity_SHAr, tmpvar_11);
  x_13.y = dot (unity_SHAg, tmpvar_11);
  x_13.z = dot (unity_SHAb, tmpvar_11);
  mediump vec3 x1_14 = vec3(0.0);
  mediump vec4 tmpvar_15 = vec4(0.0);
  tmpvar_15 = (normal_10.xyzz * normal_10.yzzx);
  x1_14.x = dot (unity_SHBr, tmpvar_15);
  x1_14.y = dot (unity_SHBg, tmpvar_15);
  x1_14.z = dot (unity_SHBb, tmpvar_15);
  res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * 
    ((normal_10.x * normal_10.x) - (normal_10.y * normal_10.y))
  )));
  mediump vec3 tmpvar_16 = vec3(0.0);
  tmpvar_16 = max (((1.055 * 
    pow (max (res_12, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  res_12 = tmpvar_16;
  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_7));
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = (unity_ObjectToWorld * tmpvar_5).xyz;
  xlv_TEXCOORD3 = tmpvar_3;
  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), tmpvar_16);
  xlv_TEXCOORD5 = tmpvar_4;
}



