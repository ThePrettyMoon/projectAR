using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using EZXR.Glass.SixDof;

namespace EZXR.Glass.SixDof
{
    /// <summary>
    /// 调用position 等数据
    /// </summary>
    public class ARFrame
    {
        #region field & properties
        private static EZVIOState m_SessionState;
        private static LostTrackingReason m_LostTrackingReason;
        private static Pose m_headPose = new Pose();
        private static Pose m_headPose_rgb = new Pose();
        private static double m_headPoseTimestamp = 0.0;
        private static double m_headPoseRgbTimestamp = 0.0;
        private static Pose m_imagePose = new Pose();
        private static EZVIOResult m_VIOResult = new EZVIOResult();
        private static long m_originVIOFetchTimeNS = 0;            // unix epoch time
        private static EZVIOResult m_VIOResult_RGB = new EZVIOResult();
        private static CamParams m_CameraParams = new CamParams();
        //private static DebugInfo m_DebugInfo;

        private static TrackableManager m_trackableManager = new TrackableManager();

        private static int m_stateFirstTracking = 0;

        /// <summary> Gets the current recenter offset;  new = offset * old(the last) </summary>
        /// <value> The recenter offset 4x4 Matrix. </value>
        private static Matrix4x4 m_RecenterOffset4x4 = Matrix4x4.identity;
        public static Matrix4x4 recenterOffset4x4
        {
            get
            {
                return m_RecenterOffset4x4;
            }
        }

        /// <summary> Gets the current recenter offset that accumulated all the interval offset;  new = offset * old(the origin) </summary>
        /// <value> The accumulated recenter offset 4x4 Matrix. </value>
        private static Matrix4x4 m_AccumulatedRecenterOffset4x4 = Matrix4x4.identity;
        public static Matrix4x4 accumulatedRecenterOffset4x4
        {
            get
            {
                return m_AccumulatedRecenterOffset4x4;
            }
        }

        public static EZVIOState SessionStatus
        {
            get { return m_SessionState; }
        }

        public static LostTrackingReason LostTrackingReason
        {
            get
            {
                return m_LostTrackingReason;
            }
        }


        public static Pose HeadPose
        {
            get
            {
                return m_headPose;
            }
        }

        public static Pose HeadPoseRGB
        {
            get
            {
                return m_headPose_rgb;
            }
        }
        public static double HeadPoseTimestamp
        {
            get
            {
                return m_headPoseTimestamp;
            }
        }
        public static double HeadPoseRgbTimestamp
        {
            get
            {
                return m_headPoseRgbTimestamp;
            }
        }

        public static Pose ImagePose
        {
            get
            {
                return m_imagePose;
            }
        }

        public static EZVIOResult OriginVIOResult
        {
            get
            {
                return m_VIOResult;
            }
        }
        public static long VIOResultFetchTimeNS
        {
            get
            {
                return m_originVIOFetchTimeNS;
            }
        }

        public static CamParams CameraParams
        {
            get
            {
                if (SessionManager.Instance.IsInited)
                {
                    NativeTracking.GetCameraParams(ref m_CameraParams);
                }
                return m_CameraParams;
            }
        }

        public static TrackableManager trackableManager
        {
            get
            {
                return m_trackableManager;
            }
        }
        #endregion

        public static void OnAwake()
        {
            m_stateFirstTracking = 0;
        }

        /// <summary>
        /// on update
        /// </summary>
        public static void OnUpdate()
        {
            NativeTracking.GetTrackResult(ref m_VIOResult);
            m_originVIOFetchTimeNS = NativeTracking.GetSystemTime();
            NativeTracking.GetRgbCameraTrackResult(ref m_VIOResult_RGB);
            m_SessionState = m_VIOResult.vioState;
            m_headPoseTimestamp = m_VIOResult.timestamp;
            m_headPoseRgbTimestamp = m_VIOResult_RGB.timestamp;
            //Debug.Log("UNITY LOG ============ : state : " + m_SessionState);

            //算法状态做限制
            if ((int)m_SessionState > (int)EZVIOState.EZVIOCameraState_Track_Fail
                || (int)m_SessionState < (int)EZVIOState.EZVIOCameraState_Initing)
            {
                m_SessionState = EZVIOState.EZVIOCameraState_Initing;
            }

            m_LostTrackingReason = (LostTrackingReason)m_VIOResult.vioReason;
            if (m_SessionState != EZVIOState.EZVIOCameraState_Tracking)
                return;

            m_headPose.position = new Vector3(m_VIOResult.translation[0], m_VIOResult.translation[1], m_VIOResult.translation[2]);
            m_headPose.rotation = new Quaternion(m_VIOResult.quaternion[0], m_VIOResult.quaternion[1], m_VIOResult.quaternion[2], m_VIOResult.quaternion[3]);

            if (m_stateFirstTracking == 0)
            {
                m_stateFirstTracking = 1;
                ReCenter();
            }

            ConversionUtility.RecenterByOffset(m_headPose, m_AccumulatedRecenterOffset4x4, out m_headPose);

            m_imagePose.position = new Vector3(m_VIOResult.translation[0], m_VIOResult.translation[1], m_VIOResult.translation[2]);
            m_imagePose.rotation = new Quaternion(m_VIOResult.quaternion[0], m_VIOResult.quaternion[1], m_VIOResult.quaternion[2], m_VIOResult.quaternion[3]);

            m_headPose_rgb.position = new Vector3(m_VIOResult_RGB.translation[0], m_VIOResult_RGB.translation[1], m_VIOResult_RGB.translation[2]);
            m_headPose_rgb.rotation = new Quaternion(m_VIOResult_RGB.quaternion[0], m_VIOResult_RGB.quaternion[1], m_VIOResult_RGB.quaternion[2], m_VIOResult_RGB.quaternion[3]);
            ConversionUtility.RecenterByOffset(m_headPose_rgb, m_AccumulatedRecenterOffset4x4, out m_headPose_rgb);
        }

        public static Pose GetHistoricalHeadPose(double imagetime)
        {
            EZVIOResult historicalResult = new EZVIOResult();
            NativeTracking.getHistoricalResult(ref historicalResult, imagetime);
            Pose historicalHeadPose = Pose.identity;
            historicalHeadPose.position = new Vector3(historicalResult.translation[0], historicalResult.translation[1], historicalResult.translation[2]);
            historicalHeadPose.rotation = new Quaternion(historicalResult.quaternion[0], historicalResult.quaternion[1], historicalResult.quaternion[2], historicalResult.quaternion[3]);

            ConversionUtility.RecenterByOffset(historicalHeadPose, m_AccumulatedRecenterOffset4x4, out historicalHeadPose);
            return historicalHeadPose;
        }
        public static void ReCenter()
        {
            Debug.Log("=============Unity Log===============   ARFrame -- ReCenter");

            Matrix4x4 Origin_on_Wld = new Matrix4x4(
                m_headPose.right, m_headPose.up, m_headPose.forward, m_headPose.position);
            Origin_on_Wld.m33 = 1.0f;

            //Debug.Log("=============Unity Log===============   ARFrame -- ReCenter OriginPose: " +
            //    Origin_on_Wld.m00 + " " + Origin_on_Wld.m01 + " " + Origin_on_Wld.m02 + " " + Origin_on_Wld.m03 + " " +
            //    Origin_on_Wld.m10 + " " + Origin_on_Wld.m11 + " " + Origin_on_Wld.m12 + " " + Origin_on_Wld.m13 + " " +
            //    Origin_on_Wld.m20 + " " + Origin_on_Wld.m21 + " " + Origin_on_Wld.m22 + " " + Origin_on_Wld.m23 + " " +
            //    Origin_on_Wld.m30 + " " + Origin_on_Wld.m31 + " " + Origin_on_Wld.m32 + " " + Origin_on_Wld.m33
            //    );

            Vector3 forward = m_headPose.forward;
            Vector3 up = m_headPose.up;
            Vector3 right = m_headPose.right;

            Vector3 forward_new;
            {
                // head facing down
                if (forward.y < -1.0f * (1.0f - 1e-3))
                {
                    forward_new = Vector3.Normalize(new Vector3(up.x, 0, up.z));
                }
                else if (forward.y > 1.0f * (1.0f - 1e-3))
                {
                    forward_new = Vector3.Normalize(new Vector3(-1.0f * up.x, 0, -1.0f * up.z));
                }
                else
                {
                    forward_new = Vector3.Normalize(new Vector3(forward.x, 0, forward.z));
                }
            }
            Vector3 up_new = new Vector3(0, 1.0f, 0);
            Vector3 right_new;
            {
                Vector3 y = up_new;
                Vector3 z = forward_new;
                right_new = new Vector3(y.y * z.z - y.z * z.y, -1.0f * (y.x * z.z - y.z * z.x), y.x * z.y - y.y * z.x);
            }
            Matrix4x4 OriginNew_on_Wld = new Matrix4x4(right_new, up_new, forward_new, m_headPose.position);
            OriginNew_on_Wld.m33 = 1.0f;

            //Debug.Log("=============Unity Log===============   ARFrame -- ReCenter NewPose: " +
            //    OriginNew_on_Wld.m00 + " " + OriginNew_on_Wld.m01 + " " + OriginNew_on_Wld.m02 + " " + OriginNew_on_Wld.m03 + " " +
            //    OriginNew_on_Wld.m10 + " " + OriginNew_on_Wld.m11 + " " + OriginNew_on_Wld.m12 + " " + OriginNew_on_Wld.m13 + " " +
            //    OriginNew_on_Wld.m20 + " " + OriginNew_on_Wld.m21 + " " + OriginNew_on_Wld.m22 + " " + OriginNew_on_Wld.m23 + " " +
            //    OriginNew_on_Wld.m30 + " " + OriginNew_on_Wld.m31 + " " + OriginNew_on_Wld.m32 + " " + OriginNew_on_Wld.m33
            //);


            // recenterOffset4x4
            m_RecenterOffset4x4 = Matrix4x4.Inverse(OriginNew_on_Wld);
            m_AccumulatedRecenterOffset4x4 = m_RecenterOffset4x4 * m_AccumulatedRecenterOffset4x4;

            // update m_headPose
            //ConversionUtility.RecenterByOffset(m_headPose, m_AccumulatedRecenterOffset4x4, out m_headPose);
            //{
            //    Matrix4x4 headPose_afterRecenter = new Matrix4x4(
            //     m_headPose.right, m_headPose.up, m_headPose.forward, m_headPose.position);
            //    headPose_afterRecenter.m33 = 1.0f;
            //    Debug.Log("=============Unity Log===============   ARFrame -- ReCenter headPose after recenter: " +
            //        headPose_afterRecenter.m00 + " " + headPose_afterRecenter.m01 + " " + headPose_afterRecenter.m02 + " " + headPose_afterRecenter.m03 + " " +
            //        headPose_afterRecenter.m10 + " " + headPose_afterRecenter.m11 + " " + headPose_afterRecenter.m12 + " " + headPose_afterRecenter.m13 + " " +
            //        headPose_afterRecenter.m20 + " " + headPose_afterRecenter.m21 + " " + headPose_afterRecenter.m22 + " " + headPose_afterRecenter.m23 + " " +
            //        headPose_afterRecenter.m30 + " " + headPose_afterRecenter.m31 + " " + headPose_afterRecenter.m32 + " " + headPose_afterRecenter.m33
            //    );
            //}

            // update TrackableManager
            trackableManager.RecenterIncrementalMeshes();
            trackableManager.RecenterPlane();
        }
    }
}
