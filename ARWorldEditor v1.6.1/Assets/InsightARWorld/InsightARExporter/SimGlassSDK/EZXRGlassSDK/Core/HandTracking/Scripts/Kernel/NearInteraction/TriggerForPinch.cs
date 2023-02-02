using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 用于触发近距离交互的基础检测区域，位于拇指指尖和食指指尖中间，像近距离捏住后进行移动、旋转、缩放等操作都是以这个区域的检测为基准
    /// </summary>
    public class TriggerForPinch : MonoBehaviour
    {
        public HandInfo handInfo;
        public HandTouch objectGrab;
        public RotateUseHandle rotateUseHandle;
        public ScaleUseCorner scaleUseCorner;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            #region 设置触发区的位置为拇指和食指指尖中心，长度也和两个指尖的距离相关
            Vector3 thumbTip;
            Vector3 indexTip;
            if (handInfo.handExist)
            {
                thumbTip = handInfo.GetJointData(HandJointType.Thumb_3).position;
                indexTip = handInfo.GetJointData(HandJointType.Index_4).position;
            }
            else
            {
                thumbTip = new Vector3(9999, 9999, 9999);
                indexTip = new Vector3(10000, 10000, 10000);
            }
            transform.position = (thumbTip + indexTip) / 2.0f;
            transform.localScale = new Vector3(0.01f, 0.01f, (thumbTip - indexTip).magnitude + 0.02f);//食指和拇指指尖的碰撞检测区域的长度要完全覆盖食指和拇指的球（为了可以用手指指尖做单指触碰）
            transform.LookAt(indexTip);
            #endregion
        }

        /// <summary>
        /// 配置拇指和食指之间触发区的起点和终点（通常就是食指指尖点和拇指指尖点）
        /// </summary>
        /// <param name="joint0"></param>
        /// <param name="joint1"></param>
        /// <param name="handRoot">用于获取所属的手的信息</param>
        public void SetUp(Transform handRoot)
        {
            handInfo = handRoot.GetComponent<HandInfo>();
            objectGrab = handRoot.GetComponent<HandTouch>();
            rotateUseHandle = handRoot.GetComponent<RotateUseHandle>();
            scaleUseCorner = handRoot.GetComponent<ScaleUseCorner>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != gameObject.layer)
            {
                if (handInfo != null)
                {
                    //“当用户的手是up状态”且“当前手没有进入其他Trigger”时
                    if (!handInfo.isPinching /*&& !handInfo.isCloseContacting*/)
                    {
                        if (other.tag == "SpacialObject")
                        {
                            //handInfo.isCloseContacting = true;
                            handInfo.CurCloseContactingTarget = other;

                            objectGrab.ForOnTriggerEnter(other);
                        }
                        else if (other.tag == "SpacialHandler")
                        {
                            rotateUseHandle.ForOnTriggerEnter(other);
                            scaleUseCorner.ForOnTriggerEnter(other);
                        }
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != gameObject.layer)
            {
                if (handInfo != null)
                {
                    //“当用户的手是up状态”且“当前手没有进入其他Trigger”时
                    if (!handInfo.isPinching /*&& !handInfo.isCloseContacting*/)
                    {
                        if (other.tag == "SpacialObject")
                        {
                            //handInfo.isCloseContacting = true;
                            handInfo.CurCloseContactingTarget = other;

                            objectGrab.ForOnTriggerStay(other);
                        }
                        else if (other.tag == "SpacialHandler")
                        {
                            rotateUseHandle.ForOnTriggerStay(other);
                            scaleUseCorner.ForOnTriggerStay(other);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != gameObject.layer)
            {
                if (handInfo != null)
                {
                    //没有进行抓取操作的时候，通过这里进行一些状态重置
                    if (!handInfo.isPinching)
                    {
                        if (other.tag == "SpacialObject")
                        {
                            if (handInfo.CurCloseContactingTarget == other)
                            {
                                handInfo.CurCloseContactingTarget = null;
                            }

                            Debug.Log("InteractWithFingerTip exit: " + other.name);
                            objectGrab.ForOnTriggerExit(other);
                        }
                        else if (other.tag == "SpacialHandler")
                        {
                            rotateUseHandle.ForOnTriggerExit(other);
                            scaleUseCorner.ForOnTriggerExit(other);
                        }
                    }
                }
            }
        }
    }
}