using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using InsightAR.Internal;

/// <summary>
/// insight ar utility
/// </summary>
public static class InsightARUtility
{
    #region create data

    public static float[] GetCameraMatrixFromTrackResult(InsightARCameraPose cameraPose)
    {
        float[] cameraMatrix = new float[8];
        cameraMatrix[0] = cameraPose.center_u3d[0];
        cameraMatrix[1] = cameraPose.center_u3d[1];
        cameraMatrix[2] = cameraPose.center_u3d[2];
        cameraMatrix[3] = cameraPose.quaternion_u3d[0];
        cameraMatrix[4] = cameraPose.quaternion_u3d[1];
        cameraMatrix[5] = cameraPose.quaternion_u3d[2];
        cameraMatrix[6] = cameraPose.quaternion_u3d[3];
        return cameraMatrix;
    }

    #endregion

    #region utility
    public static void SetColumn(this Matrix4x4 mat, int i, InsightARVector4 ivect)
    {
        Vector4 vect = new Vector4(ivect.x, ivect.y, ivect.z, ivect.w);
        mat.SetColumn(i, vect);
    }

    /// <summary>
    /// 从4*4 矩阵获取pos 
    /// </summary>
    /// <returns>The position.</returns>
    /// <param name="matrix">Matrix.</param>
    public static Vector3 GetPosition(Matrix4x4 matrix)
    {
        Vector3 position = matrix.GetColumn(3);
        return position;
    }

    /// <summary>
    /// 从mat 获取四元数  
    /// </summary>
    /// <returns>The rotation.</returns>
    /// <param name="matrix">Matrix.</param>
    public static Quaternion GetRotation(Matrix4x4 matrix)
    {
        Quaternion rotation = QuaternionFromMatrix(matrix);
        return rotation;
    }

    /// <summary>
    /// 4*4 转到四元数 
    /// </summary>
    /// <returns>The from matrix.</returns>
    /// <param name="m">M.</param>
    static Quaternion QuaternionFromMatrix(Matrix4x4 m)
    {
        // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
        Quaternion q = new Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
        q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
        q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
        return q;
    }

    public static InsightARPlaneAnchor GetPlaneAnchorFromAnchorData(InsightARAnchorData anchor)
    {
        InsightARPlaneAnchor arPlaneAnchor = new InsightARPlaneAnchor();
        arPlaneAnchor.identifier = string.Copy(anchor.identifier);
        arPlaneAnchor.alignment = anchor.alignment;
#if UNITY_ANDROID
        if (InsightARNative.iarlsGetCurrentAREngine() == AREngines_Type.ARCORE || InsightARNative.iarlsGetCurrentAREngine() == AREngines_Type.HUAWEI_AR)
        {
            float[] tran = new float[] { anchor.center.x, anchor.center.y, anchor.center.z };
            float[] quat = new float[] { anchor.rotation.x, anchor.rotation.y, anchor.rotation.z, anchor.rotation.w };
            Vector3 camPos;
            Quaternion camRot;
            InsightARMath.Cvt_GLPose_UnityPose(
                tran, quat, out camPos, out camRot);
            arPlaneAnchor.center = camPos;
            arPlaneAnchor.rotation = camRot;
            arPlaneAnchor.extent = new Vector3(anchor.extent.x, 1.0f, anchor.extent.z);

        }
        else
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetColumn(0, new Vector4(anchor.transform.column0.x, anchor.transform.column0.y, anchor.transform.column0.z, anchor.transform.column0.w));
            matrix.SetColumn(1, new Vector4(anchor.transform.column1.x, anchor.transform.column1.y, anchor.transform.column1.z, anchor.transform.column1.w));
            matrix.SetColumn(2, new Vector4(anchor.transform.column2.x, anchor.transform.column2.y, anchor.transform.column2.z, anchor.transform.column2.w));
            matrix.SetColumn(3, new Vector4(anchor.transform.column3.x, anchor.transform.column3.y, anchor.transform.column3.z, anchor.transform.column3.w));
            arPlaneAnchor.rotation = GetRotation(matrix);
            arPlaneAnchor.extent = new Vector3(anchor.extent.x, anchor.extent.y, anchor.extent.z);
#if UNITY_ANDROID
            arPlaneAnchor.center = new Vector3(anchor.center.x, anchor.center.y, anchor.center.z);
#elif UNITY_IOS
               		arPlaneAnchor.center =  GetPosition(matrix);
#endif
            arPlaneAnchor.isValid = anchor.isValid;
        }
#else
            Matrix4x4 matrix = new Matrix4x4 ();
				matrix.SetColumn (0, new Vector4 (anchor.transform.column0.x, anchor.transform.column0.y, anchor.transform.column0.z, anchor.transform.column0.w));
				matrix.SetColumn (1, new Vector4 (anchor.transform.column1.x, anchor.transform.column1.y, anchor.transform.column1.z, anchor.transform.column1.w));
				matrix.SetColumn (2, new Vector4 (anchor.transform.column2.x, anchor.transform.column2.y, anchor.transform.column2.z, anchor.transform.column2.w));
				matrix.SetColumn (3, new Vector4 (anchor.transform.column3.x, anchor.transform.column3.y, anchor.transform.column3.z, anchor.transform.column3.w));
				arPlaneAnchor.rotation = GetRotation (matrix);
				arPlaneAnchor.extent = new Vector3 (anchor.extent.x, anchor.extent.y, anchor.extent.z);
#if UNITY_ANDROID
					arPlaneAnchor.center = new Vector3 (anchor.center.x, anchor.center.y, anchor.center.z);
#elif UNITY_IOS
               		arPlaneAnchor.center =  GetPosition(matrix);
#endif
				arPlaneAnchor.isValid = anchor.isValid;
#endif

        return arPlaneAnchor;
    }

    public static InsightARMarkerAnchor GetMarkerAnchorFromAnchorData(InsightARAnchorData anchor)
    {
        InsightARMarkerAnchor arMarkerAnchor = new InsightARMarkerAnchor();
        arMarkerAnchor.identifier = string.Copy(anchor.identifier);
#if UNITY_IOS || UNITY_ANDROID
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(0, new Vector4(anchor.transform.column0.x, anchor.transform.column0.y, anchor.transform.column0.z, anchor.transform.column0.w));
        matrix.SetColumn(1, new Vector4(anchor.transform.column1.x, anchor.transform.column1.y, anchor.transform.column1.z, anchor.transform.column1.w));
        matrix.SetColumn(2, new Vector4(anchor.transform.column2.x, anchor.transform.column2.y, anchor.transform.column2.z, anchor.transform.column2.w));
        matrix.SetColumn(3, new Vector4(anchor.transform.column3.x, anchor.transform.column3.y, anchor.transform.column3.z, anchor.transform.column3.w));
        arMarkerAnchor.rotation = GetRotation(matrix);
        arMarkerAnchor.extent = new Vector3(anchor.extent.x, anchor.extent.y, anchor.extent.z);
        arMarkerAnchor.center = GetPosition(matrix);
        arMarkerAnchor.isValid = anchor.isValid;
#endif
        return arMarkerAnchor;
    }

    public static InsightARRecognizedResult GetARRecognizedResult(InsightARRecognizedResultNative result)
    {
        InsightARRecognizedResult tmp = new InsightARRecognizedResult();
        tmp.isCloudMode = result.isCloudMode == 1;
        tmp.type = result.type;
        tmp.recognizedResult = Marshal.PtrToStringAnsi(result.recognizedResultPtr);
        return tmp;
    }

    private static Vector3 cameraPosition = new Vector3(-12,0,-80);//森林
    //private static Vector3 cameraPosition = new Vector3(40, 1.2f, -2);
    private static Vector3 cameraDirection = Vector3.zero;

    public static InsightARResult CreateARResult()
    {
        InsightARResult insightARResult = new InsightARResult();
        if (Time.time < 10)
        {
            insightARResult.state = 1;
        }
        else if (Time.time < 20)
        {
            insightARResult.state = 7;
        }
        //else if (Time.time < 40)
        //{
        //    insightARResult.state = 8;
        //}
        else
        {
            insightARResult.state = 7;
        }
        insightARResult.param = new InsightARCameraParam();
        insightARResult.param.fov = new float[2] { 45.0f, 45.0f };
        insightARResult.param.width = 1280;
        insightARResult.param.height = 960;
        insightARResult.camera = new InsightARCameraPose();

        Vector3 v0 = Vector3.zero;
        Vector3 v1 = new Vector3(20.21f, 2.6f, -15.28f);
        /*Vector3 v2 = new Vector3(47.213f, 1.208f, -0.405f);
        Vector3 v3 = new Vector3(44.761f, 1.208f, -2.951f);*/
        cameraPosition.z += 0.5f*Time.deltaTime;
        //if (cameraPosition.x > 54) cameraPosition.x = 34.0f;


        /* Vector3 v01 = v0 + (v1 - v0).normalized * (Vector3.Distance(v0, v1) * 3 / 4);
         Vector3 v12 = v1 + (v2 - v1).normalized * (Vector3.Distance(v1, v2) * 3 / 4);
         Vector3 v23 = v2 + (v3 - v2).normalized * (Vector3.Distance(v2, v3) * 3 / 4);*/

        /*if (Vector3.Distance(cameraPosition,Vector3.zero) < 1f)
        {
            cameraPosition = v01;
            cameraDirection = (v1 - v0).normalized;
        }

        if (Vector3.Distance(cameraPosition, v1) <1f)
        {
            cameraPosition = v12;
            cameraDirection = (v2 - v1).normalized;
        }
        

        if (Vector3.Distance(cameraPosition, v2) < 1f)
        {
            cameraPosition = v23;
            cameraDirection = (v3 - v2).normalized;
        }


        if (Vector3.Distance(cameraPosition, v3) < 1f)
        {
            cameraPosition = v01;
            cameraDirection = (v1 - v0).normalized;
        }*/

        //cameraPosition += Time.deltaTime*0.5f * cameraDirection;

        insightARResult.camera.center_u3d = new float[3] {cameraPosition.x,cameraPosition.y,cameraPosition.z };
        Quaternion q = Quaternion.Euler(0, 90, 0);
        insightARResult.camera.quaternion_u3d = new float[4] { q.x,q.y,q.z,q.w};
        return insightARResult;
    }

    /// <summary>
    /// Gets the name of the animation index by marker.
    /// </summary>
    /// <returns>The animation index by marker name.</returns>
    /// <param name="markerName">Marker name.</param>
    public static int GetAnimationIndexByMarkerName(string markerName)
    {
        int animIndex = 0;
        if (markerName == "A-1.png" || markerName == "B-1.png")
        {
            animIndex = 1;
        }
        else if (markerName == "A-2.png" || markerName == "B-2.png")
        {
            animIndex = 2;
        }
        else if (markerName == "A-3.png" || markerName == "B-3.png")
        {
            animIndex = 3;
        }
        else if (markerName == "A-4.png" || markerName == "B-4.png")
        {
            animIndex = 4;
        }
        return animIndex;
    }


#if UNITY_ANDROID
    public static void CopyAssetsToApplicationDataDir(string folder)
    {
        AndroidJavaClass cls_fileutil = new AndroidJavaClass("com.netease.insightar.commonbase.utils.FileUtil");
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject u3d_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        cls_fileutil.CallStatic<bool>("copyAssetsToApplicationDir", u3d_Activity, folder);
    }
#endif
    #endregion
}


