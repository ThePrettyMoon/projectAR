using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationExtension
{
    /// <summary>
    ///StatusUnknown = -1,
    ///NotReachable     = 0, NotReachable
    ///ViaWWAN = 1, //3G or 4G
    ///ViaWiFi = 2, // Wifi
    /// </summary>
    /// <returns></returns>
    public static int CheckNetworkState()
    {
        NetworkReachability networkReachability = NetworkUtility.GetNetworkReachability();
        if (networkReachability == NetworkReachability.NotReachable)
        {
            return 0;
        }
        else if (networkReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return 1;
        }
        else if (networkReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return 2;
        }
        else
        {
            return -1;
        }
    }

    public static NetworkReachability NetworkState
    {
        get
        {
            return UnityEngine.Application.internetReachability;

        }
    }
    public static RuntimePlatform RuntimePlatform
    {
        get
        {
            return UnityEngine.Application.platform;
        }
    }

    public static void OpenUrl(string str)
    {
        UnityEngine.Application.OpenURL(str);
    }

    /// <summary>
    /// 运行所有AR Script脚本全局和脚本域local代码。重置全局和脚本域local变量。
    /// 不会运行函数中的代码，不会重置成员变量，函数中的变量。本操作不会立即执行，
    /// 以避免执行顺序可能引发的问题，它会在所有脚本执行完毕后执行
    /// </summary>
    public static void ResetGlobal()
    {

    }


}
