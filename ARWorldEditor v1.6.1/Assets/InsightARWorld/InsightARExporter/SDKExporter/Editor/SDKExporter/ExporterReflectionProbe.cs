using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RenderEngine
{
	static class ExporterReflectionProbe 
	{
		public static ProtoWorld.ReflectionProbeUsage ConvertReflectionProbeUsage( UnityEngine.Rendering.ReflectionProbeUsage u)
		{
			ProtoWorld.ReflectionProbeUsage ret = new ProtoWorld.ReflectionProbeUsage ();
			switch (u) {
			case UnityEngine.Rendering.ReflectionProbeUsage.BlendProbes:
				ret = ProtoWorld.ReflectionProbeUsage.RpuBlendProbes;
				break;
			case UnityEngine.Rendering.ReflectionProbeUsage.BlendProbesAndSkybox:
				ret = ProtoWorld.ReflectionProbeUsage.RpuBlendProbesAndSkybox;
				break;
			case UnityEngine.Rendering.ReflectionProbeUsage.Off:
				ret = ProtoWorld.ReflectionProbeUsage.RpuOff;
				break;
			case UnityEngine.Rendering.ReflectionProbeUsage.Simple:
				ret = ProtoWorld.ReflectionProbeUsage.RpuSimple;
				break;
			}
			return ret;
		}
		public static string ExportProbeAsSkybox(ExporterConfig config, ReflectionProbe reflectionProbe)
		{
			string ret = "";
			//disable static object
			List<GameObject> staticGameObjectList = new List<GameObject>();
			var renderers = GameObject.FindObjectsOfType<Renderer>();
			foreach (var render in renderers) {
				GameObject go = render.gameObject;
				if (go.isStatic) {
					go.isStatic = false;
					staticGameObjectList.Add (go);
				}
			}


			string args = ExporterConfig.TEXTURE_CUBEMAP_QUALITY_ARGS[(int)config.platform];
			string exrTemporaryDirectory = ExporterConfig.TEMPORARY_PATH;
			if (!System.IO.Directory.Exists(exrTemporaryDirectory))
			{
				System.IO.Directory.CreateDirectory(exrTemporaryDirectory);
			}


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
						( "exr2png" , " -i " + "\"" + exrTemporaryPath + "\"" 
						+ " -o " + "\"" + pngTemporaryPath + "\""
						+ " --sh");
					if (err.Length > 0 && err.Contains("ERROR: "))
						UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
							, "failed to convert " + exrTemporaryPath + " to PNG"
							, "OK!");

					ret = ExporterResource.Export6SidedCubemapTexture(config, config.cubemap_name + "Skybox", pngTemporaryPath, args);
				}
				else
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
						, "failed to bake ReflectionProbe " + pngTemporaryPath
						, "OK!");

				File.Delete(exrTemporaryPath);


			}

			foreach (var go in staticGameObjectList) 
			{
				go.isStatic = true;
			}

			return ret;
		}
		public static ProtoWorld.ReflectionProbe ExportReflectionProbe( ExporterConfig config , ReflectionProbe reflectionProbe, int probeIndex = 0)
		{		
			ProtoWorld.ReflectionProbe ret = new ProtoWorld.ReflectionProbe ();
			Vector3 vPos = reflectionProbe.transform.position;

			//pos.w > 0 表示采用boxProjection 这里设置为1
			if (reflectionProbe.boxProjection)
				ret.Pos = UtilityConverter.ConvertVector4 (new UnityEngine.Vector4 (vPos.x, vPos.y, vPos.z, 1.0f));
			else
				ret.Pos = UtilityConverter.ConvertVector4 (new UnityEngine.Vector4 (vPos.x, vPos.y, vPos.z, 0.0f));

			//min.w 表示两个probe的插值weight
			//if(probeIndex == 1)
			ret.Min = UtilityConverter.ConvertVector4 (new UnityEngine.Vector4 (reflectionProbe.bounds.min.x, reflectionProbe.bounds.min.y, reflectionProbe.bounds.min.z, 1.0f));
			//else 
			//ret.Min = UtilityConverter.ConvertVector4 (new UnityEngine.Vector4 (reflectionProbe.bounds.min.x, reflectionProbe.bounds.min.y, reflectionProbe.bounds.min.z, 0.5f));
			ret.Max = UtilityConverter.ConvertVector4 (new UnityEngine.Vector4 (reflectionProbe.bounds.max.x, reflectionProbe.bounds.max.y, reflectionProbe.bounds.max.z, 1.0f));


			//Cubemap texcube = (Cubemap)reflectionProbe.bakedTexture;

			string args = ExporterConfig.TEXTURE_CUBEMAP_QUALITY_ARGS[(int)config.platform];

			string exrTemporaryDirectory = ExporterConfig.TEMPORARY_PATH;
			if (!System.IO.Directory.Exists(exrTemporaryDirectory))
			{
				System.IO.Directory.CreateDirectory(exrTemporaryDirectory);
			}


			{
				string exrTemporaryName = "EnvRefProbe" + probeIndex;
				string exrTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, exrTemporaryName + ".exr");
				string pngTemporaryPath = System.IO.Path.Combine(exrTemporaryDirectory, exrTemporaryName);

				// Create an empty file first to avoid 'textureimporter'
				FileStream exrStream = File.Create(exrTemporaryPath);
				exrStream.Close();

				// Generate exr file
				if (UnityEditor.Lightmapping.BakeReflectionProbe(reflectionProbe, exrTemporaryPath))
				{
					string err = UtilityExecute.ExecuteTool
						( "exr2png" , " -i " + "\"" + exrTemporaryPath + "\"" 
						+ " -o " + "\"" + pngTemporaryPath + "\""
						+ " --sh");
					if (err.Length > 0 && err.Contains("ERROR: "))
						UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
							, "failed to convert " + exrTemporaryPath + " to PNG"
							, "OK!");


					ret.Filename = ExporterResource.Export6SidedCubemapTexture(config, config.cubemap_name + probeIndex, pngTemporaryPath, args);
				}
				else
					Debug.LogError("Bake Reflection Probe Failed!");

				File.Delete(exrTemporaryPath);
			}


			ret.Hdr = UtilityConverter.ConvertVector4(reflectionProbe.textureHDRDecodeValues);


			UnityEditor.Lightmapping.Clear ();				
			UnityEditor.Lightmapping.Bake ();
			return ret;

		}
	}
}
