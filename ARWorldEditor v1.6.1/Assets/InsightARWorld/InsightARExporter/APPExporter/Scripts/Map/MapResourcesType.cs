namespace ARWorldEditor
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum MapResourcesType
    {
        RESOURCE_TYPE_SCENE_PACKAGE = 1,  //场景内容包
        RESOURCE_TYPE_LOC_MAP_CONFIG = 2, //定位地图配置文件
        RESOURCE_TYPE_MOBILE_LOC_MAP = 3, //移动端定位地图
        RESOURCE_TYPE_HIGH_POLY_PLY = 4,  //高精点云模型
        RESOURCE_TYPE_HIGH_POLY_MESH = 5, //高模
        RESOURCE_TYPE_LOW_POLY_MESH = 6,  //低模
        RESOURCE_TYPE_MAP_DATA_SOURCE = 7, //二维显示地图数据源，算法提供
        RESOURCE_TYPE_TWOD_DISPLAYM_MAP = 8,  //二维显示地图
        RESOURCE_TYPE_THREED_PATH_MAP = 9,    // 3D路径地图(导航地图)
        RESOURCE_TYPE_TWOD_PATH_MAP =10, //2D路径地图
        RESOURCE_TYPE_POSE_SERIES = 11,   //动线pose序列   
        RESOURCE_TYPE_IMAGE_SERIES = 12   //动线图片序列
    }

    /// <summary>
    /// 引擎类型
    /// </summary>
    public enum EngineType
    {
        ENGINE_TYPE_ALL =0, //全部支持
        ENGINE_TYPE_SDK = 1, //SDK
        ENGINE_TYPE_UNITY = 2 //Unity
    }

    /// <summary>
    /// 内容类型
    /// </summary>
    public enum ContentType
    {
        CONTENT_TYPE_SCENE = 1, //主事件
        CONTENT_TYPE_EVENT = 2  //子事件
    }

    /// <summary>
    /// 支持平台类型
    /// </summary>
    public enum PlatformType
    {
        PLATFORM_TYPE_ALL = 0, //iOS + Android
        PLATFORM_TYPE_IOS = 1, //iOS
        PLATFORM_TYPE_ANDROID = 2, //android
    }
}
