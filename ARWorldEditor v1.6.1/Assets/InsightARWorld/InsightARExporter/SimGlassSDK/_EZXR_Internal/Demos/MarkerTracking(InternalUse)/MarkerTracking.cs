using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;

public class MarkerTracking : MonoBehaviour
{
    public Transform marker;
    EZVIOResult EZVIOResult = new EZVIOResult();
    Pose pose = new Pose();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NativeTracking.GetArucoResult(ref EZVIOResult);
        if (EZVIOResult.vioState == EZVIOState.EZVIOCameraState_Tracking)
        {
            Matrix4x4 tr = new Matrix4x4();
            for (int i = 0; i < 16; i++)
            {
                tr[i] = EZVIOResult.camTransform[i];
            }
            Matrix4x4 tr_tp = tr.transpose;
            Matrix4x4 transformUnity_new = ARFrame.accumulatedRecenterOffset4x4 * tr_tp;


            Vector3 pos = transformUnity_new.GetColumn(3);
            Quaternion rot = transformUnity_new.rotation;
            marker.position = pos;
            marker.rotation = rot;
        }
    }
}
