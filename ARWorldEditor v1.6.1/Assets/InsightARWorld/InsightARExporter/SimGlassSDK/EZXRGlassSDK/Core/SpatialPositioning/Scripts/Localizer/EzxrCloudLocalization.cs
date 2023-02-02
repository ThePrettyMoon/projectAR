using System;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SpatialPositioning
{
    using AOT;
    using EZXR;
    using EZXR.Glass.Common;
    using EZXR.Glass.SpatialPositioning;
    using System.Collections.Generic;
    using UnityEngine;
    using Wheels.Unity;

    public class EzxrCloudLocalization
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
        public struct EZXRSpaceLocAnchor
        {
            public EZXRPose pose;
            [MarshalAs(UnmanagedType.I1)]
            public bool isValid;
        }


        //public const string CloudLocationUrl = "https://reloc-gw.easexr.com/api/alg/cloud/aw/reloc/proxy?routeApp=parkc&map=arglass";

        private delegate void Internal_Request_CloudLoc(EzxrCloudLocRequest requestData, IntPtr pHandle);

        private static List<EzxrCloudLocRequestImpl> listCloudLocRequest;

        /// <summary>
        /// 服务器返回的云定位结果中关于定位结果的状态
        /// </summary>
        public int CloudLocAlgCode
        {
            get
            {
                return !isCreated ? 0 : cloudLocResultStatus.algCode;
            }
        }

        /// <summary>
        /// 服务器返回的云定位结果中关于定位结果的原因说明
        /// </summary>
        public string CloudLocAlgReason
        {
            get
            {
                return !isCreated ? string.Empty : cloudLocResultStatus.algResult;
            }
        }

        /// <summary>
        /// 初始化定位模块
        /// 初始化提供三种模式：
        ///     5sec：前次定位成功的情况下，每隔5秒发出一次云定位请求，失败后则2.5秒一次
        ///     3sec：前次定位成功的情况下，每隔3秒发出一次云定位请求，失败后则1.5秒一次
        ///     once：仅在主动触发的情况下发起云定位请求，配合TriggerOnceCloudLocRequest使用
        /// </summary>
        public void Create()
        {
            if (isCreated)
                return;
            listCloudLocRequest = new List<EzxrCloudLocRequestImpl>();
#if UNITY_ANDROID
            isCreated = NativeApi.nativeInitializeLocalizer(0);
            NativeApi.nativeRegisterCloudRequestCallback(onRequestCloudLoc);
#endif
        }

        /// <summary>
        /// 销毁定位模块
        /// </summary>
        public void Destroy()
        {
            if (!isCreated)
                return;
#if UNITY_ANDROID
            NativeApi.nativeDestroyLocalizer();
#endif
            if (imagebufferPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(imagebufferPtr);
            }
        }

        /// <summary>
        /// 触发云定位请求，在服务器返回云定位成功结果之前，云定位请求会一直按照5sec模式发送
        /// 云定位成功后不再发送云定位请求，直到下次调用该接口
        /// </summary>
        public void TriggerOnceCloudLocRequest()
        {

            if (!isCreated)
                return;
#if UNITY_ANDROID
            NativeApi.nativeTriggerOnceCloudLocRequest();
#endif
        }

        /// <summary>
        /// 处理云定位请求。
        /// 云定位请求通过回调发送给当前类，需要在MonoBehaviour.Update()中调用该接口，以向服务器发送请求来处理云定位
        /// </summary>
        public void HandleLocalizerRequest(string url)
        {
            int length = listCloudLocRequest.Count;

            //Debug.Log("HandleLocalizerRequest - listCloudLocRequest.Count: " + length);

            for (int i = 0; i < length; i++)
            {
                EzxrCloudLocRequestImpl requestData = listCloudLocRequest[i];
                RequestCloudLocDirectly(url, requestData);
            }
            listCloudLocRequest.Clear();
        }

        /// <summary>
        /// 获取定位节点的坐标
        /// 定位成功后，Pose是定位与跟踪结合之后的坐标变换
        /// 应用到Head父节点后，Head的global坐标就是实际建图坐标系中的位置
        /// </summary>
        /// <param name="pose">最近一次定位成功后，处理后的Pose</param>
        /// <returns>定位成功后返回true，反之，false</returns>
        public bool GettLocalizedAnchorPose(ref Pose pose)
        {
            if (pose == null)
                return false;
            if (!isCreated)
            {
                pose = Pose.identity;
                return false;
            }
#if UNITY_ANDROID
            EZXRSpaceLocAnchor anchor = NativeApi.nativeGetLocalizedAnchor();
            if (anchor.isValid == true)
            {
                pose.position = new Vector3(anchor.pose.center[0], anchor.pose.center[1], anchor.pose.center[2]);
                pose.rotation = new Quaternion(anchor.pose.quaternion[0], anchor.pose.quaternion[1], anchor.pose.quaternion[2], anchor.pose.quaternion[3]);
            }
            return anchor.isValid;
#else
            return false;
#endif
        }

        /// <summary>
        /// 向定位模块更新6Dof的Pose及时间戳
        /// </summary>
        /// <param name="pose">6Dof的Pose</param>
        /// <param name="timestamp_sec">时间戳</param>
        public void UpdateHeadPose(Pose pose, double timestamp_sec)
        {
            if (!isCreated)
                return;
            if (pose == null)
                return;
            float[] posearray = new float[7] {
                pose.position.x,pose.position.y,pose.position.z,
                    pose.rotation.x,pose.rotation.y,pose.rotation.z,pose.rotation.w
            };
#if UNITY_ANDROID
            NativeApi.nativeOnHeadPoseUpdated(posearray, timestamp_sec);
#endif
        }

        /// <summary>
        /// 向定位模块更新设备相机的图像、时间戳、宽高、图像格式
        /// </summary>
        /// <param name="buffer">以byte数组形式输入图像缓存</param>
        /// <param name="timestamp">图像缓存对应的时间戳</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="format">图像格式，0：RGB；1：RGBA；other：Grey</param>
        public void UpdateImage(byte[] buffer, double timestamp, int width, int height, int format)
        {
            if (!isCreated)
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
#if UNITY_ANDROID
            NativeApi.nativeOnImageAvailable(imagebufferPtr.ToInt64(), timestamp, width, height, format);
#endif
        }

        /// <summary>
        /// 向定位模块更新设备相机的图像、时间戳、宽高、图像格式
        /// </summary>
        /// <param name="bufferptr">以指针形式输入图像缓存</param>
        /// <param name="timestamp">图像缓存对应的时间戳</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="format">图像格式，0：RGB；1：RGBA；other：Grey</param>
        public void UpdateImage(IntPtr bufferptr, double timestamp, int width, int height, int format)
        {
            if (!isCreated)
            {
                return;
            }
            if (bufferptr == null)
                return;

            //Debug.Log("-10001- OnUpdateFrame length=" + buffer.Length + ",size=" + width + "," + height + " | " + imagebufferPtr.ToInt64() + "," + timestamp);
#if UNITY_ANDROID
            NativeApi.nativeOnImageAvailable(bufferptr.ToInt64(), timestamp, width, height, format);
#endif
        }

        /// <summary>
        /// 向定位模块更新设备相机内参
        /// </summary>
        /// <param name="intrics">fx,fy,cx,cy</param>
        public void SetCameraIntrics(float[] intrics)
        {
            if (!isCreated)
            {
                return;
            }
#if UNITY_ANDROID
            NativeApi.nativeSetImageIntrinsic(intrics);
#endif
        }

        /// <summary>
        /// 向定位模块更新Head到设备相机的外参
        /// </summary>
        /// <param name="pose">外参</param>
        public void SetPoseFromHeadToLocCam(float[] pose)
        {
            if (!isCreated)
            {
                return;
            }
#if UNITY_ANDROID
            NativeApi.nativeSetRTFromHeadToLocCam(pose);
#endif
        }


        private bool isCreated = false;
        private IntPtr imagebufferPtr = IntPtr.Zero;
        private EzxrCloudLocResultStatus cloudLocResultStatus = new EzxrCloudLocResultStatus();


        [MonoPInvokeCallback(typeof(EzxrCloudLocalization.Internal_Request_CloudLoc))]
        private static void onRequestCloudLoc(EzxrCloudLocRequest cloudLocRequestData, IntPtr pHandler)
        {
            EzxrCloudLocRequestImpl requestImpl = new EzxrCloudLocRequestImpl(cloudLocRequestData);
            listCloudLocRequest.Add(requestImpl);
        }

        [Serializable]
        public class RequestData
        {
            [Serializable]
            public class Alg
            {
                public string imageEncodingData;
                public string protobufEncodingData;
            }

            public Alg alg;
            public long timestamp;
            public string sign;

            public RequestData()
            {
                alg = new Alg();
            }
        }
        [Serializable]
        public class ResponeData
        {
            [Serializable]
            public class Result
            {
                public string protobufEncodingData;
                public int algCode;
            }
            public string code;
            public string msg;
            public Result result;
        }

        private void RequestCloudLocDirectly(string url, EzxrCloudLocRequestImpl requestData)
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long time = (long)(DateTime.UtcNow - epochStart).TotalMilliseconds;
            long requestT = time;
            string nonce = Guid.NewGuid().ToString().Substring(0, 8);

            var tk = "";

            string signature = "cloud.reloc|" + nonce + "|" + requestT + "|" + tk;
            string hashSha256 = EncodeUtility.Sha256(signature).ToLower();

            RequestData data = new RequestData();
            data.alg.imageEncodingData = requestData.jpgStr;
            data.alg.protobufEncodingData = requestData.requestInfoStr;
            data.timestamp = requestT;
            data.sign = hashSha256;

            string data4Send = JsonUtility.ToJson(data);
            //Debug.Log("RequestCloudLocDirectly - imageEncodingData.Length: " + data.alg.imageEncodingData.Length + ", protobufEncodingData: " + data.alg.protobufEncodingData);
            UnityWebRequest.Instance.SetRequestHeader("Content-Type", "application/json");
            UnityWebRequest.Instance.Create(url, data4Send, RequestMode.POST, GetData, "RequestCloudLocDirectly");
        }

        public void GetData(string data, string identifier, long statusCode = 200)
        {
            Debug.Log("RequestCloudLocDirectly - GetData: " + data);
            ResponeData responeResult = JsonUtility.FromJson<ResponeData>(data);
            HandleSingleCloudLocResult(responeResult.result.protobufEncodingData, responeResult.result.algCode.ToString());
        }

        private void HandleSingleCloudLocResult(string protoData, string algCode)
        {

            cloudLocResultStatus.algCode = int.Parse(algCode);
            if (cloudLocResultStatus.algCode >= 9000)
            {
                cloudLocResultStatus.algResult = protoData;
                return;
            }
            cloudLocResultStatus.algResult = "";

            EzxrCloudLocResult ezxrCloudLocResult = new EzxrCloudLocResult();
            EzxrCloudLocResultMeta ezxrCloudLocResultMeta = new EzxrCloudLocResultMeta();
            ezxrCloudLocResultMeta.timestamp = 0.0;
            ezxrCloudLocResultMeta.status = 0;
            ezxrCloudLocResult.meta = ezxrCloudLocResultMeta;
            try
            {
                //base64 解码
                byte[] buffer = Convert.FromBase64String(protoData);
                int length = buffer.Length;
                IntPtr resultPtr = Marshal.AllocHGlobal(length);
                Marshal.Copy(buffer, 0, resultPtr, length);
                ezxrCloudLocResult.resultInfoPtr = resultPtr;
                ezxrCloudLocResult.resultLength = length;

                if (isCreated)
                {
#if UNITY_ANDROID
                    NativeApi.nativeOnCloudLocalized(ezxrCloudLocResult);
#endif
                }

                //供底层算法调用之后，释放指针
                Marshal.FreeHGlobal(resultPtr);
            }
            catch (FormatException exp)
            {

            }
        }


        private partial struct NativeApi
        {
#if UNITY_ANDROID

            private const string Dll_Name = "glasslocalizer";


            /// <summary>
            /// 初始化定位模块
            /// Mode_5_sec: 0, 5s when success, 2.5s when fail
            /// Mode_3_sec: 1, 3s when success, 1.5s when fail
            /// Mode_trigger: 2, only when trigger 
            /// </summary>
            /// <param name="locRequestMode">locRequestMode</param>
            /// <returns></returns>
            [DllImport(Dll_Name)]
            public static extern bool nativeInitializeLocalizer(int locRequestMode);

            /// <summary>
            /// 释放定位模块
            /// </summary>
            [DllImport(Dll_Name)]
            public static extern void nativeDestroyLocalizer();

            /// <summary>
            /// 注册云定位请求回调
            /// </summary>
            /// <param name="callbaclk">云定位请求回调，delegate</param>
            [DllImport(Dll_Name)]
            public static extern void nativeRegisterCloudRequestCallback(Internal_Request_CloudLoc callback);

            /// <summary>
            /// 传递设备6Dof跟踪Pose，并更新定位后的坐标
            /// </summary>
            /// <param name="ptr">[Out, In]输入设备6Dof Pose，输出结合定位结果后的Pose,quaternion+position的float数组</param>
            /// <param name="posetimestamp">时间戳，单位为秒</param>
            [DllImport(Dll_Name)]
            public static extern void nativeOnHeadPoseUpdated([Out, In][MarshalAs(UnmanagedType.LPArray, SizeConst = 7)] float[] ptr, double posetimestamp);

            /// <summary>
            /// 获取定位坐标
            /// </summary>
            /// <param name="ptr">输出定位坐标,quaternion+position的float数组</param>
            /// <returns></returns>
            [DllImport(Dll_Name)]
            public static extern EZXRSpaceLocAnchor nativeGetLocalizedAnchor();

            /// <summary>
            /// 设置RGB相机内参
            /// </summary>
            /// <param name="ptr">相机内参，4个元素的float数组。{fx,fy,cx,cy}</param>
            [DllImport(Dll_Name)]
            public static extern void nativeSetImageIntrinsic([Out, In][MarshalAs(UnmanagedType.LPArray, SizeConst = 8)] float[] ptr);

            /// <summary>
            /// 设置从Head到RGB的偏置
            /// </summary>
            /// <param name="ptr">quaternion+position的float数组</param>
            [DllImport(Dll_Name)]
            public static extern void nativeSetRTFromHeadToLocCam([Out, In][MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] float[] ptr);

            /// <summary>
            /// 传递图像数据到定位模块
            /// </summary>
            /// <param name="ptr">图像缓存指针</param>
            /// <param name="timestamp">图像时间戳，单位为秒</param>
            /// <param name="width">图像宽</param>
            /// <param name="height">图像高</param>
            /// <param name="format">图像格式。0:RGB;1:RGBA;default:GREY</param>
            [DllImport(Dll_Name)]
            public static extern void nativeOnImageAvailable(Int64 ptr, double timestamp, int width, int height, int format);

            /// <summary>
            /// 传递定位结果到定位模块
            /// </summary>
            /// <param name="result">服务器返回的定位结果</param>
            [DllImport(Dll_Name)]
            public static extern void nativeOnCloudLocalized(EzxrCloudLocResult result);
            /// <summary>
            /// 触发一次云定位请求
            /// </summary>
            [DllImport(Dll_Name)]
            public static extern void nativeTriggerOnceCloudLocRequest();


#endif //UNITY_ANDROID
        }
    }
}