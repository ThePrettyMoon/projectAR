using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ARWorldEditor {

	public class AlgorithmGlobal
	{
		public const string FileDirectory = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/Files/";
		public const string imageDirectory = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/Images/";
		public const string objectARResourceDirectory = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/UserConfig/Resources/";
		public const string objectARResourceExtension = "*.zip";
		public static Dictionary<string, List<string>> algSceneType = new Dictionary<string, List<string>>();
		public static AlgorithmStruct algorithmData;
		public const string URL = "https://near.yuque.com/docs/share/f40e65f1-32dc-4c29-9094-cff5588483bd?#";
		public const string URL_CONTENT = "进一步了解";
		public const string URL_TITLE = "提示：此类型支持AR Kit、AR Core适配机型。";
		public const string DEFAULT_LOGO_PATH = "gif/c11thumb.png";

		public static void InitAlgorithmData()
		{
			Debug.Log("读取算法文件");
			try
			{
				//TODO，get config from web request
				string dataStr = File.ReadAllText(Application.streamingAssetsPath + "/ConfigJson.json", System.Text.Encoding.UTF8);
				algorithmData = JsonUtility.FromJson<AlgorithmStruct>(dataStr);
			}
			catch
			{
				Debug.LogError("算法文件读取失败");
			}

			foreach(var majorType in algorithmData.majorSceneList)
			{
				if(!majorType.isValid)
					continue;

				algSceneType[majorType.title] = new List<string>();
				foreach(var minorType in majorType.minorSceneList)
				{
					if(!minorType.isValid)
						continue;
						
					algSceneType[majorType.title].Add(minorType.title);
				}
			}
		}
	}

	


}