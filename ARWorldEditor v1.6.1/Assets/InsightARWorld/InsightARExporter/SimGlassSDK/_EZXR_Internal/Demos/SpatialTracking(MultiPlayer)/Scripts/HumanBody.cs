using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using Wheels.Unity;

public class HumanBody : MonoBehaviour
{
    public TextMesh text_HP;
    public Transform hpRoot;
    int hp = 20;

    // Start is called before the first frame update
    void Start()
    {
        text_HP.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
        {
            hpRoot.LookAtWithAxisLock(HMDPoseTracker.Instance.Head, Vector3.forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        hp--;
        text_HP.text = hp.ToString();

        Destroy(other.gameObject);
    }
}
