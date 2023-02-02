using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    [RequireComponent(typeof(DrawBounds))]
    public class TransformWithBounds : MonoBehaviour
    {
        DrawBounds drawBounds;
        /// <summary>
        /// 缩放控制角的Prefab
        /// </summary>
        GameObject prefab_ScaleCorner;
        /// <summary>
        /// 8个角，用来控制缩放
        /// </summary>
        Transform[] corners = new Transform[8];
        /// <summary>
        /// 旋转握柄的Prefab
        /// </summary>
        GameObject prefab_RotateHandle;
        /// <summary>
        /// 用来控制沿X轴旋转的握柄
        /// </summary>
        [HideInInspector]
        public Transform[] rotate_X = new Transform[4];
        /// <summary>
        /// 水平方向4个Bound的中间点，用来控制水平旋转
        /// </summary>
        [HideInInspector]
        public Transform[] rotate_Y = new Transform[4];
        /// <summary>
        /// 用来控制沿Z轴旋转的握柄
        /// </summary>
        [HideInInspector]
        public Transform[] rotate_Z = new Transform[4];

        // Start is called before the first frame update
        void Start()
        {
            prefab_ScaleCorner = ResourcesManager.Load<GameObject>("TransformWithBounds/ScaleCorner");
            prefab_RotateHandle = ResourcesManager.Load<GameObject>("TransformWithBounds/RotateHandle");

            drawBounds = GetComponent<DrawBounds>();

            //生成缩放控制角，并根据实例化的角的位置来设置为正确的旋转角度
            corners[0] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[0], Quaternion.identity, transform).transform;
            corners[0].localEulerAngles = new Vector3(0, -180, -90);
            corners[1] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[1], Quaternion.identity, transform).transform;
            corners[1].localEulerAngles = new Vector3(0, -180, -180);
            corners[2] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[2], Quaternion.identity, transform).transform;
            corners[2].localEulerAngles = new Vector3(0, 90, 0);
            corners[3] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[3], Quaternion.identity, transform).transform;
            corners[3].localEulerAngles = new Vector3(0, 180, 0);
            corners[4] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[4], Quaternion.identity, transform).transform;
            corners[4].localEulerAngles = new Vector3(0, 0, -180);
            corners[5] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[5], Quaternion.identity, transform).transform;
            corners[5].localEulerAngles = new Vector3(90, 0, 0);
            corners[6] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[6], Quaternion.identity, transform).transform;
            corners[6].localEulerAngles = new Vector3(0, 0, 0);
            corners[7] = Instantiate(prefab_ScaleCorner, drawBounds.vertices[7], Quaternion.identity, transform).transform;
            corners[7].localEulerAngles = new Vector3(0, -90, 0);

            //生成绕X轴旋转的控制杆
            rotate_X[0] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[0], Quaternion.identity, transform).transform;
            rotate_X[1] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[2], Quaternion.identity, transform).transform;
            rotate_X[2] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[6], Quaternion.identity, transform).transform;
            rotate_X[3] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[4], Quaternion.identity, transform).transform;
            rotate_X[0].LookAt(drawBounds.vertices[0], transform.up);
            rotate_X[1].LookAt(drawBounds.vertices[2], transform.up);
            rotate_X[2].LookAt(drawBounds.vertices[6], transform.up);
            rotate_X[3].LookAt(drawBounds.vertices[4], transform.up);

            //生成绕Y轴旋转的控制杆
            rotate_Y[0] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[1], Quaternion.identity, transform).transform;
            rotate_Y[1] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[3], Quaternion.identity, transform).transform;
            rotate_Y[2] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[7], Quaternion.identity, transform).transform;
            rotate_Y[3] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[5], Quaternion.identity, transform).transform;
            rotate_Y[0].LookAt(drawBounds.vertices[1], transform.forward);
            rotate_Y[1].LookAt(drawBounds.vertices[3], transform.forward);
            rotate_Y[2].LookAt(drawBounds.vertices[7], transform.forward);
            rotate_Y[3].LookAt(drawBounds.vertices[5], transform.forward);

            //生成绕Z轴旋转的控制杆
            rotate_Z[0] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[8], Quaternion.identity, transform).transform;
            rotate_Z[1] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[9], Quaternion.identity, transform).transform;
            rotate_Z[2] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[10], Quaternion.identity, transform).transform;
            rotate_Z[3] = Instantiate(prefab_RotateHandle, drawBounds.midPoints[11], Quaternion.identity, transform).transform;
            rotate_Z[0].LookAt(drawBounds.vertices[0], transform.up);
            rotate_Z[1].LookAt(drawBounds.vertices[1], transform.up);
            rotate_Z[2].LookAt(drawBounds.vertices[2], transform.up);
            rotate_Z[3].LookAt(drawBounds.vertices[3], transform.up);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}