using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EZXR.Glass.SixDof;

namespace EZXR.Glass.SpatialMesh
{
    public class SpatialMeshManager : MonoBehaviour
    {
        #region singleton
        private static SpatialMeshManager _instance;
        public static SpatialMeshManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        public uint chunkUpdateLimitation = 5;

        private GameObject m_MeshRoot;
        public Dictionary<Vector3Int, GameObject> m_MeshGameObjects;

        PhysicMaterial pm;

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
           
            Application.targetFrameRate = 60;
        }

        // Use this for initialization
        void Start()
        {
            if (chunkUpdateLimitation == 0)
                chunkUpdateLimitation = 5;

            m_MeshRoot = new GameObject();
            m_MeshRoot.name = "backendmesh";
            m_MeshRoot.transform.position = new Vector3(0, 0, 0);
            m_MeshRoot.transform.rotation = new Quaternion(0, 0, 0, 1);
            m_MeshGameObjects = new Dictionary<Vector3Int, GameObject>();

            pm = new PhysicMaterial();
            pm.staticFriction = 0.9f;//设置Mesh通用物理材质

            // 注册回调
            ARFrame.trackableManager.recenterIncrementalMeshesListener += RecenterMeshes;
        }

        // Update is called once per frame
        void Update()
        {
            MeshesShow();
        }

        private void MeshesShow()
        {
            // Meshes
            if (ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
            {
                m_MeshRoot.SetActive(true);

                if (ARFrame.trackableManager.chunksToUpdate.Count > 0)
                {

                    List<Vector3Int> chunksToUpdate = ARFrame.trackableManager.chunksToUpdate;
                    List<Vector3[]> cachedVSToUpdate = ARFrame.trackableManager.cachedVSToUpdate;
                    List<int[]> cachedTSToUpdate = ARFrame.trackableManager.cachedTSTupUpdate;

                    int count = Math.Min(chunksToUpdate.Count, (int)chunkUpdateLimitation);

                    Debug.Log("=============Unity Log===============   MeshesController -- MeshesShow " + chunksToUpdate.Count + " tobeupdate, really use " + count);

                    Debug.Log("=============Unity Log===============   MeshesController -- MeshesShow BEFORE chunkstoupdate totally " + m_MeshGameObjects.Count + " gameobjects");

                    for (int i = 0; i < count; i++)
                    {
                        Vector3Int chunkIdx = chunksToUpdate[i];
                        if (!m_MeshGameObjects.ContainsKey(chunkIdx))
                        {
                            GameObject newMesh = new GameObject();
                            newMesh.name = "backendmesh" + chunkIdx;
                            //newMesh.tag = "NoInteraction";
                            newMesh.transform.position = new Vector3(0, 0, 0);
                            newMesh.transform.rotation = new Quaternion(0, 0, 0, 1);

                            MeshFilter mf = newMesh.AddComponent<MeshFilter>();
                            MeshRenderer mr = newMesh.AddComponent<MeshRenderer>();
                            MeshCollider mc = newMesh.AddComponent<MeshCollider>();
                            Material material = new Material(Shader.Find("SuperSystems/SpatialMapping"));

                            mf.mesh = new Mesh();
                            mr.material = material;

                            mr.allowOcclusionWhenDynamic = true;

                            newMesh.layer = LayerMask.NameToLayer("Mesh");

                            newMesh.transform.parent = m_MeshRoot.transform;

                            m_MeshGameObjects.Add(chunkIdx, newMesh);
                        }

                        m_MeshGameObjects[chunkIdx].GetComponent<MeshFilter>().sharedMesh.Clear();
                        m_MeshGameObjects[chunkIdx].GetComponent<MeshFilter>().sharedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                        m_MeshGameObjects[chunkIdx].GetComponent<MeshFilter>().sharedMesh.vertices = cachedVSToUpdate[i];
                        m_MeshGameObjects[chunkIdx].GetComponent<MeshFilter>().sharedMesh.triangles = cachedTSToUpdate[i];

                        m_MeshGameObjects[chunkIdx].GetComponent<MeshCollider>().sharedMesh = m_MeshGameObjects[chunkIdx].GetComponent<MeshFilter>().sharedMesh;
                        m_MeshGameObjects[chunkIdx].GetComponent<MeshCollider>().material = pm;
                    }

                    ARFrame.trackableManager.CommitCachedChunksUse(count);

                    Debug.Log("=============Unity Log===============   MeshesController -- MeshesShow AFTER chunkstoupdate totally " + m_MeshGameObjects.Count + " gameobjects");
                }
            }
            else
            {
                m_MeshRoot.SetActive(false);
            }
        }

        private void RecenterMeshes(Matrix4x4 recenterOffset)
        {
            if (m_MeshGameObjects != null)
            {
                foreach (var item in m_MeshGameObjects)
                {
                    for (int i = 0; i < item.Value.GetComponent<MeshFilter>().sharedMesh.vertices.Length; i++)
                    {
                        Vector3 tmp = item.Value.GetComponent<MeshFilter>().sharedMesh.vertices[i];
                        item.Value.GetComponent<MeshFilter>().sharedMesh.vertices[i] = recenterOffset.MultiplyPoint3x4(tmp);
                    }
                }
            }
        }
    }
}