// AUTO GENERATE OFF
Shader "i3dEngine/SpecialStandard"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		[Normal] _BumpMap("BumpMap", 2D) = "bump" {}
		_BumpScale("BumpScale", Float) = 1
		_MetallicGlossMap("MetallicGlossMap", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_GlossMapScale("GlossMapScale", Range( 0 , 1)) = 0.5
		_OcclusionMap("OcclusionMap", 2D) = "white" {}
		_OcclusionStrength("OcclusionStrength", Range( 0 , 1)) = 1
		_EmissionMap("EmissionMap", 2D) = "black" {}
		_EmissionColor("EmissionColor", Color) = (0,0,0,0)

		_Cutoff("CutOff", Range( 0 , 1)) = 0.5
		_transparency("Transparency", Range(0, 1)) = 0.5

		refractionRatio("RefractionRatio", Range(0, 1)) = 0.77
		reflectivity("Reflectivity", Range(0, 1)) = 0.5

		// clear coat
		clearcoat("ClearCoat", Float) = 0
		clearcoatRoughness("ClearCoat Roughness", Range(0,1)) = 0.5
		[Normal] clearcoatNormalMap("ClearCoatBump", 2D) = "bump" {}
		clearcoatNormalScale("ClearCoatBumpScale", Float) = 1

		// Rendering state
        [HideInInspector] _RenderMode ("__rendermode", Float) = 0.0
		[HideInInspector] _BlendMode ("__blendmode", Float) = 0.0
        [HideInInspector] _SrcBlend ("__src", Float) = 1.0
        [HideInInspector] _DstBlend ("__dst", Float) = 0.0
        [HideInInspector] _ZWrite ("__zw", Float) = 1.0

		// cull
		[HideInInspector] _Cull ("__cull", Float) = 2
		
		// shadow cull
		[HideInInspector] _ShadowCull ("__shadowcull", Float) = 0

	}

	SubShader
	{

		Pass {

			Name "ForwardBase"

			Tags{ "LightMode"="ForwardBase" "RenderType" = "Opaque" "Queue" = "Geometry+0" }

			Cull [_Cull]

			Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

			CGPROGRAM

			#pragma target 3.0

			// --------------------------------------------------------------------------------------------
			// ------------------------------------------ Define ------------------------------------------
			// --------------------------------------------------------------------------------------------
			#define HIGH_PRECISION

			#define GAMMA_FACTOR 2.2
			#define FRONT_FACING 1.0

			#define SHADOW_RADIUS 2.0

			#pragma shader_feature SHADOWS_SCREEN
			#pragma shader_feature SHADOWS_SOFT

			#pragma shader_feature_local USE_MAP
			#pragma shader_feature_local USE_NORMALMAP
			#pragma shader_feature_local USE_ROUGHNESSMAP
			#pragma shader_feature_local USE_METALNESSMAP
			#pragma shader_feature_local USE_EMISSION
			#pragma shader_feature_local USE_EMISSIVEMAP
			#pragma shader_feature_local USE_AOMAP
			#pragma shader_feature_local TRANSPARENCY
			#pragma shader_feature_local ALPHATEST
			#pragma shader_feature_local SHADOWMAP_PCF_SOFT
			#pragma shader_feature_local ENVMAP_MODE_REFRACTION
			#pragma shader_feature_local CLEARCOAT
			#pragma shader_feature_local USE_CLEARCOAT_NORMALMAP

			// #define PHYSICAL
			// #define USE_FOG
			// #define FOG_EXP2
			// #define USE_MAP

			#define USE_ENVMAP
			#define ENVMAP_TYPE_CUBE
			#define ENVMAP_MODE_REFLECTION
			#define ENVMAP_BLENDING_MULTIPLY

			// #define USE_LIGHTMAP
			// #define USE_AOMAP
			// #define USE_EMISSIVEMAP
			// #define USE_BUMPMAP
			// #define USE_NORMALMAP
			// #define OBJECTSPACE_NORMALMAP
			#define TANGENTSPACE_NORMALMAP

			// #define USE_CLEARCOAT_NORMALMAP
			// #define USE_SPECULARMAP
			// #define USE_ROUGHNESSMAP
			// #define USE_METALNESSMAP
			// #define USE_ALPHAMAP
			#define USE_TANGENT
			// #define USE_COLOR
			#define USE_UV
			// #define FLAT_SHADED
			// #define USE_SKINNING
			// #define BONE_TEXTURE
			// #define USE_MORPHTARGETS
			// #define USE_MORPHNORMALS
			// #define DOUBLE_SIDED
			// #define FLIP_SIDED

			#if SHADOWS_SCREEN
				#define USE_SHADOWMAP
			#endif
			#if SHADOWS_SOFT
				#if SHADOWMAP_PCF_SOFT
					#define SHADOWMAP_TYPE_PCF_SOFT
				#else
					#define SHADOWMAP_TYPE_PCF
				#endif
			#endif
			// #define USE_SIZEATTENUATION
			// #define USE_LOGDEPTHBUF
			// #define USE_LOGDEPTHBUF_EXT

			// #ifdef VERTEX
			// #define VERTEX_TEXTURES
			// #define MAX_BONES 50
			// #define USE_DISPLACEMENTMAP
			// #endif

			// #ifdef FRAGMENT
			// #define ALPHATEST
			// #define USE_MATCAP
			// #define USE_SHEEN
			// #endif

			#ifdef PHYSICAL
				#define REFLECTIVITY
				#define CLEARCOAT
				// #define TRANSPARENCY
			#endif

			#define NUM_DIR_LIGHTS 1
			#define DIRECTIONAL_LIGHT_SHADOW 1
			#define NUM_SPOT_LIGHTS 0
			#define NUM_RECT_AREA_LIGHTS 0
			#define NUM_POINT_LIGHTS 0
			#define NUM_HEMI_LIGHTS 0

			#define NUM_DIR_LIGHT_SHADOWS 1
			#define NUM_SPOT_LIGHT_SHADOWS 0
			#define NUM_POINT_LIGHT_SHADOWS 0

			#define NUM_CLIPPING_PLANES 0
			#define UNION_CLIPPING_PLANES 0
						
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityShaderVariables.cginc"
			#include "i3d_common.cginc"

			// -----------------------------------------------------------------------------------------------------
			// ------------------------------------------ uniform ------------------------------------------
			// -----------------------------------------------------------------------------------------------------

			fixed4 _LightColor0;

			uniform half _BumpScale;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform half4 _Color;
			uniform half _Cutoff;
			#ifdef USE_EMISSION
				uniform half4 _EmissionColor;
			#endif
			uniform half _GlossMapScale;
			uniform half _Metallic;

			#ifdef USE_MAP
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			#endif

			#ifdef USE_ALPHAMAP
				uniform sampler2D alphaMap;
			#endif
			uniform sampler2D _MetallicGlossMap;
			uniform float4 _MetallicGlossMap_ST;

			#ifdef USE_AOMAP
				uniform sampler2D _OcclusionMap;
				uniform float4 _OcclusionMap_ST;
				uniform half _OcclusionStrength;
			#endif

			#ifdef USE_LIGHTMAP
				uniform sampler2D lightMap;
				uniform float lightMapIntensity;
			#endif

			#ifdef USE_EMISSIVEMAP
				uniform sampler2D _EmissionMap; //emissiveMap
				uniform float4 _EmissionMap_ST;
			#endif

			#ifdef USE_SHADOWMAP
				uniform sampler2D _ShadowMapTexture;
				uniform float4 _ShadowMapTexture_TexelSize;
			#endif

			#ifdef TRANSPARENCY
				uniform float _transparency;
			#endif

			#if ENVMAP_MODE_REFRACTION
				#define REFLECTIVITY
			#endif

			#ifdef REFLECTIVITY
				uniform float reflectivity;
			#endif

			#ifdef CLEARCOAT
				uniform float clearcoat;
				uniform float clearcoatRoughness;
			#endif

			#ifdef USE_SHEEN
				uniform float3 sheen;
			#endif

			#ifdef USE_ENVMAP
				// uniform float envMapIntensity;
				#define envMapIntensity 1.0
				// uniform float flipEnvMap;
				#define flipEnvMap 1.0
				// uniform int maxMipLevel;
				#define maxMipLevel 7
				
			#endif

			#ifdef USE_FOG

				uniform fixed3 fogColor;

				#ifdef FOG_EXP2

					uniform float fogDensity;

				#else

					uniform float fogNear;
					uniform float fogFar;

				#endif

			#endif

			#ifdef USE_BUMPMAP
			    uniform sampler2D bumpMap;
    			uniform float bumpScale;
			#endif

			#ifdef USE_CLEARCOAT_NORMALMAP
				uniform sampler2D clearcoatNormalMap;
				uniform half clearcoatNormalScale;
			#endif

			struct PhysicalMaterial {

				fixed3    diffuseColor;
				float    specularRoughness;
				fixed3    specularColor;

			#ifdef CLEARCOAT
				float clearcoat;
				float clearcoatRoughness;
			#endif
			#ifdef USE_SHEEN
				fixed3 sheenColor;
			#endif

			};

			struct IncidentLight {
				float3 color;
				float3 direction;
				bool visible;
			};

			struct ReflectedLight {
				float3 directDiffuse;
				float3 directSpecular;
				float3 indirectDiffuse;
				float3 indirectSpecular;
			};

			struct GeometricContext {
				float3 position;
				float3 normal;
				float3 viewDir;
			#ifdef CLEARCOAT
				float3 clearcoatNormal;
			#endif
			};

			#define MAXIMUM_SPECULAR_COEFFICIENT 0.16
			#define DEFAULT_SPECULAR_COEFFICIENT 0.04

			// -----------------------------------------------------------------------------------------------------
			// ------------------------------------------ attribute varying ------------------------------------------
			// -----------------------------------------------------------------------------------------------------

			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;

				#if defined( USE_LIGHTMAP ) || defined( USE_AOMAP )
					float4 texcoord1 : TEXCOORD1;
				#endif

				#ifdef USE_TANGENT
					float4 tangent : TANGENT;
				#endif

				#ifdef USE_COLOR
					fixed4 color : COLOR;
				#endif
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD1;

				#ifndef FLAT_SHADED
					float3 normal : TEXCOORD2;
				
					#ifdef USE_TANGENT
					float3 tangent : TEXCOORD3;
					float3 bitangent : TEXCOORD4;
					#endif
				#endif
				
				#ifdef USE_COLOR
					fixed4 color : TEXCOORD5;
				#endif

				float3 viewPosition : TEXCOORD6; // #ifdef USE_FOG viewPosition.w = fogDepth

				#ifdef USE_SHADOWMAP
					float4 shadowCoord : TEXCOORD7;
				#endif
			};

			// -----------------------------------------------------------------------------------------------------
			// ------------------------------------------ vertex shader ------------------------------------------
			// -----------------------------------------------------------------------------------------------------

			v2f vert(a2v v) {
				v2f o;

				// float4x4 normalMatrix = UNITY_MATRIX_MV;

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ uv_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
				#ifdef USE_UV
					o.uv.xy = v.texcoord.xy;
				#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ uv2_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
				#if defined( USE_LIGHTMAP ) || defined( USE_AOMAP )
					o.uv.zw = v.texcoord1.xy;
				#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ color_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
				#ifdef USE_COLOR
					o.color = a_color0.xyz;
				#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ beginnormal_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
				float3 objectNormal = float3( v.normal );
				#ifdef USE_TANGENT
					float3 objectTangent = float3( v.tangent.xyz );
				#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ morphnormal_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
				#ifdef USE_MORPHNORMALS

					objectNormal += ( morphNormal0 - a_normal ) * morphTargetInfluences[ 0 ];
					objectNormal += ( morphNormal1 - a_normal ) * morphTargetInfluences[ 1 ];
					objectNormal += ( morphNormal2 - a_normal ) * morphTargetInfluences[ 2 ];
					objectNormal += ( morphNormal3 - a_normal ) * morphTargetInfluences[ 3 ];

				#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ skinbase_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
				#ifdef USE_SKINNING

					float4x4 boneMatX = getBoneMatrix( skinIndex.x );
					float4x4 boneMatY = getBoneMatrix( skinIndex.y );
					float4x4 boneMatZ = getBoneMatrix( skinIndex.z );
					float4x4 boneMatW = getBoneMatrix( skinIndex.w );

				#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ skinnormal_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
					#ifdef USE_SKINNING

						float4x4 skinMatrix = float4x4( 0.0 );
						skinMatrix += skinWeight.x * boneMatX;
						skinMatrix += skinWeight.y * boneMatY;
						skinMatrix += skinWeight.z * boneMatZ;
						skinMatrix += skinWeight.w * boneMatW;
						skinMatrix  = bindMatrixInverse * skinMatrix * bindMatrix;

						objectNormal = mul( skinMatrix, float4( objectNormal, 0.0 ) ).xyz;

						#ifdef USE_TANGENT

							objectTangent = mul( skinMatrix, float4( objectTangent, 0.0 ) ).xyz;

						#endif

					#endif

				// --------------------------------------------------------------------------------------------
				// ------------------------------------------ defaultnormal_vertex ------------------------------------------
				// --------------------------------------------------------------------------------------------
					// float3 transformedNormal = mul( normalMatrix, float4( objectNormal, 0.0 ) ).xyz;
					float3 transformedNormal = mul(UNITY_MATRIX_V, mul(unity_ObjectToWorld, float4(objectNormal, 0.0))).xyz;

					#ifdef FLIP_SIDED

						transformedNormal = - transformedNormal;

					#endif

					#ifdef USE_TANGENT

						// float3 transformedTangent = mul( normalMatrix, float4( objectTangent, 0.0 ) ).xyz;
						float3 transformedTangent = mul(UNITY_MATRIX_V, mul(unity_ObjectToWorld, float4(objectTangent, 0.0))).xyz;

						#ifdef FLIP_SIDED

							transformedTangent = - transformedTangent;

						#endif

					#endif




					#ifndef FLAT_SHADED // Normal computed with derivatives when FLAT_SHADED

						o.normal = normalize( transformedNormal );

						#ifdef USE_TANGENT

							o.tangent = normalize( transformedTangent );
							o.bitangent = normalize( -cross( o.normal, o.tangent ) * v.tangent.w );

						#endif

					#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ begin_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					float3 transformed = v.vertex.xyz;

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ morphtarget_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#ifdef USE_MORPHTARGETS

						transformed += ( morphTarget0 - v.vertex ) * morphTargetInfluences[ 0 ];
						transformed += ( morphTarget1 - v.vertex ) * morphTargetInfluences[ 1 ];
						transformed += ( morphTarget2 - v.vertex ) * morphTargetInfluences[ 2 ];
						transformed += ( morphTarget3 - v.vertex ) * morphTargetInfluences[ 3 ];

						#ifndef USE_MORPHNORMALS

						transformed += ( morphTarget4 - v.vertex ) * morphTargetInfluences[ 4 ];
						transformed += ( morphTarget5 - v.vertex ) * morphTargetInfluences[ 5 ];
						transformed += ( morphTarget6 - v.vertex ) * morphTargetInfluences[ 6 ];
						transformed += ( morphTarget7 - v.vertex ) * morphTargetInfluences[ 7 ];

						#endif

					#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ skinning_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#ifdef USE_SKINNING

						float4 skinVertex = bindMatrix * float4( transformed, 1.0 );

						float4 skinned = float4( 0.0 );
						skinned += boneMatX * skinVertex * skinWeight.x;
						skinned += boneMatY * skinVertex * skinWeight.y;
						skinned += boneMatZ * skinVertex * skinWeight.z;
						skinned += boneMatW * skinVertex * skinWeight.w;

						transformed = ( bindMatrixInverse * skinned ).xyz;

					#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ displacementmap_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#ifdef USE_DISPLACEMENTMAP

						transformed += normalize( objectNormal ) * ( tex2D( displacementMap, v.texcoord.xy ).x * displacementScale + displacementBias );

					#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ project_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				
					float4 mvPosition = mul(UNITY_MATRIX_V, mul(unity_ObjectToWorld, float4( transformed, 1.0 )));

					o.pos = UnityObjectToClipPos(float4( transformed, 1.0 ));


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ logdepthbuf_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#ifdef USE_LOGDEPTHBUF

						#ifdef USE_LOGDEPTHBUF_EXT

							vFragDepth = 1.0 + gl_Position.w;

						#else

							gl_Position.z = log2( max( EPSILON, gl_Position.w + 1.0 ) ) * logDepthBufFC - 1.0;

							gl_Position.z *= gl_Position.w;

						#endif

					#endif

					o.viewPosition = - mvPosition.xyz;


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ worldpos_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#if defined( USE_ENVMAP ) || defined( DISTANCE ) || defined ( USE_SHADOWMAP )

						float4 worldPosition = mul(unity_ObjectToWorld, float4( transformed, 1.0 ));

					#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ shadowmap_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#ifdef USE_SHADOWMAP

						#if NUM_DIR_LIGHT_SHADOWS > 0

						// // #pragma unroll_loop
						// for ( int i = 0; i < NUM_DIR_LIGHT_SHADOWS; i ++ ) {

						// 	vDirectionalShadowCoord[ i ] = directionalShadowMatrix[ i ] * worldPosition;

						// }
						o.shadowCoord = mul(unity_WorldToShadow[ 0 ], worldPosition);

						#endif

						#if NUM_SPOT_LIGHT_SHADOWS > 0

						// #pragma unroll_loop
						for ( int i = 0; i < NUM_SPOT_LIGHT_SHADOWS; i ++ ) {

							vSpotShadowCoord[ i ] = spotShadowMatrix[ i ] * worldPosition;

						}

						#endif

						#if NUM_POINT_LIGHT_SHADOWS > 0

						// #pragma unroll_loop
						for ( int i = 0; i < NUM_POINT_LIGHT_SHADOWS; i ++ ) {

							vPointShadowCoord[ i ] = pointShadowMatrix[ i ] * worldPosition;

						}

						#endif

						/*
						#if NUM_RECT_AREA_LIGHTS > 0

							// TODO (abelnation): update vAreaShadowCoord with area light info

						#endif
						*/

					#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ fog_vertex ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
					#ifdef USE_FOG

						fogDepth = -mvPosition.z;

					#endif
				return o;
			}

			// -----------------------------------------------------------------------------------------------------
			// ------------------------------------------ fragment shader ------------------------------------------
			// -----------------------------------------------------------------------------------------------------

			#include "i3d_brdf.cginc"

			fixed4 frag(v2f i) : SV_Target {

				float4 diffuseColor = _Color;
				ReflectedLight reflectedLight;
				reflectedLight.directDiffuse = float3(0.0, 0.0, 0.0);
				reflectedLight.directSpecular = float3(0.0, 0.0, 0.0);
				reflectedLight.indirectDiffuse = float3(0.0, 0.0, 0.0);
				reflectedLight.indirectSpecular = float3(0.0, 0.0, 0.0);

				float3 totalEmissiveRadiance = float3(0.0, 0.0, 0.0);
				#ifdef USE_EMISSION
					totalEmissiveRadiance = _EmissionColor.rgb;
				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ logdepthbuf_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if defined( USE_LOGDEPTHBUF ) && defined( USE_LOGDEPTHBUF_EXT )

					gl_FragDepthEXT = log2( vFragDepth ) * logDepthBufFC * 0.5;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ map_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef USE_MAP

					float4 texelColor = tex2D( _MainTex, i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw );

					texelColor = mapTexelToLinear( texelColor );
					diffuseColor *= texelColor;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ color_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef USE_COLOR

					diffuseColor.rgb *= i.color;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ alphamap_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef USE_ALPHAMAP

					diffuseColor.a *= tex2D( alphaMap, i.uv.xy ).g;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ alphatest_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef ALPHATEST

					if ( diffuseColor.a < _Cutoff ) discard;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ roughnessmap_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				float roughnessFactor = _GlossMapScale;

				#ifdef USE_ROUGHNESSMAP
					
					float4 texelRoughness = tex2D( _MetallicGlossMap, i.uv.xy * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw );

					// reads channel G, compatible with a combined OcclusionRoughnessMetallic (RGB) texture
					roughnessFactor *= texelRoughness.a;

				#endif

				roughnessFactor = 1.0 - roughnessFactor;

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ metalnessmap_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				float metalnessFactor = _Metallic;

				#ifdef USE_METALNESSMAP

					// float4 texelMetalness = tex2D( _MetallicGlossMap, metallic_gloss_uv );

					// reads channel B, compatible with a combined OcclusionRoughnessMetallic (RGB) texture
					// metalnessFactor *= texelMetalness.r;
					metalnessFactor *= texelRoughness.r;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ normal_fragment_begin ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef FLAT_SHADED

					// Workaround for Adreno/Nexus5 not able able to do dFdx( o.viewPosition ) ...

					float3 fdx = float3( dFdx( o.viewPosition.x ), dFdx( o.viewPosition.y ), dFdx( o.viewPosition.z ) );
					float3 fdy = float3( dFdy( o.viewPosition.x ), dFdy( o.viewPosition.y ), dFdy( o.viewPosition.z ) );
					float3 normal = normalize( cross( fdx, fdy ) );

				#else

					float3 normal = normalize( i.normal );

					#ifdef DOUBLE_SIDED

						normal = normal * ( float( FRONT_FACING ) * 2.0 - 1.0 );

					#endif

					#ifdef USE_TANGENT

						float3 tangent = normalize( i.tangent );
						float3 bitangent = normalize( i.bitangent );

						#ifdef DOUBLE_SIDED

							tangent = tangent * ( float( FRONT_FACING ) * 2.0 - 1.0 );
							bitangent = bitangent * ( float( FRONT_FACING ) * 2.0 - 1.0 );

						#endif

					#endif

				#endif

				// non perturbed normal for clearcoat among others

				float3 geometryNormal = normal;

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ normal_fragment_maps ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef USE_NORMALMAP
					#ifdef OBJECTSPACE_NORMALMAP

						normal = UnpackNormal(tex2D( _BumpMap, i.uv.xy * _BumpMap_ST.xy + _BumpMap_ST.zw )); // overrides both flatShading and attribute normals

						#ifdef FLIP_SIDED

							normal = - normal;

						#endif

						#ifdef DOUBLE_SIDED

							normal = normal * ( float( FRONT_FACING ) * 2.0 - 1.0 );

						#endif

						// normal = normalize( mul( normalMatrix, normal ) );
						normal = normalize(mul(UNITY_MATRIX_V, mul(unity_ObjectToWorld, normal)));

					#elif defined( TANGENTSPACE_NORMALMAP )

						#ifdef USE_TANGENT

							// float3x3 vTBN = float3x3( tangent, bitangent, normal );
							float3x3 vTBN = float3x3(
								 tangent.x, bitangent.x, normal.x,
								 tangent.y, bitangent.y, normal.y,
								 tangent.z, bitangent.z, normal.z
								);

							fixed3 mapN = UnpackNormal(tex2D( _BumpMap, i.uv.xy * _BumpMap_ST.xy + _BumpMap_ST.zw ));
							mapN.xy = _BumpScale * mapN.xy;
							normal = normalize( mul(vTBN, mapN) );

						#else

							normal = perturbNormal2Arb( -o.viewPosition, normal, _BumpScale, _BumpMap, i.uv.xy * _BumpMap_ST.xy + _BumpMap_ST.zw );

						#endif
					#endif
				#elif defined( USE_BUMPMAP )

					normal = perturbNormalArb( -o.viewPosition, normal, dHdxy_fwd() );

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ clearcoat_normal_fragment_begin ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef CLEARCOAT

					float3 clearcoatNor = geometryNormal;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ clearcoat_normal_fragment_maps ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if defined( CLEARCOAT ) && defined( USE_CLEARCOAT_NORMALMAP )

					#ifdef USE_TANGENT

						// float3x3 vTBN = float3x3( tangent, bitangent, clearcoatNormal );
						float3x3 clearCoatvTBN = float3x3(
							 tangent.x, bitangent.x, clearcoatNor.x,
							 tangent.y, bitangent.y, clearcoatNor.y,
							 tangent.z, bitangent.z, clearcoatNor.z
							 );

						float3 clearCoatmapN = UnpackNormal(tex2D( clearcoatNormalMap, i.uv.xy * _BumpMap_ST.xy + _BumpMap_ST.zw ));
						clearCoatmapN.xy = clearcoatNormalScale * clearCoatmapN.xy;
						clearcoatNor = normalize( mul(clearCoatvTBN, clearCoatmapN) );

					#else

						clearcoatNor = perturbNormal2Arb( - o.viewPosition, clearcoatNor, clearcoatNormalScale, clearcoatNormalMap, i.uv.xy * _BumpMap_ST.xy + _BumpMap_ST.zw );

					#endif

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ emissivemap_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if defined( USE_EMISSION ) && defined( USE_EMISSIVEMAP )

					fixed4 emissiveColor = tex2D( _EmissionMap, i.uv.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw );

					emissiveColor.rgb = emissiveMapTexelToLinear( emissiveColor ).rgb;

					totalEmissiveRadiance *= emissiveColor.rgb;

				#endif



				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ lights_physical_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------

				// accumulation
				PhysicalMaterial material;
				material.diffuseColor = diffuseColor.rgb * ( 1.0 - metalnessFactor );
				material.specularRoughness = clamp( roughnessFactor, 0.04, 1.0 );

				#ifdef REFLECTIVITY

					material.specularColor = lerp( MAXIMUM_SPECULAR_COEFFICIENT * pow2( reflectivity ), diffuseColor.rgb, metalnessFactor );

				#else

					material.specularColor = lerp( DEFAULT_SPECULAR_COEFFICIENT , diffuseColor.rgb, metalnessFactor );

				#endif

				#ifdef CLEARCOAT

					material.clearcoat = saturate( clearcoat ); // Burley clearcoat model
					material.clearcoatRoughness = clamp( clearcoatRoughness, 0.04, 1.0 );

				#endif
				#ifdef USE_SHEEN

					material.sheenColor = sheen;

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ lights_fragment_begin ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				/**
				* This is a template that can be used to light a material, it uses pluggable
				* RenderEquations (RE)for specific lighting scenarios.
				*
				* Instructions for use:
				* - Ensure that both RE_Direct, RE_IndirectDiffuse and RE_IndirectSpecular are defined
				* - If you have defined an RE_IndirectSpecular, you need to also provide a Material_LightProbeLOD. <---- ???
				* - Create a material parameter that is to be passed as the third parameter to your lighting functions.
				*
				* TODO:
				* - Add area light support.
				* - Add sphere light support.
				* - Add diffuse light probe (irradiance cubemap) support.
				*/

				GeometricContext geometry;

				geometry.position = - i.viewPosition;
				geometry.normal = normal;
				geometry.viewDir = normalize( i.viewPosition );

				#ifdef CLEARCOAT

					geometry.clearcoatNormal = clearcoatNor;

				#endif

				IncidentLight directLight;

				#if ( NUM_POINT_LIGHTS > 0 ) && defined( RE_Direct )

					PointLight pointLight;

					// #pragma unroll_loop
					for ( int i = 0; i < NUM_POINT_LIGHTS; i ++ ) {

						pointLight = pointLights[ i ];

						getPointDirectLightIrradiance( pointLight, geometry, directLight );

						#if defined( USE_SHADOWMAP ) && ( UNROLLED_LOOP_INDEX < NUM_POINT_LIGHT_SHADOWS )
						directLight.color *= all( bvec2( pointLight.shadow, directLight.visible ) ) ? getPointShadow( pointShadowMap[ i ], pointLight.shadowMapSize, pointLight.shadowBias, pointLight.shadowRadius, vPointShadowCoord[ i ], pointLight.shadowCameraNear, pointLight.shadowCameraFar ) : 1.0;
						#endif

						RE_Direct( directLight, geometry, material, reflectedLight );

					}

				#endif

				#if ( NUM_SPOT_LIGHTS > 0 ) && defined( RE_Direct )

					SpotLight spotLight;

					// #pragma unroll_loop
					for ( int i = 0; i < NUM_SPOT_LIGHTS; i ++ ) {

						spotLight = spotLights[ i ];

						getSpotDirectLightIrradiance( spotLight, geometry, directLight );

						#if defined( USE_SHADOWMAP ) && ( UNROLLED_LOOP_INDEX < NUM_SPOT_LIGHT_SHADOWS )
						directLight.color *= all( bvec2( spotLight.shadow, directLight.visible ) ) ? getShadow( spotShadowMap[ i ], spotLight.shadowMapSize, spotLight.shadowBias, spotLight.shadowRadius, vSpotShadowCoord[ i ] ) : 1.0;
						#endif

						RE_Direct( directLight, geometry, material, reflectedLight );

					}

				#endif

				#if ( NUM_DIR_LIGHTS > 0 ) && defined( RE_Direct )

					DirectionalLight directionalLight;

					// #pragma unroll_loop
					// for ( int i = 0; i < NUM_DIR_LIGHTS; i ++ ) {

					//     directionalLight = directionalLights[ i ];

					//     getDirectionalDirectLightIrradiance( directionalLight, geometry, directLight );

					//     #if defined( USE_SHADOWMAP ) && ( UNROLLED_LOOP_INDEX < NUM_DIR_LIGHT_SHADOWS )
					//     directLight.color *= all( bvec2( directionalLight.shadow, directLight.visible ) ) ? getShadow( directionalShadowMap[ i ], directionalLight.shadowMapSize, directionalLight.shadowBias, directionalLight.shadowRadius, vDirectionalShadowCoord[ i ] ) : 1.0;
					//     #endif

					//     RE_Direct( directLight, geometry, material, reflectedLight );

					// }

					directionalLight.direction = mul(UNITY_MATRIX_V, _WorldSpaceLightPos0).xyz;
					directionalLight.color = _LightColor0.rgb;

					getDirectionalDirectLightIrradiance( directionalLight, geometry, directLight );

					#if defined( USE_SHADOWMAP )

					directionalLight.shadow = DIRECTIONAL_LIGHT_SHADOW;
					directionalLight.shadowBias = 0;
					directionalLight.shadowRadius = SHADOW_RADIUS;
					directionalLight.shadowMapSize = _ShadowMapTexture_TexelSize.zw;

					directLight.color *= ( directionalLight.shadow && directLight.visible ) ? getShadow( _ShadowMapTexture, directionalLight.shadowMapSize, directionalLight.shadowBias, directionalLight.shadowRadius, i.shadowCoord, _LightShadowData.x ) : 1.0;

					#endif

					RE_Direct( directLight, geometry, material, reflectedLight );

				#endif

				#if ( NUM_RECT_AREA_LIGHTS > 0 ) && defined( RE_Direct_RectArea )

					RectAreaLight rectAreaLight;

					// #pragma unroll_loop
					for ( int i = 0; i < NUM_RECT_AREA_LIGHTS; i ++ ) {

						rectAreaLight = rectAreaLights[ i ];
						RE_Direct_RectArea( rectAreaLight, geometry, material, reflectedLight );

					}

				#endif

				#if defined( RE_IndirectDiffuse )

					float3 iblIrradiance = float3( 0.0, 0.0, 0.0 );

					float3 irradiance = float3( 0.0, 0.0, 0.0 );

					irradiance += getLightProbeIrradiance_Unity( geometry );

					#if ( NUM_HEMI_LIGHTS > 0 )

						// #pragma unroll_loop
						for ( int i = 0; i < NUM_HEMI_LIGHTS; i ++ ) {

							irradiance += getHemisphereLightIrradiance( hemisphereLights[ i ], geometry );

						}

					#endif

				#endif

				#if defined( RE_IndirectSpecular )

					float3 radiance = float3( 0.0, 0.0, 0.0 );
					float3 clearcoatRadiance = float3( 0.0, 0.0, 0.0 );

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ lights_fragment_maps ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if defined( RE_IndirectDiffuse )

					#ifdef USE_LIGHTMAP

						float3 lightMapIrradiance = tex2D( lightMap, i.uv.zw ).xyz * lightMapIntensity;

						#ifndef PHYSICALLY_CORRECT_LIGHTS

							lightMapIrradiance *= PI; // factor of PI should not be present; included here to prevent breakage

						#endif

						irradiance += lightMapIrradiance;

					#endif

					#if defined( USE_ENVMAP ) && defined( STANDARD ) && defined( ENVMAP_TYPE_CUBE_UV )

						iblIrradiance += getLightProbeIndirectIrradiance( /*lightProbe,*/ geometry, maxMipLevel );

					#endif

				#endif

				#if defined( USE_ENVMAP ) && defined( RE_IndirectSpecular )

					radiance += getLightProbeIndirectRadiance( /*specularLightProbe,*/ geometry.viewDir, geometry.normal, material.specularRoughness, maxMipLevel );

					#ifdef CLEARCOAT

						clearcoatRadiance += getLightProbeIndirectRadiance( /*specularLightProbe,*/ geometry.viewDir, geometry.clearcoatNormal, material.clearcoatRoughness, maxMipLevel );

					#endif

				#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ lights_fragment_end ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if defined( RE_IndirectDiffuse )

					RE_IndirectDiffuse( irradiance, geometry, material, reflectedLight );

				#endif

				#if defined( RE_IndirectSpecular )

					RE_IndirectSpecular( radiance, iblIrradiance, clearcoatRadiance, geometry, material, reflectedLight );

				#endif

				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ refraction_fragment_maps ---------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if ENVMAP_MODE_REFRACTION && defined( USE_ENVMAP )

					float3 refractionColor = float3(0.0, 0.0, 0.0);
					refractionColor += getLightProbeRefractionColor(geometry.viewDir, geometry.normal, material.specularRoughness, maxMipLevel);

					refractionColor = lerp(refractionColor, reflectedLight.directDiffuse + reflectedLight.indirectDiffuse, _Color.a);

					float dotNV = saturate( dot( normalize(geometry.normal), geometry.viewDir ) );
					// float fresnel = exp2( ( -5.55473 * dotNV - 6.98316 ) * dotNV );
					float fresnel = 0.02 + 0.1 * reflectivity + pow(1.0 - dotNV, 2.0);
					fresnel = saturate(fresnel);
					reflectedLight.indirectSpecular = lerp(refractionColor, reflectedLight.indirectSpecular, fresnel);
					
				#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ aomap_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------

				// modulation
				#ifdef USE_AOMAP

					// reads channel R, compatible with a combined OcclusionRoughnessMetallic (RGB) texture
					float ambientOcclusion = ( tex2D( _OcclusionMap, i.uv.zw * _OcclusionMap_ST.xy + _OcclusionMap_ST.zw ).r - 1.0 ) * _OcclusionStrength + 1.0;

					reflectedLight.indirectDiffuse *= ambientOcclusion;

					#if defined( USE_ENVMAP ) && defined( STANDARD )

						float dotNV = saturate( dot( geometry.normal, geometry.viewDir ) );

						reflectedLight.indirectSpecular *= computeSpecularOcclusion( dotNV, ambientOcclusion, material.specularRoughness );

					#endif

				#endif


				#if ENVMAP_MODE_REFRACTION
					fixed3 outgoingLight = reflectedLight.directSpecular + reflectedLight.indirectSpecular + totalEmissiveRadiance;
				#else
					fixed3 outgoingLight = reflectedLight.directDiffuse + reflectedLight.indirectDiffuse + reflectedLight.directSpecular + reflectedLight.indirectSpecular + totalEmissiveRadiance;
				#endif

				// this is a stub for the transparency model
				#ifdef TRANSPARENCY
					diffuseColor.a *= saturate( 1.0 - _transparency + linearToRelativeLuminance( reflectedLight.directSpecular + reflectedLight.indirectSpecular ) );
				#endif

				fixed4 result = fixed4( outgoingLight, diffuseColor.a );


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ tonemapping_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#if defined( TONE_MAPPING )

					result.rgb = toneMapping( result.rgb );

				#endif



				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ encodings_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				result = linearToOutputTexel( result );



				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ fog_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef USE_FOG

					#ifdef FOG_EXP2

						float fogFactor = 1.0 - exp( - fogDensity * fogDensity * fogDepth * fogDepth );

					#else

						float fogFactor = smoothstep( fogNear, fogFar, fogDepth );

					#endif

					result.rgb = lerp( result.rgb, fogColor, fogFactor );

				#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ premultiplied_alpha_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef PREMULTIPLIED_ALPHA

					// Get get normal blending with premultipled, use with CustomBlending, OneFactor, OneMinusSrcAlphaFactor, AddEquation.
					result.rgb *= result.a;

				#endif


				// -----------------------------------------------------------------------------------------------------
				// ------------------------------------------ dithering_fragment ------------------------------------------
				// -----------------------------------------------------------------------------------------------------
				#ifdef DITHERING

					result.rgb = dithering( result.rgb );

				#endif

				return result;
			}

			ENDCG
		}

		Pass {

			Name "ShadowCaster"
        	Tags { "LightMode" = "ShadowCaster" }

			Cull [_ShadowCull]

			CGPROGRAM
			#pragma target 3.0

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_shadowcaster
			// 下面这种写法，在C#中拿不到SHADOWS_DEPTH这个KeyWord
			// #pragma shader_feature SHADOWS_DEPTH
			
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
			};

			v2f vert( a2v v )
			{
				v2f o;
				// o.pos = UnityClipSpaceShadowCasterPos(v.vertex, v.normal);

				float4 wPos = mul(unity_ObjectToWorld, v.vertex);

				if (unity_LightShadowBias.z != 0.0)
				{
					float3 wNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
					float3 wLight = normalize(_WorldSpaceLightPos0.xyz);

					// apply normal offset bias (inset position along the normal)
					// bias needs to be scaled by sine between normal and light direction
					// (http://the-witness.net/news/2013/09/shadow-mapping-summary-part-1/)
					//
					// unity_LightShadowBias.z contains user-specified normal offset amount
					// scaled by world space texel size.

					float shadowCos = dot(wNormal, wLight);
					float shadowSine = sqrt(1-shadowCos*shadowCos);
					float normalBias = unity_LightShadowBias.z * shadowSine;

					wPos.xyz -= wNormal * normalBias;
				}

				o.pos = mul(UNITY_MATRIX_VP, wPos);

				// o.pos = UnityApplyLinearShadowBias(o.pos);
				#if defined(UNITY_REVERSED_Z)
					// We use max/min instead of clamp to ensure proper handling of the rare case
					// where both numerator and denominator are zero and the fraction becomes NaN.
					o.pos.z += max(-1, min(unity_LightShadowBias.x / o.pos.w, 0));
				#else
					o.pos.z += saturate(unity_LightShadowBias.x/o.pos.w);
				#endif
				// o.pos.z += saturate(unity_LightShadowBias.x/o.pos.w);

				#if defined(UNITY_REVERSED_Z)
					float clamped = min(o.pos.z, o.pos.w*UNITY_NEAR_CLIP_VALUE);
				#else
					float clamped = max(o.pos.z, o.pos.w*UNITY_NEAR_CLIP_VALUE);
				#endif

				// float clamped = max(o.pos.z, -o.pos.w);
				o.pos.z = lerp(o.pos.z, clamped, unity_LightShadowBias.y);
				return o;
			}

			float4 frag( v2f i ) : SV_Target
			{
				return 0;
			}

			ENDCG
		}

	}

	CustomEditor "InsightStandardShaderGUI"
}







