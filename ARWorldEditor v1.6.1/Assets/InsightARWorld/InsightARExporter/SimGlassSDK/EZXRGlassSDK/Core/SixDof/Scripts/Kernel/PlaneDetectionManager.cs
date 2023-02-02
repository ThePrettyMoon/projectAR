using UnityEngine;
using System.Collections;
using EZXR.Glass.SixDof;

namespace EZXR.Glass.SixDof
{
    public class PlaneDetectionManager : MonoBehaviour
    {
        #region singleton
        private static PlaneDetectionManager _instance;
        public static PlaneDetectionManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private ulong m_FrameCount = 0;
        private EZVIOPlane planeInfo = new EZVIOPlane();
        private bool m_bRunning = true;

        public void SetIdling()
        {
            m_bRunning = false;
        }

        public void SetRunning()
        {
            m_bRunning = true;
        }

        private void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!m_bRunning)
            {
                //Debug.Log("=============Unity Log===============   PlaneExtractor -- Update   idling, no extracting planes");
                return;
            }

            m_FrameCount++;
            if (m_FrameCount % 30 == 0)
            {
                //Debug.Log("=============Unity Log===============   PlaneExtractor -- Update   time to extract plane");

                if (ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
                {
                    bool res = NativeTracking.GetPlane(ref planeInfo);

                    if (res)
                    {
                        ARFrame.trackableManager.UpdatePlane(planeInfo);
                        //Debug.Log("=============Unity Log===============   PlaneExtractor -- Update   extract plane, id: " + planeInfo.id);
                    }
                    else
                    {
                        //Debug.Log("=============Unity Log===============   PlaneExtractor -- Update   failed to extract plane cuz no plane got");
                    }
                }
                else
                {
                    //Debug.Log("=============Unity Log===============   PlaneExtractor -- Update   failed to extract plane cuz VIO state is not OK");
                }
            }
        }
    }
}

