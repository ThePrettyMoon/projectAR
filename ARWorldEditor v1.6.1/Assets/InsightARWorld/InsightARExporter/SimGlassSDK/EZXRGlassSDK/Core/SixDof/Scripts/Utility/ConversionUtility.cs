
namespace EZXR.Glass.SixDof
{
    using UnityEngine;

    public class ConversionUtility
    {
        #region transform utility
        /// <summary>
        /// Get a matrix from position and rotation.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Matrix4x4 GetTMatrix(Vector3 position, Quaternion rotation)
        {
            return Matrix4x4.TRS(position, rotation, Vector3.one);
        }

        /// <summary>
        /// Get a matrix from position , rotation and scale.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix4x4 GetTMatrix(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return Matrix4x4.TRS(position, rotation, scale);
        }

        /// <summary>
        /// Get the position from a matrix4x4.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Vector3 GetPositionFromTMatrix(Matrix4x4 matrix)
        {
            Vector3 position;
            position.x = matrix.m03;
            position.y = matrix.m13;
            position.z = matrix.m23;

            return position;
        }

        /// <summary>
        /// Get the rotation from a matrix4x4.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Quaternion GetRotationFromTMatrix(Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        public static Vector3 ConvertPosition(Vector3 vec)
        {
            // Convert to left-handed
            return new Vector3((float)vec.x, (float)vec.y, (float)-vec.z);
        }

        public static Quaternion ConvertOrientation(Quaternion quat)
        {
            // Convert to left-handed
            return new Quaternion(-(float)quat.x, -(float)quat.y, (float)quat.z, (float)quat.w);
        }

        public static void VioPose2RgbPose(float[] twc, float[] rbg_to_leftfish, out Pose unityPose)
        {

            Matrix4x4 T_UW_UC_T = new Matrix4x4();
            Matrix4x4 rgb_to_leftfish_m = new Matrix4x4();
            for (int i = 0; i < 16; i++)
            {
                T_UW_UC_T[i] = twc[i];
                rgb_to_leftfish_m[i] = rbg_to_leftfish[i];
            }

            Matrix4x4 T_UW_UC = T_UW_UC_T.transpose;
            Matrix4x4 leftfish_to_rgb = rgb_to_leftfish_m.transpose.inverse;

            Matrix4x4 left = Matrix4x4.identity;
            Matrix4x4 right = Matrix4x4.identity;

            left.m11 = -1;
            right.m11 = -1;

            Matrix4x4 Tcw_RGB = T_UW_UC * left * leftfish_to_rgb * right;

            Vector3 position = Tcw_RGB.GetColumn(3);
            Quaternion rotation = Tcw_RGB.rotation;
            unityPose = new Pose(position, rotation);
        }

        // TODO: @xuninghao， 当前是 Unity to Unity 坐标转换格式。
        public static void ApiPoseToUnityPose(float[]twc ,out Pose unityPose)
        {
            //可以直接使用camTransform
            //opencv 坐标系 先转到opengl坐标系,给的数据是按行存储，需要按列存储，转置
            /*  Matrix4x4 T_SS_D_T = new Matrix4x4();
              for (int i = 0; i < 16; i++)
              {
                  T_SS_D_T[i] = twc[i];
              }

              Matrix4x4 T_SS_D = T_SS_D_T.transpose;

              //opengl 世界坐标系，定义x轴朝着左边
                Matrix4x4 T_OW_SS = Matrix4x4.identity;
                T_OW_SS.m00 = -1;
                T_OW_SS.m11 = 0;
                T_OW_SS.m12 = 1;
                T_OW_SS.m21 = 1;
                T_OW_SS.m22 = 0;

                Matrix4x4 T_D_OC = Matrix4x4.identity;
                T_D_OC.m11 = -1;
                T_D_OC.m22 = -1;
                Matrix4x4 T_OW_OC = T_OW_SS * T_SS_D * T_D_OC;

                Matrix4x4 T_UW_OW = Matrix4x4.identity;
                T_UW_OW.m00 = -1;
                Matrix4x4 T_OC_UC = Matrix4x4.identity;
                T_OC_UC.m22 = -1;
                Matrix4x4 T_UW_UC = T_UW_OW * T_OW_OC * T_OC_UC;*/

            /* Matrix4x4 T_D_UC = Matrix4x4.identity;
              T_D_UC.m11 = -1;
             //考虑竖屏 -> 横屏
            /* T_D_UC.m00 = 0;
             T_D_UC.m01 = -1;
             T_D_UC.m10 = -1;
             T_D_UC.m11 = 0;*/

            /*  Matrix4x4 T_UW_SS = Matrix4x4.identity;
              T_UW_SS.m11 = 0;
              T_UW_SS.m12 = 1;
              T_UW_SS.m21 = 1;
              T_UW_SS.m22 = 0;
              Matrix4x4 T_UW_UC = T_UW_SS * T_SS_D * T_D_UC;*/

            Matrix4x4 T_UW_UC_T = new Matrix4x4();
            for (int i = 0; i < 16; i++)
            {
                T_UW_UC_T[i] = twc[i];
            }

            Matrix4x4 T_UW_UC = T_UW_UC_T.transpose;

            Vector3 position = T_UW_UC.GetColumn(3);
            Quaternion rotation = T_UW_UC.rotation;
            unityPose = new Pose(position, rotation);
        }

        public static void RecenterByOffset(Pose oldResult, Matrix4x4 offset, out Pose result)
        {
            Matrix4x4 oldResult4x4 = new Matrix4x4(oldResult.right, oldResult.up, oldResult.forward, oldResult.position);
            oldResult4x4.m33 = 1.0f;

            Matrix4x4 newResult4x4 = offset * oldResult4x4;

            result = new Pose(newResult4x4.GetColumn(3), newResult4x4.rotation);
        }

        #endregion

        /*
        public static void UnityPoseToApiPose(Pose unityPose, out NativeMat4f apiPose)
        {
            Matrix4x4 glWorld_T_glLocal = Matrix4x4.TRS(unityPose.position, unityPose.rotation, Vector3.one);
            Matrix4x4 unityWorld_T_glWorld = Matrix4x4.Scale(new Vector3(1, 1, -1));
            Matrix4x4 unityWorld_T_unityLocal = unityWorld_T_glWorld * glWorld_T_glLocal * unityWorld_T_glWorld.inverse;

            Vector3 position = unityWorld_T_unityLocal.GetColumn(3);
            Quaternion rotation = Quaternion.LookRotation(unityWorld_T_unityLocal.GetColumn(2),
                unityWorld_T_unityLocal.GetColumn(1));

            Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
            apiPose = new NativeMat4f(matrix);
        }

        public static void ApiPoseToUnityPose(NativeMat4f apiPose, out Pose unityPose)
        {
            Matrix4x4 glWorld_T_glLocal = apiPose.ToUnityMat4f();
            Matrix4x4 unityWorld_T_glWorld = Matrix4x4.Scale(new Vector3(1, 1, -1));
            Matrix4x4 unityWorld_T_unityLocal = unityWorld_T_glWorld * glWorld_T_glLocal * unityWorld_T_glWorld.inverse;

            Vector3 position = unityWorld_T_unityLocal.GetColumn(3);
            Quaternion rotation = unityWorld_T_unityLocal.rotation;
            unityPose = new Pose(position, rotation);
        }

        public static void ApiPoseToUnityMatrix(NativeMat4f apiPose, out Matrix4x4 unityMatrix)
        {
            Matrix4x4 glWorld_T_glLocal = apiPose.ToUnityMat4f();
            Matrix4x4 unityWorld_T_glWorld = Matrix4x4.Scale(new Vector3(1, 1, -1));
            Matrix4x4 unityWorld_T_unityLocal = unityWorld_T_glWorld * glWorld_T_glLocal * unityWorld_T_glWorld.inverse;

            unityMatrix = unityWorld_T_unityLocal;
        }

        public static NativeMat4f GetProjectionMatrixFromFov(NativeFov4f fov, float z_near, float z_far)
        {
            NativeMat4f pm = NativeMat4f.identity;

            float l = -fov.left_tan;
            float r = fov.right_tan;
            float t = fov.top_tan;
            float b = -fov.bottom_tan;

            pm.column0.X = 2f / (r - l);
            pm.column1.Y = 2f / (t - b);

            pm.column2.X = (r + l) / (r - l);
            pm.column2.Y = (t + b) / (t - b);
            pm.column2.Z = (z_near + z_far) / (z_near - z_far);
            pm.column2.W = -1f;

            pm.column3.Z = (2 * z_near * z_far) / (z_near - z_far);
            pm.column3.W = 0f;

            return pm;
        }
        */
    }
    /// @endcond
}
