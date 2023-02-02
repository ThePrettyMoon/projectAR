using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsightAR.Internal
{
    public class InsightCamerasManager
    {
        private static string TAG = "InsightARCameraTransformUpdate";
        public static Camera[] initARCameras()
        {
            var len = Camera.allCameras.Length;
            Camera[] cameras = Camera.allCameras;
            if (len <= 0)
            {
                return null;
            }

            List<Camera> arCameras = new List<Camera>();
            foreach (Camera cam in cameras)
            {
                InsightDebug.Log(TAG, cam.tag);
                if (cam.CompareTag(InsightSceneAssets.TagManager.arCamera))
                {
                    arCameras.Add(cam);
                }
            }
            return arCameras.ToArray();
        }

        public static Camera initBackgroundCamera()
        {
            var len = Camera.allCameras.Length;
            Camera[] cameras = Camera.allCameras;
            if (len <= 0)
                return null;

            foreach (Camera cam in cameras)
            {
                if (cam.CompareTag(InsightSceneAssets.TagManager.bgCamera))
                {
                    return cam;
                }
            }
            return null;
        }

        public static Camera initMaskCamera()
        {

            var len = Camera.allCameras.Length;
            Camera[] cameras = Camera.allCameras;
            if (len <= 0)
                return null;

            foreach (Camera cam in cameras)
            {
                if (cam.CompareTag(InsightSceneAssets.TagManager.maskCamera))
                {
                    return cam;
                }
            }
            return null;
        }
    }
}

