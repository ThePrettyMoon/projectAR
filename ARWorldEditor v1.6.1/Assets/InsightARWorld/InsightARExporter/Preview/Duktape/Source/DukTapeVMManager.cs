using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duktape;
using System;
using System.Threading;

public class DukTapeVMManager : MonoBehaviour,IDuktapeListener
{
    private static DukTapeVMManager _instance;
    private bool m_Loaded = false;
    private DuktapeVM m_DuktapeVM;
    private static float StartTime = 0.0f;

    public DuktapeVM DuktapeVM
    {
        get
        {
            return m_DuktapeVM;
        }
    }
    public bool IsLoaded
    {
        get
        {
            return m_Loaded;
        }
    }

    public static DukTapeVMManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("Duktape").AddComponent<DukTapeVMManager>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 无创建的获取DukTapeVMManager,
    /// 避免在unity destory时提示未清理duktape
    /// </summary>
    /// <returns></returns>
    public static DukTapeVMManager InstanceNoCreate => _instance;
   

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = GetComponent<DukTapeVMManager>();
        DontDestroyOnLoad(gameObject);


    }

    private void Start()
    {

    }

    void OnDestroy()
    {

    }

    public void OnBinded(DuktapeVM vm, int numRegs)
    {
        if (numRegs == 0)
        {
            throw new Exception("no type binding registered, please run <MENU>/Duktape/Generate Bindings in Unity Editor Mode before the first running of this project.");
        }
    }

    public void OnTypesBinding(DuktapeVM vm)
    {

    }

    public void OnBindingError(DuktapeVM vm, Type type)
    {
    }

    public void OnProgress(DuktapeVM vm, int step, int total)
    {
    }

    public void OnLoaded(DuktapeVM vm)
    {
        DuktapeUtility.SetDaktapeRunState(DuktapeUtility.DaketapeRunState.running);
        m_Loaded = true;
        m_DuktapeVM = vm;
#if UNITY_EDITOR
        Debug.Log("duktape loaded time cost " + (Time.realtimeSinceStartup - StartTime));
#endif
    }

    public void Startup()
    {
        DuktapeUtility.SetDaktapeRunState(DuktapeUtility.DaketapeRunState.initing);
        m_DuktapeVM = new DuktapeVM(null, 1024 * 1024 * 4);
        m_DuktapeVM.Initialize(this, -1);
#if UNITY_EDITOR
        Debug.Log("duktape start ");
#endif
        StartTime = Time.realtimeSinceStartup;
    }

    public void ShutDown()
    {
        DuktapeUtility.SetDaktapeRunState(DuktapeUtility.DaketapeRunState.unloading);
        if (m_DuktapeVM != null) m_DuktapeVM.Destroy();
        m_DuktapeVM = null;
        m_Loaded = false;
#if UNITY_EDITOR
        Debug.Log("duktape shut down");
#endif
    }

    /// <summary>
    /// add search path
    /// </summary>
    /// <param name="path"></param>
    public void AddSearchPath(string path)
    {
        if(m_DuktapeVM != null)
            m_DuktapeVM.AddSearchPath(path);
    }

    public DuktapeObject RunScript(string fileName,ref Dictionary<string, IntPtr> funcPtrs)
    {
      return  m_DuktapeVM.EvalCustomSource(fileName, ref funcPtrs);
    }


}
