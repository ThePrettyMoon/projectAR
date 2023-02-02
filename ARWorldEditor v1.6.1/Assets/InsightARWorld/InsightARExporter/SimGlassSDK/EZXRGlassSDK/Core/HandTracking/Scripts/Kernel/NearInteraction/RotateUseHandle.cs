using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    [RequireComponent(typeof(HandInfo))]
    public class RotateUseHandle : MonoBehaviour
    {
        HandInfo handInfo;
        TransformWithBounds transformUseBounds;
        /// <summary>
        /// 射线上次射击到的Collider，用于将射线结果转化成OnTriggerStay和OnTriggerExit
        /// </summary>
        Collider lastRayCastResult = null;

        //public LayerMask layerMask;
        /// <summary>
        /// 要被旋转的目标物体
        /// </summary>
        Transform target;
        /// <summary>
        /// 当前控制的是哪个旋转手柄
        /// </summary>
        Transform curHandle;
        /// <summary>
        /// 旋转手柄被捏住一瞬间的手柄的position
        /// </summary>
        Vector3 curHandlePos;
        /// <summary>
        /// 旋转手柄被捏住一瞬间，手柄与射线起始点的偏移量
        /// </summary>
        Vector3 offset;
        /// <summary>
        /// 旋转手柄被捏住一瞬间的localRotation
        /// </summary>
        Quaternion curTargetRotation;
        /// <summary>
        /// 用户是否捏合了手指（等于按住鼠标）
        /// </summary>
        bool press;
        /// <summary>
        /// 抓取状态，0为没有手进入此物体的Trigger区域，1为手已经进入了Trigger区域（待抓取），2为已经被左手抓起来了
        /// </summary>
        int grabState;
        /// <summary>
        /// 当前握住的手柄所在平面的法线
        /// </summary>
        Vector3 normal;
        /// <summary>
        /// 目标物体中心到射线发射点的向量在手柄所在平面的投影向量
        /// </summary>
        Vector3 projectedPos;
        /// <summary>
        /// 用于计算手柄所在平面所需要的另外一个点，连同物体中心点这三个点才能组成一个平面
        /// </summary>
        Vector3 anotherPoint;

        // Start is called before the first frame update
        void Start()
        {
            handInfo = GetComponent<HandInfo>();
            //用于得到当前手的射线检测的结果
            handInfo.Event_GetRayCastResult += OnRayCastHit;
        }

        private void OnDisable()
        {
            grabState = 0;
            if (curHandle != null)
            {
                curHandle.GetComponent<Renderer>().material.color = Color.white;
                curHandle = null;
            }
            target = null;
        }

        // Update is called once per frame
        void Update()
        {
            switch (grabState)
            {
                case 1:
                    if (handInfo.isPinching)
                    {
                        curHandlePos = curHandle.position;
                        //算出旋转手柄被捏住一瞬间，手柄与射线起始点的偏移量
                        offset = curHandle.position - handInfo.rayPoint_Start;
                        curTargetRotation = target.localRotation;
                        //得到当前手柄与target以及同轴面上另外一个手柄所在平面的法线
                        normal = GetPlaneNormal();
                        grabState = 2;
                    }
                    break;
                case 2:
                    if (handInfo.isPinching)
                    {
                        //算出手柄的实时位置，要加上刚被握住时候的偏移量
                        NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(handInfo.rayPoint_Start + offset);
                        NativeSwapManager.filterPoint(ref temp, curHandle.GetInstanceID());
                        projectedPos = target.position + Vector3.ProjectOnPlane(new Vector3(temp.x, temp.y, temp.z) - target.position, normal);
                        target.localRotation = Quaternion.FromToRotation(curHandlePos - target.position, projectedPos - target.position) * curTargetRotation;
                    }
                    else
                    {
                        grabState = 0;
                        curHandle.GetComponent<Renderer>().material.color = Color.white;
                        curHandle = null;
                        target = null;
                    }
                    break;
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawCube(anotherPoint, Vector3.one * 0.05f);
        //    if (target != null)
        //    {
        //        Gizmos.DrawLine(target.position, curHandlePos);
        //        Gizmos.DrawLine(target.position, target.position + normal);
        //        Gizmos.DrawLine(target.position, projectedPos);
        //    }
        //}

        /// <summary>
        /// 获得射线检测结果，转化成OnTriggerStay和OnTriggerExit
        /// </summary>
        public void OnRayCastHit(Collider other, bool isUI)
        {
            if (enabled)
            {
                //触发和OnTriggerExit基本一致的逻辑
                if (lastRayCastResult != other)
                {
                    if (lastRayCastResult != null)
                    {
                        //OnTriggerExit(lastRayCastResult);
                        if (!handInfo.isPinching)
                        {
                            if (other != null && other.tag == "SpacialHandler" && lastRayCastResult.name == "RotateHandle(Clone)")
                            {
                                grabState = 0;
                                if (curHandle != null)
                                {
                                    curHandle.GetComponent<Renderer>().material.color = Color.white;
                                }
                                curHandle = null;
                                target = null;
                            }
                        }
                    }
                    lastRayCastResult = other;
                }

                //触发和OnTriggerStay基本一致的逻辑
                if (other != null)
                {
                    if (!handInfo.isPinching && !handInfo.isCloseContacting)
                    {
                        if (other.tag == "SpacialHandler" && other.name == "RotateHandle(Clone)")
                        {
                            grabState = 1;
                            curHandle = other.transform;
                            curHandle.GetComponent<Renderer>().material.color = Color.red;
                            //设置要被旋转的目标物体
                            target = other.transform.parent;
                            transformUseBounds = target.GetComponent<TransformWithBounds>();
                        }
                    }
                }
            }
        }

        public void ForOnTriggerEnter(Collider other)
        {
            ForOnTriggerStay(other);
        }

        public void ForOnTriggerStay(Collider other)
        {
            if (enabled && other.name == "RotateHandle(Clone)")
            {
                grabState = 1;
                curHandle = other.transform;
                curHandle.GetComponent<Renderer>().material.color = Color.red;
                //设置要被旋转的目标物体
                target = other.transform.parent;
                transformUseBounds = target.GetComponent<TransformWithBounds>();
            }
        }

        public void ForOnTriggerExit(Collider other)
        {
            if (enabled && other.name == "RotateHandle(Clone)")
            {
                grabState = 0;
                curHandle.GetComponent<Renderer>().material.color = Color.white;
                curHandle = null;
                target = null;
            }
        }

        /// <summary>
        /// 得到当前手柄与target以及同轴面上另外一个手柄所在平面的法线
        /// </summary>
        /// <returns></returns>
        Vector3 GetPlaneNormal()
        {
            bool gotIt = false;
            anotherPoint = Vector3.zero;
            for (int i = 0; i < transformUseBounds.rotate_X.Length; i++)
            {
                if (transformUseBounds.rotate_X[i] == curHandle)
                {
                    gotIt = true;
                    if (i < 3)
                    {
                        anotherPoint = transformUseBounds.rotate_X[i + 1].position;
                    }
                    else
                    {
                        anotherPoint = transformUseBounds.rotate_X[i - 1].position;
                    }
                }
            }
            if (!gotIt)
            {
                for (int i = 0; i < transformUseBounds.rotate_Y.Length; i++)
                {
                    if (transformUseBounds.rotate_Y[i] == curHandle)
                    {
                        gotIt = true;
                        if (i < 3)
                        {
                            anotherPoint = transformUseBounds.rotate_Y[i + 1].position;
                        }
                        else
                        {
                            anotherPoint = transformUseBounds.rotate_Y[i - 1].position;
                        }
                    }
                }
            }
            if (!gotIt)
            {
                for (int i = 0; i < transformUseBounds.rotate_Z.Length; i++)
                {
                    if (transformUseBounds.rotate_Z[i] == curHandle)
                    {
                        gotIt = true;
                        if (i < 3)
                        {
                            anotherPoint = transformUseBounds.rotate_Z[i + 1].position;
                        }
                        else
                        {
                            anotherPoint = transformUseBounds.rotate_Z[i - 1].position;
                        }
                    }
                }
            }
            if (gotIt)
            {
                return Vector3.Cross(curHandlePos - target.position, anotherPoint - target.position);
            }
            else
            {
                return anotherPoint;
            }
        }
    }
}