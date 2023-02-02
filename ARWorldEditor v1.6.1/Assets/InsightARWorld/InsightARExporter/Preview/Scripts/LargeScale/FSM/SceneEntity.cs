using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dongjian.LargeScale
{
    public class SceneEntity : IEntity
    {
        #region params
        private SceneFsmSystem sceneFsmSystem;

        private LoadingController LoadingController;
        private LocationController locationController;
        private NavigationController navigationController;
        //private ProductListController productListController;
        private PoiController poiController;
        #endregion;

        #region
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitFSM()
        {
            if (sceneFsmSystem == null) sceneFsmSystem = new SceneFsmSystem(this);

            if (LoadingController == null) LoadingController = new LoadingController();
            if (locationController == null) locationController = new LocationController();
            if (navigationController == null) navigationController = new NavigationController();
            if (poiController == null) poiController = new PoiController();

            sceneFsmSystem.AddState(LoadingController);
            sceneFsmSystem.AddState(navigationController);
            sceneFsmSystem.AddState(locationController);
            sceneFsmSystem.AddState(poiController);

            sceneFsmSystem.ChangeState(SceneStateID.EN_STATE_LOADING);
        }

        public SceneFsmSystem GetSceneFsmSystem()
        {
            return sceneFsmSystem;
        }

        public LocationController GetLocationController()
        {
            return locationController;
        }

        public NavigationController GetNavigationController()
        {
            return navigationController;
        }

        public PoiController GetPoiController()
        {
            return poiController;
        }
        #endregion
    }
}
