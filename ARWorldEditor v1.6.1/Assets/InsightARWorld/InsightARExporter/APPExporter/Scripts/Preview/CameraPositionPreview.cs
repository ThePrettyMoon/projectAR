#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using ARWorldEditor;
using UnityEngine;

namespace ARWorldEditor
{
    public class CameraPositionPreview :MonoBehaviour
    {
        #region params
        [SerializeField]
        private int lerpFrameCount = 50;
        [SerializeField]
        private bool rotateOrientation = false;
        [SerializeField]
        private int mapId;
        [SerializeField]
        private int currentCameraId;
        [SerializeField]
        private int currentCameraIdx;
        [SerializeField]
        private List<int> cameraIds;
        [SerializeField]
        private int currentFrameIndex;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private Dictionary<int, List<CameraPosition>> cameraPoseDict;      //存储相机位置
        [SerializeField]
        private int currentPositionIndex;
        [SerializeField]
        private int nextPositionIndex;
        [SerializeField]
        private List<CameraParam> cameraParamList;
        [SerializeField]
        private GameObject characterObject;
        #endregion


        #region unity functions
        private void OnEnable()
        {
            Init();
      
        }

        private void Update()
        {
            UpdateCameraFov();
            UpdateCharacterPosition();
        }

        private void OnDisable()
        {
            Close();
        }

        private void OnDestroy()
        {

        }
        #endregion

        #region custom functions
        public void InitPath(long id)
        {
            this.mapId = (int)id;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            mainCamera = Camera.main;
            currentPositionIndex = 0;
            currentCameraIdx = 0;
            characterObject = GameObject.Find("toolman");
            if(characterObject!=null) characterObject.GetComponent<Animator>().SetBool("walk", true);

            string mapDirectory = string.Format(ConfigGlobal.MAP_PATH+"{0}/{1}", mapId, (int)MapResourcesType.RESOURCE_TYPE_POSE_SERIES);
            string cameraParamPath = Path.Combine(mapDirectory, "cameras.txt");
            string cameraPositonPath = Path.Combine(mapDirectory, "images.txt");

            cameraParamList = CameraParam.Parse(cameraParamPath);
            cameraPoseDict = CameraPosition.Parse(cameraPositonPath);

            if (cameraParamList != null && cameraParamList.Count > 0)
            {
                currentCameraId = cameraParamList[currentCameraIdx].GetCameraId();
                cameraIds = CameraParam.GetCameraIds(cameraParamList);
            }
        }

        /// <summary>
        /// 更新相机位置
        /// </summary>
        private void UpdateCharacterPosition()
        {
            if (mainCamera == null) return;
            if (cameraPoseDict == null || cameraPoseDict.Count < 0) return;
            if (!cameraPoseDict.ContainsKey(currentCameraId)) return;
            List<CameraPosition> cameraPoseList = cameraPoseDict[currentCameraId];
            if (cameraPoseList == null || cameraPoseList.Count < 2) return;

            if (currentFrameIndex >= lerpFrameCount)
            {
                if (currentPositionIndex >= cameraPoseList.Count - 1) currentPositionIndex = 0;
                else currentPositionIndex++;
                currentFrameIndex = 0;
            }
            else
            {
                currentFrameIndex++;
            }

            nextPositionIndex = currentPositionIndex + 1;
            if (nextPositionIndex >= cameraPoseList.Count - 1) nextPositionIndex = 0;

            Pose currentPose = ARWorldEditor.CameraUtility.OpenCVToUnity(cameraPoseList[currentPositionIndex].GetCVPose(), rotateOrientation);
            Pose nextPose = ARWorldEditor.CameraUtility.OpenCVToUnity(cameraPoseList[nextPositionIndex].GetCVPose(), rotateOrientation);

            float t = (float)currentFrameIndex / lerpFrameCount;
            Vector3 position = Vector3.Lerp(currentPose.position, nextPose.position, t);
            Quaternion rotation = Quaternion.Lerp(currentPose.rotation, nextPose.rotation, t);

            mainCamera.transform.position = position;
            mainCamera.transform.rotation = rotation;

            //人物高度降低1.6米,零点在脚底
            characterObject.transform.position = position - Vector3.up * 1.6f;
            Vector3 lookAtPosition = mainCamera.transform.position + mainCamera.transform.forward * 2.0f;
            lookAtPosition.y = characterObject.transform.position.y;
            characterObject.transform.LookAt(lookAtPosition);
        }

        /// <summary>
        /// 更新相机视场角
        /// </summary>
        private void UpdateCameraFov()
        {
            if (cameraParamList == null || mainCamera == null) return;

            var cameraParam = cameraParamList.Find(p => p.GetCameraId() == currentCameraId);
            if (cameraParam == null) return;

            int width = cameraParam.GetWidth();
            int height = cameraParam.GetHeight();
            double fx = cameraParam.GetFx();
            double fy = cameraParam.GetFy();

            float fovX = (float)Math.Atan2((double)(width / 2), fx) * Mathf.Rad2Deg * 2;
            float fovY = (float)Math.Atan2((double)(height / 2), fy) * Mathf.Rad2Deg * 2;
            mainCamera.fieldOfView = CameraUtility.CalculateFov(width, height, Screen.width, Screen.height, new float[2] { fovX, fovY });
        }





        /// <summary>
        /// close
        /// </summary>
        private void Close()
        {
            if (cameraPoseDict == null) return;
            cameraPoseDict.Clear();
            currentPositionIndex = 0;
            if (characterObject != null) characterObject.GetComponent<Animator>().SetBool("walk", false);
        }

        #endregion
    }
}

#endif








