using Dongjian.LargeScale;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;

namespace InsightAR.Internal
{
    public class InsightARManager : ARBaseManager
    {
        public const string TAG = "InsightARManager";
        private static InsightARManager _instance;

        public static InsightARManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("ARManager").AddComponent<InsightARManager>();
                }
                return _instance;
            }
        }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = gameObject.GetComponent<InsightARManager>();
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 初始化相机
        /// </summary>
        /// <param name="algorithPath"></param>
        /// <param name="arCamera"></param>
        public void Init(string algorithPath, InsightARCamera arCamera)
        {
            m_ARCamera = arCamera.GetCamera();

            InitAR(algorithPath);

            InsightARInterface.recognizeAction += RecognitionBodyCallback;

        }

        public void ResetAR(string algorithPath, InsightARCamera arCamera)
        {
            m_ARCamera = arCamera.GetCamera();
            ResetAR(algorithPath);
        }



        public void Start()
        {

            //string algorithPath = GameSceneData.Instance.GetCurrentAlgPath();

            // InitAR(algorithPath);

            //InsightARInterface.recognizeAction += RecognitionBodyCallback;

        }

        public void OnDisable()
        {
            StopAR();
            InsightARInterface.recognizeAction -= RecognitionBodyCallback;
        }

        public override void InitAR(string configDir = "")
        {
            SetConfig(configDir, InsightConst.APPKEY, InsightConst.APPSECRET);
            base.InitAR(configDir);
        }


        public override void ResetAR()
        {
            base.ResetAR();
        }

        public override void StopAR()
        {
            base.StopAR();
        }

        /// <summary>
        /// 销毁物体
        /// </summary>
        public void Close()
        {

        }

        /// <summary>
        /// 人体回调
        /// 所有非挂起回掉，所有仅识别场景，无跟踪
        /// </summary>
        /// <param name="result"></param>
        public void RecognitionBodyCallback(InsightARRecognizedResult result)
        {
            //EventNotification en = new EventNotification(null, true, result);
            //EventManager.Instance.SendEventNotification(NotificationType.BODY_RECOGNITION, en);

#if UNITY_EDITOR
            result.type = InsightARClassifiedType.InsightARClassifiedType2dImage;
            result.recognizedResult = "testdemo";
#endif
            InsightDebug.Log(TAG, result.type.ToString() + "_" + result.recognizedResult);
            if (Insight.Tracking.moduleObject_Recong != null && Insight.Tracking.funcObjcet_Recong != null)
            {
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, Insight.Tracking.moduleObject_Recong.heapPtr, Insight.Tracking.funcObjcet_Recong.heapPtr,
                    result.type.ToString(), result.recognizedResult);
            }
            else {
                InsightDebug.Log(TAG, "recongnize callback is not designed in content!");
            }
        }

        /// <summary>
        /// 检查是否可用
        /// </summary>
        /// <returns></returns>
        public int CheckNavigatorStatus()
        {
#if UNITY_EDITOR
            return 0;
#else
          return  NaviSceneManager.Instance.CheckNavigatorStatus();
#endif
        }

        /// <summary>
        /// 返回所有的POI点
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string QueryMapPoiList(int type)
        {
#if UNITY_EDITOR
            return "{\"poiSum\":22,\"mapPoILists\":[{\"identifierMap\":\"\",\"osmIdentifier\":\"-101343\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22893935, 30.24450769],\"realSpaceCoords\":[-13.65067893, -3.60000000, 9.03597189]},\"propertiesSum\":4,\"properties\":{\"type\":\"POI\",\"id\":\"cc235295c5cf4488b4a38f2fb0826fa8\",\"name\":\"内容组\",\"level\":\"1\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101340\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22886400, 30.24449951],\"realSpaceCoords\":[-12.82297718, -3.60000000, 1.77007600]},\"propertiesSum\":4,\"properties\":{\"name\":\"系统开发组\",\"level\":\"1\",\"type\":\"POI\",\"id\":\"b6253b96a801a5635f70d7d73d342181\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101339\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22883062, 30.24450492],\"realSpaceCoords\":[-13.45755829, -3.60000000, -1.43686940]},\"propertiesSum\":4,\"properties\":{\"type\":\"POI\",\"id\":\"a1a534bc1a09f391a73a37cfe2cd9feb\",\"name\":\"移动开发组\",\"level\":\"1\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101327\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22859995, 30.24419320],\"realSpaceCoords\":[20.87119588, -3.60000000, -24.02489903]},\"propertiesSum\":6,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"a190fd2e59eb8cffca63a98296958274\",\"type\":\"POI\",\"name\":\"西南沙发区\",\"level\":\"1\",\"geometry_type\":\"Point\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101325\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22865397, 30.24452813],\"realSpaceCoords\":[-16.21718914, -3.60000000, -18.41834644]},\"propertiesSum\":7,\"properties\":{\"level\":\"1\",\"geometry_type\":\"Point\",\"entrance\":\"main\",\"name\":\"西门1\",\"type\":\"POI\",\"id\":\"fdb2ebf17cbc45c396bca0b56333296f\",\"x_name_radius\":\"5\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101337\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22876294, 30.24451590],\"realSpaceCoords\":[-14.74671942, -3.60000000, -7.94047740]},\"propertiesSum\":4,\"properties\":{\"name\":\"市场、商务组1\",\"level\":\"1\",\"type\":\"POI\",\"id\":\"92cf51980d4e6bb8d9e05109399f8413\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101341\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22887200, 30.24451875],\"realSpaceCoords\":[-14.94777660, -3.60000000, 2.56388187]},\"propertiesSum\":4,\"properties\":{\"name\":\"产品项管组\",\"level\":\"1\",\"type\":\"POI\",\"id\":\"b4fc45cfae6c1c22cfeea59c8a2608ec\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101335\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22875831, 30.24451349],\"realSpaceCoords\":[-14.48401995, -2.85000000, -8.38895524]},\"propertiesSum\":15,\"properties\":{\"level\":\"1\",\"height\":\"0.75\",\"x_content_type\":\"ar_product\",\"direction\":\"190.8\",\"name\":\"2D跟踪-挂起算法\",\"x_content_id\":\"260\",\"x_anchor\":\"yes\",\"x_preview_content_radius\":\"3\",\"x_content_radius\":\"2\",\"type\":\"POI\",\"geometry_type\":\"Point\",\"x_content_alg_mode\":\"swap\",\"x_name_radius\":\"5\",\"x_popular\":\"3\",\"id\":\"14d4769517b93aa18b3d516ab175d1e9\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101333\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22892178, 30.24438237],\"realSpaceCoords\":[0.22967482, -2.80000000, 7.19251272]},\"propertiesSum\":15,\"properties\":{\"id\":\"ce03ddf57e05bf488762e6fec8b8a813\",\"x_popular\":\"3\",\"x_name_radius\":\"5\",\"x_content_alg_mode\":\"unchange\",\"type\":\"POI\",\"geometry_type\":\"Point\",\"x_anchor\":\"yes\",\"x_preview_content_radius\":\"3\",\"x_content_radius\":\"2\",\"x_content_id\":\"262\",\"name\":\"模型-默认算法\",\"level\":\"1\",\"height\":\"0.8\",\"x_content_type\":\"ar_product\",\"direction\":\"10.8\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101336\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22872916, 30.24452155],\"realSpaceCoords\":[-15.40892535, -3.60000000, -11.18621872]},\"propertiesSum\":4,\"properties\":{\"type\":\"POI\",\"id\":\"41d0e6dfdedc85c8539fb826753f0381\",\"name\":\"市场、商务组2\",\"level\":\"1\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101334\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22894304, 30.24449244],\"realSpaceCoords\":[-11.95570155, -2.04000000, 9.37232326]},\"propertiesSum\":15,\"properties\":{\"id\":\"623f3be45f270de2e184b07ac9286e77\",\"x_popular\":\"3\",\"x_name_radius\":\"5\",\"x_content_alg_mode\":\"overlay\",\"type\":\"POI\",\"geometry_type\":\"Point\",\"x_anchor\":\"yes\",\"x_preview_content_radius\":\"3\",\"x_content_radius\":\"2\",\"x_content_id\":\"261\",\"name\":\"2D识别-叠加算法\",\"level\":\"1\",\"height\":\"1.56\",\"x_content_type\":\"ar_product\",\"direction\":\"190.8\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101342\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22890490, 30.24451335],\"realSpaceCoords\":[-14.31416113, -3.60000000, 5.72550483]},\"propertiesSum\":4,\"properties\":{\"type\":\"POI\",\"id\":\"89e43ec39030e022cda8eabd7d8cfd16\",\"name\":\"产品组\",\"level\":\"1\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101338\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22880043, 30.24450995],\"realSpaceCoords\":[-14.04744133, -3.60000000, -4.33736146]},\"propertiesSum\":4,\"properties\":{\"type\":\"POI\",\"id\":\"fcdfc219d26fe5a90f7c84796bfce94e\",\"name\":\"QA组\",\"level\":\"1\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101328\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22897193, 30.24425341],\"realSpaceCoords\":[14.58431352, -3.60000000, 11.86490541]},\"propertiesSum\":7,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"8529a1c9afeb8a58ad966bc33390236b\",\"type\":\"POI\",\"name\":\"东门2\",\"level\":\"1\",\"geometry_type\":\"Point\",\"entrance\":\"secondary\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101326\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22890267, 30.24420587],\"realSpaceCoords\":[19.78371216, -3.60000000, 5.13844385]},\"propertiesSum\":6,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"4990e8192c36615ed2cdbf94a680d026\",\"type\":\"POI\",\"name\":\"东沙发区\",\"level\":\"1\",\"geometry_type\":\"Point\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101323\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22903105, 30.24446771],\"realSpaceCoords\":[-9.12082480, -3.60000000, 17.81677315]},\"propertiesSum\":7,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"d965c6c92d145119d2eec91085f361ef\",\"type\":\"POI\",\"name\":\"东门1\",\"level\":\"1\",\"geometry_type\":\"Point\",\"entrance\":\"secondary\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101324\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22891746, 30.24419925],\"realSpaceCoords\":[20.53263909, -3.60000000, 6.55439870]},\"propertiesSum\":6,\"properties\":{\"level\":\"1\",\"geometry_type\":\"Point\",\"name\":\"冰箱\",\"type\":\"POI\",\"id\":\"6f408fed2bf8cea1a6842f90f44b940d\",\"x_name_radius\":\"5\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-101322\",\"mapPoint\":{\"floorId\":\"1\",\"geographicCoords\":[120.22857076, 30.24422826],\"realSpaceCoords\":[16.95165454, -3.60000000, -26.79269535]},\"propertiesSum\":7,\"properties\":{\"level\":\"1\",\"geometry_type\":\"Point\",\"entrance\":\"secondary\",\"name\":\"西门2\",\"type\":\"POI\",\"id\":\"7b9c3436f2045fb52db8995402e272b7\",\"x_name_radius\":\"5\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-102064\",\"mapPoint\":{\"floorId\":\"2\",\"geographicCoords\":[120.22894476, 30.24422498],\"realSpaceCoords\":[17.70788517, 1, 9.21411879]},\"propertiesSum\":6,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"b3f96be515a167bfe300dd13f54e857f\",\"type\":\"POI\",\"name\":\"东南阳台\",\"level\":\"2\",\"geometry_type\":\"Point\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-102062\",\"mapPoint\":{\"floorId\":\"2\",\"geographicCoords\":[120.22872264, 30.24452206],\"realSpaceCoords\":[-15.47181145, 1, -11.81314912]},\"propertiesSum\":6,\"properties\":{\"level\":\"2\",\"geometry_type\":\"Point\",\"name\":\"西北阳台\",\"type\":\"POI\",\"id\":\"a08c1a26db7916e9f99cb838f30c77c2\",\"x_name_radius\":\"5\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-102065\",\"mapPoint\":{\"floorId\":\"2\",\"geographicCoords\":[120.22861497, 30.24423483],\"realSpaceCoords\":[16.26982725, 1, -22.52844592]},\"propertiesSum\":6,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"9774f99a57d962382ebbda915404d554\",\"type\":\"POI\",\"name\":\"西南阳台\",\"level\":\"2\",\"geometry_type\":\"Point\"}}, {\"identifierMap\":\"\",\"osmIdentifier\":\"-102063\",\"mapPoint\":{\"floorId\":\"2\",\"geographicCoords\":[120.22898087, 30.24450236],\"realSpaceCoords\":[-13.01610126, 1, 13.02720757]},\"propertiesSum\":6,\"properties\":{\"x_name_radius\":\"5\",\"id\":\"19eea3e37822c305428618e27f77fd70\",\"type\":\"POI\",\"name\":\"东北阳台\",\"level\":\"2\",\"geometry_type\":\"Point\"}}]}";
#else
            if (NaviSceneManager.Instance.CheckNavigatorStatus() == 0) {
                return NaviSceneManager.Instance.QueryPOIList(type);
            }
            return "";
#endif
        }

        /// <summary>
        /// 查询路径点
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string QueryNavigationPath(string input, int type)
        {
            //#if UNITY_EDITOR
            //            return NavigationUtility.CreateNavPathData();
            //#else
            //if (NaviSceneManager.Instance.CheckNavigatorStatus() == 0)
            //{
            //    return NaviSceneManager.Instance.QueryNavigInfos(input, type);
            //}
            return "";
            //#endif
        }

        /// <summary>
        /// 处理在挂起事件下的云定位状态重置
        /// </summary>
        public void ResetLandmarkerStatus() {
            cloudLocation.CloudLocAlgCode = 16;
            cloudLocation.CloudLocAlgReason = "";
            cloudLocation.CloudSuccessCount = 0;
        }

    }
}