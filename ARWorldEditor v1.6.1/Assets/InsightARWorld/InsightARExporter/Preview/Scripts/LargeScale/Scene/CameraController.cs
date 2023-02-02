using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

namespace Dongjian.LargeScale
{
    /// <summary>
    /// 相机管理
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private Camera mainCamera;
        private Transform mainCameraTrans;

        private static CameraController _instance;

        public static CameraController Instance
        {
            get
            {
                return _instance;
            }
        }

        public Camera MainCamera
        {
            get
            {
                return mainCamera;
            }
        }

        public Transform MainCameraTrans
        {
            get
            {
                return mainCameraTrans;
            }
        }



        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            mainCamera = Camera.main;
            mainCameraTrans = mainCamera.transform;
        }
    }
}
