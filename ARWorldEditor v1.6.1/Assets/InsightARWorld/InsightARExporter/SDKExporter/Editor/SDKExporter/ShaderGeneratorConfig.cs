/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEngine;
using System.Collections;

namespace RenderEngine
{
	class ShaderGeneratorConfig
	{
        // This type is exactly the same as in Class 'ShaderUtil'. Listed there because the original one is private
        enum CompilerPlatformType
        {
            OpenGL,
            D3D9,
            Xbox360,
            PS3,
            D3D11,
            OpenGLES20,
            OpenGLES20Desktop,
            Flash,
            D3D11_9x,
            OpenGLES30,
            PSVita,
            PS4,
            XboxOne,
            PSM,
            Metal,
            OpenGLCore,
            N3DS,
            WiiU,
            Vulkan,
            Switch,
            Count
        };

        enum ComplierModeType
        {
            CurrentGraphicsDevice,
            CurrentBuildPlatform,
            AllPlatforms,
            Custom
        };


        public enum VariablesType
        {
            Attribute,
            Uniform,
            Varying,
            Const,
            Length

        };
        public static readonly string[] VariablesName =
        {
            "attribute",
            "uniform",
            "varying",
            "const"
        };


        public static readonly System.Collections.Generic.Dictionary<string, string> GLSLInitializeDict = new System.Collections.Generic.Dictionary<string, string>
        {
            {"float", " = 0.0"},
            {"vec2", " = vec2(0.0)"},
            {"vec3", " = vec3(0.0)"},
            {"vec4", " = vec4(0.0)"}
        };

        public enum ShaderType
        {
            Vertex = 0,
            Fragment = 1,
        }

        public const string GENERATE_TOGGLE = "// AUTO GENERATE OFF";
        public const int EXPORT_PLATFORMS_MASK = 1 << (int)CompilerPlatformType.OpenGLES20;
        public const int EXPORT_MODE = (int)ComplierModeType.AllPlatforms;
		public const string COMPILED_NAME_PREFIX = "Compiled-";
        public const string DEFAULT_JSON_FILE_PATH = "Json Shader (DO NOT DELETE)/";
		// public static string[] INCLUDE  =
        // {
		//     "#include \"amplify_standard_head_vs.sh\"\n#include \"amplify_compatible_unity_vs.sh\"\n",
        //     "#include \"amplify_standard_head_fs.sh\"\n#include \"amplify_compatible_unity_fs.sh\"\n",
        // };

        public static string[] INCLUDE_DEFINATION =
        {
            // ----------VS----------
@"
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

uniform highp mat4 mat_WorldToShadow[4];

uniform vec4 u_projection_params;
uniform vec4 u_screen_params;

",

// ----------FS----------
@"
//////////////////////////////////////////////////////////////////////////
//
// --------------------------Fragment Include Begin-----------------------
//
//////////////////////////////////////////////////////////////////////////

#ifdef GL_EXT_shader_texture_lod
#extension GL_EXT_shader_texture_lod : enable
#endif

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
uniform vec4 u_sin_time;
uniform vec4 u_time;

uniform vec4 u_light0_pos_world;
uniform vec4 u_light0_color;
uniform vec4 u_light0_shadow_data;

uniform highp mat4 mat_WorldToShadow[4];
uniform highp sampler2D t_shadowmap_texture;
uniform highp vec4 u_shadow_fade_center_and_type;

uniform vec4 u_projection_params;
uniform vec4 u_screen_params;

",
        };


        public static string[] INCLUDE_COMPATIBLE =
        {
            // ----------VS----------
            // 如果修改了本文件中的变量，请务必在UtilityShaderGenerator.SHADER_EXCLUDED_UNIFORMS中添加相应的变量名！
@"#ifndef _COMPATIBLE_UNITY_VS_
#define _COMPATIBLE_UNITY_VS_

#define in_TANGENT0 a_tangent
#define in_POSITION0 vec4( a_position, 1.0 )
#define in_NORMAL0 a_normal
#define in_COLOR0 a_color0
#define in_TEXCOORD0 a_texcoord0
#define in_TEXCOORD1 a_texcoord1

#define unity_SHAr (u_sphere_harmonic[0])
#define unity_SHAg (u_sphere_harmonic[1])
#define unity_SHAb (u_sphere_harmonic[2])
#define unity_SHBr (u_sphere_harmonic[3])
#define unity_SHBg (u_sphere_harmonic[4])
#define unity_SHBb (u_sphere_harmonic[5])
#define unity_SHC (u_sphere_harmonic[6])

#ifndef SKIN_SHADER
    #define hlslcc_mtx4x4unity_ObjectToWorld mat_ObjectToWorld
#endif

#define hlslcc_mtx4x4unity_WorldToObject mat_WorldToObject
// defined in UnityShaderVaribles.cginc.
// w is usually 1.0, or -1.0 for odd-negative scale transforms
#define unity_WorldTransformParams vec4(0, 0, 0, 1)
#define hlslcc_mtx4x4unity_MatrixVP mat_MatrixVP
#define hlslcc_mtx4x4unity_MatrixV mat_MatrixView
#define hlslcc_mtx4x4glstate_matrix_projection  mat_MatrixProjection
#define _WorldSpaceCameraPos u_world_space_camera_pos

#define _CosTime (u_cos_time)
#define _SinTime (u_sin_time)
#define _Time (u_time.wwww)

#define _WorldSpaceLightPos0 u_light0_pos_world
#define _LightColor0 u_light0_color

#define hlslcc_mtx4x4unity_WorldToShadow mat_WorldToShadow[0]

// for shadow pass
#define unity_LightShadowBias vec4(0.005, 1, 0, 0)

#define _ProjectionParams u_projection_params
#define _ScreenParams u_screen_params

#endif

//////////////////////////////////////////////////////////////////////////
//
// --------------------------Vertex Include End---------------------------
//
//////////////////////////////////////////////////////////////////////////
",

    // ----------FS----------
    // 如果修改了本文件中的变量，请务必在UtilityShaderGenerator.SHADER_EXCLUDED_UNIFORMS中添加相应的变量名！
@"#ifndef _COMPATIBLE_UNITY_FS_
#define _COMPATIBLE_UNITY_FS_

#ifndef SKIN_SHADER
    #define hlslcc_mtx4x4unity_ObjectToWorld mat_ObjectToWorld
#endif

#define hlslcc_mtx4x4unity_WorldToObject mat_WorldToObject
// defined in UnityShaderVaribles.cginc.
// w is usually 1.0, or -1.0 for odd-negative scale transforms
#define unity_WorldTransformParams vec4(0, 0, 0, 1)
#define hlslcc_mtx4x4unity_MatrixVP mat_MatrixVP
#define hlslcc_mtx4x4unity_MatrixV mat_MatrixView
#define hlslcc_mtx4x4glstate_matrix_projection  mat_MatrixProjection
#define _WorldSpaceCameraPos u_world_space_camera_pos

#define _WorldSpaceLightPos0 u_light0_pos_world
#define _LightColor0 u_light0_color

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
// #define unity_SpecCube0_HDR vec4(4.594794, 1, 0, 0)
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

#define hlslcc_mtx4x4unity_WorldToShadow mat_WorldToShadow[0]
#define _ShadowMapTexture t_shadowmap_texture
#define unity_ShadowFadeCenterAndType u_shadow_fade_center_and_type

// for forward pass
#define _LightShadowData u_light0_shadow_data

// for shadow pass
#define unity_LightShadowBias vec4(0.005, 1, 0, 0)

#define _ProjectionParams u_projection_params
#define _ScreenParams u_screen_params

#endif

//////////////////////////////////////////////////////////////////////////
//
// --------------------------Fragment Include End-------------------------
//
//////////////////////////////////////////////////////////////////////////
",
        };

        public static string SHADOWCASTER_CODE = 
        @"#ifdef SHADOWS_DEPTH

#ifdef VERTEX

#ifndef INSIGHT3D_ANDROID
    #define INSIGHT3D_IOS
#endif

#define saturate(a) clamp( a, 0.0, 1.0 )

attribute vec3 a_position;
attribute vec4 a_normal;

uniform mat4 mat_ObjectToWorld; //modelMatrix
uniform mat4 mat_WorldToObject;
uniform mat4 mat_MatrixVP;
uniform vec3 u_world_space_camera_pos;//cameraPosition
uniform vec4 u_light0_pos_world;

#define unity_LightShadowBiasX 0.04
#define unity_LightShadowBiasY 1.0
#define unity_LightShadowBiasZpart1 0.00078125 // normalBias / shadowmapSize
#define unity_LightShadowBiasZpart2 0.00390625 // normalBias / shadowmapSize * shadowDis / 2

void main() {

    vec4 wPos = mat_ObjectToWorld * vec4( a_position , 1.0 );

    float biasZ = unity_LightShadowBiasZpart1 * ( 1.0 / mat_MatrixVP[0].x) + unity_LightShadowBiasZpart2;
    if (biasZ != 0.0)
    {
        vec3 wNormal = normalize(a_normal * mat_WorldToObject).xyz;
        vec3 wLight = normalize(u_light0_pos_world.xyz);

        // apply normal offset bias (inset position along the normal)
        // bias needs to be scaled by sine between normal and light direction
        // (http://the-witness.net/news/2013/09/shadow-mapping-summary-part-1/)
        //
        // unity_LightShadowBias.z contains user-specified normal offset amount
        // scaled by world space texel size.

        float shadowCos = dot(wNormal, wLight);
        float shadowSine = sqrt(1.0 - shadowCos * shadowCos);
        float normalBias = biasZ * shadowSine;

        wPos.xyz -= wNormal * normalBias;
    } 

    vec4 pos = mat_MatrixVP * wPos;

	// pos.z += saturate(unity_LightShadowBias.x/pos.w);
	pos.z += saturate(mat_MatrixVP[0].x * unity_LightShadowBiasX/pos.w);
    float clamped = max(pos.z, -pos.w);
    pos.z = mix(pos.z, clamped, unity_LightShadowBiasY);
    gl_Position = pos;
}

#endif

#ifdef FRAGMENT
void main () {
    gl_FragData[0] = vec4(0.0);
}
#endif

#endif";
        
    }

}
