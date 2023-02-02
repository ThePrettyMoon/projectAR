using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight {

    public static class CanvasExtension
    {
        public static float scaleFactor(this Canvas canvas)
        {
            return canvas.scaleFactor;
        }
        public static bool isRootCanvas(this Canvas canvas)
        {
            return canvas.isRootCanvas;
        }

        /// <summary>
        /// sdk 笔误
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static int referencePixelPerUnit(this Canvas canvas)
        {
            //todo
            return 0;
        }

        public static Insight.Matrix4x4 view(this Canvas canvas)
        {
            //todo
            return Insight.Matrix4x4.identity;
        }

        public static Insight.Matrix4x4 viewProjection(this Canvas canvas)
        {
            //todo
            return Insight.Matrix4x4.identity;
        }

        public static int renderMode(this Canvas canvas)
        {
            //todo
            return 0;
        }

        public static string toString(this Canvas canvas)
        {
            return canvas.ToString() ;
        }

        public static bool getEnabled(this Canvas canvas)
        {
            return canvas.enabled;
        }
        public static void setEnabled(this Canvas canvas, bool enabled)
        {
            if (canvas != null) canvas.enabled = enabled;
        }

        public static GameObject gameObject(this Canvas canvas)
        {
            return canvas.gameObject;
        }

        public static bool isActiveAndEnabled(this Canvas canvas)
        {
            return canvas.isActiveAndEnabled;
        }

        public static string name(this Canvas canvas)
        {
            return canvas.name;
        }

        public static string tag(this Canvas canvas)
        {
            return canvas.tag;
        }

        public static Transform transform(this Canvas canvas)
        {
            return canvas.transform ;
        }
    }
}


