using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// 所有状态的interface
    /// </summary>
    public interface IState
    {
        void Enter(IEntity owner);
        void Execute(IEntity owner);
        void Exit(IEntity owner);
        int State();
    }

