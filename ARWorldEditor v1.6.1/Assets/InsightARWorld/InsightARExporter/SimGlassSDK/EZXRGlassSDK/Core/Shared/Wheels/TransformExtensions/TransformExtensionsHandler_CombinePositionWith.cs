using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class TransformExtensionsHandler_CombinePositionWith : MonoBehaviour
    {
        Transform _other;
        Transform _self;
        Vector3 p;
        bool start = false;

        public void Init(Transform self, Transform other)
        {
            _self = self;
            _other = other;
            p = other.InverseTransformPoint(self.position);
            start = true;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (start)
            {
                _self.position = _other.position + _other.rotation * p;
            }
        }
    }
}