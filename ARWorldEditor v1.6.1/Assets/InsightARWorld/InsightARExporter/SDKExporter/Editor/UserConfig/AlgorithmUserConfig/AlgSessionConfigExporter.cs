#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using ARSession;
using ARWorldEditor;
using RenderEngine;

namespace UserConfig
{
	public class AlgSessionConfigExporter : BaseConfigWindow
	{
		public delegate void DeleteRFCallBack(string path);

		public static InsightARSetting insightARSetting;
		public static ExportStruct config;
		public static string configPath;
		const string markerFileName = "-desc.zip";
		const string tempDirectory = "InsightARConfig/";
		const string tempMarkerDirectory = tempDirectory + "markerTemp/";
		const string jsonConfigFileName = "config.json";

		public static void ExportConfig(string path, DeleteRFCallBack callback)
		{
			object[] algConfig = GameObject.FindObjectsOfType<InsightARSetting>();
			if(algConfig.Length == 0)
			{
				return;
			}

			// 清空目录方法回调
			if (null != callback)
				callback(path);
			Directory.CreateDirectory(path);

			insightARSetting = algConfig[0] as InsightARSetting;

			config = insightARSetting.configExport;


            if(!insightARSetting.useOverrideAlgorithmConfig)
            {

                configPath = path;

                if (config.ARSessionConfigs.scene == 501)
                    config.ARSessionConfigs.planeDetectType = 1; // 501中平面选项虽然不在界面上设置，但是输入json默认按1
                else if (config.ARSessionConfigs.scene / 100 == 9) // 手势识别
                {
                    config.ARSessionConfigs.detectGestures.Clear();
                    for (int i = 0; i < insightARSetting.enumValue.Length; ++i)
                    {
                        config.ARSessionConfigs.detectGestures.Add(insightARSetting.enumValue[i]);
                    }
                }

                string algAssetsPath = RenderEngine.ExporterConfig.EXPORTER_DIRECTORY + "/UserConfig/Resources/";
                if (config.ARSessionConfigs.scene / 100 == 4)
                {
                    if (insightARSetting.objectARResourcesPath.Length > 0)
						UnZipClass.Instance.UnZip(insightARSetting.objectARResourcesPath[insightARSetting.objectARIndex], path + "assets/");
				}
                else if (Directory.Exists(algAssetsPath + config.ARSessionConfigs.scene))
					RenderEngine.UtilityFileSystem.CopyDirectory(algAssetsPath + config.ARSessionConfigs.scene, path + "assets/", true);
				else if (config.ARSessionConfigs.scene / 100 == 8)
					RenderEngine.UtilityFileSystem.CopyDirectory(algAssetsPath + "Face/", path + "assets/", true);

				if (insightARSetting.markerList.Count > 0)
                    ExportMarkers();
                else
                    OnAllJsonDataDone(0, "");

            }
            else
            {
                if (insightARSetting.overrideAlgorithmConfig != null)
                {
                    string algAssetsPath = AssetDatabase.GetAssetPath(insightARSetting.overrideAlgorithmConfig);
                    Debug.Log("algAssetsPath"+ algAssetsPath);
                    RenderEngine.UtilityFileSystem.CopyDirectory(algAssetsPath, path, true);
                }
                else
                {
                    Debug.LogWarning("No overrideAlgorithmConfig");
                }
            }
        }

		static void ExportMarkers()
		{
			ARMarkerManager.TextureUploadURL = "http://mr-test.dongjian.netease.com/biz/nos/upload";
			ARMarkerManager.MarkerRequestURL = "http://mr-test.dongjian.netease.com/biz/at/genalg";

			// 创建临时图片文件目录
			if(!Directory.Exists(tempMarkerDirectory))
			{
				Directory.CreateDirectory(tempMarkerDirectory);
			}
			else
			{
				foreach( var filePath in Directory.GetFiles(tempMarkerDirectory) )
				{
					File.Delete(filePath);
				}
			}

			string prefix = GetTempFilePrefix(tempDirectory);
			bool descExist = File.Exists(prefix + markerFileName);

			// 服务器上已经传过的marker
			List<RemoteMarkerInfo> markerConfigs = insightARSetting.remoteMarkerList;
			// 用于上传图片的临时数组
			List<AlgorithmData.MarkerImage> tmpMarkerImageList = new List<AlgorithmData.MarkerImage>();
			int validMarkerCount = 0;
			foreach(var markerItem in insightARSetting.markerList)
			{
				if(!markerItem.isValid || markerItem.isRepeated || markerItem.imageFile == null)
					continue;

				AlgorithmData.MarkerImage tmp = new AlgorithmData.MarkerImage();
				Texture tex = markerItem.imageFile;
				string sourcePath = AssetDatabase.GetAssetPath(tex);
				string destPath = tempMarkerDirectory + markerItem.markerName + Path.GetExtension(sourcePath);
				// 拷一份出来，目标路径直接使用新名字，以免修改原图名称
				File.Copy(sourcePath, destPath);

				string md5 = Md5Sum(File.ReadAllBytes(destPath));
				tmp.md5 = md5;
				tmp.localPath = destPath;
				tmp.localGUIDPath = AssetDatabase.AssetPathToGUID(sourcePath);
				tmp.localName = markerItem.markerName;
				tmp.remotePath = "";
				tmp.remoteName = "";
				tmp.markerWidth = markerItem.markerWidth;

				// 查找图片是否之前已经上传过了
				for(int seek = 0; seek < markerConfigs.Count; ++seek)
				{
					if(descExist && markerConfigs[seek].md5 == md5)
					{
						tmp.remotePath = markerConfigs[seek].remotePath;
						tmp.remoteName = markerConfigs[seek].remoteName;
					}
				}		
				tmpMarkerImageList.Add(tmp);
				validMarkerCount++;
			}
			if(validMarkerCount == 0)
			{
				EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "请在InsightARSetting中选择识别图", "确定");
				ExporterMenu.AlgExportSuccess = false;
				return;
			}

			if(config.ARSessionConfigs.scene == 308)
			{
				if(validMarkerCount < 2)
				{
					EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "多图识别场景必须配置两张不同的识别图", "确定");
					ExporterMenu.AlgExportSuccess = false;
					return;
				}
			}

			if(tmpMarkerImageList.Count > 0)
			{
				insightARSetting.remoteMarkerList.Clear();
				config.ARSessionConfigs.markerNameWidth.Clear();
				for(int i = 0; i < tmpMarkerImageList.Count; i ++)
				{
					RemoteMarkerInfo newMarker = CopyFromMarkerImage(tmpMarkerImageList[i]);
					insightARSetting.remoteMarkerList.Add(newMarker);
					config.ARSessionConfigs.markerNameWidth.Add(new MarkerConfig(newMarker));
				}
				ARMarkerManager.Instance.RequestMarker(tmpMarkerImageList.ToArray(), OnAllJsonDataDone);
			}
			else
			{
				ExporterMenu.AlgExportSuccess = false;
			}

		}

		static void OnAllJsonDataDone(int status, string content, AlgorithmData.MarkerImage[] markerImageArray = null, byte[] wwwBytes = null)
		{
			if (status == 0)
			{
				// 保存图片的上传信息
				if (null != markerImageArray)
				{
					Debug.Log("上传算法图片完毕。 status : " + status + " , marker个数 ： " + markerImageArray.Length);
					for(int i = 0; i < markerImageArray.Length; i ++)
					{
						config.ARSessionConfigs.markerNameWidth[i].name = markerImageArray[i].localName;
						config.ARSessionConfigs.markerNameWidth[i].uid = Path.GetFileNameWithoutExtension(markerImageArray[i].remoteName);
						insightARSetting.remoteMarkerList[i].remoteName = markerImageArray[i].remoteName;
						insightARSetting.remoteMarkerList[i].remotePath = markerImageArray[i].remotePath;
					}
				}

				// 保存描述文件包
				if (null != wwwBytes)
					WriteFile(GetTempFilePrefix(tempDirectory) + markerFileName, wwwBytes);

				// 把存放marker文件的临时目录清空
				if(Directory.Exists(tempMarkerDirectory))
				{
					foreach( var filePath in Directory.GetFiles(tempMarkerDirectory) )
					{
						File.Delete(filePath);
					}
					Directory.Delete(tempMarkerDirectory);
				}

				BuildConfig();
				ExporterMenu.AlgExportSuccess = true;
			}
			else
			{
				Debug.Log("算法图片描述文件生成失败，请检查网络并重试：" + content);
				ExporterMenu.AlgExportSuccess = false;
			}

		}

		static void BuildConfig()
		{
			string prefix = GetTempFilePrefix(tempDirectory);
			WriteFile(configPath + jsonConfigFileName, JsonUtility.ToJson(config, true));
			if (insightARSetting.remoteMarkerList.Count > 0 && File.Exists(prefix + markerFileName)){
				UnZipClass.Instance.UnZip(prefix + markerFileName, configPath + "assets/");
				CheckUpMarkerDescs(configPath + "assets/");
			}
			// clear meta files
			RenderEngine.UtilityFileSystem.FileDeleteByExtension( configPath, ".meta" , true );
			Debug.Log("算法配置保存成功！");
		}

		// 用于在本次导出时没有上传图片文件时检测原有的desc文件中是否有多余的文件
		// 比如上一次导出时选了两张marker，这次导出删除了其中一张，就没有新的图片需要上传，也就会直接采用上一次的desc资源包
		static void CheckUpMarkerDescs(string dirc)
		{
			if(Directory.Exists(dirc))
			{
				foreach( var filePath in Directory.GetFiles(dirc) )
				{
					if(Path.GetExtension(filePath).Contains("desc"))
					{
						bool exist = false;
						for(int i = 0; i < insightARSetting.remoteMarkerList.Count; ++i)
						{
							if(Path.GetFileName(filePath).Contains(insightARSetting.remoteMarkerList[i].remoteName))
								exist = true;
						}
						if(!exist)
							File.Delete(filePath);
					}
				}
			}
				
		}

		static string Md5Sum(byte[] byteToEncrypt)
		{
			// encrypt bytes
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashBytes = md5.ComputeHash(byteToEncrypt);

			// Convert the encrypted bytes back to a string (base 16)
			string hashString = "";
			for (int i = 0; i < hashBytes.Length; i++)
				hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');

			return hashString.PadLeft(32, '0');
		}

		static RemoteMarkerInfo CopyFromMarkerImage(AlgorithmData.MarkerImage m)
		{
			RemoteMarkerInfo marker = new RemoteMarkerInfo();
			marker.md5 = m.md5;
			marker.remoteName = m.remoteName;
			marker.remotePath = m.remotePath;
			marker.localName = m.localName;
			marker.markerWidth = m.markerWidth;
			return marker;
		}

	}


}

#endif