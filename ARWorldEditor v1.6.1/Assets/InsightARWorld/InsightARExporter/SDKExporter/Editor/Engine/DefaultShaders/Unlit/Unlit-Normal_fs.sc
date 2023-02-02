#include "amplify_standard_head_fs.sh"
#include "amplify_compatible_unity_fs.sh"

uniform sampler2D _MainTex;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 col_1 = vec4(0.0);
  col_1.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  col_1.w = 1.0;
  gl_FragData[0] = col_1;
}



