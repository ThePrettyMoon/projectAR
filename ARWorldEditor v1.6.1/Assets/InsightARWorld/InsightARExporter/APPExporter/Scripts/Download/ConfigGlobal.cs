using UnityEngine;

namespace ARWorldEditor
{
    public class ConfigGlobal 
    {
        public const string MAP_PATH = "Assets/InsightARWorld/UserContent/Maps/"; //地图存储路径
        //public const string SCENE_PATH = "Assets/InsightARWorld/UserContent/Scenes/"; //场景存储路径
        #region 1.6.0
        public const string SCENE_MOBILE_PATH = "Assets/InsightARWorld/UserContent/Scenes_m/"; //手机场景存储路径
        public const string SCENE_GLASS_PATH = "Assets/InsightARWorld/UserContent/Scenes_g/"; //眼镜场景存储路径
        #endregion
        public const string MAP_TEMP_PATH = "../EditorDebug/Temp";
        public const string CACHE_FILE_NAME = "Cache.txt";
        public const string DEFAULT_CACHE_PATH = "../EditorDebug/Cache";
        public const string TEMPLATE_SCENE_PATH = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/template";
        public const string TEMPLATE_SIMGLASSSCENE_PATH = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/template/simglass_template.unity";
        public const string LOW_POLY_MODEL_NAME = "Land";
        public const string HIGH_POINT_TEMP_FOLDER = "InsightARWorld/UserContent/TEMP_PLY";
        public const string HIGH_POINT_MODEL_NAME = "LandPLY";
        public const string SCENE_WORLD_MODEL_NAME = "World";
        public const string REALITY_SCENE_MATERIAL_NAME = "RealitySceneMaterial.mat";
        public const string REALITY_SCENE_MATERIAL_PATH = "Assets/InsightARWorld/InsightARExporter/Utility/BuiltinResources/Materials";
        public const string REALITY_SCENE_MESH_NAME = "RealithSceneMeth.mesh";
        public const string RELAITY_SCENE_CLOUDPOINT_MATERIAL_NAME = "Default Point.mat";
        public const string REALITY_SCENE = "RealityScene";
        public const string MODEL_PREFAB_PATH = "Assets/InsightARWorld/InsightARExporter/Utility/BuiltinResources/Prefab";
        public const string TOOL_MAN_NAME = "toolman.prefab";
        public const string LEVELS_NAME = "levels.json";
        public const string TRANSFORM_NAME = "transform.json";
        public const string PREFAB_DIRECTORY = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/Prefabs/";
        public const string POI_PREFAB = "defaultpoilabel.prefab";
        public const string FileDirectory = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/Files/";
        public const string ImageDirectory = "Assets/InsightARWorld/InsightARExporter/SDKExporter/Editor/Images/";

        //Nav 
        public const string NAV_ROOT = "Assets/InsightARWorld/InsightARExporter/Utility/BuiltinResources/Prefab/Nav/naviroot.prefab";
        public const string NAV_PREFAB_PATH = "Assets/InsightARWorld/InsightARExporter/Utility/BuiltinResources/Prefab/Nav";
        public const string NAV_ARROW_PATH = "Nav_Arrow.prefab";
        public const string NAV_UPSTAIRS_PATH = "Nav_UpStairs.prefab";
        public const string NAV_TURN_PATH = "Nav_RoadSign.prefab";
        public const string NAV_DEST_PATH = "Nav_end.prefab";
        //tag
        public const string TAG_REALITYSCENE = REALITY_SCENE;
        //cloudplayback
        public const string CLOUD_PLAY_BACK_PATH = "CloudPlayBackData/";

        public static string DataPath
        {
            get
            {
                return Application.dataPath;
            }
        }
    }
}
