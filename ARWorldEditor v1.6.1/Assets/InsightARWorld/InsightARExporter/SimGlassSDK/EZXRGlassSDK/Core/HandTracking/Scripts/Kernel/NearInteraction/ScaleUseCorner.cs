using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public class ScaleUseCorner : MonoBehaviour
    {
        HandInfo handInfo;
        /// <summary>
        /// 射线上次射击到的Collider，用于将射线结果转化成OnTriggerStay和OnTriggerExit
        /// </summary>
        Collider lastRayCastResult = null;

        //public LayerMask layerMask;
        /// <summary>
        /// 要被缩放的目标物体
        /// </summary>
        Transform target;
        Vector3 curTargetScale;
        /// <summary>
        /// 当前控制的是哪个缩放角
        /// </summary>
        Transform curCorner;
        /// <summary>
        /// 缩放角被捏住一瞬间的position
        /// </summary>
        Vector3 curCornerPos;
        /// <summary>
        /// 缩放角被捏住一瞬间，缩放角与射线起始点的偏移量
        /// </summary>
        Vector3 offset;
        /// <summary>
        /// 缩放角被捏住一瞬间，缩放角和物体坐标点的距离
        /// </summary>
        float curCornerLength;
        /// <summary>
        /// 抓取状态，0为没有手进入此物体的Trigger区域，1为手已经进入了Trigger区域（待抓取），2为已经被左手抓起来了
        /// </summary>
        int grabState;
        /// <summary>
        /// 缩放倍率
        /// </summary>
        float scaleFactor;
        Vector3 projectedPos;

        // Start is called before the first frame update
        void Start()
        {
            handInfo = GetComponent<HandInfo>();
            handInfo.Event_GetRayCastResult += OnRayCastHit;
        }

        private void OnDisable()
        {
            grabState = 0;
            if (curCorner != null)
            {
                curCorner.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                curCorner.GetChild(1).GetComponent<Renderer>().material.color = Color.white;
                curCorner.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
                curCorner = null;
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
                        curCornerPos = curCorner.position;
                        //缩放角被捏住一瞬间，缩放角与射线起始点的偏移量
                        offset = curCorner.position - handInfo.rayPoint_Start;
                        curCornerLength = (curCornerPos - target.position).magnitude;
                        curTargetScale = target.localScale;
                        grabState = 2;
                    }
                    break;
                case 2:
                    if (handInfo.isPinching)
                    {
                        NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(handInfo.rayPoint_Start + offset);
                        NativeSwapManager.filterPoint(ref temp, curCorner.GetInstanceID());
                        //射线发射点-目标物体得到的向量在被拽住的缩放角-目标物体得到的向量上进行投影并得到投影长度，除以刚捏住时刻缩放角与目标物体的距离长度，得到缩放系数
                        projectedPos = Vector3.Project(new Vector3(temp.x, temp.y, temp.z) - target.position, curCornerPos - target.position);
                        scaleFactor = projectedPos.magnitude / curCornerLength;
                        target.localScale = scaleFactor * curTargetScale;
                    }
                    else
                    {
                        grabState = 0;
                        if (curCorner != null)
                        {
                            curCorner.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                            curCorner.GetChild(1).GetComponent<Renderer>().material.color = Color.white;
                            curCorner.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
                            curCorner = null;
                        }
                        target = null;
                    }
                    break;
            }
            //Main.Instance.textMesh.text = "grabState: " + grabState + "，isTouching: ";
        }

        private void OnDrawGizmos()
        {
            if (target != null)
            {
                Gizmos.DrawLine(target.position, projectedPos);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(target.position, curCornerPos);
                //Gizmos.DrawLine(target.position, transform.position);
            }
        }

        /// <summary>
        /// 获得射线检测结果，转化成OnTriggerStay和OnTriggerExit
        /// </summary>
        public void OnRayCastHit(Collider other, bool isUI)
        {
            //触发和OnTriggerExit基本一致的逻辑
            if (lastRayCastResult != other)
            {
                if (lastRayCastResult != null)
                {
                    //OnTriggerExit(lastRayCastResult);
                    if (!handInfo.isPinching)
                    {
                        if (other != null && other.tag == "SpacialHandler" && lastRayCastResult.name == "ScaleCorner(Clone)")
                        {
                            grabState = 0;
                            if (curCorner != null)
                            {
                                curCorner.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                                curCorner.GetChild(1).GetComponent<Renderer>().material.color = Color.white;
                                curCorner.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
                            }
                            curCorner = null;
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
                    if (other.tag == "SpacialHandler" && other.name == "ScaleCorner(Clone)")
                    {
                        grabState = 1;
                        curCorner = other.transform;
                        curCorner.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                        curCorner.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        curCorner.GetChild(2).GetComponent<Renderer>().material.color = Color.red;
                        //设置要被缩放的目标物体
                        target = other.transform.parent;
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
            if (enabled && other.tag == "SpacialHandler" && other.name == "ScaleCorner(Clone)")
            {
                grabState = 1;
                curCorner = other.transform;
                curCorner.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                curCorner.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                curCorner.GetChild(2).GetComponent<Renderer>().material.color = Color.red;
                //设置要被缩放的目标物体
                target = other.transform.parent;
            }
        }

        public void ForOnTriggerExit(Collider other)
        {
            if (enabled && other.tag == "SpacialHandler" && other.name == "ScaleCorner(Clone)")
            {
                grabState = 0;
                if (curCorner != null)
                {
                    curCorner.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                    curCorner.GetChild(1).GetComponent<Renderer>().material.color = Color.white;
                    curCorner.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
                    curCorner = null;
                }
                target = null;
            }
        }
    }
}