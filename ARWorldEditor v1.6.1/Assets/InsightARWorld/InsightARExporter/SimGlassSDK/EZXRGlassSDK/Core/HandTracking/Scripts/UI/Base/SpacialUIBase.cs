using EZXR.Glass.Hand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wheels.Unity;
using EZXR.Glass.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EZXR.Glass.UI
{
    [ExecuteInEditMode]
    public class SpacialUIBase : MonoBehaviour
    {
        #region InternalUse
        public Transform _mesh;
        public TextMesh _text;
        public MeshRenderer _meshRenderer;
        #endregion

        /// <summary>
        /// UI的位姿出现类型
        /// </summary>
        public enum UIPoseType
        {
            /// <summary>
            /// 不对UI的位姿做任何处理
            /// </summary>
            Free,
            /// <summary>
            /// 始终朝向头部，位置不变
            /// </summary>
            FaceToHead,
            /// <summary>
            /// 刚性固定到头部，随相机移动和旋转
            /// </summary>
            FixedToHead,
            /// <summary>
            /// 超出视野后重置到头部前方，在相机视野外的时候将自己重置到相机前面，且面向相机
            /// </summary>
            OutReset,
            /// <summary>
            /// 绑定在小拇指外侧
            /// </summary>
            NearPinkyFinger,
            /// <summary>
            /// 绑定在手腕上
            /// </summary>
            OnWrist,
        }

        /// <summary>
        /// UI的位姿出现类型
        /// </summary>
        [SerializeField]
        UIPoseType positionType;
        /// <summary>
        /// UI的位姿出现类型
        /// </summary>
        public UIPoseType PositionType
        {
            get
            {
                return positionType;
            }
            set
            {
                positionType = value;
                if (positionType == UIPoseType.FixedToHead)
                {
                    transform.ActAsChild(SpacialUIController.Instance.leftCamera.transform);
                }
                else
                {
                    transform.CancelActAsChild();
                }
            }
        }
        public Vector3 size = new Vector3(0.1f, 0.1f, 0.1f);

        ///// <summary>
        ///// SharedMaterial
        ///// </summary>
        //public Material sharedMaterial;
        /// <summary>
        /// 此实例的Material
        /// </summary>
        public Material material;

        /// <summary>
        /// UI上显示的文字
        /// </summary>
        public string text = "UI";
        protected MaterialPropertyBlock materialPropertyBlock;
        /// <summary>
        /// UI的颜色
        /// </summary>
        public Color color = Color.white;
        Color lastColor;
        /// <summary>
        /// UI的图
        /// </summary>
        public Texture2D texture;
        Texture2D lastBGImage;
        /// <summary>
        /// UI的叠加顺序
        /// </summary>
        public int sortingOrder;


        protected virtual void Awake()
        {
            gameObject.tag = "SpacialUI";

            if (Application.isPlaying)
            {
                if (PositionType == UIPoseType.OnWrist)
                {
                    //OnWrist的UI初始化的时候应该在视野之外
                    transform.position = new Vector3(9999, 9999, 9999);
                }

                material = _meshRenderer.material;

                materialPropertyBlock = new MaterialPropertyBlock();
                _meshRenderer.GetPropertyBlock(materialPropertyBlock);

                //设置Material（MaterialPropertyBlock）
                SetMaterial();
            }
            else
            {
#if UNITY_EDITOR
                if (SpacialUIController.Instance == null)
                {
                    PrefabUtility.InstantiatePrefab(ResourcesManager.Load<GameObject>("UI/SpacialUIController"));
                }
                if (SpacialUIEventSystem.Instance == null)
                {
                    PrefabUtility.InstantiatePrefab(ResourcesManager.Load<GameObject>("UI/SpacialUIEventSystem"));
                }
                materialPropertyBlock = new MaterialPropertyBlock();
                _meshRenderer.GetPropertyBlock(materialPropertyBlock);
#endif
            }
        }

        protected virtual void Update()
        {
            if (Application.isPlaying)
            {
                switch (PositionType)
                {
                    case UIPoseType.FaceToHead:
                        transform.LookAt(transform.position + SpacialUIController.Instance.leftCamera.transform.forward);
                        break;
                    case UIPoseType.OutReset:
                        if (!SpacialUIController.Instance.leftCamera.rect.Contains(SpacialUIController.Instance.leftCamera.WorldToViewportPoint(transform.position)) && !SpacialUIController.Instance.rightCamera.rect.Contains(SpacialUIController.Instance.rightCamera.WorldToViewportPoint(transform.position)))
                        {
                            transform.position = SpacialUIController.Instance.leftCamera.transform.TransformPoint(new Vector3(0, 0, Vector3.Distance(transform.position, SpacialUIController.Instance.leftCamera.transform.position)));
                            transform.LookAt(transform.position + SpacialUIController.Instance.leftCamera.transform.forward);
                        }
                        break;
                    case UIPoseType.NearPinkyFinger:
                        NativeSwapManager.Point3 temp = new NativeSwapManager.Point3(ARHandManager.leftHand.GetJointData(HandJointType.Pinky_1).position + ARHandManager.leftHand.GetJointData(HandJointType.Pinky_1).position - ARHandManager.leftHand.GetJointData(HandJointType.Index_1).position);
                        //NativeSwapManager.filterPoint(ref temp, transform.GetInstanceID());
                        transform.position = new Vector3(temp.x, temp.y, temp.z);
                        transform.LookAt(transform.position + ARHandManager.leftHand.palmDirection, ARHandManager.leftHand.palmNormal);
                        break;
                    case UIPoseType.OnWrist:
                        if (ARHandManager.leftHand.handExist)
                        {
                            temp = new NativeSwapManager.Point3(ARHandManager.leftHand.GetJointData(HandJointType.Wrist_Middle).position);
                            //NativeSwapManager.filterPoint(ref temp, transform.GetInstanceID());
                            Vector3 newPos = new Vector3(temp.x, temp.y, temp.z);
                            if (Vector3.Distance(newPos, SpacialUIController.Instance.leftCamera.transform.position) > 0.1f)
                            {
                                transform.position = newPos;
                                transform.LookAt(transform.position + ARHandManager.leftHand.palmNormal, ARHandManager.leftHand.palmDirection.normalized);
                            }
                        }
                        else
                        {
                            transform.position = new Vector3(9999, 9999, 9999);
                        }
                        break;
                }
            }
            else
            {
#if UNITY_EDITOR
                _meshRenderer.sortingOrder = sortingOrder;
                if (Selection.activeGameObject != null && Selection.activeGameObject.hideFlags == HideFlags.NotEditable)
                {
                    while (Selection.activeGameObject.transform.parent != null && (Selection.activeGameObject.hideFlags == HideFlags.NotEditable || Selection.activeGameObject.hideFlags == HideFlags.HideInHierarchy))
                    {
                        Selection.activeGameObject = Selection.activeGameObject.transform.parent.gameObject;
                    }
                }

                //设置UI尺寸
                _mesh.localScale = new Vector3(size.x, size.y, size.z);

                //确保materialPropertyBlock不为空
                if (materialPropertyBlock == null)
                {
                    Awake();
                }

                //设置Material（MaterialPropertyBlock）
                SetMaterial();

                //_mesh.parent.gameObject.hideFlags = HideFlags.HideInHierarchy;
                //_mesh.gameObject.hideFlags = HideFlags.NotEditable;
                //_text.transform.hideFlags = HideFlags.HideInHierarchy;
                if (_text != null)
                {
                    _text.text = text;
                }

#endif
            }
        }

        public void ChangePositionType(UIPoseType uiPoseType)
        {

        }

        protected virtual void OnDrawGizmos()
        {
            //            if (!Application.isPlaying)
            //            {
            //#if UNITY_EDITOR
            //                //设置UI尺寸
            //                _mesh.localScale = new Vector3(size.x, size.y, size.z);

            //                //确保materialPropertyBlock不为空
            //                if (materialPropertyBlock == null)
            //                {
            //                    Awake();
            //                }

            //                //设置Material（MaterialPropertyBlock）
            //                SetMaterial();

            //                //_mesh.parent.gameObject.hideFlags = HideFlags.HideInHierarchy;
            //                //_mesh.gameObject.hideFlags = HideFlags.NotEditable;
            //                //_text.transform.hideFlags = HideFlags.HideInHierarchy;
            //                if (_text != null)
            //                {
            //                    _text.text = text;
            //                }
            //#endif
            //            }
        }

        void SetMaterial()
        {
            //设置UI的bgImage（确保Texture的设定相对于其他的materialPropertyBlock的Set是最前的，因为无法直接SetTexture为null）
            //if (bgImage != lastBGImage)
            {
                if (texture != null)
                {
                    materialPropertyBlock.SetTexture("_MainTex", texture);
                }
                else
                {
                    //重置materialPropertyBlock，因为无法直接SetTexture为null
                    materialPropertyBlock.Clear();

                }
                lastBGImage = texture;
            }

            //设置UI的颜色
            //if (color != lastColor)
            {
                materialPropertyBlock.SetColor("_Color", color);
                lastColor = color;
            }

            _meshRenderer.SetPropertyBlock(materialPropertyBlock);

        }

        public void SetColor(Color newColor)
        {
            materialPropertyBlock.SetColor("_Color", newColor);
            _meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }

    }
}