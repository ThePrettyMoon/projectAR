using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public static class TransformExtensions
    {
        #region 作为子物体，作为父物体
        /// <summary>
        /// 调用一次之后，会像子物体一样跟随parent的transform变化而变化，如果需要取消请调用CancelActAsChild
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        public static void ActAsChild(this Transform child, Transform parent)
        {
            TransformExtensionsHandler_ActAs transformExtensionsHandler = child.gameObject.GetComponent<TransformExtensionsHandler_ActAs>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = child.gameObject.AddComponent<TransformExtensionsHandler_ActAs>();
            }
            transformExtensionsHandler.Init(child, parent);
        }

        /// <summary>
        /// 取消ActAsChild造成的transform影响
        /// </summary>
        /// <param name="child"></param>
        public static void CancelActAsChild(this Transform child)
        {
            TransformExtensionsHandler_ActAs transformExtensionsHandler = child.GetComponent<TransformExtensionsHandler_ActAs>();
            if (transformExtensionsHandler != null)
            {
                Object.Destroy(transformExtensionsHandler);
            }
        }

        /// <summary>
        /// 调用一次之后，child会像子物体一样跟随此transform变化而变化，如果需要取消请调用CancelActAsParent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void ActAsParent(this Transform parent, Transform child)
        {
            TransformExtensionsHandler_ActAs transformExtensionsHandler = parent.gameObject.GetComponent<TransformExtensionsHandler_ActAs>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = parent.gameObject.AddComponent<TransformExtensionsHandler_ActAs>();
            }
            transformExtensionsHandler.Init(child, parent);
        }

        /// <summary>
        /// 取消ActAsParent造成的transform影响
        /// </summary>
        /// <param name="parent"></param>
        public static void CancelActAsParent(this Transform parent)
        {
            TransformExtensionsHandler_ActAs transformExtensionsHandler = parent.GetComponent<TransformExtensionsHandler_ActAs>();
            if (transformExtensionsHandler != null)
            {
                Object.Destroy(transformExtensionsHandler);
            }
        }
        #endregion

        #region 保持与另外一个物体的相对位置
        public static void CombinePositionWith(this Transform self, Transform other)
        {
            TransformExtensionsHandler_CombinePositionWith transformExtensionsHandler = self.gameObject.GetComponent<TransformExtensionsHandler_CombinePositionWith>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = self.gameObject.AddComponent<TransformExtensionsHandler_CombinePositionWith>();
            }
            transformExtensionsHandler.Init(self, other);
        }

        public static void CancelCombinePositionWith(this Transform child)
        {
            TransformExtensionsHandler_CombinePositionWith transformExtensionsHandler = child.GetComponent<TransformExtensionsHandler_CombinePositionWith>();
            if (transformExtensionsHandler != null)
            {
                Object.Destroy(transformExtensionsHandler);
            }
        }
        #endregion

        #region 得到子物体在父物体下的LocalRotation
        /// <summary>
        /// 得到子物体在父物体下的LocalRotation
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Quaternion GetLocalRotation(this Transform child, Transform parent)
        {
            return new Quaternion(-parent.rotation.x, -parent.rotation.y, -parent.rotation.z, parent.rotation.w) * child.rotation;
        }

        /// <summary>
        /// 得到子物体在父物体下的LocalRotation
        /// </summary>
        /// <param name="child">子物体</param>
        /// <param name="parentRotation">父物体的rotation</param>
        /// <returns></returns>
        public static Quaternion GetLocalRotation(this Transform child, Quaternion parentRotation)
        {
            return new Quaternion(-parentRotation.x, -parentRotation.y, -parentRotation.z, parentRotation.w) * child.rotation;
        }

        /// <summary>
        /// 得到子物体在父物体下的LocalRotation
        /// </summary>
        /// <param name="childRotation">子物体的rotation</param>
        /// <param name="parentRotation">父物体的rotation</param>
        /// <returns></returns>
        public static Quaternion GetLocalRotation(this Quaternion childRotation, Quaternion parentRotation)
        {
            return new Quaternion(-parentRotation.x, -parentRotation.y, -parentRotation.z, parentRotation.w) * childRotation;
        }
        #endregion

        #region 从当前Pose移动并旋转到目标Pose
        /// <summary>
        /// 从当前Pose移动并旋转到目标Pose
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target">目标Pose</param>
        /// <param name="duration">花费的时长</param>
        public static void MoveAndRotateToTarget(this Transform self, Transform target, float duration, System.Action callBack = null)
        {
            TransformExtensionsHandler_MoveAndRotate transformExtensionsHandler = self.gameObject.GetComponent<TransformExtensionsHandler_MoveAndRotate>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = self.gameObject.AddComponent<TransformExtensionsHandler_MoveAndRotate>();
            }
            transformExtensionsHandler.Init(target, duration, callBack);
        }

        public static void CancelMoveAndRotateToTarget(this Transform self)
        {
            TransformExtensionsHandler_MoveAndRotate transformExtensionsHandler = self.GetComponent<TransformExtensionsHandler_MoveAndRotate>();
            if (transformExtensionsHandler != null)
            {
                Object.Destroy(transformExtensionsHandler);
            }
        }

        public static void MoveAndRotateToTarget(this Transform self, Pose target, float duration, System.Action callBack = null)
        {
            TransformExtensionsHandler_MoveAndRotate transformExtensionsHandler = self.gameObject.GetComponent<TransformExtensionsHandler_MoveAndRotate>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = self.gameObject.AddComponent<TransformExtensionsHandler_MoveAndRotate>();
            }
            transformExtensionsHandler.Init(target, duration, callBack);
        }
        #endregion

        #region 向特定方向每秒移动特定距离
        /// <summary>
        /// 每秒移动特定距离
        /// </summary>
        /// <param name="self"></param>
        /// <param name="translation"></param>
        public static void MoveAdd(this Transform self, Vector3 translation)
        {
            TransformExtensionsHandler_MoveAdd transformExtensionsHandler = self.gameObject.GetComponent<TransformExtensionsHandler_MoveAdd>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = self.gameObject.AddComponent<TransformExtensionsHandler_MoveAdd>();
            }
            transformExtensionsHandler.Init(translation);
        }

        public static void CancelMoveAdd(this Transform self)
        {
            TransformExtensionsHandler_MoveAdd transformExtensionsHandler = self.GetComponent<TransformExtensionsHandler_MoveAdd>();
            if (transformExtensionsHandler != null)
            {
                Object.DestroyImmediate(transformExtensionsHandler);
            }
        }
        #endregion

        #region 增量旋转指定角度
        /// <summary>
        /// 从当前旋转角度增加指定旋转角度
        /// </summary>
        /// <param name="self"></param>
        /// <param name="eulerAngles">目标角度</param>
        /// <param name="duration">花费的时长</param>
        public static void RotateAdd(this Transform self, Vector3 eulerAngles, float duration)
        {
            TransformExtensionsHandler_RotateAdd transformExtensionsHandler = self.gameObject.GetComponent<TransformExtensionsHandler_RotateAdd>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = self.gameObject.AddComponent<TransformExtensionsHandler_RotateAdd>();
            }
            transformExtensionsHandler.Init(eulerAngles, duration);
        }

        public static void CancelRotateAdd(this Transform self)
        {
            TransformExtensionsHandler_RotateAdd transformExtensionsHandler = self.GetComponent<TransformExtensionsHandler_RotateAdd>();
            if (transformExtensionsHandler != null)
            {
                Object.DestroyImmediate(transformExtensionsHandler);
            }
        }
        #endregion

        #region 只能绕Y旋转的LookAt，其他两轴是锁定的
        /// <summary>
        /// 只能绕特定轴旋转的LookAt，前向是z轴
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target">要LookAt的目标物体</param>
        /// <param name="direction">要用哪个方向朝向目标，只能是前后左右</param>
        public static void LookAtWithAxisLock(this Transform self, Transform target, Vector3 direction)
        {
            Dictionary<Vector3, Vector3> dirDic = new Dictionary<Vector3, Vector3>
        {
            {Vector3.forward,new Vector3(0,0,0) },
            {Vector3.back,new Vector3(180,0,0) },
            {Vector3.right,new Vector3(0,-90,0) },
            {Vector3.left,new Vector3(0,90,0) },
            //{Vector3.up,new Vector3(0,90,90) },
            //{Vector3.down,new Vector3(0,0,90) },
        };
            Vector3 localPos = self.InverseTransformPoint(target.position);
            Vector3 projectPos = Vector3.ProjectOnPlane(localPos, Vector3.up);
            //Debug.Log(localPos.ToString() + projectPos.ToString());
            float angle = Vector3.Dot(direction, projectPos.normalized);
            //Debug.Log(angle);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, projectPos);
            Debug.Log(projectPos.ToString());
            self.rotation = self.rotation * rotation * Quaternion.Euler(dirDic[direction]);
        }
        #endregion

        #region 缩放指定scale量
        /// <summary>
        /// 缩放指定scale量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="scale">目标scale</param>
        /// <param name="duration">花费的时长</param>
        public static void ScaleAdd(this Transform self, Vector3 scale, float duration)
        {
            TransformExtensionsHandler_ScaleAdd transformExtensionsHandler = self.gameObject.GetComponent<TransformExtensionsHandler_ScaleAdd>();
            if (transformExtensionsHandler == null)
            {
                transformExtensionsHandler = self.gameObject.AddComponent<TransformExtensionsHandler_ScaleAdd>();
            }
            transformExtensionsHandler.Init(scale, duration);
        }

        public static void CancelScaleAdd(this Transform self)
        {
            TransformExtensionsHandler_ScaleAdd transformExtensionsHandler = self.GetComponent<TransformExtensionsHandler_ScaleAdd>();
            if (transformExtensionsHandler != null)
            {
                Object.DestroyImmediate(transformExtensionsHandler);
            }
        }
        #endregion
    }
}