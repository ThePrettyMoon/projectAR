using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using EZXR.Glass.Common;

namespace EZXR.Glass.UI
{
    [ExecuteInEditMode]
    public class SpacialUIController : MonoBehaviour
    {
        #region Singleton
        private static SpacialUIController instance;
        public static SpacialUIController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SpacialUIController>();
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 中间位相机（通常用于Editor）
        /// </summary>
        [HideInInspector]
        public Camera centerCamera;
        /// <summary>
        /// 左眼相机
        /// </summary>
        [HideInInspector]
        public Camera leftCamera;
        /// <summary>
        /// 右眼相机
        /// </summary>
        [HideInInspector]
        public Camera rightCamera;
        /// <summary>
        /// 头的Transform
        /// </summary>
        [HideInInspector]
        public Transform headTransform;
        [Tooltip("图片的每个像素对应的UnityUnit（Cube的默认边长即为1个UnityUnit）")]
        /// <summary>
        /// 图片的每个像素对应的UnityUnit（Cube的默认边长即为1个UnityUnit）
        /// </summary>
        public float unitsPerPixel = 0.0001f;

        //public ARUIController()
        //{
        //    instance = this;
        //}

        private void Awake()
        {
            if (Application.isPlaying)
            {
                Instantiate(ResourcesManager.Load<GameObject>("UI/SpacialUIEventSystem"));

                if (HMDPoseTracker.Instance != null)
                {
                    leftCamera = HMDPoseTracker.Instance.leftCamera != null ? HMDPoseTracker.Instance.leftCamera : null;
                    rightCamera = HMDPoseTracker.Instance.rightCamera != null ? HMDPoseTracker.Instance.rightCamera : null;
                    centerCamera = HMDPoseTracker.Instance.centerCamera != null ? HMDPoseTracker.Instance.centerCamera : null;
                    headTransform = HMDPoseTracker.Instance.transform;
                }

                if (leftCamera == null)
                {
                    Debug.LogError("leftCamera 不存在!");
                }
                if (rightCamera == null)
                {
                    Debug.LogError("rightCamera 不存在!");
                }
                if (centerCamera == null)
                {
                    Debug.LogError("centerCamera 不存在!");
                }
            }
            else
            {
                instance = this;
            }
        }
    }
}