using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace EZXR.Glass.SixDof
{
    public class NativeTracking
    {
        private static double m_LastTimeStamp;
        private static NativeARConfig convertConfigToNative(ARConfig config)
        {
            NativeARConfig nativeARConfig = new NativeARConfig();
            nativeARConfig.mHandsFindingMode = config.HandsFindingMode;
            nativeARConfig.mMarkerFindingMode = config.MarkerFindingMode;
            nativeARConfig.mMeshFindingMode = config.MeshFindingMode;
            nativeARConfig.mPlaneFindingMode = config.PlaneFindingMode;
            nativeARConfig.mPointCloudFindingMode = config.PointCloudFindingMode;
            nativeARConfig.mRelocalizationMode = config.RelocalizationMode;
            nativeARConfig.mMTPMode = config.MTPMode;
            return nativeARConfig;
        }

        public static void StartArServer()
        {
            NativeAPI.startArServer();
        }


        public static void StartARSession(ARConfig config)
        {
            NativeARConfig nativeARConfig = convertConfigToNative(config);
            NativeAPI.startARSessionNative(ref nativeARConfig);
        }
        public static void StopARSession()
        {
            NativeAPI.stopARSessionNative();
        }

        public static bool GetIsARSessionInited() { return NativeAPI.isARSessionInited(); }

        public static void GetTrackResult(ref EZVIOResult vioResult)
        {
            NativeAPI.getResult(ref vioResult);
            //Debug.Log("=======UNITY getResult : " + vioResult.vioState + " " + vioResult.timestamp);
            //Debug.Log("=======UNITY getResult : " + vioResult.camTransform[0] + " " + vioResult.camTransform[1] + " " + vioResult.camTransform[2] + " " + vioResult.camTransform[3] +
            //    " " + vioResult.camTransform[4] + " " + vioResult.camTransform[5] + " " + vioResult.camTransform[6] + " " + vioResult.camTransform[7] +
            //    " " + vioResult.camTransform[8] + " " + vioResult.camTransform[9] + " " + vioResult.camTransform[10] + " " + vioResult.camTransform[11] +
            //    " " + vioResult.camTransform[12] + " " + vioResult.camTransform[13] + " " + vioResult.camTransform[14] + " " + vioResult.camTransform[15]);
            if (m_LastTimeStamp == vioResult.timestamp) return;
            m_LastTimeStamp = vioResult.timestamp;
        }
        public static void getHistoricalResult(ref EZVIOResult vioResult, double imagetime)
        {
            NativeAPI.getHistoricalResult(ref vioResult, imagetime);
        }

        public static void GetArucoResult(ref EZVIOResult arucoResult)
        {
            NativeAPI.getArucoResult(ref arucoResult);
        }

        public static void GetRgbCameraTrackResult(ref EZVIOResult vioResult)
        {
            NativeAPI.getRgbCamResult(ref vioResult);
            //Debug.Log("=======UNITY getResult : " + vioResult.vioState + " " + vioResult.timestamp);
            //Debug.Log("=======UNITY getResult : " + vioResult.camTransform[0] + " " + vioResult.camTransform[1] + " " + vioResult.camTransform[2] + " " + vioResult.camTransform[3] +
            //    " " + vioResult.camTransform[4] + " " + vioResult.camTransform[5] + " " + vioResult.camTransform[6] + " " + vioResult.camTransform[7] +
            //    " " + vioResult.camTransform[8] + " " + vioResult.camTransform[9] + " " + vioResult.camTransform[10] + " " + vioResult.camTransform[11] +
            //    " " + vioResult.camTransform[12] + " " + vioResult.camTransform[13] + " " + vioResult.camTransform[14] + " " + vioResult.camTransform[15]);
            if (m_LastTimeStamp == vioResult.timestamp) return;
            m_LastTimeStamp = vioResult.timestamp;
        }

        public static void GetGlassDisplaySize(ref DisplaySize displaySize)
        {
            NativeAPI.getGlassDisplaySize(ref displaySize);
        }

        public static void GetFisheyeCameraResolution(ref CameraResolution cameraResolution)
        {
            NativeAPI.getFisheyeCameraResolution(ref cameraResolution);
        }

        public static void GetRGBCameraResolution(ref CameraResolution cameraResolution)
        {
            NativeAPI.getRGBCameraResolution(ref cameraResolution);
        }

        public static void GetPinholeCameraIntrinsic(ref NewCameraMatrix newK)
        {
            NativeAPI.getPinholeCameraIntrinsic(ref newK);
        }

        public static void GetOSTParams(ref OSTMatrices ostMatrices)
        {
            NativeAPI.getOSTParams(ref ostMatrices);
        }

        public static void GetCameraParams(ref CamParams cameraParams)
        {
            NativeAPI.getCameraParams(ref cameraParams);
            //cameraParams.height = 720; cameraParams.width = 1280; //cameraParams.fov[0] = 69.7030146; cameraParams.fov[1] = 69.7030146;
            //cameraParams.fov[0] = 20; cameraParams.fov[1] = 20;
            // Debug.Log("=======UNITY GetCameraParams : " + cameraParams.width + " " + cameraParams.height + " " + cameraParams.fov[0] + " " + cameraParams.fov[1]);
        }

        public static bool GetPlane(ref EZVIOPlane plane)
        {
            //  Debug.Log("Unity Call Native Tracking GetPlane ");
            return NativeAPI.getPlane(ref plane);
        }

        public static void GetPointCloud(ref EZVIOPointCloud cloud)
        {
            // Debug.Log("Unity Call Native Tracking GetPointCloud ");
            NativeAPI.getPointCloud(ref cloud);
        }

        public static void UpdateOnGLThread()
        {
            NativeAPI.updateOnGlThread();
        }

        public static void SetRGBTextureId(IntPtr textureID)
        {
            NativeAPI.setRGBTextureId(textureID);
        }

        public static IntPtr GetRenderEventFunc()
        {
            return NativeAPI.getRenderEventFunc();
        }

        public static int GetTextureId()
        {
            return NativeAPI.getTextureId();
        }

        public static void UpdateOnGlThread_WithImgUndistorted()
        {
            NativeAPI.UpdateOnGlThread_WithImgUndistorted();
        }
        /*
        public static bool GetBackendMeshData(ref EZVIOBackendMesh meshData)
        {
            return NativeAPI.getBackendMeshData(ref meshData);
        }

        public static bool RunPauseMeshAndDFS()
        {
            return NativeAPI.runPauseMeshAndDFS();
        }

        public static bool RunResumeMeshAndDFS()
        {
            return NativeAPI.runResumeMeshAndDFS();
        }*/

        public static bool RunSaveMesh()
        {
            return NativeAPI.runSaveMesh();
        }
        /*
        public static bool GetBackendSmoothedMesh(ref EZVIOBackendMesh meshData)
        {
            return NativeAPI.getBackendSmoothedMeshData(ref meshData);
        }*/

        public static bool GetBackendIncrementalMeshData(ref EZVIOBackendIncrementalMesh meshData)
        {
            return NativeAPI.getBackendIncrementalMeshData(ref meshData);
        }

        //public static void Release()
        //{
        // //   Debug.Log("Unity Call Native Tracking Release ");
        //    NativeAPI.release(sSessionId);
        //}

        public static long GetSystemTime()
        {
            return NativeAPI.getSystemTimeInNanoSeconds();
        }

        public static void GetRGBCameraIntrics(float[] intrinsic)
        {
            NativeAPI.nativeGetRGBCameraIntrics(intrinsic);
        }

        public static void SetSlamMode(int mode, string map_path)
        {
            NativeAPI.setSlamMode(mode, map_path);
        }
        public static void GetVIO2Map(ref EZVIORelativePose vio_2_map)
        {
            NativeAPI.getVIO2Map(ref vio_2_map);
        }
        public static void resumeMtp()
        {
            NativeAPI.resumeMtp();
        }
        public static void pauseMtp()
        {
            NativeAPI.pauseMtp();
        }

        public static bool GetCurrentImage(ref EZVIOInputImage image, float[] intrinsic)
        {
            return NativeAPI.nativeGetCurrentImage(1, ref image, intrinsic);
        }
        [Obsolete]
        public static bool GetCuttedTrackingImage(ref EZVIOInputImage image, float[] intrinsic)
        {
            // Debug.Log("Unity Call Native Tracking GetPointCloud ");
            return NativeAPI.nativeGetCuttedTrackingImage(ref image, intrinsic);
        }
        private struct NativeARConfig
        {
            public PlaneFindingMode mPlaneFindingMode;
            public MeshFindingMode mMeshFindingMode;
            public HandsFindingMode mHandsFindingMode;
            public PointCloudFindingMode mPointCloudFindingMode;
            public RelocalizationMode mRelocalizationMode;
            public MarkerFindingMode mMarkerFindingMode;
            /// <summary>
            ///  see: "enum MTPModeOptions" and "mMTPMode" @ARConfig.cs
            /// </summary>
            public int mMTPMode;
        }

        private partial struct NativeAPI
        {
#if UNITY_ANDROID

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void startArServer();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool startARSessionNative(ref NativeARConfig config);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void stopARSessionNative();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool isARSessionInited();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getResult(ref EZVIOResult result);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getHistoricalResult(ref EZVIOResult result, double imagetime);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getRgbCamResult(ref EZVIOResult result);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getArucoResult(ref EZVIOResult result);        
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void updateOnGlThread();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void setRGBTextureId(IntPtr textureID);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern IntPtr getRenderEventFunc();


        [DllImport(NativeConsts.NativeLibrary)]
        public static extern int getTextureId();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool getGlassDisplaySize(ref DisplaySize display_size);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool getFisheyeCameraResolution(ref CameraResolution camera_res);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool getRGBCameraResolution(ref CameraResolution camera_res);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool getPinholeCameraIntrinsic(ref NewCameraMatrix newK);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getOSTParams(ref OSTMatrices ostMatrices);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getCameraParams(ref CamParams camParams);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool getPlane(ref EZVIOPlane ezvioPlane);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getPointCloud(ref EZVIOPointCloud pointCloud);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool nativeGetCurrentImage(int cameraID, ref EZVIOInputImage image, float[] intrinsic);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void UpdateOnGlThread_WithImgUndistorted();

        //[DllImport(NativeConsts.NativeLibrary)]
        //public static extern bool getBackendMeshData(ref EZVIOBackendMesh meshData);

        //[DllImport(NativeConsts.NativeLibrary)]
        //public static extern bool runPauseMeshAndDFS();

        //[DllImport(NativeConsts.NativeLibrary)]
        //public static extern bool runResumeMeshAndDFS();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool runSaveMesh();

        //[DllImport(NativeConsts.NativeLibrary)]
        //public static extern bool getBackendSmoothedMeshData(ref EZVIOBackendMesh meshData);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool getBackendIncrementalMeshData(ref  EZVIOBackendIncrementalMesh meshData);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern long getSystemTimeInNanoSeconds();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void nativeGetRGBCameraIntrics(float[] intrinsic);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void setSlamMode(int mode, string map_path);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getVIO2Map(ref EZVIORelativePose vio_2_map);

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void resumeMtp();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void pauseMtp();

        [Obsolete]
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void getCurrentImage(ref EZVIOInputImage image);

        [Obsolete]
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool nativeGetCuttedTrackingImage(ref EZVIOInputImage image,float[] intrinsic);
#else
            //public static long init(long sessionId,string model, string modelPath,string glassName)
            //{
            //    return 10000;
            //}

            //public static void release(long sessionId) { }

            public static bool startArServer() { return true; }
            public static void startARSessionNative(ref NativeARConfig config) { }
            public static void stopARSessionNative() { }
            public static bool isARSessionInited() { return true; }

            public static void getResult(ref EZVIOResult result)
            {
                //Debug.Log("===========UNITY_EDITOR Get result : " + result.vioState);
                //if (Time.time < 5)
                //{
                result.vioState = EZVIOState.EZVIOCameraState_Detecting;
                //}
                //else
                //{
                //    result.vioState = EZVIOState.EZVIOCameraState_Tracking;
                //}
                result.timestamp = (double)Time.time;
                result.camTransform = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            }
            public static void getHistoricalResult(ref EZVIOResult result, double imagetime)
            {
                //Debug.Log("===========UNITY_EDITOR Get result : " + result.vioState);
                //if (Time.time < 5)
                //{
                result.vioState = EZVIOState.EZVIOCameraState_Detecting;
                //}
                //else
                //{
                //    result.vioState = EZVIOState.EZVIOCameraState_Tracking;
                //}
                result.timestamp = imagetime;
                result.camTransform = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            }

            public static void getArucoResult(ref EZVIOResult result)
            {
                result.vioState = EZVIOState.EZVIOCameraState_Detecting;
                result.timestamp = (double)Time.time;
                result.camTransform = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            }

            public static void getRgbCamResult(ref EZVIOResult result)
            {
                //Debug.Log("===========UNITY_EDITOR Get result : " + result.vioState);
                //if (Time.time < 5)
                //{
                result.vioState = EZVIOState.EZVIOCameraState_Detecting;
                //}
                //else
                //{
                //    result.vioState = EZVIOState.EZVIOCameraState_Tracking;
                //}
                result.timestamp = (double)Time.time;
                result.camTransform = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            }

            //public static string getDebugInfo()
            //{
            //    return "debug test";
            //}

            public static void updateOnGlThread() { }

            public static void setRGBTextureId(IntPtr textureID) { }

            public static IntPtr getRenderEventFunc() { return IntPtr.Zero; }

            public static int getTextureId() { return 0; }

            public static void getCameraParams(ref CamParams camParams)
            {
                camParams.fov = new double[2] { 50.0f, 50.0f };
                camParams.width = 640;
                camParams.height = 240;
                camParams.leftProjection = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, -1, 0 };
                camParams.rightProjection = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, -1, 0 };
            }

            public static bool getPlane(ref EZVIOPlane ezvioPlane) { return true; }

            public static void getPointCloud(ref EZVIOPointCloud pointCloud) { }

            public static bool nativeGetCurrentImage(int cameraID, ref EZVIOInputImage image, float[] intrinsic) { return false; }
            [Obsolete]
            public static void getCurrentImage(ref EZVIOInputImage image) { }

            [Obsolete]
            public static bool nativeGetCuttedTrackingImage(ref EZVIOInputImage image, float[] intrinsic)
            {
                return false;
            }

            public static void UpdateOnGlThread_WithImgUndistorted() { }

            //public static bool getBackendMeshData(ref EZVIOBackendMesh meshData) { return false; }

            //public static bool runPauseMeshAndDFS() { return true; }

            //public static bool runResumeMeshAndDFS() { return true; }

            public static bool runSaveMesh() { return true; }

            //public static bool getBackendSmoothedMeshData(ref EZVIOBackendMesh meshData) { return false; }

            public static bool getGlassDisplaySize(ref DisplaySize display_size) { return true; }

            public static bool getFisheyeCameraResolution(ref CameraResolution camera_res) { return true; }

            public static bool getRGBCameraResolution(ref CameraResolution camera_res) { return true; }

            public static bool getPinholeCameraIntrinsic(ref NewCameraMatrix newK) { return true; }

            public static void getOSTParams(ref OSTMatrices ostMatrices) { }

            public static bool getBackendIncrementalMeshData(ref EZVIOBackendIncrementalMesh meshData) { return false; }

            public static long getSystemTimeInNanoSeconds() { return 0; }

            public static void nativeGetRGBCameraIntrics(float[] intrinsic) { }

            public static void setSlamMode(int mode, string map_path) { }
            public static void getVIO2Map(ref EZVIORelativePose vio_2_map) { }
            public static void resumeMtp() { }
            public static void pauseMtp() { }

#endif
        }
    }

}
