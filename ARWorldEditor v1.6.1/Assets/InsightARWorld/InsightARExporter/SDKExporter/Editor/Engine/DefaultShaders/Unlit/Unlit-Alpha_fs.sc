#include "amplify_standard_head_fs.sh"
#include "amplify_compatible_unity_fs.sh"

uniform sampler2D _MainTex;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1 = vec4(0.0);
  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0);
  gl_FragData[0] = tmpvar_1;
}



