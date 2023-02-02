
namespace RenderEngine
{
    class UtilityShaderGenerator
    {

        public class Declaration
        {
            public Declaration(string _name, string _type){name = _name; type = _type;}
            public string name;
            public string type;
        };

		public static string[ , ] SHADER_EXCLUDED_UNIFORMS = new string[2, 37]
		{
			{ "in_TANGENT0",
			"in_POSITION0",
			"in_NORMAL0",
			"in_COLOR0",
			"in_TEXCOORD0",
			"in_TEXCOORD1",
			"unity_SHAr", 
			"unity_SHAg", 
			"unity_SHAb", 
			"unity_SHBr", 
			"unity_SHBg", 
			"unity_SHBb", 
			"unity_SHC", 
			"hlslcc_mtx4x4unity_ObjectToWorld[4]", 
			"hlslcc_mtx4x4unity_WorldToObject[4]", 
			"unity_WorldTransformParams", 
			"hlslcc_mtx4x4unity_MatrixVP[4]", 
			"hlslcc_mtx4x4unity_MatrixV[4]", 
			"hlslcc_mtx4x4glstate_matrix_projection[4]", 
			"_WorldSpaceCameraPos",
			"_CosTime", 
			"_SinTime", 
			"_Time", 
			"_WorldSpaceLightPos0", 
			"_LightColor0",
            "hlslcc_mtx4x4unity_WorldToShadow[16]",
            "unity_LightShadowBias",
            "_ProjectionParams",
            "_ScreenParams",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
             },

			{ "hlslcc_mtx4x4unity_ObjectToWorld[4]",
			"hlslcc_mtx4x4unity_WorldToObject[4]",
			"unity_WorldTransformParams",
			"hlslcc_mtx4x4unity_MatrixVP[4]",
			"hlslcc_mtx4x4unity_MatrixV[4]",
			"hlslcc_mtx4x4glstate_matrix_projection[4]",
			"_WorldSpaceCameraPos",
			"_WorldSpaceLightPos0",
			"_LightColor0",
			"unity_SHAr", 
			"unity_SHAg", 
			"unity_SHAb", 
			"unity_SHBr", 
			"unity_SHBg", 
			"unity_SHBb", 
			"unity_SHC", 
			"unity_SpecCube0", 
			"unity_SpecCube0_BoxMax", 
			"unity_SpecCube0_BoxMin", 
			"unity_SpecCube0_ProbePosition", 
			"unity_SpecCube0_HDR", 
			"unity_SpecCube1", 
			"unity_SpecCube1_BoxMax", 
			"unity_SpecCube1_BoxMin", 
			"unity_SpecCube1_ProbePosition", 
			"unity_SpecCube1_HDR", 
			"_CosTime", 
			"_SinTime", 
			"_Time", 
			"_ZBufferParams",
            "hlslcc_mtx4x4unity_WorldToShadow[16]",
            "_ShadowMapTexture",
            "_LightShadowData",
            "unity_LightShadowBias",
            "unity_ShadowFadeCenterAndType",
            "_ProjectionParams",
            "_ScreenParams",
            }
		};

		public static bool IsExcludedUniform(string name, int index)
		{
			for (int i = 0; i < SHADER_EXCLUDED_UNIFORMS.GetLength(1); i ++)
			{
				if( SHADER_EXCLUDED_UNIFORMS[index, i].Equals( name ) ) return true;
			}
			return false;
		}

        static readonly System.Collections.Generic.HashSet<string> GLSLTypeName = new System.Collections.Generic.HashSet<string> 
        {
            "float", "int", "uint", "bool",
             "vec2", "vec3", "vec4",
             "ivec2", "ivec3", "ivec4",
             "uint", "uvec2", "uvec3", "uvec4",
             "bool", "bvec2", "bvec3", "bvec4",
             "mat2", "mat2x2", "mat2x3", "mat2x4",
             "mat3x2", "mat3", "mat3x3", "mat3x4",
             "mat4x2", "mat4x3", "mat4", "mat4x4",
             "sampler1D", "sampler2D", "sampler3D", "samplerCube"
        };

        static readonly System.Collections.Generic.HashSet<string> CGTypeName = new System.Collections.Generic.HashSet<string> 
        {
            "int", "char", "short", "long", "float", "half", "fixed", "bool",
            "cint", "cfloat", "void", "sampler"
        };

        public static string FindVariableName(string statement)
        {
			string[] words = statement.Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
            for (int l = 1; l < words.Length; ++ l)
                if (words[l].Length > 0 && (IsGLSLType(words[l - 1]) || IsCGType(words[l - 1])))
                    return words[l];

            return string.Empty;
        }

        // Always return the first founded declaration name
        public static Declaration GetGLSLDeclaration(string statement, out int typePosition)
        {
            string[] words = statement.Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
            int len = words.Length - 1;
            if (len > 0)
            {
                if (words[len].Length > 0 && IsCGType(words[len - 1]))
                {
                    typePosition = statement.IndexOf(words[len - 1]);
                    return new Declaration(words[len], words[len - 1]);
                }
			}
            typePosition = -1;
            return new Declaration("", "");
        }

        // Always return the first founded declaration name
		public static Declaration GetGLSLDeclaration(string statement)
        {
			string[] words = statement.Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
            for (int l = 1; l < words.Length; ++ l)
                if (words[l].Length > 0 && IsGLSLType(words[l - 1]))
                    return new Declaration(words[l], words[l - 1]);

            return new Declaration("", "");
        }

       public static Declaration GetCGDeclaration(string statement, out int typePosition)
        {
            string[] words = statement.Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
            int len = words.Length - 1;
            if (len > 0)
            {
                if (words[len].Length > 0 && IsCGType(words[len - 1]))
                {
                    typePosition = statement.IndexOf(words[len - 1]);
                    return new Declaration(words[len], words[len - 1]);

                }
			}
            typePosition = -1;
            return new Declaration("", "");
        }

        public static Declaration GetCGDeclaration(string statement)
        {
            string[] words = statement.Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
            int len = words.Length - 1;
            if (len > 0)
            {
                if (words[len].Length > 0 && IsCGType(words[len - 1]))
                    return new Declaration(words[len], words[len - 1]);
            }
            return new Declaration("", "");
        }

        static bool IsCGType(string word)
        {
            // CGType is flexiable, so we only check the prefix 
            for (int p = word.Length; p > 0; -- p)
            {
                if (CGTypeName.Contains(word.Substring(0, p)))
                    return true;
            }
            
            return false;
        }

        static bool IsGLSLType(string word)
        {
            return GLSLTypeName.Contains(word);
        }
    }
}