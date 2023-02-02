using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using AOT;

namespace InsightAR.Internal
{
    public class NavigationInterface
    {
        public const string TAG = "NavigationInterface";
        private static NavigationInterface m_Interface;
        public static NavigationInterface GetInstance()
        {
            if (m_Interface == null){ m_Interface = new NavigationInterface(); }
            return m_Interface;
        }

        public static Action<string, int> onNavigationChanged;
        public static Action<NaviCallbackData> naviCallbackAction;
        private static List<NaviCallbackData> naviCallbackDatas;

        public static bool isNavigationState = false;

        private static bool NaviWorkingState = false;

        public static void EnterNavigation() {

            
            naviCallbackDatas = new List<NaviCallbackData>();

            NaviWorkingState = true;
        }

        public static void UpdateNavigation() {

            if (NaviWorkingState == false) {
                return;
            }
#if UNITY_EDITOR
            AddNaviCallback("",1);
#endif
            updateNaviCallbacks();

        }

        public static void EndNavigation() {

            NaviWorkingState = false;
            onNavigationChanged = null;
            if (naviCallbackDatas != null)
                naviCallbackDatas.Clear();

        }

        public static void AddNaviListeners()
        {
            naviCallbackAction += OnNaviCallback;
        }

        public static void RemoveNaviListeners() 
        {
            onNavigationChanged = null;
            naviCallbackAction -= OnNaviCallback;
        }

        public static void AddNaviCallback(string navidata, int type) {
#if UNITY_EDITOR
            OnNaviCallback(new NaviCallbackData
            {
                navidata = "{\"pathLength\":14.65502591,\"userState\":{\"isOffRoute\":0,\"angleWithPath\":170.41496349},\"userNavTips\":{\"userAction\":1,\"distance\":14.12641518},\"floorPaths\":[{\"floorAction\":0,\"floorId\":\"1\",\"mapPointsSum\":18,\"mapPoints\":[{\"floorId\":\"1\",\"isPass\":1,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22880212, 30.24451797],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.93841457, -3.59999990, -4.17987394],\"rotation\":[0, 0.70710683, 0, 0.70710683]}, {\"floorId\":\"1\",\"isPass\":1,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22880101, 30.24451815],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.95939064, 0, -4.28670073],\"rotation\":[0, 0.70710683, 0, 0.70710683]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22880233, 30.24451794],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.93152905, -3.59999990, -4.14470577],\"rotation\":[0, 0.06170595, 0, 0.99809438]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22881258, 30.24451694],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.80835152, -3.59999990, -3.15232110],\"rotation\":[0, 0.06170595, 0, 0.99809438]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22882290, 30.24451593],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.68517494, -3.59999990, -2.15993619],\"rotation\":[0, 0.06170595, 0, 0.99809438]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22883258, 30.24451497],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.57053280, -3.59999990, -1.23631203],\"rotation\":[0, 0.09652379, 0, 0.99533069]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22884272, 30.24451335],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.37838745, -3.59999990, -0.25494570],\"rotation\":[0, 0.09652379, 0, 0.99533069]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22885293, 30.24451172],\"direction2DYaw\":0,\"realSpaceCoords\":[-14.18624115, -3.59999990, 0.72642064],\"rotation\":[0, 0.09652379, 0, 0.99533069]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22886616, 30.24450959],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.93873692, -3.59999990, 1.99052215],\"rotation\":[0, 0.09652315, 0, 0.99533075]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22886971, 30.24450902],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.87183094, -3.59999990, 2.33224010],\"rotation\":[0, 0.04530334, 0, 0.99897331]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22887999, 30.24450832],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.78131676, -3.59999990, 3.32813549],\"rotation\":[0, 0.04530334, 0, 0.99897331]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22889035, 30.24450760],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.69080353, -3.59999990, 4.32403040],\"rotation\":[0, 0.04530334, 0, 0.99897331]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22890338, 30.24450669],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.57749081, -3.59999990, 5.57077074],\"rotation\":[0, 0.09652365, 0, 0.99533069]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22891352, 30.24450507],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.38534546, -3.59999990, 6.55213737],\"rotation\":[0, 0.09652365, 0, 0.99533069]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22892373, 30.24450343],\"direction2DYaw\":0,\"realSpaceCoords\":[-13.19319916, -3.59999990, 7.53350353],\"rotation\":[0, 0.09652365, 0, 0.99533069]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":1,\"turnAngle\":0},\"geographicCoords\":[120.22893780, 30.24450118],\"direction2DYaw\":0,\"realSpaceCoords\":[-12.92984486, -3.59999990, 8.87856102],\"rotation\":[0, 0.09652326, 0, 0.99533075]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":3,\"turnAngle\":1.56419539},\"geographicCoords\":[120.22894472, 30.24450007],\"direction2DYaw\":0,\"realSpaceCoords\":[-12.79968166, -3.59999990, 9.54336166],\"rotation\":[0, 0.77415079, 0, 0.63300121]}, {\"floorId\":\"1\",\"isPass\":0,\"navPointTurnInfo\":{\"navAction\":6,\"turnAngle\":0},\"geographicCoords\":[120.22894304, 30.24449244],\"direction2DYaw\":0,\"realSpaceCoords\":[-11.95570183, -3.59999990, 9.37232304],\"rotation\":[0, 0.70710683, 0, 0.70710683]}]}]}",
                type = type
            });
            return;
#else
            if (NaviWorkingState == false)
            {
                if (naviCallbackDatas != null && naviCallbackDatas.Count > 0)
                {
                    naviCallbackDatas.Clear();
                }
                return;
            }
            naviCallbackDatas.Add(new NaviCallbackData
            {
                navidata = navidata,
                type = type
            });
#endif
        }

        private static void updateNaviCallbacks() {

            NaviCallbackData naviCallbackData;
            int length = naviCallbackDatas.Count;
            for (int i = 0; i < length; i++)
            {
                naviCallbackData = naviCallbackDatas[i];
                if (naviCallbackAction != null)
                    naviCallbackAction(naviCallbackData);
            }
            naviCallbackDatas.Clear();

        }


        private static void OnNaviCallback(NaviCallbackData naviCallbackData)
        {
            var data = naviCallbackData.navidata;
            var type = naviCallbackData.type;
            //update to native
            InsightAPPNative.SetCurrentNaviPath(GameSceneData.Instance.GetNavigationType(), data);
            //expose to content
            onNavigationChanged?.Invoke(data, type);

            //Debug.Log("navi data: " + data);
        }
    }
}

