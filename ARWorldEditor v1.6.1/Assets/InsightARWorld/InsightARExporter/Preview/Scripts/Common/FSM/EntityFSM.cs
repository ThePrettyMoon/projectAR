using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityFSM
{
    public bool _isReadyExit = false;

    private bool _isEnableFsm = true;

    public bool EnableFsm
    {
        get
        {
            return _isEnableFsm;
        }
        set
        {
            _isEnableFsm = value;
        }
    }

    public virtual StateID ID()
    {
        return StateID.NullStateID;
    }

    public virtual void Enter(IEntity entity)
    {

    }

    public virtual void Execute(IEntity entity)
    {

    }

    public virtual void Exit(IEntity entity)
    {

    }
}

