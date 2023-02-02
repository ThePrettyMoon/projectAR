
// --------------------------------------------------------------------------------------------
// ------------------------------------------ common ------------------------------------------
// --------------------------------------------------------------------------------------------

#define STANDARD

#define PI 3.14159265359
#define PI2 6.28318530718
#define PI_HALF 1.5707963267949
#define RECIPROCAL_PI 0.31830988618
#define RECIPROCAL_PI2 0.15915494
#define LOG2 1.442695
#define EPSILON 1e-6

// #define saturate(a) clamp( a, 0.0, 1.0 )
#define whiteComplement(a) ( 1.0 - saturate( a ) )

float pow2( const in float x ) { return x*x; }
float pow3( const in float x ) { return x*x*x; }
float pow4( const in float x ) { float x2 = x*x; return x2*x2; }
float average( const in float3 color ) { return dot( color, float3( 0.3333, 0.3333, 0.3333 ) ); }
// expects values in the range of [0,1]x[0,1], returns values in the [0,1] range.
// do not collapse into a single function per: http://byteblacksmith.com/improvements-to-the-canonical-one-liner-glsl-rand-for-opengl-es-2-0/
float rand( const in float2 uv ) {
    const float a = 12.9898, b = 78.233, c = 43758.5453;
    float dt = dot( uv.xy, float2( a,b ) ), sn = fmod( dt, PI );
    return frac(sin(sn) * c);
}

#ifdef HIGH_PRECISION
    float precisionSafeLength( float3 v ) { return length( v ); }
#else
    float max3( float3 v ) { return max( max( v.x, v.y ), v.z ); }
    float precisionSafeLength( float3 v ) {
        float maxComponent = max3( abs( v ) );
        return length( v / maxComponent ) * maxComponent;
    }
#endif



float3 transformDirection( in float3 dir, in float4x4 _matrix ) {

    return normalize( mul(_matrix, float4( dir, 0.0 ) ).xyz );

}

// http://en.wikibooks.org/wiki/GLSL_Programming/Applying_Matrix_Transformations
float3 inverseTransformDirection( in float3 dir, in float4x4 _matrix ) {

    return normalize( mul( float4( dir, 0.0 ), _matrix ).xyz );

}

float3 projectOnPlane(in float3 _point, in float3 pointOnPlane, in float3 planeNormal ) {

    float distance = dot( planeNormal, _point - pointOnPlane );

    return - distance * planeNormal + _point;

}

float sideOfPlane( in float3 _point, in float3 pointOnPlane, in float3 planeNormal ) {

    return sign( dot( _point - pointOnPlane, planeNormal ) );

}

float3 linePlaneIntersect( in float3 pointOnLine, in float3 lineDirection, in float3 pointOnPlane, in float3 planeNormal ) {

    return lineDirection * ( dot( planeNormal, pointOnPlane - pointOnLine ) / dot( planeNormal, lineDirection ) ) + pointOnLine;

}

float3x3 transposeMat3( const in float3x3 m ) {

    float3x3 tmp;

    tmp[ 0 ] = float3( m[ 0 ].x, m[ 1 ].x, m[ 2 ].x );
    tmp[ 1 ] = float3( m[ 0 ].y, m[ 1 ].y, m[ 2 ].y );
    tmp[ 2 ] = float3( m[ 0 ].z, m[ 1 ].z, m[ 2 ].z );

    return tmp;

}

// https://en.wikipedia.org/wiki/Relative_luminance
float linearToRelativeLuminance( const in float3 color ) {

    float3 weights = float3( 0.2126, 0.7152, 0.0722 );

    return dot( weights, color.rgb );

}



// --------------------------------------------------------------------------------------------
// ------------------------------------------ encodings_pars_fragment ------------------------------------------
// --------------------------------------------------------------------------------------------

// For a discussion of what this is, please read this: http://lousodrome.net/blog/light/2013/05/26/gamma-correct-and-hdr-rendering-in-a-32-bits-buffer/

float4 LinearToLinear( in float4 value ) {
    return value;
}

float4 GammaToLinear( in float4 value, in float gammaFactor ) {
    return float4( pow( value.rgb, gammaFactor ), value.a );
}

float4 LinearToGamma( in float4 value, in float gammaFactor ) {
    return float4( pow( value.rgb, 1.0 / gammaFactor ), value.a );
}

float4 sRGBToLinear( in float4 value ) {
    return float4( lerp( pow( value.rgb * 0.9478672986 + 0.0521327014 , 2.4 ), value.rgb * 0.0773993808, step( value.rgb, float3( 0.04045, 0.04045, 0.04045 ) ) ), value.a );
}

float4 LinearTosRGB( in float4 value ) {
    return float4( lerp( pow( value.rgb, 0.41666 ) * 1.055 - 0.055, value.rgb * 12.92, step( value.rgb, float3( 0.0031308, 0.0031308, 0.0031308 ) ) ), value.a );
}

float3 LinearTosRGB( in float3 value ) {
    return lerp( pow( value, 0.41666 ) * 1.055 - 0.055, value * 12.92, step( value, float3( 0.0031308, 0.0031308, 0.0031308 ) ) );
}

float4 RGBEToLinear( in float4 value ) {
    return float4( value.rgb * exp2( value.a * 255.0 - 128.0 ), 1.0 );
}

float4 LinearToRGBE( in float4 value ) {
    float maxComponent = max( max( value.r, value.g ), value.b );
    float fExp = clamp( ceil( log2( maxComponent ) ), -128.0, 127.0 );
    return float4( value.rgb / exp2( fExp ), ( fExp + 128.0 ) / 255.0 );
//  return float4( value.brg, ( 3.0 + 128.0 ) / 256.0 );
}

// reference: http://iwasbeingirony.blogspot.ca/2010/06/difference-between-rgbm-and-rgbd.html
float4 RGBMToLinear( in float4 value, in float maxRange ) {
    return float4( value.rgb * value.a * maxRange, 1.0 );
}

float4 LinearToRGBM( in float4 value, in float maxRange ) {
    float maxRGB = max( value.r, max( value.g, value.b ) );
    float M = clamp( maxRGB / maxRange, 0.0, 1.0 );
    M = ceil( M * 255.0 ) / 255.0;
    return float4( value.rgb / ( M * maxRange ), M );
}

// reference: http://iwasbeingirony.blogspot.ca/2010/06/difference-between-rgbm-and-rgbd.html
float4 RGBDToLinear( in float4 value, in float maxRange ) {
    return float4( value.rgb * ( ( maxRange / 255.0 ) / value.a ), 1.0 );
}

float4 LinearToRGBD( in float4 value, in float maxRange ) {
    float maxRGB = max( value.r, max( value.g, value.b ) );
    float D = max( maxRange / maxRGB, 1.0 );
    D = min( floor( D ) / 255.0, 1.0 );
    return float4( value.rgb * ( D * ( 255.0 / maxRange ) ), D );
}

// LogLuv reference: http://graphicrants.blogspot.ca/2009/04/rgbm-color-encoding.html

// M _matrix, for encoding
const float3x3 cLogLuvM = float3x3( 0.2209, 0.3390, 0.4184, 0.1138, 0.6780, 0.7319, 0.0102, 0.1130, 0.2969 );
float4 LinearToLogLuv( in float4 value )  {
    float3 Xp_Y_XYZp = mul(cLogLuvM, value.rgb);
    Xp_Y_XYZp = max( Xp_Y_XYZp, float3( 1e-6, 1e-6, 1e-6 ) );
    float4 vResult;
    vResult.xy = Xp_Y_XYZp.xy / Xp_Y_XYZp.z;
    float Le = 2.0 * log2(Xp_Y_XYZp.y) + 127.0;
    vResult.w = frac( Le );
    vResult.z = ( Le - ( floor( vResult.w * 255.0 ) ) / 255.0 ) / 255.0;
    return vResult;
}

// Inverse M _matrix, for decoding
const float3x3 cLogLuvInverseM = float3x3( 6.0014, -2.7008, -1.7996, -1.3320, 3.1029, -5.7721, 0.3008, -1.0882, 5.6268 );
float4 LogLuvToLinear( in float4 value ) {
    float Le = value.z * 255.0 + value.w;
    float3 Xp_Y_XYZp;
    Xp_Y_XYZp.y = exp2( ( Le - 127.0 ) / 2.0 );
    Xp_Y_XYZp.z = Xp_Y_XYZp.y / value.y;
    Xp_Y_XYZp.x = value.x * Xp_Y_XYZp.z;
    float3 vRGB = mul(cLogLuvInverseM, Xp_Y_XYZp.rgb);
    return float4( max( vRGB, 0.0 ), 1.0 );
}

// --------------------------------------------------------------------------------------------
// ------------------------------------------ encodings ------------------------------------------
// --------------------------------------------------------------------------------------------
float4 mapTexelToLinear(float4 value) {
    return sRGBToLinear(value);
}

float4 matcapTexelToLinear(float4 value) {
    return sRGBToLinear(value);
}

float4 envMapTexelToLinear(float4 value) {
    return sRGBToLinear(value);
}

float4 emissiveMapTexelToLinear(float4 value) {
    return sRGBToLinear(value);
}

float4 linearToOutputTexel(float4 value) {
    return LinearToGamma(value, 2.2);
}


// -----------------------------------------------------------------------------------------------------
// ------------------------------------------ packing ------------------------------------------
// -----------------------------------------------------------------------------------------------------
float3 packNormalToRGB( const in float3 normal ) {
    return normalize( normal ) * 0.5 + 0.5;
}

float3 unpackRGBToNormal( const in float3 rgb ) {
    return 2.0 * rgb.xyz - 1.0;
}

const float PackUpscale = 256. / 255.; // fraction -> 0..1 (including 1)
const float UnpackDownscale = 255. / 256.; // 0..1 -> fraction (excluding 1)

const float3 PackFactors = float3( 256. * 256. * 256., 256. * 256.,  256. );

const float ShiftRight8 = 1. / 256.;

float4 packDepthToRGBA( const in float v ) {
    float4 r = float4( frac( v * PackFactors ), v );
    r.yzw -= r.xyz * ShiftRight8; // tidy overflow
    return r * PackUpscale;
}

float unpackRGBAToDepth( const in float4 v ) {
    const float4 UnpackFactors = UnpackDownscale / float4( PackFactors, 1. );
    return dot( v, UnpackFactors );
}

float4 encodeHalfRGBA ( float2 v ) {
    float4 encoded = float4( 0.0, 0.0, 0.0, 0.0 );
    const float2 offset = float2( 1.0 / 255.0, 0.0 );

    encoded.xy = float2( v.x, frac( v.x * 255.0 ) );
    encoded.xy = encoded.xy - ( encoded.yy * offset );

    encoded.zw = float2( v.y, frac( v.y * 255.0 ) );
    encoded.zw = encoded.zw - ( encoded.ww * offset );

    return encoded;
}

float2 decodeHalfRGBA( float4 v ) {
    return float2( v.x + ( v.y / 255.0 ), v.z + ( v.w / 255.0 ) );
}

// NOTE: viewZ/eyeZ is < 0 when in front of the camera per OpenGL conventions

float viewZToOrthographicDepth( const in float viewZ, const in float near, const in float far ) {
    return ( viewZ + near ) / ( near - far );
}
float orthographicDepthToViewZ( const in float linearClipZ, const in float near, const in float far ) {
    return linearClipZ * ( near - far ) - near;
}

float viewZToPerspectiveDepth( const in float viewZ, const in float near, const in float far ) {
    return (( near + viewZ ) * far ) / (( far - near ) * viewZ );
}
float perspectiveDepthToViewZ( const in float invClipZ, const in float near, const in float far ) {
    return ( near * far ) / ( ( far - near ) * invClipZ - far );
}

// Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
// Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
fixed3 UnpackNormalmapRGorAG(fixed4 packednormal)
{
    // This do the trick
   packednormal.x *= packednormal.w;

    fixed3 normal;
    normal.xy = packednormal.xy * 2 - 1;
    normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
    return normal;
}
inline fixed3 UnpackNormal(fixed4 packednormal)
{
#if defined(UNITY_NO_DXT5nm)
    return packednormal.xyz * 2 - 1;
#else
    return UnpackNormalmapRGorAG(packednormal);
#endif
}

// Decodes HDR textures
// handles dLDR, RGBM formats
inline half3 DecodeHDR (half4 data, half4 decodeInstructions)
{
    // Take into account texture alpha if decodeInstructions.w is true(the alpha value affects the RGB channels)
    half alpha = decodeInstructions.w * (data.a - 1.0) + 1.0;

    // If Linear mode is not supported we can skip exponent part
    #if defined(UNITY_COLORSPACE_GAMMA)
        return (decodeInstructions.x * alpha) * data.rgb;
    #else
    #   if defined(UNITY_USE_NATIVE_HDR)
            return decodeInstructions.x * data.rgb; // Multiplier for future HDRI relative to absolute conversion.
    #   else
            return (decodeInstructions.x * pow(alpha, decodeInstructions.y)) * data.rgb;
    #   endif
    #endif
}