using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EZXR.Glass.Hand;
using EZXR.Glass.Common;

namespace EZXR.Glass.UI
{
    public class SpacialTrigger : SpacialSelectable
    {
        #region InternalUse
        public BoxCollider _collider;
        #endregion

        public UnityEvent onTriggerEnter;
        /// <summary>
        /// 用于播放触发音效
        /// </summary>
        AudioSource audioSource;
        AudioClip[] clips;
        /// <summary>
        /// 为了避免手抖动造成的在边缘瞬间触发多次Trigger
        /// </summary>
        bool allowTrigger = true;
        float timer;

        protected override void Awake()
        {
            base.Awake();

            if (Application.isPlaying)
            {
                //将自身注册到ARUIEventSystem中，以在被射线碰到的时候被回调
                SpacialUIEventSystem.RegisterCallBack(GetComponent<Collider>(), this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            clips = new AudioClip[1];
            clips[0] = ResourcesManager.Load<AudioClip>("Sounds/Trigger");
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (Application.isPlaying)
            {
                if (!allowTrigger)
                {
                    timer += Time.deltaTime;
                    if (timer > 0.5f)
                    {
                        allowTrigger = true;
                        timer = 0;
                    }
                }
            }
            else
            {
                //#if UNITY_EDITOR
                //                _collider.size = size;
                //#endif
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (allowTrigger && other.name.Contains("Index_4"))
            {
                allowTrigger = false;
                audioSource.PlayOneShot(clips[0]);
                onTriggerEnter.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {

        }

        private void OnTriggerExit(Collider other)
        {

        }

        public override void OnRayCastHit(HandInfo handInfo)
        {

        }
    }
}