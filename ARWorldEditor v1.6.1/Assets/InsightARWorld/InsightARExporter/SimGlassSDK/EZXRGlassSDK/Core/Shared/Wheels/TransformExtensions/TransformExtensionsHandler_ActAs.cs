using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class TransformExtensionsHandler_ActAs : MonoBehaviour
    {
        Transform _parent;
        Transform _child;
        /// <summary>
        /// 旋转
        /// </summary>
        Quaternion q;
        /// <summary>
        /// 位置
        /// </summary>
        Vector3 p;
        /// <summary>
        /// 缩放
        /// </summary>
        Vector3 s_Parent;
        Vector3 s_Child;
        bool start = false;

        public void Init(Transform child, Transform parent)
        {
            _child = child;
            _parent = parent;
            //得到子物体在父物体下的LocalRotation
            q = new Quaternion(-parent.rotation.x, -parent.rotation.y, -parent.rotation.z, parent.rotation.w) * child.rotation;
            //得到子物体在父物体坐标系下的LocalPosition
            p = parent.InverseTransformPoint(child.position);
            s_Child = child.localScale;
            s_Parent = parent.localScale;
            start = true;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (start)
            {
                _child.rotation = _parent.rotation * q;
                _child.position = _parent.position + _parent.rotation * p;
                _child.localScale = _parent.localScale.x / s_Parent.x * s_Child;
            }
        }
    }
}