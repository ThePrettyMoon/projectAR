using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wheels.Unity
{
    public class TransformExtensionsHandler_MoveAndRotate : MonoBehaviour
    {
        public Transform target;
        public Pose targetPose;
        public float duration;
        public Action callBack;
        bool isPose;
        /// <summary>
        /// 相机在做Lerp前的旋转信息
        /// </summary>
        Quaternion startRotation;
        /// <summary>
        /// 相机在做Lerp前的位置信息
        /// </summary>
        Vector3 startPosition;
        /// <summary>
        /// Lerp用的计时器，用于将相机Lerp移动到指定观察点位，同时Lerp旋转与指定观察点位一致
        /// </summary>
        float timer;
        /// <summary>
        /// 是否开始Lerp
        /// </summary>
        bool start;

        public void Init(Transform target, float duration, Action callBack = null)
        {
            startRotation = transform.rotation;
            startPosition = transform.position;
            this.target = target;
            this.duration = duration;
            this.callBack = callBack;
            start = true;
        }

        public void Init(Pose targetPose, float duration, Action callBack = null)
        {
            isPose = true;
            startRotation = transform.rotation;
            startPosition = transform.position;
            this.targetPose = targetPose;
            this.duration = duration;
            this.callBack = callBack;
            start = true;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (start)
            {
                if (isPose)
                {
                    timer += (1.0f / duration * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(startRotation, targetPose.rotation, timer);
                    transform.position = Vector3.Lerp(startPosition, targetPose.position, timer);
                    if (timer >= 1.0f)
                    {
                        start = false;
                        timer = 0;
                        if (callBack != null)
                        {
                            callBack();
                        }
                    }
                }
                else
                {
                    timer += (1.0f / duration * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(startRotation, target.rotation, timer);
                    transform.position = Vector3.Lerp(startPosition, target.position, timer);
                    if (timer >= 1.0f)
                    {
                        start = false;
                        timer = 0;
                        if (callBack != null)
                        {
                            callBack();
                        }
                    }
                }
            }
        }


    }
}