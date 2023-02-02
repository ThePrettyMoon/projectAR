using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public class PerformanceStatistics
    {
        public static int GetFPS()
        {
            //todo
            return 0;
        }

        public static int GetFrameTime(int t)
        {
            return UnityEngine.Time.frameCount;
        }

        public static int GetUpdateTime(int t)
        {
            return UnityEngine.Time.frameCount;
        }

        public static int GetRenderTime(int t)
        {
            return UnityEngine.Time.renderedFrameCount;
        }

        public static int GetPhysicsTime(int t)
        {
            return UnityEngine.Time.frameCount ;
        }

        public static ulong GetGPUTime(int t)
        {
            return FrameTimingManager.GetGpuTimerFrequency();
        }

        public static int GetDrawCallCount(int t)
        {
            //todo
            return 0;
        }

        public static int GetTriangleCount(int t)
        {
            //todo
            return 0;
        }

        public static int GetCostyScript()
        {
            //todo
            return 0;
        }

        public static int GetCostyScriptTime()
        {
            //todo
            return 0;
        }

    }

}
