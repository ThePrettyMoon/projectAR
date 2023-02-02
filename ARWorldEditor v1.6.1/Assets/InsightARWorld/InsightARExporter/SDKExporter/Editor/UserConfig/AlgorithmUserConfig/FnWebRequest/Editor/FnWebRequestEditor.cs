/* by 小晕晕 */
using UnityEditor;


[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
public class FnWebRequestEditor : FnWebRequestBase
{
    #region 单例、构造

    private static FnWebRequestEditor _instance = null;
    private static readonly object SynObject = new object();

    public static FnWebRequestEditor instance
    {
        get
        {
            lock (SynObject)
            {
                if (_instance == null)
                {
                    _instance = new FnWebRequestEditor();
                }
                return _instance;
            }
        }
    }

    #endregion

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
                if (threadSafe)
                {
                    EditorApplication.update += ThreadSafeUpdate;
                    // Debug.Log ("ThreadSafeUpdate Started!");
                }
                else
                {
                    EditorApplication.update -= ThreadSafeUpdate;
                    // Debug.Log ("ThreadSafeUpdate Ended!");
                }
            }
        }
        get
        {
            return this.threadSafe;
        }
    }

    /// <summary>
    /// 用于线程安全的delegate用
    /// </summary>
    void ThreadSafeUpdate()
    {
        if (giveBackBytes.Count > 0)
        {
            for (int i = 0; i < giveBackBytes.Count; i++)
            {
                giveBackBytes[i].giveBackBytes(giveBackBytes[i].responseData, giveBackBytes[i].identifier);
                giveBackBytes.RemoveAt(i--);
            }
        }
        if (giveBackProgress.Count > 0)
        {
            for (int i = 0; i < giveBackProgress.Count; i++)
            {
                giveBackProgress[i].giveBackLoadingProgress(giveBackProgress[i].progress, giveBackProgress[i].identifier);
                if (giveBackProgress[i].progress == 1 || giveBackProgress[i].progress == -1)
                {
                    giveBackProgress.RemoveAt(i--);
                }
            }
        }
    }
}