using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 手部碰撞
    /// </summary>
    [ScriptExecutionOrder(-8)]
    public class HandsCollision : MonoBehaviour
    {
        Dictionary<int, Rigidbody> rigidbodyForCollision = new Dictionary<int, Rigidbody>();

        /// <summary>
        /// 是否启用手部物理碰撞
        /// </summary>
        public bool enablePhysicsInteraction;


        GameObject prefab_KeyPointForCollision;

        HandInfo handInfo;


        private void Awake()
        {
            handInfo = GetComponent<HandInfo>();

            prefab_KeyPointForCollision = ResourcesManager.Load<GameObject>("KeyPoint/KeyPointForCollision");

        }

        void LateUpdate()
        {
            if (enablePhysicsInteraction)
            {
                if (handInfo.isValid)
                {
                    if (rigidbodyForCollision != null)
                    {
                        foreach (HandJointType key in rigidbodyForCollision.Keys)
                        {
                            int id = (int)key;

                            //更新KeyPoint的Pose
                            rigidbodyForCollision[id].MovePosition(handInfo.GetJointData(key).position);
                            rigidbodyForCollision[id].MoveRotation(handInfo.GetJointData(key).rotation);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 启用/禁用手部物理碰撞
        /// </summary>
        public void SetPhysicsInteraction(bool value)
        {
            enablePhysicsInteraction = value;

            HandJointType[] handJointTypes = new HandJointType[7] { HandJointType.Thumb_3, HandJointType.Index_4, HandJointType.Middle_4, HandJointType.Ring_4, HandJointType.Pinky_4, HandJointType.Palm, HandJointType.Wrist_Middle };

            if (enablePhysicsInteraction)
            {
                if (rigidbodyForCollision.Count == 0)
                {
                    foreach (HandJointType handJointType in handJointTypes)
                    {
                        Rigidbody rigidbodyForCollision = Instantiate(prefab_KeyPointForCollision, handInfo.root).GetComponent<Rigidbody>();
                        rigidbodyForCollision.name = "Physics_" + handInfo.handType + "_" + handJointType.ToString();
                        this.rigidbodyForCollision.Add((int)handJointType, rigidbodyForCollision);
                    }
                }
            }
            else
            {
                foreach(KeyValuePair<int,Rigidbody> item in rigidbodyForCollision)
                {
                    Destroy(item.Value.gameObject);
                }
                rigidbodyForCollision.Clear();
            }
        }
    }
}