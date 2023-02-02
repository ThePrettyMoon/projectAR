#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using ARWorldEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ARWorldEditor
{
    [Serializable]
    public class RealityPoiMap:EZXRDataProperty
    {
        #region params
        //存储楼层信息
        [SerializeField]
        private List<int> levelList;
        //只读取type为POI的数据
        public List<GeoJsonFeature> poiList;
        [SerializeField]
        private long cotentId;
        private long mapId;
        private string mapDirectory;
        private double[] utm_offset;
        private Transform rootTrans;
        private Transform defaultTrans;
        private Matrix4x4 t_geojson_feature;
        private List<Transform> transList;
        [SerializeField]
        private int currentLevel;
        [SerializeField]
        private int currentLevelIndex;
        [SerializeField]
        private float heightAboveGround;

        public static float GetHeightAboveGround(long contentId)
        {
            if (!EditorPrefs.HasKey("HeightAboveGround" + contentId))
            {
                float HeightAboveGround = 1.5f;
                EditorPrefs.SetFloat("HeightAboveGround" + contentId, HeightAboveGround);
                return HeightAboveGround;
            }
            //Debug.LogError("EditorPrefs.HasKey:" + "UTMZone" + contentId + ":" + EditorPrefs.GetInt("UTMZone" + contentId));
            return EditorPrefs.GetFloat("HeightAboveGround" + contentId);
        }
        public static void SaveHeightAboveGround(long contentId, float value)
        {
            var newValue = Math.Round((double)value, 2);
            EditorPrefs.SetFloat("HeightAboveGround" + contentId, (float)newValue);
        }

        public List<GeoJsonFeature> GetPoiList()
        {
            return poiList;
        }

        public void SetPoiList(List<GeoJsonFeature> list)
        {
            poiList = list;
        }

        /// <summary>
        /// 返回楼层信息
        /// </summary>
        public List<int> GetLevelList
        {
            get
            {
                return levelList;
            }
        }
        #endregion

        #region custom functions
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_mapId"></param>
        public void Init(long _contentId, long _mapId,double[] _utmoffset,Matrix4x4 _mat,Transform _rootTrans,Transform _defaultTrans)
        {
            this.cotentId = _contentId;
            this.mapId = _mapId;
            this.poiList = new List<GeoJsonFeature>();
            this.mapDirectory = Path.Combine(ConfigGlobal.MAP_PATH, this.mapId.ToString(), ((int)MapResourcesType.RESOURCE_TYPE_TWOD_DISPLAYM_MAP).ToString());
            this.utm_offset = _utmoffset;
            this.t_geojson_feature = _mat;
            this.defaultTrans = _defaultTrans;
            this.levelList = new List<int>();
            this.transList = new List<Transform>();

            LoadResources(_rootTrans);
            ParseMapPoiInfos();

            this.currentLevelIndex = 0;
            if (this.levelList != null && this.currentLevelIndex < this.levelList.Count)
            {
                this.currentLevel = this.levelList[currentLevelIndex];
            }
            heightAboveGround = GetHeightAboveGround(this.cotentId);

            _defaultTrans.Find("DefaultPoiLabelPrefab/LabelIcon/virtualIcon").gameObject.SetActive(false);
            _defaultTrans.Find("DefaultPoiLabelPrefab/LabelIcon/physicIcon").gameObject.SetActive(true);
        }

        private void LoadResources(Transform root)
        {
            rootTrans = root.Find("realitypoiroot");
            if(rootTrans==null) rootTrans = new GameObject().transform;
            rootTrans.name = "realitypoiroot";
            rootTrans.parent = root;
            rootTrans.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 只显示本层的level
        /// </summary>
        public void AddOrUpdatePoiObjects()
        {
            int level = currentLevel;

            if (transList != null)
            {
                for (int i = 0; i < transList.Count; i++)
                {
                    //  Debug.Log("destroy poi " + rootTrans.GetChild(i).name);
                    //GameObject.DestroyImmediate(rootTrans.GetChild(i).gameObject);
                    //rootTrans.GetChild(i).gameObject.SetActive(false);
                    // rootTrans.GetChild(i).gameObject.SetActive(false);
                    //GameObject.Destroy(rootTrans.GetChild(i).gameObject);
                    GameObject.DestroyImmediate(transList[i].gameObject);
                }
                transList.Clear();
            }

            if (poiList == null || poiList.Count == 0) return;
            for (int i = 0; i < poiList.Count; i++)
            {
                var poiInfo = poiList[i];
                if (poiInfo == null) continue;
                if (poiInfo.geometry == null || poiInfo.geometry.coordinates == null
                                             || poiInfo.geometry.coordinates.Count == 0) continue;

                //不是本层不需要绘制
                if (poiInfo.properties.level != level.ToString()) continue;

                //如果没有选中，不需要绘制
                if (!poiInfo.isActive) continue;

                var firstElement = poiInfo.geometry.coordinates[0];
                if (firstElement.GetType() == typeof(Newtonsoft.Json.Linq.JArray)) continue; //多个坐标数组不考虑

                double longitude = (double)poiInfo.geometry.coordinates[0];
                double latitude = (double)poiInfo.geometry.coordinates[1];

                double[] geoPosition = new double[] { longitude, latitude, 0 };
                float height = StringUtility.ParseFloat(poiInfo.properties.height);
                Transform trans = null;
                if (i < rootTrans.childCount)
                {
                    trans = rootTrans.GetChild(i);
                }
                else
                {
                    if (poiInfo.prefab != null)
                    {
                        trans = GameObject.Instantiate(poiInfo.prefab).transform;
                    }
                    else
                    {
                        trans = GameObject.Instantiate(defaultTrans).transform;
                    }
                    trans.parent = rootTrans;
                }
                trans.gameObject.SetActive(true);
                double[] cvPose = CoordinateTransformUtility.Convert2DMapPoseTo3DPoseByTransform(t_geojson_feature, geoPosition, utm_offset[0], utm_offset[1]);
                Matrix4x4 cvMat = Matrix4x4.TRS(new Vector3((float)cvPose[0], (float)cvPose[1], (float)cvPose[2]), Quaternion.identity, Vector3.zero);
                Matrix4x4 u3dMat = CoordinateTransformUtility.ConvertCV3DPoseToUnity3DPose(cvMat);
                Vector3 worldPosition = new Vector3(u3dMat.m03, height, u3dMat.m23);
                trans.position = worldPosition;
                Transform textTrans = trans.Find("DefaultPoiLabelPrefab/TextBackground/Text");
                if (textTrans != null) textTrans.GetComponent<Text>().text = poiInfo.properties.name;
                transList.Add(trans);
            }
        }

        /// <summary>
        /// 解析poiList
        /// </summary>
        private void ParseMapPoiInfos()
        {  
            string levelsPath = Path.Combine(mapDirectory, ConfigGlobal.LEVELS_NAME);
            //Debug.Log("levels Path " + levelsPath);
            MapLevels mapLevels = new MapLevels();

            if (File.Exists(levelsPath))
            {
                mapLevels = JsonUtil.Deserialization<MapLevels>(File.ReadAllText(levelsPath));
                if (mapLevels != null)
                {
                    if (mapLevels.levels != null && mapLevels.levels.Count > 0)
                    {
                        for (int i = 0; i < mapLevels.levels.Count; i++)
                        {
                            levelList.Add(mapLevels.levels[i].reference);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("请配置2D地图文件，否则无法使用POI功能");
                return;
            }

            if (mapLevels != null && mapLevels.levels != null && mapLevels.levels.Count > 0)
            {
                for (int i = 0; i < mapLevels.levels.Count; i++)
                {
                    LevelInfo levelInfo = mapLevels.levels[i];
                    if (levelInfo == null) continue;
                    string levelPath = Path.Combine(mapDirectory, levelInfo.reference.ToString(),"geojson.json");

                    if (!File.Exists(levelPath) || string.IsNullOrEmpty(levelPath)) continue;
                    GeoJsonMapData geoJsonMapData = JsonUtil.Deserialization<GeoJsonMapData>(File.ReadAllText(levelPath));
                    if(geoJsonMapData !=null && geoJsonMapData.features !=null && geoJsonMapData.features.Count > 0)
                    {
                        for (int j = 0; j < geoJsonMapData.features.Count; j++)
                        {
                            GeoJsonFeature geoJsonFeature = geoJsonMapData.features[j];
                            if (geoJsonFeature == null || geoJsonFeature.properties == null) continue;
                            if (geoJsonFeature.properties.type != GeoJsonPropertyType.POI.ToString()) continue;
                            // add default trans

                            string prefabAssetPath = PlayerPrefs.GetString(cotentId + geoJsonFeature.properties.id);
                            if (string.IsNullOrEmpty(prefabAssetPath))
                            {
                                geoJsonFeature.prefab = defaultTrans;
                            }
                            else
                            {
                                geoJsonFeature.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabAssetPath).transform;
                            }
                            // geoJsonFeature.prefab = defaultTrans;
                            geoJsonFeature.isActive = true;
                            geoJsonFeature.scaleDown = true;
                            poiList.Add(geoJsonFeature);
                        }
                    }
                }
            }
        }

        public void Close()
        {
            if (poiList != null) poiList.Clear();
            if (rootTrans != null) GameObject.DestroyImmediate(rootTrans.gameObject);
            if (transList != null) transList.Clear();
        }
        #endregion


    }
}
#endif
