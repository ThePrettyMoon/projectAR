using Wheels.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text;

public class SyncGameObjects : MonoBehaviour
{
    NetUtil_Server serverUDP = new NetUtil_Server();
    NetUtil_Client clientUDP = new NetUtil_Client();

    /// <summary>
    /// 专门用于同步控制指令
    /// </summary>
    NetUtil_Server serverTCP = new NetUtil_Server();
    NetUtil_Client clientTCP = new NetUtil_Client();

    /// <summary>
    /// Editor端IP
    /// </summary>
    public string editorIP;

    bool sendToEditor = true;

    public Transform[] transforms;
    int perTransformBytesCount;
    byte[] syncData;

    // Start is called before the first frame update
    void Start()
    {
        //得到Vector3和Quaternion数据结构所占字节数
        perTransformBytesCount = Marshal.SizeOf(typeof(Vector3)) + Marshal.SizeOf(typeof(Quaternion));
        //要同步的transform的数量*每个transform所占的字节数
        syncData = new byte[transforms.Length * perTransformBytesCount];

        if (Application.isEditor)
        {
            serverUDP.StartServer(6611, NetUtil.NetType.UDP, GetDataUDP, true);
            serverTCP.StartServer(6612, NetUtil.NetType.TCP, GetDataTCP, true);
        }
        else
        {
            clientUDP.Connect(editorIP, 6611, NetUtil.NetType.UDP, GetDataUDP);
            clientTCP.Connect(editorIP, 6612, NetUtil.NetType.TCP, GetDataTCP);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sendToEditor = true;
            serverTCP.SendToAll("SendToEditor");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sendToEditor = false;
            serverTCP.SendToAll("SendToClient");
        }

        if (Application.isEditor)
        {
            if (!sendToEditor)
            {
                GetSyncData();
                serverUDP.SendToAll(syncData);
            }
        }
        else
        {
            if (sendToEditor)
            {
                GetSyncData();
                clientUDP.Send(syncData);
            }
        }
    }

    private void OnDisable()
    {
        if (Application.isEditor)
        {
            serverUDP.StopServer();
            serverTCP.StopServer();
        }
        else
        {
            clientUDP.DisConnect();
            clientTCP.DisConnect();
        }
    }

    void GetSyncData()
    {
        //得到要同步的所有的Transform的数据
        int curTransformCount = 0;
        foreach (Transform item in transforms)
        {
            byte[] forFloat = BitConverter.GetBytes(item.localPosition.x);
            syncData[curTransformCount * perTransformBytesCount + 0] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 1] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 2] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 3] = forFloat[3];

            forFloat = BitConverter.GetBytes(item.localPosition.y);
            syncData[curTransformCount * perTransformBytesCount + 4] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 5] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 6] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 7] = forFloat[3];

            forFloat = BitConverter.GetBytes(item.localPosition.z);
            syncData[curTransformCount * perTransformBytesCount + 8] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 9] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 10] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 11] = forFloat[3];

            forFloat = BitConverter.GetBytes(item.localRotation.x);
            syncData[curTransformCount * perTransformBytesCount + 12] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 13] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 14] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 15] = forFloat[3];

            forFloat = BitConverter.GetBytes(item.localRotation.y);
            syncData[curTransformCount * perTransformBytesCount + 16] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 17] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 18] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 19] = forFloat[3];

            forFloat = BitConverter.GetBytes(item.localRotation.z);
            syncData[curTransformCount * perTransformBytesCount + 20] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 21] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 22] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 23] = forFloat[3];

            forFloat = BitConverter.GetBytes(item.localRotation.w);
            syncData[curTransformCount * perTransformBytesCount + 24] = forFloat[0];
            syncData[curTransformCount * perTransformBytesCount + 25] = forFloat[1];
            syncData[curTransformCount * perTransformBytesCount + 26] = forFloat[2];
            syncData[curTransformCount * perTransformBytesCount + 27] = forFloat[3];

            curTransformCount++;
        }
    }

    public void GetDataUDP(byte[] data)
    {
        //Debug.Log(syncData.Length);

        //得到要同步的所有的Transform的数据
        int curTransformCount = 0;
        foreach (Transform item in transforms)
        {
            int curIndex = curTransformCount * perTransformBytesCount;
            float x = BitConverter.ToSingle(data, curIndex + 0);
            item.localPosition = new Vector3(BitConverter.ToSingle(data, curIndex + 0), BitConverter.ToSingle(data, curIndex + 4), BitConverter.ToSingle(data, curIndex + 8));
            item.localRotation = new Quaternion(BitConverter.ToSingle(data, curIndex + 12), BitConverter.ToSingle(data, curIndex + 16), BitConverter.ToSingle(data, curIndex + 20), BitConverter.ToSingle(data, curIndex + 24));

            curTransformCount++;
        }
    }

    public void GetDataTCP(byte[] data)
    {
        string command = Encoding.UTF8.GetString(data);
        switch (command)
        {
            case "SendToEditor":
                sendToEditor = true;
                break;
            case "SendToClient":
                sendToEditor = false;
                break;
        }
    }
}
