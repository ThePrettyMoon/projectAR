using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using UnityEngine.UI;

public class PlaneDetectionDemo : MonoBehaviour
{
    public Text planeStatus;

    private Dictionary<UInt64, GameObject> m_allPlanes = new Dictionary<UInt64, GameObject>();

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        PlanesShow();

        planeStatus.text = "Plane count : " + m_allPlanes.Count.ToString() + ", planesTrackable Count: " + ARFrame.trackableManager.planesTrackable.Count;
    }

    private void PlanesShow()
    {
        // PLANES
        if (ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
        {

            foreach (UInt64 key in m_allPlanes.Keys)
            {
                m_allPlanes[key].SetActive(true);
            }

            foreach (UInt64 key in ARFrame.trackableManager.planesTrackable.Keys)
            {

                EZVIOPlane plane = ARFrame.trackableManager.planesTrackable[key];
                GameObject planeObj;
                if (m_allPlanes.ContainsKey(key))
                {
                    // update
                    planeObj = m_allPlanes[key];
                }
                else
                {
                    // new
                    planeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //Material material = new Material(Shader.Find("SuperSystems/SpatialMapping"));
                    //planeObj.GetComponent<MeshRenderer>().material = material;
                    planeObj.GetComponent<MeshRenderer>().material.SetFloat("_alpha", 0.4f);
                    m_allPlanes.Add(key, planeObj);
                }

                Matrix4x4 tr = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    tr[i] = plane.transform_unity[i];
                }

                Matrix4x4 tr_tp = tr.transpose;

                // TODO: @xuninghao 细节操作下沉
                {
                    // rotation and position
                    planeObj.transform.localPosition = tr_tp.GetColumn(3);
                    planeObj.transform.localRotation = tr_tp.rotation;

                    // scale
                    {
                        Vector3 l1 = new Vector3(plane.rect[0 * 3 + 0] - plane.rect[1 * 3 + 0], plane.rect[0 * 3 + 1] - plane.rect[1 * 3 + 1], plane.rect[0 * 3 + 2] - plane.rect[1 * 3 + 2]);
                        Vector3 l2 = new Vector3(plane.rect[1 * 3 + 0] - plane.rect[2 * 3 + 0], plane.rect[1 * 3 + 1] - plane.rect[2 * 3 + 1], plane.rect[1 * 3 + 2] - plane.rect[2 * 3 + 2]);

                        float d1 = l1.magnitude;
                        float d2 = l2.magnitude;

                        if (d1 > d2)
                        {
                            planeObj.transform.localScale = new Vector3(d1, 0.005f, d2);
                        }
                        else
                        {
                            planeObj.transform.localScale = new Vector3(d2, 0.005f, d1);
                        }
                    }
                }

                // color
                planeObj.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.gray);
            }
        }
        else
        {
            foreach (UInt64 key in m_allPlanes.Keys)
            {
                m_allPlanes[key].SetActive(false);
            }
        }
    }
}
