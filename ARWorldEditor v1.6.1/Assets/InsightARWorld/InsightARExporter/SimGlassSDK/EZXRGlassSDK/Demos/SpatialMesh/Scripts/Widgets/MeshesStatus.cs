using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using UnityEngine.UI;
using EZXR.Glass.SpatialMesh;

namespace EZXR.Glass.SpatialPositioning
{
    public class MeshesStatus : MonoBehaviour
    {
        public Text meshesStatus;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            meshesStatus.text = "gameobj chunk num : " + SpatialMeshManager.Instance.m_MeshGameObjects.Count;
        }
    }
}