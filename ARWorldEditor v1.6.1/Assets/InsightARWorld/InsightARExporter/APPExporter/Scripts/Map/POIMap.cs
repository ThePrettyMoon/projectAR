#if UNITY_EDITOR
using System.Collections.Generic;
using ARWorldEditor;
using UnityEditor;
using UnityEngine;

namespace ARWorldEditor
{
    [ExecuteInEditMode]
    public class POIMap : MonoBehaviour
    {
        #region params
        [SerializeField]
        public RealityPoiMap realityPoiMap = new RealityPoiMap();
        [SerializeField]
        public VirtualPoiMap virtualPoiMap = new VirtualPoiMap();
        [SerializeField]
        public List<GeoJsonFeature> virtualPoiList;
        [SerializeField]
        public List<GeoJsonFeature> backVirtualPoiList;
        [SerializeField]
        public List<GeoJsonFeature> realityPoiList;
        private long appId;
        private long contentId;
        private int engineType;
        [SerializeField]
        public long mapId;
        [SerializeField]
        public List<int> levelList;
        [SerializeField]
        public List<ContentsForPoiResponseData> contentForPoiList;

        private double[] utm_offset;
        private Matrix4x4 t_geojson_feature;
        private Transform rootTrans;
        public Transform defaultPoiTrans;

        #endregion

        #region custom functions
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(long _appId, long _contentId, long _mapId, int _engineType, List<double> origin, List<double> matrix, Transform _trans)
        {
            this.appId = _appId;
            this.contentId = _contentId;
            this.mapId = _mapId;
            this.rootTrans = _trans;
            this.engineType = _engineType;

            CoordinateTransformUtility.SetUTMZone(VirtualPoiMap.GetUTMZone(contentId));

            ParseOriginMatrix(origin, matrix);
            InitRealityMap();
            InitVirtualMap();
            LoadResources();
            // EditorApplication.update += UpdateMap;
        }

        private void UpdateMap()
        {
            //UpdateRealityMap(null, null);
            //UpdateVirtualMap(null, null);
        }


        private void OnDestroy()
        {
            Close();
        }

        /// <summary>
        /// 解析矩阵和零点
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="matrix"></param>
        private void ParseOriginMatrix(List<double> origin, List<double> matrix)
        {
            if (origin == null || matrix == null) { 
                Debug.LogError("请先配置地图变换矩阵");
                return;
            }

            utm_offset = new double[2] { origin[0], origin[1] };
            Matrix4x4 mat = new Matrix4x4();
            for (int i = 0; i < matrix.Count; i++)
            {
                mat[i] = (float)matrix[i];
            }
            t_geojson_feature = Matrix4x4.Transpose(mat);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        private void LoadResources()
        {
        }

        /// <summary>
        /// 初始化reality map
        /// </summary>
        public void InitRealityMap()
        {
            defaultPoiTrans = AssetDatabase.LoadAssetAtPath<GameObject>(ConfigGlobal.PREFAB_DIRECTORY + "/" + ConfigGlobal.POI_PREFAB).transform;
            realityPoiMap.Init(contentId, mapId, utm_offset, t_geojson_feature, rootTrans, defaultPoiTrans);

            levelList = realityPoiMap.GetLevelList;
            realityPoiList = realityPoiMap.poiList;
       
            realityPoiMap.PropertyHasChanged += UpdateRealityMap;

            UpdateRealityMap(null,null);
            UpdateHeightAboveGround(RealityPoiMap.GetHeightAboveGround(contentId));
        }

        /// <summary>
        ///  update reality map
        /// </summary>
        public void UpdateRealityMap(object sender, System.EventArgs eventArgs)
        {
            realityPoiMap.SetPoiList(realityPoiList);
            realityPoiMap.AddOrUpdatePoiObjects();
        }

        /// <summary>
        /// 初始化虚拟poi
        /// </summary>
        private void InitVirtualMap()
        {
            // virtualPoiMap = new VirtualPoiMap();
            virtualPoiMap.Init(appId, contentId, engineType, utm_offset, t_geojson_feature, rootTrans, defaultPoiTrans
                , levelList);
            virtualPoiList = virtualPoiMap.GetPoiList();
            backVirtualPoiList = virtualPoiMap.GetBackPoiList();

            virtualPoiMap.PropertyHasChanged += UpdateVirtualMap;

            UpdateVirtualMap(null, null);
        }

        /// <summary>
        /// 更新虚拟POI
        /// </summary>
        public void UpdateVirtualMap(object sender, System.EventArgs eventArgs)
        {
            virtualPoiList = virtualPoiMap.GetPoiList();
            backVirtualPoiList = virtualPoiMap.GetBackPoiList();
            contentForPoiList = virtualPoiMap.GetContentsForPoiList();
            virtualPoiMap.AddOrUpdatePoiObjects();
        }

        /// <summary>
        /// 取消上传
        /// </summary>
        public void OnClickCancelVirtualPoiHandler()
        {
            if (virtualPoiMap != null) virtualPoiMap.CancelUploadPoiData(); 
        }

        //上传按钮
        public void OnClickUploadVirtualPoiHandler()
        {
            if (virtualPoiMap != null) virtualPoiMap.UploadPoiData();
        }

        public void UpdateHeightAboveGround(float currentValue)
        {
            if (currentValue != RealityPoiMap.GetHeightAboveGround(contentId))
            {
                RealityPoiMap.SaveHeightAboveGround(contentId, currentValue);
            }

            if (rootTrans == null) return;

            var heightAboveGroundRoot = rootTrans.Find("heightabovegroundroot");
            if (heightAboveGroundRoot == null)
            {
                heightAboveGroundRoot = new GameObject("heightabovegroundroot").transform;
                heightAboveGroundRoot.parent = rootTrans;
            }
            heightAboveGroundRoot.position = Vector3.up * currentValue;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (realityPoiMap != null) realityPoiMap.Close();
            if (virtualPoiMap != null) virtualPoiMap.Close();
        }
        #endregion
    }
}
#endif
