using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public static class SkinnedMeshRendererExtension
    {

        public static Bounds getBounds(this SkinnedMeshRenderer skinnedMeshRenderer) {

            return skinnedMeshRenderer.bounds;
        }
        public static Material getMaterial(this SkinnedMeshRenderer skinnedMeshRenderer)
        {

            return skinnedMeshRenderer.GetComponent<Renderer>().material;
        }
        public static int getMaterialCount(this SkinnedMeshRenderer skinnedMeshRenderer)
        {

            return skinnedMeshRenderer.GetComponent<Renderer>().materials.Length;
        }
        public static Material getMaterial(this SkinnedMeshRenderer skinnedMeshRenderer, int index)
        {
            var mats = skinnedMeshRenderer.GetComponent<Renderer>().materials;
            if (index <= mats.Length)
                return mats[index];
            return null;
        }

        public static void loadFromFiles(this SkinnedMeshRenderer skinnedMeshRenderer, string meshPath, string materialPath)
        {
            //zhltodo
        }

        public static string toString(this SkinnedMeshRenderer skinnedMeshRenderer)
        {

            return skinnedMeshRenderer.ToString();
        }

        public static bool getEnabled(this SkinnedMeshRenderer skinnedMeshRenderer)
        {
            return skinnedMeshRenderer.enabled;
        }
        public static void setEnabled(this SkinnedMeshRenderer skinnedMeshRenderer, bool enabled)
        {
            if (skinnedMeshRenderer != null) skinnedMeshRenderer.enabled = enabled;
        }

        public static GameObject gameObject(this SkinnedMeshRenderer skinnedMeshRenderer)
        {
            return skinnedMeshRenderer.gameObject;
        }

        public static bool isActiveAndEnabled(this SkinnedMeshRenderer skinnedMeshRenderer)
        {
            return skinnedMeshRenderer.gameObject.activeSelf;
        }

        public static string name(this SkinnedMeshRenderer skinnedMeshRenderer)
        {
            return skinnedMeshRenderer.name;
        }

        public static string tag(this SkinnedMeshRenderer skinnedMeshRenderer)
        {
            return skinnedMeshRenderer.tag;
        }

        public static Transform transform(this SkinnedMeshRenderer skinnedMeshRenderer)
        {
            return skinnedMeshRenderer.transform;
        }


    }
}


