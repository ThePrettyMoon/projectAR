using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using EZXR.Glass.SixDof;
using Wheels.Unity;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SpatialTrackingMultiPlayer : MonoBehaviour
{
    #region 单例
    private static SpatialTrackingMultiPlayer instance;
    public static SpatialTrackingMultiPlayer Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    [Serializable]
    public class RoomInfoForClient
    {
        /// <summary>
        /// 房间对应用到的算法地图ID
        /// </summary>
        public string mapID;
        /// <summary>
        /// 房间的Server的IP
        /// </summary>
        public string serverIP;
        /// <summary>
        /// 当前客户端的IP
        /// </summary>
        public string curClientIP;
        /// <summary>
        /// 当前客户端在Room中的ID
        /// </summary>
        public string curClientID;

        public RoomInfoForClient(string mapID, string serverIP, string curClientIP, string curClientID)
        {
            this.mapID = mapID;
            this.serverIP = serverIP;
            this.curClientIP = curClientIP;
            this.curClientID = curClientID;
        }
    }

    [Serializable]
    public class InteractionData_UDP
    {
        [Serializable]
        public class Point3
        {
            public float x;
            public float y;
            public float z;
            public Point3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public Vector3 ToVector3()
            {
                return new Vector3(x, y, z);
            }
        }
        [Serializable]
        public class Point4
        {
            public float x;
            public float y;
            public float z;
            public float w;
            public Point4(float x, float y, float z, float w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }
        }
        [Serializable]
        public class GameObjectInfo
        {
            /// <summary>
            /// 物体在场景中的唯一ID，是instanceID和在物体objectInfoToSync中的index加在一起的值
            /// </summary>
            public int uuid;
            /// <summary>
            /// 在prefabs4Interaction中的ID，用于实例化
            /// </summary>
            public int prefabID;
            public Point3 pos;
            public Point4 rot;
            public Point3 scale;
            public GameObjectInfo(int uuid, int prefabID, Point3 pos, Point4 rot, Point3 scale)
            {
                this.uuid = uuid;
                this.prefabID = prefabID;
                this.pos = pos;
                this.rot = rot;
                this.scale = scale;
            }
        }
        /// <summary>
        /// 数据来自哪个主机
        /// </summary>
        public int clientID;
        public List<GameObjectInfo> poses;

        public InteractionData_UDP(int id)
        {
            this.clientID = id;
            poses = new List<GameObjectInfo>();
        }

        public void Add(int uuid, int prefabID, Vector3 pos, Quaternion rot, Vector3 scale)
        {
            Point3 _pos = new Point3(pos.x, pos.y, pos.z);
            Point4 _rot = new Point4(rot.x, rot.y, rot.z, rot.w);
            Point3 _scale = new Point3(scale.x, scale.y, scale.z);
            poses.Add(new GameObjectInfo(uuid, prefabID, _pos, _rot, _scale));
        }

        public Pose GetPose(GameObjectInfo pose)
        {
            return new Pose(new Vector3(pose.pos.x, pose.pos.y, pose.pos.z), new Quaternion(pose.rot.x, pose.rot.y, pose.rot.z, pose.rot.w));
        }
    }

    public class ClientInfo
    {
        public Dictionary<int, Transform> transforms = new Dictionary<int, Transform>();
        public Dictionary<int, long> transformsActiveStatus = new Dictionary<int, long>();
    }

    public enum LocType
    {
        Anchor,
        Marker,
    }
    public LocType locType;

    /// <summary>
    /// 要同步的物体信息
    /// </summary>
    [Serializable]
    public class ObjectInfoToSync
    {
        /// <summary>
        /// 要同步到网络中的物体的Transform
        /// </summary>
        public Transform objectToSync;
        /// <summary>
        /// 此物体用到的prefab在prefabs4Interaction数组中的索引号，用于在网络中其他客户端创建此物体
        /// </summary>
        public int prefabID;
        /// <summary>
        /// 只同步Position
        /// </summary>
        public bool onlySyncPosition;
        public ObjectInfoToSync(Transform objectToSync, int prefabID, bool onlySyncPosition)
        {
            this.objectToSync = objectToSync;
            this.prefabID = prefabID;
            this.onlySyncPosition = onlySyncPosition;
        }
    }

    IntPtr intPtr;
    EZVIORelativePose relativePose = default(EZVIORelativePose);

    public InputField inputField;
    public InputField inputField_IP;

    NetUtil_Server udpServer = new NetUtil_Server();
    NetUtil_Server tcpServer = new NetUtil_Server();
    NetUtil_Client udpClient = new NetUtil_Client();
    NetUtil_Client tcpClient = new NetUtil_Client();
    bool isServer = false;
    /// <summary>
    /// 当前收到的数据
    /// </summary>
    string curData;
    /// <summary>
    /// 连接房间所使用的IP和Port（如果是局域网连接服务器返回的就是局域网IP，如果是外网连接服务器返回的就是外网IP）
    /// </summary>
    string myIPPort;
    /// <summary>
    /// 在房间中的ID
    /// </summary>
    int clientID;
    /// <summary>
    /// 要同步到网络中的物体
    /// </summary>
    public List<ObjectInfoToSync> objectInfoToSync = new List<ObjectInfoToSync>();
    /// <summary>
    /// 用于网络同步实例化
    /// </summary>
    public List<GameObject> prefabs4Interaction = new List<GameObject>();
    Dictionary<int, ClientInfo> clientInfos = new Dictionary<int, ClientInfo>();
    /// <summary>
    /// 用于将unity坐标系下的pose转到VIO的Map坐标系下
    /// </summary>
    Matrix4x4 matrixUnity2Map = new Matrix4x4();
    /// <summary>
    /// 地图原点
    /// </summary>
    public Transform mapOri;
    public Toggle toggle_MapStatus;
    /// <summary>
    /// 当前收到的网络数据的总帧数
    /// </summary>
    public long netDataFrame;


    bool startTrackingMarker;
    public Text startTrackingMarkerText;
    public Transform marker;
    EZVIOResult EZVIOResult = new EZVIOResult();
    Pose pose = new Pose();


    private void Awake()
    {
        instance = this;
        //一次性开辟一个大一些的区域用于处理网络数据
        intPtr = Marshal.AllocHGlobal(8192);
    }

    private void Start()
    {
        ARAnchorManager.Instance.OnMapIsReady += OnMapIsReady;

        WWWForm form = new WWWForm();
        form.AddField("uid", Application.identifier);
        UnityWebRequest.Instance.Create("http://10.244.12.15/JoinRoom", form, GiveBackString, "JoinRoom");

        if (locType == LocType.Marker)
        {
            ChangeTrackingMarkerStatus();
            marker.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        inputField_IP.text = curData;

        if (locType == LocType.Anchor)
        {
            toggle_MapStatus.isOn = relativePose.is_reloc_ok;
            if (ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
            {
                GetLoc();
            }
        }
        else
        {
            if (startTrackingMarker)
            {
                NativeTracking.GetArucoResult(ref EZVIOResult);
                if (EZVIOResult.vioState == EZVIOState.EZVIOCameraState_Tracking)
                {
                    GetLocByMarker();
                }
            }
        }

        if (ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
        {
            SyncToNet();
        }
    }

    public void ChangeTrackingMarkerStatus()
    {
        startTrackingMarker = !startTrackingMarker;
        startTrackingMarkerText.text = "ChangeTrackMarker:" + startTrackingMarker;
    }

    public void GiveBackString(string data, string identifier, long statusCode)
    {
        Debug.Log("GiveBackString: " + data);
        RoomInfoForClient roomInfoForClient = JsonUtil.Deserialization<RoomInfoForClient>(data);

        Debug.Log("ServerIP is: " + roomInfoForClient.serverIP);
        Debug.Log("ClientIPPort is: " + roomInfoForClient.curClientIP);
        Debug.Log("ClientID is: " + roomInfoForClient.curClientID);

        myIPPort = roomInfoForClient.curClientIP;
        clientID = int.Parse(roomInfoForClient.curClientID);

        if (!Application.isEditor)
        {
            LoadMap(int.Parse(roomInfoForClient.mapID));
        }

        string[] ips = NetworkInfo.GetIPv4s();
        foreach (string ip in ips)
        {
            if (ip == roomInfoForClient.serverIP)
            {
                isServer = true;
                break;
            }
        }

        if (isServer)
        {
            StartServer();
        }
        else
        {
            ConnectServer(roomInfoForClient.serverIP);
        }
    }

    /// <summary>
    /// 地图准备就绪的时候会回调此处
    /// </summary>
    public void OnMapIsReady()
    {
        AddToSync(HMDPoseTracker.Instance.Head, prefabs4Interaction[0]);
        AddToSync(HMDPoseTracker.Instance.Head, prefabs4Interaction[2], true);
    }

    public void StartServer()
    {
        udpServer.StartServer(5135, NetUtil.NetType.UDP, GetData_UDP, true);
        tcpServer.StartServer(5136, NetUtil.NetType.TCP, GetData_TCP, true);
    }

    public void ConnectServer(string ip)
    {
        udpClient.Connect(ip, 5135, NetUtil.NetType.UDP, GetData_UDP, true);
        tcpClient.Connect(ip, 5136, NetUtil.NetType.TCP, GetData_TCP, true);
    }

    public void TestSend()
    {
        udpClient.Send(DateTime.Now.ToString());
    }

    public void ServerSend()
    {
        udpServer.SendToAll(DateTime.Now.ToString());
    }

    public void GetData_UDP(byte[] data)
    {
        //if (isServer)
        //{
        //    udpServer.SendToAll(data);
        //}

        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream mStream = new MemoryStream())
        {
            mStream.Write(data, 0, data.Length);
            mStream.Flush();
            mStream.Seek(0, SeekOrigin.Begin);
            InteractionData_UDP interactionData = (InteractionData_UDP)formatter.Deserialize(mStream);

            if (interactionData.clientID == clientID)
            {
                return;
            }

            if (!clientInfos.ContainsKey(interactionData.clientID))
            {
                ClientInfo clientInfo = new ClientInfo();
                clientInfos.Add(interactionData.clientID, clientInfo);
            }

            for (int i = 0; i < interactionData.poses.Count; i++)
            {
                Matrix4x4 m_Ori = new Matrix4x4();
                Pose pose = interactionData.GetPose(interactionData.poses[i]);
                m_Ori.SetTRS(pose.position, pose.rotation, interactionData.poses[i].scale.ToVector3());
                Matrix4x4 m_Tar = matrixUnity2Map.inverse * m_Ori;

                if (!clientInfos[interactionData.clientID].transforms.ContainsKey(interactionData.poses[i].uuid))
                {
                    Transform obj = Instantiate(prefabs4Interaction[interactionData.poses[i].prefabID], m_Tar.GetColumn(3), m_Tar.rotation).transform;
                    obj.localScale = m_Tar.lossyScale;
                    //if (obj.GetComponent<Rigidbody>() != null)
                    //{
                    //    obj.GetComponent<Rigidbody>().isKinematic = true;
                    //}
                    clientInfos[interactionData.clientID].transforms.Add(interactionData.poses[i].uuid, interactionData.poses[i].prefabID == -1 ? null : obj);
                    clientInfos[interactionData.clientID].transformsActiveStatus.Add(interactionData.poses[i].uuid, netDataFrame);
                }
                if (clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid] != null)
                {
                    clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid].position = m_Tar.GetColumn(3);
                    clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid].rotation = m_Tar.rotation;
                    clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid].localScale = m_Tar.lossyScale;
                    clientInfos[interactionData.clientID].transformsActiveStatus[interactionData.poses[i].uuid] = netDataFrame;
                }
            }

            List<int> idsToDestroy = new List<int>();
            foreach (KeyValuePair<int, long> item in clientInfos[interactionData.clientID].transformsActiveStatus)
            {
                if (item.Value != netDataFrame)
                {
                    idsToDestroy.Add(item.Key);
                }
            }

            foreach (int item in idsToDestroy)
            {
                Destroy(clientInfos[interactionData.clientID].transforms[item].gameObject);
                clientInfos[interactionData.clientID].transforms.Remove(item);
                clientInfos[interactionData.clientID].transformsActiveStatus.Remove(item);
            }

            string result =
                "receive from: " + interactionData.clientID;// +
                                                            //"\nposition:" + headPose.position.ToString("F4") + ", rotation:" + headPose.rotation.ToString("F2") +
                                                            //"\nposition: " + clientInfos[interactionData.clientID].transforms[interactionData.poses[0].instanceID].position.ToString("F4") + ", rotation: " + clientInfos[interactionData.clientID].transforms[interactionData.poses[0].instanceID].rotation.ToString("F2");
            curData = result;
        }

        netDataFrame++;
    }

    public void GetData_TCP(byte[] data)
    {
        //if (isServer)
        //{
        //    tcpServer.SendToAll(data);
        //}

        //BinaryFormatter formatter = new BinaryFormatter();
        //using (MemoryStream mStream = new MemoryStream())
        //{
        //    mStream.Write(data, 0, data.Length);
        //    mStream.Flush();
        //    mStream.Seek(0, SeekOrigin.Begin);
        //    InteractionData_UDP interactionData = (InteractionData_UDP)formatter.Deserialize(mStream);

        //    if (interactionData.clientID == clientID)
        //    {
        //        return;
        //    }

        //    if (!clientInfos.ContainsKey(interactionData.clientID))
        //    {
        //        ClientInfo clientInfo = new ClientInfo();
        //        clientInfos.Add(interactionData.clientID, clientInfo);
        //    }

        //    for (int i = 0; i < interactionData.poses.Count; i++)
        //    {
        //        Matrix4x4 m_Ori = new Matrix4x4();
        //        Pose pose = interactionData.GetPose(interactionData.poses[i]);
        //        m_Ori.SetTRS(pose.position, pose.rotation, Vector3.one);
        //        Matrix4x4 m_Tar = matrixUnity2Map.inverse * m_Ori;

        //        if (!clientInfos[interactionData.clientID].transforms.ContainsKey(interactionData.poses[i].uuid))
        //        {
        //            Transform obj = Instantiate(prefabs4Interaction[interactionData.poses[i].prefabID], m_Tar.GetColumn(3), m_Tar.rotation).transform;
        //            //if (obj.GetComponent<Rigidbody>() != null)
        //            //{
        //            //    obj.GetComponent<Rigidbody>().isKinematic = true;
        //            //}
        //            clientInfos[interactionData.clientID].transforms.Add(interactionData.poses[i].uuid, interactionData.poses[i].prefabID == -1 ? null : obj);
        //            clientInfos[interactionData.clientID].transformsActiveStatus.Add(interactionData.poses[i].uuid, netDataFrame);
        //        }
        //        if (clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid] != null)
        //        {
        //            clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid].position = m_Tar.GetColumn(3);
        //            clientInfos[interactionData.clientID].transforms[interactionData.poses[i].uuid].rotation = m_Tar.rotation;
        //            clientInfos[interactionData.clientID].transformsActiveStatus[interactionData.poses[i].uuid] = netDataFrame;
        //        }
        //    }

        //    //List<int> idsToDestroy = new List<int>();
        //    //foreach (KeyValuePair<int, long> item in clientInfos[interactionData.clientID].transformsActiveStatus)
        //    //{
        //    //    if (item.Value != netDataFrame)
        //    //    {
        //    //        idsToDestroy.Add(item.Key);
        //    //    }
        //    //}

        //    //foreach (int item in idsToDestroy)
        //    //{
        //    //    Destroy(clientInfos[interactionData.clientID].transforms[item].gameObject);
        //    //    clientInfos[interactionData.clientID].transforms.Remove(item);
        //    //    clientInfos[interactionData.clientID].transformsActiveStatus.Remove(item);
        //    //}

        //    string result =
        //        "receive from: " + interactionData.clientID;// +
        //                                                    //"\nposition:" + headPose.position.ToString("F4") + ", rotation:" + headPose.rotation.ToString("F2") +
        //                                                    //"\nposition: " + clientInfos[interactionData.clientID].transforms[interactionData.poses[0].instanceID].position.ToString("F4") + ", rotation: " + clientInfos[interactionData.clientID].transforms[interactionData.poses[0].instanceID].rotation.ToString("F2");
        //    curData = result;
        //}

        //netDataFrame++;
    }

    public void LoadMap(int mapID)
    {
        ARAnchorManager.Instance.LoadMap(mapID/*int.Parse(inputField.text)*/);
    }

    public void GetLoc()
    {
        if (Application.isEditor)
        {
            isServer = true;
        }
        else
        {
            //if (relativePose.is_reloc_ok)
            //{
            //    return;
            //}

            Debug.Log("EZVIORelativePose Size: " + Marshal.SizeOf(relativePose) + ", is_reloc_ok_before" + relativePose.is_reloc_ok);

            NativeTracking.GetVIO2Map(ref relativePose);

            Debug.Log("is_reloc_ok_after: " + relativePose.is_reloc_ok);

            if (!relativePose.is_reloc_ok)
            {
                return;
            }

            string result = "";
            foreach (double item in relativePose.T_vio_to_map)
            {
                result += (item + ",");
            }
            Debug.Log("is_reloc_ok_T_vio_to_map: " + result);

            result = "";
            foreach (double item in relativePose.T_w_m_u3d)
            {
                result += (item + ",");
            }
            Debug.Log("is_reloc_ok_T_w_m_u3d: " + result);

            result = "";
            foreach (double item in relativePose.T_bc)
            {
                result += (item + ",");
            }
            Debug.Log("is_reloc_ok_T_bc: " + result);

            Debug.Log("isServer: " + isServer);

            float[] tar = relativePose.T_w_m_u3d;
            matrixUnity2Map.SetRow(0, new Vector4((float)tar[0], (float)tar[1], (float)tar[2], (float)tar[3]));
            matrixUnity2Map.SetRow(1, new Vector4((float)tar[4], (float)tar[5], (float)tar[6], (float)tar[7]));
            matrixUnity2Map.SetRow(2, new Vector4((float)tar[8], (float)tar[9], (float)tar[10], (float)tar[11]));
            matrixUnity2Map.SetRow(3, new Vector4((float)tar[12], (float)tar[13], (float)tar[14], (float)tar[15]));

            matrixUnity2Map = ARFrame.accumulatedRecenterOffset4x4 * matrixUnity2Map;
            mapOri.position = matrixUnity2Map.GetColumn(3);

            matrixUnity2Map = matrixUnity2Map.inverse;

            Matrix4x4 uw2vio = new Matrix4x4();
            uw2vio.SetRow(0, new Vector4(1, 0, 0, 0));
            uw2vio.SetRow(1, new Vector4(0, 0, 1, 0));
            uw2vio.SetRow(2, new Vector4(0, 1, 0, 0));
            uw2vio.SetRow(3, new Vector4(0, 0, 0, 1));
        }
    }

    public void GetLocByMarker()
    {
        //Matrix4x4 tr = new Matrix4x4();
        //for (int i = 0; i < 16; i++)
        //{
        //    tr[i] = EZVIOResult.camTransform[i];
        //}
        //Matrix4x4 tr_tp = tr.transpose;
        //Matrix4x4 transformUnity_new = ARFrame.accumulatedRecenterOffset4x4 * tr_tp;

        //Vector3 pos = transformUnity_new.GetColumn(3);
        //Quaternion rot = transformUnity_new.rotation;
        //marker.position = pos;
        //marker.rotation = rot;

        float[] tar = EZVIOResult.camTransform;
        matrixUnity2Map.SetRow(0, new Vector4((float)tar[0], (float)tar[1], (float)tar[2], (float)tar[3]));
        matrixUnity2Map.SetRow(1, new Vector4((float)tar[4], (float)tar[5], (float)tar[6], (float)tar[7]));
        matrixUnity2Map.SetRow(2, new Vector4((float)tar[8], (float)tar[9], (float)tar[10], (float)tar[11]));
        matrixUnity2Map.SetRow(3, new Vector4((float)tar[12], (float)tar[13], (float)tar[14], (float)tar[15]));
        matrixUnity2Map = ARFrame.accumulatedRecenterOffset4x4 * matrixUnity2Map;
        marker.position = matrixUnity2Map.GetColumn(3);
        marker.rotation = matrixUnity2Map.rotation;

        matrixUnity2Map = matrixUnity2Map.inverse;
    }

    /// <summary>
    /// 添加要同步到网络中的物体信息
    /// </summary>
    /// <param name="objToSync">要同步到网络中的物体（可以重复）</param>
    /// <param name="prefabObjUse">要同步到网络中的物体用到的prefab</param>
    public void AddToSync(Transform objToSync, GameObject prefabObjUse, bool onlySyncPosition = false)
    {
        if (prefabObjUse != null)
        {
            if (!prefabs4Interaction.Contains(prefabObjUse))
            {
                prefabs4Interaction.Add(prefabObjUse);
            }
        }
        objectInfoToSync.Add(new ObjectInfoToSync(objToSync, prefabObjUse == null ? -1 : prefabs4Interaction.IndexOf(prefabObjUse), onlySyncPosition));
    }

    public void SyncToNet()
    {
        if (objectInfoToSync.Count > 0)
        {
            InteractionData_UDP interactionData = new InteractionData_UDP(clientID);

            Matrix4x4 m_Tar = Matrix4x4.identity;

            for (int i = 0; i < objectInfoToSync.Count; i++)
            {
                if (objectInfoToSync[i].objectToSync == null)
                {
                    objectInfoToSync.RemoveAt(i);
                    i--;
                    continue;
                }

                Matrix4x4 m_Ori = new Matrix4x4();
                m_Ori.SetTRS(objectInfoToSync[i].objectToSync.position, objectInfoToSync[i].onlySyncPosition ? Quaternion.identity : objectInfoToSync[i].objectToSync.rotation, objectInfoToSync[i].objectToSync.lossyScale);
                m_Tar = matrixUnity2Map * m_Ori;

                interactionData.Add(objectInfoToSync[i].objectToSync.GetInstanceID() + i, objectInfoToSync[i].prefabID, m_Tar.GetColumn(3), m_Tar.rotation, m_Tar.lossyScale);
                Debug.Log("InteractionData: " + m_Tar.GetColumn(3).x + ", " + m_Tar.GetColumn(3).y + ", " + m_Tar.GetColumn(3).z + ", Quaternion: " + m_Tar.rotation.x + ", " + m_Tar.rotation.y + ", " + m_Tar.rotation.z + ", " + m_Tar.rotation.w + ", ");
            }

            BinaryFormatter formatter = new BinaryFormatter();
            byte[] dataToSend;
            using (MemoryStream mStream = new MemoryStream())
            {
                formatter.Serialize(mStream, interactionData);
                mStream.Flush();
                dataToSend = mStream.GetBuffer();
            }

            Debug.Log("dataToSend: " + dataToSend.Length);

            if (isServer)
            {
                udpServer.SendToAll(dataToSend);
            }
            else
            {
                udpClient.Send(dataToSend);
            }
        }
    }

}
