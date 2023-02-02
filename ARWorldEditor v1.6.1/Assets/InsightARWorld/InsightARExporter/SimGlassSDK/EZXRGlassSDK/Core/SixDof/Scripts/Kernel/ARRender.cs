using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO;

namespace EZXR.Glass.SixDof
{
    public class ARRender : MonoBehaviour
    {
        #region params
        private bool m_ConfigCamera = false;
        private Material m_BackgroundMaterial;
        private CommandBuffer m_VideoCommandBuffer;
        private Camera m_Camera;
        private Texture2D m_VideoTexture;
        private bool m_enableRendering = true;

        private static int screenWidth;
        private static int screenHeight;
        private static float imageWidth;
        private static float imageHeight;
        int cnt = 0;
        public static byte[] bytes;
        #endregion

        #region unity functions
        void Start()
        {
            CameraResolution cameraResolution = new CameraResolution();
            NativeTracking.GetFisheyeCameraResolution(ref cameraResolution);
            screenWidth = cameraResolution.width;
            screenHeight = cameraResolution.height;
            imageWidth = (float)cameraResolution.width;
            imageHeight = (float)cameraResolution.height;
        }
        private void Awake()
        {
            m_Camera = GetComponent<Camera>();
        }

        private void OnPreRender()
        {
            Debug.Log("UNITY LOG ===== heihei");
            if (!m_enableRendering) return;
            Debug.Log("UNITY LOG ===== xixi");
            UpdateInsightARBackground();
        }
        #endregion

        #region custom functions
        /// <summary>
        /// 控制是否渲染背景
        /// </summary>
        /// <param name="enabled"></param>
        public void SetRenderCamera(bool enabled)
        {
            m_enableRendering = enabled;
        }

        private void UpdateInsightARBackground()
        {
            if (ARFrame.SessionStatus < EZVIOState.EZVIOCameraState_Detecting) return;

            //NativeTracking.UpdateOnGLThread();
            NativeTracking.UpdateOnGlThread_WithImgUndistorted();

            IntPtr texturePtr = (IntPtr)NativeTracking.GetTextureId();
            if (texturePtr == IntPtr.Zero) return;

            if (!m_ConfigCamera)
            {
                ConfigARCamera();
            }

            if (m_VideoTexture == null
                || m_VideoTexture.GetNativeTexturePtr().ToInt32() != texturePtr.ToInt32())
            {
                // Debug.Log("UNITY LOG haaaaaaaa");
                //m_VideoTexture = Texture2D.CreateExternalTexture(screenWidth, screenHeight,
                //    TextureFormat.RGBA32, false, false, texturePtr);
                m_VideoTexture = Texture2D.CreateExternalTexture((int)imageWidth, (int)imageHeight,
                      TextureFormat.RGBA32, false, false, texturePtr);
                m_VideoTexture.filterMode = FilterMode.Bilinear;
                m_VideoTexture.wrapMode = TextureWrapMode.Repeat;
                m_BackgroundMaterial.SetTexture("_MainTex", m_VideoTexture);
            }
            m_VideoTexture.UpdateExternalTexture(texturePtr);

            //bytes = m_VideoTexture.EncodeToPNG();
            //File.WriteAllBytes(Application.persistentDataPath + "/image" + cnt++ + ".png", bytes);

            int isPortrait = 0;
            float rotation = 0;
            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                rotation = -90;
                isPortrait = 1;
            }
            else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                rotation = 90;
                isPortrait = 1;
            }
            else if (Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.LandscapeLeft)
            {
                isPortrait = 0;
            }

            Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0.0f, 0.0f, rotation), Vector3.one);
            m_BackgroundMaterial.SetMatrix("_TextureRotation", m);

            float imageAspect = (float)imageWidth / (float)imageHeight;
            float screenAspect = (float)screenWidth / (float)screenHeight;
            float ratio = screenAspect > 1 ? imageAspect / screenAspect : imageAspect * screenAspect;


            float s_ShaderScaleX = 1.0f;
            float s_ShaderScaleY = 1.0f;
            if (isPortrait == 1)
            {
                if (ratio < 1)
                {
                    s_ShaderScaleX = ratio;
                }
                else if (ratio > 1)
                {
                    s_ShaderScaleY = 1f / ratio;
                }
            }
            else if (isPortrait == 0)
            {
                if (ratio < 1f)
                {
                    s_ShaderScaleY = ratio;
                }
                else if (ratio > 1f)
                {
                    s_ShaderScaleX = 1f / ratio;
                }
            }

            m_BackgroundMaterial.SetFloat("_texCoordScaleX", s_ShaderScaleX);
            m_BackgroundMaterial.SetFloat("_texCoordScaleY", s_ShaderScaleY);
            m_BackgroundMaterial.SetInt("_isPortrait", isPortrait);


            //float DevelopRate = imageHeight / imageWidth;
            //float ScreenRate = (float)screenHeight / (float)screenWidth;
            //float cameraRectHeightRate = imageHeight / ((imageWidth / screenWidth) * screenHeight);
            //float cameraRectWidthRate = imageWidth / ((imageHeight / screenHeight) * screenWidth);

            //Debug.Log("=========UNITY LOG : " + screenWidth + " " + screenHeight + " " + imageWidth + " " + imageHeight + " " + DevelopRate + " " + ScreenRate + " " + cameraRectWidthRate + " " + cameraRectHeightRate);

            //if (DevelopRate <= ScreenRate)
            //{
            //    m_Camera.rect = new Rect(0, (1 - cameraRectHeightRate) / 2, 1, cameraRectHeightRate);
            //}
            //else
            //{
            //    m_Camera.rect = new Rect(0, 0, cameraRectWidthRate, 1);
            //}
        }

        private void ConfigARCamera()
        {
            m_VideoCommandBuffer = new CommandBuffer();

            m_BackgroundMaterial = new Material(Shader.Find("ARBackground"));

            m_VideoCommandBuffer.Blit(null, BuiltinRenderTextureType.CurrentActive, m_BackgroundMaterial);
            m_Camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);

            m_ConfigCamera = true;
        }
        #endregion

    }
}
