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
    /// NetUtil基类
    /// </summary>
    public class NetUtil
    {
        public enum NetType
        {
            TCP,
            UDP
        }
        public class UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }
        public class TcpState
        {
            public TcpClient u;
            public IPEndPoint e;
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        public class ConnectionInfo
        {
            public string ip;
            public int port;
            public NetType netType;

            public ConnectionInfo(string ip, int port, NetType netType)
            {
                this.ip = ip;
                this.port = port;
                this.netType = netType;
            }
        }

    }
}