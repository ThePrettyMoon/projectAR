using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Wheels.Unity
{
    public class NetUtil_Test : MonoBehaviour
    {
        public string ip;
        public int port;
        public string data;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnGUI()
        {
            if (GUILayout.Button("StopServer"))
            {
                NetUtil_Server.instance.StopServer();
            }

            if (GUILayout.Button("StartServer_TCP"))
            {
                NetUtil_Server.instance.StartServer(port, NetUtil_Server.NetType.TCP, ServerGetData);
            }
            if (GUILayout.Button("Connect_TCP"))
            {
                NetUtil_Client.instance.Connect(ip, port, NetUtil_Client.NetType.TCP, ClientGetData);
            }
            if (GUILayout.Button("ClientSend_TCP"))
            {
                NetUtil_Client.instance.Send("测试发送TCP内容");
            }
            if (GUILayout.Button("ServerSend_TCP"))
            {
                //NetUtil_Server.instance.Send(data, NetUtil_Server.instance.tcpClients[0]);
            }

            if (GUILayout.Button("StartServer_UDP"))
            {
                NetUtil_Server.instance.StartServer(port, NetUtil_Server.NetType.UDP, ServerGetData);
            }
            if (GUILayout.Button("Connect_UDP"))
            {
                NetUtil_Client.instance.Connect(ip, port, NetUtil_Client.NetType.UDP, ClientGetData);
            }
            if (GUILayout.Button("ClientSend_UDP"))
            {
                NetUtil_Client.instance.Send("测试发送UDP内容");
            }
            if (GUILayout.Button("ServerSend_UDP"))
            {
                //NetUtil_Server.instance.Send("回复测试发送UDP内容", NetUtil_Server.instance.udpClients[0]);
            }
        }

        public void ClientGetData(byte[] data)
        {
            string data1 = Encoding.UTF8.GetString(data);
            Debug.Log("ClientGetData得到内容：" + data1);
            switch (data1)
            {
                case "shizhengpeitao":
                    Debug.Log("hehhehheh");
                    break;
            }
        }
        public void ServerGetData(byte[] data)
        {
            Debug.Log("ServerGetData得到内容：" + Encoding.UTF8.GetString(data));
        }

    }
}