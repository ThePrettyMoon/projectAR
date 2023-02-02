using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Wheels.Unity
{
    public class NetUtil_Server : NetUtil
    {
        NetType netType;
        event Action<byte[]> giveDataBack;
        /// <summary>
        /// 有新连接进入
        /// </summary>
        event Action<ConnectionInfo> newConnectionIn;

        TcpListener tcpServer;
        /// <summary>
        /// 已连接进来的所有TCP客户端
        /// </summary>
        public Dictionary<string, TcpClient> tcpClients = new Dictionary<string, TcpClient>();
        public Dictionary<string, UdpClient> udpClients = new Dictionary<string, UdpClient>();
        Thread thread_Receive_UDP;
        Thread thread_Receive_TCP;
        Thread thread_Accept_TCP;
        /// <summary>
        /// 当server关闭的时候用这个变量来控制全局的停止和关闭工作
        /// </summary>
        bool stop;
        AutoResetEvent autoEvent_UDP = new AutoResetEvent(false);
        AutoResetEvent autoEvent_TCP = new AutoResetEvent(false);

        /// <summary>
        /// 是否是线程安全的
        /// </summary>
        bool threadSafe;
        /// <summary>
        /// 用于线程安全储存的数据
        /// </summary>
        public byte[] receiveBytes;
        /// <summary>
        /// 有新数据进来
        /// </summary>
        public bool newDataIn;

        #region Singleton & Construction
        private static NetUtil_Server _instance = null;
        private static readonly object SynObject = new object();

        public static NetUtil_Server instance
        {
            get
            {
                lock (SynObject)
                {
                    if (_instance == null)
                    {
                        _instance = new NetUtil_Server();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        /// <summary>
        /// 开启服务端
        /// </summary>
        /// <param name="port">服务端要监听的端口</param>
        /// <param name="netType">服务端支持接入的网络类型</param>
        /// <param name="giveDataBack">服务端收到的数据回调给外部</param>
        /// <param name="threadSafe">是否以线程安全的方式返回数据（如果为true，则giveDataBack将从Unity主线程调用）</param>
        /// <param name="newConnectionIn">TCP网络类型的服务端专用，有新连接进入的时候回调给外部</param>
        /// <returns></returns>
        public bool StartServer(int port, NetType netType, Action<byte[]> giveDataBack, bool threadSafe = false, Action<ConnectionInfo> newConnectionIn = null)
        {
            this.threadSafe = threadSafe;
            new GameObject("Server_" + port + "_" + netType.ToString()).AddComponent<NetUtil_Server_Handler>().Init(this, threadSafe);

            this.netType = netType;
            if (giveDataBack == null)
            {
                return false;
            }
            else
            {
                this.giveDataBack += giveDataBack;
            }

            if (newConnectionIn != null)
            {
                this.newConnectionIn += newConnectionIn;
            }

            if (netType == NetType.UDP)
            {
                StartUDPReceiving(port);
            }
            else if (netType == NetType.TCP)
            {
                StartTCPAccept(port);
            }

            return true;
        }

        public void StopServer()
        {
            stop = true;
            autoEvent_UDP.Set();
            autoEvent_TCP.Set();
        }

        /// <summary>
        /// 向特定客户端发送string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public bool Send(string data, string clientName)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(data);
            return Send(sendBytes, clientName);
        }

        /// <summary>
        /// 向特定客户端发送byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        public bool Send(byte[] data, string clientName)
        {
            if (netType == NetType.TCP)
            {
                try
                {
                    if (tcpClients.ContainsKey(clientName))
                    {
                        if (tcpClients[clientName] != null)
                        {
                            tcpClients[clientName].GetStream().Write(data, 0, data.Length);
                            return true;
                        }
                        else
                        {
                            Debug.Log("[NetUtil_Server -> Send] " + clientName + " 的TCP连接为空！");
                        }
                    }
                    else
                    {
                        Debug.Log("[NetUtil_Server -> Send] " + clientName + " 暂未通过TCP接入！");
                    }
                }
                catch (Exception e)
                {
                    if (tcpClients[clientName] != null)
                    {
                        tcpClients[clientName].Close();
                    }
                    tcpClients.Remove(clientName);
                    Debug.Log("[NetUtil_Server -> Send] TCP发送失败：" + e.ToString());
                }
                return false;
            }
            else
            {
                if (udpClients.ContainsKey(clientName))
                {
                    try
                    {
                        if (udpClients[clientName] != null)
                        {
                            udpClients[clientName].Send(data, data.Length);
                            return true;
                        }
                        else
                        {
                            Debug.Log("[NetUtil_Server -> Send] " + clientName + " 的UDP连接为空！");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log("[NetUtil_Server -> Send] UDP发送失败：" + e.ToString());
                    }
                }
                else
                {
                    Debug.Log("[NetUtil_Server -> Send] " + clientName + " 暂未通过UDP接入");
                }
                return false;
            }
        }

        /// <summary>
        /// 发送给所有已连接客户端
        /// </summary>
        /// <param name="data"></param>
        public void SendToAll(string data)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(data);
            SendToAll(sendBytes);
        }

        /// <summary>
        /// 发送给所有已连接客户端
        /// </summary>
        /// <param name="data"></param>
        public void SendToAll(byte[] data)
        {
            if (tcpClients.Count > 0)
            {
                foreach (KeyValuePair<string, TcpClient> tcpClient in tcpClients)
                {
                    Send(data, tcpClient.Key);
                }
            }
            if (udpClients.Count > 0)
            {
                foreach (KeyValuePair<string, UdpClient> udpClient in udpClients)
                {
                    Send(data, udpClient.Key);
                }
            }
        }

        #region TCP
        void StartTCPAccept(int port)
        {
            thread_Receive_TCP = new Thread(new ParameterizedThreadStart(StartTCPAcceptAsync));
            thread_Receive_TCP.Start(port);
        }

        void StartTCPAcceptAsync(object port)
        {
            // Set the TcpListener on port 13000.
            IPAddress localAddr = IPAddress.Any;// IPAddress.Parse("127.0.0.1");
                                                // TcpListener server = new TcpListener(port);
            tcpServer = new TcpListener(localAddr, (int)port);
            // Start listening for client requests.
            tcpServer.Start();

            Debug.Log("[NetUtil_Server -> StartTCPAcceptAsync] Start - StartTCPAcceptAsync: " + localAddr.ToString() + ":" + port);
            stop = false;

            while (!stop)
            {
                tcpServer.BeginAcceptTcpClient(new AsyncCallback(ServerAcceptCallback), tcpServer);
                autoEvent_TCP.WaitOne();
            }

            //上面stop为true的时候就说明server正在关闭，现在开始停止所有新客户端的接入，并断开所有已连接的客户端
            foreach (KeyValuePair<string, TcpClient> client in tcpClients)
            {
                client.Value.Close();
            }
            tcpServer.Stop();
            Debug.Log("[NetUtil_Server -> StartTCPAcceptAsync] Exit - StartTCPAcceptAsync");
        }

        void ServerAcceptCallback(IAsyncResult ar)
        {
            TcpClient client = tcpServer.EndAcceptTcpClient(ar);

            IPEndPoint endPoint = client.Client.RemoteEndPoint as IPEndPoint;
            string ip = endPoint.Address.ToString();
            if (!tcpClients.ContainsKey(ip + endPoint.Port))
            {
                tcpClients.Add(ip + endPoint.Port, client);
                Debug.Log("新客户端接入：" + (client.Client.RemoteEndPoint as IPEndPoint).Address.ToString());
            }

            //设定向这个接入的客户端发送数据的超时时间为1秒
            client.SendTimeout = 1000;

            StartTCPReceiving(client);
            autoEvent_TCP.Set();
            if (newConnectionIn != null)
            {
                newConnectionIn(new ConnectionInfo((client.Client.RemoteEndPoint as IPEndPoint).Address.ToString(), (client.Client.RemoteEndPoint as IPEndPoint).Port, NetType.TCP));
            }
        }

        /// <summary>
        /// 开始接收客户端发来的数据
        /// </summary>
        /// <param name="tcpClient"></param>
        void StartTCPReceiving(TcpClient tcpClient)
        {
            Debug.Log("[NetUtil_Server -> StartTCPReceiving] StartTCPReceiving: " + tcpClient.Client.LocalEndPoint.ToString());
            stop = false;
            thread_Receive_TCP = new Thread(new ParameterizedThreadStart(TCPReceiveAsync));
            thread_Receive_TCP.Start(tcpClient);
        }

        void TCPReceiveAsync(object tcpClient)
        {
            TcpClient t = tcpClient as TcpClient;
            Debug.Log("[NetUtil_Server -> TCPReceiveAsync] Start - TCPReceiveAsync: " + t.Client.LocalEndPoint.ToString());

            while (!stop)
            {
                // Get a stream object for reading and writing
                NetworkStream stream = t.GetStream();
                List<byte> allData = new List<byte>();
                byte[] buffer = new byte[4096];
                // Loop to receive all the data sent by the client.
                while (stream.CanRead && stream.DataAvailable)
                {
                    int length = stream.Read(buffer, 0, buffer.Length);
                    byte[] buffer0 = new byte[length];
                    Buffer.BlockCopy(buffer, 0, buffer0, 0, length);
                    allData.AddRange(buffer0);
                }

                Debug.Log("[NetUtil_Server -> TCPReceiveAsync] 收到了新数据: " + receiveBytes.Length + "，远端：" + (t.Client.RemoteEndPoint as IPEndPoint).ToString());

                if (threadSafe)
                {
                    receiveBytes = allData.ToArray();
                    newDataIn = true;
                }
                else
                {
                    if (allData.Count > 0)
                    {
                        GiveDataBack(allData.ToArray());
                    }
                }
                Thread.Sleep(100);
            }
            Debug.Log("[NetUtil_Server -> TCPReceiveAsync] Exit - TCPReceiveAsync " + t.Client.RemoteEndPoint);
        }
        #endregion

        #region UDP
        void StartUDPReceiving(int port)
        {
            Debug.Log("[NetUtil_Server -> StartUDPReceiving] StartUDPReceiving");
            stop = false;
            thread_Receive_UDP = new Thread(new ParameterizedThreadStart(UDPReceiveAsync));
            thread_Receive_UDP.Start(port);
        }

        void UDPReceiveAsync(object port)
        {
            Debug.Log("[NetUtil_Server -> UDPReceiveAsync] Start - ReceiveAsync");
            // Receive a message and write it to the console.
            IPEndPoint e = new IPEndPoint(IPAddress.Any, (int)port);
            UdpClient u = new UdpClient(e);
            UdpState s = new UdpState();
            s.e = e;
            s.u = u;

            while (!stop)
            {
                u.BeginReceive(new AsyncCallback(UDPReceiveCallback), s);
                autoEvent_UDP.WaitOne();
            }
            Debug.Log("[NetUtil_Server -> UDPReceiveAsync] Exit - ReceiveAsync");
        }

        void UDPReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            receiveBytes = u.EndReceive(ar, ref e);
            newDataIn = true;

            string ip = e.Address.ToString();
            if (!udpClients.ContainsKey(ip + e.Port))
            {
                Debug.Log("UDPReceiveCallback：" + ip + "：" + e.Port);
                udpClients.Add(ip + e.Port, new UdpClient(ip, e.Port));
            }

            Debug.Log("[NetUtil_Server -> UDPReceiveCallback] 收到了新数据: " + receiveBytes.Length + "，远端：" + e.ToString());

            if (!threadSafe)
            {
                GiveDataBack(receiveBytes);
            }
            autoEvent_UDP.Set();
        }
        #endregion

        public void GiveDataBack(byte[] data)
        {
            if (giveDataBack != null)
            {
                giveDataBack(data);
            }
        }
    }

    /// <summary>
    /// 用于退出程序的时候关闭后台网络线程
    /// </summary>
    public class NetUtil_Server_Handler : MonoBehaviour
    {
        NetUtil_Server server;
        bool threadSafe;

        public void Init(NetUtil_Server server, bool threadSafe)
        {
            this.server = server;
            this.threadSafe = threadSafe;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (server.newDataIn && threadSafe)
            {
                server.newDataIn = false;
                server.GiveDataBack(server.receiveBytes);
            }
        }

        private void OnApplicationQuit()
        {
            server.StopServer();
        }
    }
}
