using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfos
{
    private const string TAG = "ClientInfos";

    private static ClientInfo clientData;
    private static AW_ClientInfo aw_ClientInfo;


    public static void Init()
    {
        clientData = new ClientInfo();
        clientData.deviceModel = SystemInfo.deviceModel;
        clientData.operatingSystemType = SystemInfo.operatingSystem;
        clientData.internetReachability = NetworkUtility.GetCurrentNetType();
        clientData.bundleId = Application.identifier;
        clientData.deviceBrand = "ios";

        //InsightDebug.Log(TAG, "bundleName: " + clientData.bundleId);

        aw_ClientInfo = new AW_ClientInfo();
        aw_ClientInfo.sdkVersion = InsightConst.SDKVERSION;
        aw_ClientInfo.platform = (int)AW_Platform.IOS_UNITY_SDK;
#if UNITY_IOS
        aw_ClientInfo.platform = (int)AW_Platform.IOS_UNITY_SDK;
#elif UNITY_ANDROID
         aw_ClientInfo.platform = (int)AW_Platform.ANDROID_UNITY_SDK;
#else
         aw_ClientInfo.platform = (int)AW_Platform.IOS_UNITY_SDK;
#endif
        aw_ClientInfo.brand = clientData.deviceBrand;
        aw_ClientInfo.model = clientData.deviceModel;
        aw_ClientInfo.osVersion = clientData.operatingSystemType;
        aw_ClientInfo.xnwa = clientData.internetReachability;
    }

    /// <summary>
    /// 获取设备信息
    /// </summary>
    /// <returns></returns>
    public static AW_ClientInfo GetClientInfo()
    {
        return aw_ClientInfo;
    }

    public static int GetPlatform() {
        return aw_ClientInfo.platform;
    }

    public static string GetBundleID()
    {
        return clientData.bundleId;
    }

    public static void SetPhoneBrand( string brand) {

        aw_ClientInfo.brand = brand;

    }

    public static void SetXnwa(string isWifi)
    {
        aw_ClientInfo.xnwa = isWifi;
    }

    public static void ToString(string info = "") {

        if (aw_ClientInfo != null) {
            string str = "sdkVersion: " + aw_ClientInfo.sdkVersion
                + " platform: " + aw_ClientInfo.platform
                + " brand: " + aw_ClientInfo.brand
                + " model: " + aw_ClientInfo.model
                + " osVersion: " + aw_ClientInfo.osVersion
                + " xnwa: " + aw_ClientInfo.xnwa
           ;
            InsightDebug.Log(TAG, str);
        }
       
    }


    class ClientInfo
    {

        public string deviceBrand;
        public string deviceModel;
        public string operatingSystemType;
        public string internetReachability;
        public string bundleId;
    }

}



