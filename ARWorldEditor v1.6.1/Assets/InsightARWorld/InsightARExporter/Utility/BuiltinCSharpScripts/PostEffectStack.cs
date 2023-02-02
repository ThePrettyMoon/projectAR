using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

namespace ARWorldEditor
{
    [RequireComponent(typeof(Camera))]
    public class PostEffectStack : MonoBehaviour
    {
        public bool needDepthTexture = false;
        public bool grabMainCameraTexture = false;
        public string mainCameraTextureName = "_BackgroundTex";

        [SerializeField]
        public ImageEffects afterBlendImageEffects;

        RenderTexture mainCameraTexture;

        Camera currentCamera;

        private void Awake()
        {
            currentCamera = GetComponent<Camera>();
        }

        void Start()
        {
            if (needDepthTexture)
            {
                currentCamera.depthTextureMode = DepthTextureMode.Depth;
            }

            if (grabMainCameraTexture)
            {
                mainCameraTexture = new RenderTexture(Screen.width, Screen.height, 24 ,RenderTextureFormat.ARGB32);
                CommandBuffer copyMainCamera = new CommandBuffer();
                copyMainCamera.Blit(BuiltinRenderTextureType.CurrentActive, mainCameraTexture);
                Camera.main.AddCommandBuffer(CameraEvent.AfterEverything, copyMainCamera);

                if(!string.IsNullOrEmpty(mainCameraTextureName))
                {
                    Shader.SetGlobalTexture(mainCameraTextureName, mainCameraTexture);
                }
            }
        }

        private void Update()
        {
            if (needDepthTexture && currentCamera.depthTextureMode!= DepthTextureMode.Depth)
            {
                currentCamera.depthTextureMode = DepthTextureMode.Depth;
            }
            else if (!needDepthTexture && currentCamera.depthTextureMode == DepthTextureMode.Depth)
            {
                currentCamera.depthTextureMode = DepthTextureMode.None;
            }
        }


        //before blend,blend,after blend
        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            RenderTexture lastSrc = src;
            RenderTexture tempRenderTexture;

            //before blend
            for (int i = 0; i < afterBlendImageEffects.MAXCOUNT; i++)
            {
                var imageEffect = afterBlendImageEffects.GetImageEffect(i);
                if (imageEffect.material && !imageEffect.mute)
                {

                    for (int j = 0; j < imageEffect.MAXCOUNT; j++)
                    {
                        if (!string.IsNullOrEmpty(imageEffect.GetFloatName(j)))
                        {
                            imageEffect.material.SetFloat(imageEffect.GetFloatName(j), imageEffect.GetFloatValue(j));
                        }

                        if (!string.IsNullOrEmpty(imageEffect.GetColorName(j)))
                        {
                            imageEffect.material.SetColor(imageEffect.GetColorName(j), imageEffect.GetColorValue(j));
                        }
                    }

                    tempRenderTexture = RenderTexture.GetTemporary(src.width, src.height, src.depth);
                    Graphics.Blit(lastSrc, tempRenderTexture, imageEffect.material);
                    if (lastSrc != src)
                    {
                        RenderTexture.ReleaseTemporary(lastSrc);
                    }
                    lastSrc = tempRenderTexture;
                }
            }


            Graphics.Blit(lastSrc, dst);
            if (lastSrc != src)
            {
                RenderTexture.ReleaseTemporary(lastSrc);
            }
        }
    }


    public enum MaterialParameterType
    {
        FLOAT,
        COLOR,
    }

    [Serializable]
    public struct ImageEffects
    {
        [SerializeField]
        public ImageEffect imageEffect1;
        [SerializeField]
        public ImageEffect imageEffect2;
        [SerializeField]
        public ImageEffect imageEffect3;
        [SerializeField]
        public ImageEffect imageEffect4;

        public int MAXCOUNT
        {
            get { return 4; }
        }

        public ImageEffect GetImageEffect(int index)
        {
            ImageEffect result = new ImageEffect();
            switch (index)
            {
                case 0:
                    result = imageEffect1;
                    break;
                case 1:
                    result = imageEffect2;
                    break;
                case 2:
                    result = imageEffect3;
                    break;
                case 3:
                    result = imageEffect4;
                    break;
            }
            return result;
        }
    }

    [Serializable]
    public struct ImageEffect
    {

        public Material material;

        public bool mute;

        public string materialFloat1Name;
        public float materialFloat1Value;

        public string materialFloat2Name;
        public float materialFloat2Value;

        public string materialFloat3Name;
        public float materialFloat3Value;

        public string materialFloat4Name;
        public float materialFloat4Value;

        public string materialColor1Name;
        public Color materialColor1Value;

        public string materialColor2Name;
        public Color materialColor2Value;

        public string materialColor3Name;
        public Color materialColor3Value;

        public string materialColor4Name;
        public Color materialColor4Value;

        public int MAXCOUNT
        {
            get { return 4; }
        }

        public string GetFloatName(int index)
        {
            string result = "";
            switch (index)
            {
                case 0:
                    result = materialFloat1Name;
                    break;
                case 1:
                    result = materialFloat2Name;
                    break;
                case 2:
                    result = materialFloat3Name;
                    break;
                case 3:
                    result = materialFloat4Name;
                    break;
            }
            return result;
        }

        public float GetFloatValue(int index)
        {
            float result = 0;
            switch (index)
            {
                case 0:
                    result = materialFloat1Value;
                    break;
                case 1:
                    result = materialFloat2Value;
                    break;
                case 2:
                    result = materialFloat3Value;
                    break;
                case 3:
                    result = materialFloat4Value;
                    break;
            }
            return result;
        }

        public string GetColorName(int index)
        {
            string result = "";
            switch (index)
            {
                case 0:
                    result = materialColor1Name;
                    break;
                case 1:
                    result = materialColor2Name;
                    break;
                case 2:
                    result = materialColor3Name;
                    break;
                case 3:
                    result = materialColor4Name;
                    break;
            }
            return result;
        }

        public Color GetColorValue(int index)
        {
            Color result = Color.black;
            switch (index)
            {
                case 0:
                    result = materialColor1Value;
                    break;
                case 1:
                    result = materialColor2Value;
                    break;
                case 2:
                    result = materialColor3Value;
                    break;
                case 3:
                    result = materialColor4Value;
                    break;
            }
            return result;
        }
    }
}