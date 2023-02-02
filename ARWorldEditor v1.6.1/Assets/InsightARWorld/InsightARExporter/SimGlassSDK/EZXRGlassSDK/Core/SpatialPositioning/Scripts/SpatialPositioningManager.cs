using EZXR.Glass;
using EZXR.Glass.Common;
using EZXR.Glass.SixDof;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace EZXR.Glass.SpatialPositioning
{
    public enum LocCam
    {
        LocCam_RGB,
        LocCam_FisheyeGray
    }
    public class SpatialPositioningManager : MonoBehaviour
    {
        #region singleton
        private static SpatialPositioningManager _instance;
        public static SpatialPositioningManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 定位资源的URL
        /// </summary>
        public string url = "https://reloc-gw.easexr.com/api/alg/cloud/aw/reloc/proxy?routeApp=parkc&map=arglass";
        /// <summary>
        /// 所有的3D资源都应放在此物体下（可以认作此物体的局部坐标系为世界坐标系）
        /// </summary>
        public GameObject virtualWorld;
        public LocCam locCamType = LocCam.LocCam_RGB;


        public static bool IsLocationSuccess { get { return isLocationSuccess; } }

        private static bool isLocationSuccess;

        protected EzxrCloudLocalization ezxrCloudLocalization = null;
        private bool hasIntricsSet = false;
        private bool hasPoseOffsetSet = false;
        private NormalRGBCameraDevice rgbCameraDevice;
        private EZVIOInputImage locCamImageBuffer;
        private float[] locCamIntriarray = new float[8];
        private OSTMatrices ost_m = new OSTMatrices();
        private GameObject httpManagerObject = null;

        private void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }

            //DontDestroyOnLoad(gameObject);
            if (virtualWorld != null)
                virtualWorld.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnDestroy()
        {

        }

        private void OnEnable()
        {
            Debug.LogWarning("-ar- LocalizeManager OnEnable");
            if (locCamType == LocCam.LocCam_RGB)
            {
                StartCoroutine(startRGBCamera());
            }
            startTrackSession();
        }

        private void OnDisable()
        {
            stopTrackSession();
            if (locCamType == LocCam.LocCam_RGB)
            {
                stopRGBCamera();
            }
        }
        private void Update()
        {
            if (ezxrCloudLocalization != null)
            {
                ezxrCloudLocalization.HandleLocalizerRequest(url);
            }

            UpdateCameraImage();
            RelocByCloudLoc();
        }

        private IEnumerator startRGBCamera()
        {
            yield return new WaitUntil(() => SessionManager.Instance.IsInited);
            hasIntricsSet = false;
            hasPoseOffsetSet = false;
            Debug.LogWarning("-10001-startRgbCamera new rgbCameraDevice Open");
            locCamImageBuffer = new EZVIOInputImage();
            rgbCameraDevice = new NormalRGBCameraDevice();
            rgbCameraDevice.Open();
        }

        private void stopRGBCamera()
        {
            if (rgbCameraDevice != null)
                rgbCameraDevice.Close();
        }

        private void startTrackSession()
        {
            ezxrCloudLocalization = new EzxrCloudLocalization();

            Debug.LogWarning("-10001-startTrackSession ezxrCloudLocalization.Create");

            if (ezxrCloudLocalization != null)
            {
                ezxrCloudLocalization.Create();
            }

            Debug.LogWarning("-10001-startTrackSession end");
            //Invoke("RelocByTrack2d", 2.0f);
        }

        private void stopTrackSession()
        {
            Debug.LogWarning("-10001-stopTrackSession");

            if (ezxrCloudLocalization != null)
            {
                ezxrCloudLocalization.Destroy();
                ezxrCloudLocalization = null;
            }
        }

        private void RelocByCloudLoc()
        {
            if (ezxrCloudLocalization != null)
            {
                Pose pose = Pose.identity;
                isLocationSuccess = ezxrCloudLocalization.GettLocalizedAnchorPose(ref pose);
                if (virtualWorld != null && isLocationSuccess)
                {
                    if (!virtualWorld.activeSelf)
                        virtualWorld.SetActive(true);
                    virtualWorld.transform.position = pose.position;
                    virtualWorld.transform.rotation = pose.rotation;
                }
            }
        }
        private void UpdateCameraImage()
        {
            if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking)
                return;
            if (ezxrCloudLocalization == null)
                return;

        if (!hasPoseOffsetSet)
        {
            NativeTracking.GetOSTParams(ref ost_m);
        }
        if (locCamType == LocCam.LocCam_RGB)
        {
            if (rgbCameraDevice == null)
            {
                return;
            }
            if (!hasPoseOffsetSet)
            {
                ezxrCloudLocalization.SetPoseFromHeadToLocCam(ost_m.T_RGB_Head);
                hasPoseOffsetSet = true;
            }
            bool res = false;
            res = rgbCameraDevice.getCurrentRGBImage(ref locCamImageBuffer, locCamIntriarray);
            if (res)
            {
                if (!hasIntricsSet)
                {
                    ezxrCloudLocalization.SetCameraIntrics(locCamIntriarray);
                    hasIntricsSet = true;
                }
                double timestamp_sec = locCamImageBuffer.timestamp;
                Pose imageTsHeadPose = ARFrame.GetHistoricalHeadPose(timestamp_sec);
                ezxrCloudLocalization.UpdateHeadPose(imageTsHeadPose, timestamp_sec);
                int format = 0;
                if (locCamImageBuffer.imgFormat == EZVIOImageFormat.EZVIOImageFormat_RGB)
                {
                    format = 0;
                }
                else
                {
                    format = 2;//gray
                }
                //Debug.Log("-10001-UpdateRgbFrame ezxrCloudLocalization.UpdateRGBImage:"+ locCamImageBuffer.fullImg+","+timestamp_sec.ToString("f3"));
                ezxrCloudLocalization.UpdateImage(locCamImageBuffer.fullImg, timestamp_sec, (int)locCamImageBuffer.imgRes.width, (int)locCamImageBuffer.imgRes.height, format);
            }
        }

            if (locCamType == LocCam.LocCam_FisheyeGray)
            {
                if (!hasPoseOffsetSet)
                {
                    ezxrCloudLocalization.SetPoseFromHeadToLocCam(ost_m.T_TrackCam_Head);
                    hasPoseOffsetSet = true;
                }
                bool res = NativeTracking.GetCuttedTrackingImage(ref locCamImageBuffer, locCamIntriarray);
                if (res)
                {
                    if (!hasIntricsSet)
                    {
                        ezxrCloudLocalization.SetCameraIntrics(locCamIntriarray);
                        hasIntricsSet = true;
                    }
                    double timestamp_sec = locCamImageBuffer.timestamp;
                    Pose imageTsHeadPose = ARFrame.GetHistoricalHeadPose(timestamp_sec);
                    ezxrCloudLocalization.UpdateHeadPose(imageTsHeadPose, timestamp_sec);
                    int format = 0;
                    if (locCamImageBuffer.imgFormat == EZVIOImageFormat.EZVIOImageFormat_RGB)
                    {
                        format = 0;
                    }
                    else
                    {
                        format = 2;//gray
                    }
                    //Debug.Log("-10001-UpdateRgbFrame ezxrCloudLocalization.UpdateRGBImage:"+ locCamImageBuffer.fullImg+","+timestamp_sec.ToString("f3"));
                    ezxrCloudLocalization.UpdateImage(locCamImageBuffer.fullImg, timestamp_sec, (int)locCamImageBuffer.imgRes.width, (int)locCamImageBuffer.imgRes.height, format);
                }

            }
        }
    }
}
