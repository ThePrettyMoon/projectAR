#include "amplify_standard_head_fs.sh"
#include "amplify_compatible_unity_fs.sh"

uniform lowp vec4 _Color;
void main ()
{
  lowp vec4 col_1 = vec4(0.0);
  col_1.xyz = _Color.xyz;
  col_1.w = 1.0;
  gl_FragData[0] = col_1;
}



