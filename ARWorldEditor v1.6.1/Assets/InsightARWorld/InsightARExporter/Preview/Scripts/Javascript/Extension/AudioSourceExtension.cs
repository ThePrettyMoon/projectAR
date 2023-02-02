using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtension 
{
    public static string getClipName(this AudioSource audioSource)
    {
        return audioSource.clip == null ? "" : audioSource.clip.name;
    }

    public static float getDuration(this AudioSource audioSource)
    {
        return audioSource.clip == null ? 0.0f : audioSource.clip.length;
    }

    /// <summary>
    /// 空实现
    /// </summary>
    /// <param name="audioSource"></param>
    /// <returns></returns>
    public static bool getReady(this AudioSource audioSource)
    {
        return true;
    }
}
