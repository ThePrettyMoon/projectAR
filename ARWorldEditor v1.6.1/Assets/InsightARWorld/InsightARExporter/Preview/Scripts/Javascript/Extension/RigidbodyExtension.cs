using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{
    public static class RigidbodyExtension
    {
        private static bool mEnabled = true;

        public static void addForceAtLocalPosition(this Rigidbody rigidbody,Insight.Vector3 force,Insight.Vector3 position, Insight.ForceMode mode)
        {
            //todo
        }

        public static void addRelativeForce(this Rigidbody rigidbody,Insight.Vector3 force, Insight.ForceMode mode)
        {
            //todo
        }

        public static void addRelativeForceAtLocalPosition(this Rigidbody rigidbody, Insight.Vector3 force, Insight.Vector3 position, Insight.ForceMode mode)
        {
            //todo
        }

        public static void addRelativeForceAtPosition(this Rigidbody rigidbody, Insight.Vector3 force, Insight.Vector3 position, Insight.ForceMode mode)
        {
            //todo
        }

        public static string toString(this Rigidbody rigidbody)
        {

            return rigidbody.ToString();
        }

        public static bool getEnabled(this Rigidbody rigidbody)
        {
            return mEnabled;
        }
        public static void setEnabled(this Rigidbody rigidbody, bool enabled)
        {
            if (rigidbody != null) mEnabled = enabled;
        }

        public static GameObject gameObject(this Rigidbody rigidbody)
        {
            return rigidbody.gameObject;
        }

        public static bool isActiveAndEnabled(this Rigidbody rigidbody)
        {
            return rigidbody.gameObject.activeSelf;
        }

        public static string name(this Rigidbody rigidbody)
        {
            return rigidbody.name;
        }

        public static string tag(this Rigidbody rigidbody)
        {
            return rigidbody.tag;
        }

        public static Transform transform(this Rigidbody rigidbody)
        {
            return rigidbody.transform;
        }
    }

}


