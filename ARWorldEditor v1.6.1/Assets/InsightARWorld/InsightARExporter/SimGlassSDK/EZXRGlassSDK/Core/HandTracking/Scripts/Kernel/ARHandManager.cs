using EZXR.Glass.Common;
using EZXR.Glass.SixDof;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    [ScriptExecutionOrder(-8)]
    public class ARHandManager : MonoBehaviour
    {
        #region singleton
        private static ARHandManager _instance;
        public static ARHandManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        ///// <summary>
        ///// 是否显示球棍手
        ///// </summary>
        //public bool ShowHandJoint = true;

        /// <summary>
        /// 头部坐标系所在的物体
        /// </summary>
        public static Transform head;
        /// <summary>
        /// 实例化出来作为所有手的根节点
        /// </summary>
        GameObject prefab_handRoot;
        /// <summary>
        /// 记录了左手的所有信息，包括当前有没有捏合，有没有握持物体等等
        /// </summary>
        public static HandInfo leftHand;
        /// <summary>
        /// 记录了右手的所有信息，包括当前有没有捏合，有没有握持物体等等
        /// </summary>
        public static HandInfo rightHand;


        private void Awake()
        {
            //single instance guarantee
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }

            //cross scene available
            DontDestroyOnLoad(gameObject);

            gameObject.AddComponent<NativeSwapManager>();

            //注册回调，以得到HandTrackingData的数据更新
            NativeSwapManager.OnHandTrackingDataUpdated += OnHandTrackingDataUpdated;

            //if (ShowHandJoint)
            //{
            //    gameObject.AddComponent<HandsVisualization>();
            //}

            head = HMDPoseTracker.Instance.Head;

            prefab_handRoot = ResourcesManager.Load<GameObject>("HandRoot");
            Transform handRoot = Instantiate(prefab_handRoot).transform;
            handRoot.parent = transform;
            Transform handRoot_Left = handRoot.Find("HandRoot_Left");
            Transform handRoot_Right = handRoot.Find("HandRoot_Right");

            //初始化HandInfo，以指定HandInfo是左手还是右手
            leftHand = handRoot_Left.GetComponent<HandInfo>();
            leftHand.Init(HandType.Left);
            rightHand = handRoot_Right.GetComponent<HandInfo>();
            rightHand.Init(HandType.Right);

            //拇指和食指之间用来触发近距离捏合的区域
            GameObject prefab_TriggerBetweenDaddyAndMommyFinger = ResourcesManager.Load<GameObject>("Triggers/TriggerBetweenDaddyAndMommyFinger");
            TriggerForPinch interactWithFingerTip_Left = Instantiate(prefab_TriggerBetweenDaddyAndMommyFinger, leftHand.root).GetComponent<TriggerForPinch>();
            interactWithFingerTip_Left.SetUp(leftHand.root);
            TriggerForPinch interactWithFingerTip_Right = Instantiate(prefab_TriggerBetweenDaddyAndMommyFinger, rightHand.root).GetComponent<TriggerForPinch>();
            interactWithFingerTip_Right.SetUp(rightHand.root);

            //手部的预触发区，用于切换近距离和远距离交互
            GameObject prefab_TriggerForFarNearSwitch = ResourcesManager.Load<GameObject>("Triggers/TriggerForFarNearSwitch");
            TriggerForFarNearSwitch triggerForFarNearSwitch_Left = Instantiate(prefab_TriggerForFarNearSwitch, leftHand.root).GetComponent<TriggerForFarNearSwitch>();
            triggerForFarNearSwitch_Left.SetUp(leftHand.root);
            TriggerForFarNearSwitch triggerForFarNearSwitch_Right = Instantiate(prefab_TriggerForFarNearSwitch, rightHand.root).GetComponent<TriggerForFarNearSwitch>();
            triggerForFarNearSwitch_Right.SetUp(rightHand.root);
        }

        private void OnHandTrackingDataUpdated(NativeSwapManager.HandTrackingData[] handTrackingData)
        {
            NativeSwapManager.HandTrackingData handTrackingData_Left, handTrackingData_Right;

            handTrackingData_Left = handTrackingData[0];
            handTrackingData_Right = handTrackingData[1];

            //开启或关闭虚拟手物体（直接关闭手GameObject的原因是当手不存在的时候不应该执行任何逻辑）
            leftHand.root.gameObject.SetActive(handTrackingData_Left.isTracked);
            rightHand.root.gameObject.SetActive(handTrackingData_Right.isTracked);

            //刷新手的数据
            leftHand.UpdateHandInfoData(handTrackingData_Left);
            rightHand.UpdateHandInfoData(handTrackingData_Right);
        }

        public HandInfo GetHand(HandType handedness)
        {
            if (handedness == HandType.Left)
            {
                return leftHand;
            }
            else
            {
                return rightHand;
            }
        }
    }
}