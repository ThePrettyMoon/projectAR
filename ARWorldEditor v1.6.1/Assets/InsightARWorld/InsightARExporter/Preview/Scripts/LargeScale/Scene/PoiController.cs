using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiController : ISceneController
{
    public void Enter(IEntity owner)
    {
     
    }

    public void Execute(IEntity owner)
    {
       
    }

    public void Exit(IEntity owner)
    {
        
    }

    public int State()
    {
        return (int)SceneStateID.EN_STATE_POI;
    }
}
