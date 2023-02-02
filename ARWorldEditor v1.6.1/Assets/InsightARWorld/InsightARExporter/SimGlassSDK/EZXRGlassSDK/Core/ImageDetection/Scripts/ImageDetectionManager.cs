using EZXR.Glass.SixDof;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using EZXR.Glass.Common;

namespace EZXR.Glass.ImageDetection
{
    public struct ImageDetectionInfo
    {
        /// <summary>
        /// 图像的名字
        /// </summary>
        public string name;
        /// <summary>
        /// 图像在场景中的位置
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// 图像在场景中的旋转
        /// </summary>
        public Quaternion rotation;
        /// <summary>
        /// 图像的物理宽度（单位：m）
        /// </summary>
        public float width;
        /// <summary>
        /// 图像的物理高度（单位：m）
        /// </summary>
        public float height;
    }
    public enum TrackCam
    {
        TrackCam_RGB,
        TrackCam_FisheyeGray
    }

    public class ImageDetectionManager : MonoBehaviour
    {
        #region singleton
        private static ImageDetectionManager _instance;
        public static ImageDetectionManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// ImageTrackingManager准备就绪可以进行检测
        /// </summary>
        public static bool isReady = false;

        [Serializable]
        class MarkersInfoStructure
        {
            [Serializable]
            public class MarkerInfo
            {
                public string name;
                public string uid;
                public float width;
                public float height;
            }
            public MarkerInfo[] MarkerInfos;
        }
        MarkersInfoStructure markersInfoStructure;

        Dictionary<string, MarkersInfoStructure.MarkerInfo> markersDic = new Dictionary<string, MarkersInfoStructure.MarkerInfo>();

        static bool markerStatus = false;
        static ImageDetectionInfo curImageDetectionInfo = default(ImageDetectionInfo);

        public TrackCam trackCamType = TrackCam.TrackCam_RGB;

        private bool hasIntricsSet = false;
        private bool hasPoseOffsetSet = false;

        private EZXRImageTrackingSession Track2dsession;
        private NormalRGBCameraDevice rgbCameraDevice;
        private EZVIOInputImage camImageBuffer = new EZVIOInputImage();
        private float[] camIntriArray = new float[8];
        private OSTMatrices ost_m = new OSTMatrices();

        // Start is called before the first frame update
        void Awake()
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

            DontDestroyOnLoad(gameObject);

            StartCoroutine(LoadConfig(Path.Combine(Application.streamingAssetsPath, "algModel/detect2d/config_airpro/markerInfo.json")));
        }

        IEnumerator LoadConfig(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log("LoadConfig Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log("LoadConfig Data: " + webRequest.downloadHandler.text);
                    markersInfoStructure = JsonUtility.FromJson<MarkersInfoStructure>(webRequest.downloadHandler.text);
                    foreach (MarkersInfoStructure.MarkerInfo item in markersInfoStructure.MarkerInfos)
                    {
                        markersDic.Add(item.name, item);
                    }
                    isReady = true;
                }
            }
        }

        private void OnEnable()
        {
            if (trackCamType == TrackCam.TrackCam_RGB)
            {
                StartCoroutine(startRGBCamera());
            }
            startTrackSession();
        }
        private void OnDisable()
        {
            stopTrackSession();
            if (trackCamType == TrackCam.TrackCam_RGB)
            {
                stopRGBCamera();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (isReady)
            {
                UpdateCameraImage();
                RelocByTrack2d();
            }
        }
        /// <summary>
        /// 得到检测到的Marker的结果
        /// </summary>
        /// <param name="markerInfo">返回检测到的Marker信息</param>
        /// <returns>检测到任意Marker则返回true</returns>
        public static bool GetDetectionResult(out ImageDetectionInfo markerInfo)
        {
            markerInfo = curImageDetectionInfo;
            return markerStatus;
        }


        private IEnumerator startRGBCamera()
        {
            yield return new WaitUntil(() => SessionManager.Instance.IsInited);
            hasIntricsSet = false;
            hasPoseOffsetSet = false;
            Debug.LogWarning("-10001-startRGBCamera new rgbCameraDevice Open");
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
            Track2dsession = new EZXRImageTrackingSession();

            Debug.Log("-10001-startTrackSession Track2dsession.Create");

            if (Track2dsession != null)
            {
                Track2dsession.Create(Application.persistentDataPath + "/ezxr/algModel/detect2d");
            }

            Debug.Log("-10001-startTrackSession end");
            //Invoke("RelocByTrack2d", 2.0f);
        }

        private void stopTrackSession()
        {
            Debug.Log("-10001-stopTrackSession");

            if (Track2dsession != null)
            {
                Track2dsession.Destroy();
                Track2dsession = null;
            }
        }
        private void RelocByTrack2d()
        {
            if (Track2dsession != null)
            {
                EZXRImageTrackingSession.EZXRMarkerAnchor marker = Track2dsession.GetTrackedObject();
                markerStatus = marker.isValid;
                if (marker.isValid)
                {
                    Debug.Log("RelocByTrack2d GetTrackedObject name=" + marker.name);
                    curImageDetectionInfo.name = marker.name;
                    curImageDetectionInfo.position = new Vector3(marker.pose.center[0], marker.pose.center[1], marker.pose.center[2]);
                    curImageDetectionInfo.rotation = new Quaternion(marker.pose.quaternion[0], marker.pose.quaternion[1], marker.pose.quaternion[2], marker.pose.quaternion[3]);
                    if (markersDic.ContainsKey(marker.name))
                    {
                        curImageDetectionInfo.width = markersDic[marker.name].width;
                        curImageDetectionInfo.height = markersDic[marker.name].height;
                    }
                    else
                    {
                        curImageDetectionInfo.width = 0.1f;
                        curImageDetectionInfo.height = 0.1f;
                    }
                }
            }
        }

        private void UpdateCameraImage()
        {
            if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking)
                return;
            if (Track2dsession == null)
                return;

            if (!hasPoseOffsetSet)
            {
                NativeTracking.GetOSTParams(ref ost_m);
            }
            if (trackCamType == TrackCam.TrackCam_RGB)
            {
                if (rgbCameraDevice == null)
                    return;
                if (!hasPoseOffsetSet)
                {
                    Track2dsession.SetPoseFromHeadToLocCam(ost_m.T_RGB_Head);
                    hasPoseOffsetSet = true;
                }
                bool res = false;
                if (!hasIntricsSet)
                {
                    res = rgbCameraDevice.getCameraIntrics(camIntriArray);
                    if (res)
                    {
                        Track2dsession.SetCameraIntrics(camIntriArray);
                        hasIntricsSet = true;
                    }
                }
                float[] locCamIntriarray = new float[8];
                res = rgbCameraDevice.getCurrentRGBImage(ref camImageBuffer, locCamIntriarray);
                if (res)
                {
                    double timestamp_sec = camImageBuffer.timestamp;
                    Pose imageTsHeadPose = ARFrame.GetHistoricalHeadPose(timestamp_sec);
                    Track2dsession.UpdateHeadPose(imageTsHeadPose, timestamp_sec);
                    int format = 0;
                    if (camImageBuffer.imgFormat == EZVIOImageFormat.EZVIOImageFormat_RGB)
                    {
                        format = 0;
                    }
                    else
                    {
                        format = 2;//gray
                    }
                    Track2dsession.UpdateImage(camImageBuffer.fullImg, timestamp_sec, (int)camImageBuffer.imgRes.width, (int)camImageBuffer.imgRes.height, format);
                }
            }
            if (trackCamType == TrackCam.TrackCam_FisheyeGray)
            {
                if (!hasPoseOffsetSet)
                {
                    Track2dsession.SetPoseFromHeadToLocCam(ost_m.T_TrackCam_Head);
                    hasPoseOffsetSet = true;
                }
                bool res = NativeTracking.GetCuttedTrackingImage(ref camImageBuffer, camIntriArray);
                if (res)
                {
                    if (!hasIntricsSet)
                    {
                        Track2dsession.SetCameraIntrics(camIntriArray);
                        hasIntricsSet = true;
                    }
                    double timestamp_sec = camImageBuffer.timestamp;
                    Pose imageTsHeadPose = ARFrame.GetHistoricalHeadPose(timestamp_sec);
                    Track2dsession.UpdateHeadPose(imageTsHeadPose, timestamp_sec);
                    int format = 0;
                    if (camImageBuffer.imgFormat == EZVIOImageFormat.EZVIOImageFormat_RGB)
                    {
                        format = 0;
                    }
                    else
                    {
                        format = 2;//gray
                    }
                    Track2dsession.UpdateImage(camImageBuffer.fullImg, timestamp_sec, (int)camImageBuffer.imgRes.width, (int)camImageBuffer.imgRes.height, format);
                }
            }
        }
    }
}