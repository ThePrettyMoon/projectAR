using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EZXR.Glass.Hand;
using UnityEditor;
using EZXR.Glass.Common;

namespace EZXR.Glass.UI
{
    [ExecuteInEditMode]
    public class SpacialButton : SpacialSelectable
    {
        #region InternalUse
        public BoxCollider _collider;
        public Transform _iconMesh;
        public MeshRenderer _iconRenderer;
        #endregion

        /// <summary>
        /// 按钮的图标
        /// </summary>
        public Texture2D icon;
        public Vector3 iconSize = new Vector3(0.1f, 0.1f, 0.1f);
        protected MaterialPropertyBlock icon_MaterialPropertyBlock;
        /// <summary>
        /// 浮层式Button的浮层，当用指尖进入触发区的时候显示此浮层
        /// </summary>
        public GameObject buttonHover;
        /// <summary>
        /// 按钮前置触发区的厚度(单位:m),用户手指进入这个区域后开始按钮判定流程
        /// </summary>
        public float preTriggerZoneThickness = 0.15f;
        /// <summary>
        /// 按钮后置触发区的厚度(单位:m),决定了用户手指按过(穿过)按钮后还保持按下状态的距离,当手指离开这个后置触发区的时候按钮将会弹起
        /// </summary>
        public float rearTriggerZoneThickness = 0.05f;
        /// <summary>
        /// 按钮被按下
        /// </summary>
        public bool pressed;
        /// <summary>
        /// 0是未按下，1是按下但是没按到底，2是按下且按到底
        /// </summary>
        public byte state;
        /// <summary>
        /// 按键可视部分的高度(单位:m)
        /// </summary>
        float height;
        /// <summary>
        /// 当前是哪个指尖点点击到了按键，用于计算按下的z距离
        /// </summary>
        Transform other;
        /// <summary>
        /// 当前被点击的Button
        /// </summary>
        SpacialButton curButton;
        /// <summary>
        /// 指尖点当前在按键坐标系下的坐标
        /// </summary>
        Vector3 curPos;
        /// <summary>
        /// 当前指尖点距离按键底面的z距离
        /// </summary>
        float length;
        /// <summary>
        /// 是否开始点击，只要有指尖点进入触发区就开始点击判断流程
        /// </summary>
        bool start = false;
        //bool rayStart = false;
        /// <summary>
        /// 用于播放按键按下和弹起的音效
        /// </summary>
        AudioSource audioSource;
        /// <summary>
        /// 0是按键按下的音效，1是按键弹起的音效
        /// </summary>
        static AudioClip[] clips;
        HandInfo handInfo;

        public UnityEvent clicked;

        protected override void Awake()
        {
            base.Awake();

            //将自身注册到ARUIEventSystem中，以在被射线碰到的时候被回调
            SpacialUIEventSystem.RegisterCallBack(GetComponent<Collider>(), this);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (Application.isPlaying)
            {
                buttonHover.SetActive(false);
                icon_MaterialPropertyBlock = new MaterialPropertyBlock();
                _iconRenderer.GetPropertyBlock(icon_MaterialPropertyBlock);

                //_collider = GetComponent<BoxCollider>();
                height = size.z;
                //material = _visual.GetChild(0).GetComponent<Renderer>().material;
                audioSource = GetComponent<AudioSource>();
                clips = new AudioClip[2];
                clips[0] = ResourcesManager.Load<AudioClip>("Sounds/ButtonDown");
                clips[1] = ResourcesManager.Load<AudioClip>("Sounds/ButtonUp");
            }
            else
            {
#if UNITY_EDITOR
                icon_MaterialPropertyBlock = new MaterialPropertyBlock();
                _iconRenderer.GetPropertyBlock(icon_MaterialPropertyBlock);
#endif
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (Application.isPlaying)
            {
                //射线点击
                if (handInfo != null)
                {
                    //如果当前射线射到的物体不再是此按钮
                    if (handInfo.CurRayContactingTarget == null || handInfo.CurRayContactingTarget.gameObject != gameObject)
                    {
                        ////将按键颜色变成白色未选中效果
                        //SetColor(new Color(1, 1, 1, 1));

                        handInfo = null;
                    }
                    else
                    {
                        if (handInfo.isPinching)
                        {
                            //将按键按到底
                            _mesh.localScale = new Vector3(_mesh.localScale.x, _mesh.localScale.y, 0.001f);

                            _iconMesh.localPosition = new Vector3(0, 0, -_mesh.localScale.z / 2.0f / size.z);

                            ////将按键颜色变成按下的灰色效果
                            //SetColor(new Color(0.5f, 0.5f, 0.5f, 1));

                            ButtonDown();
                        }
                        else
                        {
                            //if(material.color != new Color(0.8f, 0.8f, 0.8f, 1))
                            //{
                            //    //播放按键被射线触碰的音效
                            //    audioSource.PlayOneShot(clips[2]);
                            //}

                            ////将按键颜色变成浅灰色选中效果
                            //SetColor(new Color(0.8f, 0.8f, 0.8f, 1));

                            _mesh.localScale = new Vector3(_mesh.localScale.x, _mesh.localScale.y, height);

                            _iconMesh.localPosition = new Vector3(0, 0, -_mesh.localScale.z / 2.0f / size.z);

                            ButtonUp();

                            //handInfo = null;
                        }
                    }
                }
                else
                {
                    ////没有射线选中，所以将按键颜色变成白色未选中效果
                    //material.color = new Color(1, 1, 1, 1);
                    //handInfo = null;

                    if (start)//直接近距离按压
                    {
                        if (other == null || !other.gameObject.activeInHierarchy)//手丢失了的话直接ButtonUp
                        {
                            ButtonUp();
                            start = false;
                            curPos = new Vector3(9999, 9999, 9999);
                        }
                        else//手正常存在的话实时算出指尖在Button局部坐标系下的位置
                        {
                            //先算出指尖点当前在按键坐标系下的坐标
                            curPos = transform.InverseTransformPoint(other.position);
                        }

                        //然后算出当前指尖点距离按键底面的z距离，这个0.005是指尖点的半径长度
                        length = Mathf.Clamp(curPos.z - 0.005f, 0.001f, height);

                        //将算出的距离直接赋给按键的scale（因为此处按键本身就是按照实际尺寸进行的缩放，所以计算出的length就是scale.z）
                        _mesh.localScale = new Vector3(_mesh.localScale.x, _mesh.localScale.y, length);

                        _iconMesh.localPosition = new Vector3(0, 0, -_mesh.localScale.z / 2.0f / size.z);

                        ////将按键的整个键程的（0-1）映射为颜色的（0.5-1），即灰色到白色，未按下的时候为白色
                        //float color = 1.0f / height * length / 2 + 0.5f;
                        //SetColor(new Color(color, color, color, 1));

                        //更新按钮状态
                        if (length == height)//按钮已经弹起来
                        {
                            ButtonUp();
                        }
                        else if (length == 0.001f)//按钮按到底
                        {
                            ButtonDown();
                        }
                        else if (length < height)//按钮按下但没到底
                        {
                            if (state == 0)
                            {
                                //播放按键按下的音效
                                audioSource.PlayOneShot(clips[0]);
                            }
                            state = 1;
                        }

                        ////这个0.005是指尖点的半径长度，当指尖刚接触到按键的时候，指尖点在按键坐标系下的z距离刚好是height+0.005，所以这个距离-curPos.z如果小于0的话就说明指尖还没有碰到按键
                        //if (height + 0.005f - curPos.z < 0)
                        //{
                        //    start = false;
                        //    other = null;
                        //}
                    }
                }
            }
            else
            {
#if UNITY_EDITOR
                _iconRenderer.sortingOrder = sortingOrder + 1;
                _mesh.localScale = size;
                _collider.size = new Vector3(size.x, size.y, size.z + preTriggerZoneThickness + rearTriggerZoneThickness);
                _collider.center = new Vector3(0, 0, preTriggerZoneThickness - _collider.size.z / 2.0f);

                //确保icon_MaterialPropertyBlock不为空
                if (icon_MaterialPropertyBlock == null)
                {
                    icon_MaterialPropertyBlock = new MaterialPropertyBlock();
                    _iconRenderer.GetPropertyBlock(icon_MaterialPropertyBlock);
                }

                //icon有更改的话同步修改到Material
                if (icon != null)
                {
                    _iconRenderer.gameObject.SetActive(true);
                    icon_MaterialPropertyBlock.SetTexture("_MainTex", icon);
                }
                else
                {
                    _iconRenderer.gameObject.SetActive(false);
                    //重置materialPropertyBlock，因为无法直接SetTexture为null
                    icon_MaterialPropertyBlock.Clear();

                }
                _iconRenderer.SetPropertyBlock(icon_MaterialPropertyBlock);

                //设置UI尺寸
                _iconMesh.localScale = new Vector3(iconSize.x / size.x, iconSize.y / size.y, 0.0001f);
#endif
            }
        }

        protected override void OnDrawGizmos()
        {
//            base.OnDrawGizmos();

//            if (!Application.isPlaying)
//            {
//#if UNITY_EDITOR
//                //确保icon_MaterialPropertyBlock不为空
//                if (icon_MaterialPropertyBlock == null)
//                {
//                    icon_MaterialPropertyBlock = new MaterialPropertyBlock();
//                    _iconRenderer.GetPropertyBlock(icon_MaterialPropertyBlock);
//                }

//                //icon有更改的话同步修改到Material
//                if (icon != null)
//                {
//                    _iconRenderer.gameObject.SetActive(true);
//                    icon_MaterialPropertyBlock.SetTexture("_MainTex", icon);
//                }
//                else
//                {
//                    _iconRenderer.gameObject.SetActive(false);
//                    //重置materialPropertyBlock，因为无法直接SetTexture为null
//                    icon_MaterialPropertyBlock.Clear();

//                }
//                _iconRenderer.SetPropertyBlock(icon_MaterialPropertyBlock);
//#endif
//            }

//            //设置UI尺寸
//            _iconMesh.localScale = new Vector3(iconSize.x / size.x, iconSize.y / size.y, 0.00001f);
        }

        private void OnTriggerStay(Collider other)
        {
            if (Application.isPlaying)
            {
                if (!start)
                {
                    //对方必须具备rigidbody才会触发ARButton的Tigger
                    if (other.name.Contains("Index_4"))
                    {
                        if (_collider.bounds.Contains(other.transform.position) && (transform.InverseTransformPoint(other.transform.position).z - 0.005f > height))
                        {
                            curButton = this;
                            this.other = other.transform;
                            start = true;
                            //显示浮层
                            buttonHover.SetActive(true);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Application.isPlaying)
            {
                if (other.name.Contains("Index_4"))
                {
                    //隐藏浮层
                    buttonHover.SetActive(false);

                    _iconMesh.localPosition = new Vector3(0, 0, -0.00001f);

                    //只有按钮正在被按的状态下，指尖离开触发区才会触发ButtonUp
                    if (state != 0)
                    {
                        ButtonUp();
                    }
                    start = false;
                    curButton = null;
                    this.other = null;
                }
            }
        }

        public override void OnRayCastHit(HandInfo handInfo)
        {
            this.handInfo = handInfo;

            //显示浮层
            buttonHover.SetActive(handInfo == null ? false : true);
        }

        /// <summary>
        /// 按钮按到底的时候调用，播放按键按下的音效，并更新此按键的pressed状态为true
        /// </summary>
        void ButtonDown()
        {
            if (state == 0)
            {
                //播放按键按下的音效
                audioSource.PlayOneShot(clips[0]);

            }
            if (state != 2)
            {
                ////回调点击事件
                //clicked.Invoke();

                state = 2;
                pressed = true;
            }
        }

        /// <summary>
        /// 按键完全弹起的时候调用，播放按键弹起的音效，并更新此按键的pressed状态为false
        /// </summary>
        void ButtonUp()
        {
            if (state != 0)
            {
                //将按键完全弹起
                _mesh.localScale = new Vector3(_mesh.localScale.x, _mesh.localScale.y, height);

                ////将按键颜色变成按下的灰色效果
                //SetColor(new Color(1, 1, 1, 1));

                //播放按键弹起的音效
                audioSource.PlayOneShot(clips[1]);
            }

            if (pressed)//按到底然后弹起来才算是一次点击
            {
                //回调点击事件
                clicked.Invoke();
            }

            state = 0;
            pressed = false;
        }

        //private void OnTriggerExit(Collider other)
        //{
        //    if (other == this.other)
        //    {
        //        this.other = null;
        //    }
        //}

    }
}