using Wheels.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 用于从眼镜端同步算法数据到Editor端进行调试
    /// </summary>
    public class SyncAlgData : MonoBehaviour
    {
        public string ip;

        NetUtil_Server server = new NetUtil_Server();
        NetUtil_Client client = new NetUtil_Client();

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("NativeSwapManager.HandTrackingData 的长度为：" + Marshal.SizeOf(typeof(NativeSwapManager.HandTrackingData)));
            #region 远程调试
            if (Application.isEditor)
            {
                server.StartServer(5511, NetUtil.NetType.UDP, GetData);
            }
            else
            {
                client.Connect(ip, 5511, NetUtil.NetType.UDP);

                InvokeRepeating("SendOne", 2, 0.5f);
            }
            #endregion
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 在InvokeRepeating中调用
        /// </summary>
        public void SendOne()
        {
            int size = Marshal.SizeOf(typeof(NativeSwapManager.HandTrackingData)) * 2;
            IntPtr intPtr = Marshal.AllocHGlobal(size);
            try
            {
                byte[] tempArray = new byte[size];
                Marshal.Copy(NativeSwapManager.ptr_HandTrackingData, tempArray, 0, size);
                client.Send(tempArray);
                //Debug.Log("发送数据" + size);
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);
            }
        }

        /// <summary>
        /// 用于手机端远程调试，接收手机端发来的数据
        /// </summary>
        /// <param name="data"></param>
        public void GetData(byte[] data)
        {
            //Debug.Log("收到数据" + data);
            if (NativeSwapManager.Instance != null)
            {
                try
                {
                    Marshal.Copy(data, 0, NativeSwapManager.ptr_HandTrackingData, data.Length);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }

    }
}