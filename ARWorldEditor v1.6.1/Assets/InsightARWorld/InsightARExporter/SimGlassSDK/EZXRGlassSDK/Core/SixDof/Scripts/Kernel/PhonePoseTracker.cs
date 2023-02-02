using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePoseTracker : MonoBehaviour
{
    private Transform m_Transform;

    private static PhonePoseTracker _instance;

    private static int picwith;
    private static int picheight; 

    public static PhonePoseTracker Instance
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
        Camera camera = this.gameObject.GetComponent<Camera>();

        NewCameraMatrix newCameraMatrix = new NewCameraMatrix();
        NativeTracking.GetPinholeCameraIntrinsic(ref newCameraMatrix);
        float fx = newCameraMatrix.fx;
        float fy = newCameraMatrix.fy;
        float cx = newCameraMatrix.cx;
        float cy = newCameraMatrix.cy;
        
        CameraResolution cameraResolution = new CameraResolution();
        NativeTracking.GetFisheyeCameraResolution(ref cameraResolution);
        picwith = cameraResolution.width;
        picheight = cameraResolution.height;
        Matrix4x4 m = CalculateProjectionMatrixFromCameraMatrixValues(fx, fy, cx, cy, picwith, picheight, camera.nearClipPlane, camera.farClipPlane);

        camera.projectionMatrix = m;
    }

    private void Update()
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

    private void UpdatePoseByTrackingType()
    {
        if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking) return;

        Pose phonePose = ARFrame.ImagePose;

        transform.position = phonePose.position;
        transform.rotation = phonePose.rotation;
    }

}
