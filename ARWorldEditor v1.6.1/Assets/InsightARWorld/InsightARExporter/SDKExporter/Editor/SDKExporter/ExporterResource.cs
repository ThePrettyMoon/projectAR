/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Security.Cryptography;

namespace RenderEngine
{
	struct ExportTextureArgs
	{
		public Texture tex;
		public Vector4 trans;
	}

	class ExporterResource
	{
        struct TextureCacheMeta
        {
            public string hash;
            public string arg;
        }

        private static string ExportTexture2D(ExporterConfig config, Texture2D texture, string args)
        {
            string src = config.asset_path.GetPath(texture);
            string dest;

            if (ExporterUtility.PrepareExport
                (src, config.root_path, ExporterConfig.TEXTURE_EXTENSION, out dest))
            {

                int width = Mathf.NextPowerOfTwo(texture.width);
                int height = Mathf.NextPowerOfTwo(texture.width);
                int square = Mathf.Max(width, height);


                string cacheTexture = "Temp/TextureCache/" + Path.GetDirectoryName(src) + "/" + Path.GetFileName(dest);
                string cacheTextureMeta = "Temp/TextureCache/" + Path.GetDirectoryName(src) + "/" + Path.GetFileName(dest) + ".meta";

                string arg = (texture.mipmapCount > 1 ? ExporterConfig.TEXTURE_MIPMAP_ARGS : "")
                        + ExporterConfig.TEXTURE_COMMON_ARGS[(int)config.platform]
                        + String.Format(args, square, square);


                string md5Str = "";
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(src))
                    {
                        var hash = md5.ComputeHash(stream);
                        md5Str = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }

                bool hitCache = false;
                if (File.Exists(cacheTexture) && File.Exists(cacheTextureMeta))
                {
                    string metaJson = File.ReadAllText(cacheTextureMeta);
                    TextureCacheMeta cacheMeta = JsonUtility.FromJson<TextureCacheMeta>(metaJson);

                    if (cacheMeta.hash == md5Str && cacheMeta.arg == arg)
                    {
                        if (!Directory.Exists(config.root_path + dest))
                            Directory.CreateDirectory(Path.GetDirectoryName(config.root_path + dest));

                        File.Copy(cacheTexture, config.root_path + dest, true);
                        hitCache = true;
                    }
                }

                if (!hitCache)
                {
                    string result = UtilityExecute.ExecuteTool
                        ("PVRTexToolCLI", "-i \"" + src
                            + "\" -o \"" + config.root_path + dest
                            + "\" "
                            + arg);

                    if (result.Length > 0)
                        Debug.Log(result);

                    if (!Directory.Exists(cacheTexture))
                        Directory.CreateDirectory(Path.GetDirectoryName(cacheTexture));

                    File.Copy(config.root_path + dest, cacheTexture, true);
                    TextureCacheMeta cacheMeta = new TextureCacheMeta() { hash = md5Str, arg = arg };
                    string metaJson = JsonUtility.ToJson(cacheMeta);
                    File.WriteAllText(cacheTextureMeta, metaJson);
                }

            }

            return dest.Replace( "\\" , "/" );
        }

        private static string ExportTexture3D( ExporterConfig config , Texture3D texture , string args )
		{
			UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, "not implemented: exporting Texture3D " + config.asset_path.GetPath( texture ) , "OK!");
			return "";
		}

		public static string ExportTextureCube( ExporterConfig config, Cubemap texture, string args)
		{
			//string cubeamp_quality_args = ExporterConfig.TEXTURE_CUBEMAP_QUALITY_ARGS[(int)config.platform];// + ExporterConfig.TEXTURE_COMMON_ARGS[(int)config.platform];
			args = ExporterConfig.TEXTURE_CUBEMAP_QUALITY_ARGS[(int)config.platform];
			return ExportTextureCubeInternal(config, false, null, texture, args);

		}

		public static string ExportReflectionProbe( ExporterConfig config, ReflectionProbe reflectionProbe)
		{
			string args = ExporterConfig.TEXTURE_CUBEMAP_QUALITY_ARGS[(int)config.platform];// + ExporterConfig.TEXTURE_COMMON_ARGS[(int)config.platform];
			return ExportTextureCubeInternal(config, true, reflectionProbe, null, args);
		}

		// private static void GetOriginalTextureSize(UnityEditor.TextureImporter importer, out int width, out int height)
		// {
		// 	System.Reflection.MethodInfo getWidthAndHeightMethod = 
		// 				importer.GetType().GetMethod("GetWidthAndHeight", 
		// 				System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
		// 				null,
		// 				new System.Type[] { typeof(int).MakeByRefType(), typeof(int).MakeByRefType() },
		// 				null
		// 				);
		// 	object[] arguments = new object[2];
		// 	getWidthAndHeightMethod.Invoke(importer, arguments);
		// 	width = (int)arguments[0];
		// 	height = (int)arguments[1];
		// }

		private static string ExportTextureCubeInternal( ExporterConfig config , bool isReflectionProbe, ReflectionProbe reflectionProbe, Cubemap texture, string args)
		{
			string ret = "";

			string exrTemporaryDirectory = ExporterConfig.TEMPORARY_PATH;
			if (!System.IO.Directory.Exists(exrTemporaryDirectory))
			{
				System.IO.Directory.CreateDirectory(exrTemporaryDirectory);
			}

			if (isReflectionProbe)
			{
				string exrTemporaryName = "EnvRefProbe";
				string exrTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, exrTemporaryName + ".exr");
				string pngTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, exrTemporaryName);

				// Create an empty file first to avoid 'textureimporter'
				FileStream exrStream = File.Create(exrTemporaryPath);
				exrStream.Close();

				// Generate exr file
				if (UnityEditor.Lightmapping.BakeReflectionProbe(reflectionProbe, exrTemporaryPath))
				{
					string err = UtilityExecute.ExecuteTool
							( "exr2png" , "\"" + exrTemporaryPath + "\"" + ' ' + "\"" + pngTemporaryPath + "\"");
					if (err.Length > 0)
						UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, "failed to convert " + exrTemporaryPath + " to PNG" , "OK!");

					ret = ExporterResource.Export6SidedCubemapTexture(config, config.cubemap_name, pngTemporaryPath, args);
				}
				else
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, "failed to bake ReflectionProbe " + exrTemporaryPath , "OK!");
				
				File.Delete(exrTemporaryPath);
			}
			else
			{
				string src = config.asset_path.GetPath( texture );
				string src_name = Path.GetFileNameWithoutExtension(src);

				string pngTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, src_name);

				string err = UtilityExecute.ExecuteTool
						( "exr2png" , "\"" + src + "\"" + ' ' + "\"" + pngTemporaryPath + "\"");
				if (err.Length > 0)
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, "failed to convert " + src + " to PNG" , "OK!");

				ret = ExporterResource.Export6SidedCubemapTexture(config, src, pngTemporaryPath, args);
			}

			return ret.Replace( "\\" , "/" );

		}

		private static string ExportTexture2DArray( ExporterConfig config , Texture2DArray texture , string args )
		{
			UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, "not implemented: exporting Texture2DArray " + config.asset_path.GetPath( texture ) , "OK!");
			return "";
		}

		private static string ExportTextureInfo( ExporterConfig config , Texture texture , UnityEngine.Vector4 transform , string filename )
		{
			string src = config.asset_path.GetPath( texture );
			string dest;

			if( ExporterUtility.PrepareExport
				( src , config.root_path , ExporterConfig.TEXTURE_INFO_EXTENSION , out dest ) )
			{
				ProtoWorld.Texture ptex = Converter.ConvertTexture( config , texture , transform , filename );
				ExporterUtility.WriteBinary( ptex , config.root_path , dest );
			}

			return dest.Replace( "\\" , "/" );
		}

		public static string Export6SidedCubemapTexture (ExporterConfig config, string src, string texturePrefix, string args, string default_texture = "")
		{
			//string src = texturePrefix;
			//string ret = default_texture;
			
			string dest = string.Empty;
			if( ExporterUtility.PrepareExport
				( src, config.root_path , ExporterConfig.OUTPUT_CUBEMAP_EXTENSION[(int)config.platform] , out dest ) )
			{
				string[] textureGroupSrc = new string[6];
				for (int face = 0; face < 6; ++ face)
					textureGroupSrc[face] = texturePrefix + (char)(face + 48) + ExporterConfig.INPUT_CUBEMAP_EXTENSION;

				string err = UtilityExecute.ExecuteTool
					( "PVRTexToolCLI" , "-i " 
						+ textureGroupSrc[0] + ','
						+ textureGroupSrc[1] + ','
						+ textureGroupSrc[2] + ','
						+ textureGroupSrc[3] + ','
						+ textureGroupSrc[4] + ','
						+ textureGroupSrc[5]
						+ " -o \"" + config.root_path + dest + "\" "
						+ "-cube "
						+ ExporterConfig.TEXTURE_MIPMAP_ARGS
						+ args
						);
				if( err.Length > 0 )
					Debug.Log( err );
			}

			return dest.Replace( "\\" , "/" );
		}

		public static void ExportIllumination(ExporterConfig config)
		{
			ProtoWorld.IlluminationBundle ret = new ProtoWorld.IlluminationBundle{
				Version = ExporterConfig.VERSION
			};

			// export lights
			{
				Light[] lights = GameObject.FindObjectsOfType(typeof(Light)) as Light[];
				foreach(Light light in lights)
				{
					ret.Lights.Add( ConverterComponent.ConvertLight( config , light ) );
				}
			}

			// export reflection probes
			{
				UnityEngine.GameObject probeGameObject;
				ProtoWorld.ReflectionProbe reflectionProbe = Converter.ConvertReflectionProbe(config, 
															UtilityUnityCreator.CreateReflectionProbe(config, out probeGameObject));

				UtilityUnityCreator.Delete(probeGameObject);

				// TODO: Figure out how probe0 and probe1 works. now they just have the same value
				ret.ReflectionProbes.Add(reflectionProbe);
				ret.ReflectionProbes.Add(reflectionProbe);
			}

			// export sphere harmonic lighting
			{
				string str = "";
				UnityEngine.Vector4 [] sh = new UnityEngine.Vector4[7];
				UtilityRendering.ConvertSHEMapConstants( sh , UnityEngine.RenderSettings.ambientProbe , UnityEngine.RenderSettings.ambientIntensity);

				str = "";
				for( int i = 0 ; i < 7 ; ++i )
				{
					str += sh[i].x + ", ";
					str += sh[i].y + ", ";
					str += sh[i].z + ", ";
					str += sh[i].w + "\n";

					ret.SphereHarmonicLighting.Add( sh[i].x );
					ret.SphereHarmonicLighting.Add( sh[i].y );
					ret.SphereHarmonicLighting.Add( sh[i].z );
					ret.SphereHarmonicLighting.Add( sh[i].w );
				}
				UnityEngine.Debug.Log( "SH Light: " + str );
			}

			string dest = "";
			if( ExporterUtility.PrepareExport
					( config.cubemap_name , config.root_path , ExporterConfig.ILLUMINATION_EXTENSION , out dest ) )
			{
				ExporterUtility.WriteBinary(ret, config.root_path, dest);
			}
		}

		public static string ExportTextureTask( ExporterConfig config, Texture texture , Vector4 transform )
		{
			// get exported texture file name
			string src = config.asset_path.GetPath( texture );
			string dest;

			if( ExporterUtility.PrepareExport
				( src , config.root_path , ExporterConfig.TEXTURE_INFO_EXTENSION , out dest ) )
			{
				// push task
				config.texture_export_tasks.Enqueue (new ExportTextureArgs{ tex = texture, trans = transform });
			}

			return dest.Replace( "\\" , "/" );
		}

		public static string ExportTextureDo( ExporterConfig config, Texture texture, Vector4 transform )
		{
			string ret = "";
			if( null == texture ) return ret;
			string src = config.asset_path.GetPath( texture );

			if (src.Equals(ExporterConfig.UNITY_BUILDLIN_RESOURCES))
			{
				UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
							, "导出 " + texture.ToString() + " 失败！\n" + "不支持Unity的内置图片或精灵，请检查您的UI或Sprite，并把它们换成自定义图片。"
							, "OK");
				// throw new System.Exception("不支持Unity的内置图片或精灵，请检查您的UI或Sprite，并把它们换成自定义图片。");
			}

			string ext = Path.GetExtension( src ).ToLower();

			// {
			// 	// is HDR?
			// 	if( ext.Equals( ".hdr" ))
			// 	{
			// 		Debug.LogWarning( "HDR texture(" + src + ") is not supported currently by RenderEngine." );
			// 		return ret;
			// 	}
			// }

			// is render texture?
			string texture_file = "";
			if( ext.Equals( ".rendertexture" ) )
			{
				// there is no image file to export
			}
			else
			{
				// compression format
				string args = "";
				UnityEditor.TextureImporter importer
				= (UnityEditor.TextureImporter)UnityEditor.TextureImporter.GetAtPath
					( src );

				if( config.TEXTURE_FORCE_LOW_QUALITY )
				{
					switch( null != importer ? importer.textureCompression : UnityEditor.TextureImporterCompression.CompressedLQ )
					{
					case UnityEditor.TextureImporterCompression.CompressedHQ:
					case UnityEditor.TextureImporterCompression.Compressed:
					case UnityEditor.TextureImporterCompression.CompressedLQ:
						args = ExporterConfig.TEXTURE_LOW_QUALITY_ARGS[(int)config.platform];
						break;
					case UnityEditor.TextureImporterCompression.Uncompressed:
						args = ExporterConfig.TEXTURE_NONE_QUALITY_ARGS[(int)config.platform];
						break;
					}
				}
				else
				{
					switch( null != importer ? importer.textureCompression : UnityEditor.TextureImporterCompression.CompressedHQ )
					{
					case UnityEditor.TextureImporterCompression.CompressedHQ:
						args = ExporterConfig.TEXTURE_HIGH_QUALITY_ARGS[(int)config.platform];
						break;
					case UnityEditor.TextureImporterCompression.Compressed:
						args = ExporterConfig.TEXTURE_MID_QUALITY_ARGS[(int)config.platform];
						break;
					case UnityEditor.TextureImporterCompression.CompressedLQ:
						args = ExporterConfig.TEXTURE_LOW_QUALITY_ARGS[(int)config.platform];
						break;
					case UnityEditor.TextureImporterCompression.Uncompressed:
						args = ExporterConfig.TEXTURE_NONE_QUALITY_ARGS[(int)config.platform];
						break;
					}
				}

				//args += ExporterConfig.TEXTURE_COMMON_ARGS[(int)config.platform];

				// export
				UnityEngine.Rendering.TextureDimension dim = texture.dimension;
				switch( dim )
				{
				case UnityEngine.Rendering.TextureDimension.Tex2D:
					Texture2D tex2d = (Texture2D)texture;
					texture_file = ExportTexture2D( config , tex2d , args );
					break;
				case UnityEngine.Rendering.TextureDimension.Tex3D:
					Texture3D tex3d = (Texture3D)texture;
					texture_file = ExportTexture3D( config , tex3d , args );
					break;
				case UnityEngine.Rendering.TextureDimension.Cube:
					Cubemap texcube = (Cubemap)texture;
					texture_file = ExportTextureCube( config, texcube , args );
					break;
				case UnityEngine.Rendering.TextureDimension.Tex2DArray:
					Texture2DArray tex2darray = (Texture2DArray)texture;
					texture_file = ExportTexture2DArray( config , tex2darray , args );
					break;
				}
			}

			// export texture info
			ret = ExportTextureInfo( config , texture , transform , texture_file );
			return ret.Replace( "\\" , "/" );
		}

		public static string ExportMovie( ExporterConfig config , UnityEngine.Video.VideoClip video )
		{
			string src = config.asset_path.GetPath( video );
			if( null != src )
			{
				if( src.Length > 0 )
				{
					string dest = "";
					if( ExporterUtility.PrepareExport
						( src , config.root_path , Path.GetExtension( src ) , out dest ) )
					{
						File.Copy( src , config.root_path + dest );
						return dest.Replace( "\\" , "/" );
					}
					return dest.Replace( "\\" , "/" );
				}
			}
			return null;
		}

		public static string ExportShader
			( ExporterConfig config
			, ShaderLabShader json_shader
			, int subshader_index
			, int pass_index
			, int buildin_shader_index)
		{
			if( -1 == buildin_shader_index )
			{
				// is not build in shader, use the shader name instead.
				// let the engine to search for the shader.
							
				// 因为有多个pass，所以除了第0个pass外，其他pass命名要加后缀
				string name_index = (pass_index == 0) ? "" : ("_" + pass_index);
				string src = Path.GetDirectoryName( json_shader.srcpath ) + '/' + Path.GetFileNameWithoutExtension( json_shader.srcpath ) + name_index;
				string dest = null;
				if( ExporterUtility.PrepareExport
					( src, config.root_path , ExporterConfig.SHADER_EXTENSION , out dest ) )
				{
					ProtoWorld.Shader dest_shader = Converter.ConvertShader( config , json_shader , pass_index, src);
					ExporterUtility.WriteBinary( dest_shader , config.root_path , dest );
				}
				return dest.Replace( "\\" , "/" );
			}
			else // is buildin shader
			{
				string source_vs_file = ExporterConfig.BUILDIN_SHADER_SOURCES[ buildin_shader_index ] + ExporterConfig.SHADER_COMPILED_VS_EXTENSION;
				string source_fs_file = ExporterConfig.BUILDIN_SHADER_SOURCES[ buildin_shader_index ] + ExporterConfig.SHADER_COMPILED_FS_EXTENSION;
				string dest_vs_file = ExporterConfig.BUILDIN_SHADER_DESTS[buildin_shader_index] + ExporterConfig.SHADER_COMPILED_VS_EXTENSION;
				string dest_fs_file = ExporterConfig.BUILDIN_SHADER_DESTS[buildin_shader_index] + ExporterConfig.SHADER_COMPILED_FS_EXTENSION;


				string dest = null;
				if( ExporterUtility.PrepareExport
					( ExporterConfig.BUILDIN_SHADER_DESTS[buildin_shader_index] , config.root_path , ExporterConfig.SHADER_EXTENSION , out dest ) )
				{
					ProtoWorld.Shader s = new ProtoWorld.Shader
					{
						VsFile = dest_vs_file ,
						FsFile = dest_fs_file ,
						CpuBone = true ,
					};
					ExporterUtility.WriteBinary( s , config.root_path , dest );

					UtilityFileSystem.CopyFile(source_vs_file, config.root_path + dest_vs_file, true);
					UtilityFileSystem.CopyFile(source_fs_file, config.root_path + dest_fs_file, true);
				}

				return dest.Replace( "\\" , "/" );
			}
		}

		public static string ExportI3dShader
			( ExporterConfig config
			, ShaderLabShader json_shader
			, int subshader_index
			, int pass_index
			, int buildin_shader_index)
		{
			// 因为有多个pass，命名要加后缀
			string name_index = ("_pass" + pass_index);
			string dest = null;

			if (-1 == buildin_shader_index)
			{
				bool needSrcFile = true;
				// 如果是内置的unity shader，但不是导出工具认为的内置shader（因为太多了我们不可能每个都顾及到）
				string srcpath = json_shader.srcpath;
				if (srcpath == ExporterConfig.UNITY_BUILDLIN_RESOURCES)
				{
					srcpath = json_shader.name.Replace(' ', '_');
					needSrcFile = false;
				}
				string src = Path.GetDirectoryName( srcpath ) + '/' + Path.GetFileNameWithoutExtension( srcpath ) + name_index;
				if( ExporterUtility.PrepareExport
					( src, config.root_path , ExporterConfig.I3DSHADER_EXTENSION , out dest ) )
				{
					if (needSrcFile)
					{
						string src_file = src + ExporterConfig.I3DSHADER_EXTENSION;
						// 如果不是内置shader，则生成i3dShader内容（自动检测是否修改，如unity shader无修改则不生成新的）
						ShaderGenerator.GenerateI3dShader( src_file, json_shader, pass_index );
						UtilityFileSystem.CopyFile( src_file , config.root_path + dest , true );
					}
					else
					{
						// 如果是内置的unity shader，但不是导出工具认为的内置shader，则把srcfile的路径设为Assets根目录文件夹
						string src_file = "Assets/InsightARWorld/__Temp__/" + src + ExporterConfig.I3DSHADER_EXTENSION;
						if (!Directory.Exists(Path.GetDirectoryName(src_file)))
							Directory.CreateDirectory(Path.GetDirectoryName(src_file));

						ShaderGenerator.GenerateI3dShader( src_file, json_shader, pass_index );
						UtilityFileSystem.CopyFile( src_file , config.root_path + dest , true );

						UtilityFileSystem.DirectoryDeleteRF("Assets/InsightARWorld/__Temp__/");
					}
				}
			}
			else
			{
				string src = ExporterConfig.BUILDIN_SHADER_SOURCES[ buildin_shader_index ] + name_index;
				if( ExporterUtility.PrepareExport
					( src, config.root_path , ExporterConfig.I3DSHADER_EXTENSION , out dest ) )
				{
					string src_file = src + ExporterConfig.I3DSHADER_EXTENSION;
					UtilityFileSystem.CopyFile(src_file, config.root_path + dest, true);
				}
			}

			return dest.Replace( "\\" , "/" );
		}

		public static string ExportMaterial
			( ExporterConfig config
			, Material material, bool skin_mesh
			, UnityEngine.Renderer renderer
			, bool [] using_depth_of_render_texture )
		{
			string buildin_material_path = 
				ExporterUtility.GetBuildinMaterialPath( material.name);

			string src;	
			if (string.Empty == buildin_material_path)	
				src = config.asset_path.GetPath( material );
			else
				src = buildin_material_path;
				
			string dest = "";

			if( ExporterUtility.PrepareExport
				( src , config.root_path , ExporterConfig.MATERIAL_EXTENSION , out dest ) )
			{
				ProtoWorld.Material rew_mat = Converter.ConvertMaterial
					( config, material, dest, skin_mesh, renderer, using_depth_of_render_texture );
				ExporterUtility.WriteBinary( rew_mat , config.root_path , dest );
			}
			return dest.Replace( "\\" , "/" );
		}

		public static string ExportModel( ExporterConfig config , Mesh mesh , SkinnedMeshRenderer skinmesh = null )
		{
			string src = config.asset_path.GetPath( mesh );
			if( src.Length == 0 )
			{
				UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
					, "ExportModel error: can not find the mesh " + mesh.name
					, "OK!");
				return null;
			}
			string path_name = Path.GetDirectoryName( src ) + "/" + Path.GetFileNameWithoutExtension( src );
			string dest_path_name_ext;

			if( ExporterUtility.PrepareExport
				( path_name + "__" + mesh.name + "_" + mesh.GetHashCode() , config.root_path , ExporterConfig.MESH_EXTENSION , out dest_path_name_ext ) )
			{
				ProtoWorld.Model dest_model = Converter.ConvertModel( mesh , dest_path_name_ext , skinmesh );
				ExporterUtility.WriteBinary( dest_model , config.root_path , dest_path_name_ext );
			}
			return dest_path_name_ext.Replace( "\\" , "/" );
		}

#if UNITY_EDITOR
		public static string ExportAnimation( ExporterConfig config , AnimationClip clip )
		{
			string dest = "";
			string src = config.asset_path.GetPath( clip );
			string path_name = Path.GetDirectoryName( src ) + "/" + Path.GetFileNameWithoutExtension( src );
			string file_name = Path.GetFileNameWithoutExtension (src);

			if( ExporterUtility.PrepareExport
				( path_name , config.root_path , ExporterConfig.ANIMATION_EXTENSION , out dest ) )
			{
				ProtoWorld.Animation dest_anim = ConverterAnimation.ConvertAnimation( file_name , clip , dest );
				ExporterUtility.WriteBinary( dest_anim , config.root_path , dest );
			}

			return dest.Replace( "\\" , "/" );
		}

		public static string FindProtoClip
		( Google.Protobuf.Collections.RepeatedField< string > src , string name )
		{
			if( null == src ) return null;
			foreach( string clip in src )
				if( clip.Equals( name ) )
					return clip;
			return null;
		}

		public static string ExportAnimatorController( ExporterConfig config , Animator anim )
		{
			string dest = "";
			if( null != anim.runtimeAnimatorController )
			{
				string src = config.asset_path.GetPath( anim.runtimeAnimatorController );
				string path_name = Path.GetDirectoryName( src ) + "/" + Path.GetFileNameWithoutExtension( src );
				//string file_name = Path.GetFileNameWithoutExtension (src);
				//UnityEditor.ModelImporter model_import = UnityEditor.AssetImporter.GetAtPath( src ) as UnityEditor.ModelImporter;

				if( ExporterUtility.PrepareExport
					( path_name , config.root_path , ExporterConfig.ANIMATOR_CONTROLLER_EXTENSION , out dest ) )
				{
					ProtoWorld.AnimatorController dest_animator_controller = ConverterAnimationController.ConvertAnimatorController( config, anim );
					ExporterUtility.WriteBinary( dest_animator_controller , config.root_path , dest );
				}
			}
			return dest.Replace( "\\" , "/" );
		}
#endif

        public static string ExportAudioClip(ExporterConfig config, AudioClip clip)
        {
            string src = config.asset_path.GetPath(clip);
            string dest;


            if (ExporterUtility.PrepareExport
                (src, config.root_path, ExporterConfig.AUDIOCLIP_EXTENSION, out dest))
            {
                if (ExporterConfig.AUDIOCLIP_EXTENSION.EndsWith(Path.GetExtension(src).ToLower()))
                {
                    File.Copy(src, config.root_path + dest);
                }
                else
                {
                    string err = UtilityExecute.ExecuteTool("ffmpeg", String.Format("-i {0} {1}", src, config.root_path + dest));
                    if (err.Length > 0)
                        Debug.Log(err);
                }
            }

            return dest.Replace( "\\" , "/" );
        }
    }
}
