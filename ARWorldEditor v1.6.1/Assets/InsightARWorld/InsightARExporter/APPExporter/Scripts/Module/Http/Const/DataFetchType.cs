namespace ARWorldEditor
{
    public static class DataFetchType
    {
        //普通事件
      public const  int TYPE_NORMAL_EVENT_PID = 1;

        // sticker
        public const int TYPE_STICKER = 2;

        // 云识别
        public const int TYPE_CLOUD_RECO = 3;

        //当前key下所有的数据
        public const int TYPE_ALL_EVENT = 4;

        //sticker 推荐数据
        public const int TYPE_STICKER_RECOMMEND = 5;

        // 扫一扫versionid 来获取数据
        public const int TYPE_SCAN_VERSION_ID = 6;

        //扫一扫pid来获取数据
        public const int TYPE_SCAN_PID = 7;

        //公共算法包资源
        public const int TYPE_COMMON_ALGO_PACKAGE = 9;

        //大场景2d地图数据
        public const int TYPE_LARGE_SCENE_PLANE_MAP = 10;

        //大场景3d地图数据
        public const int TYPE_LARGE_SCENE_RELIEF_MAP = 11;
    }
}
