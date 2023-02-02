using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Insight {

    public static class RawImageExtension
    {
        public static Material getMaterial(this RawImage rawImage) {

            return rawImage.material;
        }

        public static void setMaterial(this RawImage rawImage, Material material)
        {
            if (rawImage != null) rawImage.material = material;
        }


        public static Insight.Vector4 getColor(this RawImage rawImage)
        {

            return new Insight.Vector4(rawImage.color.r,rawImage.color.g,rawImage.color.b,rawImage.color.a);
        }

        public static void setColor(this RawImage rawImage, Vector4 color)
        {
            if (rawImage != null) rawImage.color = new Color(color.x,color.y,color.z,color.w);
        }

        public static string toString(this RawImage rawImage)
        {

            return rawImage.ToString();
        }


        public static bool getEnabled(this RawImage rawImage)
        {
            return rawImage.enabled;
        }
        public static void setEnabled(this RawImage rawImage, bool enabled)
        {
            if (rawImage != null) rawImage.enabled = enabled;
        }

        public static GameObject gameObject(this RawImage rawImage)
        {
            return rawImage.gameObject;
        }

        public static bool isActiveAndEnabled(this RawImage rawImage)
        {
            return rawImage.isActiveAndEnabled;
        }

        public static string name(this RawImage rawImage)
        {
            return rawImage.name;
        }

        public static string tag(this RawImage rawImage)
        {
            return rawImage.tag;
        }

        public static Transform transform(this RawImage rawImage)
        {
            return rawImage.transform;
        }
    }

}

