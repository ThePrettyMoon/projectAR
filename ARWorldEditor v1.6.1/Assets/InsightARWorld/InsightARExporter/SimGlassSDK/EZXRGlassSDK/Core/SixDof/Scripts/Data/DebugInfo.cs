using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EZXR.Glass.SixDof
{
    [Serializable]
    public class DebugInfo
    {
        public float gyroFPS;
        public float accFPS;
        public float gestureTime;
        public float vioTime;
        public float cameFPS;

        public static DebugInfo FromJson(string str)
        {
            return JsonUtility.FromJson<DebugInfo>(str);
        }

        public static string ToJson(DebugInfo debugInfo)
        {
            return JsonUtility.ToJson(debugInfo);
        }
    }
}

