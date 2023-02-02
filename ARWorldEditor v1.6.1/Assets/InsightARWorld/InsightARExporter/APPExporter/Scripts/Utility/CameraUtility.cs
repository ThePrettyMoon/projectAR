using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    /// 相机类
    /// </summary>
    public class CameraUtility 
    {
        /// <summary>
        /// 坐标系转换
        /// </summary>
        /// <param name="pose"></param>
        public static Pose OpenCVToUnity(Pose cvPose,bool isRotate)
        {
            Quaternion rotation = new Quaternion(cvPose.rotation.x, cvPose.rotation.y, cvPose.rotation.z, cvPose.rotation.w);
            Vector3 position = new Vector3(cvPose.position.x, cvPose.position.y, cvPose.position.z);
            Vector3 scale = new Vector3(1, 1, 1);
            //opencv坐标系，y朝里，z朝上
            Matrix4x4 T_OC_OW = Matrix4x4.TRS(position, rotation, scale);

            Matrix4x4 T_OW_OC = T_OC_OW.inverse;
            //opencv 坐标系
            Matrix4x4 T_OC_UC = Matrix4x4.identity;

            if (isRotate)
            {
                T_OC_UC.m00 = 0;
                T_OC_UC.m01 = -1;
                T_OC_UC.m10 = -1;
                T_OC_UC.m11 = 0;
            }
            else
            {
                T_OC_UC.m11 = -1;
            }

            Matrix4x4 T_UW_OW = Matrix4x4.identity;
            T_UW_OW.m11 = 0;
            T_UW_OW.m12 = 1;
            T_UW_OW.m21 = 1;
            T_UW_OW.m22 = 0;

            Matrix4x4 T_UW_UC = T_UW_OW * T_OW_OC * T_OC_UC;

            Pose pose = new Pose();
            pose.rotation = T_UW_UC.rotation;
            pose.position = new Vector3(T_UW_UC.m03, T_UW_UC.m13, T_UW_UC.m23);
            return pose;
        }

        /// <summary>
        /// 计算fov
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="fieldOfView"></param>
        /// <returns></returns>
        public static float CalculateFov(int imageWidth, int imageHeight,
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
            return oriScreenWidth > oriScreenHeight ? screenFov[1] : screenFov[0];
        }
    }
}
