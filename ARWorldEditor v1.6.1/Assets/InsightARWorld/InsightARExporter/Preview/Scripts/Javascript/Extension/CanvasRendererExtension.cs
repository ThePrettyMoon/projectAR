using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{
    public static class CanvasRendererExtension
    {
        private static bool mEnabled = true;

        public static string toString(this CanvasRenderer canvasRenderer)
        {
            return canvasRenderer.ToString();
        }

        public static bool getEnabled(this CanvasRenderer canvasRenderer)
        {
            return mEnabled;
        }
        public static void setEnabled(this CanvasRenderer canvasRenderer, bool enabled)
        {
            if (canvasRenderer != null) mEnabled = enabled;
        }

        public static GameObject gameObject(this CanvasRenderer canvasRenderer)
        {
            return canvasRenderer.gameObject;
        }

        public static bool isActiveAndEnabled(this CanvasRenderer canvasRenderer)
        {
            return canvasRenderer.gameObject.activeSelf;
        }

        public static string name(this CanvasRenderer canvasRenderer)
        {
            return canvasRenderer.name;
        }

        public static string tag(this CanvasRenderer canvasRenderer)
        {
            return canvasRenderer.tag;
        }

        public static Transform transform(this CanvasRenderer canvasRenderer)
        {
            return canvasRenderer.transform;
        }
    }
}


