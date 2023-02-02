using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight {

    public static class RectTransformExtension
    {

        public static string toString(this RectTransform rectTransform)
        {
            return rectTransform.ToString();
        }

        public static bool getEnabled(this RectTransform rectTransform)
        {
            //return rectTransform.enabled;
            //todo
            return true;
        }
        public static void setEnabled(this RectTransform rectTransform, bool enabled)
        {
            //if (rectTransform != null) rectTransform.enabled = enabled;
            //todo
        }

        public static GameObject gameObject(this RectTransform rectTransform)
        {
            return rectTransform.gameObject;
        }

        public static bool isActiveAndEnabled(this RectTransform rectTransform)
        {
            return rectTransform.gameObject.activeSelf;
        }

        public static string name(this RectTransform rectTransform)
        {
            return rectTransform.name;
        }

        public static string tag(this RectTransform rectTransform)
        {
            return rectTransform.tag;
        }

        public static Transform transform(this RectTransform rectTransform)
        {
            return rectTransform.transform;
        }

    }

}


