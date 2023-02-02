using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 可视化手部信息：关节、关节点等等
    /// </summary>
    [ScriptExecutionOrder(-8)]
    public class HandsVisualization : MonoBehaviour
    {
        #region singleton
        private static HandsVisualization _instance;
        public static HandsVisualization Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 是否显示球棍手
        /// </summary>
        public bool ShowHandJoint = true;

        /// <summary>
        /// 是否启用手部物理碰撞
        /// </summary>
        public bool enablePhysicsInteraction;

        #region 变量声明，3D球棍模型展示
        /// <summary>
        /// 这个其实是眼镜的RGB相机焦点与人眼焦点的固定偏移量，加了这个偏移之后展示的球棍还有射线等等的模型才能和真实的手贴合
        /// </summary>
        Vector3 fixedOffset = new Vector3(0, 0.025f, 0.075f);
        /// <summary>
        /// 手指关节点的prefab
        /// </summary>
        public GameObject prefab_Keypoint;
        /// <summary>
        /// 手指关节prefab的实例，每个手25个点，用于展示3d关节点，0是拇指根节点，3号是拇指指尖，7是是食指指尖
        /// </summary>
        public Transform[] keyPoints = new Transform[25];
        /// <summary>
        /// 手指关节的prefab
        /// </summary>
        public GameObject prefab_FingerBone;
        #endregion

        #region 变量声明，2D球棍显示
        /// <summary>
        /// 关节点的prefab
        /// </summary>
        GameObject prefab_Keypoint2D;
        /// <summary>
        /// 关节线的prefab
        /// </summary>
        GameObject prefab_FingerBone2D;
        #endregion

        #region 变量声明，手部射线展示
        ///// <summary>
        ///// 记录了右手的所有信息，包括当前有没有捏合，有没有握持物体等等
        ///// </summary>
        //[HideInInspector]
        //public HandInfo handInfo_Right;
        #endregion

        HandInfo handInfo;
        public Dictionary<int, Transform> fingerBones = new Dictionary<int, Transform>();

        #region 变量声明，灰度相关
        /// <summary>
        /// 左灰度相机的左手关节点的信息
        /// </summary>
        RectTransform[] keyPoints2D_LeftFish_LeftHand = new RectTransform[21];
        /// <summary>
        /// 左灰度相机的右手关节点的信息
        /// </summary>
        RectTransform[] keyPoints2D_LeftFish_RightHand = new RectTransform[21];
        /// <summary>
        /// 右灰度相机的左手关节点的信息
        /// </summary>
        RectTransform[] keyPoints2D_RightFish_LeftHand = new RectTransform[21];
        /// <summary>
        /// 右灰度相机的右手关节点的信息
        /// </summary>
        RectTransform[] keyPoints2D_RightFish_RightHand = new RectTransform[21];

        /// <summary>
        /// 左灰度相机的左手关节线的信息
        /// </summary>
        Dictionary<int, RectTransform> fingerBones2D_LeftFish_LeftHand = new Dictionary<int, RectTransform>();
        /// <summary>
        /// 左灰度相机的右手关节线的信息
        /// </summary>
        Dictionary<int, RectTransform> fingerBones2D_LeftFish_RightHand = new Dictionary<int, RectTransform>();
        /// <summary>
        /// 右灰度相机的左手关节线的信息
        /// </summary>
        Dictionary<int, RectTransform> fingerBones2D_RightFish_LeftHand = new Dictionary<int, RectTransform>();
        /// <summary>
        /// 右灰度相机的右手关节线的信息
        /// </summary>
        Dictionary<int, RectTransform> fingerBones2D_RightFish_RightHand = new Dictionary<int, RectTransform>();

        /// <summary>
        /// 左灰度相机图像上用于绘制关节点和关节线的物体
        /// </summary>
        Transform canvasRoot_Left;
        /// <summary>
        /// 右灰度相机图像上用于绘制关节点和关节线的物体
        /// </summary>
        Transform canvasRoot_Right;
        #endregion


        private void Awake()
        {
            _instance = this;

            handInfo = GetComponent<HandInfo>();

            prefab_Keypoint = ResourcesManager.Load<GameObject>("KeyPoint/KeyPoint");
            prefab_FingerBone = ResourcesManager.Load<GameObject>("FingerBone/FingerBone");
            prefab_Keypoint2D = ResourcesManager.Load<GameObject>("KeyPoint/KeyPoint_2D");
            prefab_FingerBone2D = ResourcesManager.Load<GameObject>("FingerBone/FingerBone_2D");
            //prefab_RigidbodyForCollision = ResourcesManager.Load<GameObject>("Collision/KeyPointRigidbody");
        }

        // Start is called before the first frame update
        void Start()
        {
            #region 3D展示
            //得到所有关节点的名字
            string[] jointNames = System.Enum.GetNames(typeof(HandJointType));

            //实例化所有关节点（球棍型可视化用）
            for (int i = 0; i < jointNames.Length; i++)
            {
                keyPoints[i] = Instantiate(prefab_Keypoint, handInfo.root).transform;
                keyPoints[i].name = handInfo.handType.ToString() + "_" + jointNames[i];// "IndexFingerTip";

                //食指尖
                if (i == 7)
                {
                    keyPoints[i].GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f);
                    Rigidbody leftRigidbody = keyPoints[i].gameObject.AddComponent<Rigidbody>();
                    leftRigidbody.useGravity = false;
                    leftRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                    ////只要是Art/Standard的shader的物体在被这个脚本的物体靠近的时候会产生打光的效果
                    //keyPoints_Left[i].gameObject.AddComponent<HoverLight>();
                }
            }

            //实例化骨骼
            for (int i = 0; i < jointNames.Length; i++)
            {
                if (i < 16)//小拇指之前的节点的关节实例化走这个逻辑
                {
                    if ((i + 1) % 4 > 0 /*|| (i + 1) % 4 == 0*/)
                    {
                        Transform fingerBone = Instantiate(prefab_FingerBone, handInfo.root).transform;
                        fingerBone.name = handInfo.handType.ToString() + "_" + i + "_" + i + 1;
                        //fingerBone.SetUp(keyPoints_Left[i], keyPoints_Left[i + 1]);
                        fingerBone.localScale = new Vector3(0.01f, 0.01f, (keyPoints[i].position - keyPoints[i + 1].position).magnitude);
                        fingerBones.Add(i + 1, fingerBone.transform);
                    }
                }
                else if (i < 20)//小拇指的5个节点的关节实例化走这个逻辑
                {
                    Transform fingerBone = Instantiate(prefab_FingerBone, handInfo.root).transform;
                    fingerBone.name = handInfo.handType.ToString() + "_" + i + "_" + i + 1;
                    //fingerBone.SetUp(keyPoints_Left[i], keyPoints_Left[i + 1]);
                    fingerBone.localScale = new Vector3(0.01f, 0.01f, (keyPoints[i].position - keyPoints[i + 1].position).magnitude);
                    fingerBones.Add(i + 1, fingerBone.transform);
                }
            }
            #endregion

            #region 2D展示
            //canvasRoot_Left = GameObject.Find("TextureCanvas/LeftGrayCamera").transform;
            //canvasRoot_Right = GameObject.Find("TextureCanvas/RightGrayCamera").transform;

            ////实例化21个关节点的prefab
            //for (int i = 0; i < 21; i++)
            //{
            //    //左鱼眼
            //    keyPoints2D_LeftFish_LeftHand[i] = Instantiate(prefab_Keypoint2D, canvasRoot_Left).GetComponent<RectTransform>();
            //    keyPoints2D_LeftFish_RightHand[i] = Instantiate(prefab_Keypoint2D, canvasRoot_Left).GetComponent<RectTransform>();

            //    //右鱼眼
            //    keyPoints2D_RightFish_LeftHand[i] = Instantiate(prefab_Keypoint2D, canvasRoot_Right).GetComponent<RectTransform>();
            //    keyPoints2D_RightFish_RightHand[i] = Instantiate(prefab_Keypoint2D, canvasRoot_Right).GetComponent<RectTransform>();
            //}
            //for (int i = 0; i < 21; i++)
            //{
            //    if ((i + 1) % 4 > 1 || (i + 1) % 4 == 0)
            //    {
            //        GameObject fingerBone = Instantiate(prefab_FingerBone2D, canvasRoot_Left).gameObject;
            //        fingerBone.name = "L_L_" + (i - 1) + "_" + i;
            //        fingerBones2D_LeftFish_LeftHand.Add(i, fingerBone.GetComponent<RectTransform>());
            //        if (i < 4)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.blue;
            //        }
            //        else if (i < 8)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.red;
            //        }
            //        else if (i < 12)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.cyan;
            //        }
            //        else if (i < 16)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.yellow;
            //        }
            //        else if (i < 20)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.green;
            //        }

            //        fingerBone = Instantiate(prefab_FingerBone2D, canvasRoot_Right).gameObject;
            //        fingerBone.name = "R_L_" + (i - 1) + "_" + i;
            //        fingerBones2D_RightFish_LeftHand.Add(i, fingerBone.GetComponent<RectTransform>());
            //        if (i < 4)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.blue;
            //        }
            //        else if (i < 8)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.red;
            //        }
            //        else if (i < 12)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.cyan;
            //        }
            //        else if (i < 16)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.yellow;
            //        }
            //        else if (i < 20)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.green;
            //        }

            //        fingerBone = Instantiate(prefab_FingerBone2D, canvasRoot_Left).gameObject;
            //        fingerBone.name = "L_R_" + (i - 1) + "_" + i;
            //        fingerBones2D_LeftFish_RightHand.Add(i, fingerBone.GetComponent<RectTransform>());
            //        if (i < 4)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.blue;
            //        }
            //        else if (i < 8)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.red;
            //        }
            //        else if (i < 12)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.cyan;
            //        }
            //        else if (i < 16)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.yellow;
            //        }
            //        else if (i < 20)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.green;
            //        }

            //        fingerBone = Instantiate(prefab_FingerBone2D, canvasRoot_Right).gameObject;
            //        fingerBone.name = "R_R_" + (i - 1) + "_" + i;
            //        fingerBones2D_RightFish_RightHand.Add(i, fingerBone.GetComponent<RectTransform>());
            //        if (i < 4)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.blue;
            //        }
            //        else if (i < 8)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.red;
            //        }
            //        else if (i < 12)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.cyan;
            //        }
            //        else if (i < 16)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.yellow;
            //        }
            //        else if (i < 20)
            //        {
            //            fingerBone.GetComponent<Image>().color = Color.green;
            //        }
            //    }
            //}
            #endregion

        }

        // Update is called once per frame
        void LateUpdate()
        {
            #region 3D展示
            if (handInfo.isValid)
            {
                //if (handLeft.handTrackingData.handJointData != null)
                {
                    foreach (HandJointType key in handInfo.jointsPose.Keys)
                    {
                        int id = (int)key;

                        //更新KeyPoint的Pose
                        keyPoints[id].position = handInfo.GetJointData(key).position;
                        keyPoints[id].rotation = handInfo.GetJointData(key).rotation;

                        //更新FingerBone的Pose
                        if (id > 0)
                        {
                            if (fingerBones.ContainsKey(id))
                            {
                                fingerBones[id].transform.position = keyPoints[id].position;
                                fingerBones[id].transform.localScale = new Vector3(0.01f, 0.01f, (keyPoints[id].position - keyPoints[id - 1].position).magnitude);
                                fingerBones[id].transform.LookAt(keyPoints[id - 1]);
                            }
                        }
                    }
                }
            }
            #endregion

            #region 2D展示
            //if (NativeSwapManager.iKData.hand1_valid)
            //{
            //    if (NativeSwapManager.iKData.positions1_2d != null)
            //    {
            //        for (int i = 0; i < NativeSwapManager.iKData.positions1_2d.Length; i++)
            //        {
            //            keyPoints2D_LeftFish_LeftHand[i].anchoredPosition = new Vector2(NativeSwapManager.iKData.positions1_2d[i].x, -NativeSwapManager.iKData.positions1_2d[i].y);
            //            keyPoints2D_RightFish_LeftHand[i].anchoredPosition = new Vector2(NativeSwapManager.iKData.positions3_2d[i].x, -NativeSwapManager.iKData.positions3_2d[i].y);

            //            if (fingerBones2D_LeftFish_LeftHand.ContainsKey(i - 1))
            //            {
            //                fingerBones2D_LeftFish_LeftHand[i - 1].anchoredPosition = (keyPoints2D_LeftFish_LeftHand[i - 1].anchoredPosition + keyPoints2D_LeftFish_LeftHand[i].anchoredPosition) / 2.0f;
            //                Vector2 temp0 = keyPoints2D_LeftFish_LeftHand[i - 1].anchoredPosition - keyPoints2D_LeftFish_LeftHand[i].anchoredPosition;
            //                fingerBones2D_LeftFish_LeftHand[i - 1].localRotation = Quaternion.FromToRotation(Vector2.up, temp0);
            //                fingerBones2D_LeftFish_LeftHand[i - 1].localScale = new Vector3(1, temp0.magnitude / fingerBones2D_LeftFish_LeftHand[i - 1].sizeDelta.y, 1);

            //                fingerBones2D_RightFish_LeftHand[i - 1].anchoredPosition = (keyPoints2D_RightFish_LeftHand[i - 1].anchoredPosition + keyPoints2D_RightFish_LeftHand[i].anchoredPosition) / 2.0f;
            //                temp0 = keyPoints2D_RightFish_LeftHand[i - 1].anchoredPosition - keyPoints2D_RightFish_LeftHand[i].anchoredPosition;
            //                fingerBones2D_RightFish_LeftHand[i - 1].localRotation = Quaternion.FromToRotation(Vector2.up, temp0);
            //                fingerBones2D_RightFish_LeftHand[i - 1].localScale = new Vector3(1, temp0.magnitude / fingerBones2D_RightFish_LeftHand[i - 1].sizeDelta.y, 1);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (NativeSwapManager.iKData.positions1_2d != null)
            //    {
            //        for (int i = 0; i < NativeSwapManager.iKData.positions1_2d.Length; i++)
            //        {
            //            keyPoints2D_LeftFish_LeftHand[i].anchoredPosition = new Vector2(9999, 9999);
            //            keyPoints2D_RightFish_LeftHand[i].anchoredPosition = new Vector2(9999, 9999);

            //            if (fingerBones2D_LeftFish_LeftHand.ContainsKey(i - 1))
            //            {
            //                fingerBones2D_LeftFish_LeftHand[i - 1].anchoredPosition = new Vector2(9999, 9999);
            //                fingerBones2D_RightFish_LeftHand[i - 1].anchoredPosition = new Vector2(9999, 9999);
            //            }
            //        }
            //    }
            //}

            //if (NativeSwapManager.iKData.hand2_valid)
            //{
            //    if (NativeSwapManager.iKData.positions2_2d != null)
            //    {
            //        for (int i = 0; i < NativeSwapManager.iKData.positions2_2d.Length; i++)
            //        {
            //            keyPoints2D_LeftFish_RightHand[i].anchoredPosition = new Vector2(NativeSwapManager.iKData.positions2_2d[i].x, -NativeSwapManager.iKData.positions2_2d[i].y);
            //            keyPoints2D_RightFish_RightHand[i].anchoredPosition = new Vector2(NativeSwapManager.iKData.positions4_2d[i].x, -NativeSwapManager.iKData.positions4_2d[i].y);

            //            if (fingerBones2D_LeftFish_RightHand.ContainsKey(i - 1))
            //            {
            //                fingerBones2D_LeftFish_RightHand[i - 1].anchoredPosition = (keyPoints2D_LeftFish_RightHand[i - 1].anchoredPosition + keyPoints2D_LeftFish_RightHand[i].anchoredPosition) / 2.0f;
            //                Vector2 temp0 = keyPoints2D_LeftFish_RightHand[i - 1].anchoredPosition - keyPoints2D_LeftFish_RightHand[i].anchoredPosition;
            //                fingerBones2D_LeftFish_RightHand[i - 1].localRotation = Quaternion.FromToRotation(Vector2.up, temp0);
            //                fingerBones2D_LeftFish_RightHand[i - 1].localScale = new Vector3(1, temp0.magnitude / fingerBones2D_LeftFish_RightHand[i - 1].sizeDelta.y, 1);

            //                fingerBones2D_RightFish_RightHand[i - 1].anchoredPosition = (keyPoints2D_RightFish_RightHand[i - 1].anchoredPosition + keyPoints2D_RightFish_RightHand[i].anchoredPosition) / 2.0f;
            //                temp0 = keyPoints2D_RightFish_RightHand[i - 1].anchoredPosition - keyPoints2D_RightFish_RightHand[i].anchoredPosition;
            //                fingerBones2D_RightFish_RightHand[i - 1].localRotation = Quaternion.FromToRotation(Vector2.up, temp0);
            //                fingerBones2D_RightFish_RightHand[i - 1].localScale = new Vector3(1, temp0.magnitude / fingerBones2D_RightFish_RightHand[i - 1].sizeDelta.y, 1);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (NativeSwapManager.iKData.positions2_2d != null)
            //    {
            //        for (int i = 0; i < NativeSwapManager.iKData.positions2_2d.Length; i++)
            //        {
            //            keyPoints2D_LeftFish_RightHand[i].anchoredPosition = new Vector2(9999, 9999);
            //            keyPoints2D_RightFish_RightHand[i].anchoredPosition = new Vector2(9999, 9999);

            //            if (fingerBones2D_LeftFish_LeftHand.ContainsKey(i - 1))
            //            {
            //                fingerBones2D_LeftFish_RightHand[i - 1].anchoredPosition = new Vector2(9999, 9999);
            //                fingerBones2D_RightFish_RightHand[i - 1].anchoredPosition = new Vector2(9999, 9999);
            //            }
            //        }
            //    }
            //}
            #endregion
        }
    }
}