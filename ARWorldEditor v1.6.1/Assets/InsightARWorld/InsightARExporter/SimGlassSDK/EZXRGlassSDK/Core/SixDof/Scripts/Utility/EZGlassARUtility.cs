namespace EZXR.Glass.SixDof
{
    using UnityEngine;

    public class EZGlassARUtility
    {
        public static RenderTexture CreateRenderTexture(int width, int height, int depth = 24, RenderTextureFormat format = RenderTextureFormat.ARGB32, bool usequaAnti = true)
        {
            var rt = new RenderTexture(width, height, depth, format);
            rt.wrapMode = TextureWrapMode.Clamp;
            if (QualitySettings.antiAliasing > 0 && usequaAnti)
            {
                rt.antiAliasing = QualitySettings.antiAliasing;
            }
            rt.Create();
            return rt;
        }
    }
}