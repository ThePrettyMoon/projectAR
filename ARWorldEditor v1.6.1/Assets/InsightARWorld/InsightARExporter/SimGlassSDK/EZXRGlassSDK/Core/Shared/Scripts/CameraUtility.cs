using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Common
{
    public class CameraUtility
    {
        public static float[] CalculateFov(int imageWidth, int imageHeight,
        int screenWidth, int screenHeight, float[] fieldOfView)
        {
            float[] screenFov = new float[2] { 0, 0 };
            int oriScreenWidth = screenWidth;
            int oriScreenHeight = screenHeight;

            // 图像是横屏，屏幕先按照横屏处理
            if (oriScreenWidth < oriScreenHeight)
            {
                screenWidth = oriScreenHeight;
                screenHeight = oriScreenWidth;
            }

            float screenRatio = (float)screenWidth / (float)screenHeight;
            float imageRatio = (float)imageWidth / (float)imageHeight;
            float ratio = imageRatio / screenRatio;

            if (ratio < 1)
            {
                screenFov[0] = fieldOfView[0];
                screenFov[1] = 2.0f * Mathf.Atan(ratio * Mathf.Tan(0.5f * fieldOfView[1] * Mathf.PI / 180.0f)) * 180.0f / Mathf.PI;
            }
            else
            {
                screenFov[0] = 2.0f * Mathf.Atan(1.0f / ratio * Mathf.Tan(0.5f * fieldOfView[0] * Mathf.PI / 180.0f)) * 180.0f / Mathf.PI;
                screenFov[1] = fieldOfView[1];
            }
            return screenFov;
        }
    }
}