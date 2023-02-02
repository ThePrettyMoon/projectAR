using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Android;
using AOT;
using EZXR.Glass.Common;

namespace EZXR.Glass.SixDof
{
    [ScriptExecutionOrder(-19)]
    public class SessionManager : MonoBehaviour
    {
        #region 单例
        private static SessionManager instance;
        public static SessionManager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region params
        //private bool m_isRequestCamera;
        public ARProjectConfig config;

        private RenderMode m_renderMode = RenderMode.Bino;
        public RenderMode RenderMode
        {
            get
            {
                return m_renderMode;
            }
        }

        public bool IsInited
        {
            get
            {
                return NativeTracking.GetIsARSessionInited();
            }
        }

        #endregion

        #region unity functions
        private void Awake()
        {
            //single instance guarantee
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            else
            {
                instance = this;
            }

            //cross scene available
            DontDestroyOnLoad(gameObject);

#if EZXRCS
            if (!Application.isEditor)
            {
                NativeTracking.StartArServer();
            }
#endif

            ARConfig.DefaultConfig.HandsFindingMode = config.handTracking ? HandsFindingMode.Enable : HandsFindingMode.Disable;
            ARConfig.DefaultConfig.PlaneFindingMode = config.planeDetection ? PlaneFindingMode.Enable : PlaneFindingMode.Disable;
            ARConfig.DefaultConfig.MarkerFindingMode = config.imageDetection ? MarkerFindingMode.Enable : MarkerFindingMode.Disable;
            ARConfig.DefaultConfig.MeshFindingMode = config.spatialMesh ? MeshFindingMode.Enable : MeshFindingMode.Disable;
            ARConfig.DefaultConfig.RelocalizationMode = config.spatialTrackingMultiPlayer ? RelocalizationMode.PrebuiltMap : RelocalizationMode.Disable;

            ARFrame.OnAwake();

            //config = ARConfig.DefaultConfig;
            ////TODO:only for Test
            //config.HandsFindingMode = HandsFindingMode.Disable;
            //config.MarkerFindingMode = MarkerFindingMode.Enable;
        }

        private void Start()
        {
            Debug.Log("=============Unity Log===============   SessionManager -- Start");
            if (CheckAndRequestPermissions())
            {
                StartSession();
            }
        }

        private void Update()
        {
            // NativeTracking.GetIsARSessionInited() 必须调用
            if (!IsInited)
            {
                Debug.Log("=============Unity Log===============   SessionManager -- Update NativeTracking GetIsARSessionInited: not initilized");
                return;
            }

            //Debug.Log("xnh M2P: sessionmanager Update, BEFORE get propagate pose, systime: " + NativeTracking.GetSystemTime() / 1e9);
            // update vio
            ARFrame.OnUpdate();
            //Debug.Log("xnh M2P: sessionmanager Update, AFTER get propagate pose, systime: " + NativeTracking.GetSystemTime() / 1e9 + " VIORESULT: " + ARFrame.OriginVIOResult.timestamp);

            //update gesture
            //ARGesture.OnFixedUpdate();
        }


        private void OnApplicationPause(bool pause)
        {
            Debug.Log("=============Unity Log===============   SessionManager -- OnApplicationPause " + pause);

            if (pause)
            {
                //DisableSession();
            }
            else
            {
                if (CheckAndRequestPermissions())
                {
                    StartSession();
                }
                //ResumeSession();
            }
        }

        private void OnDisable()
        {
            Debug.Log("=============Unity Log===============   SessionManager -- OnDisable");
            DisableSession();
        }

        private void OnDestroy()
        {
            Debug.Log("=============Unity Log===============   SessionManager -- OnDestroy");
            DestroySession();
        }
        #endregion

        #region custom functions
        private bool CheckAndRequestPermissions()
        {
            Debug.Log("=============Unity Log===============   SessionManager -- RequestCamera");
            bool writePermissionGranted = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            bool cameraPermissionGranted = Permission.HasUserAuthorizedPermission(Permission.Camera);
            if (!writePermissionGranted)
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                return false;
            }
            if (!cameraPermissionGranted)
            {
                Permission.RequestUserPermission(Permission.Camera);
                return false;
            }
            if (!GlassDevicePermissionHelper.IsGlassDevicePermissionAllGranted())
            {
                GlassDevicePermissionHelper.RequestGlassDevicePermission(OnGlassPermssionChange);
                return false;
            }
            return true;
        }

        [MonoPInvokeCallback(typeof(GlassDevicePermissionHelper.GlassDevicePermissionListener))]
        private void OnGlassPermssionChange(int usbVenderId, int usbProductId, bool isGranted)
        {
            Debug.Log("=============Unity Log=============== SessionManager -- OnGlassPermssionChange " + usbVenderId + "," + usbProductId + "," + isGranted);
            if (GlassDevicePermissionHelper.IsGlassDevicePermissionAllGranted())
            {
                StartSession();
            }
            else {
                CheckAndRequestPermissions();
            }
        }

        private void StartSession()
        {
            Debug.Log("=============Unity Log=============== SessionManager -- StartSession");
            if (!IsInited)
            {
                ARConfig config = ARConfig.DefaultConfig;

                ///// @miaozhuang
                //config.unsetMTPMode_warping_1();

                NativeTracking.StartARSession(config);
            }
        }

        private void DisableSession()
        {
            Debug.Log("=============Unity Log=============== SessionManager -- DisableSession");
            if (!IsInited) return;
#if CS
            NativeTracking.pauseMtp();
#endif
        }

        private void ResumeSession()
        {
            Debug.Log("=============Unity Log=============== SessionManager -- ResumeSession");
            if (!IsInited) return;

#if EZXRCS
            NativeTracking.resumeMtp();
#endif
        }

        private void DestroySession()
        {
            Debug.Log("=============Unity Log=============== SessionManager -- DestroySession");
            if (!NativeTracking.GetIsARSessionInited()) return;

#if EZXRCS
            NativeTracking.StopARSession();
#endif
        }
#endregion
    }
}
