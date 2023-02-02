
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

uniform highp mat4 mat_WorldToShadow[4];
uniform highp sampler2D t_shadowmap_texture;

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

#define unity_WorldToShadow mat_WorldToShadow
#define _ShadowMapTexture t_shadowmap_texture
#define unity_ShadowFadeCenterAndType vec4(0.0, 0.0, 0.0, 0.0)

// for forward pass
#define _LightShadowData vec4(0.5, 0.0, 0.0, 0.0)
// for shadow pass
#define unity_LightShadowBias vec4(0.005, 1, 0, 0)

#endif

//////////////////////////////////////////////////////////////////////////
// 
// --------------------------Fragment Include End-------------------------
// 
//////////////////////////////////////////////////////////////////////////

uniform mediump float _BumpScale;
uniform sampler2D _BumpMap;
uniform highp vec4 _BumpMap_ST;
uniform mediump vec4 _Color;
uniform sampler2D _MainTex;
uniform highp vec4 _MainTex_ST;
uniform sampler2D _EmissionMap;
uniform highp vec4 _EmissionMap_ST;
uniform mediump vec4 _EmissionColor;
uniform sampler2D _MetallicGlossMap;
uniform highp vec4 _MetallicGlossMap_ST;
uniform mediump float _Metallic;
uniform mediump float _GlossMapScale;
uniform sampler2D _OcclusionMap;
uniform highp vec4 _OcclusionMap_ST;
uniform mediump float _OcclusionStrength;
uniform lowp float _Cutoff;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
void main ()
{
  mediump vec3 tmpvar_1 = vec3(0.0);
  highp vec4 tmpvar_2 = vec4(0.0);
  mediump vec3 tmpvar_3 = vec3(0.0);
  mediump vec3 tmpvar_4 = vec3(0.0);
  lowp vec3 worldN_5 = vec3(0.0);
  lowp vec4 c_6 = vec4(0.0);
  mediump vec3 tmpvar_7 = vec3(0.0);
  mediump float tmpvar_8 = 0.0;
  lowp vec3 worldViewDir_9 = vec3(0.0);
  lowp vec3 lightDir_10 = vec3(0.0);
  highp vec3 tmpvar_11 = vec3(0.0);
  tmpvar_11.x = xlv_TEXCOORD1.w;
  tmpvar_11.y = xlv_TEXCOORD2.w;
  tmpvar_11.z = xlv_TEXCOORD3.w;
  mediump vec3 tmpvar_12 = vec3(0.0);
  tmpvar_12 = _WorldSpaceLightPos0.xyz;
  lightDir_10 = tmpvar_12;
  highp vec3 tmpvar_13 = vec3(0.0);
  tmpvar_13 = normalize((_WorldSpaceCameraPos - tmpvar_11));
  worldViewDir_9 = tmpvar_13;
  lowp vec3 tmpvar_14 = vec3(0.0);
  lowp vec3 tmpvar_15 = vec3(0.0);
  mediump float tmpvar_16 = 0.0;
  mediump float tmpvar_17 = 0.0;
  lowp float tmpvar_18 = 0.0;
  highp vec4 tex2DNode19_19 = vec4(0.0);
  highp vec4 tex2DNode14_20 = vec4(0.0);
  highp vec2 tmpvar_21 = vec2(0.0);
  tmpvar_21 = ((xlv_TEXCOORD0 * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  tmpvar_15 = (_BumpScale * ((texture2D (_BumpMap, tmpvar_21).xyz * 2.0) - 1.0));
  highp vec2 tmpvar_22 = vec2(0.0);
  tmpvar_22 = ((xlv_TEXCOORD0 * _MainTex_ST.xy) + _MainTex_ST.zw);
  lowp vec4 tmpvar_23 = vec4(0.0);
  tmpvar_23 = texture2D (_MainTex, tmpvar_22);
  tex2DNode14_20 = tmpvar_23;
  tmpvar_14 = (_Color * tex2DNode14_20).xyz;
  highp vec2 tmpvar_24 = vec2(0.0);
  tmpvar_24 = ((xlv_TEXCOORD0 * _EmissionMap_ST.xy) + _EmissionMap_ST.zw);
  lowp vec4 tmpvar_25 = vec4(0.0);
  tmpvar_25 = texture2D (_EmissionMap, tmpvar_24);
  highp vec2 tmpvar_26 = vec2(0.0);
  tmpvar_26 = ((xlv_TEXCOORD0 * _MetallicGlossMap_ST.xy) + _MetallicGlossMap_ST.zw);
  lowp vec4 tmpvar_27 = vec4(0.0);
  tmpvar_27 = texture2D (_MetallicGlossMap, tmpvar_26);
  tex2DNode19_19 = tmpvar_27;
  tmpvar_16 = (tex2DNode19_19.x * _Metallic);
  tmpvar_17 = (tex2DNode19_19.w * _GlossMapScale);
  highp vec2 tmpvar_28 = vec2(0.0);
  tmpvar_28 = ((xlv_TEXCOORD0 * _OcclusionMap_ST.xy) + _OcclusionMap_ST.zw);
  lowp vec4 tmpvar_29 = vec4(0.0);
  tmpvar_29 = texture2D (_OcclusionMap, tmpvar_28);
  tmpvar_18 = (_Color.w * tex2DNode14_20.w);
  tmpvar_7 = (tmpvar_25 * _EmissionColor).xyz;
  tmpvar_8 = ((tmpvar_29.y * _OcclusionStrength) + (1.0 - _OcclusionStrength));
  lowp float x_30 = 0.0;
  x_30 = (tmpvar_18 - _Cutoff);
  if ((x_30 < 0.0)) {
    discard;
  };
  highp float tmpvar_31 = 0.0;
  tmpvar_31 = dot (xlv_TEXCOORD1.xyz, tmpvar_15);
  worldN_5.x = tmpvar_31;
  highp float tmpvar_32 = 0.0;
  tmpvar_32 = dot (xlv_TEXCOORD2.xyz, tmpvar_15);
  worldN_5.y = tmpvar_32;
  highp float tmpvar_33 = 0.0;
  tmpvar_33 = dot (xlv_TEXCOORD3.xyz, tmpvar_15);
  worldN_5.z = tmpvar_33;
  tmpvar_3 = _LightColor0.xyz;
  tmpvar_4 = lightDir_10;
  tmpvar_1 = worldViewDir_9;
  tmpvar_2 = unity_SpecCube0_HDR;
  mediump vec3 Normal_34 = vec3(0.0);
  Normal_34 = worldN_5;
  mediump float tmpvar_35 = 0.0;
  tmpvar_35 = (1.0 - tmpvar_17);
  mediump vec3 I_36 = vec3(0.0);
  I_36 = -(tmpvar_1);
  mediump vec3 normalWorld_37 = vec3(0.0);
  normalWorld_37 = worldN_5;
  mediump vec4 tmpvar_38 = vec4(0.0);
  tmpvar_38.w = 1.0;
  tmpvar_38.xyz = normalWorld_37;
  mediump vec3 x_39 = vec3(0.0);
  x_39.x = dot (unity_SHAr, tmpvar_38);
  x_39.y = dot (unity_SHAg, tmpvar_38);
  x_39.z = dot (unity_SHAb, tmpvar_38);
  mediump vec4 hdr_40 = vec4(0.0);
  hdr_40 = tmpvar_2;
  mediump vec4 tmpvar_41 = vec4(0.0);
  tmpvar_41.xyz = (I_36 - (2.0 * (
    dot (Normal_34, I_36)
   * Normal_34)));
  tmpvar_41.w = ((tmpvar_35 * (1.7 - 
    (0.7 * tmpvar_35)
  )) * 6.0);
  lowp vec4 tmpvar_42 = vec4(0.0);
  tmpvar_42 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_41.xyz, tmpvar_41.w);
  mediump vec4 tmpvar_43 = vec4(0.0);
  tmpvar_43 = tmpvar_42;
  lowp vec3 tmpvar_44 = vec3(0.0);
  mediump vec3 viewDir_45 = vec3(0.0);
  viewDir_45 = worldViewDir_9;
  mediump vec4 c_46 = vec4(0.0);
  lowp vec3 tmpvar_47 = vec3(0.0);
  tmpvar_47 = normalize(worldN_5);
  mediump vec3 tmpvar_48 = vec3(0.0);
  mediump vec3 albedo_49 = vec3(0.0);
  albedo_49 = tmpvar_14;
  mediump vec3 tmpvar_50 = vec3(0.0);
  tmpvar_50 = mix (vec3(0.2209163, 0.2209163, 0.2209163), albedo_49, vec3(tmpvar_16));
  mediump float tmpvar_51 = 0.0;
  tmpvar_51 = (0.7790837 - (tmpvar_16 * 0.7790837));
  tmpvar_48 = (albedo_49 * tmpvar_51);
  tmpvar_44 = tmpvar_48;
  mediump vec3 diffColor_52 = vec3(0.0);
  diffColor_52 = tmpvar_44;
  mediump float alpha_53 = 0.0;
  alpha_53 = tmpvar_18;
  tmpvar_44 = diffColor_52;
  mediump vec3 diffColor_54 = vec3(0.0);
  diffColor_54 = tmpvar_44;
  mediump vec3 normal_55 = vec3(0.0);
  normal_55 = tmpvar_47;
  mediump vec3 tmpvar_56 = vec3(0.0);
  mediump vec3 inVec_57 = vec3(0.0);
  inVec_57 = (tmpvar_4 + viewDir_45);
  tmpvar_56 = (inVec_57 * inversesqrt(max (0.001, 
    dot (inVec_57, inVec_57)
  )));
  mediump float tmpvar_58 = 0.0;
  tmpvar_58 = clamp (dot (normal_55, tmpvar_56), 0.0, 1.0);
  mediump float tmpvar_59 = 0.0;
  tmpvar_59 = (1.0 - tmpvar_17);
  mediump float tmpvar_60 = 0.0;
  tmpvar_60 = (tmpvar_59 * tmpvar_59);
  mediump float x_61 = 0.0;
  x_61 = (1.0 - clamp (dot (normal_55, viewDir_45), 0.0, 1.0));
  mediump vec4 tmpvar_62 = vec4(0.0);
  tmpvar_62.w = 1.0;
  tmpvar_62.xyz = (((
    ((diffColor_54 + ((tmpvar_60 / 
      ((max (0.32, clamp (
        dot (tmpvar_4, tmpvar_56)
      , 0.0, 1.0)) * (1.5 + tmpvar_60)) * (((tmpvar_58 * tmpvar_58) * (
        (tmpvar_60 * tmpvar_60)
       - 1.0)) + 1.00001))
    ) * tmpvar_50)) * tmpvar_3)
   * 
    clamp (dot (normal_55, tmpvar_4), 0.0, 1.0)
  ) + (
    (max (((1.055 * 
      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD4 + x_39)), vec3(0.4166667, 0.4166667, 0.4166667))
    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_8)
   * diffColor_54)) + ((
    (1.0 - ((tmpvar_60 * tmpvar_59) * 0.28))
   * 
    (((hdr_40.x * (
      (hdr_40.w * (tmpvar_43.w - 1.0))
     + 1.0)) * tmpvar_43.xyz) * tmpvar_8)
  ) * mix (tmpvar_50, vec3(
    clamp ((tmpvar_17 + (1.0 - tmpvar_51)), 0.0, 1.0)
  ), vec3(
    ((x_61 * x_61) * (x_61 * x_61))
  ))));
  c_46.xyz = tmpvar_62.xyz;
  c_46.w = alpha_53;
  c_6 = c_46;
  c_6.xyz = (c_6.xyz + tmpvar_7);
  gl_FragData[0] = c_6;
}


