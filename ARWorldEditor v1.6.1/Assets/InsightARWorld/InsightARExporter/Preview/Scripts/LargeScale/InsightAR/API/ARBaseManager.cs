using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace InsightAR.Internal
{
    public class ARBaseManager : BaseBehaviour
    {
        #region params
        private const string TAG = "ARBaseManager";
        [SerializeField]
        protected Camera m_ARCamera;

        protected InsightARInterface m_ARInterface;

        protected bool _isRungning = false;

        private InsightARSettings _ARSetting;

        //云端重定位
        public InsightARCloudLocation cloudLocation;

        //attach 算法
        protected InsightARAttach aRAttach;

        //private Coroutine startNavCoroutine;

        public bool isRunning()
        {
            return _isRungning;
        }

        public InsightARAttach GetARAttach()
        {
            return aRAttach;
        }

        public InsightARInterface GetARInterface()
        {
            return m_ARInterface;
        }

        public string GetResultString(int idx)
        {
#if UNITY_EDITOR
            if (m_ARInterface == null)
            {
                return "";
            }
#endif
            return m_ARInterface.GetResultString(idx);
        }


        /// <summary>
        /// 暴露当前算法状态
        /// </summary>
        public InsightARState ARState
        {
            get
            {
#if UNITY_EDITOR
                if (m_ARInterface == null)
                {
                    return InsightARState.Tracking;
                }
#endif
                return m_ARInterface.ARState;
            }
        }

        public int ARReason
        {
            get
            {
                return m_ARInterface.ARReason;
            }
        }

        public InsightARResult GetTrackingResult()
        {
            return m_ARInterface.GetTrackingResult();
        }

        //cloud status
        public int CloudLocStatus
        {
            get {
#if UNITY_EDITOR
                if (cloudLocation==null)
                {
                    return 1;
                }
#endif
                return cloudLocation.CloudLocAlgCode;
            }
        }

        public string CloudLocReason
        {
            get
            {
#if UNITY_EDITOR
                if (cloudLocation == null)
                {
                    return "";
                }
#endif
                return cloudLocation?.CloudLocAlgReason;
            }
        }

        public long CloudLocTotalCount
        {
            get
            {
#if UNITY_EDITOR
                if (cloudLocation == null)
                {
                    return 0;
                }
#endif
                return cloudLocation.CloudLocTotalCount;
            }
        }

        /// <summary>
        ///  返回markers
        /// </summary>
        /// <returns></returns>
        public List<InsightARMarkerAnchor> GetInsightARMarkers()
        {
            return m_ARInterface.GetMarkerList();
        }

        public Camera GetCamera()
        {
            return m_ARCamera;
        }

        #endregion

        #region unity_functions
        public void Update()
        {
            if (!_isRungning)
                return;


            m_ARInterface.Update();
#if UNITY_EDITOR
            if (ARState == InsightARState.Tracking) {
                //maskCameraUpdate.OnMaskUpdateHandler(new InsightARMaskResult());
                //bgCameraUpdate.OnBgCameraUpdate(new Material(Shader.Find("Unlit/Texture")));
                //maskCameraUpdate.OnMaskCameraUpdateHandler(Camera.main);
                //bgCameraUpdate.OnBgTransformUpdate(Camera.main);
            }
#endif

            //导航定位update
            LocationInterface.UpdateLocation();

        }

        public void LateUpdate() {
            if (!_isRungning)
                return;

            m_ARInterface.LateUpdate();
        }
        #endregion

        #region custom_functions
        /// <summary>
        /// 清理缓存数据
        /// </summary>
        public void ClearData()
        {
            m_ARInterface.ClearData();
        }

        public virtual void InitAR(string configDir = "")
        {
            if (m_ARCamera == null)
                m_ARCamera = GetComponent<Camera>();

            // Fallback to main camera
            if (m_ARCamera == null)
                m_ARCamera = Camera.main;

            if (m_ARInterface == null)
                m_ARInterface = InsightARInterface.GetInstance();

            DoStartAR();
        }


        public virtual void StopAR()
        {
            if (!_isRungning)
            {
                return;
            }

            SceneController.Instance.loadSceneSuccess -= SceneLoadSuccessHandler;

            //cloud location close
            cloudLocation.RemoverListener();

            //导航定位 close
            LocationInterface.EndLocation();
            NavigationInterface.RemoveNaviListeners();

            m_ARInterface.StopAR();

            _isRungning = false;

            ////关闭加载导航协程
            //if (startNavCoroutine != null)
            //{
            //    StopCoroutine(startNavCoroutine);
            //}
        }

        public virtual void ResetAR()
        {
            m_ARInterface.ResetAR(_ARSetting.configPath);
        }

        public virtual void ResetAR(string path)
        {
            _ARSetting.configPath = path;
            ResetAR();
        }

        public void SetConfig(string _configPath, string _appKey, string _appSecret)
        {
            _ARSetting = new InsightARSettings()
            {
                configPath = _configPath,
                appKey = _appKey,
                appSecret = _appSecret
            };
        }
        public void SetConfig(string _configPath)
        {
            _ARSetting.configPath = _configPath;
        }

        public void SetUpCamera(InsightARCamera arCamera)
        {
            m_ARCamera = arCamera.GetCamera();
            m_ARInterface.SetupCamera(m_ARCamera);
        }

        protected void DoStartAR()
        {
            m_ARInterface.StartAR(_ARSetting);
            m_ARInterface.SetupCamera(m_ARCamera);
            _isRungning = true;

            SceneController.Instance.loadSceneSuccess += SceneLoadSuccessHandler;

            //云端定位
            cloudLocation = new InsightARCloudLocation();
            cloudLocation.AddListener();

            //导航定位
            LocationInterface.EnterLocation();
            NavigationInterface.AddNaviListeners();

            aRAttach = new InsightARAttach();
        }

        public void AdaptScreenOrientation(ScreenOrientation orien)
        {

#if UNITY_EDITOR
            InsightARUIOrientation orientation = orien == ScreenOrientation.Portrait ? InsightARUIOrientation.Portrait : InsightARUIOrientation.LandscapeLeft;
#elif UNITY_ANDROID
            InsightARUIOrientation orientation = orien == ScreenOrientation.Portrait ? InsightARUIOrientation.Portrait : InsightARUIOrientation.LandscapeLeft;
#elif UNITY_IOS
            //iOS UI横屏和安卓刚好相反，iOS device 横屏和安卓相同
            InsightARUIOrientation orientation = orien == ScreenOrientation.Portrait ? InsightARUIOrientation.Portrait : InsightARUIOrientation.LandscapeRight;
#endif
            m_ARInterface.AdaptUIOrientation(orientation);
        }


        #endregion

        public void SceneLoadSuccessHandler() {

            //maskCameraUpdate.AddListener();
    
        }
    }
}
