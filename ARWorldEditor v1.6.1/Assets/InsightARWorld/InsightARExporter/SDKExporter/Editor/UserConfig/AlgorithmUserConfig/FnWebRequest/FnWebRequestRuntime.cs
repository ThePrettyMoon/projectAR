/* by 小晕晕 */
using UnityEngine;

/// <summary>
/// 不可用于WebGL这种不支持多线程的平台
/// </summary>
[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
public class FnWebRequestRuntime : MonoBehaviour
{
    public bool threadSafe;

    #region 单例、构造
    private static FnWebRequestRuntimeHandler _instance = null;
    private static readonly object SynObject = new object();

    public static FnWebRequestRuntimeHandler instance
    {
        get
        {
            lock (SynObject)
            {
                if (_instance == null)
                {
                    FnWebRequestRuntime fnWebRequestRuntime = new GameObject("FnWebRequestRuntime").AddComponent<FnWebRequestRuntime>();
                    _instance = new FnWebRequestRuntimeHandler(fnWebRequestRuntime);
                }
                return _instance;
            }
        }
    }
    #endregion

    /// <summary>
    /// 用于线程安全的delegate用
    /// </summary>
    void Update()
    {
        if (_instance.ThreadSafe)
        {
            if (_instance.giveBackBytes.Count > 0)
            {
                for (int i = 0; i < _instance.giveBackBytes.Count; i++)
                {
                    _instance.giveBackBytes[i].giveBackBytes(_instance.giveBackBytes[i].responseData, _instance.giveBackBytes[i].identifier);
                    _instance.giveBackBytes.RemoveAt(i--);
                }
            }
            if (_instance.giveBackProgress.Count > 0)
            {
                for (int i = 0; i < _instance.giveBackProgress.Count; i++)
                {
                    _instance.giveBackProgress[i].giveBackLoadingProgress(_instance.giveBackProgress[i].progress, _instance.giveBackProgress[i].identifier);
                    if (_instance.giveBackProgress[i].progress == 1 || _instance.giveBackProgress[i].progress == -1)
                    {
                        _instance.giveBackProgress.RemoveAt(i--);
                    }
                }
            }
        }
    }

}

[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
public class FnWebRequestRuntimeHandler : FnWebRequestBase
{
    FnWebRequestRuntime _behaviour;
    public FnWebRequestRuntimeHandler(FnWebRequestRuntime behaviour)
    {
        _behaviour = behaviour;
    }

    /// <summary>
    /// 启用或禁用ThreadSafeUpdate
    /// </summary>
    public bool ThreadSafe
    {
        set
        {
            if (this.threadSafe != value)
            {
                this.threadSafe = value;
                _behaviour.threadSafe = value;
            }
        }
        get
        {
            return this.threadSafe;
        }
    }

}
