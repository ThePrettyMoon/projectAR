
// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ dithering_pars_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------
#ifdef DITHERING

    // based on https://www.shadertoy.com/view/MslGR8
    float3 dithering( float3 color ) {
        //Calculate grid position
        float grid_position = rand( gl_FragCoord.xy );

        //Shift the individual colors differently, thus making it even harder to see the dithering pattern
        float3 dither_shift_RGB = float3( 0.25 / 255.0, -0.25 / 255.0, 0.25 / 255.0 );

        //modify shift acording to grid position.
        dither_shift_RGB = lerp( 2.0 * dither_shift_RGB, -2.0 * dither_shift_RGB, grid_position );

        //shift the color by dither_shift
        return color + dither_shift_RGB;
    }

#endif

// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ bsdfs ------------------------------------------
// -----------------------------------------------------------------------------------------------------

// Analytical approximation of the DFG LUT, one half of the
// split-sum approximation used in indirect specular lighting.
// via 'environmentBRDF' from "Physically Based Shading on Mobile"
// https://www.unrealengine.com/blog/physically-based-shading-on-mobile - environmentBRDF for GGX on mobile
float2 integrateSpecularBRDF( const in float dotNV, const in float roughness ) {
    const float4 c0 = float4( - 1, - 0.0275, - 0.572, 0.022 );

    const float4 c1 = float4( 1, 0.0425, 1.04, - 0.04 );

    float4 r = roughness * c0 + c1;

    float a004 = min( r.x * r.x, exp2( - 9.28 * dotNV ) ) * r.x + r.y;

    return float2( -1.04, 1.04 ) * a004 + r.zw;

}

float punctualLightIntensityToIrradianceFactor( const in float lightDistance, const in float cutoffDistance, const in float decayExponent ) {

#if defined ( PHYSICALLY_CORRECT_LIGHTS )

    // based upon Frostbite 3 Moving to Physically-based Rendering
    // page 32, equation 26: E[window1]
    // https://seblagarde.files.wordpress.com/2015/07/course_notes_moving_frostbite_to_pbr_v32.pdf
    // this is intended to be used on spot and point lights who are represented as luminous intensity
    // but who must be converted to luminous irradiance for surface lighting calculation
    float distanceFalloff = 1.0 / max( pow( lightDistance, decayExponent ), 0.01 );

    if( cutoffDistance > 0.0 ) {

        distanceFalloff *= pow2( saturate( 1.0 - pow4( lightDistance / cutoffDistance ) ) );

    }

    return distanceFalloff;

#else

    if( cutoffDistance > 0.0 && decayExponent > 0.0 ) {

        return pow( saturate( -lightDistance / cutoffDistance + 1.0 ), decayExponent );

    }

    return 1.0;

#endif

}

float3 BRDF_Diffuse_Lambert( const in float3 diffuseColor ) {

    return RECIPROCAL_PI * diffuseColor;

} // validated

float3 F_Schlick( const in float3 specularColor, const in float dotLH ) {

    // Original approximation by Christophe Schlick '94
    // float fresnel = pow( 1.0 - dotLH, 5.0 );

    // Optimized variant (presented by Epic at SIGGRAPH '13)
    // https://cdn2.unrealengine.com/Resources/files/2013SiggraphPresentationsNotes-26915738.pdf
    float fresnel = exp2( ( -5.55473 * dotLH - 6.98316 ) * dotLH );

    return ( 1.0 - specularColor ) * fresnel + specularColor;

} // validated

float3 F_Schlick_RoughnessDependent( const in float3 F0, const in float dotNV, const in float roughness ) {

    // See F_Schlick
    float fresnel = exp2( ( -5.55473 * dotNV - 6.98316 ) * dotNV );
    float3 Fr = max( float3( 1.0 - roughness, 1.0 - roughness, 1.0 - roughness ), F0 ) - F0;

    return Fr * fresnel + F0;

}


// Microfacet Models for Refraction through Rough Surfaces - equation (34)
// http://graphicrants.blogspot.com/2013/08/specular-brdf-reference.html
// alpha is "roughness squared" in Disney’s reparameterization
float G_GGX_Smith( const in float alpha, const in float dotNL, const in float dotNV ) {

    // geometry term (normalized) = G(l)⋅G(v) / 4(n⋅l)(n⋅v)
    // also see #12151

    float a2 = pow2( alpha );

    float gl = dotNL + sqrt( a2 + ( 1.0 - a2 ) * pow2( dotNL ) );
    float gv = dotNV + sqrt( a2 + ( 1.0 - a2 ) * pow2( dotNV ) );

    return 1.0 / ( gl * gv );

} // validated

// Moving Frostbite to Physically Based Rendering 3.0 - page 12, listing 2
// https://seblagarde.files.wordpress.com/2015/07/course_notes_moving_frostbite_to_pbr_v32.pdf
float G_GGX_SmithCorrelated( const in float alpha, const in float dotNL, const in float dotNV ) {

    float a2 = pow2( alpha );

    // dotNL and dotNV are explicitly swapped. This is not a mistake.
    float gv = dotNL * sqrt( a2 + ( 1.0 - a2 ) * pow2( dotNV ) );
    float gl = dotNV * sqrt( a2 + ( 1.0 - a2 ) * pow2( dotNL ) );

    return 0.5 / max( gv + gl, EPSILON );

}

// Microfacet Models for Refraction through Rough Surfaces - equation (33)
// http://graphicrants.blogspot.com/2013/08/specular-brdf-reference.html
// alpha is "roughness squared" in Disney’s reparameterization
float D_GGX( const in float alpha, const in float dotNH ) {

    float a2 = pow2( alpha );

    float denom = pow2( dotNH ) * ( a2 - 1.0 ) + 1.0; // avoid alpha = 0 with dotNH = 1

    return RECIPROCAL_PI * a2 / pow2( denom );

}

// GGX Distribution, Schlick Fresnel, GGX-Smith Visibility
float3 BRDF_Specular_GGX( const in IncidentLight incidentLight, const in float3 viewDir, const in float3 normal, const in float3 specularColor, const in float roughness ) {

    float alpha = pow2( roughness ); // UE4's roughness

    float3 halfDir = normalize( incidentLight.direction + viewDir );

    float dotNL = saturate( dot( normal, incidentLight.direction ) );
    float dotNV = saturate( dot( normal, viewDir ) );
    float dotNH = saturate( dot( normal, halfDir ) );
    float dotLH = saturate( dot( incidentLight.direction, halfDir ) );

    float3 F = F_Schlick( specularColor, dotLH );

    float G = G_GGX_SmithCorrelated( alpha, dotNL, dotNV );

    float D = D_GGX( alpha, dotNH );

    return F * ( G * D );

} // validated

// Rect Area Light

// Real-Time Polygonal-Light Shading with Linearly Transformed Cosines
// by Eric Heitz, Jonathan Dupuy, Stephen Hill and David Neubelt
// code: https://github.com/selfshadow/ltc_code/

float2 LTC_Uv( const in float3 N, const in float3 V, const in float roughness ) {

    const float LUT_SIZE  = 64.0;
    const float LUT_SCALE = ( LUT_SIZE - 1.0 ) / LUT_SIZE;
    const float LUT_BIAS  = 0.5 / LUT_SIZE;

    float dotNV = saturate( dot( N, V ) );

    // texture parameterized by sqrt( GGX alpha ) and sqrt( 1 - cos( theta ) )
    float2 uv = float2( roughness, sqrt( 1.0 - dotNV ) );

    uv = uv * LUT_SCALE + LUT_BIAS;

    return uv;

}

float LTC_ClippedSphereFormFactor( const in float3 f ) {

    // Real-Time Area Lighting: a Journey from Research to Production (p.102)
    // An approximation of the form factor of a horizon-clipped rectangle.

    float l = length( f );

    return max( ( l * l + f.z ) / ( l + 1.0 ), 0.0 );

}

float3 LTC_EdgeVectorFormFactor( const in float3 v1, const in float3 v2 ) {

    float x = dot( v1, v2 );

    float y = abs( x );

    // rational polynomial approximation to theta / sin( theta ) / 2PI
    float a = 0.8543985 + ( 0.4965155 + 0.0145206 * y ) * y;
    float b = 3.4175940 + ( 4.1616724 + y ) * y;
    float v = a / b;

    float theta_sintheta = ( x > 0.0 ) ? v : 0.5 * rsqrt( max( 1.0 - x * x, 1e-7 ) ) - v;

    return cross( v1, v2 ) * theta_sintheta;

}

float3 LTC_Evaluate( const in float3 N, const in float3 V, const in float3 P, const in float3x3 mInv, const in float3 rectCoords[ 4 ] ) {

    // bail if point is on back side of plane of light
    // assumes ccw winding order of light vertices
    float3 v1 = rectCoords[ 1 ] - rectCoords[ 0 ];
    float3 v2 = rectCoords[ 3 ] - rectCoords[ 0 ];
    float3 lightNormal = cross( v1, v2 );

    if( dot( lightNormal, P - rectCoords[ 0 ] ) < 0.0 ) return float3( 0.0, 0.0, 0.0 );

    // construct orthonormal basis around N
    float3 T1, T2;
    T1 = normalize( V - N * dot( V, N ) );
    T2 = - cross( N, T1 ); // negated from paper; possibly due to a different handedness of world coordinate system

    // compute transform
    float3x3 mat = mul(mInv, transposeMat3( float3x3( T1, T2, N ) ) );

    // transform rect
    float3 coords[ 4 ];
    coords[ 0 ] = mul(mat, ( rectCoords[ 0 ] - P ));
    coords[ 1 ] = mul(mat, ( rectCoords[ 1 ] - P ));
    coords[ 2 ] = mul(mat, ( rectCoords[ 2 ] - P ));
    coords[ 3 ] = mul(mat, ( rectCoords[ 3 ] - P ));

    // project rect onto sphere
    coords[ 0 ] = normalize( coords[ 0 ] );
    coords[ 1 ] = normalize( coords[ 1 ] );
    coords[ 2 ] = normalize( coords[ 2 ] );
    coords[ 3 ] = normalize( coords[ 3 ] );

    // calculate vector form factor
    float3 vectorFormFactor = float3( 0.0, 0.0, 0.0 );
    vectorFormFactor += LTC_EdgeVectorFormFactor( coords[ 0 ], coords[ 1 ] );
    vectorFormFactor += LTC_EdgeVectorFormFactor( coords[ 1 ], coords[ 2 ] );
    vectorFormFactor += LTC_EdgeVectorFormFactor( coords[ 2 ], coords[ 3 ] );
    vectorFormFactor += LTC_EdgeVectorFormFactor( coords[ 3 ], coords[ 0 ] );

    // adjust for horizon clipping
    float result = LTC_ClippedSphereFormFactor( vectorFormFactor );

/*
    // alternate method of adjusting for horizon clipping (see referece)
    // refactoring required
    float len = length( vectorFormFactor );
    float z = vectorFormFactor.z / len;

    const float LUT_SIZE  = 64.0;
    const float LUT_SCALE = ( LUT_SIZE - 1.0 ) / LUT_SIZE;
    const float LUT_BIAS  = 0.5 / LUT_SIZE;

    // tabulated horizon-clipped sphere, apparently...
    float2 uv = float2( z * 0.5 + 0.5, len );
    uv = uv * LUT_SCALE + LUT_BIAS;

    float scale = tex2D( ltc_2, uv ).w;

    float result = len * scale;
*/

    return float3( result, result, result );

}

// End Rect Area Light

// ref: https://www.unrealengine.com/blog/physically-based-shading-on-mobile - environmentBRDF for GGX on mobile
float3 BRDF_Specular_GGX_Environment( const in float3 viewDir, const in float3 normal, const in float3 specularColor, const in float roughness ) {

    float dotNV = saturate( dot( normal, viewDir ) );

    float2 brdf = integrateSpecularBRDF( dotNV, roughness );

    return specularColor * brdf.x + brdf.y;

} // validated

// Fdez-Agüera's "Multiple-Scattering Microfacet Model for Real-Time Image Based Lighting"
// Approximates multiscattering in order to preserve energy.
// http://www.jcgt.org/published/0008/01/03/
void BRDF_Specular_Multiscattering_Environment( const in GeometricContext geometry, const in float3 specularColor, const in float roughness, inout float3 singleScatter, inout float3 multiScatter ) {

    float dotNV = saturate( dot( geometry.normal, geometry.viewDir ) );

    float3 F = F_Schlick_RoughnessDependent( specularColor, dotNV, roughness );
    float2 brdf = integrateSpecularBRDF( dotNV, roughness );
    float3 FssEss = F * brdf.x + brdf.y;

    float Ess = brdf.x + brdf.y;
    float Ems = 1.0 - Ess;

    float3 Favg = specularColor + ( 1.0 - specularColor ) * 0.047619; // 1/21
    float3 Fms = FssEss * Favg / ( 1.0 - Ems * Favg );

    singleScatter += FssEss;
    multiScatter += Fms * Ems;

}

float G_BlinnPhong_Implicit( /* const in float dotNL, const in float dotNV */ ) {

    // geometry term is (n dot l)(n dot v) / 4(n dot l)(n dot v)
    return 0.25;

}

float D_BlinnPhong( const in float shininess, const in float dotNH ) {

    return RECIPROCAL_PI * ( shininess * 0.5 + 1.0 ) * pow( dotNH, shininess );

}

float3 BRDF_Specular_BlinnPhong( const in IncidentLight incidentLight, const in GeometricContext geometry, const in float3 specularColor, const in float shininess ) {

    float3 halfDir = normalize( incidentLight.direction + geometry.viewDir );

    //float dotNL = saturate( dot( geometry.normal, incidentLight.direction ) );
    //float dotNV = saturate( dot( geometry.normal, geometry.viewDir ) );
    float dotNH = saturate( dot( geometry.normal, halfDir ) );
    float dotLH = saturate( dot( incidentLight.direction, halfDir ) );

    float3 F = F_Schlick( specularColor, dotLH );

    float G = G_BlinnPhong_Implicit( /* dotNL, dotNV */ );

    float D = D_BlinnPhong( shininess, dotNH );

    return F * ( G * D );

} // validated

// source: http://simonstechblog.blogspot.ca/2011/12/microfacet-brdf.html
float GGXRoughnessToBlinnExponent( const in float ggxRoughness ) {
    return ( 2.0 / pow2( ggxRoughness + 0.0001 ) - 2.0 );
}

float BlinnExponentToGGXRoughness( const in float blinnExponent ) {
    return sqrt( 2.0 / ( blinnExponent + 2.0 ) );
}

#if defined( USE_SHEEN )

// https://github.com/google/filament/blob/master/shaders/src/brdf.fs#L94
float D_Charlie(float roughness, float NoH) {
    // Estevez and Kulla 2017, "Production Friendly Microfacet Sheen BRDF"
    float invAlpha  = 1.0 / roughness;
    float cos2h = NoH * NoH;
    float sin2h = max(1.0 - cos2h, 0.0078125); // 2^(-14/2), so sin2h^2 > 0 in fp16
    return (2.0 + invAlpha) * pow(sin2h, invAlpha * 0.5) / (2.0 * PI);
}

// https://github.com/google/filament/blob/master/shaders/src/brdf.fs#L136
float V_Neubelt(float NoV, float NoL) {
    // Neubelt and Pettineo 2013, "Crafting a Next-gen Material Pipeline for The Order: 1886"
    return saturate(1.0 / (4.0 * (NoL + NoV - NoL * NoV)));
}

float3 BRDF_Specular_Sheen( const in float roughness, const in float3 L, const in GeometricContext geometry, float3 specularColor ) {

    float3 N = geometry.normal;
    float3 V = geometry.viewDir;

    float3 H = normalize( V + L );
    float dotNH = saturate( dot( N, H ) );

    return specularColor * D_Charlie( roughness, dotNH ) * V_Neubelt( dot(N, V), dot(N, L) );

}

#endif


// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ cube_uv_reflection_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------
#ifdef ENVMAP_TYPE_CUBE_UV

#define cubeUV_textureSize (1024.0)

int getFaceFromDirection(float3 direction) {
    float3 absDirection = abs(direction);
    int face = -1;
    if( absDirection.x > absDirection.z ) {
        if(absDirection.x > absDirection.y )
            face = direction.x > 0.0 ? 0 : 3;
        else
            face = direction.y > 0.0 ? 1 : 4;
    }
    else {
        if(absDirection.z > absDirection.y )
            face = direction.z > 0.0 ? 2 : 5;
        else
            face = direction.y > 0.0 ? 1 : 4;
    }
    return face;
}
#define cubeUV_maxLods1  (log2(cubeUV_textureSize*0.25) - 1.0)
#define cubeUV_rangeClamp (exp2((6.0 - 1.0) * 2.0))

float2 MipLevelInfo( float3 vec, float roughnessLevel, float roughness ) {
    float scale = exp2(cubeUV_maxLods1 - roughnessLevel);
    float dxRoughness = dFdx(roughness);
    float dyRoughness = dFdy(roughness);
    float3 dx = dFdx( vec * scale * dxRoughness );
    float3 dy = dFdy( vec * scale * dyRoughness );
    float d = max( dot( dx, dx ), dot( dy, dy ) );
    // Clamp the value to the max mip level counts. hard coded to 6 mips
    d = clamp(d, 1.0, cubeUV_rangeClamp);
    float mipLevel = 0.5 * log2(d);
    return float2(floor(mipLevel), frac(mipLevel));
}

#define cubeUV_maxLods2 (log2(cubeUV_textureSize*0.25) - 2.0)
#define cubeUV_rcpTextureSize (1.0 / cubeUV_textureSize)

float2 getCubeUV(float3 direction, float roughnessLevel, float mipLevel) {
    mipLevel = roughnessLevel > cubeUV_maxLods2 - 3.0 ? 0.0 : mipLevel;
    float a = 16.0 * cubeUV_rcpTextureSize;

    float2 exp2_packed = exp2( float2( roughnessLevel, mipLevel ) );
    float2 rcp_exp2_packed = float2( 1.0 ) / exp2_packed;
    // float powScale = exp2(roughnessLevel + mipLevel);
    float powScale = exp2_packed.x * exp2_packed.y;
    // float scale =  1.0 / exp2(roughnessLevel + 2.0 + mipLevel);
    float scale = rcp_exp2_packed.x * rcp_exp2_packed.y * 0.25;
    // float mipOffset = 0.75*(1.0 - 1.0/exp2(mipLevel))/exp2(roughnessLevel);
    float mipOffset = 0.75*(1.0 - rcp_exp2_packed.y) * rcp_exp2_packed.x;

    bool bRes = mipLevel == 0.0;
    scale =  bRes && (scale < a) ? a : scale;

    float3 r;
    float2 offset;
    int face = getFaceFromDirection(direction);

    float rcpPowScale = 1.0 / powScale;

    if( face == 0) {
        r = float3(direction.x, -direction.z, direction.y);
        offset = float2(0.0+mipOffset,0.75 * rcpPowScale);
        offset.y = bRes && (offset.y < 2.0*a) ? a : offset.y;
    }
    else if( face == 1) {
        r = float3(direction.y, direction.x, direction.z);
        offset = float2(scale+mipOffset, 0.75 * rcpPowScale);
        offset.y = bRes && (offset.y < 2.0*a) ? a : offset.y;
    }
    else if( face == 2) {
        r = float3(direction.z, direction.x, direction.y);
        offset = float2(2.0*scale+mipOffset, 0.75 * rcpPowScale);
        offset.y = bRes && (offset.y < 2.0*a) ? a : offset.y;
    }
    else if( face == 3) {
        r = float3(direction.x, direction.z, direction.y);
        offset = float2(0.0+mipOffset,0.5 * rcpPowScale);
        offset.y = bRes && (offset.y < 2.0*a) ? 0.0 : offset.y;
    }
    else if( face == 4) {
        r = float3(direction.y, direction.x, -direction.z);
        offset = float2(scale+mipOffset, 0.5 * rcpPowScale);
        offset.y = bRes && (offset.y < 2.0*a) ? 0.0 : offset.y;
    }
    else {
        r = float3(direction.z, -direction.x, direction.y);
        offset = float2(2.0*scale+mipOffset, 0.5 * rcpPowScale);
        offset.y = bRes && (offset.y < 2.0*a) ? 0.0 : offset.y;
    }
    r = normalize(r);
    float texelOffset = 0.5 * cubeUV_rcpTextureSize;
    float2 s = ( r.yz / abs( r.x ) + float2( 1.0 ) ) * 0.5;
    float2 base = offset + float2( texelOffset );
    return base + s * ( scale - 2.0 * texelOffset );
}

#define cubeUV_maxLods3 (log2(cubeUV_textureSize*0.25) - 3.0)

float4 textureCubeUV( sampler2D envMap, float3 reflectedDirection, float roughness ) {
    float roughnessVal = roughness* cubeUV_maxLods3;
    float r1 = floor(roughnessVal);
    float r2 = r1 + 1.0;
    float t = frac(roughnessVal);
    float2 mipInfo = MipLevelInfo(reflectedDirection, r1, roughness);
    float s = mipInfo.y;
    float level0 = mipInfo.x;
    float level1 = level0 + 1.0;
    level1 = level1 > 5.0 ? 5.0 : level1;

    // round to nearest mipmap if we are not interpolating.
    level0 += min( floor( s + 0.5 ), 5.0 );

    // Tri linear interpolation.
    float2 uv_10 = getCubeUV(reflectedDirection, r1, level0);
    float4 color10 = envMapTexelToLinear(tex2D(envMap, uv_10));

    float2 uv_20 = getCubeUV(reflectedDirection, r2, level0);
    float4 color20 = envMapTexelToLinear(tex2D(envMap, uv_20));

    float4 result = lerp(color10, color20, t);

    return float4(result.rgb, 1.0);
}

#endif


// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ envmap_physical_pars_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------
#if defined( USE_ENVMAP )

    float3 getLightProbeIndirectIrradiance( /*const in SpecularLightProbe specularLightProbe,*/ const in GeometricContext geometry, const in int maxMIPLevel ) {

        float3 worldNormal = inverseTransformDirection( geometry.normal, UNITY_MATRIX_V );

        #ifdef ENVMAP_TYPE_CUBE

            float3 queryVec = float3( flipEnvMap * worldNormal.x, worldNormal.yz );

            half4 envMapColor = UNITY_SAMPLE_TEXCUBE( unity_SpecCube0, queryVec );

            envMapColor.rgb = envMapTexelToLinear( envMapColor ).rgb;

        #elif defined( ENVMAP_TYPE_CUBE_UV )

            float3 queryVec = float3( flipEnvMap * worldNormal.x, worldNormal.yz );
            float4 envMapColor = textureCubeUV( envMap, queryVec, 1.0 );

        #else

            float4 envMapColor = float4( 0.0 );

        #endif

        return PI * envMapColor.rgb * envMapIntensity;

    }

    // Trowbridge-Reitz distribution to Mip level, following the logic of http://casual-effects.blogspot.ca/2011/08/plausible-environment-lighting-in-two.html
    float getSpecularMIPLevel( const in float roughness, const in int maxMIPLevel ) {

        float maxMIPLevelScalar = float( maxMIPLevel );

        float sigma = PI * roughness * roughness / ( 1.0 + roughness );
        float desiredMIPLevel = maxMIPLevelScalar + log2( sigma );

        // clamp to allowable LOD ranges.
        return clamp( desiredMIPLevel, 0.0, maxMIPLevelScalar );

    }

    float3 getLightProbeIndirectRadiance( /*const in SpecularLightProbe specularLightProbe,*/ const in float3 viewDir, const in float3 normal, const in float roughness, const in int maxMIPLevel ) {

        float3 reflectVec = reflect( -viewDir, normal );

        // Mixing the reflection with the normal is more accurate and keeps rough objects from gathering light from behind their tangent plane.
        reflectVec = normalize( lerp( reflectVec, normal, roughness * roughness) );

        reflectVec = inverseTransformDirection( reflectVec, UNITY_MATRIX_V );

        float specularMIPLevel = getSpecularMIPLevel( roughness, maxMIPLevel );

        #ifdef ENVMAP_TYPE_CUBE

            float3 queryReflectVec = float3( flipEnvMap * reflectVec.x, reflectVec.yz );

            half4 envMapColor = UNITY_SAMPLE_TEXCUBE_LOD( unity_SpecCube0, queryReflectVec, specularMIPLevel ) * unity_SpecCube0_HDR.x;

            envMapColor.rgb = envMapTexelToLinear( envMapColor ).rgb ;

        #elif defined( ENVMAP_TYPE_CUBE_UV )

            float3 queryReflectVec = float3( flipEnvMap * reflectVec.x, reflectVec.yz );
            float4 envMapColor = textureCubeUV( envMap, queryReflectVec, roughness );

        #elif defined( ENVMAP_TYPE_EQUIREC )

            float2 sampleUV;
            sampleUV.y = asin( clamp( reflectVec.y, - 1.0, 1.0 ) ) * RECIPROCAL_PI + 0.5;
            sampleUV.x = atan( reflectVec.z, reflectVec.x ) * RECIPROCAL_PI2 + 0.5;

            #ifdef TEXTURE_LOD_EXT

                float4 envMapColor = texture2DLodEXT( t_reflection_prob0_cube, sampleUV, specularMIPLevel );

            #else

                float4 envMapColor = tex2D( t_reflection_prob0_cube, sampleUV, specularMIPLevel );

            #endif

            envMapColor.rgb = envMapTexelToLinear( envMapColor ).rgb;

        #elif defined( ENVMAP_TYPE_SPHERE )

            float3 reflectView = normalize( ( mat_MatrixView * float4( reflectVec, 0.0 ) ).xyz + float3( 0.0,0.0,1.0 ) );

            #ifdef TEXTURE_LOD_EXT

                float4 envMapColor = texture2DLodEXT( t_reflection_prob0_cube, reflectView.xy * 0.5 + 0.5, specularMIPLevel );

            #else

                float4 envMapColor = tex2D( t_reflection_prob0_cube, reflectView.xy * 0.5 + 0.5, specularMIPLevel );

            #endif

            envMapColor.rgb = envMapTexelToLinear( envMapColor ).rgb;

        #endif

        return envMapColor.rgb * envMapIntensity;
        // return queryReflectVec;
    }

    #ifdef ENVMAP_MODE_REFRACTION
        uniform float refractionRatio;

        float3 getLightProbeRefractionColor(const in float3 viewDir, const in float3 normal, const in float roughness, const in int maxMIPLevel )
        {
            float3 reflectVec = refract( -viewDir, normal, refractionRatio );

            reflectVec = inverseTransformDirection( reflectVec, UNITY_MATRIX_V );
            float specularMIPLevel = getSpecularMIPLevel( roughness, maxMIPLevel );

            // ENVMAP_TYPE_CUBE
            float3 queryReflectVec = float3( flipEnvMap * reflectVec.x, reflectVec.yz );
            half4 envMapColor = UNITY_SAMPLE_TEXCUBE_LOD( unity_SpecCube0, queryReflectVec, specularMIPLevel ) * unity_SpecCube0_HDR.x;
            envMapColor.rgb = envMapTexelToLinear( envMapColor ).rgb ;

            return envMapColor.rgb * envMapIntensity;
        }
    #endif

#endif

// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ lights_pars_begin ------------------------------------------
// -----------------------------------------------------------------------------------------------------


// normal should be normalized, w=1.0
// output in active color space
float3 ShadeSH9( in float4 normal ) {
    float3 res;
    // Linear + constant polynomial terms
    res.r = dot(unity_SHAr,normal);
    res.g = dot(unity_SHAg,normal);
    res.b = dot(unity_SHAb,normal);

    // Quadratic polynomials
    float3 x1, x2;
    // 4 of the quadratic (L2) polynomials
    float4 vB = normal.xyzz * normal.yzzx;
    x1.r = dot(unity_SHBr,vB);
    x1.g = dot(unity_SHBr,vB);
    x1.b = dot(unity_SHBr,vB);

    // Final (5th) quadratic (L2) polynomial
    float vC = normal.x*normal.x - normal.y*normal.y;
    x2 = unity_SHC.rgb * vC;

    res += x1 + x2;
    res *= PI; 
    // res = LinearTosRGB( res );
    return res;
}

float3 getLightProbeIrradiance_Unity( const in GeometricContext geometry ) {

    float3 worldNormal = inverseTransformDirection( geometry.normal, UNITY_MATRIX_V );

    float3 irradiance = ShadeSH9( float4(worldNormal, 1.0) );

    return irradiance;

}

float3 getAmbientLightIrradiance( const in float3 ambientLightColor ) {

    float3 irradiance = ambientLightColor;

    #ifndef PHYSICALLY_CORRECT_LIGHTS

        irradiance *= PI;

    #endif

    return irradiance;

}

#if NUM_DIR_LIGHTS > 0

    struct DirectionalLight {
        float3 direction;
        float3 color;

        int shadow;
        float shadowBias;
        float shadowRadius;
        float2 shadowMapSize;
    };

    void getDirectionalDirectLightIrradiance( const in DirectionalLight directionalLight, const in GeometricContext geometry, out IncidentLight directLight ) {

        directLight.color = directionalLight.color;
        directLight.direction = directionalLight.direction;
        directLight.visible = true;

    }

#endif


#if NUM_POINT_LIGHTS > 0

    struct PointLight {
        float3 position;
        float3 color;
        float distance;
        float decay;

        int shadow;
        float shadowBias;
        float shadowRadius;
        float2 shadowMapSize;
        float shadowCameraNear;
        float shadowCameraFar;
    };

    uniform PointLight pointLights[ NUM_POINT_LIGHTS ];

    // directLight is an out parameter as having it as a return value caused compiler errors on some devices
    void getPointDirectLightIrradiance( const in PointLight pointLight, const in GeometricContext geometry, out IncidentLight directLight ) {

        float3 lVector = pointLight.position - geometry.position;
        directLight.direction = normalize( lVector );

        float lightDistance = length( lVector );

        directLight.color = pointLight.color;
        directLight.color *= punctualLightIntensityToIrradianceFactor( lightDistance, pointLight.distance, pointLight.decay );
        directLight.visible = ( directLight.color != float3( 0.0 ) );

    }

#endif


#if NUM_SPOT_LIGHTS > 0

    struct SpotLight {
        float3 position;
        float3 direction;
        float3 color;
        float distance;
        float decay;
        float coneCos;
        float penumbraCos;

        int shadow;
        float shadowBias;
        float shadowRadius;
        float2 shadowMapSize;
    };

    uniform SpotLight spotLights[ NUM_SPOT_LIGHTS ];

    // directLight is an out parameter as having it as a return value caused compiler errors on some devices
    void getSpotDirectLightIrradiance( const in SpotLight spotLight, const in GeometricContext geometry, out IncidentLight directLight  ) {

        float3 lVector = spotLight.position - geometry.position;
        directLight.direction = normalize( lVector );

        float lightDistance = length( lVector );
        float angleCos = dot( directLight.direction, spotLight.direction );

        if ( angleCos > spotLight.coneCos ) {

            float spotEffect = smoothstep( spotLight.coneCos, spotLight.penumbraCos, angleCos );

            directLight.color = spotLight.color;
            directLight.color *= spotEffect * punctualLightIntensityToIrradianceFactor( lightDistance, spotLight.distance, spotLight.decay );
            directLight.visible = true;

        } else {

            directLight.color = float3( 0.0 );
            directLight.visible = false;

        }
    }

#endif


#if NUM_RECT_AREA_LIGHTS > 0

    struct RectAreaLight {
        float3 color;
        float3 position;
        float3 halfWidth;
        float3 halfHeight;
    };

    // Pre-computed values of LinearTransformedCosine approximation of BRDF
    // BRDF approximation Texture is 64x64
    uniform sampler2D ltc_1; // RGBA Float
    uniform sampler2D ltc_2; // RGBA Float

    uniform RectAreaLight rectAreaLights[ NUM_RECT_AREA_LIGHTS ];

#endif


#if NUM_HEMI_LIGHTS > 0

    struct HemisphereLight {
        float3 direction;
        float3 skyColor;
        float3 groundColor;
    };

    uniform HemisphereLight hemisphereLights[ NUM_HEMI_LIGHTS ];

    float3 getHemisphereLightIrradiance( const in HemisphereLight hemiLight, const in GeometricContext geometry ) {

        float dotNL = dot( geometry.normal, hemiLight.direction );
        float hemiDiffuseWeight = 0.5 * dotNL + 0.5;

        float3 irradiance = lerp( hemiLight.groundColor, hemiLight.skyColor, hemiDiffuseWeight );

        #ifndef PHYSICALLY_CORRECT_LIGHTS

            irradiance *= PI;

        #endif

        return irradiance;

    }

#endif


// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ lights_physical_pars_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------


// Clear coat directional hemishperical reflectance (this approximation should be improved)
float clearcoatDHRApprox( const in float roughness, const in float dotNL ) {

    return DEFAULT_SPECULAR_COEFFICIENT + ( 1.0 - DEFAULT_SPECULAR_COEFFICIENT ) * ( pow( 1.0 - dotNL, 5.0 ) * pow( 1.0 - roughness, 2.0 ) );

}

#if NUM_RECT_AREA_LIGHTS > 0

    void RE_Direct_RectArea_Physical( const in RectAreaLight rectAreaLight, const in GeometricContext geometry, const in PhysicalMaterial material, inout ReflectedLight reflectedLight ) {

        float3 normal = geometry.normal;
        float3 viewDir = geometry.viewDir;
        float3 position = geometry.position;
        float3 lightPos = rectAreaLight.position;
        float3 halfWidth = rectAreaLight.halfWidth;
        float3 halfHeight = rectAreaLight.halfHeight;
        float3 lightColor = rectAreaLight.color;
        float roughness = material.specularRoughness;

        float3 rectCoords[ 4 ];
        rectCoords[ 0 ] = lightPos + halfWidth - halfHeight; // counterclockwise; light shines in local neg z direction
        rectCoords[ 1 ] = lightPos - halfWidth - halfHeight;
        rectCoords[ 2 ] = lightPos - halfWidth + halfHeight;
        rectCoords[ 3 ] = lightPos + halfWidth + halfHeight;

        float2 uv = LTC_Uv( normal, viewDir, roughness );

        float4 t1 = tex2D( ltc_1, uv );
        float4 t2 = tex2D( ltc_2, uv );

        mat3 mInv = mat3(
            float3( t1.x, 0, t1.y ),
            float3(    0, 1,    0 ),
            float3( t1.z, 0, t1.w )
        );

        // LTC Fresnel Approximation by Stephen Hill
        // http://blog.selfshadow.com/publications/s2016-advances/s2016_ltc_fresnel.pdf
        float3 fresnel = ( material.specularColor * t2.x + ( float3( 1.0 ) - material.specularColor ) * t2.y );

        reflectedLight.directSpecular += lightColor * fresnel * LTC_Evaluate( normal, viewDir, position, mInv, rectCoords );

        reflectedLight.directDiffuse += lightColor * material.diffuseColor * LTC_Evaluate( normal, viewDir, position, mat3( 1.0 ), rectCoords );

    }

#endif

void RE_Direct_Physical( const in IncidentLight directLight, const in GeometricContext geometry, const in PhysicalMaterial material, inout ReflectedLight reflectedLight ) {

    float dotNL = saturate( dot( geometry.normal, directLight.direction ) );

    float3 irradiance = dotNL * directLight.color;

    #ifndef PHYSICALLY_CORRECT_LIGHTS

        irradiance *= PI; // punctual light

    #endif

    #ifdef CLEARCOAT

        float ccDotNL = saturate( dot( geometry.clearcoatNormal, directLight.direction ) );

        float3 ccIrradiance = ccDotNL * directLight.color;

        #ifndef PHYSICALLY_CORRECT_LIGHTS

            ccIrradiance *= PI; // punctual light

        #endif

        float clearcoatDHR = material.clearcoat * clearcoatDHRApprox( material.clearcoatRoughness, ccDotNL );

        float3 clearcoatColor = ( DEFAULT_SPECULAR_COEFFICIENT );
        reflectedLight.directSpecular += ccIrradiance * material.clearcoat * BRDF_Specular_GGX( directLight, geometry.viewDir, geometry.clearcoatNormal, clearcoatColor, material.clearcoatRoughness );

    #else

        float clearcoatDHR = 0.0;
        float ccDotNL = 1.0;

    #endif

    #ifdef USE_SHEEN
        reflectedLight.directSpecular += ( 1.0 - clearcoatDHR ) * irradiance * BRDF_Specular_Sheen(
            material.specularRoughness,
            directLight.direction,
            geometry,
            material.sheenColor
        );
    #else
        reflectedLight.directSpecular += ( 1.0 - clearcoatDHR ) * irradiance * BRDF_Specular_GGX( directLight, geometry.viewDir, geometry.normal, material.specularColor, material.specularRoughness);
    #endif

    reflectedLight.directDiffuse += ccDotNL * irradiance * BRDF_Diffuse_Lambert( material.diffuseColor );
}

void RE_IndirectDiffuse_Physical( const in float3 irradiance, const in GeometricContext geometry, const in PhysicalMaterial material, inout ReflectedLight reflectedLight ) {

    reflectedLight.indirectDiffuse += irradiance * BRDF_Diffuse_Lambert( material.diffuseColor );

}

void RE_IndirectSpecular_Physical( const in float3 radiance, const in float3 irradiance, const in float3 clearcoatRadiance, const in GeometricContext geometry, const in PhysicalMaterial material, inout ReflectedLight reflectedLight) {

    #ifdef CLEARCOAT

        float ccDotNV = saturate( dot( geometry.clearcoatNormal, geometry.viewDir ) );

        float3 clearcoatColor = ( DEFAULT_SPECULAR_COEFFICIENT );
        reflectedLight.indirectSpecular += clearcoatRadiance * material.clearcoat * BRDF_Specular_GGX_Environment( geometry.viewDir, geometry.clearcoatNormal, clearcoatColor, material.clearcoatRoughness );

        float ccDotNL = ccDotNV;
        float clearcoatDHR = material.clearcoat * clearcoatDHRApprox( material.clearcoatRoughness, ccDotNL );

    #else

        float clearcoatDHR = 0.0;

    #endif

    float clearcoatInv = 1.0 - clearcoatDHR;

    // Both indirect specular and diffuse light accumulate here
    // if energy preservation enabled, and PMREM provided.

    float3 singleScattering = float3( 0.0, 0.0, 0.0 );
    float3 multiScattering = float3( 0.0, 0.0, 0.0 );
    float3 cosineWeightedIrradiance = irradiance * RECIPROCAL_PI;

    BRDF_Specular_Multiscattering_Environment( geometry, material.specularColor, material.specularRoughness, singleScattering, multiScattering );

    float3 diffuse = material.diffuseColor * ( 1.0 - ( singleScattering + multiScattering ) );

    float3 scaleRadiance = radiance;
    #ifndef ENVMAP_MODE_REFRACTION
        scaleRadiance *= singleScattering;
    #endif
    reflectedLight.indirectSpecular += clearcoatInv * scaleRadiance;
    reflectedLight.indirectDiffuse += multiScattering * cosineWeightedIrradiance;
    reflectedLight.indirectDiffuse += diffuse * cosineWeightedIrradiance;

}

#define RE_Direct                RE_Direct_Physical
#define RE_Direct_RectArea        RE_Direct_RectArea_Physical
#define RE_IndirectDiffuse        RE_IndirectDiffuse_Physical
#define RE_IndirectSpecular        RE_IndirectSpecular_Physical

// ref: https://seblagarde.files.wordpress.com/2015/07/course_notes_moving_frostbite_to_pbr_v32.pdf
float computeSpecularOcclusion( const in float dotNV, const in float ambientOcclusion, const in float roughness ) {

    return saturate( pow( dotNV + ambientOcclusion, exp2( - 16.0 * roughness - 1.0 ) ) - 1.0 + ambientOcclusion );

}



// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ shadowmap_pars_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------
#ifdef USE_SHADOWMAP

    float texture2DCompare( sampler2D depths, float2 uv, float compare ) {

        // return step( compare, unpackRGBAToDepth( tex2D( depths, uv ) ) );
        #if defined(UNITY_REVERSED_Z)
        return step( tex2D( depths, uv).r, compare);
        #else
        return step( compare, tex2D( depths, uv).r );
        #endif

    }

    float2 texture2DDistribution( sampler2D shadow, float2 uv ) {

        return decodeHalfRGBA( tex2D( shadow, uv ) );

    }

    float VSMShadow (sampler2D shadow, float2 uv, float compare ){

        float occlusion = 1.0;

        float2 distribution = texture2DDistribution( shadow, uv );

        float hard_shadow = step( compare , distribution.x ); // Hard Shadow

        if (hard_shadow != 1.0 ) {

            float distance = compare - distribution.x ;
            float variance = max( 0.00000, distribution.y * distribution.y );
            float softness_probability = variance / (variance + distance * distance ); // Chebeyshevs inequality
            softness_probability = clamp( ( softness_probability - 0.3 ) / ( 0.95 - 0.3 ), 0.0, 1.0 ); // 0.3 reduces light bleed
            occlusion = clamp( max( hard_shadow, softness_probability ), 0.0, 1.0 );

        }
        return occlusion;

    }

    float texture2DShadowLerp( sampler2D depths, float2 size, float2 uv, float compare ) {

        const float2 offset = float2( 0.0, 1.0 );

        float2 texelSize = float2( 1.0, 1.0 ) / size;
        float2 centroidUV = ( floor( uv * size - 0.5 ) + 0.5 ) * texelSize;

        float lb = texture2DCompare( depths, centroidUV + texelSize * offset.xx, compare );
        float lt = texture2DCompare( depths, centroidUV + texelSize * offset.xy, compare );
        float rb = texture2DCompare( depths, centroidUV + texelSize * offset.yx, compare );
        float rt = texture2DCompare( depths, centroidUV + texelSize * offset.yy, compare );

        float2 f = frac( uv * size + 0.5 );

        float a = lerp( lb, lt, f.y );
        float b = lerp( rb, rt, f.y );
        float c = lerp( a, b, f.x );

        return c;

    }

    float getShadow( sampler2D shadowMap, float2 shadowMapSize, float shadowBias, float shadowRadius, float4 shadowCoord, float shadowStrenth ) {

        float shadow = 1.0;

        shadowCoord.xyz /= shadowCoord.w;
        shadowCoord.z += shadowBias;

        // if ( something && something ) breaks ATI OpenGL shader compiler
        // if ( all( something, something ) ) using this instead
        bool frustumTest = shadowCoord.x >= 0.0 && shadowCoord.x <= 1.0 && shadowCoord.y >= 0.0 && shadowCoord.y <= 1.0 && shadowCoord.z <= 1.0;
        
        if ( frustumTest ) {

        #if defined( SHADOWMAP_TYPE_PCF )

            float2 texelSize = float2( 1.0, 1.0 ) / shadowMapSize;

            float dx0 = - texelSize.x * shadowRadius;
            float dy0 = - texelSize.y * shadowRadius;
            float dx1 = + texelSize.x * shadowRadius;
            float dy1 = + texelSize.y * shadowRadius;
            float dx2 = dx0 / 2.0;
            float dy2 = dy0 / 2.0;
            float dx3 = dx1 / 2.0;
            float dy3 = dy1 / 2.0;

            shadow = max( (
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx0, dy0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( 0.0, dy0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx1, dy0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx2, dy2 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( 0.0, dy2 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx3, dy2 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx0, 0.0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx2, 0.0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy, shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx3, 0.0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx1, 0.0 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx2, dy3 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( 0.0, dy3 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx3, dy3 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx0, dy1 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( 0.0, dy1 ), shadowCoord.z ) +
                texture2DCompare( shadowMap, shadowCoord.xy + float2( dx1, dy1 ), shadowCoord.z )
            ) * ( 1.0 / 17.0 ), shadowStrenth );

        #elif defined( SHADOWMAP_TYPE_PCF_SOFT )

            float2 texelSize = float2( 1.0, 1.0 ) / shadowMapSize;

            float dx0 = - texelSize.x * shadowRadius;
            float dy0 = - texelSize.y * shadowRadius;
            float dx1 = + texelSize.x * shadowRadius;
            float dy1 = + texelSize.y * shadowRadius;

            shadow = max( (
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( dx0, dy0 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( 0.0, dy0 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( dx1, dy0 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( dx0, 0.0 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy, shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( dx1, 0.0 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( dx0, dy1 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( 0.0, dy1 ), shadowCoord.z ) +
                texture2DShadowLerp( shadowMap, shadowMapSize, shadowCoord.xy + float2( dx1, dy1 ), shadowCoord.z )
            ) * ( 1.0 / 9.0 ), shadowStrenth );

        #elif defined( SHADOWMAP_TYPE_VSM )

            shadow = VSMShadow( shadowMap, shadowCoord.xy, shadowCoord.z );

        #else // no percentage-closer filtering:

            shadow = max(texture2DCompare( shadowMap, shadowCoord.xy, shadowCoord.z ), shadowStrenth);
            // float dist = tex2D(shadowMap, shadowCoord.xy).r;
            // shadow = max(dist > shadowCoord.z, _LightShadowData.x);
        #endif

        }

        return shadow;

    }

    // cubeToUV() maps a 3D direction vector suitable for cube texture mapping to a 2D
    // vector suitable for 2D texture mapping. This code uses the following layout for the
    // 2D texture:
    //
    // xzXZ
    //  y Y
    //
    // Y - Positive y direction
    // y - Negative y direction
    // X - Positive x direction
    // x - Negative x direction
    // Z - Positive z direction
    // z - Negative z direction
    //
    // Source and test bed:
    // https://gist.github.com/tschw/da10c43c467ce8afd0c4

    float2 cubeToUV( float3 v, float texelSizeY ) {

        // Number of texels to avoid at the edge of each square

        float3 absV = abs( v );

        // Intersect unit cube

        float scaleToCube = 1.0 / max( absV.x, max( absV.y, absV.z ) );
        absV *= scaleToCube;

        // Apply scale to avoid seams

        // two texels less per square (one texel will do for NEAREST)
        v *= scaleToCube * ( 1.0 - 2.0 * texelSizeY );

        // Unwrap

        // space: -1 ... 1 range for each square
        //
        // #X##        dim    := ( 4 , 2 )
        //  # #        center := ( 1 , 1 )

        float2 planar = v.xy;

        float almostATexel = 1.5 * texelSizeY;
        float almostOne = 1.0 - almostATexel;

        if ( absV.z >= almostOne ) {

            if ( v.z > 0.0 )
                planar.x = 4.0 - v.x;

        } else if ( absV.x >= almostOne ) {

            float signX = sign( v.x );
            planar.x = v.z * signX + 2.0 * signX;

        } else if ( absV.y >= almostOne ) {

            float signY = sign( v.y );
            planar.x = v.x + 2.0 * signY + 2.0;
            planar.y = v.z * signY - 2.0;

        }

        // Transform to UV space

        // scale := 0.5 / dim
        // translate := ( center + 0.5 ) / dim
        return float2( 0.125, 0.25 ) * planar + float2( 0.375, 0.75 );

    }

    float getPointShadow( sampler2D shadowMap, float2 shadowMapSize, float shadowBias, float shadowRadius, float4 shadowCoord, float shadowCameraNear, float shadowCameraFar ) {

        float2 texelSize = float2( 1.0, 1.0 ) / ( shadowMapSize * float2( 4.0, 2.0 ) );

        // for point lights, the uniform @vShadowCoord is re-purposed to hold
        // the vector from the light to the world-space position of the fragment.
        float3 lightToPosition = shadowCoord.xyz;

        // dp = normalized distance from light to fragment position
        float dp = ( length( lightToPosition ) - shadowCameraNear ) / ( shadowCameraFar - shadowCameraNear ); // need to clamp?
        dp += shadowBias;

        // bd3D = base direction 3D
        float3 bd3D = normalize( lightToPosition );

        #if defined( SHADOWMAP_TYPE_PCF ) || defined( SHADOWMAP_TYPE_PCF_SOFT ) || defined( SHADOWMAP_TYPE_VSM )

            float2 offset = float2( - 1, 1 ) * shadowRadius * texelSize.y;

            return (
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.xyy, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.yyy, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.xyx, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.yyx, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.xxy, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.yxy, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.xxx, texelSize.y ), dp ) +
                texture2DCompare( shadowMap, cubeToUV( bd3D + offset.yxx, texelSize.y ), dp )
            ) * ( 1.0 / 9.0 );

        #else // no percentage-closer filtering

            return texture2DCompare( shadowMap, cubeToUV( bd3D, texelSize.y ), dp );

        #endif

    }

#endif

// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ bumpmap_pars_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------
#ifdef USE_BUMPMAP

    // Bump Mapping Unparametrized Surfaces on the GPU by Morten S. Mikkelsen
    // http://api.unrealengine.com/attachments/Engine/Rendering/LightingAndShadows/BumpMappingWithoutTangentSpace/mm_sfgrad_bump.pdf

    // Evaluate the derivative of the height w.r.t. screen-space using forward differencing (listing 2)

    float2 dHdxy_fwd() {

        float2 dSTdx = dFdx( vUv );
        float2 dSTdy = dFdy( vUv );

        float Hll = bumpScale * tex2D( bumpMap, vUv ).x;
        float dBx = bumpScale * tex2D( bumpMap, vUv + dSTdx ).x - Hll;
        float dBy = bumpScale * tex2D( bumpMap, vUv + dSTdy ).x - Hll;

        return float2( dBx, dBy );

    }

    float3 perturbNormalArb( float3 surf_pos, float3 surf_norm, float2 dHdxy ) {

        // Workaround for Adreno 3XX dFd*( float3 ) bug. See #9988

        float3 vSigmaX = float3( dFdx( surf_pos.x ), dFdx( surf_pos.y ), dFdx( surf_pos.z ) );
        float3 vSigmaY = float3( dFdy( surf_pos.x ), dFdy( surf_pos.y ), dFdy( surf_pos.z ) );
        float3 vN = surf_norm;        // normalized

        float3 R1 = cross( vSigmaY, vN );
        float3 R2 = cross( vN, vSigmaX );

        float fDet = dot( vSigmaX, R1 );

        fDet *= ( float( gl_FrontFacing ) * 2.0 - 1.0 );

        float3 vGrad = sign( fDet ) * ( dHdxy.x * R1 + dHdxy.y * R2 );
        return normalize( abs( fDet ) * surf_norm - vGrad );

    }

#endif

// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ normalmap_pars_fragment ------------------------------------------
// -----------------------------------------------------------------------------------------------------
#if ! defined ( USE_TANGENT ) && ( defined ( TANGENTSPACE_NORMALMAP ) || defined ( USE_CLEARCOAT_NORMALMAP ) )

    // Per-Pixel Tangent Space Normal Mapping
    // http://hacksoflife.blogspot.ch/2009/11/per-pixel-tangent-space-normal-mapping.html

    float3 perturbNormal2Arb( float3 eye_pos, float3 surf_norm, float2 normalScale, in sampler2D normalMap, float2 uv ) {

        // Workaround for Adreno 3XX dFd*( float3 ) bug. See #9988

        float3 q0 = float3( dFdx( eye_pos.x ), dFdx( eye_pos.y ), dFdx( eye_pos.z ) );
        float3 q1 = float3( dFdy( eye_pos.x ), dFdy( eye_pos.y ), dFdy( eye_pos.z ) );
        float2 st0 = dFdx( vUv.st );
        float2 st1 = dFdy( vUv.st );

        float scale = sign( st1.t * st0.s - st0.t * st1.s ); // we do not care about the magnitude

        float3 S = normalize( ( q0 * st1.t - q1 * st0.t ) * scale );
        float3 T = normalize( ( - q0 * st1.s + q1 * st0.s ) * scale );
        float3 N = normalize( surf_norm );

        float3 mapN = tex2D( normalMap, uv ).xyz * 2.0 - 1.0;

        mapN.xy *= _BumpScale;

        #ifdef DOUBLE_SIDED

            // Workaround for Adreno GPUs gl_FrontFacing bug. See #15850 and #10331
            // http://hacksoflife.blogspot.com/2009/11/per-pixel-tangent-space-normal-mapping.html?showComment=1522254677437#c5087545147696715943
            float3 NfromST = cross( S, T );
            if( dot( NfromST, N ) > 0.0 ) {

                S *= -1.0;
                T *= -1.0;

            }

        #else

            mapN.xy *= ( float( gl_FrontFacing ) * 2.0 - 1.0 );

        #endif

        mat3 tsn = mat3( S, T, N );
        return normalize( tsn * mapN );

    }

#endif