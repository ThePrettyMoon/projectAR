using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ARWorldEditor
{
	
	[System.Serializable]
	public class MarkerInfo
	{
		public bool isValid;
		public bool isRepeated;
		public Texture imageFile;
		public string markerName = "";
		public float markerWidth = 1;
		public int markerDirection = 1;

		public static bool DuplicationMarkerName(List<MarkerInfo> markerList, string newName)
		{
			foreach(var item in markerList)
			{
				if(item.markerName != "" && item.markerName.ToLower() == newName.ToLower())
					return true;
			}
			return false;
		}

		public static bool IsRepeatedTexture(List<MarkerInfo> markerList, int newMarkerIndex)
		{
			for(int i = 0; i < newMarkerIndex; ++i)
			{
				if(markerList[i].imageFile && markerList[i].imageFile.Equals(markerList[newMarkerIndex].imageFile))
					return true;
			}
			return false;
		}
		
	}

	[System.Serializable]
	public class RemoteMarkerInfo
	{
		public string md5 = "";
		public string remotePath = "";
		public string remoteName = "";
		public string localName = "";
		public float markerWidth = 1.0f;

		public void Copy(RemoteMarkerInfo m)
		{
			this.md5 = m.md5;
			this.remotePath = m.remotePath;
			this.remoteName = m.remoteName;
		}
	}


	[System.Serializable]
	public class ExportStruct
	{
		public SessionConfigs ARSessionConfigs;
	}

	[System.Serializable]
	public class SessionConfigs 
	{
		public int scene;
		public int UIOrientationPortrait = 0;
		public bool isEnableLightEstimate = false;
		public int planeDetectType = 0;
		public int markerDirection = 0;
		public List<MarkerConfig> markerNameWidth = new List<MarkerConfig>();
		public float markerWidth = 1.0f;
		public int markerTrackingMaxNum = 0;
		public float groundDistance = 1.0f;
		public bool isUseFrontCamera = false;
		public List<CheckBoxItem> detectGestures = new List<CheckBoxItem>();
	}

	[System.Serializable]
	public class MarkerConfig
	{
		public string name;
		public string uid;
		public float width;
		public MarkerConfig(RemoteMarkerInfo m)
		{
			this.name = m.localName;
			this.uid = Path.GetFileNameWithoutExtension(m.remoteName);
			this.width = m.markerWidth;
		}
	}

	[System.Serializable]
	public class CheckBoxItem
	{
		public string name;
		public bool enable = true;
		public CheckBoxItem(string n, bool e)
		{
			name = n;
			enable = e;
		}
	}


}


