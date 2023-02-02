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
    /// <summary>
    /// 一个NetUtil_Client实例只能建立一个连接，除非连接断开，不然不能重复建立连接，如果需要建立多个连接则需要多个NetUtil_Client实例
    /// </summary>
    public class NetUtil_Client : NetUtil
    {
        public class ConnectInfo
        {
            public string ip;
            public int port;

            public ConnectInfo(string ip, int port)
            {
                this.ip = ip;
                this.port = port;
            }
        }
        public enum ConnectState
        {
            /// <summary>
            /// 未连接或已断开
            /// </summary>
            NotConnected,
            /// <summary>
            /// 连接中
            /// </summary>
            Connecting,
            /// <summary>
            /// 已连接
            /// </summary>
            Connected,
        }

        /// <summary>
        /// 数据传输分隔符：[^]
        /// </summary>
        byte[] splitKey = new byte[3] { 91, 94, 93 };
        /// <summary>
        /// 当前连接的信息，包括远端的ip和port
        /// </summary>
        ConnectInfo connectInfo;
        /// <summary>
        /// 当前连接的类型
        /// </summary>
        NetType netType;
        /// <summary>
        /// 失败重连次数（允许失败的次数），失败达到此值将不再尝试重连，默认为1即失败1次后就不再尝试重连，0表示无限次重连直到连接成功。
        /// </summary>
        int repeatTimes;
        /// <summary>
        /// 当前连接次数
        /// </summary>
        int curRepeatTimes;
        /// <summary>
        /// 自动断线重连
        /// </summary>
        bool autoReconnect;
        /// <summary>
        /// 当前的连接状态，有3种状态
        /// </summary>
        public ConnectState connectState;
        /// <summary>
        /// 返回连接成功或失败，第二个参数是失败或成功的附加信息
        /// </summary>
        event Action<bool, string> connectedCallBack;

        public event Action<byte[]> giveDataBack;

        Thread thread_Receive_UDP;
        Thread thread_Receive_TCP;
        bool stop;
        AutoResetEvent autoEvent_UDP = new AutoResetEvent(false);
        AutoResetEvent autoEvent_TCP = new AutoResetEvent(false);
        UdpClient udpClient;
        TcpClient tcpClient;

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
        private static NetUtil_Client _instance = null;
        private static readonly object SynObject = new object();

        public static NetUtil_Client instance
        {
            get
            {
                lock (SynObject)
                {
                    if (_instance == null)
                    {
                        _instance = new NetUtil_Client();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ip">要连接的目标IP</param>
        /// <param name="port">要连接的目标端口</param>
        /// <param name="netType">连接类型</param>
        /// <param name="giveDataBack">远端发来的数据将回调到此</param>
        /// <param name="threadSafe">是否以线程安全的方式返回数据（如果为true，则giveDataBack将从Unity主线程调用）</param>
        /// <param name="connectedCallBack">连接状态将回调到此</param>
        /// <param name="repeatTimes">允许尝试连接的次数。0表示无限次尝试连接直到连接成功，1表示连接失败1次后将不再尝试连接，2表示连接失败后将再尝试连接1次如果再次失败就不再尝试连接，以此类推...默认值为1</param>
        /// <param name="autoReconnect">自动断线重连</param>
        public void Connect(string ip, int port, NetType netType, Action<byte[]> giveDataBack = null, bool threadSafe = false, Action<bool, string> connectedCallBack = null, int repeatTimes = 1, bool autoReconnect = true)
        {
            if (connectState == ConnectState.NotConnected)
            {
                this.threadSafe = threadSafe;
                new GameObject("Client_" + ip + ":" + port + "_" + netType.ToString()).AddComponent<NetUtil_Client_Handler>().Init(this, threadSafe);

                //将当前实例的连接状态置为连接中以避免用户重复Connect
                connectState = ConnectState.Connecting;
                connectInfo = new ConnectInfo(ip, port);
                Debug.Log("[NetUtil_Client -> Connect] 开始连接：" + connectInfo.ip + ":" + connectInfo.port);
                this.netType = netType;
                this.repeatTimes = repeatTimes;
                this.autoReconnect = autoReconnect;

                if (giveDataBack != null)
                {
                    this.giveDataBack += giveDataBack;
                }

                if (connectedCallBack == null)
                {
                    this.connectedCallBack += ConnectedCallBack;
                    this.connectedCallBack += connectedCallBack;
                }
                if (netType == NetType.UDP)
                {
                    thread_Receive_UDP = new Thread(new ParameterizedThreadStart(ConnectUDPAsync));
                    thread_Receive_UDP.Start(connectInfo);
                }
                else if (netType == NetType.TCP)
                {
                    thread_Receive_TCP = new Thread(new ParameterizedThreadStart(ConnectTCPAsync));
                    thread_Receive_TCP.Start(connectInfo);
                }
            }
            else
            {
                Debug.LogWarning("[NetUtil_Client -> Connect] 当前实例已建立连接，请先断开当前连接再建立新的连接，或者使用一个新的NetUtil_Client实例");
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            Debug.Log("[NetUtil_Client -> DisConnect] 网络断开");

            StopClient();
            connectState = ConnectState.NotConnected;
            curRepeatTimes = 0;

            try
            {
                if (udpClient != null)
                {
                    udpClient.Close();
                }
                if (tcpClient != null && tcpClient.Connected)
                {
                    tcpClient.Close();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[NetUtil_Client -> DisConnect] 网络断开异常：" + e.ToString());
            }
        }

        public void ReConnect()
        {
            DisConnect();

            this.repeatTimes = 0;

            //将当前实例的连接状态置为连接中以避免用户重复Connect
            connectState = ConnectState.Connecting;
            Debug.Log("[NetUtil_Client -> ReConnect] 开始重新连接：" + connectInfo.ip + ":" + connectInfo.port);

            if (netType == NetType.UDP)
            {
                thread_Receive_UDP = new Thread(new ParameterizedThreadStart(ConnectUDPAsync));
                thread_Receive_UDP.Start(connectInfo);
            }
            else if (netType == NetType.TCP)
            {
                thread_Receive_TCP = new Thread(new ParameterizedThreadStart(ConnectTCPAsync));
                thread_Receive_TCP.Start(connectInfo);
            }
        }

        public void GetData(Action<byte[]> giveDataBack)
        {
            this.giveDataBack = giveDataBack;
        }

        /// <summary>
        /// 不论连接成功或失败都会回调这里
        /// </summary>
        /// <param name="state">连接状态，true为已建立连接，false为连接失败</param>
        /// <param name="info"></param>
        void ConnectedCallBack(bool state, string info)
        {
            if (state)
            {
                connectState = ConnectState.Connected;
                Debug.Log("[NetUtil_Client -> ConnectedCallBack] 已建立连接：" + connectInfo.ip + ":" + connectInfo.port);
            }
            else
            {
                curRepeatTimes++;
                if (repeatTimes == 0 || curRepeatTimes < repeatTimes)
                {
                    connectState = ConnectState.Connecting;
                    Debug.Log("[NetUtil_Client -> ConnectedCallBack] 准备连接：" + connectInfo.ip + ":" + connectInfo.port);
                    if (netType == NetType.UDP)
                    {
                        thread_Receive_UDP = new Thread(new ParameterizedThreadStart(ConnectUDPAsync));
                        thread_Receive_UDP.Start(connectInfo);
                    }
                    else if (netType == NetType.TCP)
                    {
                        thread_Receive_TCP = new Thread(new ParameterizedThreadStart(ConnectTCPAsync));
                        thread_Receive_TCP.Start(connectInfo);
                    }
                }
                else
                {
                    connectState = ConnectState.NotConnected;
                    Debug.Log("[NetUtil_Client -> ConnectedCallBack] 已断开连接：" + connectInfo.ip + ":" + connectInfo.port);
                }
            }
        }

        public bool Send(string data)
        {
            if (connectState == ConnectState.Connected)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(data);
                return Send(sendBytes);
            }
            else
            {
                Debug.LogError("[NetUtil_Client -> Send] 尚未建立连接！");
                return false;
            }
        }

        public bool Send(byte[] data)
        {
            byte[] dataForSend = new byte[data.Length];
            data.CopyTo(dataForSend, 0);
            //splitKey.CopyTo(dataForSend, data.Length);
            if (connectState == ConnectState.Connected)
            {
                try
                {
                    if (netType == NetType.UDP)
                    {
                        udpClient.Send(dataForSend, dataForSend.Length);
                        return true;
                    }
                    else if (netType == NetType.TCP)
                    {
                        if (tcpClient != null)
                        {
                            tcpClient.GetStream().Write(dataForSend, 0, dataForSend.Length);
                            return true;
                        }
                        else
                        {
                            Debug.Log("[NetUtil_Client -> Send] 网络还未连接成功");
                        }

                    }
                }
                catch (Exception e)
                {
                    Debug.Log("[NetUtil_Client -> Send] 发送失败：" + e.ToString());

                    if (autoReconnect)
                    {
                        ReConnect();
                    }
                }
            }
            else
            {
                Debug.LogError("[NetUtil_Client -> Send] 尚未建立连接！");
            }

            return false;
        }

        #region TCP
        void ConnectTCPAsync(object connectInfo)
        {
            ConnectInfo ci = connectInfo as ConnectInfo;
            Debug.Log("[NetUtil_Client -> ConnectTCPAsync] 正在连接TCP：" + ci.ip + ":" + ci.port);
            try
            {
                tcpClient = new TcpClient(ci.ip, ci.port);
                //设置发送超时时间，如果超过这个时间就认为网络已断开
                tcpClient.SendTimeout = 1000;
                Debug.Log("[NetUtil_Client -> ConnectTCPAsync] 网络连接成功：" + ci.ip + ":" + ci.port);
                if (connectedCallBack != null)
                {
                    connectedCallBack(true, "success");
                }
                StartTCPReceiving(tcpClient);
            }
            catch (Exception e)
            {
                Debug.Log("[NetUtil_Client -> ConnectTCPAsync] 连接失败：" + ci.ip + ":" + ci.port + "，" + e.ToString());
                if (connectedCallBack != null)
                {
                    connectedCallBack(false, "failed");
                }
            }
        }

        void StartTCPReceiving(TcpClient tcpClient)
        {
            Debug.Log("[NetUtil_Client -> StartTCPReceiving] StartTCPReceiving");
            stop = false;
            thread_Receive_TCP = new Thread(new ParameterizedThreadStart(TCPReceiveAsync));
            thread_Receive_TCP.Start(tcpClient);
        }

        /// <summary>
        /// 通过TCP接收数据
        /// </summary>
        /// <param name="tcpClient"></param>
        void TCPReceiveAsync(object tcpClient)
        {
            Debug.Log("[NetUtil_Client -> TCPReceiveAsync] Start - TCPReceiveAsync");
            TcpClient t = tcpClient as TcpClient;

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

                Debug.Log("[NetUtil_Client -> TCPReceiveAsync] 收到了新数据: " + receiveBytes.Length + "，远端：" + (t.Client.RemoteEndPoint as IPEndPoint).ToString());

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
            Debug.Log("[NetUtil_Client -> TCPReceiveAsync] Exit - TCPReceiveAsync " + t.Client.RemoteEndPoint);
        }
        #endregion

        #region UDP
        void ConnectUDPAsync(object connectInfo)
        {
            ConnectInfo ci = connectInfo as ConnectInfo;
            Debug.Log("[NetUtil_Client -> ConnectUDPAsync] 正在连接UDP：" + ci.ip + ":" + ci.port);
            try
            {
                udpClient = new UdpClient(ci.ip, ci.port);
                Debug.Log("[NetUtil_Client -> ConnectUDPAsync] 网络连接成功：" + ci.ip + ":" + ci.port);
                if (connectedCallBack != null)
                {
                    connectedCallBack(true, "success");
                }
                IPEndPoint localIPEndPoint = udpClient.Client.LocalEndPoint as IPEndPoint;
                StartUDPReceiving(new UdpClient(localIPEndPoint));
            }
            catch (Exception e)
            {
                Debug.Log("[NetUtil_Client -> ConnectUDPAsync] Exception：" + ci.ip + ":" + ci.port + "，" + e.ToString());
                if (connectedCallBack != null)
                {
                    connectedCallBack(false, "failed");
                }
            }
        }

        void StartUDPReceiving(UdpClient udpClient)
        {
            Debug.Log("[NetUtil_Client -> StartUDPReceiving]");
            stop = false;
            thread_Receive_UDP = new Thread(new ParameterizedThreadStart(UDPReceiveAsync));
            thread_Receive_UDP.Start(udpClient);
        }

        public void StopClient()
        {
            Debug.Log("[NetUtil_Client -> StopUDPReceiving]");
            stop = true;
            autoEvent_UDP.Set();
            autoEvent_TCP.Set();
        }

        void UDPReceiveAsync(object udpClient)
        {
            Debug.Log("[NetUtil_Client -> UDPReceiveAsync] Start - ReceiveAsync");
            IPEndPoint e = new IPEndPoint(IPAddress.Any, 0);
            UdpClient u = udpClient as UdpClient;
            UdpState s = new UdpState();
            s.e = e;
            s.u = u;

            while (!stop)
            {
                u.BeginReceive(new AsyncCallback(UDPReceiveCallback), s);
                autoEvent_UDP.WaitOne();
            }
            Debug.Log("[NetUtil_Client -> UDPReceiveAsync] Exit - ReceiveAsync");
        }

        void UDPReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            receiveBytes = u.EndReceive(ar, ref e);
            newDataIn = true;

            Debug.Log("[NetUtil_Client -> UDPReceiveCallback] 收到了新数据: " + receiveBytes.Length + "，远端：" + e.ToString());

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
    public class NetUtil_Client_Handler : MonoBehaviour
    {
        NetUtil_Client client;
        bool threadSafe;

        public void Init(NetUtil_Client client, bool threadSafe)
        {
            this.client = client;
            this.threadSafe = threadSafe;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (client.newDataIn && threadSafe)
            {
                client.GiveDataBack(client.receiveBytes);
            }
        }

        private void OnApplicationQuit()
        {
            client.StopClient();
        }
    }
}