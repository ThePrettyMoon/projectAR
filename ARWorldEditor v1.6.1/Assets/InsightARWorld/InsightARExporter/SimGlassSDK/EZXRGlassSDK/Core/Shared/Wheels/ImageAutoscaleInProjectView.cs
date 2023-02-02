using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    [ExecuteInEditMode]
    /// <summary>
    /// 在透视摄像机下将图像屏幕最大化显示
    /// </summary>
    public class ImageAutoscaleInProjectView : MonoBehaviour
    {
        public enum HowToFit
        {
            /// <summary>
            /// 等比缩放图片以适配屏幕的高度或者宽度（图片高大于宽就是适配屏幕高度，反之则适配屏幕宽度）
            /// </summary>
            ScaleToFitScreenWidthOrHeight,
            /// <summary>
            /// 缩放以拉伸到全屏大小（图片比例会变成屏幕的比例）
            /// </summary>
            ScaleToFullScreen,
            /// <summary>
            /// 按图片的真实像素大小显示在屏幕上
            /// </summary>
            ActurePixelSize,
        }
        public Camera myCamera;
        /// <summary>
        /// 图片直接拉伸到全屏幕
        /// </summary>
        public HowToFit fitToScreen;
        /// <summary>
        /// 要适配的图片
        /// </summary>
        public Texture2D texture;
        /// <summary>
        /// 在transform.localPosition.z这个距离下图片应该缩放到的物理高度（米）
        /// </summary>
        float screenHeightInMetres;
        /// <summary>
        /// 在transform.localPosition.z这个距离下屏幕的物理宽度（米）
        /// </summary>
        float screenWidthInMetres;
        /// <summary>
        /// 使用相机的透视矩阵来进行计算（对于更改了透视矩阵的相机需要勾选此处）
        /// </summary>
        bool usePerspectiveMatrix = true;
        Matrix4x4 projectionMatrix;

        // Start is called before the first frame update
        void Start()
        {
            if (texture == null)
            {
                texture = (Texture2D)gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (myCamera.orthographic)
            {
                //在transform.localPosition.z这个距离下图片应该缩放到的物理高度
                screenHeightInMetres = myCamera.orthographicSize * 2;
                //在transform.localPosition.z这个距离下屏幕的物理宽度
                screenWidthInMetres = (float)Screen.width * myCamera.rect.width / (float)Screen.height * screenHeightInMetres;
            }
            else
            {
                //图像显示面距离相机的z距离
                float z = myCamera.transform.InverseTransformPoint(transform.position).z;

                if (usePerspectiveMatrix)
                {
                    if (projectionMatrix != myCamera.projectionMatrix)
                    {
                        projectionMatrix = myCamera.projectionMatrix;

                        float x = myCamera.projectionMatrix[0, 0];
                        float y = myCamera.projectionMatrix[1, 1];
                        float a = myCamera.projectionMatrix[0, 2];
                        float b = myCamera.projectionMatrix[1, 2];
                        float near = myCamera.nearClipPlane;

                        float left = (a - 1) * near / x;
                        float right = (a + 1) * near / x;
                        float top = (b - 1) * near / y;
                        float bottom = (b + 1) * near / y;

                        float leftRadians = Mathf.Atan2(left, near);
                        float rightRadians = Mathf.Atan2(right, near);
                        float topRadians = Mathf.Atan2(top, near);
                        float bottomRadians = Mathf.Atan2(bottom, near);

                        float left_Image = left / near * z;
                        float right_Image = right / near * z;
                        float top_Image = top / near * z;
                        float bottom_Image = bottom / near * z;

                        //得到图像在透视相机下的相对偏移坐标（如果相机是非对称相机的话这里才有意义，对于普通的对称相机来讲这个是0）
                        Vector3 imageInCameraLocalPosition = new Vector3((left_Image + right_Image) / 2.0f, (top_Image + bottom_Image) / 2.0f, z);
                        //将图像设置到偏移后的位置（目的是居中在相机视野正中间）
                        transform.position = myCamera.transform.TransformPoint(imageInCameraLocalPosition);

                        screenHeightInMetres = Mathf.Abs(top_Image) + Mathf.Abs(bottom_Image);
                        screenWidthInMetres = Mathf.Abs(left_Image) + Mathf.Abs(right_Image);
                    }
                }
                else
                {
                    screenHeightInMetres = Mathf.Tan(Mathf.Deg2Rad * myCamera.fieldOfView / 2.0f) * z * 2.0f;
                    screenWidthInMetres = (float)Screen.width * myCamera.rect.width / (float)Screen.height * screenHeightInMetres;
                }
            }

            switch (fitToScreen)
            {
                case HowToFit.ScaleToFullScreen:
                    transform.localScale = new Vector3(screenWidthInMetres, screenHeightInMetres, 1);
                    break;
                case HowToFit.ScaleToFitScreenWidthOrHeight:
                    if (texture == null)//如果还是空的话就不执行下面的内容
                    {
                        return;
                    }
                    //在transform.localPosition.z这个距离下图片应该等比缩放到的物理宽度
                    float width = texture.width / (float)texture.height * screenHeightInMetres;
                    if (width < screenWidthInMetres)//算出来的图片显示宽度小于屏幕宽度，直接就用算出来的图片宽度和屏幕高度作为显示区域的尺寸
                    {
                        transform.localScale = new Vector3(width, screenHeightInMetres, 1);
                    }
                    else//算出来的图片显示宽度大于屏幕宽度，重新按屏幕宽度缩放图片高度
                    {
                        float height = screenHeightInMetres * (screenWidthInMetres / width);
                        transform.localScale = new Vector3(screenWidthInMetres, height, 1);
                    }
                    break;
                case HowToFit.ActurePixelSize:
                    if (texture == null)//如果还是空的话就不执行下面的内容
                    {
                        return;
                    }
                    transform.localScale = new Vector3(texture.width / (float)Screen.width * screenWidthInMetres, texture.height / (float)Screen.height * screenHeightInMetres, 1);
                    break;
            }
        }
    }
}