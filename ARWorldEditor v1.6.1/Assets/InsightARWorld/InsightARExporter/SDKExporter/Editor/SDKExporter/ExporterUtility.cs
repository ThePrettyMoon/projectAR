/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using System.Collections;
using System.IO;
using System.IO.Compression;
using ARWorldEditor;
using ICSharpCode.SharpZipLib.Zip;

namespace RenderEngine
{
	class ExporterUtility
	{
		public static void WriteGzip( Google.Protobuf.IMessage message , string path , string dest )
		{
			using( var file = File.Create( path + dest ) )
			{
				using( GZipStream output = new GZipStream( file , CompressionMode.Compress) )
				{
					Google.Protobuf.CodedOutputStream cos = new Google.Protobuf.CodedOutputStream( output );
					message.WriteTo( cos );
					cos.Dispose();
				}
			}
		}

		public static void WriteJson( Google.Protobuf.IMessage message , string path , string dest )
		{
			using( StreamWriter sw = new StreamWriter( path + dest ) )
			{
				Google.Protobuf.JsonFormatter.Settings settings = new Google.Protobuf.JsonFormatter.Settings( true );
				Google.Protobuf.JsonFormatter formater = new Google.Protobuf.JsonFormatter( settings );
				sw.Write( formater.Format( message ) );
			}
		}

		public static void WriteBinary( Google.Protobuf.IMessage message , string path , string dest )
		{
			using( var file = File.Create( path + dest ) )
			{
				Google.Protobuf.CodedOutputStream cos = new Google.Protobuf.CodedOutputStream( file );
				message.WriteTo( cos );
				cos.Dispose();
			}
		}

		public static void WriteString( string str , string path , string dest )
		{
			System.IO.File.WriteAllText( path + dest , str );
		}

		public static void CompressScene(string input_directory, string output_zip)
		{
			//ZipClass.Instance.ZipDirectory(input_directory, output_zip);
			//update by wy
            ARWorldEditor.ZipUtility.Zip(input_directory, output_zip);		
		}

		public static void DecompressScene(string output_directory, string input_zip)
		{
			// TODO
			UnZipClass.Instance.UnZip(input_zip, output_directory);
		}

		public static bool PrepareExport
			( string asset_path_name_ext , string export_path , string export_extension
				, out string dest )
		{
			// get asset path file ext
			dest = null;

			// get asset path and file
			string src_path = Path.GetDirectoryName( asset_path_name_ext );
			string src_file = Path.GetFileNameWithoutExtension( asset_path_name_ext );

			// build export path file ext
			dest = src_path + "/" + src_file + export_extension;

			// check existance
			if( File.Exists( export_path + dest ) ) return false;

			// create folders
			Directory.CreateDirectory( export_path + src_path );

			//
			return true;
		}

		public static void PostProcess( ExporterConfig config )
		{
			// copy framework
			UtilityFileSystem.CopyDirectory
			( ExporterConfig.FRAMEWORKS[(int)config.platform]
				, config.root_path , true );

			// clear meta files
			UtilityFileSystem.FileDeleteByExtension
			( config.root_path
				, ".meta" , true );
		}

		public static string GetBuildinShaderDefaultTexturePath( string pname )
		{
			for( int i = 0 ; i < ExporterConfig.BUILDIN_SHADER_DEFAULT_TEXTURES.Length / 2 ; ++i )
			{
				if( 0 == pname.CompareTo( ExporterConfig.BUILDIN_SHADER_DEFAULT_TEXTURES[i*2 + 0] ) )
				{
					return ExporterConfig.BUILDIN_SHADER_DEFAULT_TEXTURES[i*2 + 1];
				}
			}
			// do not export any texture if this texture is not important.
			// because iPhone5 supports only 8 textures per pass.
			return ExporterConfig.DEFAULT_TEXTURE_FOR_CUSTOM_SHADER; 
			//return ExporterConfig.DEFAULT_TEXTURES[0*2+1]; // return unknown texture
		}

		public static int GetBuildinShaderPath( string pname )
		{
			//string name = pname + ( skin_mesh ? "_skin" : "" );
			for( int i = 0 ; i < ExporterConfig.BUILDIN_SHADER_NAMES.Length ; ++i )
			{
				if( pname.Equals( ExporterConfig.BUILDIN_SHADER_NAMES[i] ) )
				{
					return i;
				}
			}
			return -1;
		}

		public static string GetBuildinMaterialPath( string name)
		{
			for( int i = 0 ; i < ExporterConfig.BUILDIN_MATERIAL_NAMES.Length ; ++i )
			{
				if( name.Equals( ExporterConfig.BUILDIN_MATERIAL_NAMES[i] ) )
				{
					return ExporterConfig.BUILDIN_MATERIAL_DESTS[i];
				}
			}
			return string.Empty;
		}

		public static bool IsExcludedPass(string name)
		{
			for (int i = 0; i < ExporterConfig.EXCLUDED_PASSES.Length; ++i)
				if (name.Equals(ExporterConfig.EXCLUDED_PASSES[i]))
					return true;
			return false;
		}
		public static bool IsExcludedPass_NewFormat(string name)
		{
			for (int i = 0; i < ExporterConfig.EXCLUDED_PASSES_NEW_FORMAT.Length; ++i)
				if (name.Equals(ExporterConfig.EXCLUDED_PASSES_NEW_FORMAT[i]))
					return true;
			return false;
		}

		public static string FindRelativePath( UnityEngine.Transform trans, UnityEngine.Transform root, out bool found )
		{
			string path = GetRelativePath( trans , root , out found );
			if( found )
				return path;

			// look for the bone in parents
			path = "";
			for( UnityEngine.Transform rt = root.parent ; null != rt ; rt = rt.parent )
			{
				path +=  "../";
				string found_path = GetRelativePath( trans , rt , out found );
				if( found )
					return path + found_path;
			}

			// look for trans in roots
			// do not clear path here, we need the "../".
			path += "../";
			UnityEngine.GameObject[] rts = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
			foreach( UnityEngine.GameObject rt in rts )
			{
				string found_path = GetRelativePath( trans , rt.transform , out found );
				if( found )
					return path + rt.name + "/" + found_path;
			}

			return "";
		}

		private static string GetRelativePath( UnityEngine.Transform trans , UnityEngine.Transform root , out bool found )
		{
			string path = "";
			for( UnityEngine.Transform cur = trans; null != cur ; cur = cur.parent )
			{
				if( cur == root )
				{
					found = true;
					return path; // the target bone is a child of root
				}
				path = cur.name + "/" + path;
			}
			found = false;
			return "";
		}
	}
}