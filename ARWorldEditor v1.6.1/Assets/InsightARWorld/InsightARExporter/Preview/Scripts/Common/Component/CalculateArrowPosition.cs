using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dongjian.LargeScale
{
    /// <summary>
    /// 计算UI箭头位置
    /// </summary>
    public class CalculateArrowPosition
    {
        #region utility
        /// <summary>
        /// 计算图标rotation
        /// </summary>
        /// <returns></returns>
        public static Quaternion CalculateRotation(Camera camera, Vector3 stuffPosition)
        {

            float angle = CalculateAngle(camera, stuffPosition);
            return Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

        }

        /// <summary>
        /// 计算图标anchorposition
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static Vector3 CalculateAnchorPosition(Camera camera, RectTransform rectTrans,Vector3 stuffPosition,
            int screenHeight = 1920,int screenWidth = 1080,int marginHorizontal = 160,int marginVertical = 320)
        {
            float angle = CalculateAngle(camera, stuffPosition);
            float cosA = Mathf.Cos(Mathf.Deg2Rad * angle);
            float sinA = Mathf.Sin(Mathf.Deg2Rad * angle);

            float positionZ = rectTrans.anchoredPosition3D.z;
            return new Vector3((screenWidth - marginHorizontal) / 2.0f * cosA, (screenHeight - marginVertical) / 2.0f * sinA, positionZ);
        }


        /// <summary>
        /// 计算图标的角度
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private static float CalculateAngle(Camera camera, Vector3 stuffPosition)
        {
            Transform camTrans = camera.transform;
            Vector3 stuffOffset = stuffPosition - camTrans.position;
            Vector3 camRight = camTrans.right;
            Vector3 camUp = camTrans.up;
            Vector3 camForward = camTrans.forward;

            float cosRight = Vector3.Dot(camRight, stuffOffset) / stuffOffset.magnitude; //在相机right轴投影

            float cosUp = Vector3.Dot(camUp, stuffOffset) / stuffOffset.magnitude; //在相机up轴投影


            float length = Mathf.Sqrt(cosRight * cosRight + cosUp * cosUp);
            if (length < 0.0001f)
                return 0.0f;

            // 在right - O - up平面，计算投影角度
            float cosA = cosRight / length;
            float sinA = cosUp / length;


            float angle = 0;

            if (sinA >= 0 && cosA >= 0)  //第一象限
                angle = Mathf.Rad2Deg * Mathf.Asin(sinA);
            else if (sinA >= 0 && cosA < 0)    //第二象限
                angle = Mathf.Rad2Deg * Mathf.Acos(cosA);
            else if (sinA < 0 && cosA < 0)    //第三象限
                angle = 180 - Mathf.Rad2Deg * Mathf.Asin(sinA);
            else  //第四象限
                angle = Mathf.Rad2Deg * Mathf.Asin(sinA);

            return angle;

        }
        #endregion

    }
}
