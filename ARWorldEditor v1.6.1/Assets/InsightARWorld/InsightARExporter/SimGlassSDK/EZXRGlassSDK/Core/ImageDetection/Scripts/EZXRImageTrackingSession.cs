using EZXR.Glass.SixDof;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EZXR.Glass.ImageDetection
{
    public class EZXRImageTrackingSession
    {

        public struct EZXRPose
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public float[] transform;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public float[] quaternion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] center;
            public double timestamp;
        }
        public struct EZXRMarkerAnchor
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string name;
            public EZXRPose pose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public float[] physicalSize;
            [MarshalAs(UnmanagedType.I1)]
            public bool isValid;
        }
        private bool isCreated = false;
        private IntPtr imagebufferPtr = IntPtr.Zero;

        private bool useInnerMarkerDetect = true;
        public void Create(string path)
        {
            if (isCreated)
                return;
            if (useInnerMarkerDetect)
            {
                isCreated = true;
            }
            else
            {
                isCreated = NativeApi.nativeInitialize2dtracker(path);
            }
        }
        public void Destroy()
        {
            if (!isCreated)
                return;
            if (useInnerMarkerDetect)
            {
            }
            else
            {
                NativeApi.nativeDestroy2dtracker();
            }
            if (imagebufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(imagebufferPtr);
            }
        }

        public EZXRMarkerAnchor GetTrackedObject()
        {
            EZXRMarkerAnchor marker = new EZXRMarkerAnchor();
            marker.isValid = false;
            if (!isCreated)
            {
                return marker;
            }

            if (useInnerMarkerDetect)
            {
                EZVIOResult markerPose = new EZVIOResult();
                NativeTracking.GetArucoResult(ref markerPose);
                if (markerPose.vioState == EZVIOState.EZVIOCameraState_Tracking)
                {
                    marker.isValid = true;
                    /*
                    Pose poseMarker = new Pose(
                        new Vector3(markerPose.translation[0], markerPose.translation[1], markerPose.translation[2]),
                        new Quaternion(markerPose.quaternion[0], markerPose.quaternion[1], markerPose.quaternion[2], markerPose.quaternion[3]));
                    ConversionUtility.RecenterByOffset(poseMarker, ARFrame.accumulatedRecenterOffset4x4, out poseMarker);
                    marker.pose.center[0] = poseMarker.position.x;
                    marker.pose.center[1] = poseMarker.position.y;
                    marker.pose.center[2] = poseMarker.position.z;
                    marker.pose.quaternion[0] = poseMarker.rotation.x;
                    marker.pose.quaternion[1] = poseMarker.rotation.y;
                    marker.pose.quaternion[2] = poseMarker.rotation.z;
                    marker.pose.quaternion[3] = poseMarker.rotation.w;
                    */

                    marker.name = "default";
                    //marker.physicalSize = new float[] { 0.407f, 1.0f, 0.283f };
                    marker.physicalSize = new float[] { 1.0f, 1.0f, 1.0f };

                    Matrix4x4 tr = new Matrix4x4();
                    for (int i = 0; i < 16; i++)
                    {
                        tr[i] = markerPose.camTransform[i];
                    }
                    Matrix4x4 tr_tp = tr.transpose;
                    Matrix4x4 transformUnity_new = ARFrame.accumulatedRecenterOffset4x4 * tr_tp;


                    Vector3 pos = transformUnity_new.GetColumn(3);
                    Quaternion rot = transformUnity_new.rotation;
                    marker.pose.center = new float[] { pos.x, pos.y, pos.z };
                    marker.pose.quaternion = new float[] { rot.x, rot.y, rot.z, rot.w };

                }
            }
            else
            {
                marker = NativeApi.nativeGetTrackedMarker();
            }
            return marker;
        }

        public void UpdateImage(IntPtr buffer, double timestamp, int width, int height, int format)
        {
            if (useInnerMarkerDetect)
            {
                return;
            }
            NativeApi.nativeOnImageAvailable(buffer.ToInt64(), timestamp, width, height, format);
        }
        public void UpdateImage(byte[] buffer, double timestamp, int width, int height, int format)
        {
            if (!isCreated)
            {
                return;
            }
            if (useInnerMarkerDetect)
            {
                return;
            }
            if (buffer == null)
                return;
            if (buffer.Length == 0)
                return;
            if (imagebufferPtr == IntPtr.Zero)
            {
                imagebufferPtr = Marshal.AllocHGlobal(buffer.Length);
            }
            Marshal.Copy(buffer, 0, imagebufferPtr, buffer.Length);

            //Debug.Log("-10001- OnUpdateFrame length=" + buffer.Length + ",size=" + width + "," + height + " | " + imagebufferPtr.ToInt64() + "," + timestamp);
            NativeApi.nativeOnImageAvailable(imagebufferPtr.ToInt64(), timestamp, width, height, format);
        }

        public void SetCameraIntrics(float[] intrics)
        {
            if (!isCreated)
            {
                return;
            }
            if (useInnerMarkerDetect)
            {
                return;
            }
            NativeApi.nativeSetImageIntrinsic(intrics);
        }

        public void SetPoseFromHeadToLocCam(float[] pose)
        {
            if (!isCreated)
            {
                return;
            }
            if (useInnerMarkerDetect)
            {
                return;
            }
            NativeApi.nativeSetRTFromHeadToLocCam(pose);
        }

        public void UpdateHeadPose(Pose pose, double timestamp)
        {
            if (!isCreated)
            {
                return;
            }
            if (useInnerMarkerDetect)
            {
                return;
            }
            if (pose == null)
                return;
            float[] posearray = new float[7] {
                pose.position.x,pose.position.y,pose.position.z,
                    pose.rotation.x,pose.rotation.y,pose.rotation.z,pose.rotation.w
            };
            NativeApi.nativeOnHeadPoseUpdated(posearray, timestamp);
        }

        private partial struct NativeApi
        {
            private const string nativelibraryname = "ezglass2dtrack";

            /// <summary>
            /// 初始化图像跟踪模块
            /// </summary>
            /// <param name="assetpath">图像资源路径</param>
            /// <returns>是否成功</returns>
            [DllImport(nativelibraryname)]
            public static extern bool nativeInitialize2dtracker(string assetpath);

            /// <summary>
            /// 释放图像跟踪模块
            /// </summary>
            [DllImport(nativelibraryname)]
            public static extern void nativeDestroy2dtracker();

            /// <summary>
            /// 获取图像跟踪结果
            /// </summary>
            /// <returns>输出图像跟踪结果</returns>
            [DllImport(nativelibraryname)]
            public static extern EZXRMarkerAnchor nativeGetTrackedMarker();

            /// <summary>
            /// 传递图像数据到定位模块
            /// </summary>
            /// <param name="ptr">图像缓存指针</param>
            /// <param name="timestamp">图像时间戳，单位为秒</param>
            /// <param name="width">图像宽</param>
            /// <param name="height">图像高</param>
            /// <param name="format">图像格式。0:RGB;1:RGBA;default:GREY</param>
            [DllImport(nativelibraryname)]
            public static extern void nativeOnImageAvailable(Int64 ptr, double timestamp, int width, int height, int format);

            /// <summary>
            /// 设置相机内参
            /// </summary>
            /// <param name="ptr">相机内参，4个元素的float数组。{fx,fy,cx,cy,k1,k2,p1,p2}</param>
            [DllImport(nativelibraryname)]
            public static extern void nativeSetImageIntrinsic([Out, In][MarshalAs(UnmanagedType.LPArray, SizeConst = 8)] float[] ptr);
            /// <summary>
            /// 设置从Head到相机的偏置
            /// </summary>
            /// <param name="ptr">quaternion+position的float数组</param>
            [DllImport(nativelibraryname)]
            public static extern void nativeSetRTFromHeadToLocCam([Out, In][MarshalAs(UnmanagedType.LPArray, SizeConst = 7)] float[] ptr);
            /// <summary>
            /// 传递设备6Dof跟踪Pose
            /// </summary>
            /// <param name="ptr">输入设备6Dof Pose，时间戳，quaternion+position的float数组</param>
            /// <param name="posetimestamp">时间戳，单位为秒</param>
            [DllImport(nativelibraryname)]
            public static extern bool nativeOnHeadPoseUpdated([MarshalAs(UnmanagedType.LPArray, SizeConst = 7)] float[] ptr, double timestamp);
        }
    }
}