using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkUtility
{
    #region params
    private const string CONNECT_TYPE_WIFI = "wifi";
    private const string CONNECT_TYPE_CTNET = "ctnet";
    private const string CONNECT_TYPE_CTWAP = "ctwap";
    private const string CONNECT_TYPE_CMNET = "cmnet";
    private const string CONNECT_TYPE_CMWAP = "cmwap";
    private const string CONNECT_TYPE_UNIWAP = "uniwap";
    private const string CONNECT_TYPE_UNINET = "uninet";
    private const string CONNECT_TYPE_UNI3GWAP = "3gwap";
    private const string CONNECT_TYPE_NOT_REACHABLE = "unavaible";
    #endregion

    #region custom functions
    /// <summary>
    /// 返回网络连通性
    /// </summary>
    /// <returns></returns>
    public static NetworkReachability GetNetworkReachability()
    {
        return Application.internetReachability;
    }

    /// <summary>
    /// 返回当前网络类型
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentNetType()
    {
        NetworkReachability networkReachability = GetNetworkReachability();
        if (networkReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return CONNECT_TYPE_WIFI;
        }
        else if (networkReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return CONNECT_TYPE_UNI3GWAP;
        }
        return CONNECT_TYPE_NOT_REACHABLE;
    }

    /// <summary>
    /// 判断网络是否可用
    /// </summary>
    /// <returns></returns>
    public static bool IsNetworkAvaible()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return true;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return true;
        }
        return false;
    }
    #endregion
}
