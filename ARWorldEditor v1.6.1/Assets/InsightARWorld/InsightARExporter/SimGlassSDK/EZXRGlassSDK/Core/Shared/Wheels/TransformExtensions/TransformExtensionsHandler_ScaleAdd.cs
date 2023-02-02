using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class TransformExtensionsHandler_ScaleAdd : MonoBehaviour
    {
        public Vector3 amount;
        public float duration;
        /// <summary>
        /// 计时器
        /// </summary>
        float timer;
        /// <summary>
        /// 是否开始Lerp
        /// </summary>
        bool start;

        public void Init(Vector3 amount, float duration, Space space = Space.Self)
        {
            this.amount = amount;
            this.duration = duration;
            timer = 0;
            start = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (start)
            {
                timer += Time.deltaTime;
                transform.localScale += amount * Time.deltaTime / duration;
                if (timer >= duration)
                {
                    start = false;
                    timer = 0;
                }
            }
        }


    }
}