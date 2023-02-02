using AOT;
using System;
using System.Runtime.InteropServices;

namespace InsightAR.Internal
{
    /// <summary>
    /// 管理navigator 数据
    /// </summary>
    public class NaviSceneManager:Singleton<NaviSceneManager>
    {
        #region params
        private const string TAG = "NaviSceneManager";
        #endregion

        #region functions
        /// <summary>
        /// 判断是否可以调用
        /// </summary>
        /// <returns></returns>
        public int CheckNavigatorStatus()
        {
            return NavigatorNative.iarlsCheckNavigatorStatus();
        }

        public void SetNaviCallback() {
            NavigatorNative.iarlsSetNaviCallback(onNaviUpdate);
        }

        [MonoPInvokeCallback(typeof(NavigatorNative.Internal_NaviCallback))]
        public static void onNaviUpdate(IntPtr navidata, int type, IntPtr pHandler)
        {
            NavigationInterface.AddNaviCallback(Marshal.PtrToStringAnsi(navidata), type);
        }

        public void SetNavi2DPoseCallback()
        {
            NavigatorNative.iarlsSetNavi2DPoseCallback(onNavi2DPoseUpdate);
        }

        [MonoPInvokeCallback(typeof(NavigatorNative.Internal_Navi2DPoseCallback))]
        public static void onNavi2DPoseUpdate(IntPtr navidata, IntPtr pHandler)
        {
            LocationInterface.AddNavi2DPoseCallback(Marshal.PtrToStringAnsi(navidata));
        }

        //[new]
        public void InitNavi(string naviPath, string geoPath) {
            NavigatorNative.iarlsInitNavi(naviPath, geoPath);
        }

        public void BeginNavi(string begin, string end) {
            NavigatorNative.iarlsBeginNavi(begin, end);
        }

        public void ConvertPose(string begin, string end) {
#if UNITY_IOS
            NavigatorNative.iarlsConvertPose(begin, end);
#endif
        }

        public void EndNavi() {
            NavigatorNative.iarlsEndNavi();
        }

        public string QueryPOIList(int type) {
            //NavigatorNative.iarlsQueryPOIList(out result, type);
            IntPtr result;
            NavigatorNative.iarlsQueryPOIList(out result, type);

            if (result == IntPtr.Zero) return null;

            string jsonStr = Marshal.PtrToStringAnsi(result);
            //InsightDebug.Log(TAG, "poiinfo " + jsonStr);
            //Marshal.FreeHGlobal(result);
            return jsonStr;
        }

        public string QueryNavigInfos(string input, int type)
        {
            //NavigatorNative.iarlsQueryNavigInfos(out result, input, type);
            IntPtr result;
            NavigatorNative.iarlsQueryNavigInfos(out result, input, type);
            if (result == IntPtr.Zero) return null;

            string jsonStr = Marshal.PtrToStringAnsi(result);
            //  InsightDebug.Log(TAG, "Query Navigation Poi Path " + jsonStr);
            //Marshal.FreeHGlobal(result);
            return jsonStr;
        }
#endregion
    }
}