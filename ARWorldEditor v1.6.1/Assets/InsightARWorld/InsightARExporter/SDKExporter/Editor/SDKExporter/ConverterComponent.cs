using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;

namespace RenderEngine
{
	class ConverterComponent
	{
		public static ProtoWorld.Camera ConvertCamera
			( ExporterConfig config
			, UnityEngine.Camera camera, UnityEngine.Transform tran )
		{
			ProtoWorld.CAMERA_CLEAR clear;
			ProtoWorld.BACKGROUND background;
			ProtoMath.float4 viewrect = new ProtoMath.float4();

			// 主相机的background一定为skybox
			if (camera.tag == "MainCamera")
				camera.clearFlags = UnityEngine.CameraClearFlags.Skybox;

			switch( camera.clearFlags )
			{
			default:
			case UnityEngine.CameraClearFlags.Skybox:
				clear = ProtoWorld.CAMERA_CLEAR.ColorDepth;
				background = ProtoWorld.BACKGROUND.Skybox;
				break;
			case UnityEngine.CameraClearFlags.SolidColor:
				//case UnityEngine.CameraClearFlags.Color:
				clear = ProtoWorld.CAMERA_CLEAR.ColorDepth;
				background = ProtoWorld.BACKGROUND.Color;
				break;
			case UnityEngine.CameraClearFlags.Depth:
				clear = ProtoWorld.CAMERA_CLEAR.DepthOnly;
				background = ProtoWorld.BACKGROUND.Color;
				break;
			case UnityEngine.CameraClearFlags.Nothing:
				clear = ProtoWorld.CAMERA_CLEAR.DoNotClear;
				background = ProtoWorld.BACKGROUND.Color;
				break;
			}

			viewrect.X = camera.rect.x;
			viewrect.Y = camera.rect.y;
			viewrect.Z = camera.rect.width;
			viewrect.W = camera.rect.height;

			ProtoWorld.Camera ret = new ProtoWorld.Camera
			{ 
				Enabled = camera.enabled
				, Fov = camera.fieldOfView
				, Aspect = camera.aspect
				, Near = camera.nearClipPlane
				, Far = camera.farClipPlane
				, Viewrect = viewrect
				, Order = camera.depth
				, Mask = ( camera.cullingMask == -1 ? ExporterConfig.MASK_EVERY_THING : (ulong)camera.cullingMask )
				, FreeCamera = ( false == camera.tag.Equals( "MainCamera" ) )
				, Clear = clear
				, Background = background
				, HashId = camera.GetHashCode(),
			};

			// export target
			if( null != camera.targetTexture )
				ret.Target = ExporterResource.ExportTextureTask( config, camera.targetTexture, new UnityEngine.Vector4(1,1,0,0) );

			// export skybox
			{ //HDR map does not work with current render engine,
				// use oridinary texture for skybox instead.
				UnityEngine.Skybox skybox = camera.GetComponent<UnityEngine.Skybox>();
				ret.UseCustomSkybox = (null != skybox);
				UnityEngine.Material skybox_material = ( ret.UseCustomSkybox ? skybox.material : UnityEngine.RenderSettings.skybox );

				if( null != skybox_material )
				{
					//export skybox
					UnityEngine.GameObject probeGameObject;
					UtilityUnityCreator.CreateReflectionProbe (config, out probeGameObject);
					ret.Skybox = ExporterReflectionProbe.ExportProbeAsSkybox (config, probeGameObject.GetComponent<UnityEngine.ReflectionProbe>());
					UtilityUnityCreator.Delete (probeGameObject);

					//export ReflectionProbe 
					//we can have any number of ReflectionProbe, because of probe has range, but in shader just use one, because of 8 texture limit
					//so we just blend skybox and reflectionProbe 
					UnityEngine.ReflectionProbe[] probes = UnityEngine.GameObject.FindObjectsOfType<UnityEngine.ReflectionProbe> ();
					if (probes.Length > 0) 
					{
						int index = 0;
						foreach (var probe in probes) 
						{
							ProtoWorld.ReflectionProbe reflectionProbe = ExporterReflectionProbe.ExportReflectionProbe (config, probe, index);
							ret.ReflectionProbes.Add (reflectionProbe);
							index++;
						}

						UnityEditor.Lightmapping.Clear ();
						UnityEditor.Lightmapping.Bake ();
					}				

				}
				else
				{
					ret.Skybox = "";
				}
			}

			// export sphere harmonic lighting
			{
				string str = "";
				// for( int i = 0 ; i < 3 ; ++i )
				// {
				// 	for( int j = 0 ; j < 9 ; ++j )
				// 		str += UnityEngine.RenderSettings.ambientProbe[i,j] + " ";
				// 	str += "\n";
				// }
				// UnityEngine.Debug.Log( "SH Light: " + str );
				// UnityEngine.Debug.Log( "SH ambientEquatorColor " + UnityEngine.RenderSettings.ambientEquatorColor );
				// UnityEngine.Debug.Log( "SH ambientGroundColor " + UnityEngine.RenderSettings.ambientGroundColor );
				// UnityEngine.Debug.Log( "SH ambientIntensity " + UnityEngine.RenderSettings.ambientIntensity );
				// UnityEngine.Debug.Log( "SH ambientLight " + UnityEngine.RenderSettings.ambientLight );
				// UnityEngine.Debug.Log( "SH ambientSkyColor " + UnityEngine.RenderSettings.ambientSkyColor );


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

			// export post effects
			{
				EnginePostEffect[] effects = tran.GetComponents< EnginePostEffect >();
				foreach( var effect in effects )
				{
					if( null != effect._material )
					{
						// export material
						string material_file_name = ExporterResource.ExportMaterial( config , effect._material , false , null , effect._using_depth );

						// create post effect
						ProtoWorld.PostEffect pe = new ProtoWorld.PostEffect(){ Name = effect._material.name , Material = material_file_name };

						// set sources and targets
						for( int pi = 0 ; pi < effect._material.passCount ; ++pi )
						{
							string tar = "";
							if( effect._targets.Length > pi )
							if( effect._targets[pi] != null )
								tar = ExporterResource.ExportTextureTask( config, effect._targets[pi], new UnityEngine.Vector4(1,1,0,0) );
							pe.Targets.Add( tar );
						}

						// add
						ret.PostEffects.Add( pe );
					}
				}
			}

			return ret;
		}

		public static ProtoWorld.WindZone ConvertWindZone( ExporterConfig config , UnityEngine.WindZone wind )
		{
			ProtoWorld.WindZone ret = new ProtoWorld.WindZone();
			ret.WindMain = wind.windMain;
			ret.WindTurbulence = wind.windTurbulence;
			ret.WindPulseFrequency = wind.windPulseFrequency;
			ret.WindPulseMagnitude = wind.windPulseMagnitude;
			switch( wind.mode )
			{
			case UnityEngine.WindZoneMode.Directional:
				ret.Mode = ProtoWorld.WindZoneMode.WzmDirectional;
				break;
			case UnityEngine.WindZoneMode.Spherical:
				ret.Mode = ProtoWorld.WindZoneMode.WzmSpherical;
				break;
			}
			
			return ret;
		}

		public static ProtoWorld.Light ConvertLight( ExporterConfig config , UnityEngine.Light light )
		{
			ProtoWorld.Light ret = new ProtoWorld.Light
			{
				Enabled = light.enabled ,
				Color = UtilityConverter.ConvertColor( light.color ) ,
				Intensity = light.intensity , ShadowStrength = light.shadowStrength ,
			};
			switch( light.type )
			{
			case UnityEngine.LightType.Directional:
				ret.Type = ProtoWorld.Light.Types.TYPE.Directional;
				break;
			case UnityEngine.LightType.Point:
				ret.Type = ProtoWorld.Light.Types.TYPE.Point;
				ret.Range = light.range;
				break;
			case UnityEngine.LightType.Spot:
				ret.Type = ProtoWorld.Light.Types.TYPE.Spot;
				ret.Range = light.range;
				ret.Angle = light.spotAngle;
				break;
			case UnityEngine.LightType.Area:
				ret.Type = ProtoWorld.Light.Types.TYPE.Area;
				break;
			}
			switch( light.shadows )
			{
			case UnityEngine.LightShadows.None:
				ret.ShadowType = ProtoWorld.Light.Types.SHADOW_TYPE.No;
				break;
			case UnityEngine.LightShadows.Hard:
				ret.ShadowType = ProtoWorld.Light.Types.SHADOW_TYPE.Hard;
				break;
			case UnityEngine.LightShadows.Soft:
				ret.ShadowType = ProtoWorld.Light.Types.SHADOW_TYPE.Soft;
				break;
			}

			// calculate a ortho cam for directional light shadowmap
			if (light.type == UnityEngine.LightType.Directional)
			{
				UnityEngine.Matrix4x4 lightWorldToLocalMatrix = light.transform.worldToLocalMatrix;
				UnityEngine.Matrix4x4 lightLocalToWorldMatrix = light.transform.localToWorldMatrix;
				UnityEngine.Matrix4x4 matrix_negatez = UnityEngine.Matrix4x4.identity;
				matrix_negatez.m22 = -1;
				
				UnityEngine.Camera cam = UnityEngine.Camera.main != null ? 
											UnityEngine.Camera.main : 
											(UnityEngine.Camera.allCameras.Length > 0 ? UnityEngine.Camera.allCameras[0] : null);

				if (cam == null)
					return ret;

				UnityEngine.Matrix4x4 inverse_matrix_v = UnityEngine.Matrix4x4.Inverse( matrix_negatez *  cam.worldToCameraMatrix);
				
				UnityEngine.Vector3[] frustumCornersNear = new UnityEngine.Vector3[4];
				UnityEngine.Vector3[] frustumCornersFar = new UnityEngine.Vector3[4];
				cam.CalculateFrustumCorners(new UnityEngine.Rect(0, 0, 1, 1), cam.nearClipPlane, UnityEngine.Camera.MonoOrStereoscopicEye.Mono, frustumCornersNear);
				cam.CalculateFrustumCorners(new UnityEngine.Rect(0, 0, 1, 1), cam.farClipPlane, UnityEngine.Camera.MonoOrStereoscopicEye.Mono, frustumCornersFar);

				float xnear = float.MaxValue, ynear = float.MaxValue, znear = float.MaxValue;
				float xfar = -float.MaxValue, yfar = -float.MaxValue, zfar = -float.MaxValue;

				for (int i = 0; i < 4; i++)
				{
					var worldSpaceCornerNear = inverse_matrix_v * new UnityEngine.Vector4(frustumCornersNear[i].x,frustumCornersNear[i].y,frustumCornersNear[i].z,1);
					var worldSpaceCornerFar = inverse_matrix_v * new UnityEngine.Vector4(frustumCornersFar[i].x,frustumCornersFar[i].y,frustumCornersFar[i].z,1);
					var lightLocalSpaceCornerNear = lightWorldToLocalMatrix * worldSpaceCornerNear;
					var lightLocalSpaceCornerFar = lightWorldToLocalMatrix * worldSpaceCornerFar;

					if (xnear > lightLocalSpaceCornerNear.x)
						xnear = lightLocalSpaceCornerNear.x;

					if (xnear > lightLocalSpaceCornerFar.x)
						xnear = lightLocalSpaceCornerFar.x;

					if (xfar < lightLocalSpaceCornerNear.x)
						xfar = lightLocalSpaceCornerNear.x;

					if (xfar < lightLocalSpaceCornerFar.x)
						xfar = lightLocalSpaceCornerFar.x;	
					
					if (ynear > lightLocalSpaceCornerNear.y)
						ynear = lightLocalSpaceCornerNear.y;

					if (ynear > lightLocalSpaceCornerFar.y)
						ynear = lightLocalSpaceCornerFar.y;

					if (yfar < lightLocalSpaceCornerNear.y)
						yfar = lightLocalSpaceCornerNear.y;

					if (yfar < lightLocalSpaceCornerFar.y)
						yfar = lightLocalSpaceCornerFar.y;	

					if (znear > lightLocalSpaceCornerNear.z)
						znear = lightLocalSpaceCornerNear.z;

					if (znear > lightLocalSpaceCornerFar.z)
						znear = lightLocalSpaceCornerFar.z;

					if (zfar < lightLocalSpaceCornerNear.z)
						zfar = lightLocalSpaceCornerNear.z;

					if (zfar < lightLocalSpaceCornerFar.z)
						zfar = lightLocalSpaceCornerFar.z;					
				}

				UnityEngine.Vector4 OBBworldSpaceCornerNear0 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xnear,ynear,znear, 1));
				// UnityEngine.Vector4 OBBworldSpaceCornerNear1 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xnear,yfar,znear, 1));
				// UnityEngine.Vector4 OBBworldSpaceCornerNear2 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xfar,ynear,znear, 1));
				UnityEngine.Vector4 OBBworldSpaceCornerNear3 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xfar,yfar,znear, 1));

				// UnityEngine.Vector4 OBBworldSpaceCornerFar0 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xnear,ynear,zfar, 1));
				// UnityEngine.Vector4 OBBworldSpaceCornerFar1 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xnear,yfar,zfar, 1));
				// UnityEngine.Vector4 OBBworldSpaceCornerFar2 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xfar,ynear,zfar, 1));
				// UnityEngine.Vector4 OBBworldSpaceCornerFar3 = lightLocalToWorldMatrix * (new UnityEngine.Vector4(xfar,yfar,zfar, 1));
				
				//OthoCam.transform.position = lightLocalToWorldMatrix* (new Vector4((xfar + xnear) / 2, (yfar + ynear) / 2, znear, 1));
				ret.OrthoPosition = UtilityConverter.ConvertVector3((OBBworldSpaceCornerNear3 + OBBworldSpaceCornerNear0) / 2);
				ret.OrthoSize = (yfar - ynear) / 2;
				ret.OrthoNear = 0;
				ret.OrthoFar = zfar - znear;
			}

			return ret;
		}

		public static ProtoWorld.AudioSource ConvertAudioSource( ExporterConfig config , UnityEngine.AudioSource audio_source )
		{
			if( audio_source.rolloffMode == UnityEngine.AudioRolloffMode.Custom )
				UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
					, "exporting " + audio_source.name + ", while AudioRolloffMode.Custom is not supported."
					, "OK!");

			ProtoWorld.AudioSource ret = new ProtoWorld.AudioSource
			{
				Enabled = audio_source.enabled ,
				Name = audio_source.clip.name ,

				Mute = audio_source.mute ,
				BypassEffect = audio_source.bypassEffects ,
				BypassListenerEffect = audio_source.bypassListenerEffects ,
				BypassReverbZones = audio_source.bypassReverbZones ,
				PlayOnAwake = audio_source.playOnAwake ,
				Loop = audio_source.loop ,

				Priority = audio_source.priority ,
				Volume = audio_source.volume ,
				Pitch = audio_source.pitch ,
				StereoPan = audio_source.panStereo ,
				SpatialBlend = audio_source.spatialBlend ,
				ReverbZoneMix = audio_source.reverbZoneMix ,

				DopplerLevel = audio_source.dopplerLevel ,
				Spread = audio_source.spread ,
				Rolloff = audio_source.rolloffMode == UnityEngine.AudioRolloffMode.Logarithmic
					? ProtoWorld.AudioSource.Types.ROLLOFF.Logarithmic
					: ProtoWorld.AudioSource.Types.ROLLOFF.Linear ,
				MinDistance = audio_source.minDistance ,
				MaxDistance = audio_source.maxDistance ,
			};

            ret.Filename = ExporterResource.ExportAudioClip(config, audio_source.clip);

			return ret;
		}

		public static ProtoWorld.VideoPlayer ConvertVideoPlayer
			( ExporterConfig config
			, UnityEngine.Transform owner
			, UnityEngine.Video.VideoPlayer video_player )
		{
			// check renderer
			int material_index = 0;
			string renderer_path = "";
			string target_property = "_MainTex";
			if (null == video_player.targetMaterialRenderer)
			{
				// the targetMaterialRenderer == null, maybe owner is a post effect camera
				if( null == owner.gameObject.GetComponent< UnityEngine.Camera >() )
				{
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
						, video_player.ToString () + ": VideoPlayer的Renderer为空，且不是后处理相机。\n可能是Unity的Bug，请重新设置一遍Renderer。或者添加后处理相机。"
						, "OK!");
					return null;
				}

				EnginePostEffect[] epe = owner.gameObject.GetComponents<EnginePostEffect> ();
				if (0 == epe.Length)
				{
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
						, video_player.ToString () + ": VideoPlayer的Renderer为空，且不是后处理相机。\n可能是Unity的Bug，请重新设置一遍Renderer。或者添加后处理相机。"
						, "OK!");
					return null;
				}

				UnityEngine.Material material = null;
				for (int mi = epe.Length - 1; mi >= 0; --mi)
				{
					UnityEngine.Material mat = epe[mi]._material;
					if (mat.HasProperty (target_property))
					{
						if (null != material)
						{
							UnityEditor.EditorUtility.DisplayDialog( ExporterConfig.TITLE
								, video_player.ToString() + ": 在多个PostEffect上找到Material含有“" + target_property + "”，自动选取index较小的那个PostEffect。"
								, "OK!" );
						}
						material_index = mi;
						material = mat;
					}
				}

				if (null == material)
				{
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
						, video_player.ToString () + ": VideoPlayer用于后处理相机。但后处理Material不包含\"_MainTex\"用于播放。"
						, "OK!");
					return null;
				}
			}
			else
			{

				// find renderer
				bool found = false;
				renderer_path = ExporterUtility.FindRelativePath( video_player.targetMaterialRenderer.gameObject.transform, owner, out found );
				if (false == found)
				{
					UnityEditor.EditorUtility.DisplayDialog( ExporterConfig.TITLE
						, video_player.ToString() + ": 无法找到指向VideoPlayer Renderer的相对路径。"
						, "OK!" );
					return null;
				}

				// find material
				UnityEngine.Material material = null;
				for (int mi = video_player.targetMaterialRenderer.sharedMaterials.Length-1; mi >= 0; --mi)
				{
					UnityEngine.Material mat = video_player.targetMaterialRenderer.sharedMaterials[mi];
					if(mat.HasProperty (video_player.targetMaterialProperty))
					{
						if (null != material)
						{
							UnityEditor.EditorUtility.DisplayDialog( ExporterConfig.TITLE
								, video_player.ToString() + ": 在Renderer上找到多个Material含有“Material Propety”，自动选取index较小的那个material。"
								, "OK!" );
						}
						material_index = mi;
						material = mat;
					}
				}

				if (null == material)
				{
					UnityEditor.EditorUtility.DisplayDialog( ExporterConfig.TITLE
						, video_player.ToString() + ": 无法在Renderer上找到Material含有“Material Propety”。"
						, "OK!" );
					return null;
				}

				target_property = video_player.targetMaterialProperty;
			}

			// export video clip
			if (null == video_player.clip)
			{
				UnityEditor.EditorUtility.DisplayDialog( ExporterConfig.TITLE
					, video_player.ToString() + ": VideoClip为空。"
					, "OK!" );
				return null;
			}

			string path_to_movie = ExporterResource.ExportMovie( config , video_player.clip );
			if (null == path_to_movie || 0 == path_to_movie.Length)
			{
				UnityEditor.EditorUtility.DisplayDialog( ExporterConfig.TITLE
					, video_player.ToString() + ": 导出VideoClip失败。"
					, "OK!" );
				return null;
			}

			// export the video player
			ProtoWorld.VideoPlayer ret = new ProtoWorld.VideoPlayer
			{
				Name = video_player.name, VideoClip = path_to_movie,
				PlayOnAwake = video_player.playOnAwake, Loop = video_player.isLooping, Speed = video_player.playbackSpeed,
				Entity = renderer_path, Material = material_index, Property = target_property,
				Volume = video_player.GetDirectAudioVolume( 0 ), 
			};

			return ret;
		}
	}
}