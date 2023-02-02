using EZXR.Glass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public class PhonePoseTrackerRGB : MonoBehaviour
    {
        private Transform m_Transform;

        private static PhonePoseTrackerRGB _instance;

        private static int picwith;
        private static int picheight;

        public static PhonePoseTrackerRGB Instance
        {
            get
            {
                return _instance;
            }
        }

        //https://gitfub.space/Pownie/arskrald/blob/716b28b3e55d0d2e253dc346ccc79b23a08bcd1c/AR-3/Assets/OpenCVForUnity/org/opencv/unity/ARUtils.cs
        static Matrix4x4 CalculateProjectionMatrixFromCameraMatrixValues(float fx, float fy, float cx, float cy, float width, float height, float near, float far)
        {
            Matrix4x4 projectionMatrix = new Matrix4x4();
            projectionMatrix.m00 = 2.0f * fx / width;
            projectionMatrix.m02 = 1.0f - 2.0f * cx / width;
            projectionMatrix.m11 = 2.0f * fy / height;
            projectionMatrix.m12 = -1.0f + 2.0f * cy / height;
            projectionMatrix.m22 = -(far + near) / (far - near);
            projectionMatrix.m23 = -2.0f * far * near / (far - near);
            projectionMatrix.m32 = -1.0f;

            return projectionMatrix;
        }

        // Start is called before the first frame update
        void Start()
        {
            //@buqing init param when start
            StartCoroutine(InitParam());
        }

        // Update is called once per frame
        void Update()
        {
            if (!SessionManager.Instance.IsInited) return;

            if (ARFrame.SessionStatus < EZVIOState.EZVIOCameraState_Detecting) return;

            UpdatePoseByTrackingType();
        }

        private void Awake()
        {
            _instance = this;
            m_Transform = gameObject.transform;
        }

        public Transform PhonePose
        {
            get
            {
                return m_Transform;
            }
        }

        private IEnumerator InitParam()
        {
            yield return new WaitUntil(() => SessionManager.Instance.IsInited);
            Camera camera = this.gameObject.GetComponent<Camera>();

            //TODO: set camera K using api
            //sim202 640*480
            // float fx = 440.5479f; float fy = 439.5698f; float cx = 327.0984f; float cy = 232.1016f;

            // //suuny906_5 1280*720
            // float fx = 948.22821044921875f; float fy = 964.41040039062500f; float cx = 632.86542646638190f; float cy = 363.23454826979287f;

            //suuny905_5 1280*720
            // float fx = 9.3901688684921101e+02f; float fy = 9.3694444882146183e+02f; float cx = 6.3597585809675786e+02f; float cy = 3.5656284432573852e+02f;

            //suuny905_3 1280*720
            // float fx = 9.3951211058148999e+02f; float fy = 9.4484850672782250e+02f; float cx = 6.2849769441352487e+02f; float cy = 3.6032056679124508e+02f;
            float[] intrinsic = new float[4];
            NativeTracking.GetRGBCameraIntrics(intrinsic);
            float fx = intrinsic[0];
            float fy = intrinsic[1];
            float cx = intrinsic[2];
            float cy = intrinsic[3];

            Debug.Log("=============Unity Log===============   PhonePoseTrackerRGB -- InitParam   rgb intrinsic " + fx + " " + fy + " " + cx + " " + cy);

            CameraResolution cameraResolution = new CameraResolution();
            NativeTracking.GetRGBCameraResolution(ref cameraResolution);
            picwith = cameraResolution.width;
            picheight = cameraResolution.height;
            Matrix4x4 m = CalculateProjectionMatrixFromCameraMatrixValues(fx, fy, cx, cy, picwith, picheight, camera.nearClipPlane, camera.farClipPlane);

            camera.projectionMatrix = m;
        }

        private void UpdatePoseByTrackingType()
        {
            // ARFrame.OnFixedUpdate();
            if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking) return;
            Pose phonePose = ARFrame.HeadPoseRGB;

            transform.localPosition = phonePose.position;
            transform.localRotation = phonePose.rotation;
        }

    }

}