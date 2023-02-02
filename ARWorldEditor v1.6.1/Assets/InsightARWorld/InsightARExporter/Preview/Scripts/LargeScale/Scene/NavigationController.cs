using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;
using System;

namespace Dongjian.LargeScale
{
    public class NavigationController : ISceneController
    {
        #region params
        private const string TAG = "NavigationController";

        private bool m_enabled = false;
        #endregion

        #region custom functions
        /// <summary>
        /// state
        /// </summary>
        /// <returns></returns>
        public int State()
        {
            return (int)SceneStateID.EN_STATE_NAVIGATION;
        }

        /// <summary>
        /// enter
        /// </summary>
        /// <param name="entity"></param>
        public void Enter(IEntity entity)
        {
            if (!GameSceneData.Instance.GetNaviEnabled())
            {
                LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_LOCATION);
                return;
            }

            if (NaviSceneManager.Instance.CheckNavigatorStatus() != 0) {
                InsightDebug.Log(TAG, "navigation has not inited completely");
                LSGameManager.Instance.ChangeState(SceneStateID.EN_STATE_LOCATION);
                return;
            }

            Init();
            NavigationInterface.EnterNavigation();

            var poiTarget = GameSceneData.Instance.GetNaviPoiInput();
            InsightDebug.Log(TAG, poiTarget);
            NaviSceneManager.Instance.BeginNavi("", poiTarget);
            NaviSceneManager.Instance.ConvertPose("", poiTarget);

            InsightDebug.Log(TAG, " enter navigation");
        }

        /// <summary>
        /// execute
        /// </summary>
        /// <param name="entity"></param>
        public void Execute(IEntity entity)
        {
            if (!m_enabled) return;

            NavigationInterface.UpdateNavigation();

        }

        /// <summary>
        /// exit
        /// </summary>
        /// <param name="entity"></param>
        public void Exit(IEntity entity)
        {
            if (!m_enabled)
                return;
            Close();
            NavigationInterface.EndNavigation();

            InsightDebug.Log(TAG, " exit navigation");
        }

        private void Init()
        {
            m_enabled = true;

        }

        /// <summary>
        /// 停止导航
        /// </summary>
        private void Close()
        {
            m_enabled = false;
        }

        #endregion
    }
}
