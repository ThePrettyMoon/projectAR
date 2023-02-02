using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using AOT;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Dongjian.LargeScale;

namespace InsightAR.Internal
{
    public class InsightARInterface
    {
        #region params
        public const string TAG = "InsightARInterface";

        private static InsightARInterface m_Interface;

        private static string[] trackResultList = new string[(int)TrackingResultType.TRACKING_RESULT_STRING_COUNT] { "", "", "", "", "", "", "" };

        private static bool sInited = false;

        private InsightARState arState;

        private int arReason;

        public static InsightARInterface GetInstance()
        {
            if (m_Interface == null)
            {
                m_Interface = new InsightARInterface();
            }

            return m_Interface;
        }

        public InsightARState ARState
        {
            get
            {
                return arState;
            }
        }

        public int ARReason
        {
            get
            {
                return arReason;
            }
        }



        public InsightARResult GetTrackingResult()
        {
            return trackResult;
        }

        public string GetResultString(int idx)
        {
            if (idx < trackResultList.Length)
            {
                return trackResultList[idx];
            }
            return string.Empty;
        }

        /// <summary>
        /// 数据情况
        /// </summary>
        public void ClearData()
        {
            for(int i = 0; i < (int)TrackingResultType.TRACKING_RESULT_STRING_COUNT; i++)
            {
                trackResultList[i] = "";
            }
        }

        public List<InsightARMarkerAnchor> GetMarkerList()
        {
            return listMarkerAnchors;
        }

        public static Action<InsightARMarkerAnchor> markerAddedAction;
        public static Action<InsightARMarkerAnchor> markerUpdatedAction;
        public static Action<InsightARMarkerAnchor> markerRemovedAction;

        public static Action<InsightARPlaneAnchor> planeAddedAction;
        public static Action<InsightARPlaneAnchor> planeUpdatedAction;
        public static Action<InsightARPlaneAnchor> planeRemovedAction;
        public static Action<InsightARRecognizedResult> recognizeAction;
        public static Action<InsightARCloudLocRequestImpl> cloudLocAction;
        public static Action<InsightARMaskResult> maskResultAction;

        private Camera mARCamera;
        private bool mARCameraConfiged = false;
        private Material m_BackgroundMaterial = null;
        //mask
        // private Texture2D maskTexture;
        //  private Vector4 maskUV = new Vector4(8, 64, -3.5f, -0.5f);
        private Camera mBgCamera = null;//相机，提供相机原图+相机位姿（场景只存在一个）
        private static Material mMaskMaterial = null;
        private static Camera mMaskCamera = null;   //mask相机，用于人像或者天空分割（场景只存在一个）
        private static CommandBuffer mMaskCommandBuffer = null;
        private static Texture2D mMaskTexture = null;
        private static IntPtr mMaskTexInptr = IntPtr.Zero;
        // cameras need to asyc arcamera tranform
        private Camera[] arCameraNeedSyncTrans = null;  //相机，同步相机位置（场景存在多个）

        private static InsightARResult trackResult = new InsightARResult();
        private static bool algInitedWithTrackResult = false;
        private static List<InsightARResult> listARResult;

        private static List<InsightARPlaneAnchor> listAnchorsToUpdate;
        private static List<InsightARPlaneAnchor> listAnchorsToAdd;
        private static List<InsightARPlaneAnchor> listAnchorsToRemove;

        private static List<InsightARMarkerAnchor> listMarkerAnchorsToAdd;
        private static List<InsightARMarkerAnchor> listMarkerAnchorsToUpdate;
        private static List<InsightARMarkerAnchor> listMarkerAnchorsToRemove;

        private static List<InsightARRecognizedResult> listRecognizedResults;
        //存储云端定位请求
        private static List<InsightARCloudLocRequestImpl> listCloudLocRequest;

        //记录所有的anchors
        private static List<InsightARMarkerAnchor> listMarkerAnchors;

        private static List<InsightARMaskResult> listMaskResult;

        private float[] uvcoords = new float[8] { 0f, 1f, 0f, 0f, 1f, 1f, 1f, 0f };

#if UNITY_IOS
        private Texture2D _videoTextureY = null;
        private Texture2D _videoTextureCbCr = null;
#elif UNITY_ANDROID
        private Texture2D _videoTextureRGBA = null;
#endif
        private int textureTarget = 3553;

        private CommandBuffer m_VideoCommandBuffer;
#endregion

#region custom functions
        [MonoPInvokeCallback(typeof(InsightARNative.Internal_FrameUpdate))]
        private static void onFrameUpdate(InsightARResult insightResult, IntPtr pHandler)
        {
            listARResult.Add(insightResult);
        }

        [MonoPInvokeCallback(typeof(InsightARNative.Internal_AnchorAdded))]
        private static void onAnchorAdded(InsightARAnchorData anchor, IntPtr pHandler)
        {
            if (anchor.type == InsightARAnchorType.Plane)
            {
                InsightARPlaneAnchor arPlaneAnchor = InsightARUtility.GetPlaneAnchorFromAnchorData(anchor);
                listAnchorsToAdd.Add(arPlaneAnchor);
            }
            else if (anchor.type == InsightARAnchorType.Marker_2D)
            {
                InsightARMarkerAnchor arMarkerAnchor = InsightARUtility.GetMarkerAnchorFromAnchorData(anchor);
                arMarkerAnchor.isValid = 1;
                listMarkerAnchorsToAdd.Add(arMarkerAnchor);
            }
            //记录所有的anchor
            InsightARMarkerAnchor markerAnchor = InsightARUtility.GetMarkerAnchorFromAnchorData(anchor);
            markerAnchor.isValid = 1;
            bool markerFound = false;
            for(int i = 0; i < listMarkerAnchors.Count; i++)
            {
                if (listMarkerAnchors[i].identifier == markerAnchor.identifier)
                {
                    markerFound = true;
                    break;
                }
            }
            if (!markerFound)
            {
                listMarkerAnchors.Add(markerAnchor);
            }
        }

        [MonoPInvokeCallback(typeof(InsightARNative.Internal_AnchorUpdated))]
        private static void onAnchorUpdated(InsightARAnchorData anchor, IntPtr pHandler)
        {
            if (anchor.type == InsightARAnchorType.Plane)
            {
                InsightARPlaneAnchor arPlaneAnchor = InsightARUtility.GetPlaneAnchorFromAnchorData(anchor);
                listAnchorsToUpdate.Add(arPlaneAnchor);
            }
            else if (anchor.type == InsightARAnchorType.Marker_2D)
            {
                InsightARMarkerAnchor arMarkerAnchor = InsightARUtility.GetMarkerAnchorFromAnchorData(anchor);
                listMarkerAnchorsToUpdate.Add(arMarkerAnchor);
            }

            //update
            InsightARMarkerAnchor markerAnchor = InsightARUtility.GetMarkerAnchorFromAnchorData(anchor);
            for (int i = 0; i < listMarkerAnchors.Count; i++)
            {
                if (listMarkerAnchors[i].identifier == markerAnchor.identifier)
                {
                    listMarkerAnchors[i] = markerAnchor;
                }
            }
        }

        [MonoPInvokeCallback(typeof(InsightARNative.Internal_AnchorRemoved))]
        private static void onAnchorRemoved(InsightARAnchorData anchor, IntPtr pHandler)
        {
            if (anchor.type == InsightARAnchorType.Plane)
            {
                InsightARPlaneAnchor arPlaneAnchor = InsightARUtility.GetPlaneAnchorFromAnchorData(anchor);
                listAnchorsToRemove.Add(arPlaneAnchor);
            }
            else if (anchor.type == InsightARAnchorType.Marker_2D)
            {
                InsightARMarkerAnchor arMarkerAnchor = InsightARUtility.GetMarkerAnchorFromAnchorData(anchor);
                listMarkerAnchorsToRemove.Add(arMarkerAnchor);
            }

            //remove  不移除，只按disable处理
            InsightARMarkerAnchor markerAnchor = InsightARUtility.GetMarkerAnchorFromAnchorData(anchor);
            markerAnchor.isValid = 0;
            for (int i = 0; i < listMarkerAnchors.Count; i++)
            {
                if (listMarkerAnchors[i].identifier == markerAnchor.identifier)
                {
                    listMarkerAnchors[i] = markerAnchor;
                }
            }
        }

        [MonoPInvokeCallback(typeof(InsightARNative.Internal_RecognizedUpdate))]
        public static void onRecognizedUpdate(InsightARRecognizedResultNative recogRes, IntPtr pHandler)
        {
            //Marshal.FreeHGlobal(jsonRes);
            InsightARRecognizedResult tmp = InsightARUtility.GetARRecognizedResult(recogRes);
            listRecognizedResults.Add(tmp);
            // update tracking result

            TrackingResultType trackType = TrackingResultType.TRACKING_RESULT_STRING_2DIMAGE_JSON;
            if (tmp.type == InsightARClassifiedType.InsightARClassifiedType2dImage)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_2DIMAGE_JSON;
            }
            else if (tmp.type == InsightARClassifiedType.InsightARClassifiedTypeARCode)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_ARCODE_JSON;
            }
            else if (tmp.type == InsightARClassifiedType.InsightARClassifiedTypeBody)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_BODY_JSON;
            }
            else if (tmp.type == InsightARClassifiedType.InsightARClassifiedTypeFace)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_FACE_JSON;
            }
            else if (tmp.type == InsightARClassifiedType.InsightARClassifiedTypeGesture)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_GESTURE_JSON;
            }
            else if (tmp.type == InsightARClassifiedType.InsightARClassifiedTypeObject)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_OBJECT_JSON;
            }
            else if (tmp.type == InsightARClassifiedType.InsightARClassifiedTypeQRCode)
            {
                trackType = TrackingResultType.TRACKING_RESULT_STRING_QRCODE_JSON;
            }
            trackResultList[(int)trackType] = tmp.recognizedResult;
        }

        /// <summary>
        /// 请求小地图
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="pHandler"></param>
        [MonoPInvokeCallback(typeof(InsightARNative.Internal_Request_LocMap))]
        private static void onRequestLocMap(IntPtr mapId, IntPtr pHandler) { }

        /// <summary>
        /// 请求云端重定位
        /// </summary>
        /// <param name="cloudLocRequestData"></param>
        /// <param name="pHandler"></param>
        [MonoPInvokeCallback(typeof(InsightARNative.Internal_Request_CloudLoc))]
        public static void onRequestCloudLoc(InsightARCloudLocRequest cloudLocRequestData, IntPtr pHandler)
        {
            InsightARCloudLocRequestImpl requestImpl = new InsightARCloudLocRequestImpl(cloudLocRequestData);
            listCloudLocRequest.Add(requestImpl);
        }

        /// <summary>
        /// 人体分割/天空分割
        /// </summary>
        /// <param name="cloudLocRequestData"></param>
        /// <param name="pHandler"></param>
        [MonoPInvokeCallback(typeof(InsightARNative.Internal_MaskResult))]
        public static void onMaskResult(InsightARMaskResult result, IntPtr pHandler)
        {
            listMaskResult.Add(result);
        }

#region PUBLIC_API


        public void StartAR(InsightARSettings settings)
        {
            checkARSupport(settings);
            registerInsightAR(settings);
            startInsightAR(settings);      
        }

        public void StopAR()
        {
#if UNITY_ANDROID || UNITY_IOS
            ResetConfigARCamera();

            sInited = false;
            algInitedWithTrackResult = false;

            listARResult.Clear();
            listAnchorsToAdd.Clear();
            listAnchorsToUpdate.Clear();
            listAnchorsToRemove.Clear();
            listMarkerAnchorsToAdd.Clear();
            listMarkerAnchorsToUpdate.Clear();
            listMarkerAnchorsToRemove.Clear();
            listCloudLocRequest.Clear();

            listMarkerAnchors.Clear();
            listMaskResult.Clear();
#if UNITY_IOS
            InsightARNative.iarlsStopAsync();
#elif UNITY_ANDROID
            InsightARNative.iarlsStop();
#endif
            InsightDebug.Log(TAG, "-ar- StopAR");

            // InsightARNative.iarlsRelease();
#endif
        }

        public void ResetAR(string path)
        {
#if UNITY_ANDROID || UNITY_IOS
            sInited = false;
            InsightARNative.iarlsReload(path);
            InsightDebug.Log(TAG, "-ar- ResetAR over" + path);
#endif
        }


        /// <summary>
        /// 重置绘制相关数据
        /// </summary>
        public void ResetConfigARCamera()
        {
            if (mARCameraConfiged)
            {
                RemoveCommandBuffers(ref mARCamera, ref m_VideoCommandBuffer);
                RemoveCommandBuffers(ref mBgCamera, ref m_VideoCommandBuffer);
                RemoveCommandBuffers(ref mMaskCamera, ref mMaskCommandBuffer);
                mARCameraConfiged = false;
            }
#if UNITY_IOS
            DetroyTexture2D(ref _videoTextureY);
            DetroyTexture2D(ref _videoTextureCbCr);
#elif UNITY_ANDROID
            DetroyTexture2D(ref _videoTextureRGBA);
#endif
            DetroyTexture2D(ref mMaskTexture);
        }

    public void AdaptUIOrientation(InsightARUIOrientation orien)
        {

#if UNITY_ANDROID || UNITY_IOS
            InsightARNative.iarlsAdaptUIOri(orien);
#endif
        }


        public void Update()
        {
            //updateInsightARResult();
            //UpdateInsightAR();

            updateInsightARPlanes();
            updateInsightRecogeResults();
            updateInsightARMarkerAnchors();
            updateInsightARCloudLocRequest();
            //updateInsightARMaskResult();
        }

        public void LateUpdate() {

            UpdateARBackground();
#if UNITY_ANDROID
            UpdataInsightARResult(InsightARNative.iarlsGetARResultNative());
#elif UNITY_IOS
            updateInsightARResult();
#endif
            UpdateInsightAR();

        }

        public void UpdateARBackground()
        {
            updateInsightARBackground();
            updateInsightARMaskResult();
        }

        public void SetupCamera(Camera camera)
        {
            mARCamera = camera;
            //安卓，传递相机view
#if UNITY_ANDROID
            InsightDebug.Log(TAG, "mainCamera pixel, width: " + mARCamera.pixelWidth + ", height: " + mARCamera.pixelHeight);
            InsightARNative.iarOnViewSizeChangeNative(mARCamera.pixelWidth, mARCamera.pixelHeight);
#endif
            //reset
            ResetConfigARCamera();
        }

        #endregion //PUBLIC_API

        #region private_API

        private void RemoveCommandBuffers(ref Camera camera, ref CommandBuffer commandBuffer)
        {

            if (camera != null)
            {
                CommandBuffer[] cbs = camera.GetCommandBuffers(CameraEvent.BeforeForwardOpaque);
                if (cbs != null && cbs.Length > 0)
                {
                    camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, commandBuffer);
                }
            }

        }

        private void DetroyTexture2D(ref Texture2D tex)
        {
            if (tex != null)
            {
                UnityEngine.Object.Destroy(tex);
                tex = null;
            }
        }

        private void configARCamera(int textureTarget = 3553)
        {
           InsightDebug.Log(TAG, "-ar- config ar camera target == " + textureTarget);
            m_VideoCommandBuffer = new CommandBuffer();
#if UNITY_IOS
            m_BackgroundMaterial = new Material(Shader.Find("Unlit/ARCameraShader"));
#else
            if (textureTarget == 36197) // GL_TEXTURE_EXTERNAL_OES
            {
                m_BackgroundMaterial = new Material(Shader.Find("VideoPlaneOES"));
            }
            else // GL_TEXTURE_2D（3553）
            {
                m_BackgroundMaterial = new Material(Shader.Find("VideoPlaneNoLight"));
            }
#endif
            mARCameraConfiged = true;

            m_VideoCommandBuffer.Blit(null, BuiltinRenderTextureType.CurrentActive, m_BackgroundMaterial);
            mARCamera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);

            // 背景相机
            mBgCamera = InsightCamerasManager.initBackgroundCamera();
            if (mBgCamera)
                mBgCamera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_VideoCommandBuffer);

            // mask相机
            mMaskCamera = InsightCamerasManager.initMaskCamera();
            mMaskMaterial = new Material(Shader.Find("Unlit/MaskUnlitShader"));
            mMaskCommandBuffer = new CommandBuffer();
            mMaskCommandBuffer.Blit(null, BuiltinRenderTextureType.CurrentActive, mMaskMaterial);
            if (mMaskCamera)
            {
                mMaskCamera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, mMaskCommandBuffer);
                InsightDebug.Log(TAG, "mask cemera add commandbuffer");
            }

            //arcameras need to sync transform
            arCameraNeedSyncTrans = InsightCamerasManager.initARCameras();

        }

        private void checkARSupport(InsightARSettings settings)
        {
            InsightDebug.Log(TAG, "-ar- checkARSupport" + settings.appKey + "  " + settings.appSecret);
#if UNITY_ANDROID
            //InsightARNative.RequestInstallARServiceNative(AREngines_Type.ARCore|AREngines_Type.HUAWEI);
            //check InsightAR support
            AREngines_Type res = InsightARNative.iarlsSupport();
#endif
        }

        private void updateInsightARBackground()
        {
            if (!algInitedWithTrackResult) {
                //算法未完全inited
                InsightDebug.Log(TAG, "alg has not total inited with green screen!");
                return;
            }
            if (arState < InsightARState.Init_OK || arState >= InsightARState.Track_Stop)
            {
                return;
            }


#if UNITY_ANDROID || UNITY_IOS||UNITY_EDITOR
#if UNITY_ANDROID
            InsightARNative.iarlsUpdateOnRenderThread();
#endif
            InsightARTextureHandles handles = InsightARNative.iarlsGetVideoTextureHandles();
            if (handles.textureY == null || handles.textureCbCr == null) {
                return;
            }
            if (handles.textureY == System.IntPtr.Zero)
            {
                return;
            }
          
#if UNITY_IOS
            if (handles.textureCbCr == System.IntPtr.Zero)
            {
                return;
            }
#endif
            if (!mARCameraConfiged)
            {
#if UNITY_IOS
                configARCamera();
#elif UNITY_ANDROID
                textureTarget = handles.textureTarget;
                configARCamera(textureTarget);
#endif
            }
            Resolution currentResolution = Screen.currentResolution;
           // Debug.Log("-ar- current resolution " + currentResolution +" " + handles.textureY.ToString() +" " + textureTarget);

#if UNITY_IOS
            // Texture Y
            _videoTextureY = Texture2D.CreateExternalTexture(currentResolution.width, currentResolution.height,
                TextureFormat.R8, false, false, (System.IntPtr)handles.textureY);
            _videoTextureY.filterMode = FilterMode.Bilinear;
            _videoTextureY.wrapMode = TextureWrapMode.Repeat;
            _videoTextureY.UpdateExternalTexture(handles.textureY);

            // Texture CbCr
            _videoTextureCbCr = Texture2D.CreateExternalTexture(currentResolution.width, currentResolution.height,
                TextureFormat.RG16, false, false, (System.IntPtr)handles.textureCbCr);
            _videoTextureCbCr.filterMode = FilterMode.Bilinear;
            _videoTextureCbCr.wrapMode = TextureWrapMode.Repeat;
            _videoTextureCbCr.UpdateExternalTexture(handles.textureCbCr);

            m_BackgroundMaterial.SetTexture("_textureY", _videoTextureY);
            m_BackgroundMaterial.SetTexture("_textureCbCr", _videoTextureCbCr);

#elif UNITY_ANDROID
            if (_videoTextureRGBA == null
                || _videoTextureRGBA.GetNativeTexturePtr().ToInt32() != handles.textureY.ToInt32())
            {
                _videoTextureRGBA = Texture2D.CreateExternalTexture(currentResolution.width, currentResolution.height,
                    TextureFormat.RGBA32, false, false, (System.IntPtr)handles.textureY);
                _videoTextureRGBA.filterMode = FilterMode.Bilinear;
                _videoTextureRGBA.wrapMode = TextureWrapMode.Clamp;
            }
            _videoTextureRGBA.UpdateExternalTexture(handles.textureY);
            m_BackgroundMaterial.SetTexture("_MainTex", _videoTextureRGBA);

#endif

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

            float imageAspect = (float)trackResult.param.width / (float)trackResult.param.height;
            float screenAspect = (float)currentResolution.width / (float)currentResolution.height;
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

            if (textureTarget == 3553) //GL_TEXTURE_2D
            {
#if UNITY_IOS
                Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0.0f, 0.0f, rotation), Vector3.one);
                m_BackgroundMaterial.SetMatrix("_TextureRotation", m);
#elif UNITY_ANDROID
                s_ShaderScaleX = isPortrait == 1 ? s_ShaderScaleX : -s_ShaderScaleX;
#endif
                m_BackgroundMaterial.SetFloat("_texCoordScaleX", s_ShaderScaleX);
                m_BackgroundMaterial.SetFloat("_texCoordScaleY", s_ShaderScaleY);
                m_BackgroundMaterial.SetInt("_isPortrait", isPortrait);

            }
            else if (textureTarget == 36197) //GL_TEXTURE_EXTERNAL_OES
            {
                const string topLeftRight = "_UvTopLeftRight";
                const string botLeftRight = "_UvBottomLeftRight";
                float deltaX = (1.0f - s_ShaderScaleX) * 0.5f;
                float deltaY = (1.0f - s_ShaderScaleY) * 0.5f;
                if (isPortrait == 1)
                {
                    uvcoords[0] = 1.0f - deltaY;
                    uvcoords[1] = 1.0f - deltaX;
                    uvcoords[2] = 1.0f - deltaY;
                    uvcoords[3] = 0.0f + deltaX;
                    uvcoords[4] = 0.0f + deltaY;
                    uvcoords[5] = 1.0f - deltaX;
                    uvcoords[6] = 0.0f + deltaY;
                    uvcoords[7] = 0.0f + deltaX;
                }
                else
                {
                    uvcoords[0] = 0.0f + deltaX;
                    uvcoords[1] = 1.0f - deltaY;
                    uvcoords[2] = 1.0f - deltaX;
                    uvcoords[3] = 1.0f - deltaY;
                    uvcoords[4] = 0.0f + deltaX;
                    uvcoords[5] = 0.0f + deltaY;
                    uvcoords[6] = 1.0f - deltaX;
                    uvcoords[7] = 0.0f + deltaY;
                }

                m_BackgroundMaterial.SetVector(topLeftRight, new Vector4(uvcoords[0], uvcoords[1], uvcoords[2], uvcoords[3]));
                m_BackgroundMaterial.SetVector(botLeftRight, new Vector4(uvcoords[4], uvcoords[5], uvcoords[6], uvcoords[7]));
                
            }
#endif
            
        }

        private void UpdataInsightARResult(InsightARResult insightResult) {

            InsightARState state = (InsightARState)insightResult.state;
#if !UNITY_EDITOR
            InsightDebug.Log(TAG, "-ar- State = " + state + " " + sInited);
#endif
            //解决iOS resetar后还会发送tracking状态问题
#if UNITY_IOS
            if (!sInited)
            {
                if (state == InsightARState.Detecting)
                {
                    sInited = true;
                }
                else if (state == InsightARState.Tracking) //解决一些resetAR误判问题
                {
                    trackResult.state = (int)InsightARState.Initing;
                    return;
                }
            }
#endif

            algInitedWithTrackResult = true;//算法成功运行并输出结果

            if (state == InsightARState.Initing || state == InsightARState.Init_Fail
                || state == InsightARState.Detect_Fail || state == InsightARState.Track_Fail
                || state == InsightARState.Track_Stop || state == InsightARState.Uninitialized)
            {
                trackResult.state = insightResult.state;
            }
            else
            {
                trackResult = insightResult;
            }
        }

        private static void updataInsightARMaskResult(InsightARMaskResult maskResult)
        {
            //InsightDebug.Log(TAG, string.Format("mask result: {0}/{1}/{2}/{3}", maskResult.width, maskResult.height, maskResult.maskPtr, maskResult.maskType));
            if (mMaskMaterial == null)
            {
                InsightDebug.Log(TAG, "mask material is null");
#if UNITY_EDITOR
                mMaskMaterial = new Material(Shader.Find("Unlit/MaskUnlitShader"));
                mMaskCommandBuffer = new CommandBuffer();
                mMaskCommandBuffer.Blit(null, BuiltinRenderTextureType.CurrentActive, mMaskMaterial);
                if (mMaskCamera)
                {
                    mMaskCamera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, mMaskCommandBuffer);
                    InsightDebug.Log(TAG, "mask cemera add commandbuffer");
                }
#endif
                return;
            }
//#if UNITY_EDITOR
//            maskResult = new InsightARMaskResult();
//            var intp = (int)GameStartManager.tex.GetNativeTexturePtr();
//            maskResult.width = 256;
//            maskResult.height = 256;
//            maskResult.maskPtr = (IntPtr)GameStartManager.intp;
//            //Debug.Log(maskResult.maskPtr);
//#endif
            if (maskResult.maskPtr == System.IntPtr.Zero)
                return;

            if (mMaskTexInptr != maskResult.maskPtr || mMaskTexture == null)
            {
                mMaskTexInptr = maskResult.maskPtr;

                mMaskTexture = Texture2D.CreateExternalTexture(maskResult.width, maskResult.height,
#if UNITY_IOS||UNITY_EDITOR
                                TextureFormat.BGRA32,
#elif UNITY_ANDROID
                                TextureFormat.RGBA32,
#endif
                                false, false, maskResult.maskPtr);
                Debug.Log("create: " + maskResult.maskPtr);
            }
            else
            {
                mMaskTexture.UpdateExternalTexture(maskResult.maskPtr);
            }

            mMaskTexture.filterMode = FilterMode.Bilinear;
            mMaskTexture.wrapMode = TextureWrapMode.Clamp;
            //mMaskTexture.UpdateExternalTexture(maskResult.maskPtr);
            //mMaskMaterial.mainTexture = mMaskTexture;
            mMaskMaterial.SetTexture("_MainTex", mMaskTexture);
        }
        private void UpdateInsightAR()
        {
#if UNITY_EDITOR
            trackResult = InsightARUtility.CreateARResult();
            arState = (InsightARState)trackResult.state;
            if (arState == InsightARState.Tracking) {
                InsightARManager.Instance.cloudLocation.CloudLocAlgCode = 1;
            }
            arReason = trackResult.reason;
            return;
#endif
            arState = (InsightARState)trackResult.state;

            arReason = trackResult.reason;

            //InsightDebug.Log(TAG, "-ar- current state == :" + arState + " " + arReason);

            if (arState == InsightARState.Init_Fail)
            {
                InsightDebug.Log(TAG,"-ar- Init_Fail:" + trackResult.reason);
                return;
            }
            if (arState == InsightARState.Track_Fail)
            {
                InsightDebug.Log(TAG, "-ar- Track_Fail:" + trackResult.reason);
                return;
            }
            if (arState == InsightARState.Detect_Fail)
            {
                InsightDebug.Log(TAG, "-ar- Detect_Fail:" + trackResult.reason);
                return;
            }

            if (arState == InsightARState.Initing || arState == InsightARState.Track_Stop || arState == InsightARState.Uninitialized)
            {
                return;
            }


#if UNITY_EDITOR
            float fov = trackResult.param.fov[1];
            //iOS重新计算fov
#elif UNITY_IOS
            float fov = CameraUtility.CalculateFov(trackResult.param.width, trackResult.param.height
                , Screen.width, Screen.height, trackResult.param.fov);
#elif UNITY_ANDROID
            float fov = trackResult.param.fov[1];
#endif

            if (mARCamera == null) return;

            if (arState > InsightARState.Init_OK && mARCamera.fieldOfView != fov)
            {
                //InsightDebug.Log(TAG, "camera acitve fov is: " + fov);
                mARCamera.fieldOfView = fov;
            }
            //InsightDebug.Log(TAG, "camera fov is: " + trackResult.param.fov[0]);
            //InsightDebug.Log(TAG, "camera fov is: " + trackResult.param.fov[1]);

            //InsightDebug.Log(TAG, "camera opengl localRotation is: "
            //    + trackResult.camera.quaternion_opengl[0] + ","
            //    + trackResult.camera.quaternion_opengl[1] + ","
            //    + trackResult.camera.quaternion_opengl[2] + ","
            //    + trackResult.camera.quaternion_opengl[3]);

            //InsightDebug.Log(TAG, "camera opengl localPosition is: "
            //    + trackResult.camera.center_opengl[0] + ","
            //    + trackResult.camera.center_opengl[1] + ","
            //    + trackResult.camera.center_opengl[2]);

            //InsightDebug.Log(TAG, "camera unity localRotation is: "
            //      + trackResult.camera.quaternion_u3d[0] + ","
            //      + trackResult.camera.quaternion_u3d[1] + ","
            //      + trackResult.camera.quaternion_u3d[2] + ","
            //      + trackResult.camera.quaternion_u3d[3]);

            //InsightDebug.Log(TAG, "camera unity localPosition is: "
            //    + trackResult.camera.center_u3d[0] + ","
            //    + trackResult.camera.center_u3d[1] + ","
            //    + trackResult.camera.center_u3d[2]);

            mARCamera.transform.localPosition = new Vector3(
                trackResult.camera.center_u3d[0],
                trackResult.camera.center_u3d[1],
                trackResult.camera.center_u3d[2]);
            mARCamera.transform.localRotation = new Quaternion(
                trackResult.camera.quaternion_u3d[0],
                trackResult.camera.quaternion_u3d[1],
                trackResult.camera.quaternion_u3d[2],
                trackResult.camera.quaternion_u3d[3]
            );

            if (mBgCamera)
            {
                //InsightDebug.Log(TAG, "bg camera fov: " + mARCamera.fieldOfView.ToString());
                mBgCamera.fieldOfView = mARCamera.fieldOfView;
                mBgCamera.transform.position = mARCamera.transform.position;
                mBgCamera.transform.rotation = mARCamera.transform.rotation;
            }

            if (arCameraNeedSyncTrans != null)
            {
                if (arCameraNeedSyncTrans.Length > 0)
                {
                    //InsightDebug.Log(TAG, "ar camera fov: " + mARCamera.fieldOfView.ToString());
                    foreach (Camera c in arCameraNeedSyncTrans)
                    {
                        c.fieldOfView = mARCamera.fieldOfView;
                        c.transform.position = mARCamera.transform.position;
                        c.transform.rotation = mARCamera.transform.rotation;
                    }
                }
            }

        }

        private void registerInsightAR(InsightARSettings settings)
        {
#if UNITY_ANDROID || UNITY_IOS
            int res = InsightARNative.iarlsRegisterAppKey(settings.appKey, settings.appSecret);
#endif
        }

        private void startInsightAR(InsightARSettings settings)
        {
#if UNITY_ANDROID || UNITY_IOS||UNITY_EDITOR
            listARResult = new List<InsightARResult>();
            listAnchorsToUpdate = new List<InsightARPlaneAnchor>();
            listAnchorsToAdd = new List<InsightARPlaneAnchor>();
            listAnchorsToRemove = new List<InsightARPlaneAnchor>();
            listMarkerAnchorsToAdd = new List<InsightARMarkerAnchor>();
            listMarkerAnchorsToUpdate = new List<InsightARMarkerAnchor>();
            listMarkerAnchorsToRemove = new List<InsightARMarkerAnchor>();
            listRecognizedResults = new List<InsightARRecognizedResult>();
            listMarkerAnchors = new List<InsightARMarkerAnchor>();
            listCloudLocRequest = new List<InsightARCloudLocRequestImpl>();
            listMaskResult = new List<InsightARMaskResult>();

            string configPath = settings.configPath;
            string assetPath = string.IsNullOrEmpty(configPath) ? "" : configPath + "/assets";
            InsightARNative.iarlsInit(configPath,
                                      assetPath,
#if UNITY_ANDROID||UNITY_EDITOR
                                      InsightARTextureType.InsightAR_OPENGL,
#elif UNITY_IOS
                                      InsightARTextureType.InsightAR_METAL,
#endif
                                      onFrameUpdate,
                                      onAnchorAdded, onAnchorUpdated, onAnchorRemoved, onRecognizedUpdate,
                                      onMaskResult,
                                      onRequestLocMap,onRequestCloudLoc,
                                      IntPtr.Zero);
#endif

            InsightDebug.Log(TAG, "-ar- Init AR " + settings.configPath + "\n" + assetPath);
        }

        private void updateInsightARResult() {

            var length = listARResult.Count;
            for (int i = 0; i < length; i++) {
                InsightARResult arResult = listARResult[i];
                UpdataInsightARResult(arResult);
            }
            listARResult.Clear();
        }

        private void updateInsightARPlanes()
        {
            InsightARPlaneAnchor tPlane;
            int length = listAnchorsToAdd.Count;
            for (int i = 0; i < length; i++)
            {
                tPlane = listAnchorsToAdd[i];
                if (planeAddedAction != null)
                    planeAddedAction(tPlane);
            }
            listAnchorsToAdd.Clear();

            length = listAnchorsToUpdate.Count;
            for (int i = 0; i < length; i++)
            {
                tPlane = listAnchorsToUpdate[i];
                if (planeUpdatedAction != null)
                    planeUpdatedAction(tPlane);
            }
            listAnchorsToUpdate.Clear();

            length = listAnchorsToRemove.Count;
            for (int i = 0; i < length; i++)
            {
                tPlane = listAnchorsToRemove[i];
                if (planeRemovedAction != null)
                    planeRemovedAction(tPlane);
            }
            listAnchorsToRemove.Clear();
        }

        // xnh : remove放置在最前和现在的多marker的渲染方式有关， 之后会去掉这个约束。
        private void updateInsightARMarkerAnchors()
        {
            InsightARMarkerAnchor tMarker;
            int length = listMarkerAnchorsToRemove.Count;
            for (int i = 0; i < length; i++)
            {
                tMarker = listMarkerAnchorsToRemove[i];
                if (markerRemovedAction != null)
                    markerRemovedAction(tMarker);
            }
            listMarkerAnchorsToRemove.Clear();

            length = listMarkerAnchorsToAdd.Count;
            for (int i = 0; i < length; i++)
            {
                tMarker = listMarkerAnchorsToAdd[i];
                if (markerAddedAction != null)
                    markerAddedAction(tMarker);
            }
            listMarkerAnchorsToAdd.Clear();

            length = listMarkerAnchorsToUpdate.Count;
            for (int i = 0; i < length; i++)
            {
                tMarker = listMarkerAnchorsToUpdate[i];
                if (markerUpdatedAction != null)
                    markerUpdatedAction(tMarker);
            }
            listMarkerAnchorsToUpdate.Clear();
        }

        private void updateInsightRecogeResults()
        {
            int length = listRecognizedResults.Count;
            for (int i = 0; i < length; i++)
            {
                InsightARRecognizedResult res = listRecognizedResults[i];
                if (recognizeAction != null)
                    recognizeAction(res);
            }
            listRecognizedResults.Clear();
        }

        /// <summary>
        /// 更新cloud loc 请求
        /// </summary>
        private void updateInsightARCloudLocRequest()
        {
            int length =  listCloudLocRequest.Count;
            for (int i = 0; i < length; i++)
            {
                var res = listCloudLocRequest[i];
                if (cloudLocAction != null)
                    cloudLocAction(res);
            }
            listCloudLocRequest.Clear();
        }

        /// <summary>
        /// 更新mask result
        /// </summary>
        private void updateInsightARMaskResult()
        {
            //和主相机在同一帧绘制
            int length = listMaskResult.Count;
            if (length > 0)
            {
                InsightARMaskResult result = listMaskResult[length - 1];
                updataInsightARMaskResult(result);
                listMaskResult.Clear();
            }
        }


#endregion //private_API



        public static int GetInsightARSceenWidth()
        {
            return trackResult.param.width;
        }

        public static int GetInsightARSceenHeight()
        {
            return trackResult.param.height;
        }
    }
#endregion
}
