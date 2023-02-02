using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EZXR.Glass.Common
{
    public class FitScreenAspect : MonoBehaviour
    {
        /// <summary>
        /// 缩放的基础宽度
        /// </summary>
        public int baseWidth = 640;
        /// <summary>
        /// 缩放的基础高度
        /// </summary>
        public int baseHeight = 480;
        public Camera myCamera;
        public RectTransform image;

        // Start is called before the first frame update
        void Start()
        {
            if (myCamera != null)
            {
                float width = baseWidth * ((float)Screen.height / baseHeight) / Screen.width;
                myCamera.rect = new Rect((1 - width) / 2.0f, 0, width, 1);
            }

            if (image != null)
            {
                float width = baseWidth * ((float)Screen.height / baseHeight);
                image.sizeDelta = new Vector2(width, Screen.height);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}