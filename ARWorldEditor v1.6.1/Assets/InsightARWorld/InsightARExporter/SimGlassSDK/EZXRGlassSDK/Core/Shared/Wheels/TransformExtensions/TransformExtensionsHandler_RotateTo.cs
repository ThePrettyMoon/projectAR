using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class TransformExtensionsHandler_RotateAdd : MonoBehaviour
    {
        public Vector3 eulerAngles;
        public float duration;
        public Space space;
        /// <summary>
        /// 计时器
        /// </summary>
        float timer;
        /// <summary>
        /// 是否开始Lerp
        /// </summary>
        bool start;

        public void Init(Vector3 eulerAngles, float duration, Space space = Space.Self)
        {
            this.eulerAngles = eulerAngles;
            this.duration = duration;
            this.space = space;
            timer = 0;
            start = true;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (start)
            {
                timer += Time.deltaTime;
                if (space == Space.Self)
                {
                    transform.localEulerAngles += eulerAngles * Time.deltaTime / duration;
                }
                else
                {
                    transform.eulerAngles += eulerAngles * Time.deltaTime / duration; ;
                }
                if (timer >= duration)
                {
                    start = false;
                    timer = 0;
                }
            }
        }


    }
}