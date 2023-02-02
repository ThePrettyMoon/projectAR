/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RenderEngine
{
    using sgc = ShaderGeneratorConfig; 
    class ShaderGenerator
    {
        public static ShaderLabShader GenerateJson(Shader shader)
        {
            string sourceShaderFile = new AssetPath().GetPath( shader );
			string jsonFile = sgc.DEFAULT_JSON_FILE_PATH + shader.name + ".json";			

			ShaderLabShader sl = null;
            if (!IsJsonNeedRegenerate(sourceShaderFile, jsonFile, out sl))
			{
				Debug.Log(Path.GetFileName(sourceShaderFile) + " is up to date, skip json generating.");
                return sl; 
			}

			// start generate json
			// NOTICE: built-in shader may not be readable.
			string compiledShaderFile;
			string IDCode = "";
			if (File.Exists(sourceShaderFile))
			{
            	// 1. first modify all 'float2' and 'float3' to 'float4' in sourceShader
				string contentBak = ReadFile(sourceShaderFile);				
				bool isModified = AdjustShaderVariables(sourceShaderFile, contentBak);

				AssetDatabase.Refresh();
				compiledShaderFile = GetCompiledShaderPath(shader);

				if (isModified) WriteFile(sourceShaderFile, contentBak);

				// 2. Get ID Code
				IDCode = GetTimeStamp(sourceShaderFile);
				string line = new StringReader(contentBak).ReadLine();
				if (line.Trim().ToUpper() == sgc.GENERATE_TOGGLE.Trim())
					IDCode += sgc.GENERATE_TOGGLE;

				// string line = new StringReader(contentBak).ReadLine();
				// if (line.Trim().ToUpper() == sgc.GENERATE_TOGGLE.Trim())
				// 	IDCode = sgc.GENERATE_TOGGLE;
				// else 
				// 	IDCode = GetTimeStamp(sourceShaderFile);

				
			}
			else
			{
				compiledShaderFile = GetCompiledShaderPath(shader);
			}

			// 2. Create Json file
            if (null != compiledShaderFile)
            {	
				if (!Directory.Exists(Path.GetDirectoryName(jsonFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(jsonFile));

				string err = UtilityExecute.ExecuteTool
								( "ShaderLabParser" , 
								"\"" + compiledShaderFile + "\"" 
								+ ' ' + "\"" + jsonFile + "\""
								+ ' ' + "\"" + sourceShaderFile + "\""
								+ ' ' + "\"" + IDCode + "\""
								);
				if (err.Length > 0)
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
						, "compiling " + compiledShaderFile + " failed. " + err
						, "OK!");
				
				string jsonContent = File.ReadAllText(jsonFile, System.Text.Encoding.UTF8);
				sl = JsonUtility.FromJson<ShaderLabShader>(jsonContent);

                Debug.Log("Detect modification of " + Path.GetFileName(sourceShaderFile) + ". Generate json successfully.");
            }
            else 
            {
				UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
					, "compiling " + Path.GetFileName(sourceShaderFile) + " failed."
					, "OK!");
            }

			return sl;
        }

		public static bool GenerateVSFS(string src_vs_file, string src_fs_file, ShaderLabShader sl, int pass_index)
		{
			if (sl == null)
				return false;

			// 用户强制不生成vs 和 fs ，即shader里写 “// AUTO GENERATE OFF”
			if (sl.IDCode == sgc.GENERATE_TOGGLE)
                return true;

			// 用户强制不生成vs，即vs里写 “// AUTO GENERATE OFF”
			if (File.Exists(src_vs_file))
				using (System.IO.StreamReader readingFile = new System.IO.StreamReader(src_vs_file))
				{
					string line = readingFile.ReadLine();
					if (line.Trim().ToUpper() == sgc.GENERATE_TOGGLE.Trim())
						return true;
				}			
			WriteFile(src_vs_file, GenerateGLSLCode(sl, sgc.ShaderType.Vertex, pass_index));

			// 用户强制不生成fs，即fs里写 “// AUTO GENERATE OFF”
			if (File.Exists(src_fs_file))
				using (System.IO.StreamReader readingFile = new System.IO.StreamReader(src_fs_file))
				{
					string line = readingFile.ReadLine();
					if (line.Trim().ToUpper() == sgc.GENERATE_TOGGLE.Trim())
						return true;
				}
			WriteFile(src_fs_file, GenerateGLSLCode(sl, sgc.ShaderType.Fragment, pass_index));

			return true;
		}

		public static HashSet<string> GenerateAllI3dSubShaderKeywords(ShaderLabShader sl)
		{
			HashSet<string> keywordDic = new HashSet<string>();
			if (sl.subshaders.Length == 0)
				return keywordDic;
				
			// 目前默认只用第0个subshader
			ShaderLabSubShader subshader = sl.subshaders[0];

			// 得到所有keyword
			foreach (var pass in subshader.passes)
			{
				foreach (ShaderLabPlatform platform in pass.platforms)
				foreach (string keyword in platform.keywords)
					keywordDic.Add(keyword);	
			}
			return keywordDic;
		}

		public static bool GenerateI3dShader(string src_file, ShaderLabShader sl, int pass_index)
		{
			if (sl == null)
				return false;
	
			// 用户强制不生成i3dShader ，即shader里写 “// AUTO GENERATE OFF”
			if (sl.IDCode != null && sl.IDCode.IndexOf(sgc.GENERATE_TOGGLE) != -1)
                return true;

			// 用户强制不生成i3dShader，即i3dShader里写 “// AUTO GENERATE OFF”
			if (File.Exists(src_file))
				using (System.IO.StreamReader readingFile = new System.IO.StreamReader(src_file))
				{
					string line = readingFile.ReadLine();
					if (line.Trim().ToUpper() == sgc.GENERATE_TOGGLE.Trim())
						return true;
				}
			
			if (sl.subshaders.Length == 0)
				return false;
			// 目前默认只用第0个subshader
			ShaderLabSubShader subshader = sl.subshaders[0];
			if (subshader.passes.Length < pass_index + 1)
				return false;
			ShaderLabPass pass =subshader.passes[pass_index];

			// 是否是一个阴影pass
			bool isShadowPass = false;
			for (int i = 0; i < pass.state.tags.Length / 2; i += 2)
				if (pass.state.tags[i] == "LIGHTMODE" && pass.state.tags[i + 1] == "SHADOWCASTER")
					isShadowPass = true;
			
			if (isShadowPass)
			{
				WriteFile(src_file, sgc.SHADOWCASTER_CODE);
				return true;
			}
			
			string ret = string.Empty;

			// Keywords
			ret += "#ifdef KEYWORD_SECTION\n";

			// 得到所有keyword
			HashSet<string> keywordDic = new HashSet<string>();
			foreach (ShaderLabPlatform platform in pass.platforms)
				foreach (string keyword in platform.keywords)
					keywordDic.Add(keyword);
			foreach (string keyword in keywordDic)
				ret += "#define " + keyword + "\n";
			ret += "#endif\n";

			ret += "\n";
			// 头文件
			{
				ret += "#ifdef VERTEX\n";
				ret += sgc.INCLUDE_DEFINATION[0] + sgc.INCLUDE_COMPATIBLE[0] + '\n';
				ret += "#endif\n";

				ret += "\n";

				ret += "#ifdef FRAGMENT\n";
				ret += sgc.INCLUDE_DEFINATION[1] + sgc.INCLUDE_COMPATIBLE[1] + '\n';
				ret += "#endif\n";
			}

			ret += "\n";
			// 遍历所有variant，输出keywords，输出vs和fs代码段
			{
				foreach (ShaderLabPlatform platform in pass.platforms)
				{
					if (platform.name == "gles")
					{
						// keywords
						if (platform.keywords.Length == 0)
						{
							bool first = true;
							foreach (var item in keywordDic)
							{
								if (first)
								{
									ret += "#if !defined(" + item + ")";
									first = false;
								}
								else 
									ret += " && !defined(" + item + ")";
							}
							ret += "\n";
						}
						else
						{
							ret += "#if defined(" + platform.keywords[0] + ")";
							HashSet<string> tmpSet = new HashSet<string>() {platform.keywords[0]};
							for (int i = 1; i < platform.keywords.Length; i ++)
							{
								ret += " && defined(" + platform.keywords[i] + ")";
								tmpSet.Add(platform.keywords[i]);
							}
							HashSet<string> negativeSet = new HashSet<string>(keywordDic);
							negativeSet.ExceptWith(tmpSet);
							foreach (var item in negativeSet)
								ret += " && !defined(" + item + ")";

							ret += "\n";
						}

						ret += "\n";

						ret += GetSnippets(platform, sgc.ShaderType.Vertex);
						ret += "\n";
						ret += GetSnippets(platform, sgc.ShaderType.Fragment);

						if (keywordDic.Count != 0)
							ret += "#endif\n";

						ret += "\n";
					}
				}
			}

			WriteFile(src_file, ret);
			return true;
		}
		
		public static void ClearCache()
		{
			UtilityFileSystem.DirectoryDeleteRF(sgc.DEFAULT_JSON_FILE_PATH);
		}

		private static string GetSnippets(ShaderLabPlatform platform, sgc.ShaderType type)
		{
			ShaderLabGLSL current = (type == sgc.ShaderType.Vertex) ? platform.vertex : platform.fragment;
			string ret = (type == sgc.ShaderType.Vertex) ? "#ifdef VERTEX\n" : "#ifdef FRAGMENT\n";

			List<string> variables = new List<string>();
			string mainCode = string.Empty;
			// add uniforms
			for (int i = 0; i < current.uniform_name.Length; i ++)
				if (!UtilityShaderGenerator.IsExcludedUniform(current.uniform_name[i], (int)type))
					variables.Add(current.uniform_string[i]);
			// add varyings
			for (int i = 0; i < current.varying_string.Length; i ++)
				variables.Add(current.varying_string[i]);
			// add main code
			mainCode = current.main_string.Replace("#version 100", " ");
			mainCode = mainCode.Replace("#extension GL_EXT_shader_texture_lod : enable", " ");

			// initialize local variables in mainCode
            // initializeLocalVariables(ref mainCode);

			// find void main ()
			int pos = mainCode.IndexOf("void main ()");

			ret += mainCode.Substring(0, pos);
			foreach (var variable in variables)
                ret += variable + ";\n";
			ret += mainCode.Substring(pos);
			ret += "#endif\n";

#if UNITY_2017
			// replace all "lowp " and "mediump " to "highp "
			ret = ret.Replace("lowp ", "highp ");
			ret = ret.Replace("mediump ", "highp ");
#endif

#if UNITY_2019
			// 适配mat4
			Regex r = new Regex(@"(hlslcc_mtx4x4unity_WorldToShadow)\[(\d)\]\.");
			ret = r.Replace(ret, @"vec4($1[$2])." );
#endif
			return ret;
		}
		
        private static string GenerateGLSLCode(ShaderLabShader shaderLabShader, sgc.ShaderType type, int passIndex)
        {
            string ret = sgc.INCLUDE_DEFINATION[(int)type] + sgc.INCLUDE_COMPATIBLE[(int)type] + '\n';
            ret += GetSnippets(shaderLabShader, type, passIndex);
            return ret;
        }

		private static string GetSnippets(ShaderLabShader shaderLabShader, sgc.ShaderType type, int passIndex)
		{
			// 1. Extract mainCode and uniforms and varyings
			List<string> variables = new List<string>();
			string mainCode ="";
			if (shaderLabShader.subshaders.Length > 0)
			{
				ShaderLabSubShader subshader = shaderLabShader.subshaders[0];
				if (subshader.passes.Length > 0)
				{
                    if (passIndex >= subshader.passes.Length)
                    {
                        Debug.Log("Error: " + shaderLabShader.name + "doesn't have a pass index of " + passIndex);
                        return "";
                    }
					ShaderLabPass pass = subshader.passes[passIndex];

					foreach (ShaderLabPlatform platform in pass.platforms)
					{
						if (platform.name == "gles")
						{
							bool directionalKeywordFlag = false;
							// bool shadowScreenKeywordFlag = false;
							// bool shadowDepthKeywordFlag = false;
							if (platform.keywords.Length == 0)
							{
								directionalKeywordFlag = true;
								// shadowScreenKeywordFlag = true;
							}
							else
							{
								for (int i = 0; i < platform.keywords.Length; i ++)
								{
									if (platform.keywords[i] == "DIRECTIONAL")
										directionalKeywordFlag = true;
									// if (variant.keywords[i] == "SHADOWS_SCREEN")
									// 	shadowScreenKeywordFlag = true;
									// if (variant.keywords[i] == "SHADOWS_DEPTH")
									// 	shadowDepthKeywordFlag = true;
								}
							}
								
							// if (directionalKeywordFlag && shadowScreenKeywordFlag || shadowDepthKeywordFlag)
							if (directionalKeywordFlag)
							{
								ShaderLabGLSL current = (type == sgc.ShaderType.Vertex) ? platform.vertex : platform.fragment;
								// add uniforms
								for (int i = 0; i < current.uniform_name.Length; i ++)
									if (!UtilityShaderGenerator.IsExcludedUniform(current.uniform_name[i], (int)type))
										variables.Add(current.uniform_string[i]);
								// for (int i = 0; i < shaderLabShader.properties.Length; i ++)
								// 	for (int j = 0; j < current.uniform_name.Length; j ++)
								// 	{
								// 		if (shaderLabShader.properties[i].variable == current.uniform_name[j]
								// 			|| ((int)shaderLabShader.properties[i].type > 3 
								// 				&&  (shaderLabShader.properties[i].variable + "_ST") == current.uniform_name[j]))
								// 			variables.Add(current.uniform_string[j]);
								// 	}
								// add varyings
								for (int i = 0; i < current.varying_string.Length; i ++)
									variables.Add(current.varying_string[i]);
								// add main code
								mainCode = current.main_string;
								break;
							}
						}
					}
				}
			}

			// 2. initialize local variables in mainCode
            initializeLocalVariables(ref mainCode);

			// 3. add all parts
            string ret = string.Empty;
			foreach (var variable in variables)
                ret += variable + ";\n";
            ret += mainCode;
			return ret;
		}

		private static string GetTimeStamp(string fileName)
        {
			if (!File.Exists(fileName))
				return sgc.GENERATE_TOGGLE;

            System.TimeSpan span= File.GetLastWriteTime(fileName).Subtract(new System.DateTime(1970,1,1,0,0,0, System.DateTimeKind.Utc));
            return "// " + span.TotalSeconds.ToString() + '\n';
        }

		// 几种需要重新生成情况：1. 没有json 2. 时间戳不一样 
        private static bool IsJsonNeedRegenerate(string sourceShaderFile, string jsonFile, out ShaderLabShader sl)
        {
			sl = null;

			// 1. 没有json 返回true
			if (!File.Exists(jsonFile))
				return true;

            string timeStamp = GetTimeStamp(sourceShaderFile);

			string jsonContent = File.ReadAllText(jsonFile, System.Text.Encoding.UTF8);
			sl = JsonUtility.FromJson<ShaderLabShader>(jsonContent);

			if (timeStamp == sgc.GENERATE_TOGGLE)
				return false;

			// 2. 时间戳不一样 返回true
			string oldTimeStamp;
			if (sl.IDCode.IndexOf(sgc.GENERATE_TOGGLE) != -1)
				oldTimeStamp = sl.IDCode.Substring(0, sl.IDCode.IndexOf(sgc.GENERATE_TOGGLE));
			else
				oldTimeStamp = sl.IDCode;

            if (oldTimeStamp != timeStamp)
				return true;
			
			// 返回false，sl不为空
			return false;

        }

       	// return true - has been modified
        // return false - no need to modified
        private static bool AdjustShaderVariables(string filePath, string text)
        {
            string finalText = "";
            int strPoint = 0;
            string remain = text;
            while (-1 != (strPoint = remain.IndexOf(sgc.VariablesName[(int) sgc.VariablesType.Uniform])))
            {
                finalText += remain.Substring(0, strPoint);
                remain = remain.Substring(strPoint);
                string statement = remain.Substring(0, remain.IndexOf(';') + 1);

                int typePosition;
                UtilityShaderGenerator.Declaration declaration = UtilityShaderGenerator.GetCGDeclaration(statement, out typePosition);
                if (-1 != declaration.type.IndexOf("float2"))
                {
                    statement = statement.Substring(0, typePosition) + 
                                    declaration.type.Replace("float2", "float4") + " " + declaration.name;
                }
                else if (-1 != declaration.type.IndexOf("float3"))
                {
                    statement = statement.Substring(0, typePosition) + 
                                    declaration.type.Replace("float3", "float4") + " " + declaration.name;
                }
                
                finalText += statement;
                remain = remain.Substring(remain.IndexOf(';') + 1);
            }

            finalText += remain;
            if (text.Trim().Equals(finalText.Trim()))
            {
                return false;
            }

            WriteFile(filePath, finalText);
            return true;
        }

        static void initializeLocalVariables(ref string mainCode)
        {
            // check every statment
            string[] statments = mainCode.Split(';');

            // Clear for regeneration
            mainCode = string.Empty;

            foreach (var statement in statments)
            {
                UtilityShaderGenerator.Declaration declaration = UtilityShaderGenerator.GetGLSLDeclaration(statement);
                string ret = statement;
                // Make sure the declaration is found and without initialization
                if (declaration.name.Length > 0 && (-1 == statement.IndexOf('=')))
                {
                    string value = string.Empty;
                    if (sgc.GLSLInitializeDict.TryGetValue(declaration.type, out value))
                        ret += value;
                }
                mainCode += ret + ';';
            }

            // Remove the last ';' that the forloop added
			int last_index_of = mainCode.LastIndexOf(';');
			if( last_index_of > 0 )
				mainCode = mainCode.Remove( last_index_of - 1 );
        }

        static string GetCompiledShaderPath(Shader shader)
        {
            System.Reflection.MethodInfo openShader = typeof(ShaderUtil).GetMethod("OpenCompiledShader", 
                                                      System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            openShader.Invoke(null, new object[] { shader, sgc.EXPORT_MODE, 
                                                    sgc.EXPORT_PLATFORMS_MASK, false });

            string compliedShaderFile = ExporterConfig.TEMPORARY_PATH + sgc.COMPILED_NAME_PREFIX + shader.name.Replace('/','-') + ExporterConfig.SHADER_EXTENSION;
            return compliedShaderFile;
        }

        static string ReadFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string ret;
            using (sr)
            {
                ret = sr.ReadToEnd();
            }
            return ret;
        }

        static void WriteFile(string filename, string content)
        {
            StreamWriter sw = new StreamWriter(filename);
            using (sw)
            {
                sw.WriteLine(content);
            }

        }
    }
}

