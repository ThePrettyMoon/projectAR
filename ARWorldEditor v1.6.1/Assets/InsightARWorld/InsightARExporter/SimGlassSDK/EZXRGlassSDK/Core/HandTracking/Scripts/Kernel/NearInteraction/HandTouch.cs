using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 近距离触碰交互
    /// </summary>
    [RequireComponent(typeof(HandInfo))]
    public class HandTouch : MonoBehaviour
    {
        /// <summary>
        /// 食指指尖圆环的Prefab
        /// </summary>
        GameObject prefab_IndexRing;
        IndexRing indexRing;
        ///// <summary>
        ///// 旋转握把和缩放角所在的Layer，可以被射线射到但是不会被射线拖动
        ///// </summary>
        //public LayerMask layerMask_Handler;
        ///// <summary>
        ///// ARUI的Layer，可以被射线射到但是不会被射线拖动
        ///// </summary>
        //public LayerMask layerMask_ARUI;
        HandInfo handInfo;
        /// <summary>
        /// 抓取状态，0为拇指和食指中间的Trigger区域没有碰到任何物体，1为拇指和食指中间的Trigger区域触碰到了物体（待抓取），2为已经被手抓起来了
        /// </summary>
        int grabState;
        /// <summary>
        /// 当前手正在Trigger的物体
        /// </summary>
        Transform touchingObj;
        /// <summary>
        /// 子物体在父物体下的LocalRotation
        /// </summary>
        Quaternion q_UnderPalm;
        /// <summary>
        /// 子物体在父物体坐标系下的LocalPosition
        /// </summary>
        Vector3 p_UnderPalm;

        #region 双手操作
        /// <summary>
        /// 左手射线起始点在被抓物体坐标系下的坐标，用于双手操作
        /// </summary>
        static Vector3 leftRaypointInLocalSpace;
        /// <summary>
        /// 在物体被捏住的时刻，右手射线起始点向左手射线起始点做向量，用于计算两手的旋转四元数
        /// </summary>
        static Vector3 hitDirection;
        /// <summary>
        /// 在物体被捏住的时刻，物体在相机父物体下的localScale，用于双手缩放
        /// </summary>
        static Vector3 hitLocalScale;
        /// <summary>
        /// 在物体被双手捏住的时刻，双手射线起始点的距离
        /// </summary>
        static float distanceOfTwoRaypoint;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            handInfo = GetComponent<HandInfo>();

            //显示食指指尖圆环
            prefab_IndexRing = ResourcesManager.Load<GameObject>("Touch/IndexRing");
            indexRing = Instantiate(prefab_IndexRing, transform).GetComponent<IndexRing>();
            indexRing.SetUp(handInfo);
        }

        private void OnDisable()
        {
            handInfo.CurCloseGrabbingTarget = null;
            handInfo.IsCloseGrabbing = false;
            grabState = 0;
            indexRing.SetStatus(IndexRingStatus.Ring);
            if (handInfo.CurCloseContactingTarget != null)
            {
                handInfo.CurCloseContactingTarget = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //当前眼镜视野中是否存在手
            if (handInfo.handExist)
            {
                if (handInfo.isPinching)//手指是捏合状态
                {
                    switch (grabState)
                    {
                        case 1://手正在触碰物体
                            handInfo.CurCloseGrabbingTarget = touchingObj;
                            handInfo.IsCloseGrabbing = true;
                            ////如果当前握持的是普通物体，不是旋转手柄或者缩放角
                            //if (1 << touchingObj.gameObject.layer != layerMask_Handler.value && 1 << touchingObj.gameObject.layer != layerMask_ARUI.value)
                            {
                                grabState = 2;

                                indexRing.SetStatus(IndexRingStatus.Solid);

                                //取食指指尖位置和食指挨着指尖的关节的位置的中心为捏合点
                                handInfo.grabLocalPoint = touchingObj.InverseTransformPoint((handInfo.GetJointData(HandJointType.Thumb_3).position + handInfo.GetJointData(HandJointType.Index_4).position) / 2.0f);

                                //得到子物体在父物体下的LocalRotation
                                q_UnderPalm = new Quaternion(-handInfo.palm.rotation.x, -handInfo.palm.rotation.y, -handInfo.palm.rotation.z, handInfo.palm.rotation.w) * touchingObj.rotation;
                                //得到子物体在父物体坐标系下的LocalPosition
                                p_UnderPalm = handInfo.palm.InverseTransformPoint(touchingObj.position);

                                if (HandInfo.isTwoHandCloseGrabbing)
                                {
                                    //得到左手射线点在物体坐标系下的坐标，用于物体位置的计算
                                    leftRaypointInLocalSpace = touchingObj.InverseTransformPoint(ARHandManager.leftHand.rayPoint_Start);

                                    //用于双手旋转，得到物体被捏住的时刻，右手射线起始点向左手射线起始点做向量，用于计算两手的旋转四元数
                                    hitDirection = (ARHandManager.leftHand.rayPoint_Start) - (ARHandManager.rightHand.rayPoint_Start);

                                    //用于双手缩放，得到击中物体的localScale，后面缩放都是基于此值
                                    hitLocalScale = touchingObj.localScale;

                                    //用于双手缩放，得到物体被捏住时刻左右手射线起始点的距离，后面缩放都是基于此值
                                    distanceOfTwoRaypoint = Vector3.Distance(ARHandManager.leftHand.rayPoint_Start, ARHandManager.rightHand.rayPoint_Start);
                                }
                            }
                            break;
                        case 2://手正在执行拖拽
                               ////如果当前握持的是普通物体，不是旋转手柄或者缩放角
                               //if (1 << touchingObj.gameObject.layer != layerMask_Handler.value && 1 << touchingObj.gameObject.layer != layerMask_ARUI.value)
                            {
                                //双手操作：如果当前双手在操作同一个物体的话，物体的位置放在左手逻辑中来计算（避免在两个手的逻辑中各计算一次）
                                if (HandInfo.isTwoHandCloseGrabbing)
                                {
                                    if (handInfo.handType == HandType.Left)
                                    {
                                        //计算缩放
                                        float dis = Vector3.Distance(ARHandManager.leftHand.rayPoint_Start, ARHandManager.rightHand.rayPoint_Start);
                                        touchingObj.localScale = dis / distanceOfTwoRaypoint * hitLocalScale;

                                        //计算旋转
                                        Vector3 newHitDirection = ARHandManager.leftHand.rayPoint_Start - ARHandManager.rightHand.rayPoint_Start;
                                        Quaternion q = Quaternion.FromToRotation(hitDirection, newHitDirection);
                                        hitDirection = newHitDirection;
                                        //设置目标物体的旋转
                                        touchingObj.rotation = q * touchingObj.rotation;

                                        //设置目标物体的位置（必须先计算rotation才能计算position），通过左手射线起始点的当前位置和刚捏住的时候得到的“物体坐标点到左手射线起始点的射线”来算出物体的当前坐标点
                                        touchingObj.position = ARHandManager.leftHand.rayPoint_Start - (touchingObj.TransformPoint(leftRaypointInLocalSpace) - touchingObj.position);
                                    }

                                    //得到子物体在父物体下的LocalRotation
                                    q_UnderPalm = new Quaternion(-handInfo.palm.rotation.x, -handInfo.palm.rotation.y, -handInfo.palm.rotation.z, handInfo.palm.rotation.w) * touchingObj.rotation;
                                    //得到子物体在父物体坐标系下的LocalPosition
                                    p_UnderPalm = handInfo.palm.InverseTransformPoint(touchingObj.position);
                                }
                                else
                                {
                                    ////给手的位置加滤波
                                    //NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(handInfo.rayPoint_Start + grabOffset);
                                    //NativeSwapManager.filterPoint(ref temp, touchingObj.GetInstanceID());
                                    //Vector3 tarPos = new Vector3(temp.x, temp.y, temp.z);

                                    touchingObj.rotation = handInfo.palm.rotation * q_UnderPalm;
                                    touchingObj.position = handInfo.palm.position + handInfo.palm.rotation * p_UnderPalm;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    //Debug.Log("HandTouch - handInfo.isPinching: " + handInfo.isPinching + ", " + grabState);
                    //根据拇指食指触碰物体的情况来决定圆环的显示与否
                    indexRing.gameObject.SetActive(handInfo.preCloseContacting | handInfo.isCloseContacting);

                    handInfo.CurCloseGrabbingTarget = null;
                    handInfo.IsCloseGrabbing = false;
                    if (grabState == 2)
                    {
                        //Debug.Log("HandTouch - grabState: " + grabState);
                        grabState = 0;
                        indexRing.SetStatus(IndexRingStatus.Ring);
                        touchingObj = null;
                    }
                    //if (handInfo.CurCloseContactingTarget == null)
                    //{
                    //    handInfo.isCloseContacting = false;
                    //}
                }
            }
            else
            {
                OnDisable();
            }
        }

        public void ForOnTriggerEnter(Collider other)
        {
            ForOnTriggerStay(other);
        }

        public void ForOnTriggerStay(Collider other)
        {
            if (enabled && !handInfo.isPinching)
            {
                grabState = 1;
                indexRing.SetStatus(IndexRingStatus.Ring);
                touchingObj = other.transform;
            }
        }

        public void ForOnTriggerExit(Collider other)
        {
            if (enabled && !handInfo.isPinching)
            {
                handInfo.CurCloseGrabbingTarget = null;
                grabState = 0;
                indexRing.SetStatus(IndexRingStatus.Ring);
                touchingObj = null;
            }
        }
    }
}