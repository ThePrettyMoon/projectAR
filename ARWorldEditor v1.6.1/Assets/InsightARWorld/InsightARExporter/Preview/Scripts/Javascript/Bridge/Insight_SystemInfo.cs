using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight {

    public class SystemInfo
    {

        public static int engineMajorVersion{
            get;
        }

        public static int engineMinorVersion
        {
            get;
        }

        public static int engineType
        {
            get;
        }

        public static int operatingSystemMajorVersion
        {
            get;
        }

        public static int operatingSystemMinorVersion
        {
            get;
        }

        public static int operatingSystemType
        {
            get;
        }


        public static int GetType(string key) {
            //todo
            return 0;
        }

        public static string GetValue(string key)
        {
            //todo
            return "";
        }

    }

}


