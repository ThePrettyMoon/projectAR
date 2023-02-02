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

uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD7;
void main ()
{
  lowp float tangentSign_1 = 0.0;
  lowp vec3 worldTangent_2 = vec3(0.0);
  lowp vec3 worldNormal_3 = vec3(0.0);
  highp vec2 tmpvar_4 = vec2(0.0);
  highp vec4 tmpvar_5 = vec4(0.0);
  highp vec4 tmpvar_6 = vec4(0.0);
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _glesVertex.xyz;
  highp vec3 tmpvar_7 = vec3(0.0);
  tmpvar_7 = (unity_ObjectToWorld * _glesVertex).xyz;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = unity_WorldToObject[0].xyz;
  tmpvar_8[1] = unity_WorldToObject[1].xyz;
  tmpvar_8[2] = unity_WorldToObject[2].xyz;
  highp vec3 tmpvar_9 = vec3(0.0);
  tmpvar_9 = normalize((_glesNormal * tmpvar_8));
  worldNormal_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
  highp vec3 tmpvar_11 = vec3(0.0);
  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_11;
  highp float tmpvar_12 = 0.0;
  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_12;
  lowp vec3 tmpvar_13 = vec3(0.0);
  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_14 = vec4(0.0);
  tmpvar_14.x = worldTangent_2.x;
  tmpvar_14.y = tmpvar_13.x;
  tmpvar_14.z = worldNormal_3.x;
  tmpvar_14.w = tmpvar_7.x;
  highp vec4 tmpvar_15 = vec4(0.0);
  tmpvar_15.x = worldTangent_2.y;
  tmpvar_15.y = tmpvar_13.y;
  tmpvar_15.z = worldNormal_3.y;
  tmpvar_15.w = tmpvar_7.y;
  highp vec4 tmpvar_16 = vec4(0.0);
  tmpvar_16.x = worldTangent_2.z;
  tmpvar_16.y = tmpvar_13.z;
  tmpvar_16.z = worldNormal_3.z;
  tmpvar_16.w = tmpvar_7.z;
  mediump vec3 normal_17 = vec3(0.0);
  normal_17 = worldNormal_3;
  mediump vec3 x1_18 = vec3(0.0);
  mediump vec4 tmpvar_19 = vec4(0.0);
  tmpvar_19 = (normal_17.xyzz * normal_17.yzzx);
  x1_18.x = dot (unity_SHBr, tmpvar_19);
  x1_18.y = dot (unity_SHBg, tmpvar_19);
  x1_18.z = dot (unity_SHBb, tmpvar_19);
  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_6));
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_14;
  xlv_TEXCOORD2 = tmpvar_15;
  xlv_TEXCOORD3 = tmpvar_16;
  xlv_TEXCOORD4 = (x1_18 + (unity_SHC.xyz * (
    (normal_17.x * normal_17.x)
   - 
    (normal_17.y * normal_17.y)
  )));
  xlv_TEXCOORD5 = tmpvar_4;
  xlv_TEXCOORD7 = tmpvar_5;
}


