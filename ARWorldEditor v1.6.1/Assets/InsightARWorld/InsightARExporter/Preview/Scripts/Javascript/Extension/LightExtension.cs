using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight {

    public static class LightExtension
    {

        public static string toString(this Light light)
        {
            return light.ToString();
        }

        public static bool getEnabled(this Light light)
        {
            return light.enabled;
        }
        public static void setEnabled(this Light light, bool enabled)
        {
            if (light != null) light.enabled = enabled;
        }

        public static GameObject gameObject(this Light light)
        {
            return light.gameObject;
        }

        public static bool isActiveAndEnabled(this Light light)
        {
            return light.gameObject.activeSelf;
        }

        public static string name(this Light light)
        {
            return light.name;
        }

        public static string tag(this Light light)
        {
            return light.tag;
        }

        public static Transform transform(this Light light)
        {
            return light.transform;
        }


        public static Vector4 estimationColor(this Light light)
        {
            return new Vector4(1,1,1,1);
        }

        public static float estimationIntensity(this Light light)
        {
            return 0.5f;
        }
    }

}


