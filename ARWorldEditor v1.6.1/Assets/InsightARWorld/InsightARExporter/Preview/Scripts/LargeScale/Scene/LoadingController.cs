using InsightAR.Internal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class LoadingController : ISceneController
{
    const string TAG = "LoadingController";

    private void Init() {
    }

    private void Close()
    {

    }

    public void Enter(IEntity owner)
    {
        //waiting 0.5s for initing js VM
        //Init();
        //Thread.Sleep(500);
        Init();
    }

    public void Execute(IEntity owner)
    {
        
    }

    public void Exit(IEntity owner)
    {
        Close();
    }

    public int State()
    {
        return (int)SceneStateID.EN_STATE_LOADING;
    }

}
