using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private IEntity m_ower;
    private EntityFSM m_currentState;
    private EntityFSM m_PreviousState;
    private EntityFSM m_GlobalState;
    private List<EntityFSM> states;
    // 状态列表

    public StateID CurrentStateID
    {
        get
        {
            if (m_currentState != null)
            {
                return m_currentState.ID();
            }
            else
            {
                return StateID.NullStateID;
            }
        }
    }


    public FSMSystem(IEntity _ower)
    {
        m_ower = _ower;
        states = new List<EntityFSM>();
    }

    /// <summary>
    /// States the machine update.
    /// </summary>
    public void StateMachineUpdate()
    {

        if (m_currentState != null)
        {
            m_currentState.Execute(m_ower);
        }
    }

    /// <summary>
    /// Adds the state.
    /// </summary>
    /// <param name="state">State.</param>
    public void AddState(EntityFSM state)
    {
        // 空值检验
        if (state == null)
        {
            Debug.LogError("FSM ERROR: 不可添加空状态");
        }

        // 当所添加状态为初始状态
        if (states.Count == 0)
        {
            states.Add(state);
            m_currentState = state;
            return;
        }

        // 遍历状态列表，若不存在该状态，则添加
        foreach (EntityFSM s in states)
        {
            if (state.ID() == s.ID())
            {
                Debug.LogError("FSM ERROR: 无法添加状态 " + s.ID().ToString() + " 因为该状态已存在");
                return;
            }
        }
        states.Add(state);
    }

    /// <summary>
    /// Deletes the state.
    /// </summary>
    /// <param name="id">Identifier.</param>
    public void DeleteState(StateID id)
    {
        // 空值检验
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: 状态ID 不可为空ID");
            return;
        }

        // 遍历并删除状态
        foreach (EntityFSM state in states)
        {
            if (state.ID() == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: 无法删除状态 " + id.ToString() + ". 状态列表中不存在");
    }

    /// <summary>
    /// Changes the state.
    /// </summary>
    /// <param name="id">Identifier.</param>
    public void ChangeState(StateID id)
    {
        if (id == StateID.NullStateID)
        {
            Debug.Log("状态ID不可为空");
        }

        foreach (EntityFSM state in states)
        {
            if (state.ID() == id)
            {
                m_PreviousState = m_currentState;
                m_currentState.Exit(m_ower);
                m_currentState = state;
                m_currentState.Enter(m_ower);
            }
        }
    }

    /// <summary>
    /// Currents the state.
    /// </summary>
    /// <returns>The state.</returns>
    public EntityFSM CurrentState()
    {
        return m_currentState;
    }

    /// <summary>
    /// Previouses the state.
    /// </summary>
    /// <returns>The state.</returns>
    public EntityFSM PreviousState()
    {
        return m_PreviousState;
    }

    /// <summary>
    /// Globals the state.
    /// </summary>
    /// <returns>The state.</returns>
    public EntityFSM GlobalState()
    {
        return m_GlobalState;
    }
}

