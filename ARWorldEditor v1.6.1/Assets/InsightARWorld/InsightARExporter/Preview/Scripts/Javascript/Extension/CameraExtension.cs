using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public static class CameraExtension
    {
        public static string toString(this Camera camera)
        {
            return camera.ToString();
        }

        public static Material getPostEffectMaterial(this Camera camera, int index)
        {
            return camera.getPostEffectMaterial(index);
        }
        //AR独有
        public static float estimationSHIntensity(this Camera camera)
        {
            return 0;
        }

        public static float aspect(this Camera camera)
        {
            return camera.aspect;
        }

        public static Insight.Matrix4x4 cameraToWorldMatrix(this Camera camera)
        {
            return MathConverter.FromMatrix4x4( camera.cameraToWorldMatrix);
        }

        public static int cullingMask(this Camera camera)
        {
            return camera.cullingMask;
        }

        public static float depth(this Camera camera)
        {
            return camera.depth;
        }


        public static float farClipPlane(this Camera camera)
        {
            return camera.farClipPlane;
        }

        public static float fieldOfView(this Camera camera)
        {
            return camera.fieldOfView;
        }

        public static float nearClipPlane(this Camera camera)
        {
            return camera.nearClipPlane;
        }

        public static bool orthographic(this Camera camera)
        {
            //return camera.orthographic;
            return false; //always false
        }


        public static float pixelHeight(this Camera camera)
        {
            return camera.pixelHeight;
        }

        public static float pixelWidth(this Camera camera)
        {
            return camera.pixelWidth;
        }

        public static Insight.Vector4 pixelRect(this Camera camera)
        {
            return new Insight.Vector4(camera.pixelRect.x, camera.pixelRect.y, camera.pixelRect.width, camera.pixelRect.height);
        }

        public static Insight.Matrix4x4 projectionMatrix(this Camera camera)
        {
            return MathConverter.FromMatrix4x4(camera.projectionMatrix);
        }

        public static Insight.Vector4 rect(this Camera camera)
        {
            return new Insight.Vector4(camera.rect.x, camera.rect.y, camera.rect.width, camera.rect.height);
        }

        public static Insight.Matrix4x4 worldToCameraMatrix(this Camera camera)
        {
            return MathConverter.FromMatrix4x4( camera.worldToCameraMatrix);
        }

        public static Ray screenPointToRay(this Camera camera, Insight.Vector3 position)
        {
            return camera.ScreenPointToRay(MathConverter.ToVector3( position));
        }

        public static Insight.Vector3 screenToViewportPoint(this Camera camera, Insight.Vector3 position)
        {

            return MathConverter.FromVector3(camera.ScreenToViewportPoint(MathConverter.ToVector3(position)));
        }

        public static Insight.Vector3 screenToWorldPoint(this Camera camera, Vector3 position)
        {

            return MathConverter.FromVector3(camera.ScreenToWorldPoint(MathConverter.ToVector3(position)));
        }

        public static Ray viewportPointToRay(this Camera camera, Vector3 position)
        {
            return camera.ViewportPointToRay(MathConverter.ToVector3(position));
        }

        public static Insight.Vector3 viewportToScreenPoint(this Camera camera, Vector3 position)
        {

            return MathConverter.FromVector3(camera.ViewportToScreenPoint(MathConverter.ToVector3(position)));
        }

        public static Insight.Vector3 viewportToWorldPoint(this Camera camera, Vector3 position)
        {

            return MathConverter.FromVector3(camera.ViewportToWorldPoint(MathConverter.ToVector3(position)));
        }

        public static Insight.Vector3 worldToScreenPoint(this Camera camera, Vector3 position)
        {

            return MathConverter.FromVector3(camera.WorldToScreenPoint(MathConverter.ToVector3(position)));
        }


        public static Vector3 worldToViewportPoint(this Camera camera, Vector3 position)
        {

            return MathConverter.FromVector3(camera.WorldToViewportPoint(MathConverter.ToVector3(position)));
        }


        public static bool getEnabled(this Camera camera)
        {
            return camera.enabled;
        }

        public static void setEnabled(this Camera camera, bool enabled)
        {
            if (camera != null) camera.enabled = enabled;
        }

        public static GameObject gameObject(this Camera camera)
        {
            return camera.gameObject;
        }

        public static bool isActiveAndEnabled(this Camera camera)
        {
            return camera.isActiveAndEnabled;
        }

        public static string name(this Camera camera)
        {
            return camera.name;
        }

        public static string tag(this Camera camera)
        {
            return camera.tag;
        }

        public static Transform transform(this Camera camera)
        {
            return camera.transform;
        }

        /// <summary>
        /// filename : 保存图片到名称
        ///x : int 要截取的保存图片的左上角起始x坐标
        ///y : int 要截取的保存图片的左上角起始y坐标
        ///w : int 要保存图片的宽
        ///h : int 要保存图片的高
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void save(this Camera camera, string filename, int x, int y, int w, int h)
        {
            //todo
        }
    }
}





