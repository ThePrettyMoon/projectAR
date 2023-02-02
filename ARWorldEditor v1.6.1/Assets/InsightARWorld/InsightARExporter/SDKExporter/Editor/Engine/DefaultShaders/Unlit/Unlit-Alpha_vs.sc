#include "amplify_standard_head_vs.sh"
#include "amplify_compatible_unity_vs.sh"

uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1 = vec4(0.0);
  tmpvar_1.w = 1.0;
  tmpvar_1.xyz = _glesVertex.xyz;
  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_1));
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
}



