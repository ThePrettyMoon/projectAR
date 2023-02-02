using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UserConfig
{
    [System.Serializable]
    public class ExporterData
    {
        // 默认值0， 0表示SDK，1表示APP
        public int ExportType = 0;
        // 动态加载场景的prefab路径

        // prefab路径
		public string[] PrefabPaths;

        // sharelogo
        public string ShareLogoTexturePath;

        // js libraries
        public string[] JSLibrariesName;
        public string[] JSLibrariesCustomPath;
        public bool[] JSLibrariesCustomStatus;
    }

    class ExportSceneData
    {
        public RenderEngine.ExporterConfig.REPlatform platform;
        public RenderEngine.ExporterConfig config;
		public UnityEngine.Texture2D shareLogo;
        public List<SceneAsset> prefabs;
        public string[] JSLibrariesName;
        public List<TextAsset> JSLibrariesCustomAsset;
        public bool[] JSLibrariesCustomStatus;
        public bool sketch; 
        public bool bundle;
        public string parentPath;
        public string bundle_path;
    }
}