using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class TransformExtensionsHandler_MoveAdd : MonoBehaviour
    {
        public Vector3 translation;
        bool start;

        public void Init(Vector3 translation)
        {
            this.translation = translation;
            start = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (start)
            {
                transform.Translate(translation * Time.deltaTime, Space.World);
            }
        }


    }
}