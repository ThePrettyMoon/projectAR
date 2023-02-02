using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public static class MeshRendererExtension 
    {
        public static Bounds getBounds(this MeshRenderer meshRenderer)
        {

            return meshRenderer.bounds;
        }
        public static Material getMaterial(this MeshRenderer meshRenderer)
        {

            return meshRenderer.GetComponent<Renderer>().material;
        }
        public static int getMaterialCount(this MeshRenderer meshRenderer)
        {

            return meshRenderer.GetComponent<Renderer>().materials.Length;
        }

        public static Material getMaterial(this MeshRenderer meshRenderer, int index)
        {
            var mats = meshRenderer.GetComponent<Renderer>().materials;
            if (index <= mats.Length)
                return mats[index];
            return null;
        }

        public static void loadFromFiles(this MeshRenderer meshRenderer, string meshPath, string materialPath)
        {
            //zhltodo
        }

        public static string toString(this MeshRenderer meshRenderer)
        {

            return meshRenderer.ToString();
        }

        public static bool getEnabled(this MeshRenderer meshRenderer)
        {
            return meshRenderer.enabled;
        }
        public static void setEnabled(this MeshRenderer meshRenderer, bool enabled)
        {
            if (meshRenderer != null) meshRenderer.enabled = enabled;
        }
        public static GameObject gameObject(this MeshRenderer meshRenderer)
        {
            return meshRenderer.gameObject;
        }

        public static bool isActiveAndEnabled(this MeshRenderer meshRenderer)
        {
            return meshRenderer.gameObject.activeSelf;
        }

        public static string name(this MeshRenderer meshRenderer)
        {
            return meshRenderer.name;
        }

        public static string tag(this MeshRenderer meshRenderer)
        {
            return meshRenderer.tag;
        }

        public static Transform transform(this MeshRenderer meshRenderer)
        {
            return meshRenderer.transform;
        }

    }
}