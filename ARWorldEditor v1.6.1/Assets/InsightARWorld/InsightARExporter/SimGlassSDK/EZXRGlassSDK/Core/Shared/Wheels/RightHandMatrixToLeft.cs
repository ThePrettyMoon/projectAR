using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class RightHandMatrixToLeft : MonoBehaviour
    {
        private static Matrix4x4 m_Origin;
        private static Matrix4x4 m_Converted;

        /// <summary>
        /// right hand matrix to left hand
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public static void MatrixToTRS(List<float> matrix, out Vector3 position, out Quaternion rotation)
        {
            //set matrix
            m_Origin = new Matrix4x4();
            m_Origin.SetRow(0, new Vector4(matrix[0], matrix[1], matrix[2], matrix[3]));
            m_Origin.SetRow(1, new Vector4(matrix[4], matrix[5], matrix[6], matrix[7]));
            m_Origin.SetRow(2, new Vector4(matrix[8], matrix[9], matrix[10], matrix[11]));
            m_Origin.SetRow(3, new Vector4(matrix[12], matrix[13], matrix[14], matrix[15]));

            MatrixToTRS(m_Origin, out position, out rotation);
        }

        /// <summary>
        /// right hand matrix to left hand
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public static void MatrixToTRS(Matrix4x4 m_Origin, out Vector3 position, out Quaternion rotation)
        {
            //matrix transformation
            Matrix4x4 exchangeMatrix_R = new Matrix4x4(new Vector4(-1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            Matrix4x4 exchangeMatrix_L = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, -1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            m_Converted = exchangeMatrix_L * m_Origin * exchangeMatrix_R;

            //Get Rotation and Position
            Vector4 vy = m_Converted.GetColumn(1);
            Vector4 vz = m_Converted.GetColumn(2);
            Quaternion q = Quaternion.identity;
            if (!float.IsNaN(vz.x) && vz != Vector4.zero && vy != Vector4.zero)
            {
                q = Quaternion.LookRotation(new Vector3(vz.x, vz.y, vz.z), new Vector3(vy.x, vy.y, vy.z));
                rotation = q;
                position = new Vector3(m_Converted.GetColumn(3).x, m_Converted.GetColumn(3).y, m_Converted.GetColumn(3).z);
            }
            else
            {
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }
        }
    }
}