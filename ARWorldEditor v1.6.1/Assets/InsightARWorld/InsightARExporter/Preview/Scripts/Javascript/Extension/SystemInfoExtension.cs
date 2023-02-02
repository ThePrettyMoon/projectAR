using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{

    public static class SystemInfoExtension
    {
        /// <summary>
        /// 1.0.0 major*10000+ min
        /// </summary>
        /// <returns></returns>
        public static int getEngineMajorVersion()
        {
            //todo
            return 10000;
        }

        public static int getEngineMinorVersion()
        {
            //todo
            return 0;
        }

        /// <summary>
        /// enum { InsightApp = 0， Insight3D = 1， InsightWeb = 2 }
        /// </summary>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        public static int getEngineType()
        {
            //todo
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        public static int getOperatingSystemMajorVersion()
        {
            //todo
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int getOperatingSystemMinorVersion()
        {
            //todo
            return 0;
        }

        /// <summary>
        /// { Other = 0, MacOSX = 1, Windows = 2, Linux = 3, Android = 4, iOS = 5， Web = 6 }
        /// </summary>
        /// <returns></returns>
        public static int getOperatingSystemType()
        {
#if UNITY_STANDALONE_OSX
            return 1;
#elif UNITY_STANDALONE_WIN
            return 2;
#elif UNITY_STANDALONE_LINUX
            return 3;
#elif UNITY_ANDROID
            return 4;
#elif UNITY_IPHONE
            return 5;
#elif UNITY_WEBPLAYER
            return 6;
#else
            return 0;
#endif
        }

    }
}


