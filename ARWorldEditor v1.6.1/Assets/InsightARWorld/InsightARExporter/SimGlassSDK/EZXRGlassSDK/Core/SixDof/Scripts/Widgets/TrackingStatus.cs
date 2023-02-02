using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using UnityEngine.UI;

namespace EZXR.Glass.SixDof
{
    public class TrackingStatus : MonoBehaviour
    {
        public Text trackingStatus;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch (ARFrame.SessionStatus)
            {
                case (EZVIOState.EZVIOCameraState_Initing):
                    trackingStatus.text = "Tracking Status : " + "Initing";
                    break;
                case (EZVIOState.EZVIOCameraState_Detecting):
                    trackingStatus.text = "Tracking Status : " + "Detecting";
                    break;
                case (EZVIOState.EZVIOCameraState_Tracking):
                    trackingStatus.text = "Tracking Status : " + "Tracking";
                    break;
                case (EZVIOState.EZVIOCameraState_Track_Limited):
                    trackingStatus.text = "Tracking Status : " + "Track_Limited";
                    break;
                case (EZVIOState.EZVIOCameraState_Track_Fail):
                    trackingStatus.text = "Tracking Status : " + "Track_Fail";
                    break;
                default:
                    trackingStatus.text = "Tracking Status : " + "Initing";
                    break;
            }
        }
    }
}