using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public class ARAnchorManager : MonoBehaviour
    {
        #region 单例
        private static ARAnchorManager instance;
        public static ARAnchorManager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public GameObject prefab_CameraRig;
        public GameObject prefab_HandRig;
        public GameObject[] objsToActive;
        bool getMapSuccess;
        public bool isReady;
        /// <summary>
        /// 地图加载完成时回调此处
        /// </summary>
        public Action OnMapIsReady;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (getMapSuccess)
            {
                Instantiate(prefab_CameraRig);
                Instantiate(prefab_HandRig);
                foreach (GameObject obj in objsToActive)
                {
                    obj.SetActive(true);
                }
                getMapSuccess = false;

                isReady = true;
                if (OnMapIsReady != null)
                {
                    OnMapIsReady();
                }
            }
        }

        public void LoadMap(int mapID)
        {
            Debug.Log("LoadMap: " + mapID);
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

            EZGlassLoadMapListener mapListener = new EZGlassLoadMapListener(LoadMapCallback);

            AndroidJavaObject mla = new AndroidJavaObject("com.ezxr.ezglassarsdk.multiar.MapLoadApi", currentActivity);
            mla.Call("loadMap", mapID, mapListener);
        }

        /// <summary>
        /// 下载成功：code返回0，msg返回本地存储路径
        /// 下载失败：code返回非0
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public void LoadMapCallback(int code, string msg)
        {
            Debug.Log("LoadMapCallback: " + code + ", " + msg);
            if (code == 0)
            {
                NativeTracking.SetSlamMode(4, msg);
                getMapSuccess = true;
            }
        }
    }
}