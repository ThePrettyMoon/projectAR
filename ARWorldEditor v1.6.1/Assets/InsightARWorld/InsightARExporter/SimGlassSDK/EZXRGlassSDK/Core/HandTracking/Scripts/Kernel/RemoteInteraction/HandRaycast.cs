using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.UI;
using UnityEngine.EventSystems;
using EZXR.Glass.Common;
using Wheels.Unity;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 远距离射线交互
    /// </summary>
    [RequireComponent(typeof(HandInfo))]
    public class HandRaycast : MonoBehaviour
    {
        /// <summary>
        /// 射线起点的prefab
        /// </summary>
        GameObject prefab_RayPoint_Start;
        /// <summary>
        /// 射线终止点的prefab
        /// </summary>
        GameObject prefab_RayPoint_End;
        GameObject prefab_RayPoint_End_Virtual;
        /// <summary>
        /// 射线的线的prefab
        /// </summary>
        GameObject prefab_RayLine;
        /// <summary>
        /// 记录了手的所有信息，包括当前有没有捏合，有没有握持物体等等
        /// </summary>
        HandInfo handInfo;
        /// <summary>
        /// 手射线起止点。0是起点（是prefab_RayPoint的实例），1是射线终点（是一个EmptyObject，实际不会绘制出来），2是贝塞尔曲线后的射线终点（是prefab_RayPoint的实例）
        /// </summary>
        Transform[] rayPoints = new Transform[3];
        Renderer[] rayPointsRenderer = new Renderer[2];
        /// <summary>
        /// 左右手的射线
        /// </summary>
        HandRayDrawer rayLine;
        ///// <summary>
        ///// 旋转握把和缩放角所在的Layer，可以被射线射到但是不会被射线拖动
        ///// </summary>
        //public LayerMask layerMask_Handler;
        ///// <summary>
        ///// ARUI的Layer，可以被射线射到但是不会被射线拖动
        ///// </summary>
        //public LayerMask layerMask_ARUI;
        /// <summary>
        /// 射线
        /// </summary>
        Ray ray;
        /// <summary>
        /// 射线碰撞返回信息
        /// </summary>
        RaycastHit hitInfo;
        /// <summary>
        /// 用户手指没有捏合的时候等于hitInfo.distance，当用户手指捏合之后会根据手的距离远近来实现拉近拉远的操作（手射线起始点的localPosition.z<半个胳膊的长度的话hitDistance=hitInfo.distance，半个胳膊到一个胳膊的距离内会有一个线性的增长）
        /// </summary>
        float hitDistance;
        float lastRayPointDistance;
        /// <summary>
        /// 在物体被射线捏住的时刻，射线击中到的点和击中物体的坐标原点的偏移量
        /// </summary>
        Vector3 hitOffset;
        /// <summary>
        /// 在物体被射线捏住的时刻，射线击中到的点在被击中物体的坐标系下的坐标
        /// </summary>
        Vector3 hitPointLocal;
        /// <summary>
        /// 击中的面的法线转换到被击中物体的坐标系下
        /// </summary>
        Vector3 hitPointNormalLocal;
        /// <summary>
        /// 在物体被射线捏住的时刻，物体在相机父物体下的localRotation，用于确保物体被射线抓取并移动过程中始终保持被射线抓取时的旋转角度
        /// </summary>
        Quaternion hitLocalRotation;
        /// <summary>
        /// 左手射线起始点在被抓物体坐标系下的坐标
        /// </summary>
        static Vector3 leftRaypointInLocalSpace;
        /// <summary>
        /// 在抓取时刻被抓取物体的rotation，用于双手旋转物体
        /// </summary>
        static Quaternion hitRotation;
        /// <summary>
        /// 在物体被射线捏住的时刻，右手射线起始点向左手射线起始点做向量，用于计算两手的旋转四元数
        /// </summary>
        static Vector3 hitDirection;
        /// <summary>
        /// 在物体被射线捏住的时刻，物体在相机父物体下的localScale，用于双手缩放
        /// </summary>
        static Vector3 hitLocalScale;
        /// <summary>
        /// 在物体被双手射线捏住的时刻，双手射线起始点的距离
        /// </summary>
        static float distanceOfTwoRaypoint;
        /// <summary>
        /// 射线击中了物体
        /// </summary>
        bool getHit;
        /// <summary>
        /// 状态量，用户没捏合手指的时候为false，刚捏合手指的时候为true，主要用于用户刚捏合了手指的时刻记录一些数据，比如捏合时刻的hitOffset和hitLocalRotation
        /// </summary>
        bool beginDrag;

        private void Awake()
        {
            handInfo = GetComponent<HandInfo>();

            prefab_RayPoint_Start = ResourcesManager.Load<GameObject>("Ray/RayPoint_Start");
            prefab_RayPoint_End_Virtual = ResourcesManager.Load<GameObject>("Ray/RayPoint_End_Virtual");
            prefab_RayPoint_End = ResourcesManager.Load<GameObject>("Ray/RayPoint_End");
            prefab_RayLine = ResourcesManager.Load<GameObject>("Ray/RayLineRenderer");

            //实例化2个射线点（起点和终点）
            rayPoints[0] = Instantiate(prefab_RayPoint_Start, transform).transform;
            rayPoints[1] = Instantiate(prefab_RayPoint_End_Virtual, transform).transform;
            rayPoints[2] = Instantiate(prefab_RayPoint_End, transform).transform;

            ////获得射线点的Renderer以在不需要显示射线的时候关闭Renderer
            //rayPointsRenderer[0] = rayPoints[0].GetComponent<Renderer>();
            //rayPointsRenderer[1] = rayPoints[1].GetComponent<Renderer>();

            //绘制射线
            rayLine = Instantiate(prefab_RayLine, transform).GetComponent<HandRayDrawer>();
            rayLine.name = "Line";
            rayLine.SetUp(handInfo, rayPoints[0], rayPoints[1]);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnDisable()
        {
            handInfo.IsRayGrabbing = false;
            //通知handInfo当前射线检测到的物体
            handInfo.SetCurRayContactingTarget(null);
            rayLine.SetLineState(false);
            getHit = false;
        }

        private void FixedUpdate()
        {
            //如果算法发来的射线数据不为空，并且当前手心不是面向用户方向的
            if (handInfo.isValid)
            {
                //构造一条UnityEngine.Ray，用于进行射线检测
                ray.origin = handInfo.rayPoint_Start;
                ray.direction = handInfo.rayDirection;

                //设置射线的起点球位置
                rayPoints[0].position = handInfo.rayPoint_Start + 0.1f * ray.direction;

                if (handInfo.isPinching) //只要用户捏合了手指就显示实线
                {
                    rayLine.SetLineState(true);
                }
                else //如果用户没有捏合手指，则用Physics.Raycast来判断有没有射击到物体
                {
                    getHit = false;

                    handInfo.CurRayGrabbingTarget = null;

                    //如果手的预触发区内有物体（是手的大触发区，不是手指尖触发区），就隐藏射线及射线起点球和终点球，将射线的终点球位置设置为算法提供的位置
                    if (handInfo.preCloseContacting || handInfo.isCloseContacting)
                    {
                        //隐藏射线起始点、终止点和线
                        rayPoints[0].gameObject.SetActive(false);
                        rayPoints[2].gameObject.SetActive(false);
                        rayLine.gameObject.SetActive(false);
                        handInfo.enableRayInteraction = false;

                        //通知handInfo当前射线检测到的物体
                        handInfo.SetCurRayContactingTarget(null);
                    }
                    else
                    {
                        handInfo.IsRayGrabbing = false;
                        rayLine.SetLineState(false);

                        getHit = Physics.Raycast(ray, out hitInfo);

                        //显示射线起始点、终止点和线
                        //rayPoints[0].gameObject.SetActive(true);
                        rayPoints[2].gameObject.SetActive(true);
                        rayLine.gameObject.SetActive(true);
                        handInfo.enableRayInteraction = true;
                    }
                }

                //如果Physics.Raycast射击到物体（这里如果用户在一个物体上捏合了手指，那么这个getHit就始终为true，直到用户松开手指）
                if (getHit)
                {
                    if (!handInfo.isPinching)
                    {
                        hitDistance = hitInfo.distance + 0.01f;

                        //物体坐标系下的偏移量
                        hitPointLocal = hitInfo.transform.InverseTransformPoint(hitInfo.point);

                        hitPointNormalLocal = hitInfo.transform.InverseTransformDirection(hitInfo.normal).normalized;

                        handInfo.rayPoint_End.position = hitInfo.point + hitInfo.normal * 0.001f;
                        handInfo.rayPoint_End.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);

                        //通知handInfo当前射线检测到的物体
                        if (hitInfo.transform.tag == "SpacialObject" || hitInfo.transform.tag == "SpacialUI" || hitInfo.transform.tag == "SpacialHandler")
                        {
                            handInfo.SetCurRayContactingTarget(hitInfo.collider, hitInfo.transform.tag == "SpacialUI");
                        }

                        if (handInfo.IsRayPressDown)
                        {
                            handInfo.IsRayPressDown = false;
                            ExecuteEvents.Execute(hitInfo.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
                            ExecuteEvents.Execute(hitInfo.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                            ExecuteEvents.Execute(hitInfo.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.deselectHandler);
                        }
                        else
                        {
                            if (handInfo.lastPointingObject != hitInfo.transform)
                            {
                                if (handInfo.lastPointingObject != null)
                                {
                                    ExecuteEvents.Execute(handInfo.lastPointingObject.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                                }
                                ExecuteEvents.Execute(hitInfo.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);

                                handInfo.lastPointingObject = hitInfo.transform;
                            }
                        }

                        ////如果射击到的点和发射点相距0.2米以上，显示射线起点球和终点球以及射线
                        //if (hitInfo.distance > 0.2f)
                        //{
                        //    rayPointsRenderer[0].enabled = true;
                        //    rayPointsRenderer[1].enabled = true;
                        //    rayLine.gameObject.SetActive(true);
                        //    handInfo.showRay = true;
                        //}
                        //else//如果射击到的点和发射点相距0.2米以下，隐藏射线起点球和终点球以及射线
                        //{
                        //    rayPointsRenderer[0].enabled = false;
                        //    rayPointsRenderer[1].enabled = false;
                        //    rayLine.gameObject.SetActive(false);
                        //    handInfo.showRay = false;
                        //}
                    }
                    else//如果用户当前捏合了食指和拇指
                    {
                        //如果射线是可见的才认为当前是可以用射线进行交互的，才会处理用户的捏合动作
                        if (handInfo.enableRayInteraction)
                        {
                            //算出射线的终点
                            handInfo.rayPoint_End.position = ray.origin + hitDistance * ray.direction;

                            if (!handInfo.IsRayGrabbing)//开始拖动前的数据初始化（相当于是RayDragStart）
                            {
                                //射线打到的点在物体坐标系下的偏移量
                                hitPointLocal = hitInfo.transform.InverseTransformPoint(hitInfo.point);

                                hitPointNormalLocal = hitInfo.transform.InverseTransformDirection(hitInfo.normal).normalized;

                                //如果射线打到的是普通物体
                                if (hitInfo.transform.tag == "SpacialObject")
                                {
                                    //用户当前正在射线拖拽的物体（非旋转手柄或者缩放角）
                                    handInfo.CurRayGrabbingTarget = hitInfo.transform;

                                    handInfo.IsRayGrabbing = true;

                                    lastRayPointDistance = Vector3.Distance(handInfo.rayPoint_Start, ARHandManager.head.position);

                                    //算出射线击中到的点和击中物体的坐标点的偏移量
                                    hitOffset = hitInfo.transform.position - hitInfo.point;

                                    //将目标物体当前的坐标点给出，用于双手操作物体
                                    if (handInfo.handType == HandType.Left)
                                    {
                                        HandInfo.targetObjectPosition_Left = hitInfo.point + hitOffset;
                                    }
                                    else
                                    {
                                        HandInfo.targetObjectPosition_Right = hitInfo.point + hitOffset;
                                    }

                                    //得到击中物体在相机坐标系下的localRotation，后面无论单手还是双手旋转都是基于此值
                                    Quaternion parentRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(ARHandManager.head.forward, Vector3.up), Vector3.up);
                                    hitLocalRotation = hitInfo.transform.GetLocalRotation(parentRotation);

                                    if (HandInfo.isTwoHandRayGrabbing)
                                    {
                                        //得到左手射线点在物体坐标系下的坐标，用于物体位置的计算
                                        leftRaypointInLocalSpace = hitInfo.transform.InverseTransformPoint(ARHandManager.leftHand.rayPoint_Start);

                                        //用于双手旋转物体，得到物体被捏住的时刻，右手射线起始点向左手射线起始点做向量，用于计算两手的旋转四元数
                                        hitDirection = (ARHandManager.leftHand.rayPoint_Start) - (ARHandManager.rightHand.rayPoint_Start);

                                        //用于双手缩放物体，得到击中物体的localScale，后面的双手缩放就是基于此值
                                        hitLocalScale = hitInfo.transform.localScale;

                                        //用于双手缩放，得到物体被捏住时刻左右手射线起始点的距离，后面缩放都是基于此值
                                        distanceOfTwoRaypoint = (ARHandManager.leftHand.rayPoint_Start - ARHandManager.rightHand.rayPoint_Start).magnitude;
                                    }
                                }
                                else
                                {
                                    if (!handInfo.IsRayPressDown)
                                    {
                                        handInfo.IsRayPressDown = true;

                                        handInfo.curPressingObject = hitInfo.transform;

                                        ExecuteEvents.Execute(hitInfo.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                                    }
                                }
                            }
                            else//这里是真正的拖动逻辑（相当于是RayDragUpdate）
                            {
                                //如果射线打到的是普通物体
                                if (hitInfo.transform.tag == "SpacialObject")
                                {
                                    //双手操作：如果当前双手在操作同一个物体的话，物体的位置放在左手逻辑中来计算（避免在两个手的逻辑中各计算一次）
                                    if (HandInfo.isTwoHandRayGrabbing)
                                    {
                                        if (handInfo.handType == HandType.Left)
                                        {
                                            //为了避免松开一只手的时候物体瞬间移动，所以要重新算出hitOffset和lastRayPointDistance
                                            hitOffset = hitInfo.transform.position - handInfo.rayPoint_End.position;
                                            lastRayPointDistance = Vector3.Distance(handInfo.rayPoint_Start, ARHandManager.head.position);

                                            //计算缩放
                                            float dis = Vector3.Distance(ARHandManager.leftHand.rayPoint_Start, ARHandManager.rightHand.rayPoint_Start);
                                            hitInfo.transform.localScale = dis / distanceOfTwoRaypoint * hitLocalScale;

                                            //计算旋转
                                            Vector3 newHitDirection = ARHandManager.leftHand.rayPoint_Start - ARHandManager.rightHand.rayPoint_Start;
                                            Quaternion q = Quaternion.FromToRotation(hitDirection, newHitDirection);
                                            hitDirection = newHitDirection;
                                            //设置目标物体的旋转
                                            hitInfo.transform.rotation = q * hitInfo.transform.rotation;

                                            //设置目标物体的位置（必须先计算rotation才能计算position），通过左手射线起始点的当前位置和刚捏住的时候得到的“物体坐标点到左手射线起始点的射线”来算出物体的当前坐标点
                                            //hitInfo.transform.position = ARHandManager.Instance.handInfo_Left.rayPoint_Start - (hitInfo.transform.TransformPoint(leftRaypointInLocalSpace) - hitInfo.transform.position);

                                            //刷新左手射线
                                            //temp = new NativeSwapManager.Point3(HandInfo.targetObjectPosition_Left);
                                            //NativeSwapManager.filterPoint(ref temp, hitInfo.transform.GetInstanceID() + 1);//这里+1是为了避免两个手以及物体自身这三者的滤波冲突（因为是控制的同一个物体，得到的InstanceID是一样的）
                                            //tarPos = new Vector3(temp.x, temp.y, temp.z);
                                            rayLine.SetLineBezierPoint(hitInfo.transform.TransformPoint(hitPointLocal));//
                                        }
                                        else
                                        {
                                            //为了避免松开一只手的时候物体瞬间移动，所以要重新算出hitOffset
                                            hitOffset = hitInfo.transform.position - handInfo.rayPoint_End.position;
                                            lastRayPointDistance = Vector3.Distance(handInfo.rayPoint_Start, ARHandManager.head.position);

                                            //刷新右手射线
                                            NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(HandInfo.targetObjectPosition_Right);
                                            NativeSwapManager.filterPoint(ref temp, hitInfo.transform.GetInstanceID() + 2);//这里避免两个手以及物体自身这三者的滤波冲突，因为是控制的同一个物体，得到的InstanceID是一样的
                                            Vector3 tarPos = new Vector3(temp.x, temp.y, temp.z);
                                            rayLine.SetLineBezierPoint(hitInfo.transform.TransformPoint(hitPointLocal));//
                                        }
                                    }
                                    else//单手操作走这里
                                    {
                                        float distanceFromHeadToRaystart = Vector3.Distance(handInfo.rayPoint_Start, ARHandManager.head.position);

                                        //用于拉近拉远物体
                                        float dx = distanceFromHeadToRaystart - lastRayPointDistance;

                                        //给物体目标坐标点加滤波，这样物体就会平滑移动到目标坐标点
                                        NativeSwapManager.Point3 tempDX = new NativeSwapManager.Point3(1, 1, distanceFromHeadToRaystart);
                                        NativeSwapManager.filterPoint(ref tempDX, hitInfo.transform.GetInstanceID() - 1);
                                        dx = tempDX.z - lastRayPointDistance;

                                        //距离越远拉近的速度越快
                                        if (dx > 0)//handInfo.rayPoint_Start.magnitude > hitHandLength 
                                        {
                                            hitDistance += (4 * (hitInfo.transform.position - handInfo.rayPoint_Start).magnitude + 1f) * dx;// + hitDistanceInit + (handInfo.rayPoint_Start.magnitude - hitHandLength) * 5
                                        }
                                        else
                                        {
                                            hitDistance += (4 * (hitInfo.transform.position - handInfo.rayPoint_Start).magnitude + 0.5f) * dx;// hitDistanceInit + (handInfo.rayPoint_Start.magnitude - hitHandLength) * 1;
                                        }
                                        lastRayPointDistance = tempDX.z;

                                        //重新设置射线的终点球为射线碰撞到的点的位置
                                        handInfo.rayPoint_End.position = ray.origin + hitDistance * ray.direction;

                                        //给物体目标坐标点加滤波，这样物体就会平滑移动到目标坐标点
                                        NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(handInfo.rayPoint_End.position/* + handInfo.rayPoint_End_OffsetForTwoHand*/ + hitOffset);
                                        NativeSwapManager.filterPoint(ref temp, hitInfo.transform.GetInstanceID());
                                        Vector3 tarPos = new Vector3(temp.x, temp.y, temp.z);

                                        hitInfo.transform.position = tarPos;
                                        Quaternion q = Quaternion.LookRotation(Vector3.ProjectOnPlane(ARHandManager.head.forward, Vector3.up), Vector3.up);//Main.Instance.transform.rotation
                                                                                                                                                           //hitInfo.transform.rotation = q * hitLocalRotation;

                                        rayLine.SetLineBezierPoint(hitInfo.transform.TransformPoint(hitPointLocal));
                                    }
                                }
                                else
                                {
                                    //如果射到的是ARUI
                                    if (hitInfo.transform.tag == "SpacialUI")
                                    {
                                        //重新设置射线的终点球为射线碰撞到的物体的位置
                                        handInfo.rayPoint_End.position = hitInfo.transform.position;
                                    }
                                    rayLine.SetLineBezierPoint(hitInfo.transform.TransformPoint(hitPointLocal));
                                }
                            }
                        }
                    }
                }
                else
                {
                    //直接将射线终止点设置到最远端
                    handInfo.rayPoint_End.position = handInfo.rayDirection * 0.5f + handInfo.rayPoint_Start;
                    rayPoints[2].gameObject.SetActive(false);
                    handInfo.rayPoint_End.rotation = Quaternion.identity;

                    //通知handInfo当前射线检测到的物体
                    handInfo.SetCurRayContactingTarget(null);

                    if (handInfo.lastPointingObject != null)
                    {
                        ExecuteEvents.Execute(handInfo.lastPointingObject.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(handInfo.lastPointingObject.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.deselectHandler);
                        ExecuteEvents.Execute(handInfo.lastPointingObject.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                        handInfo.lastPointingObject = null;
                    }
                }

                rayPoints[1].position = handInfo.rayPoint_End.position;
                rayPoints[1].rotation = handInfo.rayPoint_End.rotation;

                rayPoints[2].position = getHit ? (hitInfo.transform.TransformPoint(hitPointLocal) + hitInfo.transform.TransformDirection(hitPointNormalLocal) * 0.001f) : handInfo.rayPoint_End.position;
                rayPoints[2].rotation = getHit ? Quaternion.FromToRotation(Vector3.forward, hitInfo.transform.TransformDirection(hitPointNormalLocal)) : Quaternion.identity;
            }
            else
            {
                OnDisable();
            }
        }

    }
}