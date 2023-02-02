#include "amplify_standard_head_vs.sh"
#include "amplify_compatible_unity_vs.sh"

void main ()
{
  highp vec4 tmpvar_1 = vec4(0.0);
  tmpvar_1.w = 1.0;
  tmpvar_1.xyz = _glesVertex.xyz;
  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_1));
}



