using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public class VirtualHandController : MonoBehaviour
    {
        #region 单例
        private static VirtualHandController instance;
        public static VirtualHandController Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 所有手指节点数据，手腕是0，大拇指根节点是1，大拇指之间是4，食指指尖是8，以此类推
        /// </summary>
        Vector3[] keyPoints_Right = new Vector3[21];
        /// <summary>
        /// 所有手指的模型骨骼点，用于控制虚拟手的骨骼运动，从大拇指到小拇指，从根部到指尖
        /// </summary>
        public Transform[] fingers;
        public Transform palm;


        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //右手
            //if (NativeSwapManager.iKData.hand2_valid)
            //{
            //    //设置右手关节点的位置
            //    if (NativeSwapManager.iKData.positions2 != null)
            //    {
            //        for (int i = 0; i < NativeSwapManager.iKData.positions2.Length; i++)
            //        {
            //            keyPoints_Right[i] = new Vector3(NativeSwapManager.iKData.positions2[i].x, NativeSwapManager.iKData.positions2[i].y, NativeSwapManager.iKData.positions2[i].z)  ;
            //        }
            //    }

            //    //计算食指0关节（根部关节）的旋转角
            //    fingers[3].localEulerAngles = new Vector3(0, Vector3.Angle(keyPoints_Right[5] - keyPoints_Right[0], keyPoints_Right[6] - keyPoints_Right[5]), 0);
            //}
        }
    }
}