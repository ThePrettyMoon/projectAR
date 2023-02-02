using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Threading;
using ARWorldEditor;

namespace RenderEngine
{
	class ExporterScene
	{
		private static ProtoMath.transform TRANSFORM_IDENTITY = new ProtoMath.transform
		{
			Scale = new ProtoMath.float3{ X = 1 , Y = 1 , Z = 1 } ,
			RotType = ProtoMath.transform.Types.ROTATION.Quaternion ,
			Rotation = new ProtoMath.float4{ X = 0 , Y = 0 , Z = 0 , W = 1 } ,
			Translate = new ProtoMath.float3{ X = 0 , Y = 0 , Z = 0 } ,
		};

		public static List<string> scriptPathList = new List<string>();
		public static ProtoWorld.Entity ExportGameObject( ExporterConfig config , Transform tran , out bool error)
		{
			error = false;
			bool subError = false;
			// create an entity
			ProtoWorld.Entity ret
			= new ProtoWorld.Entity
			{ Version = ExporterConfig.VERSION
					, Name = tran.name
					, Transform = UtilityConverter.ConvertTransform( tran )
					, Enabled = tran.gameObject.activeSelf
					, Mask = (ulong)(1<<tran.gameObject.layer)
					, FrustumCulled = true }; // frustum为了兼容做视锥体剔除前的老资源，引擎里这个值默认为false

			try
			{
				//export Wind Zones
				WindZone wind = tran.GetComponent<WindZone>();
				if(wind != null)
				{
					ret.Wind = ConverterComponent.ConvertWindZone(config, wind);
				}
				//export particleSystem
				ParticleSystem ps = tran.GetComponent<ParticleSystem>();
				if(ps != null)
				{
					ret.Particle = ExporterParticle.Export(config, ps);
				}

				// export its videos
				UnityEngine.Video.VideoPlayer[] video_players = tran.GetComponents< UnityEngine.Video.VideoPlayer >();
				foreach( UnityEngine.Video.VideoPlayer video_player in video_players )
				{
					ProtoWorld.VideoPlayer vp = ConverterComponent.ConvertVideoPlayer( config, tran, video_player );
					if( null != vp )
						ret.VideoPlayers.Add( vp );
				}

				// export its materials
				List< string > export_material_files = new List<string>();
				MeshRenderer meshrenderer = tran.GetComponent<MeshRenderer>();
				MeshFilter meshfilter = tran.GetComponent<MeshFilter>();
				SkinnedMeshRenderer skinmesh = tran.GetComponent<SkinnedMeshRenderer>();
				bool shadowTwoSides = false;
				if( null != meshrenderer )
				{
					shadowTwoSides = (meshrenderer.shadowCastingMode == UnityEngine.Rendering.ShadowCastingMode.TwoSided);
					int index = 0;
					foreach( var m in meshrenderer.sharedMaterials )
					{
						if(m.HasProperty(Shader.PropertyToID("_ShadowCull")))
							m.SetInt("_ShadowCull", shadowTwoSides ? (int)UnityEngine.Rendering.CullMode.Off : (int)UnityEngine.Rendering.CullMode.Back);

						export_material_files.Add( ExporterResource.ExportMaterial( config , m , false , meshrenderer , null ) );
						++index;
					}
				}
				else if( null != skinmesh )
				{
					shadowTwoSides = (skinmesh.shadowCastingMode == UnityEngine.Rendering.ShadowCastingMode.TwoSided);
					int index = 0;
					foreach( var m in skinmesh.sharedMaterials )
					{
						if(m.HasProperty(Shader.PropertyToID("_ShadowCull")))
							m.SetInt("_ShadowCull", shadowTwoSides ? (int)UnityEngine.Rendering.CullMode.Off : (int)UnityEngine.Rendering.CullMode.Back);

						export_material_files.Add( ExporterResource.ExportMaterial( config , m , ExporterConfig.GPU_SKIN_ANIMATION ? true : false , skinmesh , null ) );
						++index;
					}
				}

				// export its animation
				Animator animator = tran.GetComponent< Animator >();
				if( null != animator && null != animator.runtimeAnimatorController )
				{
					AnimationClip[] animator_clips = animator.runtimeAnimatorController.animationClips;
					foreach( AnimationClip clip in animator_clips )
					{
						if( null != ExporterResource.FindProtoClip( ret.AnimationClips , clip.name ) )
							continue;
						ret.AnimationClips.Add(ExporterResource.ExportAnimation( config , clip ));
					}
					ret.AnimatorController = ExporterResource.ExportAnimatorController( config , animator );
				}

				// export its mesh
				Mesh mesh = null;
				string export_mesh_file = "";
				if( null != meshfilter )
				{
					mesh = meshfilter.sharedMesh;
					export_mesh_file = ExporterResource.ExportModel( config , mesh );
				}
				else if( null != skinmesh )
				{
					mesh = skinmesh.sharedMesh;
					export_mesh_file = ExporterResource.ExportModel( config , mesh , skinmesh );
				}

				// add material and mesh into ret
				if( export_material_files.Count > 0 && null != mesh )
				{
					ret.Renderer = new ProtoWorld.Renderer { Enabled = ( null != meshrenderer ? meshrenderer.enabled : ( null != skinmesh ? skinmesh.enabled : false ) ), ModelFile = export_mesh_file };
					ret.Renderer.MaterialFiles.AddRange( export_material_files );

					// export root bone
					if( null != skinmesh )
					{
						bool found = false;
						ret.Renderer.RootBone = ExporterUtility.FindRelativePath( skinmesh.rootBone , tran, out found );
					}

					// export shadow bool
					if (meshrenderer != null)
					{
						ret.Renderer.CastShadow = (int)meshrenderer.shadowCastingMode;
						ret.Renderer.ReceiveShadow = meshrenderer.receiveShadows;
						ret.Renderer.ReflectionProbeUsage = ExporterReflectionProbe.ConvertReflectionProbeUsage( meshrenderer.reflectionProbeUsage);
					}
				}

				// export its ScriptRunner
				List<ScriptRunner> scripts = new List<ScriptRunner>();
                UnityEngine.GameObject[] rootGameObjects = tran.gameObject.scene.GetRootGameObjects();
                foreach (UnityEngine.GameObject rootGameObject in rootGameObjects)
                {
					ScriptRunner[] runners = rootGameObject.GetComponentsInChildren<ScriptRunner>(true);
                    foreach (ScriptRunner runner in runners)
                    {
						if (runner.OverrideGameObject == null)
                        {
                            if(runner.transform == tran)
                                scripts.Add(runner);
                        }
                        else
                        {
                            if (runner.OverrideGameObject.transform == tran)
                                scripts.Add(runner);
                        }
                        
                    }
                }

				foreach( var scp in scripts )
				{
					if( null != scp.GetScriptPath() )
					{
						if( scp.GetScriptPath().Length > 0 )
						{
							string src = scp.GetScriptPath();
							ret.Scripts.Add( new ProtoWorld.ScriptRunner{ ScriptFile = src } );
							scriptPathList.Add(src);
							
							string dest = "";

							if( ExporterUtility.PrepareExport
								( src , config.root_path , Path.GetExtension( src ), out dest ) )
							{
								File.Copy( src , config.root_path + dest );
							}
						}
					}
				}

				{
					// export box colliders
					BoxCollider[] box_colliders = tran.GetComponents< BoxCollider >();
					foreach( var bc in box_colliders )
						ret.ColliderBox.Add( ConverterPhysics.ConvertCollider( bc ) );

					SphereCollider[] sphere_collider = tran.GetComponents< SphereCollider >();
					foreach( var sc in sphere_collider )
						ret.ColliderSphere.Add( ConverterPhysics.ConvertCollider( sc ) );

					// export rigidbody
					Rigidbody rigidbody = tran.GetComponent< Rigidbody >();
					if( null != rigidbody )
						ret.Rigidbody = ConverterPhysics.ConvertRigidbody( rigidbody );

					// export hinge joint
					HingeJoint hinge_joint = tran.GetComponent< HingeJoint >();
					if( null != hinge_joint )
						ret.HingeJoint = ConverterPhysics.ConvertHingJoint( tran , hinge_joint );
				}

				// export camera
				{
					Camera camera = tran.GetComponent< Camera >();
					if( null != camera )
						ret.Camera = ConverterComponent.ConvertCamera( config , camera , tran );
				}

				// export light
				{
					Light light = tran.GetComponent< Light >();
					if( null != light )
						ret.Light = ConverterComponent.ConvertLight( config , light );
				}

				// export audio source
				AudioSource[] audio_sources = tran.GetComponents< AudioSource >();
				foreach( var uas in audio_sources )
				{
					if( null != uas.clip )
					{
						string src = config.asset_path.GetPath( uas.clip );
						if( src.Length > 0 )
						{
							ret.AudioSources.Add( ConverterComponent.ConvertAudioSource( config , uas ) );
						}
					}
				}

				// export UI

				//export RectTransform
				{
					RectTransform rectTransform = tran.GetComponent< RectTransform > ();
					if ( null != rectTransform )
					{
						ret.RectTransform = UtilityConverter.ConvertRectTransform(rectTransform) ;
					}
				}

				// export CanvasRenderer
				{
					CanvasRenderer canvasRenderer = tran.GetComponent< CanvasRenderer > ();
					if ( null != canvasRenderer )
					{
						ret.CanvasRenderer = ConverterGUI.ConvertCanvasRenderer(config, canvasRenderer, tran);

					}
				}
				// export Canvas
				{
					Canvas canvas = tran.GetComponent< Canvas >();
					if ( null != canvas)
					{
						ret.Canvas = ConverterGUI.ConvertCanvas(config, canvas, tran.GetComponent< UnityEngine.UI.CanvasScaler > ());
					}
				}

				// export Button
				{
					UnityEngine.UI.Button button = tran.GetComponent< UnityEngine.UI.Button > ();
					if ( null != button)
					{
						ret.Button = ConverterGUI.ConvertButton(config, button);
					}
				}

				// export InputField
				{
					UnityEngine.UI.InputField inputField = tran.GetComponent< UnityEngine.UI.InputField > ();
					if (null != inputField)
					{
						ret.InputField = ConverterGUI.ConvertInputField(config, inputField);
					}
				}

				// traverse over its children
				for( int ci = 0 ; ci < tran.childCount ; ++ci )
				{
					Transform child = tran.GetChild( ci );
					ret.Children.Add( ExportGameObject( config , child , out subError) );
				}
			}
			catch (System.Exception e)
			{
				Transform t = tran;
				string path = t.name;
				while (t.parent != null)
				{
					t = t.parent;
					path = t.name + "/" + path;
				}
				UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
					, "导出 " + path + " 失败！\n" + e.Message
					, "OK");
				error = true;
			}
			error = error || subError;
			return ret;
		}

		// 返回true表示导出成功
		public static bool ExportScene( ExporterConfig config )
		{
			bool error = false;

			UnityEditor.EditorUtility.DisplayProgressBar(ExporterConfig.TITLE, "Export Scene ", 0);

			// initialize the Scene
			ProtoWorld.Scene scene
			= new ProtoWorld.Scene
			{ Version = ExporterConfig.VERSION , Name = "[Scene]" };

			// export the quality settings
			ProtoWorld.QualitySettings qualitySettings
			= new ProtoWorld.QualitySettings
			{ DefaultQualityLevel = (uint) UnityEngine.QualitySettings.GetQualityLevel() };

			// 枚举所有的level并进行保存
			string[] qualityNames = UnityEngine.QualitySettings.names;
			for ( int i = 0; i < qualityNames.Length; i ++)
			{
				UnityEngine.QualitySettings.SetQualityLevel(i, true);
				ProtoWorld.QualityLevel level = new ProtoWorld.QualityLevel
				{
					PixelLightCount = (uint)UnityEngine.QualitySettings.pixelLightCount,
					TextureQuality = (uint)UnityEngine.QualitySettings.masterTextureLimit,
					AnisotropicTextures = (uint)UnityEngine.QualitySettings.anisotropicFiltering,
					AntiAliasing = (uint)UnityEngine.QualitySettings.antiAliasing,

					Shadows = (uint)UnityEngine.QualitySettings.shadows,
					ShadowProjection = (uint)UnityEngine.QualitySettings.shadowProjection,
					ShadowResolution = (uint)UnityEngine.QualitySettings.shadowResolution,
					ShadowDistance = (uint)UnityEngine.QualitySettings.shadowDistance
				};
				qualitySettings.QualityLevels.Add(level);
			}
			scene.QualitySettings = qualitySettings;
			// 恢复默认level
			UnityEngine.QualitySettings.SetQualityLevel((int)qualitySettings.DefaultQualityLevel);

			// initialize the root entity
			ProtoWorld.Entity root
				= new ProtoWorld.Entity
				{ Name = "[Root]" , Transform = TRANSFORM_IDENTITY , Enabled = true , Mask = 0 , Rigidbody = ConverterPhysics.ConvertRigidbody( null ) };
			scene.Root = root;

			// traverse over all the game objects
			GameObject[] gos = Resources.FindObjectsOfTypeAll< GameObject >();
			foreach( GameObject obj in gos )
			{
				Transform tran = obj.transform;

				if( null != tran.parent ) continue;
				if( tran.hideFlags == HideFlags.NotEditable || tran.hideFlags == HideFlags.HideAndDontSave ) continue;
                if (obj.scene.name == null) continue;

                #if UNITY_EDITOR
                if ( Application.isEditor )
				{
					string asset_path = UnityEditor.AssetDatabase.GetAssetPath( tran.root.gameObject );
					if( false == string.IsNullOrEmpty( asset_path ) ) continue;
				}
				#endif

				root.Children.Add( ExportGameObject( config , tran , out error) );
				if (error)
				{
					UnityEditor.EditorUtility.ClearProgressBar();
					return false;
				}
			}

			// write the scene
			string dest;
			if( ExporterUtility.PrepareExport
				( config.scene_path , config.root_path , ExporterConfig.SCENE_BIN_EXTENSION , out dest ) )
			{
				ExporterUtility.WriteBinary( scene , config.root_path , dest );
			}
			if( ExporterUtility.PrepareExport
				( config.scene_path , config.root_path , ExporterConfig.SCENE_JSON_EXTENSION , out dest ) )
			{
				//ExporterUtility.WriteJson( scene , config.root_path , dest );
			}
			if( ExporterUtility.PrepareExport
				( config.scene_path , config.root_path , ExporterConfig.SCENE_GZIP_EXTENSION , out dest ) )
			{
				//ExporterUtility.WriteGzip( scene , config.root_path , dest );
			}


			var allSceneObject = Resources.FindObjectsOfTypeAll(typeof(GameObject));
			string imagePath = "";
			int limitCount = 0;
			foreach (var item in allSceneObject)
			{
				var image = (item as GameObject).GetComponent<UnityEngine.UI.Image>();
				if (image != null)
				{
					if (image.mainTexture == null)
					{
						limitCount++;
                        if (limitCount > 40)
                        {
							imagePath += "\n......";
							break;
                        }
						imagePath+=(GetScenePath(image.gameObject)+"\n");
						Debug.LogError(GetScenePath(image.gameObject));
					}
				}
			}

			if (!string.IsNullOrEmpty(imagePath))
            {
				UnityEditor.EditorUtility.DisplayDialog(ExporterConfig.TITLE
							, "不支持Unity的内置图片或精灵，请检查您的UI或Sprite，并把它们换成自定义图片。\n" + imagePath
							, "OK");
				UnityEditor.EditorUtility.ClearProgressBar();
				return error;
			}

			// export textures
			{
				int count = config.texture_export_tasks.Count;

				for (int ti = 0; ti < count; ++ti)
				{
					ExportTextureArgs args = config.texture_export_tasks.Dequeue();
					UnityEditor.EditorUtility.DisplayProgressBar(ExporterConfig.TITLE, "Export texture " + args.tex.name, ti / (float)count);
					try
					{
						ExporterResource.ExportTextureDo (config, args.tex, args.trans);
					}
					catch(System.Exception e)
					{
						UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
							, "导出 " + args.tex.ToString() + " 失败！\n" + e.Message
							, "OK");
						error = true;
					}
				}
				UnityEditor.EditorUtility.ClearProgressBar();
			}

			return !error;
		}

		public static string GetScenePath(GameObject gameObject)
        {
			var trans = gameObject.transform;
			string pathStr = "";
			List<string> pathList = new List<string>();
			var parent = trans.parent;

            while (parent != null)
            {
				pathList.Add(parent.name+"/");
				parent = parent.parent;
            }
            for (int i = pathList.Count - 1; i >=0 ; i--)
            {
				pathStr += pathList[i];
			}
			pathStr += gameObject.name;
			return pathStr;
        }
	}
}
