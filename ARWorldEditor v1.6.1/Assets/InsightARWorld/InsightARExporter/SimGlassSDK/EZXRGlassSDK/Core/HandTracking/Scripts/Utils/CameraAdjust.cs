using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Common
{
    public class CameraAdjust
    {
        private static CameraAdjust instance;
        public static CameraAdjust Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CameraAdjust();
                }
                return instance;
            }
        }

        /// <summary>
        /// 目标屏幕像素宽
        /// </summary>
        int screenWidth = 640;
        /// <summary>
        /// 目标屏幕像素高
        /// </summary>
        int screenHeight = 480;

        public Camera myCamera;
        public float left = -0.2F;
        public float right = 0.2F;
        public float top = 0.2F;
        public float bottom = -0.2F;

        public static float ax, ay, u0, v0, dx, dy;

        public float offset_left, offset_horizontal, offset_top, offset_vertical;
        public static float[] projector_KK = null;

        public void SetCameraProjectionMatrix(Camera camera, float[] k, int targetScreenWidth, int targetScreenHeight)
        {
            myCamera = camera;
            SetK(k);
            screenWidth = targetScreenWidth;
            screenHeight = targetScreenHeight;
            RefreshCameraMatrix();
        }

        void SetK(float[] k)
        {
            ax = k[0];
            ay = k[4];
            u0 = k[2];
            v0 = k[5];

            if (ax == 0 || ay == 0)
            {
                return;
            }

            dx = myCamera.nearClipPlane / ax;
            dy = myCamera.nearClipPlane / ay;
        }

        void RefreshCameraMatrix()
        {
            left = u0 * dx * (-1) + (offset_horizontal) + (offset_left);
            right = (screenWidth - u0) * dx + (offset_horizontal);
            top = dy * v0 + offset_vertical + offset_top;
            bottom = (screenHeight - v0) * dy * (-1) + offset_vertical;

            Matrix4x4 m = PerspectiveOffCenter(left, right, bottom, top, myCamera.nearClipPlane, myCamera.farClipPlane);
            myCamera.projectionMatrix = m;
        }

        Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            float x = 2.0F * near / (right - left);
            float y = 2.0F * near / (top - bottom);
            float a = (right + left) / (right - left);
            float b = (top + bottom) / (top - bottom);
            float c = -(far + near) / (far - near);
            float d = -(2.0F * far * near) / (far - near);
            float e = -1.0F;
            Matrix4x4 m = new Matrix4x4();
            m[0, 0] = x;
            m[0, 1] = 0;
            m[0, 2] = a;
            m[0, 3] = 0;
            m[1, 0] = 0;
            m[1, 1] = y;
            m[1, 2] = b;
            m[1, 3] = 0;
            m[2, 0] = 0;
            m[2, 1] = 0;
            m[2, 2] = c;
            m[2, 3] = d;
            m[3, 0] = 0;
            m[3, 1] = 0;
            m[3, 2] = e;
            m[3, 3] = 0;
            return m;
        }

    }
}