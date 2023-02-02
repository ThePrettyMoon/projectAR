using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.IO;
using System.Threading;
using EZXR.Glass.Common;
using EZXR.Glass.SixDof;

namespace EZXR.Glass.MiraCast
{
    public class ARRenderRGB : MonoBehaviour
    {
        #region params
        private Material m_BackgroundMaterial;
        private CommandBuffer m_VideoCommandBuffer;
        private Camera m_Camera;
        private Texture2D m_VideoTexture;
        private bool m_enableRendering = true;

        private static int screenWidth;
        private static int screenHeight;
        private static float imageWidth;
        private static float imageHeight;

        private NormalRGBCameraDevice rgbCameraDevice;

        #endregion

        #region unity functions

        private void Awake()
        {
            m_Camera = GetComponent<Camera>();
        }
        private void OnEnable()
        {
            StartCoroutine(startRGBCamera());
            ConfigARCamera();
        }

        private void OnDisable()
        {
            DisableARBackgroundRendering();
            stopRGBCamera();
        }

        private void Start()
        {
            StartCoroutine(InitParam());
        }

        private void OnPreRender()
        {
            UpdateInsightARBackground();
        }

        #endregion

        #region custom functions
        /// <summary>
        /// 控制是否渲染背景
        /// </summary>
        /// <param name="enabled"></param>
        // public void SetRenderCamera(bool enabled)
        // {
        //     m_enableRendering = enabled;
        // }

        private void CreateTextureAndPassToPlugin(int width, int height)
        {
            // Create a texture
            m_VideoTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
            // m_VideoTexture.filterMode = FilterMode.Point;
            m_VideoTexture.filterMode = FilterMode.Bilinear;
            m_VideoTexture.wrapMode = TextureWrapMode.Repeat;
            // Call Apply() so it's actually uploaded to the GPU
            m_VideoTexture.Apply();
            // Pass texture pointer to the plugin
            NativeTracking.SetRGBTextureId(m_VideoTexture.GetNativeTexturePtr());
        }

        private IEnumerator InitParam()
        {
            yield return new WaitUntil(() => SessionManager.Instance.IsInited);
            CameraResolution cameraResolution = new CameraResolution();
            NativeTracking.GetRGBCameraResolution(ref cameraResolution);
            screenWidth = cameraResolution.width;
            screenHeight = cameraResolution.height;
            imageWidth = (float)cameraResolution.width;
            imageHeight = (float)cameraResolution.height;
            CreateTextureAndPassToPlugin(screenWidth, screenHeight);

            // yield return StartCoroutine("CallPluginAtEndOfFrames");
            StartCoroutine(CallPluginAtEndOfFrames());
        }

        private void UpdateInsightARBackground()
        {
            if (m_BackgroundMaterial == null)
            {
                return;
            }

            m_BackgroundMaterial.SetTexture("_MainTex", m_VideoTexture);
            // m_BackgroundMaterial.SetFloat("_Brightness", 1.0f);
            //@buqing Texture 坐标和opengl图像坐标是上下颠倒的
            m_BackgroundMaterial.SetVector(
                "_UvTopLeftRight",
                new Vector4(
                    0.0f, 1.0f, 1.0f, 1.0f));
            m_BackgroundMaterial.SetVector(
                "_UvBottomLeftRight",
                new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
            //@buqing unity 2019.4.24f1以后的版本需要重新更新buffer
            RemoveCommandBuffer(m_Camera);
            UpdateCommandBuffer(m_Camera, m_BackgroundMaterial);
        }

        private void ConfigARCamera()
        {
            if (m_VideoCommandBuffer == null)
            {
                m_VideoCommandBuffer = new CommandBuffer();
            }

            if (m_BackgroundMaterial == null)
            {
                m_BackgroundMaterial = new Material(Shader.Find("ARBackgroundNew"));
            }

            m_VideoCommandBuffer.Blit(null, BuiltinRenderTextureType.CameraTarget, m_BackgroundMaterial);
            m_Camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
        }

        private void DisableARBackgroundRendering()
        {
            if (m_VideoCommandBuffer == null || m_Camera == null)
            {
                return;
            }
            m_Camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
        }

        private void UpdateCommandBuffer(Camera cam, Material material)
        {
            RemoveCommandBuffer(cam);
            if (!cam || !material)
            {
                return;
            }

            m_VideoCommandBuffer = new CommandBuffer();
            m_VideoCommandBuffer.Blit(material.HasProperty("_MainTex") ? material.GetTexture("_MainTex") : null, BuiltinRenderTextureType.CameraTarget, material);
            cam.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);

        }

        private void RemoveCommandBuffer(Camera cam)
        {
            if (m_VideoCommandBuffer != null)
            {
                if (cam)
                {
                    cam.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);
                }
                m_VideoCommandBuffer.Dispose();
                m_VideoCommandBuffer = null;
            }
        }

        private IEnumerator CallPluginAtEndOfFrames()
        {
            while (true)
            {
                // Wait until all frame rendering is done
                yield return new WaitForEndOfFrame();

                // Issue a plugin event with arbitrary integer identifier.
                // The plugin can distinguish between different
                // things it needs to do based on this ID.
                // For our simple plugin, it does not matter which ID we pass here.
                GL.IssuePluginEvent(NativeTracking.GetRenderEventFunc(), 1);
            }
        }

        private IEnumerator startRGBCamera()
        {
            yield return new WaitUntil(() => SessionManager.Instance.IsInited);
            Debug.LogWarning("-10001-startRgbCamera new rgbCameraDevice Open");
            rgbCameraDevice = new NormalRGBCameraDevice();
            rgbCameraDevice.Open();
        }

        private void stopRGBCamera()
        {
            if (rgbCameraDevice != null)
                rgbCameraDevice.Close();
        }
        #endregion

    }
}
