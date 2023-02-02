using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using UnityEngine.UI;

public class CityController : MonoBehaviour
{

    public GameObject showGameObject;
    //public Text trackingStatus;
    public Text planeStatus;
    public PlaneDetectionManager planeExtractor;

    private bool isCityInit = false;
    private const float MAXPLANEDETECTTIME = 5.0f;
    private float currentPlaneDetectTime = 0.0f;

    private void Awake()
    {
        Application.targetFrameRate = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100;
    }

    // Update is called once per frame
    void Update()
    {

        //switch (ARFrame.SessionStatus)
        //{
        //    case (EZVIOState.EZVIOCameraState_Initing):
        //        trackingStatus.text = "Tracking Status : " + "Initing";
        //        break;
        //    case (EZVIOState.EZVIOCameraState_Detecting):
        //        trackingStatus.text = "Tracking Status : " + "Detecting";
        //        break;
        //    case (EZVIOState.EZVIOCameraState_Tracking):
        //        trackingStatus.text = "Tracking Status : " + "Tracking";
        //        break;
        //    case (EZVIOState.EZVIOCameraState_Track_Limited):
        //        trackingStatus.text = "Tracking Status : " + "Track_Limited";
        //        break;
        //    case (EZVIOState.EZVIOCameraState_Track_Fail):
        //        trackingStatus.text = "Tracking Status : " + "Track_Fail";
        //        break;
        //    default:
        //        trackingStatus.text = "Tracking Status : " + "Initing";
        //        break;
        //}

        CityShow();
        planeStatus.text = "CityStatus : " + isCityInit;
    }

    private void CityShow()
    {
        if (!CheckAndInitCity())
        {
            if (showGameObject.gameObject.activeSelf)
            {
                showGameObject.gameObject.SetActive(true);
            }
        }
        else
        {
            if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking)
            {
                if (showGameObject.gameObject.activeSelf)
                    showGameObject.gameObject.SetActive(false);
            }
            else
            {
                showGameObject.gameObject.SetActive(true);
            }
        }
    }

    private bool CheckAndInitCity()
    {
        if (isCityInit) return true;

        if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking) return false;

        currentPlaneDetectTime += Time.deltaTime;
        if (ARFrame.trackableManager.planesTrackable.Count > 0)
        {
            foreach (UInt64 key in ARFrame.trackableManager.planesTrackable.Keys)
            {
                EZVIOPlane plane = ARFrame.trackableManager.planesTrackable[key];
                Matrix4x4 tr = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    tr[i] = plane.transform_unity[i];
                }

                Matrix4x4 tr_tp = tr.transpose;
                showGameObject.transform.position = tr_tp.GetColumn(3);

                isCityInit = true;
                break;
            }
        }
        else
        {
            if (currentPlaneDetectTime < MAXPLANEDETECTTIME)
                isCityInit = false;
            else
            {
                Transform cameraTransform = HMDPoseTracker.Instance.Head;
                showGameObject.transform.position = cameraTransform.position + 1.0f * cameraTransform.forward - 0.7f * cameraTransform.up;
                isCityInit = true;
            }

        }

        if (isCityInit)
            planeExtractor.SetIdling();
        return isCityInit;
    }
}
