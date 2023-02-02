using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public static class Texture2DUtility
{
    /// <summary>
    ///旋转texture2d ，根据当前设备朝向旋转纹理
    /// </summary>
    /// <param name="src">Source.</param>
    /// <param name="dst">Dst.</param>
    public static void   RotateTexture(Texture2D tex, ScreenDeviceOrientation orientation)
    {
        if (orientation == ScreenDeviceOrientation.UnChanged)
        {
            return;
        }

        int w = tex.width;
        int h = tex.height;
        Color[] srcColors = tex.GetPixels();

        if (srcColors.Length < w * h)
            return;
             
        Color[] dstColors = new Color[srcColors.Length];

        if (orientation == ScreenDeviceOrientation.PortraitToLandScape)
        {
            tex.Resize(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    dstColors[h * i + (h - 1 - j)] = srcColors[j * w + i];
                }
            }  
        }
        else if (orientation == ScreenDeviceOrientation.PortraitToReverseLandScape)
        {
            tex.Resize(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    dstColors[h * (w - 1 - i) + j] = srcColors[j * w + i];
                }
            }   
        }
        else if (orientation == ScreenDeviceOrientation.LandScapeToPortrait)
        {
            tex.Resize(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    dstColors[h * i + j] = srcColors[j * w + (w - 1 - i)];
                }
            }   
        }
        else if (orientation == ScreenDeviceOrientation.LandScapeToReversePortrait)
        {
            tex.Resize(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    dstColors[h * i + j] = srcColors[(h - 1 - j) * w + i];
                }
            }    
        }
        else if (orientation == ScreenDeviceOrientation.PortraitToReversePortrait || orientation == ScreenDeviceOrientation.LandScapeToReverseLandScape)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    dstColors[h * i + j] = srcColors[h * (w - 1 - i) + (h - 1 - j)];
                }
            }    
        }

        tex.SetPixels(dstColors);
        tex.Apply(false);
    }

    /// <summary>
    ///旋转texture2d ，内容宽高都会旋转,顺时针 ,0-360
    /// -90  竖屏转横屏
    /// 90 横屏转竖屏
    /// </summary>
    /// <param name="src">Source.</param>
    /// <param name="dst">Dst.</param>
    public static void  RotateTexture(Texture2D src, Texture2D dst, ScreenDeviceOrientation orientation)
    {
        
        dst = new Texture2D(src.width, src.height, src.format, false);
        RotateTexture(dst, orientation); 
    }


    /// <summary>
    /// 缩放纹理 
    /// </summary>
    /// <returns>The texture.</returns>
    /// <param name="tex">Tex.</param>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    public static Texture2D ResizeTexture(Texture2D tex, float scale)
    {
        if (tex == null)
            return null;
        Texture2D new_tex = new Texture2D(tex.width, tex.height, tex.format, false);
        new_tex.SetPixels(tex.GetPixels());
        if (Mathf.Abs(scale - 1.0f) < 0.001f)
            return new_tex; 
        TextureScale.Bilinear(new_tex, (int)(scale * tex.width), (int)(scale * tex.height));
        return new_tex;
    }

    /// <summary>
    /// 缩放纹理 
    /// </summary>
    /// <returns>The texture.</returns>
    /// <param name="tex">Tex.</param>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    public static Texture2D ResizeTexture(Texture2D tex, float scale, int refrenceHeight)
    {
        if (tex == null)
            return null;

        int scaleHeight = (int)(scale * tex.height);
        int scaleRefrenceHeight = (int)(scale * refrenceHeight);
        //按照固定比例缩放
        if (Mathf.Abs(scaleHeight - scaleRefrenceHeight) > 1)
        {
            scale = scale * (float)refrenceHeight / (float)tex.height;
        }

        Texture2D new_tex = new Texture2D(tex.width, tex.height, tex.format, false);
        new_tex.SetPixels(tex.GetPixels());

        if (Mathf.Abs(scale - 1.0f) < 0.001f)
            return new_tex; 
        
        TextureScale.Bilinear(new_tex, (int)(scale * tex.width), (int)(scale * tex.height));
        return new_tex;
    }

    /// <summary>
    ///设置纹理大小 
    /// </summary>
    /// <param name="Text">Text.</param>
    /// <param name="w">The width.</param>
    /// <param name="h">The height.</param>
    public static void ResizeTexture(Texture2D Text, int w, int h)
    {
        TextureScale.Bilinear(Text, w, h); 
    }

    /// <summary>
    /// Loads the local texture.
    /// </summary>
    /// <returns>The local texture.</returns>
    /// <param name="path">Path.</param>
    public static Texture2D LoadLocalTexture(string path, TextureFormat format = TextureFormat.RGB24)
    {
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D new_tex = new Texture2D(2, 2, format, false);
            new_tex.filterMode = FilterMode.Trilinear;
            new_tex.LoadImage(bytes); 
            return new_tex;
        } 
        return null;
    }

    /// <summary>
    /// 返回纹理根据设备需要翻转的角度 
    /// </summary>
    /// <returns>The texture rotate angle.</returns>
    public static ScreenDeviceOrientation   GetTextureRotateOrientation()
    {
        int deviceOrientation = 0;
        ScreenOrientation screenOrientation = Screen.orientation;

        if (screenOrientation == ScreenOrientation.Portrait)
        {
            if (deviceOrientation == 90)
            {
                return ScreenDeviceOrientation.PortraitToLandScape; 
            }
            else if (deviceOrientation == 180)
            {
                return ScreenDeviceOrientation.PortraitToReversePortrait;
            }
            else if (deviceOrientation == 270)
            {
                return ScreenDeviceOrientation.PortraitToReverseLandScape;  
            }
        }
        else if (screenOrientation == ScreenOrientation.LandscapeLeft || screenOrientation == ScreenOrientation.LandscapeRight
                 || screenOrientation == ScreenOrientation.Landscape)
        {
            if (deviceOrientation == 0)
            {
                return ScreenDeviceOrientation.LandScapeToPortrait;
            }
            else if (deviceOrientation == 180)
            {
                return ScreenDeviceOrientation.LandScapeToReversePortrait; 
            }
        } 

        return ScreenDeviceOrientation.UnChanged;
    }

    /// <summary>
    /// 合并纹理
    /// </summary>
    /// <returns>The textures.</returns>
    /// <param name="obj">Object.</param>
    /// <param name="background">Background.</param>
    /// <param name="logo">Todraw logo.</param>
    public static void BlendTexture(Texture2D background, Texture2D logo, int startX, int startY)
    {
        if (logo != null && background != null)
        {
            for (int x = startX; x < startX + logo.width && x < background.width; x++)
            {
                for (int y = startY; y < startY + logo.height && y < background.height; y++)
                {
                    Color bgColor = background.GetPixel(x, y);
                    Color wmColor = logo.GetPixel(x - startX, y - startY);

                    Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

                    background.SetPixel(x, y, final_color);
                }
            }
            background.Apply(false);
        }
    }


    /// <summary>
    ///调整纹理至屏幕大小 
    /// </summary>
    /// <param name="tex">Tex.</param>
    public static Texture2D   ResizeTextureToScreenSize(Texture2D tex)
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        int texWidth = tex.width;
        int texHeight = tex.height;
        float texScale = (float)texWidth / (float)texHeight;

        if (texWidth != screenWidth && texHeight != screenHeight)
        {
            Texture2D screenTex = new Texture2D(screenWidth, screenHeight, tex.format, false);

            //如果是横屏显示竖屏
            if (screenWidth > screenHeight)
            { 
                ResizeTexture(tex, (int)(texScale * (float)screenHeight), screenHeight);
            }
            else  //竖屏显示横屏
            {
                ResizeTexture(tex, screenWidth, (int)((float)screenWidth / texScale));
            }

            int startX = (screenTex.width - tex.width) / 2;
            int startY = (screenTex.height - tex.height) / 2;

            for (int x = 0; x < screenTex.width; x++)
            {
                for (int y = 0; y < screenTex.height; y++)
                {
                    Color color = Color.black;
                    if (x >= startX && x < startX + tex.width && y >= startY && y < startY + tex.height)
                    {
                        color = tex.GetPixel(x - startX, y - startY);  
                    }    
                    screenTex.SetPixel(x, y, color); 
                }
            }
            screenTex.Apply(false);
    

            UnityEngine.Object.Destroy(tex);
            tex = null;
            return screenTex;
        }
        else
        {
            return tex;
        }
    }

}
