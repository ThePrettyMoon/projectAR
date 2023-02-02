using UnityEngine;
using System.Collections;
using EZXR.Glass.SixDof;
using UnityEngine.UI;
using EZXR.Glass.Common;

namespace EZXR.Glass.SixDof
{
    [ScriptExecutionOrder(-10)]
    public class IncrementalMeshExtractor : MonoBehaviour
    {
        private ulong m_FrameCount = 0;
        private EZVIOBackendIncrementalMesh m_IncrementalMesh = new EZVIOBackendIncrementalMesh();
        private bool m_OnceTracking = false;

        public uint meshExtractFrame = 30;
        //public Button startMeshButtion;
        //public Button stopMeshButtion;

        // Use this for initialization
        void Start()
        {
            if(meshExtractFrame == 0)
            {
                meshExtractFrame = 30;
            }

            //startMeshButtion.onClick.AddListener(HandleStartMeshButtonClick);
            //stopMeshButtion.onClick.AddListener(HandleStopMeshGetClick);

            HandleStartMeshButtonClick();

            //this.startMeshButtion.interactable = false; // 当前mesh是默认运行的
            //this.stopMeshButtion.interactable = true;
        }

        private void HandleStartMeshButtonClick()
        {
            if (m_OnceTracking)
            {
                Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- HandleStartMeshButtonClick   START mesh process");
                //NativeTracking.RunResumeMeshAndDFS();
                //this.startMeshButtion.interactable = false;
                //this.stopMeshButtion.interactable = true;
            }
            else
            {
                Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- HandleStartMeshButtonClick   FAILED START mesh procss, not tracking once");
            }
        }

        private void HandleStopMeshGetClick()
        {
            if (m_OnceTracking)
            {
                Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- HandleStopMeshGetClick   STOP mesh process");

                //NativeTracking.RunPauseMeshAndDFS();
                //this.startMeshButtion.interactable = true;
                //this.stopMeshButtion.interactable = false;
            }
            else
            {
                //Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- HandleStopMeshGetClick   FAILED STOP mesh procss, not tracking once");
            }
        }

        // Update is called once per frame
        void Update()
        {
            m_FrameCount++;
            if(m_FrameCount % meshExtractFrame == 0)
            { 
                //Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- Update   time to extract mesh");

                if(ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
                {
                    if (!m_OnceTracking)
                        m_OnceTracking = true;
                    if (NativeTracking.GetBackendIncrementalMeshData(ref m_IncrementalMesh))
                    {
                        ARFrame.trackableManager.UpdateIncrementalMeshes(m_IncrementalMesh);
                        //Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- Update   extract mesh with " + m_IncrementalMesh.chunksCount + " chunks");
                    }
                    else
                    {
                        //Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- Update   failed to extract plane cuz no mesh got");
                    }
                }
                else
                {
                    //Debug.Log("=============Unity Log===============   IncrementalMeshExtractor -- Update   failed to extract mesh cuz VIO state is not OK");
                }
            }
        }
    }
}
