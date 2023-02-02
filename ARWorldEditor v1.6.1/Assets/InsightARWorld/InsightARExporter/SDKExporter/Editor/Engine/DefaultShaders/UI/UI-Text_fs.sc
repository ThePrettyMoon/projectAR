// AUTO GENERATE OFF

//////////////////////////////////////////////////////////////////////////
// 
// --------------------------Fragment Include Begin-----------------------
// 
//////////////////////////////////////////////////////////////////////////

#extension GL_EXT_shader_texture_lod : enable
    #ifdef __ANDROID__
    #extension GL_OES_EGL_image_external : require
#endif
precision highp float;
#ifdef __ANDROID__
    #define INSIGHT3D_ANDROID
#else
    #define INSIGHT3D_IOS
#endif

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

varying lowp vec4 xlv_COLOR;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;

uniform sampler2D _MainTex;

void main ()
{
    lowp vec4 color = texture2D (_MainTex, xlv_TEXCOORD0);

    lowp float flag = 1.0 - floor(color.x * color.y * color.z);
    color.x = color.x * max(flag, xlv_COLOR.x);
    color.y = color.y * max(flag, xlv_COLOR.y);
    color.z = color.z * max(flag, xlv_COLOR.z);
    color.w = color.w * xlv_COLOR.w;

    gl_FragData[0] = color;
}

