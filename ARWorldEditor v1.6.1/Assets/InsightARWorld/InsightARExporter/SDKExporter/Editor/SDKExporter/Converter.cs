/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using ARWorldEditor;
using UnityEngine;

namespace RenderEngine
{
	class Converter
	{
		public static ProtoWorld.Shader ConvertShader
			( ExporterConfig config
			, ShaderLabShader json_shader
			, int pass_index
			, string src_path_file_ext)
		{
			// 因为有多个pass，所以除了第0个pass外，其他pass命名要加后缀
			string name_index = (pass_index == 0) ? "" : ("_" + pass_index);
			ProtoWorld.Shader ret = new ProtoWorld.Shader
			{ Version = ExporterConfig.VERSION, Name = json_shader.name + name_index };

			// copy shaders
			string src_path = Path.GetDirectoryName( src_path_file_ext ) + "/";
			string src_name = Path.GetFileNameWithoutExtension( src_path_file_ext );

			string src_vs_file = src_path + src_name + ExporterConfig.SHADER_COMPILED_VS_EXTENSION;
			string src_fs_file = src_path + src_name + ExporterConfig.SHADER_COMPILED_FS_EXTENSION;

			// 如果不是内置shader，则生成vs和fs（自动检测是否修改，如shader无修改则不生成新的）
			ShaderGenerator.GenerateVSFS( src_vs_file, src_fs_file, json_shader, pass_index );

			string dest_vs_file = config.root_path + src_path + src_name + ExporterConfig.SHADER_COMPILED_VS_EXTENSION;
			string dest_fs_file = config.root_path + src_path + src_name + ExporterConfig.SHADER_COMPILED_FS_EXTENSION;

			UtilityFileSystem.CopyFile( src_vs_file , dest_vs_file , true );
			UtilityFileSystem.CopyFile( src_fs_file , dest_fs_file , true );

			ret.CpuBone = true;
			ret.VsFile = src_vs_file;
			ret.FsFile = src_fs_file;

			// return
			return ret;
		}

		public static ProtoWorld.ReflectionProbe ConvertReflectionProbe(ExporterConfig config, UnityEngine.ReflectionProbe reflectionProbe)
		{
			ProtoWorld.ReflectionProbe ret = new ProtoWorld.ReflectionProbe
			{
				Pos = UtilityConverter.ConvertVector4(new UnityEngine.Vector4(reflectionProbe.transform.position.x, reflectionProbe.transform.position.y, reflectionProbe.transform.position.z, 1.0f)),
				Min = UtilityConverter.ConvertVector4(new UnityEngine.Vector4(reflectionProbe.bounds.min.x, reflectionProbe.bounds.min.y, reflectionProbe.bounds.min.z, 0) ),
				Max = UtilityConverter.ConvertVector4(new UnityEngine.Vector4(reflectionProbe.bounds.max.x, reflectionProbe.bounds.max.y, reflectionProbe.bounds.max.z, 1) )
			};

			ret.Filename = ExporterResource.ExportReflectionProbe(config, reflectionProbe);
			// string exrTemporaryDirectory = ExporterConfig.TEMPORARY_PATH;
			// if (!System.IO.Directory.Exists(exrTemporaryDirectory))
			// {
			// 	System.IO.Directory.CreateDirectory(exrTemporaryDirectory);
			// }
			// string exrTemporaryName = "EnvRefProbe";
			// string exrTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, exrTemporaryName + ".exr");
			// string pngTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, exrTemporaryName);

			// // Create an empty file first to avoid 'textureimporter'
			// FileStream exrStream = File.Create(exrTemporaryPath);
			// exrStream.Close();

			// // Generate exr file
			// if (UnityEditor.Lightmapping.BakeReflectionProbe(reflectionProbe, exrTemporaryPath))
			// {
			// 	ret.Hdr = UtilityConverter.ConvertVector4(reflectionProbe.textureHDRDecodeValues);

			// 	string err = UtilityExecute.ExecuteTool
			// 			( "exr2png" , exrTemporaryPath + ' ' + pngTemporaryPath + " 6");
			// 	if (err.Length > 0)
			// 		UnityEngine.Debug.Log( err );

			// 	ret.Filename = ExporterResource.Export6SidedCubemapTexture(config, config.cubemap_name, pngTemporaryPath);
			// }

			ret.Hdr = UtilityConverter.ConvertVector4(reflectionProbe.textureHDRDecodeValues);
			return ret;
		}

		public static ProtoWorld.Pass ConvertPass
			( ExporterConfig config
			, ProtoWorld.Material material
			, ShaderLabShader json_shader
			, int subshader_index
			, int pass_index , int buildin_shader_index)
		{
			ProtoWorld.Pass ret = new ProtoWorld.Pass();
			ret.Name = pass_index.ToString();

			if (ExporterConfig.USE_NEW_SHADER_FORMAT)
				ret.ShaderFile = ExporterResource.ExportI3dShader(config, json_shader, subshader_index, pass_index, buildin_shader_index);
			else
				ret.ShaderFile = ExporterResource.ExportShader( config , json_shader , subshader_index, pass_index, buildin_shader_index);

			// export state
			ShaderLabState state = json_shader.subshaders[subshader_index].passes[pass_index].state;

			ret.DepthFunc = (ProtoWorld.DEPTH_FUNC) ShaderLabState.GetRealValue(state.zTest, material);
			ret.DepthWrite = ShaderLabState.GetRealValue(state.zWrite, material) == 0 ? false : true;

			ret.CullMode = ShaderLabType.GetProtoCullMode((ShaderLabType.CullMode) ShaderLabState.GetRealValue(state.culling, material));
			ret.BlendColor = ShaderLabType.GetProtoBlendOp((ShaderLabType.BlendOp) ShaderLabState.GetRealValue(state.blendOp, material));
			ret.BlendAlpha = ShaderLabType.GetProtoBlendOp((ShaderLabType.BlendOp) ShaderLabState.GetRealValue(state.blendOpAlpha, material));
			ret.BlendColorSrc = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) ShaderLabState.GetRealValue(state.srcBlend, material));
			ret.BlendColorDest = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) ShaderLabState.GetRealValue(state.destBlend, material));
			ret.BlendAlphaSrc = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) ShaderLabState.GetRealValue(state.srcBlendAlpha, material));
			ret.BlendAlphaDest = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) ShaderLabState.GetRealValue(state.destBlendAlpha, material));

			ret.ColorMask = (int) ShaderLabState.GetRealValue(state.colorMask, material);
			ret.AlphaToMask = (int) ShaderLabState.GetRealValue(state.alphaToMask, material);
			ret.OffsetFactor = (int) ShaderLabState.GetRealValue(state.offsetFactor, material);
			ret.OffsetUnits = (int) ShaderLabState.GetRealValue(state.offsetUnits, material);

			ret.StencilRef = (int) ShaderLabState.GetRealValue(state.stencilRef, material);
			ret.StencilReadMask = (int) ShaderLabState.GetRealValue(state.stencilReadMask, material);
			ret.StencilWriteMask = (int) ShaderLabState.GetRealValue(state.stencilWriteMask, material);
			ret.StencilComp = (ProtoWorld.DEPTH_FUNC) ShaderLabState.GetRealValue(state.stencilComp, material);
			ret.StencilOpPass = (ProtoWorld.STENCIL_OP) ShaderLabState.GetRealValue(state.stencilOpPass, material);
			ret.StencilOpFail = (ProtoWorld.STENCIL_OP) ShaderLabState.GetRealValue(state.stencilOpFail, material);
			ret.StencilOpZfail = (ProtoWorld.STENCIL_OP) ShaderLabState.GetRealValue(state.stencilOpZFail, material);


			ret.LightMode = ProtoWorld.LIGHT_MODE.Always;
			// export tags
			for (int i = 0; i < state.tags.Length / 2; i += 2)
			{
				ret.Tags.Add(state.tags[i], state.tags[i + 1]);
				if (state.tags[i] == "LIGHTMODE")
				{
					switch (state.tags[i + 1])
					{
						case "ALWAYS": ret.LightMode = ProtoWorld.LIGHT_MODE.Always; break;
						case "FORWARDBASE": ret.LightMode = ProtoWorld.LIGHT_MODE.ForwardBase; break;
						case "FORWARDADD": ret.LightMode = ProtoWorld.LIGHT_MODE.ForwardAdd; break;
						case "DEFERRED": ret.LightMode = ProtoWorld.LIGHT_MODE.Deffered; break;
						case "SHADOWCASTER": ret.LightMode = ProtoWorld.LIGHT_MODE.ShadowCaster; break;
						case "MOTIONVECTORS": ret.LightMode = ProtoWorld.LIGHT_MODE.MotionVectors; break;
						case "PREPASSBASE": ret.LightMode = ProtoWorld.LIGHT_MODE.PrePassBase; break;
						case "PREPASSFINAL": ret.LightMode = ProtoWorld.LIGHT_MODE.PrePassFinal; break;
						case "VERTEX": ret.LightMode = ProtoWorld.LIGHT_MODE.Vertex; break;
						case "VERTEXLMRGBM": ret.LightMode = ProtoWorld.LIGHT_MODE.VertexLmrgbm; break;
						case "VERTEXLM": ret.LightMode = ProtoWorld.LIGHT_MODE.VertexLM; break;
					}
				}
			}

			return ret;
		}

		public static ProtoWorld.Texture ConvertTexture( ExporterConfig config , UnityEngine.Texture texture , UnityEngine.Vector4 transform , string filename )
		{
			ProtoWorld.Texture ret = new ProtoWorld.Texture{ Version = ExporterConfig.VERSION , File = filename , Transform = UtilityConverter.ConvertVector4( transform ) };
			if( null != texture )
			{
				ret.Width = texture.width;
				ret.Height = texture.height;

				ret.WrapU = UtilityConverter.ConvertWrap( texture.wrapMode );
				ret.WrapV = UtilityConverter.ConvertWrap( texture.wrapMode );
				ret.WrapW = UtilityConverter.ConvertWrap( texture.wrapMode );

				ret.Filter = UtilityConverter.ConvertFilter( texture.filterMode );

				if( texture.GetType().UnderlyingSystemType == typeof( UnityEngine.RenderTexture ) )
				{

					UnityEngine.RenderTexture rt = ( UnityEngine.RenderTexture )texture;

					ret.Type = ProtoWorld.TEXTURE_TYPE.RenderTexture2D;
					ret.Format = UtilityConverter.ConvertFormat( rt.format );
					ret.Depth = 1;
					ret.GenerateMipmap = false;
					ret.RenderTarget = true;

					ret.SizeType = ProtoWorld.RENDER_TEXTURE_SIZE_TYPE.AbsoluteSizeInPixel;
					ret.RelativeToScreenW = 0;
					ret.RelativeToScreenH = 0;
					ret.RelativeToScreenD = 0;

					string ext_path_file_ext = UtilityFileSystem.ReplaceExtension( config.asset_path.GetPath( rt ) , ".asset" );
					EngineRenderTexture ert = ( EngineRenderTexture )UnityEditor.AssetDatabase.LoadAssetAtPath( ext_path_file_ext , typeof( EngineRenderTexture ) );
					if( null != ert )
					{
						switch( ert.size_type )
						{
						default:
						case RENDER_TEXTURE_SIZE_TYPE.AbsoluteSizeInPixel:
							ret.SizeType = ProtoWorld.RENDER_TEXTURE_SIZE_TYPE.AbsoluteSizeInPixel;
							ret.Width = ert.abs_width;
							ret.Height = ert.abs_height;
							ret.RelativeToScreenW = 0;
							ret.RelativeToScreenH = 0;
							break;
						case RENDER_TEXTURE_SIZE_TYPE.RelativeToScreen:
							ret.SizeType = ProtoWorld.RENDER_TEXTURE_SIZE_TYPE.RelativeToScreen;
							ret.Width = 0;
							ret.Height = 0;
							ret.RelativeToScreenW = ert.rel_width;
							ret.RelativeToScreenH = ert.rel_height;
							break;
						}
					}
				}
				else
				{
					switch( texture.dimension )
					{
					case UnityEngine.Rendering.TextureDimension.Tex2D:
						UnityEngine.Texture2D tex2d = (UnityEngine.Texture2D)texture;
						ret.Type = ProtoWorld.TEXTURE_TYPE.Tex2D;
						ret.Format = UtilityConverter.ConvertFormat( tex2d.format );
						ret.Depth = 1;
						ret.GenerateMipmap = tex2d.mipmapCount > 1;
						break;
					case UnityEngine.Rendering.TextureDimension.Tex3D:
						UnityEngine.Texture3D tex3d = (UnityEngine.Texture3D)texture;
						ret.Type = ProtoWorld.TEXTURE_TYPE.Tex3D;
						ret.Format = UtilityConverter.ConvertFormat( tex3d.format );
						ret.Depth = tex3d.depth;
						ret.GenerateMipmap = false; // ((UnityEngine.Texture3D)texture);
						break;
					case UnityEngine.Rendering.TextureDimension.Cube:
						UnityEngine.Cubemap texcube = (UnityEngine.Cubemap)texture;
						ret.Type = ProtoWorld.TEXTURE_TYPE.Cube;
						ret.Format = UtilityConverter.ConvertFormat( texcube.format );
						ret.Depth = 1;
						ret.GenerateMipmap = texcube.mipmapCount > 1;
						break;
					case UnityEngine.Rendering.TextureDimension.Tex2DArray:
						UnityEngine.Texture2DArray tex2darray = (UnityEngine.Texture2DArray)texture;
						ret.Type = ProtoWorld.TEXTURE_TYPE.Tex2DArray;
						ret.Format = UtilityConverter.ConvertFormat( tex2darray.format );
						ret.Depth = tex2darray.depth;
						ret.GenerateMipmap = false; // ((UnityEngine.Texture2DArray)texture);
						break;
					}
					ret.RenderTarget = false;
					ret.SizeType = ProtoWorld.RENDER_TEXTURE_SIZE_TYPE.Count;
					ret.RelativeToScreenW = 0;
					ret.RelativeToScreenH = 0;
					ret.RelativeToScreenD = 0;
				}
			}
			else
			{
				ret.Type = ProtoWorld.TEXTURE_TYPE.Any;
				ret.WrapU = ProtoWorld.TEXTURE_WRAP.WrapRepeat;
				ret.WrapV = ProtoWorld.TEXTURE_WRAP.WrapRepeat;
				ret.WrapW = ProtoWorld.TEXTURE_WRAP.WrapRepeat;

				ret.Filter = ProtoWorld.TEXTURE_FILTER.FilterBilinear;

				ret.GenerateMipmap = false;
				ret.RenderTarget = false;
			}
			return ret;
		}

		public static ProtoWorld.Material ConvertMaterial( ExporterConfig config
			, UnityEngine.Material material , string dest_file , bool skin_mesh
			, UnityEngine.Renderer renderer
			, bool [] using_depth_of_render_texture )
		{
			UnityEngine.Material materialInUse = material;
			// -----针对 Standard & Standard (Specular setup)特殊处理：
			if (material.shader.name == "Standard")
			{
				UnityEngine.Shader standardShader = UnityEngine.Shader.Find("i3dEngine/Standard");
				if (material.name == "Default-Material")
					materialInUse = new UnityEngine.Material(standardShader);
				else
					materialInUse = new UnityEngine.Material(material);

				bool transparent = material.GetTag( ExporterConfig.SHADER_TAGS.RenderType.ToString() , true ).Equals( "Transparent" );
				if (transparent)
					materialInUse.shader = UnityEngine.Shader.Find("i3dEngine/Standard Transparent");
				else
					materialInUse.shader = standardShader;
			}
			else if (material.shader.name == "Standard (Specular setup)")
			{
				UnityEngine.Shader specularShader = UnityEngine.Shader.Find("i3dEngine/Standard Specular");

				if (material.name == "Default-Material")
					materialInUse = new UnityEngine.Material(specularShader);
				else
					materialInUse = new UnityEngine.Material(material);

				bool transparent = material.GetTag( ExporterConfig.SHADER_TAGS.RenderType.ToString() , true ).Equals( "Transparent" );
				if (transparent)
					materialInUse.shader = UnityEngine.Shader.Find("i3dEngine/Standard Specular Transparent");
				else
					materialInUse.shader = specularShader;
			}
			// -----------


			// find material in world
			ProtoWorld.Material ret;

			// new material
			ret = new ProtoWorld.Material{ Version = ExporterConfig.VERSION , Name = materialInUse.name , File = dest_file };

			UnityEngine.Shader shader = materialInUse.shader;

			int buildin_shader_index = ExporterUtility.GetBuildinShaderPath( shader.name );
			ShaderLabShader json_shader = ShaderGenerator.GenerateJson(shader);
			// TODO how to use different subshader?
			ShaderLabSubShader subshader = json_shader.subshaders[0];


			// export keywords
			{
				HashSet<string> allKeywordsHashSet = ShaderGenerator.GenerateAllI3dSubShaderKeywords(json_shader);
				HashSet<string> customKeywordsHashSet = new HashSet<string>();
				foreach (var keyword in materialInUse.shaderKeywords)
					customKeywordsHashSet.Add(keyword);

				HashSet<string> buildinKeywordHashSet = new HashSet<string>(allKeywordsHashSet);
				buildinKeywordHashSet.ExceptWith(customKeywordsHashSet);

				bool mainLightShadow = false;
				UnityEngine.Light[] lights = UnityEngine.GameObject.FindObjectsOfType<UnityEngine.Light>();
				if (lights.Length > 0 && lights[lights.Length - 1].shadows > 0)
					mainLightShadow = true;

				// buildin keywords
				foreach (string keyword in buildinKeywordHashSet)
				{
					switch (keyword)
					{
						case "DIRECTIONAL":
							ret.Keywords.Add( keyword, true);
							break;
						case "LIGHTPROBE_SH":
							ret.Keywords.Add( keyword, true);
							break;
						case "SHADOWS_SCREEN":
							if (renderer && renderer.receiveShadows && UnityEngine.QualitySettings.shadows > 0 && mainLightShadow)
								ret.Keywords.Add( keyword, true);
							else
								ret.Keywords.Add( keyword, false);
							break;
						case "SHADOWS_DEPTH":
							if (renderer && renderer.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off)
								ret.Keywords.Add( "SHADOWS_DEPTH", true);
							else
								ret.Keywords.Add( "SHADOWS_DEPTH", false);
							break;
						default:
							ret.Keywords.Add(keyword, materialInUse.IsKeywordEnabled( keyword ));
							break;
					}
				}

				// custom keywords
				foreach (string keyword in customKeywordsHashSet)
					ret.Keywords.Add( keyword , materialInUse.IsKeywordEnabled( keyword ) );

				// shadow keywords
				if(mainLightShadow && renderer && renderer.receiveShadows && UnityEngine.QualitySettings.shadows > 0)
				{
					bool soft = (lights[lights.Length - 1].shadows == LightShadows.Soft);
					Debug.Log("material : " + material.name + ", soft : " + soft.ToString());
					if(!ret.Keywords.ContainsKey("USE_SHADOWMAP"))
						ret.Keywords.Add( "USE_SHADOWMAP" , true );
					if(!ret.Keywords.ContainsKey("SHADOWS_SOFT"))
						ret.Keywords.Add( "SHADOWS_SOFT" , soft );
				}
				else{
					Debug.Log("material : " + material.name + " use shadowmap false");
					if(!ret.Keywords.ContainsKey("USE_SHADOWMAP"))
						ret.Keywords.Add( "USE_SHADOWMAP" , false );
				}
			}

			// export properties
			{
				int num = ShaderUtility.GetPropertyCount( shader );
				for( int pi = 0 ; pi < num ; ++pi )
				{
					string pname = ShaderUtility.GetPropertyName( shader , pi );
					//UnityEngine.Debug.Log( "GetPropertyName: " + pname );
					switch( ShaderUtility.GetPropertyType( shader , pi ) )
					{
					case ShaderUtility.TYPE.Color:
						UnityEngine.Color pcolor = materialInUse.GetColor( pname );
						ret.Colors.Add( new ProtoWorld.MPColor
							{ Name = pname
									, Value = new ProtoMath.float4
								{ X = pcolor.r , Y = pcolor.g , Z = pcolor.b , W = pcolor.a } } );
						break;
					case ShaderUtility.TYPE.Vector:
						UnityEngine.Vector4 pvec = materialInUse.GetVector( pname );
						ret.Float4S.Add( new ProtoWorld.MPFloat4
							{ Name = pname ,
								Value = new ProtoMath.float4
								{ X = pvec.x , Y = pvec.y , Z = pvec.z , W = pvec.w } } );
						break;
					case ShaderUtility.TYPE.Float:
						float pfloat = materialInUse.GetFloat( pname );
						ret.Floats.Add( new ProtoWorld.MPFloat
							{ Name = pname , Value = pfloat } );
						break;
					case ShaderUtility.TYPE.Range:
						float prange = materialInUse.GetFloat( pname );
						float pmin = ShaderUtility.GetRangeLimits( shader , pi , 1 );
						float pmax = ShaderUtility.GetRangeLimits( shader , pi , 2 );
						float pdefault = ShaderUtility.GetRangeLimits( shader , pi , 0 );
						ret.Ranges.Add( new ProtoWorld.MPRange
							{ Name = pname , Value = prange , Min = pmin , Max = pmax , Default = pdefault } );
						break;
					case ShaderUtility.TYPE.TexEnv:
						UnityEngine.Texture utex = materialInUse.GetTexture( pname );
						UnityEngine.Vector2 transform_scale = materialInUse.GetTextureScale( pname );
						UnityEngine.Vector2 transform_offset = materialInUse.GetTextureOffset( pname );
						UnityEngine.Vector4 transform = new UnityEngine.Vector4( transform_scale.x , transform_scale.y , transform_offset.x , transform_offset.y );
						if( null != utex )
						{
							string path_to_texture = ExporterResource.ExportTextureTask( config, utex, transform );
							if( null != path_to_texture )
							if( path_to_texture.Length > 0 )
								ret.Textures.Add( new ProtoWorld.MPTexture
									{ Name = pname , Value = path_to_texture
											, UseDepth = using_depth_of_render_texture != null
											? using_depth_of_render_texture[pi] : false } );
						}
						else // use default textures instead
						{
							foreach (var prop in json_shader.properties)
							{
								if ((prop.type == ShaderLabType.Property.kTexture2D
									|| prop.type == ShaderLabType.Property.kTexture3D
									|| prop.type == ShaderLabType.Property.kTextureCube)
									&& prop.variable == pname)
									{
										string buildin_texture_path = ExporterUtility.GetBuildinShaderDefaultTexturePath( prop.textureName );
										ret.Textures.Add( new ProtoWorld.MPTexture
											{ Name = pname , Value = buildin_texture_path , UseDepth = false } );
										break;
									}
							}
						}

						break;
					}
				}

				// add "texture_ST" if needed
				// foreach (var tex in ret.Textures)
				// {
				// 	bool existing_ST = false;
				// 	foreach (var v4 in ret.Float4S)
				// 		if (v4.Name == tex.Name + "_ST"){ existing_ST = true; break; }
				// 	if (!existing_ST)
				// 	{
				// 		UnityEngine.Vector2 transform_scale = material.GetTextureScale( tex.Name );
				// 		UnityEngine.Vector2 transform_offset = material.GetTextureOffset( tex.Name );
				// 		ret.Float4S.Add( new ProtoWorld.MPFloat4
				// 		{ Name = tex.Name + "_ST" ,
				// 			Value = new ProtoMath.float4
				// 			{ X = transform_scale.x , Y = transform_scale.y , Z = transform_offset.x , W = transform_offset.y } } );
				// 	}
				// }
			}

			// export passes
			int seek;
			if (ExporterConfig.USE_NEW_SHADER_FORMAT)
			{

				for( seek = 0 ; seek < subshader.passes.Length ; ++ seek )
				{
					// exclude some passes like forwardadd, deferred
					if (ExporterUtility.IsExcludedPass_NewFormat(subshader.passes[seek].state.GetSpecficTagValue("LIGHTMODE")))
						continue;

					string usage = subshader.passes[seek].state.GetSpecficTagValue("LIGHTMODE");
					if (usage == "") usage = "ALWAYS";

					bool hasShadowDepth = ret.Keywords.ContainsKey("SHADOWS_DEPTH") ? ret.Keywords["SHADOWS_DEPTH"] : false;
					if(usage == "SHADOWCASTER" && !hasShadowDepth)
						continue;

					int index = -1;
					for (int i = 0; i < ret.PassGroups.Count; i ++)
					{
						if (ret.PassGroups[i].Usage == usage)
						{
							index = i;
							break;
						}
					}
					if (index == -1)
					{
						ProtoWorld.PassGroup passGroup = new ProtoWorld.PassGroup();
						passGroup.Usage = usage;
						passGroup.Passes.Add(ConvertPass( config , ret , json_shader , 0 , seek , buildin_shader_index ));
						ret.PassGroups.Add(passGroup);
					}
					else
					{
						ret.PassGroups[index].Passes.Add(ConvertPass( config , ret , json_shader , 0 , seek , buildin_shader_index ));
					}

					{
						// 是否需要额外添加一个阴影pass
						usage = subshader.passes[seek].state.GetSpecficTagValue("SHADOWSUPPORT");
						if (usage == "true")
						{
							if(!ret.Keywords.ContainsKey("SHADOWS_DEPTH"))
							{
								if (renderer && renderer.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off)
									ret.Keywords.Add( "SHADOWS_DEPTH", true);
								else
									ret.Keywords.Add( "SHADOWS_DEPTH", false);
							}
							if(!ret.Keywords["SHADOWS_DEPTH"])
								continue;

							usage = "SHADOWCASTER";
							index = -1;
							for (int i = 0; i < ret.PassGroups.Count; i ++)
							{
								if (ret.PassGroups[i].Usage == usage)
								{
									index = i;
									break;
								}
							}
							if (index == -1)
							{
								ProtoWorld.PassGroup passGroup = new ProtoWorld.PassGroup();
								passGroup.Usage = usage;

								ProtoWorld.Pass shadowPass = new ProtoWorld.Pass();
								shadowPass.Name = usage;
								
								// export state
								ShaderLabState state = new ShaderLabState();

								shadowPass.DepthFunc = (ProtoWorld.DEPTH_FUNC) 4;
								shadowPass.DepthWrite = true;

								shadowPass.CullMode = ShaderLabType.GetProtoCullMode((ShaderLabType.CullMode) 2);
								shadowPass.BlendColor = ShaderLabType.GetProtoBlendOp((ShaderLabType.BlendOp) 35);
								shadowPass.BlendAlpha = ShaderLabType.GetProtoBlendOp((ShaderLabType.BlendOp) 35);
								shadowPass.BlendColorSrc = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) 1);
								shadowPass.BlendColorDest = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) 0);
								shadowPass.BlendAlphaSrc = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) 1);
								shadowPass.BlendAlphaDest = ShaderLabType.GetProtoBlendMode((ShaderLabType.BlendMode) 0);

								shadowPass.ColorMask = 15;
								shadowPass.AlphaToMask = 0;
								shadowPass.OffsetFactor = 0;
								shadowPass.OffsetUnits = 0;

								shadowPass.StencilRef = 0;
								shadowPass.StencilReadMask = 255;
								shadowPass.StencilWriteMask = 255;
								shadowPass.StencilComp = (ProtoWorld.DEPTH_FUNC) 8;
								shadowPass.StencilOpPass = (ProtoWorld.STENCIL_OP) 0;
								shadowPass.StencilOpFail = (ProtoWorld.STENCIL_OP) 0;
								shadowPass.StencilOpZfail = (ProtoWorld.STENCIL_OP) 0;


								shadowPass.LightMode = ProtoWorld.LIGHT_MODE.ShadowCaster;

								bool needSrcFile = true;
								// 如果是内置的unity shader，但不是导出工具认为的内置shader（因为太多了我们不可能每个都顾及到）
								string srcpath = json_shader.srcpath;
								if (srcpath == ExporterConfig.UNITY_BUILDLIN_RESOURCES)
								{
									srcpath = json_shader.name.Replace(' ', '_');
									needSrcFile = false;
								}

								// 把阴影pass index设置为最后一个
								string src = Path.GetDirectoryName( srcpath ) + '/' + Path.GetFileNameWithoutExtension( srcpath ) + "_pass" + subshader.passes.Length;
								string dest = null;
								if( ExporterUtility.PrepareExport
									( src, config.root_path , ExporterConfig.I3DSHADER_EXTENSION , out dest ) )
								{
									if (needSrcFile)
									{
										string src_file = src + ExporterConfig.I3DSHADER_EXTENSION;
										StreamWriter sw = new StreamWriter(src_file);
										using (sw)
										{
											sw.WriteLine(ShaderGeneratorConfig.SHADOWCASTER_CODE);
										}
										UtilityFileSystem.CopyFile( src_file , config.root_path + dest , true );
									}
									else
									{
										// 如果是内置的unity shader，但不是导出工具认为的内置shader，则把srcfile的路径设为Assets根目录文件夹
										string src_file = "Assets/InsightARWorld/__Temp__/" + src + ExporterConfig.I3DSHADER_EXTENSION;
										if (!Directory.Exists(Path.GetDirectoryName(src_file)))
											Directory.CreateDirectory(Path.GetDirectoryName(src_file));
										
										StreamWriter sw = new StreamWriter(src_file);
										using (sw)
										{
											sw.WriteLine(ShaderGeneratorConfig.SHADOWCASTER_CODE);
										}
										UtilityFileSystem.CopyFile( src_file , config.root_path + dest , true );

										UtilityFileSystem.DirectoryDeleteRF("Assets/InsightARWorld/__Temp__/");
									}


								}

								shadowPass.ShaderFile = dest.Replace( "\\" , "/" );
								passGroup.Passes.Add(shadowPass);
								ret.PassGroups.Add(passGroup);
							}
						}
					}

				}
			}
			// 兼容旧版本shader
			else
			{
				// 添加一个无效的shadow pass占位置
				// ret.Passes.Add(new ProtoWorld.Pass());

				// exclude some passes like forwardadd, shadow caster, deferred
				for( seek = 0 ; seek < subshader.passes.Length ; ++ seek )
				{
					if (!ExporterUtility.IsExcludedPass(subshader.passes[seek].state.GetSpecficTagValue("LIGHTMODE")))
						ret.Passes.Add( ConvertPass( config , ret , json_shader , 0 , seek , buildin_shader_index) );
				}
			}


			// export lod
			ret.Lod = (int) ShaderLabState.GetRealValue(subshader.state.lod, ret);

			// export tags
			for (int i = 0; i < subshader.state.tags.Length / 2; i += 2)
				ret.Tags.Add(subshader.state.tags[i], subshader.state.tags[i + 1]);

			// export render queue
			ret.RenderQueue = materialInUse.renderQueue;

			return ret;
		}

		public static ProtoWorld.Model ConvertModel( UnityEngine.Mesh mesh , string dest_file , UnityEngine.SkinnedMeshRenderer skinmesh = null )
		{
			// find mesh in world
			ProtoWorld.Model ret = null;

			// create a new mesh
			ret = new ProtoWorld.Model{ Version = ExporterConfig.VERSION , Name = mesh.name , File = dest_file };

			// submeshes
			for( int smi = 0 ; smi < mesh.subMeshCount ; ++smi )
			{
				// create a submesh
				ProtoWorld.Mesh sub = new ProtoWorld.Mesh();
				ret.Meshes.Add( sub );

				// primitive type
				switch( mesh.GetTopology( smi ) )
				{
				case UnityEngine.MeshTopology.Triangles:
					sub.Type = ProtoWorld.PRIMITIVE_TYPE.Triangles;
					break;
				case UnityEngine.MeshTopology.Quads:
					sub.Type = ProtoWorld.PRIMITIVE_TYPE.Quads;
					break;
				case UnityEngine.MeshTopology.Lines:
					sub.Type = ProtoWorld.PRIMITIVE_TYPE.Lines;
					break;
				case UnityEngine.MeshTopology.LineStrip:
					sub.Type = ProtoWorld.PRIMITIVE_TYPE.LineStrip;
					break;
				case UnityEngine.MeshTopology.Points:
					sub.Type = ProtoWorld.PRIMITIVE_TYPE.Points;
					break;
				}

				// indice
				sub.Indice.AddRange( mesh.GetIndices( smi ) ); // TODO mesh.GetTriangles(smi); ?
			}

			UtilityDataCopy.CopyFloat3s( ret.Position , mesh.vertices );

			if( mesh.colors.Length == mesh.vertexCount )
				UtilityDataCopy.CopyColors( ret.Color , mesh.colors );

			if( mesh.normals.Length == mesh.vertexCount )
				UtilityDataCopy.CopyFloat3s( ret.Normal , mesh.normals );

			if( mesh.tangents.Length == mesh.vertexCount )
			{
				UtilityDataCopy.CopyFloat4s( ret.Tangent , mesh.tangents );
			}
			else if( mesh.normals.Length == mesh.vertexCount && mesh.uv.Length == mesh.vertexCount )
			{
				UnityEngine.Vector4[] tangents = TangentSolver.Solve( mesh );
				if( tangents.Length == mesh.vertexCount )
					UtilityDataCopy.CopyFloat4s( ret.Tangent , tangents );
			}

			if( mesh.uv.Length == mesh.vertexCount )
				UtilityDataCopy.CopyFloat2s( ret.Texcoord0 , mesh.uv );

			if( mesh.uv2.Length == mesh.vertexCount )
				UtilityDataCopy.CopyFloat2s( ret.Texcoord1 , mesh.uv2 );

			if( mesh.uv3.Length == mesh.vertexCount )
				UtilityDataCopy.CopyFloat2s( ret.Texcoord2 , mesh.uv3 );

			if( mesh.uv4.Length == mesh.vertexCount )
				UtilityDataCopy.CopyFloat2s( ret.Texcoord3 , mesh.uv4 );

			// convert skin
			if( null != skinmesh )
			{
				//Debug.Log ("skinmesh = " + skinmesh.name);
				if (mesh.boneWeights.Length == mesh.vertexCount)
				{
					UnityEngine.BoneWeight[] bweight = mesh.boneWeights;
					UnityEngine.Vector4[] indices = new UnityEngine.Vector4[bweight.Length];
					UnityEngine.Vector4[] weight = new UnityEngine.Vector4[bweight.Length];
					for (int i = 0; i < bweight.Length; ++i)
					{
						indices [i].x = bweight [i].boneIndex0;
						indices [i].y = bweight [i].boneIndex1;
						indices [i].z = bweight [i].boneIndex2;
						indices [i].w = bweight [i].boneIndex3;

						weight [i].x = bweight [i].weight0;
						weight [i].y = bweight [i].weight1;
						weight [i].z = bweight [i].weight2;
						weight [i].w = bweight [i].weight3;
					}

					UtilityDataCopy.CopyInt4s( ret.Bindice , indices );
					UtilityDataCopy.CopyFloat4s( ret.Bweight , weight );
				}

				UnityEngine.Transform[] bones = skinmesh.bones;
				UnityEngine.Matrix4x4[] bindposes = skinmesh.sharedMesh.bindposes;
				for (int i = 0; i < bones.Length; ++i)
				{
					bool found = false;
					string bone_path = ExporterUtility.FindRelativePath( bones[i] , skinmesh.rootBone, out found );
					ret.Bones.Add( new ProtoWorld.Bone()
							{ Name = bone_path
							, Bindpose = UtilityConverter.ConvertMatrix( bindposes[i] ) , } );
				}
			}

			// convert bounding box
			if( null != skinmesh )
			{
				/** SkinnedMeshRenderer.localBounds
				 **	是unity对模型做预处理计算得到的，考虑了模型上挂载的所有动画，计算出一个最大的包围盒
				 **	这个包围盒的大小比mesh.bounds准确，但是center不够准确，所以center采用render.bounds转换到local空间来计算
				 **	然而测试发现SkinnedMeshRenderer.localBounds.extents对于部分模型可能轴向会不准确，如果引擎采用的是包围球的方式就没关系
				 **	TODO. by chenyanlei 如果发现了更准确的计算方式再修改
				**/
				Matrix4x4 localToWorld = skinmesh.gameObject.transform.worldToLocalMatrix;
				Vector3 center = localToWorld.MultiplyPoint(skinmesh.bounds.center);
				ret.BoundsCenter = new ProtoMath.float3
					{ X = center.x , Y = center.y , Z = center.z };
				ret.BoundsExtents = new ProtoMath.float3
					{ X = skinmesh.localBounds.extents.x , Y = skinmesh.localBounds.extents.y , Z = skinmesh.localBounds.extents.z };
			}
			else
			{
				ret.BoundsCenter = new ProtoMath.float3
					{ X = mesh.bounds.center.x , Y = mesh.bounds.center.y , Z = mesh.bounds.center.z };
				ret.BoundsExtents = new ProtoMath.float3
					{ X = mesh.bounds.extents.x , Y = mesh.bounds.extents.y , Z = mesh.bounds.extents.z };
			}

			return ret;
		}
	}
}
