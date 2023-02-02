using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    [ScriptExecutionOrder(-8)]
    public class TriggerForFarNearSwitch : MonoBehaviour
    {
        //检测Trigger的长度
        public static float length = 0.24f;
        HandInfo handInfo;

        private void Awake()
        {
            //transform.localScale = Vector3.one * radius * 2;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (handInfo != null && handInfo.handExist)
            {
                Pose pose = handInfo.GetJointData(HandJointType.Palm);
                transform.position = pose.position;
                transform.rotation = pose.rotation; ;
            }
        }

        private void OnDisable()
        {
            handInfo.PreCloseContactingTarget = null;
            handInfo.Last_PreCloseContactingTarget = null;
        }

        public void SetUp(Transform handRoot)
        {
            handInfo = handRoot.GetComponent<HandInfo>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != gameObject.layer)
            {
                if (handInfo != null)
                {
                    //“当用户的手是up状态”且“当前手没有进入其他Trigger”时
                    if (!handInfo.isPinching && !handInfo.isCloseContacting)
                    {
                        if (other.tag == "SpacialObject" || other.tag == "SpacialUI" || other.tag == "SpacialHandler")
                        {
                            handInfo.preCloseContacting = true;
                            if (handInfo.PreCloseContactingTarget == null || Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, handInfo.PreCloseContactingTarget.position))
                            {
                                //handInfo.Last_PreCloseContactingTarget = handInfo.PreCloseContactingTarget;
                                handInfo.PreCloseContactingTarget = other.transform;
                            }
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
                    //“当用户的手是up状态”且“当前手没有进入其他Trigger”时
                    if (!handInfo.isPinching && !handInfo.isCloseContacting)
                    {
                        if (other.tag == "SpacialObject" || other.tag == "SpacialUI" || other.tag == "SpacialHandler")
                        {
                            if (handInfo.PreCloseContactingTarget == other.transform)
                            {
                                //handInfo.Last_PreCloseContactingTarget = handInfo.PreCloseContactingTarget;
                                handInfo.PreCloseContactingTarget = null;
                            }
                        }
                    }
                }
            }
        }
    }
}