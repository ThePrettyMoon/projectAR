using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public static class RendererExtension
    {
        public static Bounds getBounds(this Renderer renderer)
        {

            return renderer.bounds;
        }
        public static Material getMaterial(this Renderer renderer)
        {

            return renderer.GetComponent<Renderer>().material;
        }
        public static int getMaterialCount(this Renderer renderer)
        {

            return renderer.GetComponent<Renderer>().materials.Length;
        }

        public static Material getMaterial(this Renderer renderer, int index)
        {
            var mats = renderer.GetComponent<Renderer>().materials;
            if (index <= mats.Length)
                return mats[index];
            return null;
        }

        public static void loadFromFiles(this Renderer renderer, string meshPath, string materialPath)
        {
            //zhltodo
        }

        public static string toString(this Renderer renderer)
        {

            return renderer.ToString();
        }

        public static bool getEnabled(this Renderer renderer)
        {
            return renderer.enabled;
        }
        public static void setEnabled(this Renderer renderer, bool enabled)
        {
            if (renderer != null) renderer.enabled = enabled;
        }
        public static GameObject gameObject(this Renderer renderer)
        {
            return renderer.gameObject;
        }

        public static bool isActiveAndEnabled(this Renderer renderer)
        {
            return renderer.gameObject.activeSelf;
        }

        public static string name(this Renderer renderer)
        {
            return renderer.name;
        }

        public static string tag(this Renderer renderer)
        {
            return renderer.tag;
        }

        public static Transform transform(this Renderer renderer)
        {
            return renderer.transform;
        }

    }
}
