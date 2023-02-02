// AUTO GENERATE OFF
//////////////////////////////////////////////////////////////////////////
//
// --------------------------Fragment Include Begin-----------------------
//
//////////////////////////////////////////////////////////////////////////

#extension GL_EXT_shader_texture_lod : enable
#ifdef INSIGHT3D_ANDROID
    #extension GL_OES_EGL_image_external : require
#else
	#define INSIGHT3D_IOS
#endif
precision highp float;

uniform samplerCube t_skybox_texture_cube;
uniform sampler2D t_skybox_texture;

uniform samplerCube t_reflection_prob0_cube;
uniform vec4 u_reflection_prob0_min;
uniform vec4 u_reflection_prob0_max;
uniform vec4 u_reflection_prob0_pos;
uniform vec4 u_reflection_prob0_hdr;

uniform samplerCube t_reflection_prob1_cube;
uniform vec4 u_reflection_prob1_min;
uniform vec4 u_reflection_prob1_max;
uniform vec4 u_reflection_prob1_pos;
uniform vec4 u_reflection_prob1_hdr;

uniform vec4 u_sphere_harmonic[7]; // Ar, Ag, Ab, Br, Bg, Bb, C

uniform highp mat4 mat_ObjectToWorld;
uniform highp mat4 mat_WorldToObject;
uniform highp mat4 mat_MatrixVP;
uniform highp mat4 mat_MatrixView;
uniform highp mat4 mat_MatrixProjection;
uniform vec3 u_world_space_camera_pos;

uniform vec4 u_cos_time;
vec4 u_sin_time;
uniform vec4 u_time;

uniform vec4 u_light0_pos_world;
uniform vec4 u_light0_color;

// 如果修改了本文件中的变量，请务必在UtilityShaderGenerator.SHADER_EXCLUDED_UNIFORMS中添加相应的变量名！

#ifndef _COMPATIBLE_UNITY_FS_
#define _COMPATIBLE_UNITY_FS_

// USE: tmpvar_31 = impl_low_texture2DLodEXT (unity_SpecCube0, sVec3WorldToTexcoord(tmpvar_30.xyz), tmpvar_30.w);
lowp vec4 impl_low_texture2DLodEXT(lowp sampler2D sampler, highp vec2 coord, mediump float lod)
{
#if defined(GL_EXT_shader_texture_lod)
    return texture2DLodEXT(sampler, coord, lod);
#else
    return texture2D(sampler, coord, lod);
#endif
}

lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
{
#if defined(GL_EXT_shader_texture_lod)
    return textureCubeLodEXT(sampler, coord, lod);
#else
    return textureCube(sampler, coord, lod);
#endif
}

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

#define _WorldSpaceLightPos0 u_light0_pos_world
#define _LightColor0 (u_light0_color.rgb)

#define unity_SHAr (u_sphere_harmonic[0])
#define unity_SHAg (u_sphere_harmonic[1])
#define unity_SHAb (u_sphere_harmonic[2])
#define unity_SHBr (u_sphere_harmonic[3])
#define unity_SHBg (u_sphere_harmonic[4])
#define unity_SHBb (u_sphere_harmonic[5])
#define unity_SHC (u_sphere_harmonic[6])

#define unity_SpecCube0 t_reflection_prob0_cube
#define unity_SpecCube0_BoxMax u_reflection_prob0_max
#define unity_SpecCube0_BoxMin u_reflection_prob0_min
#define unity_SpecCube0_ProbePosition u_reflection_prob0_pos
#define unity_SpecCube0_HDR u_reflection_prob0_hdr

#define unity_SpecCube1 t_reflection_prob1_cube
#define unity_SpecCube1_BoxMax u_reflection_prob1_max
#define unity_SpecCube1_BoxMin u_reflection_prob1_min
#define unity_SpecCube1_ProbePosition u_reflection_prob1_pos
#define unity_SpecCube1_HDR u_reflection_prob1_hdr

#define _CosTime (u_cos_time)
#define _SinTime (u_sin_time)
#define _Time (u_time.wwww)

#define _ZBufferParams vec4(1,0,0,0)

#endif

//////////////////////////////////////////////////////////////////////////
//
// --------------------------Fragment Include End-------------------------
//
//////////////////////////////////////////////////////////////////////////

#if defined( INSIGHT3D_ANDROID )
uniform samplerExternalOES _MainTex;
#elif defined( INSIGHT3D_IOS )
uniform sampler2D _MainTex;

#else
#error Please define INSIGHT3D_ANDROID or INSIGHT3D_IOS.
#endif

uniform highp vec4 _KeyColor;
uniform highp vec4 _ReplacedColor;
uniform highp float _LowerThreshold1;
uniform highp float _HigherThreshold1;
uniform highp float _LowerThreshold2;
uniform highp float _HigherThreshold2;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 col_1 = vec4(0.0);
  lowp vec4 tmpvar_2 = vec4(0.0);

    #if defined( INSIGHT3D_ANDROID )
        tmpvar_2 = texture2D( _MainTex , xlv_TEXCOORD0 );
    #elif defined( INSIGHT3D_IOS )
        tmpvar_2 = texture2D( _MainTex , vec2( xlv_TEXCOORD0.x, 1.0 - xlv_TEXCOORD0.y ));
	#else
	#error Please define INSIGHT3D_ANDROID or INSIGHT3D_IOS.
	#endif

  lowp vec3 tmpvar_3 = vec3(0.0);
  tmpvar_3 = tmpvar_2.xyz;
  highp vec3 rgb_4 = vec3(0.0);
  rgb_4 = tmpvar_3;
  highp float tmpvar_5 = 0.0;
  highp vec2 x_6 = vec2(0.0);
  x_6 = (((mat3(0.299, -0.169, 0.5, 0.587, -0.331, -0.419, 0.114, 0.5, -0.081) * rgb_4) + vec3(0.0, 0.5, 0.5)).yz - ((mat3(0.299, -0.169, 0.5, 0.587, -0.331, -0.419, 0.114, 0.5, -0.081) * _KeyColor.xyz) + vec3(0.0, 0.5, 0.5)).yz);
  tmpvar_5 = sqrt(dot (x_6, x_6));
  highp float tmpvar_7 = 0.0;
  highp float tmpvar_8 = 0.0;
  tmpvar_8 = clamp (((tmpvar_5 - _LowerThreshold1) / (_HigherThreshold1 - _LowerThreshold1)), 0.0, 1.0);
  tmpvar_7 = (tmpvar_8 * (tmpvar_8 * (3.0 -
    (2.0 * tmpvar_8)
  )));
  highp float tmpvar_9 = 0.0;
  tmpvar_9 = clamp (((tmpvar_5 - _LowerThreshold2) / (_HigherThreshold2 - _LowerThreshold2)), 0.0, 1.0);
  highp vec4 tmpvar_10 = vec4(0.0);
  tmpvar_10 = mix (_ReplacedColor, tmpvar_2, vec4((tmpvar_9 * (tmpvar_9 *
    (3.0 - (2.0 * tmpvar_9))
  ))));
  col_1 = tmpvar_10;
  col_1.w = (col_1.w * tmpvar_7);
  gl_FragData[0] = col_1;
}
