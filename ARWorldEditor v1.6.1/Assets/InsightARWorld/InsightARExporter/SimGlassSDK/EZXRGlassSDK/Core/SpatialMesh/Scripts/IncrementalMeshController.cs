using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using UnityEngine.AI;
using System; 

public enum IncrementalMeshState { NONE = 0, GETTING = 1, GETTED, STOPING = 2, STOPED = 3, INIT = 4 };
[DefaultExecutionOrder(-200)]

public class IncrementalMeshController : MonoBehaviour
{
    public Text meshStatus;
    public Text pointsNum;
    public Button meshButton;
    public Button stopMeshButton;
    public Button meshOC;
    public Button disableMesh;
    public Button SaveMesh;

    //private List<GameObject> meshGameObjects;
    private Dictionary<Vector3Int, GameObject> meshGameObjects;
    private List<Vector3Int> beUpdated;
    private GameObject meshRoot;
    private EZVIOBackendIncrementalMesh incrementalMesh = new EZVIOBackendIncrementalMesh();

    // private bool isGettingMesh = false;
    private bool isMeshGet = false;
    private bool isMeshGetDone = false;
    private bool isShowMesh = true;
    private bool beSaveMesh = false;

    private Dictionary<Vector3Int, int> chunkStatus;

    IncrementalMeshState incre_meshState;

    Thread meshThread;
    private Mutex mutex = new Mutex();

    // private List<MeshFilter> m_Meshes;
    private List<Vector3[]> m_Meshes_vs;
    private List<int[]> m_Meshes_ts;

    public static List<MeshFilter> nav_Meshes = new List<MeshFilter>();

    PhysicMaterial pm;
    public int BlockUpdateLimitation = 5;
    public int index_current = 0;

    // Start is called before the first frame update
    void Start()
    {
        meshRoot = new GameObject();
        meshRoot.name = "backendmesh";

        meshGameObjects = new Dictionary<Vector3Int, GameObject>();
        beUpdated = new List<Vector3Int>();
        chunkStatus = new Dictionary<Vector3Int, int>();

        // m_Meshes = new List<MeshFilter>();
        m_Meshes_vs = new List<Vector3[]>();
        m_Meshes_ts = new List<int[]>();

        meshButton.onClick.AddListener(HandleMeshButtonClick);
        stopMeshButton.onClick.AddListener(HandleStopMeshGetClick);
        SaveMesh.onClick.AddListener(HandleSaveMeshClick);
        meshOC.onClick.AddListener(HandleMeshOCClick);
        disableMesh.onClick.AddListener(HandleDisableMeshClick);

        Debug.Log("==========UNITY LOG : Start Init Mesh");

        meshButton.enabled = true;
        meshButton.interactable = true;
        stopMeshButton.enabled = false;
        stopMeshButton.interactable = false;

        incre_meshState = IncrementalMeshState.NONE;
        pm = new PhysicMaterial();
        pm.staticFriction = 0.9f;//设置Mesh通用物理材质
#if UNITY_ANDROID
        //@buqing 默认一开始就开始开启mesh
        HandleMeshButtonClick();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        meshStatus.text = "MeshStatus : " + isMeshGet;
        if (mutex.WaitOne())
        {
            if (/*isMeshGetDone &&*/incre_meshState!= IncrementalMeshState.NONE && meshRoot.activeSelf)
            {
                if(beUpdated.Count <= 0){
                    index_current = 0;
                    beUpdated.Clear();
                    m_Meshes_vs.Clear();
                    m_Meshes_ts.Clear();
                    GetBackEndMesh();
 
                }else{
                    int count = Math.Min(beUpdated.Count, BlockUpdateLimitation);
                    for (int i = 0; i < count; i++)
                    {
                        UpdateMesh(beUpdated[i], i+index_current);
                    }
                    index_current += count;
                    beUpdated.RemoveRange(0, count);
                }
                // for (int i = 0; i < beUpdated.Count; i++)
                // {
                //     UpdateMesh(beUpdated[i], i);
                // }

            }
            mutex.ReleaseMutex();
        }
        if (incre_meshState == IncrementalMeshState.STOPED)
        {
            incre_meshState = IncrementalMeshState.NONE;
            nav_Meshes.Clear();
            foreach(var item in meshGameObjects)
            {
                nav_Meshes.Add(item.Value.GetComponent<MeshFilter>());
            }
            //for(int i = 0; i < meshGameObjects.Count; i ++)
            //{
            //    m_Meshes.Add(meshGameObjects[i].GetComponent<MeshFilter>());
            //}
        }
    }

    private void UpdateMesh(Vector3Int idx, int update_idx)
    {
        if(!meshGameObjects.ContainsKey(idx))
        {
            GameObject newMesh = new GameObject();
            newMesh.name = "backendmesh" + idx;
            MeshFilter mf = newMesh.AddComponent<MeshFilter>();
            MeshRenderer mr = newMesh.AddComponent<MeshRenderer>();

            MeshCollider mc = newMesh.AddComponent<MeshCollider>();

            Material material = new Material(Shader.Find("SuperSystems/SpatialMapping"));//new Material(Shader.Find("SuperSystems /Wireframe"));//new Material(Shader.Find("VR/SpatialMapping/Wireframe"));////new Material(Shader.Find("Unlit/Color"));
                                                                                         // material.renderQueue = 2000;
            mf.mesh = new Mesh();
            mr.material = material;
            mr.allowOcclusionWhenDynamic = true;

            newMesh.layer = LayerMask.NameToLayer("Mesh");

            newMesh.transform.parent = meshRoot.transform;

            meshGameObjects.Add(idx, newMesh);
        }
        // MeshFilter tmp = m_Meshes[update_idx];
        meshGameObjects[idx].GetComponent<MeshFilter>().sharedMesh.Clear();
        meshGameObjects[idx].GetComponent<MeshFilter>().sharedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        meshGameObjects[idx].GetComponent<MeshFilter>().sharedMesh.vertices = /*tmp.sharedMesh.vertices*/m_Meshes_vs[update_idx];
        meshGameObjects[idx].GetComponent<MeshFilter>().sharedMesh.triangles = /*tmp.sharedMesh.triangles*/m_Meshes_ts[update_idx];

        meshGameObjects[idx].GetComponent<MeshCollider>().sharedMesh = meshGameObjects[idx].GetComponent<MeshFilter>().sharedMesh;
        meshGameObjects[idx].GetComponent<MeshCollider>().material = pm;
    }

    public void GetMeshData(EZVIOBackendIncrementalMesh backendMesh)
    {
        // int vetexNum = backendMesh.vertexCount;
        // int faceNum = backendMesh.facesCount;
        // int normalNum = backendMesh.normalCount;
        int chunkNum = backendMesh.chunksCount;
        // Debug.Log("===========UNITY LOG chunks num: " + chunkNum);

        EZVIOChunkInfo[] chunkInfo = new EZVIOChunkInfo[chunkNum];

        IntPtr p = backendMesh.chunks;

        for(int i = 0; i < chunkNum; i ++)
        {
            // Marshal.PtrToStructure(p, chunkInfo[i]);
            chunkInfo[i] = (EZVIOChunkInfo)Marshal.PtrToStructure(p, typeof(EZVIOChunkInfo));
            p += Marshal.SizeOf(typeof(EZVIOChunkInfo));
            // chunkInfo[i] = new EZVIOChunkInfo();
            // IntPtr chunkPtr = (IntPtr)((UInt32)backendMesh.chunks + i * Marshal.SizeOf(typeof(EZVIOChunkInfo)));
            // chunkInfo[i] = (EZVIOChunkInfo)Marshal.PtrToStructure(chunkPtr, typeof(EZVIOChunkInfo));
        }

        // Debug.Log("===========UNITY LOG : chunks info : " + chunkInfo[0].id_x + " " + chunkInfo[0].id_y + " " +
        // chunkInfo[0].id_z + " " + chunkInfo[0].nv + " " + chunkInfo[0].sv + " " + chunkInfo[0].ni + " " +
        // chunkInfo[0].si + " " + chunkInfo[0].a );

        float[] vertexes = new float[backendMesh.mesh.vertexCount * 3];
        int[] ts = new int[backendMesh.mesh.facesCount * 3];

        Marshal.Copy(backendMesh.mesh.vertex, vertexes, 0, backendMesh.mesh.vertexCount * 3);
        Marshal.Copy(backendMesh.mesh.faces, ts, 0, backendMesh.mesh.facesCount * 3);

        //         for(int i = 0; i < 5; i ++){
        //     Debug.Log("===========UNITY LOG : incre mesh info : " + vertexes[i*3] + ", " 
        // + vertexes[i*3+1] + ", " + vertexes[i*3+2] );
        // }

        if (mutex.WaitOne())
        {
            incre_meshState = IncrementalMeshState.GETTING;

            // m_Meshes.Clear();
            m_Meshes_vs.Clear();
            m_Meshes_ts.Clear();
            beUpdated.Clear();


            for (int i = 0; i < chunkNum; i ++)
            {

                EZVIOChunkInfo tmp = chunkInfo[i];
                Vector3Int chunkPos = new Vector3Int(tmp.id_x, tmp.id_y, tmp.id_z);
                if(chunkStatus.ContainsKey(chunkPos))
                {
                    if(chunkStatus[chunkPos] >= tmp.a)
                    {
                        continue;
                    }
                    else
                    {
                        //update ori value
                        chunkStatus[chunkPos] = tmp.a;                
                    }
                }else{
                    //add new chunk
                    chunkStatus.Add(chunkPos, tmp.a);
                    // Debug.Log("========add new chunk");
                }

                //MeshFilter meshFilter = new MeshFilter();
                // Mesh mesh_tmp = new Mesh();

                // meshFilter.sharedMesh = mesh_tmp;

                // meshFilter.sharedMesh.vertices = new Vector3[tmp.nv];

                // meshFilter.sharedMesh.triangles = new int[tmp.ni];

                Vector3[] vs_tmp = new Vector3[tmp.nv]; 
                int[] ts_tmp = new int[tmp.ni];
                for (int j = 0; j < tmp.nv; j++)
                {
                    int oriIdx = (tmp.sv + j) * 3;
                    // Debug.Log("========vertex pose");
                    // meshFilter.sharedMesh.vertices[j] = new Vector3(vertexes[oriIdx], vertexes[oriIdx + 2], vertexes[oriIdx + 1]);
                    vs_tmp[j] = new Vector3(vertexes[oriIdx], vertexes[oriIdx + 2], vertexes[oriIdx + 1]);  // yz互换
                    // Debug.Log("========vertex pose ： " + vs_tmp[j]);
                }
                for (int j = 0; j < tmp.ni/3; j++)
                {
                    // meshFilter.sharedMesh.triangles[j*3] = ts[tmp.si + j*3];
                    // meshFilter.sharedMesh.triangles[j*3 + 1] = ts[tmp.si + j*3+2];
                    // meshFilter.sharedMesh.triangles[j*3 + 2] = ts[tmp.si + j*3+1];
                    ts_tmp[j*3] = ts[tmp.si + j*3];
                    ts_tmp[j*3 + 1] = ts[tmp.si + j*3+2];
                    ts_tmp[j*3 + 2] = ts[tmp.si + j*3+1];
                    // Debug.Log("========vertex index ： " + ts_tmp[j*3] + " " +  ts_tmp[j*3+1] + " " + ts_tmp[j*3+2]);
                }
                // m_Meshes.Add(meshFilter);
                m_Meshes_vs.Add(vs_tmp);
                m_Meshes_ts.Add(ts_tmp);
                beUpdated.Add(chunkPos);
            }

            isMeshGetDone = true;
            incre_meshState = IncrementalMeshState.GETTED;
            mutex.ReleaseMutex();
        }
        // Debug.Log("===========Get Mesh Once Time");
    }

    private void GetBackEndMesh()
    {
        // int collect_num = 0;
        // while (true)
        // {
            if (!meshRoot.activeSelf)
            {
                // continue;
                return;
            }
            // if (isGettingMesh)
            // {
                // collect_num++;
                // Debug.Log("==========UNITY LOG : Mesh Get incre start");
                isMeshGet = NativeTracking.GetBackendIncrementalMeshData(ref incrementalMesh);

                // Debug.Log("==========UNITY LOG : Mesh Get " + isMeshGet);

                if (isMeshGet)
                {
                    GetMeshData(incrementalMesh);
                }
                
                return;
            // }
            // else
            // {
            //     meshButton.enabled = true;
            //     meshButton.interactable = true;
            //     stopMeshButton.enabled = false;
            //     stopMeshButton.interactable = false;

            //     //isMeshGet = NativeTracking.GetBackendSmoothedMesh(ref backendMesh);
            //     // isMeshGet = NativeTracking.GetBackendIncrementalMeshData(ref incrementalMesh);

            //     // if (isMeshGet)
            //     // {
            //     //     GetMeshData(incrementalMesh);
            //     // }
            //     NativeTracking.RunPauseMeshAndDFS();
            //     // meshState = MeshState.STOPED;
            //     // Thread.CurrentThread.Abort();
            //     return;
            // }
            //@buqing 优化获取数据的频率，不影响实时渲染的情况下，最优化速度
            // Thread.CurrentThread.Join(100);
        // }
    }

    public void HandleMeshButtonClick()
    {
        // Debug.Log("==========UNITY LOG : Mesh Button Clicked");
        //if (!CheckInitPlane()) return;
        //if (isGettingMesh) return;
        meshButton.enabled = false;
        meshButton.interactable = false;
        stopMeshButton.enabled = true;
        stopMeshButton.interactable = true;

        // isGettingMesh = true;
        incre_meshState = IncrementalMeshState.INIT;

        //NativeTracking.RunResumeMeshAndDFS();

        // meshThread = new Thread(GetBackEndMesh);
        // meshThread.Start();
    }

    public void HandleStopMeshGetClick()
    {
        // isGettingMesh = false;
        meshButton.enabled = true;
        meshButton.interactable = true;
        stopMeshButton.enabled = false;
        stopMeshButton.interactable = false;
        //NativeTracking.RunPauseMeshAndDFS();
        incre_meshState = IncrementalMeshState.STOPED;
    }

    public void HandleMeshOCClick()
    {
        if(isShowMesh){
            isShowMesh = false;
            meshOC.transform.Find("Text").GetComponent<Text>().text = "Show Mesh";
            foreach (var item in meshGameObjects)
            {
                MeshRenderer mr = item.Value.GetComponent<MeshRenderer>();
                mr.material.SetInt("_alpha", 0);
            }
        }else{
            isShowMesh = true;
            meshOC.transform.Find("Text").GetComponent<Text>().text = "Hide Mesh";
            foreach (var item in meshGameObjects)
            {
                MeshRenderer mr = item.Value.GetComponent<MeshRenderer>();
                mr.material.SetInt("_alpha", 1);
            }
        }
    }

    public void HandleSaveMeshClick()
    {
        beSaveMesh = true;
    }

    public void HandleDisableMeshClick()
    {
        if (meshRoot.activeSelf)
        {
            meshRoot.SetActive(false);
            disableMesh.transform.Find("Text").GetComponent<Text>().text = "Enable Mesh";
        }
        else
        {
            meshRoot.SetActive(true);
            disableMesh.transform.Find("Text").GetComponent<Text>().text = "Disable Mesh";
        }

    }

    public static void Collect(ref List<NavMeshBuildSource> sources)
    {
        sources.Clear();

        for (var i = 0; i < nav_Meshes.Count; ++i)
        {
            var mf = nav_Meshes[i];
            if (mf == null) continue;

            var m = mf.sharedMesh;
            if (m == null) continue;

            var s = new NavMeshBuildSource();
            s.shape = NavMeshBuildSourceShape.Mesh;
            s.sourceObject = m;
            s.transform = mf.transform.localToWorldMatrix;
            s.area = 0;
            sources.Add(s);
        }
    }
}
