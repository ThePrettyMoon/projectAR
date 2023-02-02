/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEngine;
using System.Collections.Generic;

namespace RenderEngine
{
	 class ExporterConfig
	{
		//后续根据需要
		public const string VERSION = "InsightSDKResourceTool@1.7";
		//public const string VERSION = "InsightAR-World@1.0";
		//public const string TITLE = "Insight ResourceTool";
		public const string TITLE = "洞见AR-World";


		public const string EXPORTER_DIRECTORY = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor";

		// export paths
		public const string defaultIOSDestinationPath = "../Insight3dEngine/assets/DemoExport/";
		public const string defaultAndroidDestinationPath = "../Insight3dEngine/assets_android/DemoExport/";
		public const string defaultBundleDestinationPath = "ExportedBundle";
		public static string IOSDestinationPath = PlayerPrefs.GetString("IOSDestinationPath", defaultIOSDestinationPath);
		public static string AndroidDestinationPath = PlayerPrefs.GetString("AndroidDestinationPath", defaultAndroidDestinationPath);
		public static string BundleDestinationPath = PlayerPrefs.GetString("BundleDestinationPath", defaultBundleDestinationPath);

        public const string JS_LIBRARY_PATH = "Assets/InsightARWorld/InsightARExporter/JSLibrary/";

		public static bool OpenTargetFolder = PlayerPrefs.GetInt("GLTFOpenTargetFolder", 1) > 0;

		public enum REPlatform
		{
			iOS = 0 ,
			Android = 1 ,
			WebGL = 2 ,
		}

		public static Vector3 REFLECTION_PROBE0_MIN = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
		public static Vector3 REFLECTION_PROBE0_MAX = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		public static Vector3 REFLECTION_PROBE0_POSITION = Vector3.zero;
		public const int REFLECTION_PROBE0_LOW_RESOLUTION = 128;


		public static string[] FRAMEWORKS =
		{
			EXPORTER_DIRECTORY + "/Framework_iOS" ,
			EXPORTER_DIRECTORY + "/Framework_Android" ,
		};

        public const string AUDIOCLIP_EXTENSION = ".mp3";
		public const string TEXTURE_EXTENSION = ".ktx";
		public const string TEXTURE_INFO_EXTENSION = ".texture";
		public static string[] OUTPUT_CUBEMAP_EXTENSION = { ".pvr", ".ktx" };
		public const string INPUT_CUBEMAP_EXTENSION = ".png";


		public static string TEXTURE_MIPMAP_ARGS = "-m -mfilter cubic ";
		public static string[] TEXTURE_COMMON_ARGS =
		{
			" -flip y ",
			" -flip y ",
		};
		public static string[] TEXTURE_CUBEMAP_QUALITY_ARGS =
		{
			"-f r8g8b8A8" ,
			"-f r8g8b8A8" ,
		};
		public static string[] TEXTURE_HIGH_QUALITY_ARGS =
		{
			"-f PVRTC1_4,UBN -q pvrtcbest -r {0},{1}" ,
            "-f ETC2_RGBA,UBN,sRGB -q etcslowperceptual -r {0},{1}" ,
		};
		public static string[] TEXTURE_MID_QUALITY_ARGS =
		{
            "-f PVRTC1_4,UBN -q pvrtcnormal -r {0},{1}" ,
            "-f ETC2_RGBA,UBN,sRGB -q etcfastperceptual -r {0},{1}" ,
		};
		public static string[] TEXTURE_LOW_QUALITY_ARGS =
		{
            "-f PVRTC1_4,UBN -q pvrtcfastest -r {0},{1}" ,
            "-f ETC2_RGBA,UBN,sRGB -q etcfastperceptual -r {0},{1}" ,
		};
		public static string[] TEXTURE_NONE_QUALITY_ARGS =
		{
            "-f r8g8b8a8,UBN" ,
            "-f r8g8b8a8,UBN" ,
		};
		public bool TEXTURE_FORCE_LOW_QUALITY = false;

		public const string MESH_EXTENSION = ".mesh";
		public const string GPB_EXTENSION = ".gpb";
		public const string MATERIAL_EXTENSION = ".material";
		public const string SHADER_EXTENSION = ".shader";
		public const string SHADER_COMPILED_VS_EXTENSION = "_vs.sc";
		public const string SHADER_COMPILED_FS_EXTENSION = "_fs.sc";
		public const string SCRIPT_EXTENSION = ".lua";
		public const string ANIMATION_EXTENSION = ".anm";
		public const string ANIMATOR_CONTROLLER_EXTENSION = ".ctrl";
		public const string PARTICLE_EXTENSION = ".particle";

		public static float ANIMATION_CLIP_SPACE = 1;
		public static ulong MASK_EVERY_THING = 0x7FFFFFFF;
		public static bool GPU_SKIN_ANIMATION = false;

		public const string SCENE_BIN_EXTENSION = ".res";
		public const string SCENE_JSON_EXTENSION = ".json";
		public const string SCENE_GZIP_EXTENSION = ".gz";
		public const string INSIGHT_BUNDLE_EXTENSION = ".zip";
		public const string ILLUMINATION_EXTENSION = ".illumination";

		public const string UNITY_BUILDLIN_RESOURCES = "Resources/unity_builtin_extra";

		public static string[] BUILDIN_SHADER_NAMES =
		{
			"Standard"	,
			"Standard (Specular setup)"	,
			"Unlit/Transparent"	,
			"Unlit/Transparent Cutout"	,
			"Unlit/Texture"	,
			"Unlit/Color"	,
			"Sprites/Default"	,
			"Sprites/Diffuse"	,
			"Sprites/Mask"	,
			"UI/Default"	,
			"UI/Text",
			"Legacy Shaders/Particles/Additive",
			"Legacy Shaders/Particles/Alpha Blended",
			"Legacy Shaders/Particles/Alpha Blended Premultiply",
			"Particles/Standard Unlit",
			"Particles/Standard Surface",
		};

		public static string[] BUILDIN_SHADER_SOURCES =
		{
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Standard/Standard" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Standard/Standard-Specular" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Unlit/Unlit-Alpha" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Unlit/Unlit-AlphaTest" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Unlit/Unlit-Normal" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Unlit/Unlit-Color" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Sprites/Sprites-Default" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Sprites/Sprites-Diffuse" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Sprites/Sprites-Mask" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/UI/UI-Default" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/UI/UI-Text" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Particles/Additive" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Particles/Additive" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Particles/Additive" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Particles/Additive" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Particles/Additive" ,
		};
		public static string[] BUILDIN_SHADER_DESTS =
		{
			"Engine/DefaultShaders/Standard/Standard" ,
			"Engine/DefaultShaders/Standard/Standard-Specular" ,
			"Engine/DefaultShaders/Unlit/Unlit-Alpha" ,
			"Engine/DefaultShaders/Unlit/Unlit-AlphaTest" ,
			"Engine/DefaultShaders/Unlit/Unlit-Normal" ,
			"Engine/DefaultShaders/Unlit/Unlit-Color" ,
			"Engine/DefaultShaders/Sprites/Sprites-Default" ,
			"Engine/DefaultShaders/Sprites/Sprites-Diffuse" ,
			"Engine/DefaultShaders/Sprites/Sprites-Mask" ,
			"Engine/DefaultShaders/UI/UI-Default" ,
			"Engine/DefaultShaders/UI/UI-Text"	,
			"Engine/DefaultShaders/Particles/Additive"	,
			"Engine/DefaultShaders/Particles/Additive"	,
			"Engine/DefaultShaders/Particles/Additive"	,
		};

		public static string[] BUILDIN_MATERIAL_NAMES =
		{
			"Default-Diffuse"	,
			"Default-Line"	,
			"Default-Particle"	,
			"Default-Skybox"	,
			"Default-Material"	,
			"Sprites-Default"	,
			"Default UI Material"	,
			"Text UI Material"	,
		};
		public static string[] BUILDIN_MATERIAL_DESTS =
		{
			"Engine/DefaultMaterials/Diffuse-Default" 	,
			"Engine/DefaultMaterials/Line-Default" 	,
			"Engine/DefaultMaterials/Particle-Default" 	,
			"Engine/DefaultMaterials/Skybox-Default" 	,
			"Engine/DefaultMaterials/Standard-Default" 	,
			"Engine/DefaultMaterials/Sprites-Default" 	,
			"Engine/DefaultMaterials/UI-Default"	,
			"Engine/DefaultMaterials/UI-Text"	,
		};

		public static string[] BUILDIN_SHADER_DEFAULT_TEXTURES =
		{
			"white" , "textures/framework/White.ktx" ,
			"black" , "textures/framework/Black.ktx" ,
			"grey" , "textures/framework/Gray.ktx" ,
			"bump" , "textures/framework/Normal.ktx" ,
			"red" , "textures/framework/Red.ktx" ,
		};
		public const string DEFAULT_TEXTURE_FOR_CUSTOM_SHADER = "textures/framework/Gray.ktx";


		public static string[] DEFAULT_VERTEX_SHADER =
		{
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Standard/Standard_vs.sc" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Standard/Standard_vs.sc" ,
		};

		public static string[] DEFAULT_SKIN_VERTEX_SHADER =
		{
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Standard/Standard_vs.sc" ,
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/Standard/Standard_vs.sc" ,
		};

		public static string DEFAULT_UI_VERTEX_SHADER =
			EXPORTER_DIRECTORY + "/Engine/DefaultShaders/UI/UI-Default";

		//
		public enum SHADER_TAGS
		{
			RenderType = 0 ,
			DisableBatching = 1 ,
			ForceNoShadowCasting = 2 ,
			IgnoreProjector = 3,
			CanUseSpriteAtlas = 4,
		};

		public const string DEFAULT_SCENE_PATH = "scene";

		public const string DEFAULT_COMPRESSED_FILE_PATH = "ExportedBundle/";
		public const string TEMPORARY_PATH = "Temp/";



		public static string[] EXCLUDED_PASSES =
		{
			"FORWARDADD",
			"DEFERRED",
			"SHADOWCASTER",
			"MOTIONVECTORS",
			"PREPASSBASE",
			"PREPASSFINAL",
			"VERTEX",
			"VERTEXLMRGBM",
			"VERTEXLM",
		};
		public static string[] EXCLUDED_PASSES_NEW_FORMAT =
		{
			"FORWARDADD",
			"DEFERRED",
			"MOTIONVECTORS",
			"PREPASSBASE",
			"PREPASSFINAL",
			"VERTEX",
			"VERTEXLMRGBM",
			"VERTEXLM",
		};

		public static bool USE_NEW_SHADER_FORMAT = true;
		public const string I3DSHADER_EXTENSION = ".i3dShader";

		public AssetPath asset_path = new AssetPath();

		// 注意这里是资源文件的root，即DEFAULT_EXPORT_SUBFOLDER + "/scene/"
		public string root_path;
		public string scene_path = DEFAULT_SCENE_PATH;
		public REPlatform platform;
		public string cubemap_name = "cubemaps/EnvRefProbe";
		public Queue< ExportTextureArgs > texture_export_tasks = new Queue<ExportTextureArgs>();


		public ExporterConfig( REPlatform p, bool sketch = false)
		{
			platform = p;
			TEXTURE_FORCE_LOW_QUALITY = sketch;
		}

		public static string GenReversedStateName( string name )
		{
			return "[reversed_" + name + "]";
		}
	}
}
