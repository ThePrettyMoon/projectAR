using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight {

    public static class ColliderExtension
    {

        public static string toString(this Collider collider) {
            return collider.ToString();
        }

        public static bool getEnabled(this Collider collider)
        {
            return collider.enabled;
        }
        public static void setEnabled(this Collider collider, bool enabled)
        {
            if (collider != null) collider.enabled = enabled;
        }

        public static GameObject gameObject(this Collider collider)
        {
            return collider.gameObject;
        }

        public static bool isActiveAndEnabled(this Collider collider)
        {
            return collider.gameObject.activeSelf;
        }

        public static string name(this Collider collider)
        {
            return collider.name;
        }

        public static string tag(this Collider collider)
        {
            return collider.tag;
        }

        public static Transform transform(this Collider collider)
        {
            return collider.transform;
        }

    }


}

