using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{
    public static class SphereColliderExtension
    {

        public static bool isTrigger(this SphereCollider sphereCollider)
        {
            return sphereCollider.isTrigger;
        }


        public static string toString(this SphereCollider sphereCollider)
        {

            return sphereCollider.ToString();
        }

        public static bool getEnabled(this SphereCollider sphereCollider)
        {
            return sphereCollider.enabled;
        }
        public static void setEnabled(this SphereCollider sphereCollider, bool enabled)
        {
            if (sphereCollider != null) sphereCollider.enabled = enabled;
        }

        public static GameObject gameObject(this SphereCollider sphereCollider)
        {
            return sphereCollider.gameObject;
        }

        public static bool isActiveAndEnabled(this SphereCollider sphereCollider)
        {
            return sphereCollider.gameObject.activeSelf;
        }

        public static string name(this SphereCollider sphereCollider)
        {
            return sphereCollider.name;
        }

        public static string tag(this SphereCollider sphereCollider)
        {
            return sphereCollider.tag;
        }

        public static Transform transform(this SphereCollider sphereCollider)
        {
            return sphereCollider.transform;
        }
    }


}

