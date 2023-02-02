using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 截屏
/// </summary>
public static class CaptureUtility 
{
    /// <summary>
    /// 截屏
    /// </summary>
    public static string TakePhoto()
    {
#if UNITY_EDITOR
        string directory = Path.Combine(Application.streamingAssetsPath, "Images");
#else
        string directory = Path.Combine(Application.persistentDataPath, "Images");
#endif      
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        string fileName = TimeUtility.GetTimeString() + ".png";

        string filePath = Path.Combine(directory, fileName);
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(width, height, 24);
        RenderTexture.active = rt;

        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.gameObject.tag == "MainCamera")
            {
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = null;
            }
        }

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        tex.Apply(false);

        RenderTexture.active = null;
        GameObject.Destroy(rt);

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(filePath,bytes);
        return filePath;
    }

    public static string TakePhoto(string cameraName)
    {
#if UNITY_EDITOR
        string directory = Path.Combine(Application.streamingAssetsPath, "Images");
#else
        string directory = Path.Combine(Application.persistentDataPath, "Images");
#endif      
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        string fileName = TimeUtility.GetTimeString() + ".png";

        string filePath = Path.Combine(directory, fileName);
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(width, height, 24);
        RenderTexture.active = rt;

        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.gameObject.name == cameraName)
            {
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = null;
            }
        }

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        tex.Apply(false);

        RenderTexture.active = null;
        GameObject.Destroy(rt);

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        return filePath;
    }

    public static void TakePhoto(string cameraName, string rawImageName )
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(width, height, 24);
        RenderTexture.active = rt;

        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.gameObject.name == cameraName)
            {
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = null;
            }
        }

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        tex.Apply(false);

        RenderTexture.active = null;
        GameObject.Destroy(rt);

        GameObject raw = GameObject.Find(rawImageName);
        RawImage rawImage = null;
        if (raw)
            rawImage = raw.GetComponent<RawImage>();
        if(rawImage)
            rawImage.texture = tex;
    }
}
