using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dongjian.LargeScale
{
    public class SceneFsmSystem
    {
        public const string TAG = "SceneFsmSystem";

        public IState m_previousState;
        public IState m_nextState;
        public IState m_currentState;
        public List<IState> states;
        public IEntity m_owner;

        public IState CurrentState()
        {
            return m_currentState;
        }

        public IState NextState()
        {
            return m_nextState;
        }

        public IState PreviousState()
        {
            return m_previousState;
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="entity"></param>
        public SceneFsmSystem(IEntity _entity)
        {
            m_owner = _entity;
            states = new List<IState>();
        }

        /// <summary>
        /// 切换到下个场景
        /// </summary>
        /// <param name="nextStateID"></param>
        public void ChangeState(SceneStateID nextStateID)
        {
            ChangeState((int)nextStateID);
        }

        /// <summary>
        /// 下个场景完成需要加载的场景
        /// </summary>
        /// <param name="nextStateId"></param>
        /// <param name="nextDoneSceneId"></param>
        public void ChangeState(SceneStateID nextStateId, SceneStateID nextDoneSceneId)
        {
            ChangeState((int)nextStateId, (int)nextDoneSceneId);
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public  void AddState(IState state)
        {
            // 空值检验
            if (state == null)
            {
                InsightDebug.LogError(TAG, "FSM ERROR: 不可添加空状态");
            }

            // 当所添加状态为初始状态
            if (states.Count == 0)
            {
                states.Add(state);
                m_currentState = state;
                return;
            }

            // 遍历状态列表，若不存在该状态，则添加
            foreach (IState s in states)
            {
                if (state.State() == s.State())
                {
                    InsightDebug.LogError(TAG, "FSM ERROR: 无法添加状态 " + s.State().ToString() + " 因为该状态已存在");
                    return;
                }
            }
            states.Add(state);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="state"></param>
        public  void ChangeState(int id)
        {
            if (id == -1)
            {
                InsightDebug.LogError(TAG, "状态ID不可为空");
            }

            m_nextState = null; //一个状态没有nextstate

            foreach (IState state in states)
            {
                if (state.State() == id)
                {
                    m_previousState = m_currentState;
                    m_currentState.Exit(m_owner);
                    m_currentState = state;
                    m_currentState.Enter(m_owner);              
                }
            }
        }

        public  void ChangeState(int id, int nextId)
        {
            if (id == -1 || nextId == -1)
            {
                InsightDebug.LogError(TAG, "状态ID不可为空");
            }

            // 记录下一个状态
            foreach (IState state in states)
            {
                if (state.State() == nextId)
                {
                    m_nextState = state;
                }
            }

            foreach (IState state in states)
            {
                if (state.State() == id)
                {
                    m_previousState = m_currentState;
                    m_currentState.Exit(m_owner);
                    m_currentState = state;
                    m_currentState.Enter(m_owner);
                }
            }
        }

        /// <summary>
        /// Machine Update
        /// </summary>
        public  void MachineUpdate()
        {
            if (m_currentState != null)
            {
                m_currentState.Execute(m_owner);
            }
        }

        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="state"></param>
        public  void DeleteState(int id)
        {
            // 空值检验
            if (id == -1)
            {
                InsightDebug.LogError(TAG, "FSM ERROR: 状态ID 不可为空ID");
                return;
            }

            // 遍历并删除状态
            foreach (IState state in states)
            {
                if (state.State() == id)
                {
                    states.Remove(state);
                    return;
                }
            }
            InsightDebug.LogError(TAG, "FSM ERROR: 无法删除状态 " + id.ToString() + ". 状态列表中不存在");
        }
    }


}
