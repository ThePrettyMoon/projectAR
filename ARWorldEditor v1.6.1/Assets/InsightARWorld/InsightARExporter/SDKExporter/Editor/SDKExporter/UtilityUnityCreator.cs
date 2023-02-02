/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEditor;
using UnityEngine;

namespace RenderEngine{

    class UtilityUnityCreator
    {
        public static ReflectionProbe CreateReflectionProbe(ExporterConfig config, out GameObject probeGameObject)
        {
            probeGameObject = new UnityEngine.GameObject("TempReflectionProbe");
            UnityEngine.ReflectionProbe ret = probeGameObject.AddComponent<UnityEngine.ReflectionProbe>() as UnityEngine.ReflectionProbe;

            ret.transform.position = ExporterConfig.REFLECTION_PROBE0_POSITION;
            ret.bounds.SetMinMax(ExporterConfig.REFLECTION_PROBE0_MIN, ExporterConfig.REFLECTION_PROBE0_MAX);
            ret.resolution = config.TEXTURE_FORCE_LOW_QUALITY ? ExporterConfig.REFLECTION_PROBE0_LOW_RESOLUTION : UnityEngine.RenderSettings.defaultReflectionResolution;

            return ret;
        }

        public static void Delete(GameObject gameObject)
        {
            Object.DestroyImmediate(gameObject);
        }
    }
}