using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dongjian.LargeScale;
using InsightAR.Internal;

/// <summary>
/// 定位场景的初始化
/// </summary>
public class LocationController:ISceneController
{
    private const string TAG = "LocationController";

    private bool m_enabled = false;



    public void Enter(IEntity owner)
    {

        Init();

    }

    public void Execute(IEntity owner)
    {
        if (!m_enabled) return;


    }

    public void Exit(IEntity owner)
    {
        Close();
    }

    public int State()
    {
        return (int)SceneStateID.EN_STATE_LOCATION;
    }

    private void Close()
    {
        m_enabled = false;
    }

    private void Init()
    {
        m_enabled = true;
    }
}
