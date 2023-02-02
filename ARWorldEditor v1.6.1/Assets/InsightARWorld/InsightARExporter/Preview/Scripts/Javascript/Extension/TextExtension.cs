using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Insight
{
    public static class TextExtension 
    {

        public static Vector4 getColor(this Text text)
        {
            return new Insight.Vector4(text.color.r, text.color.g, text.color.b, text.color.a) ;
        }

        public static void setColor(this Text text, Vector4 color)
        {
            if (text != null) text.color = new Color(color.x,color.y,color.z,color.w);
        }


        public static Material getMaterial(this Text text)
        {
            return text.material;
        }

        public static void setMaterial(this Text text, Material material)
        {
            if(text != null) text.material = material;
        }


        public static string toString(this Text text)
        {

            return text.ToString();
        }

        public static bool getEnabled(this Text text)
        {
            return text.enabled;
        }
        public static void setEnabled(this Text text, bool enabled)
        {
            if (text != null) text.enabled = enabled;
        }

        public static GameObject gameObject(this Text text)
        {
            return text.gameObject;
        }

        public static bool isActiveAndEnabled(this Text text)
        {
            return text.gameObject.activeSelf;
        }

        public static string name(this Text text)
        {
            return text.name;
        }

        public static string tag(this Text text)
        {
            return text.tag;
        }

        public static Transform transform(this Text text)
        {
            return text.transform;
        }
    }
}


