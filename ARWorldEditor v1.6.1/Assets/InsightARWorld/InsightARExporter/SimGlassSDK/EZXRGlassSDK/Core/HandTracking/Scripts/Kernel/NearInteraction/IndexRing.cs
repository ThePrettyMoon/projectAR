using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public enum IndexRingStatus
    {
        /// <summary>
        /// 圆环
        /// </summary>
        Ring,
        /// <summary>
        /// 实心圆
        /// </summary>
        Solid,
    }

    public class IndexRing : MonoBehaviour
    {
        HandInfo handInfo;
        Ray ray = new Ray();
        Pose indexTip, indexSecond;
        RaycastHit hitInfo;
        /// <summary>
        /// 圆环的Renderer
        /// </summary>
        public Renderer rendererRing;
        /// <summary>
        /// 实心圆的Renderer
        /// </summary>
        public Renderer rendererSolid;
        bool hit;
        MaterialPropertyBlock propertyBlock;
        /// <summary>
        /// 指尖圆环的显示状态是圆环还是实心圆
        /// </summary>
        IndexRingStatus indexRingStatus;

        // Start is called before the first frame update
        void Start()
        {
            propertyBlock = new MaterialPropertyBlock();
            rendererRing.GetPropertyBlock(propertyBlock);
        }

        private void FixedUpdate()
        {
            if (handInfo.handExist)
            {
                //食指指尖Pose
                indexTip = handInfo.GetJointData(HandJointType.Index_4);
                //食指挨着指尖的关节的Pose
                indexSecond = handInfo.GetJointData(HandJointType.Index_3);

                //食指射线的发射点
                ray.origin = indexTip.position;
                //食指射线的发射方向
                ray.direction = indexTip.position - indexSecond.position;

                //得到食指射线射到的物体信息
                hit = Physics.Raycast(ray, out hitInfo);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (handInfo != null && handInfo.handExist)
            {
                if (indexRingStatus == IndexRingStatus.Ring)
                {
                    //显示圆环
                    rendererSolid.gameObject.SetActive(false);
                    rendererRing.gameObject.SetActive(true);

                    //圆环在食指指尖稍微靠外一点的位置，圆环法线沿着食指射线方向
                    transform.position = indexTip.position + ray.direction * 0.006f;
                    transform.LookAt(indexTip.position);

                    if (hit)
                    {
                        //当前距离与预设的最远出现圆环的距离（在TriggerForFarNearSwitch.radius这个距离上环达到最大直径，与近距离远距离切换绑定）
                        float percent = Mathf.Clamp01(hitInfo.distance / TriggerForFarNearSwitch.length);
                        propertyBlock.SetFloat("_OuterRadius", 0.3f + 0.2f * percent);
                        propertyBlock.SetFloat("_InnerRadius", 0.4f * percent);
                        rendererRing.SetPropertyBlock(propertyBlock);
                    }
                }
                else//如果是实心圆说明用户捏合了手指
                {
                    //显示实心圆
                    rendererRing.gameObject.SetActive(false);
                    rendererSolid.gameObject.SetActive(true);

                    //IndexRing在捏合点
                    transform.position = handInfo.CurCloseContactingTarget.transform.TransformPoint(handInfo.grabLocalPoint);
                }
            }
        }

        public void SetUp(HandInfo handInfo)
        {
            this.handInfo = handInfo;
        }

        /// <summary>
        /// 设置指尖圆环的显示状态是圆环还是实心圆
        /// </summary>
        /// <param name="indexRingStatus"></param>
        public void SetStatus(IndexRingStatus indexRingStatus)
        {
            this.indexRingStatus = indexRingStatus;
        }
    }
}