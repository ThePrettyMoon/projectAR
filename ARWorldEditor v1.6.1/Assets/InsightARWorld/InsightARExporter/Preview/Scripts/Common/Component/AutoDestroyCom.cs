using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 自动销毁组件
/// </summary>
public class AutoDestroyCom : MonoBehaviour
{
    #region params

    private float fDestroyTime;
    private float _curTime;
    private bool isActive = false;

    #endregion

    #region unity_functions

    // Use this for initialization
    void Start()
    {
        _curTime = 0.0f; 
    }

    void Update()
    {
        if (isActive)
        {
            if (_curTime < fDestroyTime)
            {
                _curTime += Time.deltaTime;
            }
            else
            {
                GameUtility.Destroy(gameObject);
                isActive = false;
            }
        }

    }

    #endregion

    #region custom_functions

    public void Init(GameObject _objDestroy, float _fDestroyTime)
    {
        fDestroyTime = _fDestroyTime;
        isActive = true;
    }

    public void Init(float _time)
    {
        fDestroyTime = _time;
        isActive = true;
    }

    #endregion
}
