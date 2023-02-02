using System.Collections.Generic;

namespace ARWorldEditor {
	[System.Serializable]
	public class AlgorithmStruct 
	{
		public List<MajorScene> majorSceneList = new List<MajorScene>();
	}

	[System.Serializable]
	public class MajorScene
	{
		public int sceneID;
		public bool isValid = true;
		public string title;
		public List<MinorScene> minorSceneList = new List<MinorScene>();
	}

	[System.Serializable]
	public class MinorScene
	{
		public int sceneID;
		public bool isValid = true;
		public string title;
		public string caption;
		public URLTip urlTip;
		public List<ConfigurationItem> configItemList = new List<ConfigurationItem>();

	}

	[System.Serializable]
	public class URLTip
	{
		public string caption;
		public string content;
		public string url;
	}

	public enum ConfigType { Marker, Bool, Text, Float, DropDown, CheckBox, Tip }

	[System.Serializable]
	public class ConfigurationItem
	{
		public string type = "Marker";
		public int itemMaxNum = 1;
		public bool imageDirectionEnabled = false;
		public string paramName;
		public string displayName;
		public string defaultValue;
		public string toolTip;
		public string tipContent;
		public string[] enumDisplay;
		public int[] enumValue;
		public string range;
	}


}

