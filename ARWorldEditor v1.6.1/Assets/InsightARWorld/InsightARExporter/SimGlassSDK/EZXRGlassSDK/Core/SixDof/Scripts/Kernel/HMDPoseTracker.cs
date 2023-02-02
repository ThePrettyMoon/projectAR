using EZXR.Glass.Common;
using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    [ScriptExecutionOrder(-11)]
    public class HMDPoseTracker : MonoBehaviour
    {
        #region singleton
        private static HMDPoseTracker _instance;
        public static HMDPoseTracker Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region params
        private bool isInit = false;
        public Camera leftCamera;
        public Camera rightCamera;
        public Camera centerCamera;

        [SerializeField]
        private bool m_UseLocalPose = true;

        private Transform m_Transform;

        private static OSTMatrices ost_m = new OSTMatrices();
        float[] left_rgb;
        float[] right_rgb;

        public Transform Head
        {
            get
            {
                return m_Transform;
            }
        }
        #endregion

        #region unity functions
        private void Awake()
        {
            _instance = this;
            m_Transform = transform;
        }

        private void Start()
        {
            StartCoroutine(InitParam());
        }

        private void Update()
        {
            //Debug.Log("=============Unity Log===============   HMDPoseTracker -- Update");

            if (!SessionManager.Instance.IsInited) return;

            if (ARFrame.SessionStatus < EZVIOState.EZVIOCameraState_Detecting) return;

            //if (!isInit)
            //{
            //    InitializeCameraPose();
            //    //InitializeCameraFieldOfViews();
            //    isInit = true;
            //}

            UpdatePoseByTrackingType();
        }

        #endregion

        #region custom functions

        private IEnumerator InitParam()
        {
            yield return new WaitUntil(() => SessionManager.Instance.IsInited);
            ost_m = new OSTMatrices();

            if (Application.isEditor)
            {
                centerCamera.gameObject.SetActive(true);
                leftCamera.gameObject.SetActive(false);
                rightCamera.gameObject.SetActive(false);

                ost_m.T_TrackCam_Head = new float[16];
                ost_m.T_RightEye_Head = new float[16];
                ost_m.T_LeftEye_Head = new float[16];
            }
            else
            {
#if !EZXRForMRTK
                centerCamera.gameObject.SetActive(false);
#endif
                leftCamera.gameObject.SetActive(true);
                rightCamera.gameObject.SetActive(true);

                NativeTracking.GetOSTParams(ref ost_m);
            }
            left_rgb = ost_m.T_LeftEye_Head;
            right_rgb = ost_m.T_RightEye_Head;
            InitializeCameraPose();
        }
        private void InitializeCameraPose()
        {
            Matrix4x4 left_head_matrix = new Matrix4x4();
            Matrix4x4 right_head_matrix = new Matrix4x4();

            for (int i = 0; i < 16; i++)
            {
                left_head_matrix[i] = left_rgb[i];
                right_head_matrix[i] = right_rgb[i];
            }

            Matrix4x4 head_left_matrix = left_head_matrix.transpose.inverse;
            Matrix4x4 head_right_matrix = right_head_matrix.transpose.inverse;

            Vector3 eyePoseLeft = head_left_matrix.GetColumn(3);
            Vector3 eyePoseRight = head_right_matrix.GetColumn(3);
            Quaternion eyeRotLeft = head_left_matrix.rotation;
            Quaternion eyeRotRight = head_right_matrix.rotation;

            if (leftCamera != null)
            {
                leftCamera.transform.localPosition = eyePoseLeft;
                leftCamera.transform.localRotation = eyeRotLeft;
            }

            // right

            if (rightCamera != null)
            {
                rightCamera.transform.localPosition = eyePoseRight;
                rightCamera.transform.localRotation = eyeRotRight;
            }

            isInit = true;
        }

        private void InitializeCameraFieldOfViews()
        {
            double[] fovs = ARFrame.CameraParams.fov;
            int imageWidth = ARFrame.CameraParams.width;
            int imageHeight = ARFrame.CameraParams.height;
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            Debug.Log("==========UNITY LOG Screen Size and fov : " + Screen.width + " " + Screen.height + " " + SessionManager.Instance.RenderMode);

            if (SessionManager.Instance.RenderMode == RenderMode.Bino) screenWidth /= 2;
            float[] screenFov = Common.CameraUtility.CalculateFov(imageWidth, imageHeight, screenWidth, screenHeight,
                new float[] { (float)fovs[0], (float)fovs[1] });
            float cameraFov = screenFov[1];

            if (leftCamera != null)
            {
                leftCamera.fieldOfView = cameraFov;
            }

            if (rightCamera != null)
            {
                rightCamera.fieldOfView = cameraFov;
            }

            Debug.Log("==========UNITY LOG FOV : " + cameraFov + " " + fovs[0] + " " + fovs[1]);

            if (centerCamera != null)
            {
                centerCamera.fieldOfView = cameraFov;
            }
        }

        private void UpdatePoseByTrackingType()
        {
            if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking) return;

            Pose headPose = ARFrame.HeadPose;

            if (m_UseLocalPose)
            {
                transform.localPosition = headPose.position;
                transform.localRotation = headPose.rotation;
            }
            else
            {
                transform.position = headPose.position;
                transform.rotation = headPose.rotation;
            }
        }
        #endregion
    }
}
