using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

namespace InsightAR.Internal
{
    public static class NavigatorNative
    {
        public struct InsightARLightEstimateResult
        {
            public int flag;                            // scene claasification result, for indoor: 1; for outdoor: -1
            public double illuminIntensity;                // illumin intensity
            public double illuminTemperature;              // illumin temperature
            public double[] illuminColor;                 // illumin color

            public int primaryLightAvailable;
            public float[] primaryLightDirection;
            public double primaryLightIntensity;
            public double[] primaryLightColor;
        }
        public delegate void Internal_NaviCallback(IntPtr navidata, int type, IntPtr pHandle);

        public delegate void Internal_Navi2DPoseCallback(IntPtr navidata, IntPtr pHandle);

#if UNITY_EDITOR

        /// 检查导航模块的状态是否正常。
        /// 请在iarlsinit/iarlsstart之后，iarlsstop之前调用。
        /// return:  0 状态正常。
        ///         -1 仍未初始化，可于稍后重新尝试调用。
        ///         >0 大场景初始化错误。【iOS】对应的errorcode，详见该文件末尾。
        public static int iarlsCheckNavigatorStatus() { return 0; }

        /// [NEW]
        /// 注册导航的回调
        /// @param callback 导航的回调，回调的参数是json字符串和type，json字符串在同步调用是安全的。
        public static void iarlsSetNaviCallback(Internal_NaviCallback callback) { }

        /// [NEW]
        /// 注册VIO坐标转地图2D坐标的回调
        /// @param callback 坐标的回调，回调的参数是json字符串，json字符串在同步调用是安全的
        public static void iarlsSetNavi2DPoseCallback(Internal_Navi2DPoseCallback callback) { }

        /// Init navigation
        /// @param naviPath 导航地图，不包括'/'
        /// @param geoPath  geoJson路径，不包含'/'
        public static void iarlsInitNavi(string naviPath, string geoPath) { }

        /// [NEW]
        /// 开始进行导航，导航信息会通过注册的callback返回。
        /// @param begin 结束位置的描述，json格式，可为NULL
        /// @param end 结束位置的描述，json格式
        public static void iarlsBeginNavi(string begin, string end) { }

        /// [NEW]
        /// 开始进行坐标转换，坐标转换信息会通过注册的callback返回。
        /// @param begin 结束位置的描述，json格式，可为NULL
        /// @param end 结束位置的描述，json格式，可为NULL
        public static void iarlsConvertPose(string begin, string end) { }

        /// [NEW]
        /// 结束导航。
        public static void iarlsEndNavi() { }

        /// [NEW]
        /// 主动获取所有POI信息。
        /// @param result 传入的二维指针，(*result)会填入结果数据，需要外部释放(*result)
        /// @param type 获取的navi信息的类型，返回对应的导航信息(*result)
        public static void iarlsQueryPOIList([Out] out IntPtr result, int type) {
            result = IntPtr.Zero;
        }

        /// [NEW]
        /// 主动获取地图信息。
        /// @param result 传入的二维指针，(*result)会填入结果数据，需要外部释放(*result)
        /// @param input 获取的navi信息的类型，返回对应的导航信息(*result)
        /// @param type 获取的navi信息的类型，返回对应的导航信息(*result)
        public static void iarlsQueryNavigInfos([Out] out IntPtr result, string input, int type){
            result = IntPtr.Zero;
        }
        #region 测试光照估计
        public static bool isLightEstimateEnabled() { return true; }
        public static InsightARLightEstimateResult iarGetLightEstimate()
        {
             var obj = new InsightARLightEstimateResult();
            obj.illuminIntensity = 900;
            obj.illuminTemperature = 1;
            return obj;
        }
        #endregion
#else
#if UNITY_ANDROID
        private const string Dll_Name = "AREngine";
#elif UNITY_IOS
        private const string Dll_Name = "__Internal";
#endif
        ///Return true means navigator instance alive; false means navigator instance unavailable.
         [DllImport(Dll_Name)]
         public static extern int iarlsCheckNavigatorStatus();

        /// [NEW]
        /// 注册导航的回调
        /// @param callback 导航的回调，回调的参数是json字符串和type，json字符串在同步调用是安全的。
        [DllImport(Dll_Name)]
        public static extern void iarlsSetNaviCallback(Internal_NaviCallback callback);

        /// [NEW]
        /// 注册VIO坐标转地图2D坐标的回调
        /// @param callback 坐标的回调，回调的参数是json字符串，json字符串在同步调用是安全的
        [DllImport(Dll_Name)]
        public static extern void iarlsSetNavi2DPoseCallback(Internal_Navi2DPoseCallback callback);

        /// Init navigation
        /// @param naviPath 导航地图，不包括'/'
        /// @param geoPath  geoJson路径，不包含'/'
        [DllImport(Dll_Name)]
        public static extern void iarlsInitNavi(string naviPath, string geoPath);

        /// [NEW]
        /// 开始进行导航，导航信息会通过注册的callback返回。
        /// @param begin 结束位置的描述，json格式，可为NULL
        /// @param end 结束位置的描述，json格式
        [DllImport(Dll_Name)]
        public static extern void iarlsBeginNavi(string begin, string end);

        /// [NEW]
        /// 开始进行坐标转换，坐标转换信息会通过注册的callback返回。
        /// @param begin 结束位置的描述，json格式，可为NULL
        /// @param end 结束位置的描述，json格式，可为NULL
        [DllImport(Dll_Name)]
        public static extern void iarlsConvertPose(string begin, string end);

        /// [NEW]
        /// 结束导航。
        [DllImport(Dll_Name)]
        public static extern void iarlsEndNavi();

        /// [NEW]
        /// 主动获取所有POI信息。
        /// @param result 传入的二维指针，(*result)会填入结果数据，需要外部释放(*result)
        /// @param type 获取的navi信息的类型，返回对应的导航信息(*result)
        [DllImport(Dll_Name)]
        public static extern void iarlsQueryPOIList([Out]out IntPtr result, int type);

        /// [NEW]
        /// 主动获取地图信息。
        /// @param result 传入的二维指针，(*result)会填入结果数据，需要外部释放(*result)
        /// @param input 获取的navi信息的类型，返回对应的导航信息(*result)
        /// @param type 获取的navi信息的类型，返回对应的导航信息(*result)
        [DllImport(Dll_Name)]
        public static extern void iarlsQueryNavigInfos([Out]out IntPtr result, string input, int type);
        #region 测试光照估计
        [DllImport(Dll_Name)]
        public static extern bool isLightEstimateEnabled();
          [DllImport(Dll_Name)]
        public static extern InsightARLightEstimateResult iarGetLightEstimate();
        #endregion
#endif
    }

}