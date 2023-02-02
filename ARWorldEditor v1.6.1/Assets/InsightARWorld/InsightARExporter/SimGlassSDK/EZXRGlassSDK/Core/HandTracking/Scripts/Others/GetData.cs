using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public class GetData : MonoBehaviour
    {
        [Serializable]
        public class Root
        {
            /// <summary>
            /// 大拇指，相对于相机的localPosition和屏幕坐标，数组从0到3依次是指尖到手指根部关节点
            /// </summary>
            public Vector3[] f03;
            public Vector2[] f02;
            /// <summary>
            /// 食指
            /// </summary>
            public Vector3[] f13;
            public Vector2[] f12;
            /// <summary>
            /// 中指
            /// </summary>
            public Vector3[] f23;
            public Vector2[] f22;
            /// <summary>
            /// 无名指
            /// </summary>
            public Vector3[] f33;
            public Vector2[] f32;
            /// <summary>
            /// 小拇指
            /// </summary>
            public Vector3[] f43;
            public Vector2[] f42;
            /// <summary>
            /// 手腕
            /// </summary>
            public Vector3[] f53;
            public Vector2[] f52;

            public Root()
            {
                f03 = new Vector3[4];
                f02 = new Vector2[4];

                f13 = new Vector3[4];
                f12 = new Vector2[4];

                f23 = new Vector3[4];
                f22 = new Vector2[4];

                f33 = new Vector3[4];
                f32 = new Vector2[4];

                f43 = new Vector3[4];
                f42 = new Vector2[4];

                f53 = new Vector3[1];
                f52 = new Vector2[1];
            }
        }
        
        public Camera mainCamera;
        /// <summary>
        /// 大拇指
        /// </summary>
        public Transform[] joints_DaddyFinger;
        /// <summary>
        /// 食指
        /// </summary>
        public Transform[] joints_MommyFinger;
        /// <summary>
        /// 中指
        /// </summary>
        public Transform[] joints_BrotherFinger;
        /// <summary>
        /// 无名指
        /// </summary>
        public Transform[] joints_SisterFinger;
        /// <summary>
        /// 小拇指
        /// </summary>
        public Transform[] joints_BabbyFinger;
        /// <summary>
        /// 手腕
        /// </summary>
        public Transform[] joints_Wrist;

        // Start is called before the first frame update
        void Start()
        {
            Root jointsData = new Root();
            jointsData.f03[0] = transform.InverseTransformPoint(joints_DaddyFinger[0].position);
            jointsData.f03[1] = transform.InverseTransformPoint(joints_DaddyFinger[1].position);
            jointsData.f03[2] = transform.InverseTransformPoint(joints_DaddyFinger[2].position);
            jointsData.f03[3] = transform.InverseTransformPoint(joints_DaddyFinger[3].position);
            jointsData.f02[0] = mainCamera.WorldToScreenPoint(joints_DaddyFinger[0].position);
            jointsData.f02[0] = new Vector2(jointsData.f02[0].x, Screen.height - jointsData.f02[0].y);
            jointsData.f02[1] = mainCamera.WorldToScreenPoint(joints_DaddyFinger[1].position);
            jointsData.f02[1] = new Vector2(jointsData.f02[1].x, Screen.height - jointsData.f02[1].y);
            jointsData.f02[2] = mainCamera.WorldToScreenPoint(joints_DaddyFinger[2].position);
            jointsData.f02[2] = new Vector2(jointsData.f02[2].x, Screen.height - jointsData.f02[2].y);
            jointsData.f02[3] = mainCamera.WorldToScreenPoint(joints_DaddyFinger[3].position);
            jointsData.f02[3] = new Vector2(jointsData.f02[3].x, Screen.height - jointsData.f02[3].y);

            jointsData.f13[0] = transform.InverseTransformPoint(joints_MommyFinger[0].position);
            jointsData.f13[1] = transform.InverseTransformPoint(joints_MommyFinger[1].position);
            jointsData.f13[2] = transform.InverseTransformPoint(joints_MommyFinger[2].position);
            jointsData.f13[3] = transform.InverseTransformPoint(joints_MommyFinger[3].position);
            jointsData.f12[0] = mainCamera.WorldToScreenPoint(joints_MommyFinger[0].position);
            jointsData.f12[0] = new Vector2(jointsData.f12[0].x, Screen.height - jointsData.f12[0].y);
            jointsData.f12[1] = mainCamera.WorldToScreenPoint(joints_MommyFinger[1].position);
            jointsData.f12[1] = new Vector2(jointsData.f12[1].x, Screen.height - jointsData.f12[1].y);
            jointsData.f12[2] = mainCamera.WorldToScreenPoint(joints_MommyFinger[2].position);
            jointsData.f12[2] = new Vector2(jointsData.f12[2].x, Screen.height - jointsData.f12[2].y);
            jointsData.f12[3] = mainCamera.WorldToScreenPoint(joints_MommyFinger[3].position);
            jointsData.f12[3] = new Vector2(jointsData.f12[3].x, Screen.height - jointsData.f12[3].y);

            jointsData.f23[0] = transform.InverseTransformPoint(joints_BrotherFinger[0].position);
            jointsData.f23[1] = transform.InverseTransformPoint(joints_BrotherFinger[1].position);
            jointsData.f23[2] = transform.InverseTransformPoint(joints_BrotherFinger[2].position);
            jointsData.f23[3] = transform.InverseTransformPoint(joints_BrotherFinger[3].position);
            jointsData.f22[0] = mainCamera.WorldToScreenPoint(joints_BrotherFinger[0].position);
            jointsData.f22[0] = new Vector2(jointsData.f22[0].x, Screen.height - jointsData.f22[0].y);
            jointsData.f22[1] = mainCamera.WorldToScreenPoint(joints_BrotherFinger[1].position);
            jointsData.f22[1] = new Vector2(jointsData.f22[1].x, Screen.height - jointsData.f22[1].y);
            jointsData.f22[2] = mainCamera.WorldToScreenPoint(joints_BrotherFinger[2].position);
            jointsData.f22[2] = new Vector2(jointsData.f22[2].x, Screen.height - jointsData.f22[2].y);
            jointsData.f22[3] = mainCamera.WorldToScreenPoint(joints_BrotherFinger[3].position);
            jointsData.f22[3] = new Vector2(jointsData.f22[3].x, Screen.height - jointsData.f22[3].y);

            jointsData.f33[0] = transform.InverseTransformPoint(joints_SisterFinger[0].position);
            jointsData.f33[1] = transform.InverseTransformPoint(joints_SisterFinger[1].position);
            jointsData.f33[2] = transform.InverseTransformPoint(joints_SisterFinger[2].position);
            jointsData.f33[3] = transform.InverseTransformPoint(joints_SisterFinger[3].position);
            jointsData.f32[0] = mainCamera.WorldToScreenPoint(joints_SisterFinger[0].position);
            jointsData.f32[0] = new Vector2(jointsData.f32[0].x, Screen.height - jointsData.f32[0].y);
            jointsData.f32[1] = mainCamera.WorldToScreenPoint(joints_SisterFinger[1].position);
            jointsData.f32[1] = new Vector2(jointsData.f32[1].x, Screen.height - jointsData.f32[1].y);
            jointsData.f32[2] = mainCamera.WorldToScreenPoint(joints_SisterFinger[2].position);
            jointsData.f32[2] = new Vector2(jointsData.f32[2].x, Screen.height - jointsData.f32[2].y);
            jointsData.f32[3] = mainCamera.WorldToScreenPoint(joints_SisterFinger[3].position);
            jointsData.f32[3] = new Vector2(jointsData.f32[3].x, Screen.height - jointsData.f32[3].y);

            jointsData.f43[0] = transform.InverseTransformPoint(joints_BabbyFinger[0].position);
            jointsData.f43[1] = transform.InverseTransformPoint(joints_BabbyFinger[1].position);
            jointsData.f43[2] = transform.InverseTransformPoint(joints_BabbyFinger[2].position);
            jointsData.f43[3] = transform.InverseTransformPoint(joints_BabbyFinger[3].position);
            jointsData.f42[0] = mainCamera.WorldToScreenPoint(joints_BabbyFinger[0].position);
            jointsData.f42[0] = new Vector2(jointsData.f42[0].x, Screen.height - jointsData.f42[0].y);
            jointsData.f42[1] = mainCamera.WorldToScreenPoint(joints_BabbyFinger[1].position);
            jointsData.f42[1] = new Vector2(jointsData.f42[1].x, Screen.height - jointsData.f42[1].y);
            jointsData.f42[2] = mainCamera.WorldToScreenPoint(joints_BabbyFinger[2].position);
            jointsData.f42[2] = new Vector2(jointsData.f42[2].x, Screen.height - jointsData.f42[2].y);
            jointsData.f42[3] = mainCamera.WorldToScreenPoint(joints_BabbyFinger[3].position);
            jointsData.f42[3] = new Vector2(jointsData.f42[3].x, Screen.height - jointsData.f42[3].y);

            jointsData.f53[0] = transform.InverseTransformPoint(joints_Wrist[0].position);
            jointsData.f52[0] = mainCamera.WorldToScreenPoint(joints_Wrist[0].position);
            jointsData.f52[0] = new Vector2(jointsData.f52[0].x, Screen.height - jointsData.f52[0].y);

            File.WriteAllText(Application.dataPath + "/Capture" + 0 + ".json", JsonUtility.ToJson(jointsData));

        }
    }
}