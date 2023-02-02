using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duktape;
using System;
using Dongjian.LargeScale;
using System.IO;
using InsightAR.Internal;

namespace Insight
{
    public class Tracking
    {

        public static DuktapeObject moduleObject_Recong;
        public static DuktapeObject funcObjcet_Recong;

        public static void OnRecongnizedTarget(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            moduleObject_Recong = moduleObject;
            funcObjcet_Recong = funcObjcet;
        }

       /* public enum AREngines_Type
        {
            NONE_SUPPORTED = 0,
            INSIGHT_AR = 1,
            ARCORE = 2,
            ARKIT = 4,
            HUAWEI_AR = 8,
        }*/
        public static int GetCurrentAREngine()
        {
#if UNITY_EDITOR
            int result = 0;
#elif UNITY_ANDROID
            int result = (int)InsightARNative.iarlsGetCurrentAREngine();
#elif UNITY_IOS
            int result = (int)AREngines_Type.ARKIT;
#endif
            return result;
        }
    }
}
