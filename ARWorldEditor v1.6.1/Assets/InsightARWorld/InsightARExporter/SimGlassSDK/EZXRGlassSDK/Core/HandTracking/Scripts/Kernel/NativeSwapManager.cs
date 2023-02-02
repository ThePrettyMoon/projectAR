#define GrayCamera

using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using EZXR.Glass.SixDof;
using EZXR.Glass.Common;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 确保此脚本执行顺序在ARHandSDK中为首位，因为整个ARHandSDK的数据刷新在此脚本的Update中
    /// </summary>
    [ScriptExecutionOrder(-10)]
    /// <summary>
    /// 与底层算法交互
    /// </summary>
    public class NativeSwapManager : MonoBehaviour
    {
        #region 单例
        private static NativeSwapManager instance;
        public static NativeSwapManager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region 与算法交互的struct定义
        [StructLayout(LayoutKind.Sequential)]
        public struct Point2
        {
            public float x;
            public float y;

            public override string ToString()
            {

                return "(" + x + "," + y + ")";
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point3
        {
            public float x;
            public float y;
            public float z;

            public Point3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            public Point3(Vector3 point)
            {
                x = point.x;
                y = point.y;
                z = point.z;
            }
            public override string ToString()
            {

                return "(" + x + "," + y + "," + z + ")";
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HandTrackingData
        {
            public int version;
            /// <summary>
            /// 检测范围内是否存在手
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool isTracked;
            public float confidence;
            /// <summary>
            /// 用来确定这个数据是左手的还是右手的
            /// </summary>
            public HandType handType;
            /// <summary>
            /// 手势类型
            /// </summary>
            public GestureType gestureType;
            /// <summary>
            /// 手部节点的数量
            /// </summary>
            public uint handJointsCount;
            /// <summary>
            /// 0是拇指根节点，3是大拇指指尖点，4是食指根节点，16是小拇指根节点向手腕延伸点（不到手腕），21是掌心点，22是手腕靠近大拇指，23是手腕靠近小拇指，24是手腕中心点
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public HandJointData[] handJointData;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public int[] reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HandJointData
        {
            public int version;
            public HandJointType handJointType;
            public Mat4f handJointPose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public int[] reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Mat4f
        {
            public Vector4f col0;
            public Vector4f col1;
            public Vector4f col2;
            public Vector4f col3;
        }

        public struct Vector4f
        {
            public float x, y, z, w;
        }
        #endregion

        private partial struct NativeAPI
        {
#if EZXRCS
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getHandPoseData([Out, In] IntPtr handposedata, int size);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getHandPoseDataWithHeadPose([Out, In] IntPtr handposedata, int handposedataSize, float[] headpose, int headposeSize, double headposetimestamp);
#else
            [DllImport(NativeConsts.NativeLibrary)]
            public static extern void getHandPoseData([Out, In] IntPtr handposedata);

            [DllImport(NativeConsts.NativeLibrary)]
            public static extern void getHandPoseDataWithHeadPose([Out, In] IntPtr handposedata, float[] headpose, double headposetimestamp);
#endif
        }

        public static void filterPoint(ref Point3 point, int id)
        {
            //if (!Application.isEditor)
            //{
            //    filter_point(ref point, id);
            //}
        }

        #region 变量定义
        /// <summary>
        /// HandTrackingData更新的话会通知到外部
        /// </summary>
        public static event Action<HandTrackingData[]> OnHandTrackingDataUpdated;

        /// <summary>
        /// 用于从算法c++获得检测结果，这个指针是在非托管区域开辟的内存的指针（内存大小是2个HandTrackingData长度）
        /// </summary>
        public static IntPtr ptr_HandTrackingData;
        IntPtr ptr_HandTrackingData0;
        /// <summary>
        /// 手部追踪数据
        /// </summary>
        HandTrackingData[] handTrackingData = new HandTrackingData[2];
        int handTrackingDataLength;
        #endregion


        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            //为了接收算法的结果，在非托管内存区域开辟长度为2个HandTrackingData的内存
            handTrackingDataLength = Marshal.SizeOf(typeof(HandTrackingData));
            ptr_HandTrackingData = Marshal.AllocHGlobal(handTrackingDataLength * 2);
            ptr_HandTrackingData0 = new IntPtr(ptr_HandTrackingData.ToInt64() + handTrackingDataLength);

            //用于将Marshal开辟的非托管区域的所有值清零以避免内存区域原数据造成的影响
            HandTrackingData handTrackingData = new HandTrackingData();
            Marshal.StructureToPtr(handTrackingData, ptr_HandTrackingData, false);
            Marshal.StructureToPtr(handTrackingData, ptr_HandTrackingData0, false);
        }

        void Update()
        {
            if (Application.isEditor || ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking || ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Track_Limited)
            {
                if (!Application.isEditor)
                {
                    //Debug.Log("开始get_nreal_result_predict");
                    //getHandPoseData(ptr_HandTrackingData);

                    float[] headPosearr = new float[]{
                        ARFrame.HeadPose.position.x,ARFrame.HeadPose.position.y,ARFrame.HeadPose.position.z,
                        ARFrame.HeadPose.rotation.x,ARFrame.HeadPose.rotation.y,ARFrame.HeadPose.rotation.z,ARFrame.HeadPose.rotation.w };

#if EZXRCS
                    NativeAPI.getHandPoseDataWithHeadPose(ptr_HandTrackingData, handTrackingDataLength * 2, headPosearr, headPosearr.Length, ARFrame.HeadPoseTimestamp);
#else
                    NativeAPI.getHandPoseDataWithHeadPose(ptr_HandTrackingData, headPosearr, ARFrame.HeadPoseTimestamp);
#endif
                    //Debug.Log("得到数据get_nreal_result_predict");
                }

                //Debug.Log("开始解析handTrackingData");
                handTrackingData[0] = (HandTrackingData)Marshal.PtrToStructure(ptr_HandTrackingData, typeof(HandTrackingData));
                handTrackingData[1] = (HandTrackingData)Marshal.PtrToStructure(ptr_HandTrackingData0, typeof(HandTrackingData));
                //Debug.Log("解析handTrackingData完毕");

                if (OnHandTrackingDataUpdated != null)
                {
                    OnHandTrackingDataUpdated(handTrackingData);
                }
            }
        }
    }
}