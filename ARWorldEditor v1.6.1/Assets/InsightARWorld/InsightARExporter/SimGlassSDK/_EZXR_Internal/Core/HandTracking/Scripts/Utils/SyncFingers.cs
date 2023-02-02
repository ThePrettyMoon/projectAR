using Wheels.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    /// <summary>
    /// 眼镜端接收Editor的数据用
    /// </summary>
    public class SyncFingers : MonoBehaviour
    {
        Vector3 rot_MainCamera;
        public Transform hand_Left;
        public Transform hand_Right;
        Vector3 rot;
        Vector3 pos;
        public Transform eye_Left;
        public Transform eye_Right;
        Vector3 rot_eye_L;
        Vector3 pos_eye_L;
        Vector3 rot_eye_R;
        Vector3 pos_eye_R;

        public Transform hand;
        Vector3 scale_Hand;



        // Start is called before the first frame update
        void Start()
        {
            if (!Application.isEditor)
            {
                NetUtil_Client.instance.Connect("10.244.10.31", 5551, NetUtil_Client.NetType.TCP, GetData);
                //NetUtil_Client.instance.GetData(GetData);
            }
            else
            {
                NetUtil_Server.instance.StartServer(5551, NetUtil_Server.NetType.TCP, ServerGetData);
            }
        }

        public void ServerGetData(byte[] data)
        {
            Debug.Log("ServerGetData得到内容：" + Encoding.UTF8.GetString(data));
        }

        /// <summary>
        /// 手机端接收Editor端发来的数据
        /// </summary>
        /// <param name="data"></param>
        public void GetData(byte[] data)
        {
            pos = new Vector3(BitConverter.ToSingle(data, 0), BitConverter.ToSingle(data, 4), BitConverter.ToSingle(data, 8));
            rot = new Vector3(BitConverter.ToSingle(data, 12), BitConverter.ToSingle(data, 16), BitConverter.ToSingle(data, 20));
            rot_MainCamera = new Vector3(BitConverter.ToSingle(data, 24), BitConverter.ToSingle(data, 28), BitConverter.ToSingle(data, 32));
            pos_eye_L = new Vector3(BitConverter.ToSingle(data, 36), BitConverter.ToSingle(data, 40), BitConverter.ToSingle(data, 44));
            //rot_eye_L = new Vector3(BitConverter.ToSingle(data, 36), BitConverter.ToSingle(data, 40), BitConverter.ToSingle(data, 44));
            //pos_eye_R = new Vector3(BitConverter.ToSingle(data, 48), BitConverter.ToSingle(data, 52), BitConverter.ToSingle(data, 56));
            //rot_eye_R = new Vector3(BitConverter.ToSingle(data, 60), BitConverter.ToSingle(data, 64), BitConverter.ToSingle(data, 68));

            scale_Hand = new Vector3(BitConverter.ToSingle(data, 48), BitConverter.ToSingle(data, 52), BitConverter.ToSingle(data, 56));
        }

        // Update is called once per frame
        void Update()
        {
            if (Application.isEditor)
            {
                if (Application.isEditor)
                {
                    List<byte> data = new List<byte>();
                    data.AddRange(BitConverter.GetBytes(hand_Right.localPosition.x));
                    data.AddRange(BitConverter.GetBytes(hand_Right.localPosition.y));
                    data.AddRange(BitConverter.GetBytes(hand_Right.localPosition.z));
                    data.AddRange(BitConverter.GetBytes(hand_Right.localEulerAngles.x));
                    data.AddRange(BitConverter.GetBytes(hand_Right.localEulerAngles.y));
                    data.AddRange(BitConverter.GetBytes(hand_Right.localEulerAngles.z));

                    data.AddRange(BitConverter.GetBytes(transform.localEulerAngles.x));
                    data.AddRange(BitConverter.GetBytes(transform.localEulerAngles.y));
                    data.AddRange(BitConverter.GetBytes(transform.localEulerAngles.z));

                    data.AddRange(BitConverter.GetBytes(eye_Left.localPosition.x));
                    data.AddRange(BitConverter.GetBytes(eye_Left.localPosition.y));
                    data.AddRange(BitConverter.GetBytes(eye_Left.localPosition.z));
                    //data.AddRange(BitConverter.GetBytes(eye_Left.localEulerAngles.x));
                    //data.AddRange(BitConverter.GetBytes(eye_Left.localEulerAngles.y));
                    //data.AddRange(BitConverter.GetBytes(eye_Left.localEulerAngles.z));

                    //data.AddRange(BitConverter.GetBytes(eye_Right.localPosition.x));
                    //data.AddRange(BitConverter.GetBytes(eye_Right.localPosition.y));
                    //data.AddRange(BitConverter.GetBytes(eye_Right.localPosition.z));
                    //data.AddRange(BitConverter.GetBytes(eye_Right.localEulerAngles.x));
                    //data.AddRange(BitConverter.GetBytes(eye_Right.localEulerAngles.y));
                    //data.AddRange(BitConverter.GetBytes(eye_Right.localEulerAngles.z));

                    data.AddRange(BitConverter.GetBytes(hand.localScale.x));
                    data.AddRange(BitConverter.GetBytes(hand.localScale.y));
                    data.AddRange(BitConverter.GetBytes(hand.localScale.z));

                    NetUtil_Server.instance.SendToAll(data.ToArray());
                }
            }
            else
            {
                hand_Left.localPosition = pos;
                hand_Left.localEulerAngles = rot;

                hand_Right.localPosition = pos;
                hand_Right.localEulerAngles = rot;

                transform.localEulerAngles = rot_MainCamera;

                eye_Left.localPosition = pos_eye_L;
                //eye_Left.localEulerAngles = rot_eye_L;

                eye_Right.localPosition = -1 * pos_eye_L;
                //eye_Right.localEulerAngles = rot_eye_R;

                hand.localScale = scale_Hand;
            }
        }
    }
}