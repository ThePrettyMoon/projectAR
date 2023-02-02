using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Insight
{

    public static class VideoPlayerExtension
    {

        public static string toString(this VideoPlayer videoPlayer)
        {
            return videoPlayer.ToString();
        }

        public static bool getEnabled(this VideoPlayer videoPlayer)
        {
            return videoPlayer.enabled;
        }
        public static void setEnabled(this VideoPlayer videoPlayer, bool enabled)
        {
            if (videoPlayer != null) videoPlayer.enabled = enabled;
        }

        public static GameObject gameObject(this VideoPlayer videoPlayer)
        {
            return videoPlayer.gameObject;
        }

        public static bool isActiveAndEnabled(this VideoPlayer videoPlayer)
        {
            return videoPlayer.gameObject.activeSelf;
        }

        public static string name(this VideoPlayer videoPlayer)
        {
            return videoPlayer.name;
        }

        public static string tag(this VideoPlayer videoPlayer)
        {
            return videoPlayer.tag;
        }

        public static Transform transform(this VideoPlayer videoPlayer)
        {
            return videoPlayer.transform;
        }

    }

}


