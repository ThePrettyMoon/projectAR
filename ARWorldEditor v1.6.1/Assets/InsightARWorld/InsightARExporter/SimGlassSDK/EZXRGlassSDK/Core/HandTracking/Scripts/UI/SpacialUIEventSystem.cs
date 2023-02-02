using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.Hand;

namespace EZXR.Glass.UI
{
    public class SpacialUIEventSystem : MonoBehaviour
    {
        #region Singleton
        private static SpacialUIEventSystem instance;
        public static SpacialUIEventSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SpacialUIEventSystem>();
                }
                return instance;
            }
        }
        #endregion

        static Dictionary<Collider, SpacialSelectable> selectablesDic = new Dictionary<Collider, SpacialSelectable>();
        Collider lastOne_Left, lastOne_Right;

        void Start()
        {
            //用于得到当前手的射线检测的结果
            ARHandManager.leftHand.Event_GetRayCastResult += OnRayCastHit_Left;
            ARHandManager.rightHand.Event_GetRayCastResult += OnRayCastHit_Right;
        }

        public static void RegisterCallBack(Collider collider, SpacialSelectable selectable)
        {
            if (!selectablesDic.ContainsKey(collider))
            {
                selectablesDic.Add(collider, selectable);
            }
        }

        public void OnRayCastHit_Left(Collider other, bool isUI)
        {
            if (other != null)
            {
                if (isUI)
                {
                    selectablesDic[other].OnRayCastHit(other == null ? null : ARHandManager.leftHand);
                }
                lastOne_Left = other;
            }
            else
            {
                if (lastOne_Left != null)
                {
                    selectablesDic[lastOne_Left].OnRayCastHit(other == null ? null : ARHandManager.leftHand);
                }
            }
        }

        public void OnRayCastHit_Right(Collider other, bool isUI)
        {
            if (other != null)
            {
                if (isUI)
                {
                    selectablesDic[other].OnRayCastHit(other == null ? null : ARHandManager.rightHand);
                }
                lastOne_Right = other;
            }
            else
            {
                if (lastOne_Right != null)
                {
                    selectablesDic[lastOne_Right].OnRayCastHit(other == null ? null : ARHandManager.rightHand);
                }
            }
        }
    }
}