using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlassContentManager : MonoBehaviour
{
    #region 单例
    private static GlassContentManager _instance;
    public static GlassContentManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region 参数
    private WristType _currentWristType = WristType.ResetMainPanel;
    private bool _hasLoadedMain = false;
    /// <summary>
    /// 判断主界面是否加载过一次
    /// </summary>
    public bool HasLoadedMain { get { return _hasLoadedMain; } }
    // Event Start
    private Action _resetMainCallBack;
    private Action _returnMainCallBack;
    // Event End
    #endregion

    #region 公共函数，外部调用
    /// <summary>
    /// 设置当前Wrist的触发类型
    /// </summary>
    /// <param name="wristType">目前reset在sdk层设置，return在内容层设置</param>
    public void SetWristType(WristType wristType)
    {
        _currentWristType = wristType;
        Debug.Log("****** _currentWristType:" + _currentWristType.ToString());
    }
    public void AddResetMainListener(Action resetMainCallBack)
    {
        _resetMainCallBack = resetMainCallBack;
    }
    public void AddReturnMainListener(Action returnMainCallBack)
    {
        _returnMainCallBack = returnMainCallBack;
    }
    #endregion

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("****** 回到主界面 ******");
            _returnMainCallBack?.Invoke();
        }
    }

    #region 触发事件
    /// <summary>
    /// wrist触发事件，在物体上绑定函数
    /// </summary>
    public void WristTriggerEnter()
    {
        switch (_currentWristType)
        {
            case WristType.ReturnMainPanel:
                _returnMainCallBack?.Invoke();
                break;
            case WristType.ResetMainPanel:
                _resetMainCallBack?.Invoke();
                break;
            default:
                break;
        }
    }
    #endregion
    
    // Private Methods End
}

public enum WristType
{
    ReturnMainPanel,
    ResetMainPanel,
}
