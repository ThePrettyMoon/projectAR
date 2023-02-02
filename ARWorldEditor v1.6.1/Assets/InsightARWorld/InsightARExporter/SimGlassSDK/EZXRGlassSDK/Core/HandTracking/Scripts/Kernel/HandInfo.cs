using EZXR.Glass.Common;
using EZXR.Glass.SixDof;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public enum HandType
    {
        Left,
        Right,
    }

    public enum GestureType
    {
        Unknown = -1,
        OpenHand = 0,
        Grab,
        Pinch,
        Point,
        Victory,
        Call,
    }

    public enum HandJointType
    {
        Thumb_0,
        Thumb_1,
        Thumb_2,
        /// <summary>
        /// 拇指尖
        /// </summary>
        Thumb_3,
        /// <summary>
        /// 食指根节点
        /// </summary>
        Index_1,
        Index_2,
        Index_3,
        /// <summary>
        /// 食指尖
        /// </summary>
        Index_4,
        /// <summary>
        /// 中指根节点
        /// </summary>
        Middle_1,
        Middle_2,
        Middle_3,
        /// <summary>
        /// 中指指尖
        /// </summary>
        Middle_4,
        Ring_1,
        Ring_2,
        Ring_3,
        /// <summary>
        /// 无名指指尖
        /// </summary>
        Ring_4,
        Pinky_0,
        /// <summary>
        /// 小指根节点
        /// </summary>
        Pinky_1,
        Pinky_2,
        Pinky_3,
        /// <summary>
        /// 小指指尖
        /// </summary>
        Pinky_4,
        /// <summary>
        /// 掌心点
        /// </summary>
        Palm,
        /// <summary>
        /// 手腕横切线，靠近拇指根节点的点
        /// </summary>
        Wrist_Thumb,
        /// <summary>
        /// 手腕横切线，靠近小指根节点的点
        /// </summary>
        Wrist_Pinky,
        /// <summary>
        /// 手腕横切线，中点
        /// </summary>
        Wrist_Middle,
    }


    /// <summary>
    /// 包含单个手的各种状态信息，是个基础信息类，供其他类进行状态获取和协同
    /// </summary>
    [ScriptExecutionOrder(-9)]
    public class HandInfo : MonoBehaviour
    {
        /// <summary>
        /// 手的所有关节等物体的父节点
        /// </summary>
        public Transform root
        {
            get
            {
                return transform;
            }
        }

        /// <summary>
        /// 射线碰到Collider的时候触发此事件
        /// </summary>
        public event Action<Collider, bool> Event_GetRayCastResult;

        #region 当前预触碰的物体信息
        /// <summary>
        /// 在手的近距离交互预判区内是否存在物体，只是在手的触发区域内但是还没有碰到食指指尖（主要用于避免在手即将触摸到旋转握把的时候会出现射线射到另外一个握把或者缩放角）
        /// </summary>
        public bool preCloseContacting;
        /// <summary>
        /// 手部近距离交互预判区（TriggerForFarNearSwitch中定义区域大小，此区域内则认为用户要近距离抓取）内存在的距离最近的物体
        /// </summary>
        Transform preCloseContactingTarget;
        /// <summary>
        /// 手部近距离交互预判区内存在的距离最近的物体
        /// </summary>
        public Transform PreCloseContactingTarget
        {
            get
            {
                return preCloseContactingTarget;
            }
            set
            {
                //if (preCloseContactingTarget != value)
                {
                    //记录上一帧食指指尖的近距离交互预判区内存在的距离最近的物体
                    Last_PreCloseContactingTarget = preCloseContactingTarget;

                    preCloseContactingTarget = value;
                    preCloseContactingTarget_Renderer = (preCloseContactingTarget == null ? null : preCloseContactingTarget.GetComponent<Renderer>());
                    if (preCloseContactingTarget_Renderer != null)
                    {
                        if (preCloseContactingTarget_PropertyBlock == null)
                        {
                            preCloseContactingTarget_PropertyBlock = new MaterialPropertyBlock();
                        }
                        preCloseContactingTarget_Renderer.GetPropertyBlock(preCloseContactingTarget_PropertyBlock);
                    }

                    if (preCloseContactingTarget == null)
                    {
                        preCloseContacting = false;
                    }
                    else
                    {
                        preCloseContacting = true;
                    }
                }
            }
        }
        public Renderer preCloseContactingTarget_Renderer;
        public MaterialPropertyBlock preCloseContactingTarget_PropertyBlock;

        /// <summary>
        /// 上一帧手部近距离交互预判区内存在的距离最近的物体
        /// </summary>
        Transform last_PreCloseContactingTarget;
        /// <summary>
        /// 上一帧手部近距离交互预判区内存在的距离最近的物体
        /// </summary>
        public Transform Last_PreCloseContactingTarget
        {
            get
            {
                return last_PreCloseContactingTarget;
            }
            set
            {
                if (last_PreCloseContactingTarget != value)
                {
                    last_PreCloseContactingTarget = value;
                    last_PreCloseContactingTarget_Renderer = (last_PreCloseContactingTarget == null ? null : last_PreCloseContactingTarget.GetComponent<Renderer>());
                    if (last_PreCloseContactingTarget_Renderer != null)
                    {
                        if (last_PreCloseContactingTarget_PropertyBlock == null)
                        {
                            last_PreCloseContactingTarget_PropertyBlock = new MaterialPropertyBlock();
                        }
                        last_PreCloseContactingTarget_Renderer.GetPropertyBlock(last_PreCloseContactingTarget_PropertyBlock);
                    }
                }
            }
        }
        public Renderer last_PreCloseContactingTarget_Renderer;
        public MaterialPropertyBlock last_PreCloseContactingTarget_PropertyBlock;
        #endregion

        #region 当前正在触碰的物体信息
        /// <summary>
        /// 指示当前射线是否正射在某个物体上
        /// </summary>
        public bool isRayContacting;
        /// <summary>
        /// 当前射线碰到的Collider（仅仅是射线碰到，并不是射线抓取）
        /// </summary>
        private Collider curRayContactingTarget;
        public Collider CurRayContactingTarget
        {
            get
            {
                return curRayContactingTarget;
            }
            set
            {
                if (value == null)
                {
                    isRayContacting = false;
                }
                else
                {
                    isRayContacting = true;
                }

                //if (curRayContactingTarget != value)
                {
                    //记录上一帧被射线打到的物体的相关信息
                    LastRayContactingTarget = curRayContactingTarget;

                    curRayContactingTarget = value;

                    if (curRayContactingTarget != null)
                    {
                        curRayContactingTarget_Renderer = (curRayContactingTarget == null ? null : curRayContactingTarget.GetComponent<Renderer>());
                        if (curRayContactingTarget_Renderer != null)
                        {
                            if (curRayContactingTarget_PropertyBlock == null)
                            {
                                curRayContactingTarget_PropertyBlock = new MaterialPropertyBlock();
                            }
                            curRayContactingTarget_Renderer.GetPropertyBlock(curRayContactingTarget_PropertyBlock);
                        }

                        if (curRayContactingTarget_Renderer != null && curRayContactingTarget_PropertyBlock != null)
                        {
                            curRayContactingTarget_PropertyBlock.SetVector("_HitWorldPos", Vector3.one * 1000);
                            curRayContactingTarget_Renderer.SetPropertyBlock(curRayContactingTarget_PropertyBlock);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 由HandRaycast调用此处来设置当前射线射击到的目标
        /// </summary>
        /// <param name="other">射线打到的Collider</param>
        /// <param name="isUI">射线打到的是否是UI</param>
        public void SetCurRayContactingTarget(Collider other, bool isUI = false)
        {
            if (CurRayContactingTarget != null && other != null)
            {
                if (CurRayContactingTarget != other)
                {
                    SpacialObject.PerformOnHandRayExit(CurRayContactingTarget);
                    SpacialObject.PerformOnHandRayEnter(other);
                }
                else
                {
                    SpacialObject.PerformOnHandRayStay(other);
                }
            }
            else if (CurRayContactingTarget == null && other != null)
            {
                SpacialObject.PerformOnHandRayEnter(other);
            }
            else if (CurRayContactingTarget != null && other == null)
            {
                SpacialObject.PerformOnHandRayExit(CurRayContactingTarget);
            }

            CurRayContactingTarget = other;

            if (Event_GetRayCastResult != null)
            {
                //将射线检测结果分发出
                Event_GetRayCastResult(other, isUI);
            }
        }
        public Renderer curRayContactingTarget_Renderer;
        public MaterialPropertyBlock curRayContactingTarget_PropertyBlock;

        /// <summary>
        /// 射线上一次碰到的Collider
        /// </summary>
        private Collider lastRayContactingTarget;
        public Collider LastRayContactingTarget
        {
            get
            {
                return lastRayContactingTarget;
            }
            set
            {
                if (lastRayContactingTarget != value)
                {
                    lastRayContactingTarget = value;
                    if (lastRayContactingTarget != null)
                    {
                        lastRayContactingTarget_Renderer = (lastRayContactingTarget == null ? null : lastRayContactingTarget.GetComponent<Renderer>());
                        if (lastRayContactingTarget_Renderer != null)
                        {
                            if (lastRayContactingTarget_PropertyBlock == null)
                            {
                                lastRayContactingTarget_PropertyBlock = new MaterialPropertyBlock();
                            }
                            lastRayContactingTarget_Renderer.GetPropertyBlock(lastRayContactingTarget_PropertyBlock);
                        }
                    }
                }
            }
        }
        public Renderer lastRayContactingTarget_Renderer;
        public MaterialPropertyBlock lastRayContactingTarget_PropertyBlock;

        /// <summary>
        /// 当前射线碰到的Collider的点
        /// </summary>
        public Vector3 curRayContactingPoint;


        /// <summary>
        /// 指示手是否正在近距离触碰某个物体（被拇指和食指指尖的连线穿过）
        /// </summary>
        public bool isCloseContacting;

        Collider curCloseContactingTarget;
        /// <summary>
        /// 当前正在被近距离触碰的物体
        /// </summary>
        public Collider CurCloseContactingTarget
        {
            get
            {
                return curCloseContactingTarget;
            }
            set
            {
                if (curCloseContactingTarget != null && value != null)
                {
                    if (curCloseContactingTarget != value)
                    {
                        SpacialObject.PerformOnHandTriggerExit(curCloseContactingTarget);
                        SpacialObject.PerformOnHandTriggerEnter(curCloseContactingTarget);
                    }
                    else
                    {
                        SpacialObject.PerformOnHandTriggerStay(value);
                    }
                }
                else if (curCloseContactingTarget == null && value != null)
                {
                    SpacialObject.PerformOnHandTriggerEnter(value);
                }
                else if (curCloseContactingTarget != null && value == null)
                {
                    SpacialObject.PerformOnHandTriggerExit(curCloseContactingTarget);
                }

                curCloseContactingTarget = value;
                if (curCloseContactingTarget == null)
                {
                    isCloseContacting = false;
                }
                else
                {
                    isCloseContacting = true;
                }
            }
        }

        /// <summary>
        /// 指示手是否正在近距离触碰某个物体或者射线是否正射在某个物体上
        /// </summary>
        public bool isContacting
        {
            get
            {
                return isCloseContacting | isRayContacting;
            }
        }
        /// <summary>
        /// 当前正在被触碰的物体（无论射线还是近距离）
        /// </summary>
        public Collider CurContactingTarget
        {
            get
            {
                if (CurCloseContactingTarget == null)
                {
                    if (curRayContactingTarget != null)
                    {
                        return curRayContactingTarget;
                    }
                }
                else
                {
                    return CurCloseContactingTarget;
                }
                return null;
            }
        }
        #endregion

        #region 当前正在抓取的物体的信息
        /// <summary>
        /// 指示手正在通过射线拖拽物体
        /// </summary>
        private bool isRayGrabbing;
        /// <summary>
        /// 指示手正在通过射线拖拽物体
        /// </summary>
        public bool IsRayGrabbing
        {
            get
            {
                return isRayGrabbing;
            }
            set
            {
                isRayGrabbing = value;
                if (isRayGrabbing)
                {
                    if (((handType == HandType.Left && ARHandManager.rightHand.IsRayGrabbing) || (handType == HandType.Right && ARHandManager.leftHand.IsRayGrabbing)) && ARHandManager.leftHand.CurRayGrabbingTarget == ARHandManager.rightHand.CurRayGrabbingTarget)
                    {
                        isTwoHandRayGrabbing = true;
                    }
                }
                else
                {
                    isTwoHandRayGrabbing = false;
                }
            }
        }

        private Transform curRayGrabbingTarget;
        /// <summary>
        /// 当前射线正在抓取的物体（非旋转手柄或者缩放角）
        /// </summary>
        public Transform CurRayGrabbingTarget
        {
            get
            {
                return curRayGrabbingTarget;
            }
            set
            {
                if (curRayGrabbingTarget == null && value == null)
                {
                    return;
                }

                if (curRayGrabbingTarget == null && value != null)
                {
                    SpacialObject.PerformOnHandRayGrab(value.GetComponent<Collider>());
                }
                else if (curRayGrabbingTarget != null && value == null)
                {
                    SpacialObject.PerformOnHandRayRelease(curRayGrabbingTarget.GetComponent<Collider>());
                }

                curRayGrabbingTarget = value;
            }
        }

        /// <summary>
        /// 指示手正在通过近距离抓取的方式拖拽物体
        /// </summary>
        private bool isCloseGrabbing;
        /// <summary>
        /// 指示手正在通过近距离抓取的方式拖拽物体
        /// </summary>
        public bool IsCloseGrabbing
        {
            get
            {
                return isCloseGrabbing;
            }
            set
            {
                isCloseGrabbing = value;
                if (isCloseGrabbing)
                {
                    if (((handType == HandType.Left && ARHandManager.rightHand.IsCloseGrabbing) || (handType == HandType.Right && ARHandManager.leftHand.IsCloseGrabbing)) && ARHandManager.leftHand.CurCloseGrabbingTarget == ARHandManager.rightHand.CurCloseGrabbingTarget)
                    {
                        isTwoHandCloseGrabbing = true;
                    }
                }
                else
                {
                    isTwoHandCloseGrabbing = false;
                }
            }
        }

        private Transform curCloseGrabbingTarget;
        /// <summary>
        /// 当前正在近距离抓取的Transform（非旋转手柄或者缩放角）
        /// </summary>
        /// <param name="other"></param>
        public Transform CurCloseGrabbingTarget
        {
            get
            {
                return curCloseGrabbingTarget;
            }
            set
            {
                if (curCloseGrabbingTarget == null && value == null)
                {
                    return;
                }

                if (curCloseGrabbingTarget == null && value != null)
                {
                    SpacialObject.PerformOnHandTriggerGrab(value.GetComponent<Collider>());
                }
                else if (curCloseGrabbingTarget != null && value == null)
                {
                    SpacialObject.PerformOnHandTriggerRelease(curCloseGrabbingTarget.GetComponent<Collider>());
                }

                curCloseGrabbingTarget = value;
            }
        }

        /// <summary>
        /// 手当前是否正在抓取物体（无论射线还是近距离）
        /// </summary>
        public bool IsGrabbing
        {
            get
            {
                if (CurCloseGrabbingTarget == null && CurRayGrabbingTarget == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 得到当前手正在抓取的物体（无论射线还是近距离）
        /// </summary>
        public Transform CurGrabbingTarget
        {
            get
            {
                if (CurCloseGrabbingTarget == null)
                {
                    if (CurRayGrabbingTarget != null)
                    {
                        return CurRayGrabbingTarget;
                    }
                }
                else
                {
                    return CurCloseGrabbingTarget;
                }
                return null;
            }
        }
        #endregion

        #region 用于非SpacialObject物体
        public Transform lastPointingObject;
        /// <summary>
        /// 仅用于非SpacialObject物体，指示手正在通过射线点击非SpacialObject，会向这种物体发送pointerDownHandler事件，通常用于UGUI这类的点击
        /// </summary>
        public bool IsRayPressDown;
        public Transform curPressingObject;

        #endregion

        #region 单手信息
        /// <summary>
        /// 从算法得到的此手的原始关节点数据，单位为mm
        /// </summary>
        NativeSwapManager.HandTrackingData handTrackingData;
        /// <summary>
        /// 是否启用射线交互
        /// </summary>
        public bool enableRayInteraction;
        /// <summary>
        /// 手的射线起始点
        /// </summary>
        public Vector3 rayPoint_Start;
        /// <summary>
        /// 手的射线终止点
        /// </summary>
        public Pose rayPoint_End;
        /// <summary>
        /// 从射线起始点到终止点的射线方向（单位向量）
        /// </summary>
        public Vector3 rayDirection;
        /// <summary>
        /// 自行定义的一个肩部点，相对于头部坐标系的
        /// </summary>
        Vector3 shoulderPoint;
        /// <summary>
        /// 这个HandInfo是属于左手还是右手，0为左手，1为右手
        /// </summary>
        public HandType handType;
        /// <summary>
        /// 手的动作
        /// </summary>
        public GestureType gestureType
        {
            get
            {
                return handTrackingData.gestureType;
            }
        }
        /// <summary>
        /// 当前眼镜视野中是否存在这个手
        /// </summary>
        public bool handExist
        {
            get
            {
                return handTrackingData.isTracked;
            }
        }
        /// <summary>
        /// 手捏合时刻抓住的点（在被抓住的物体的局部坐标系下的坐标）
        /// </summary>
        public Vector3 grabLocalPoint;
        /// <summary>
        /// 所有手指关节点的Pose
        /// </summary>
        public Dictionary<HandJointType, Pose> jointsPose = new Dictionary<HandJointType, Pose>();
        ///// <summary>
        ///// 手腕点在世界坐标系下的坐标
        ///// </summary>
        //public Pose wristPoint;
        ///// <summary>
        ///// 拇指指尖点在世界坐标系下的坐标
        ///// </summary>
        //public Pose thumbFingerTip;
        ///// <summary>
        ///// 食指指尖点在世界坐标系下的坐标
        ///// </summary>
        //public Pose indexFingerTip;
        ///// <summary>
        ///// 食指指根点
        ///// </summary>
        //public Pose indexFingerRoot;
        ///// <summary>
        ///// 小拇指指根点
        ///// </summary>
        //public Pose pinkyRoot;
        /// <summary>
        /// 手掌面向的方向
        /// </summary>
        public Vector3 palmNormal;
        /// <summary>
        /// 手掌指尖朝向（从手腕点到中指和无名指根节点中间点的朝向，世界坐标系）
        /// </summary>
        public Vector3 palmDirection;
        public Transform palm;
        #endregion

        #region 双手信息
        /// <summary>
        /// 正在执行双手射线抓取操作
        /// </summary>
        public static bool isTwoHandRayGrabbing;
        /// <summary>
        /// 正在执行双手近距离抓取操作
        /// </summary>
        public static bool isTwoHandCloseGrabbing;
        /// <summary>
        /// 用于双手，左手当前捏住物体拖动的目标坐标点
        /// </summary>
        public static Vector3 targetObjectPosition_Left;
        /// <summary>
        /// 用于双手，右手当前捏住物体拖动的目标坐标点
        /// </summary>
        public static Vector3 targetObjectPosition_Right;
        #endregion


        /// <summary>
        /// 初始化，设置当前HandInfo所属的手是左手还是右手
        /// </summary>
        /// <param name="handType">0为左手，1为右手</param>
        public void Init(HandType handType)
        {
            this.handType = handType;

            //设定肩部点，用于射线计算
            shoulderPoint = new Vector3(handType == HandType.Left ? -0.15f : 0.15f, -0.1f, -0.1f);

            palm = new GameObject("Palm").transform;
            palm.parent = transform;
        }

        private void OnDisable()
        {
            //handExist = false;
        }

        private void Update()
        {
            //handExist = handTrackingData.isTracked;

            if (handExist)
            {
                palm.position = GetJointData(HandJointType.Palm).position;
                palm.rotation = GetJointData(HandJointType.Palm).rotation;

                //for (int i = 0; i < jointsPose.Length; i++)
                //{
                //    jointsPose[i].position = new Vector3(handTrackingData[i].x, handTrackingData[i].y, handTrackingData[i].z);
                //}
                ////手腕点
                //wristPoint.position = GetJointData(HandJointType.Wrist_Middle).position;

                ////拇指指尖点
                //thumbFingerTip.position = GetJointData(HandJointType.Thumb_3).position;

                ////食指指尖点
                //indexFingerTip.position = GetJointData(HandJointType.Index_4).position;

                ////食指指根点
                //indexFingerRoot.position = GetJointData(HandJointType.Index_1).position;

                ////小拇指指根点
                //pinkyRoot.position = GetJointData(HandJointType.Pinky_1).position;

                //掌心面向的方向
                Vector3 wristToIndexFingerRoot = GetJointData(HandJointType.Index_1).position - GetJointData(HandJointType.Wrist_Middle).position;
                Vector3 wristToPinkyRoot = GetJointData(HandJointType.Pinky_1).position - GetJointData(HandJointType.Wrist_Middle).position;
                palmNormal = (handType == HandType.Left ? 1 : -1) * Vector3.Cross(wristToIndexFingerRoot, wristToPinkyRoot).normalized;

                //手掌指尖朝向
                Vector3 middleFingerRoot = GetJointData(HandJointType.Middle_1).position;
                Vector3 ringFingerRoot = GetJointData(HandJointType.Ring_1).position;
                palmDirection = (middleFingerRoot + ringFingerRoot) / 2.0f - GetJointData(HandJointType.Wrist_Middle).position;

                //射线起点和终点
                rayPoint_Start = GetJointData(HandJointType.Index_1).position;

                ////用于双手操作，得到左手加滤波之后的掌心点位置
                //NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(rayPoint_Start);
                //NativeSwapManager.filterPoint(ref temp, transform.GetInstanceID());
                //Vector3 tarPos = new Vector3(temp.x, temp.y, temp.z);
                //rayPoint_Start_Left = tarPos;

                //从算法得到的预估的射线终点位置
                //rayPoint_End = new Vector3(ray[1].x, Main.ray[1].y, ray[1].z);

                rayDirection = (rayPoint_Start - ARHandManager.head.TransformPoint(shoulderPoint)).normalized;
            }
        }

        /// <summary>
        /// 当前手是否为有效姿态（手背面向用户为true，掌心面向用户为false）
        /// </summary>
        /// <returns></returns>
        public bool isValid
        {
            get
            {
                return handTrackingData.isTracked;
            }
        }

        /// <summary>
        /// 当前手是否在做捏合动作
        /// </summary>
        /// <returns></returns>
        public bool isPinching
        {
            get
            {
                //return testPressing;
                if (handTrackingData.isTracked && (handTrackingData.gestureType == GestureType.Pinch || handTrackingData.gestureType == GestureType.Grab))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 启用远距离射线交互
        /// </summary>
        public void EnableRaycastInteraction()
        {
            GetComponent<HandRaycast>().enabled = true;
        }
        /// <summary>
        /// 禁用远距离射线交互
        /// </summary>
        public void DisableRaycastInteraction()
        {
            GetComponent<HandRaycast>().enabled = false;
        }

        /// <summary>
        /// 启用近距离交互
        /// </summary>
        public void EnableTouchInteraction()
        {
            GetComponent<HandTouch>().enabled = true;
        }
        /// <summary>
        /// 禁用近距离交互
        /// </summary>
        public void DisableTouchInteraction()
        {
            GetComponent<HandTouch>().enabled = false;
        }

        /// <summary>
        /// 启用手部的物理碰撞
        /// </summary>
        public void EnablePhysicsInteraction()
        {
            GetComponent<HandsCollision>().SetPhysicsInteraction(true);
        }
        /// <summary>
        /// 禁用手部的物理碰撞
        /// </summary>
        public void DisablePhysicsInteraction()
        {
            GetComponent<HandsCollision>().SetPhysicsInteraction(false);
        }

        public void UpdateHandInfoData(NativeSwapManager.HandTrackingData handTrackingData)
        {
            this.handTrackingData = handTrackingData;
            if (handTrackingData.handJointData != null)
            {
                Pose pose;
                for (int i = 0; i < handTrackingData.handJointData.Length; i++)
                {
                    NativeSwapManager.Mat4f mat4 = handTrackingData.handJointData[i].handJointPose;
                    Matrix4x4 m = new Matrix4x4();
                    m.SetColumn(0, new Vector4(mat4.col0.x, mat4.col0.y, mat4.col0.z, mat4.col0.w));
                    m.SetColumn(1, new Vector4(mat4.col1.x, mat4.col1.y, mat4.col1.z, mat4.col1.w));
                    m.SetColumn(2, new Vector4(mat4.col2.x, mat4.col2.y, mat4.col2.z, mat4.col2.w));
                    m.SetColumn(3, new Vector4(mat4.col3.x, mat4.col3.y, mat4.col3.z, mat4.col3.w));
                    if (!m.ValidTRS())
                    {
                        return;
                    }
                    pose.rotation = ARBodyManager.Instance == null ? m.rotation : (ARBodyManager.Instance.transform.rotation * m.rotation);
                    Vector3 pos = m.GetColumn(3);
                    pose.position = ARBodyManager.Instance == null ? pos : (ARBodyManager.Instance.transform.TransformPoint(pos));
                    HandJointType handJointType = (HandJointType)i;
                    if (jointsPose.ContainsKey(handJointType))
                    {
                        jointsPose[handJointType] = pose;
                    }
                    else
                    {
                        jointsPose.Add(handJointType, pose);
                    }
                }
            }
        }

        public bool JointDataExist(HandJointType handJointType)
        {
            return jointsPose.ContainsKey(handJointType);
        }

        public Pose GetJointData(HandJointType handJointType)
        {
            if (jointsPose.ContainsKey(handJointType))
            {
                return jointsPose[handJointType];
            }
            return Pose.identity;
        }

        public Pose GetJointData(int handJointTypeID)
        {
            return GetJointData((HandJointType)handJointTypeID);
        }

    }
}