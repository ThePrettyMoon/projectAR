#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ARWorldEditor
{
    [Serializable]
    public class VirtualPoiMap:EZXRDataProperty
    {
        #region params
        public List<GeoJsonFeature> poiList;
        private List<GeoJsonFeature> backPoiList; //缓存提交数据
        private long contentId;
        private long appId;
        private int engineType;
        private double[] utm_offset;
        private Matrix4x4 t_geojson_feature;
        private Transform rootTrans;
        [SerializeField]
        private Transform defaultTrans;
        private List<ContentsForPoiResponseData> contentsForPoiList;
        private List<POIItem> transList;
        [SerializeField]
        private int currentLevel;
        [SerializeField]
        private int currentLevelIndex;
        [SerializeField]
        private int utmZone;
        [SerializeField]
        private List<int> levelList;
        private JsonSerializerSettings jsonSettings;


        public List<GeoJsonFeature> GetPoiList()
        {
            return poiList;
        }

        public List<GeoJsonFeature> GetBackPoiList()
        {
            return backPoiList;
        }

        public void SetPoiList(List<GeoJsonFeature> list)
        {
            poiList = list;
        }

        public List<ContentsForPoiResponseData> GetContentsForPoiList()
        {
            return contentsForPoiList;
        }

        /// <summary>
        /// 临时使用，当前geo.json未传
        /// V2.0 geo.json会传utm值，直接获取并设置，这里就不再需要
        /// </summary>
        public static int GetUTMZone(long contentId)
        {
            if (!EditorPrefs.HasKey("UTMZone" + contentId))
            {
                int defaultUTMZone = 51;
                EditorPrefs.SetInt("UTMZone" + contentId, defaultUTMZone);
                return defaultUTMZone;
            }
            //Debug.LogError("EditorPrefs.HasKey:" + "UTMZone" + contentId + ":" + EditorPrefs.GetInt("UTMZone" + contentId));
            return EditorPrefs.GetInt("UTMZone" + contentId);
        }
        /// <summary>
        /// 保存UTMZone
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="value"></param>
        public static void SaveUTMZone(long contentId,int value)
        {
            EditorPrefs.SetInt("UTMZone" + contentId, value);
        }

        #endregion

        #region custom functions
        public VirtualPoiMap()
        {
            this.poiList = new List<GeoJsonFeature>();
            this.backPoiList = new List<GeoJsonFeature>();
            this.transList = new List<POIItem>();
       
            BackUpData();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_appId"></param>
        /// <param name="_contentId"></param>
        /// <param name="_utm_offset"></param>
        /// <param name="_mat"></param>
        /// <param name="root"></param>
        public void Init(long _appId,long _contentId,int _engineType,double[] _utm_offset,Matrix4x4 _mat,Transform _rootTrans,Transform _defaultTrans
            ,List<int> levles)
        {
            this.appId = _appId;
            this.contentId = _contentId;
            this.engineType = _engineType;
            this.utm_offset = _utm_offset;
            this.t_geojson_feature = _mat;
            this.defaultTrans = _defaultTrans;
            this.levelList = levles;

            LoadResources(_rootTrans);
            GetPoiData();
            GetContentsForPOI();

            this.currentLevelIndex = 0;
            if(this.levelList!=null && this.currentLevelIndex < this.levelList.Count)
            {
                this.currentLevel = this.levelList[currentLevelIndex];
            }

            utmZone = GetUTMZone(contentId);

            _defaultTrans.Find("DefaultPoiLabelPrefab/LabelIcon/virtualIcon").gameObject.SetActive(true);
            _defaultTrans.Find("DefaultPoiLabelPrefab/LabelIcon/physicIcon").gameObject.SetActive(false);

            jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
        }



        /// <summary>
        /// 点击取消按钮
        /// </summary>
        public void CancelUploadPoiData()
        {
            RecoverData();
        }

        /// <summary>
        /// 点击上传按钮
        /// </summary>
        public void UploadPoiData()
        {
            SaveUTMZone(contentId, utmZone);
            //添加唯一id
            for (int i = 0; i < poiList.Count; i++)
            {
                GeoJsonFeature feature = poiList[i];
                if (feature.geometry == null)
                {
                    GeoJsonGeometry geometry = new GeoJsonGeometry();
                    geometry.uposition = GeoVector3.zero;
                    geometry.urotation = GeoVector3.zero;
                    geometry.uscale = GeoVector3.one;
                    feature.geometry = geometry;
                }
                else
                {
                    feature.geometry.uposition = GeoVector3.GetVector3(feature.geometry.position);
                    feature.geometry.urotation = GeoVector3.GetVector3(feature.geometry.rotation);
                    feature.geometry.uscale = GeoVector3.GetVector3(feature.geometry.scale);
                }
                feature.type = "Feature";
                feature.geometry.type = "Point";

                if (feature.geometry.coordinates == null || feature.geometry.coordinates.Count == 0)
                {
                    feature.geometry.coordinates = new List<object>(2) { 0, 0 };
                }

                if (feature.properties == null)
                {
                    GeoJsonProperties properties = new GeoJsonProperties();
                    properties.name = "poi";
                    feature.properties = properties;
                }

                feature.properties.id = RandomUtility.Random();
                feature.properties.x_anchor = "yes";
                feature.properties.height = feature.geometry.position.y.ToString();
                feature.properties.direction = "0"; //默认是0
                feature.properties.amenity = "virtualpoint";

                //默认是叠加算法
                // if (string.IsNullOrEmpty(feature.properties.x_content_alg_mode)) feature.properties.x_content_alg_mode = AlgorithmType.unchange.ToString();

                Matrix4x4 unityMat = Matrix4x4.TRS(feature.geometry.position, Quaternion.Euler(feature.geometry.rotation), feature.geometry.scale);
         
                CoordinateTransformUtility.SetUTMZone(utmZone);
                Matrix4x4 cvMat = CoordinateTransformUtility.ConverUnity3DPoseToCV3DPose(unityMat);
                double[] localPose = CoordinateTransformUtility.ConvertCV3DPoseTo2DMapPoseByTransform(t_geojson_feature, cvMat, utm_offset[0], utm_offset[1]);
                feature.geometry.coordinates[0] = (object)localPose[0];
                feature.geometry.coordinates[1] = (object)localPose[1];

                if (feature.prefab != null) feature.prefabAssetPath = AssetDatabase.GetAssetPath(feature.prefab);

                //检查数据
                if (StringUtility.ParseInt(feature.properties.x_preview_content_radius) > StringUtility.ParseInt(feature.properties.x_name_radius))
                {
                    EditorUtility.DisplayDialog("warning",  feature.properties.name + " preview radius must less than or equal to information distance", "ok");
                    feature.properties.x_preview_content_radius = feature.properties.x_name_radius;
                    return;
                }

                if (StringUtility.ParseInt(feature.properties.x_content_radius) > StringUtility.ParseInt(feature.properties.x_preview_content_radius))
                {
                    EditorUtility.DisplayDialog("warning", feature.properties.name + " experience radius must less than or equal to preview distance", "ok");
                    feature.properties.x_content_radius = feature.properties.x_preview_content_radius;
                    return;
                }
            }

            string poiData = ARWorldEditor.JsonUtil.Serialize(poiList,Formatting.Indented,jsonSettings);
            //Debug.Log("upload poi data " + poiData);

            ARWorldEditor.NetDataFetchManager.Instance.UploadPOIData(contentId, poiData, new OnOasisNetworkDataFetchCallback<UploadPOIDataResponseData>(
                OnUploadPoiSuccess, OnUploadPoiFail));
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="root"></param>
        private void LoadResources(Transform root)
        {
            rootTrans = root.Find("virtualpoiroot");
            if (rootTrans == null) rootTrans = new GameObject().transform;

            rootTrans.name = "virtualpoiroot";
            rootTrans.parent = root;
            rootTrans.localPosition = Vector3.zero;

            poiList = new List<GeoJsonFeature>();
            BackUpData();
        }

        private void OnUploadPoiSuccess(UploadPOIDataResponseData response)
        {
            BackUpData();
        }

        private void OnUploadPoiFail(string code,string msg)
        {
        
        }

        /// <summary>
        /// 数据备份
        /// </summary>
        private void BackUpData()
        {
            if (backPoiList != null) backPoiList.Clear();
            if (poiList == null || poiList.Count == 0)
            {
                return;
            }
            if (backPoiList == null) backPoiList = new List<GeoJsonFeature>();
            for (int i = 0; i < poiList.Count; i++)
            {
                GeoJsonFeature backFeature = poiList[i].Clone();
                backPoiList.Add(backFeature);
            }
        }

        private void RecoverData()
        {
            if (poiList != null) poiList.Clear();
            if (backPoiList == null || backPoiList.Count == 0)
            {
                return;
            }

            if (poiList == null) poiList = new List<GeoJsonFeature>();
            for(int i = 0; i < backPoiList.Count; i++)
            {
                GeoJsonFeature feature = backPoiList[i].Clone();
                poiList.Add(feature);
            }

        }


        /// <summary>
        /// 请求poi列表
        /// </summary>
        private void GetPoiData()
        {
            ARWorldEditor.NetDataFetchManager.Instance.GetPOIData(this.contentId,new OnOasisNetworkDataFetchCallback<GetPoiDataResponseData>(OnGetPoiDataSuccess,OnGetPoiDataError));
        }

        /// <summary>
        /// 请求可编辑poi场景列表
        /// </summary>
        private void GetContentsForPOI()
        {
            ARWorldEditor.NetDataFetchManager.Instance.GetContentsForPOI(this.contentId, this.engineType, new OnOasisNetworkDataFetchCallback<GetContentsForPoiResponseData>(
                GetContentsForPOISuccess, GetContentsForPOIFail));
        }

        /// <summary>
        /// 请求poi列表成功
        /// </summary>
        /// <param name="response"></param>
        private void GetContentsForPOISuccess(GetContentsForPoiResponseData response)
        {
            contentsForPoiList = response.result;
        }

        /// <summary>
        ///请求poi列表失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        private void GetContentsForPOIFail(string code ,string msg)
        {

        }

        /// <summary>
        /// 获取poi列表成功
        /// </summary>
        /// <param name="response"></param>
        private void OnGetPoiDataSuccess(GetPoiDataResponseData response)
        {
            poiList = JsonUtil.Deserialization<List<GeoJsonFeature>>(response.result.poiData);

            if (poiList != null && poiList.Count > 0)
            {
                for (int i = 0; i < poiList.Count; i++)
                {
                    var feature = poiList[i];
                    if (feature == null) continue;
                    if (string.IsNullOrEmpty(feature.prefabAssetPath))
                    {
                        feature.prefab = defaultTrans;
                    }
                    else
                    {
                        feature.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(feature.prefabAssetPath).transform;
                    }
                    feature.isActive = true;
                    feature.scaleDown = true;
                    if (feature.geometry != null)
                    {
                        feature.geometry.position = feature.geometry.uposition.GetVector3();
                        feature.geometry.rotation = feature.geometry.urotation.GetVector3();
                        feature.geometry.scale = feature.geometry.uscale.GetVector3();
                    }
                }
            }
            else
            {
                poiList = new List<GeoJsonFeature>();
            }

            BackUpData();

            //获取事件后更新一次
            HasChanged = true;
        }

        /// <summary>
        /// 获取poi列表失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        private void OnGetPoiDataError(string code ,string msg)
        {
            Debug.Log("request poi data error code = " + code + " msg = " + msg);
        }

        /// <summary>
        /// 只显示本层的level
        /// </summary>
        public void AddOrUpdatePoiObjects()
        {
            if (poiList == null || poiList.Count == 0)
            {
                if (transList != null)
                {
                    for (int i = 0; i < transList.Count; i++)
                    {
                        if (transList[i].trans != null)
                        {
                            GameObject.DestroyImmediate(transList[i].trans.gameObject);
                        }
                    }
                    transList.Clear();
                }   
                return;
            }

            List<POIItem> list = new List<POIItem>();
            if (transList != null)
            {
                for (int i = 0; i < transList.Count; i++)
                {
                    var item = transList[i];
                    var feature = poiList.Find(p => p.properties.id == item.id);
                    if (feature != null)
                    {
                        if(item.trans!=null) item.trans.gameObject.SetActive(false);
                    }
                    else
                    {
                        list.Add(item);
                        if (item.trans != null) GameObject.DestroyImmediate(item.trans.gameObject);
                    }
                }
            }
            if (list != null) list.ForEach(p => transList.Remove(p));
            list.Clear();


            int level = currentLevel;

            for (int i = 0; i < poiList.Count; i++)
            {
                var poiInfo = poiList[i];
                if (poiInfo == null) continue;

                //不是本层不需要绘制
                if (poiInfo.properties.level != level.ToString()) continue;

                //如果没有选中，不需要绘制
                if (!poiInfo.isActive) continue;

                double[] geoPosition = null;
                if (poiInfo.geometry != null && poiInfo.geometry.coordinates != null && poiInfo.geometry.coordinates.Count > 0)
                {
                    var firstElement = poiInfo.geometry.coordinates[0];
                    if (firstElement.GetType() == typeof(Newtonsoft.Json.Linq.JArray)) continue; //多个坐标数组不考虑

                    double longitude = (double)poiInfo.geometry.coordinates[0];
                    double latitude = (double)poiInfo.geometry.coordinates[1];

                    geoPosition = new double[] { longitude, latitude, 0 };
                }

                float height = StringUtility.ParseFloat(poiInfo.properties.height);
                POIItem poiItem = null;
                if (transList != null) poiItem = transList.Find(p => p.id == poiInfo.properties.id);
                Transform trans = poiItem != null ? poiItem.trans : null;

                if (trans == null)
                {
                    if (poiInfo.prefab == null)
                    {
                        trans = GameObject.Instantiate(defaultTrans).transform;
                    }
                    else
                    {
                        trans = GameObject.Instantiate(poiInfo.prefab).transform;
                    }
                }
                else
                {
                    if (poiInfo.replacePrefab)
                    {
                        GameObject.DestroyImmediate(trans.gameObject);
                        trans = null;

                        if (poiInfo.prefab == null)
                        {
                            trans = GameObject.Instantiate(defaultTrans).transform;
                        }
                        else
                        {
                            trans = GameObject.Instantiate(poiInfo.prefab.gameObject).transform;
                        }
                        poiInfo.replacePrefab = false;
                    }
                }

                poiInfo.prefabAssetPath = AssetDatabase.GetAssetPath(trans);

                trans.parent = rootTrans;

                trans.gameObject.SetActive(true);
                trans.position = poiInfo.geometry.position;
                Transform textTrans = trans.Find("DefaultPoiLabelPrefab/TextBackground/Text");
                if (textTrans != null) textTrans.GetComponent<Text>().text = poiInfo.properties.name;

                if (poiItem == null)
                {
                    poiItem = new POIItem(poiInfo.properties.id, trans);
                    transList.Add(poiItem);
                }
                else poiItem.trans = trans;
            }
        }
        #endregion

        public void Close()
        {
            if (poiList != null) poiList.Clear();
            if (backPoiList != null) backPoiList.Clear();
            if (rootTrans != null) GameObject.DestroyImmediate(rootTrans.gameObject);
            if (transList != null) transList.Clear();
        }
    }
    public class POIItem
    {
        public string id;
        public Transform trans;

        public POIItem(string _id,Transform _trans)
        {
            this.id = _id;
            this.trans = _trans;
        }
    }
}
#endif