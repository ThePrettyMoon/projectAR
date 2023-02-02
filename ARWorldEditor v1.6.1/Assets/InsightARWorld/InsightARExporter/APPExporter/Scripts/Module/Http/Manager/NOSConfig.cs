using UnityEngine;

namespace ARWorldEditor
{
    public class NOSConfig
    {
        public const string HOST_NAME = "ar-scene-source.nosdn.127.net";// EndPoint
        public const string END_POINT = "nos.netease.com";
        public const string ACCESS_KEY = "742041648f6941a19eea2ef76c1f2fea";
        public const string SECRET_KEY = "0dd4a7f6ae8946ae86073fe258d0206f";

        private static string sNosToken;
        private static string sNosObject;
        private static string sNosBucket;
        public const string NOS_TOKEN_KEY = "NOS_TOKEN_KEY";
        public const string NOS_OBJECT_KEY = "NOS_OBJECT_KEY";
        public const string NOS_BUCKET_KEY = "NOS_BUCKET_KEY";

        public static string GetNosToken()
        {
            if (string.IsNullOrEmpty(sNosToken))
            {
                sNosToken =  PlayerPrefs.GetString(NOS_TOKEN_KEY);
            }
            return sNosToken;
        }

        public static void SetNosToken(string token)
        {
            sNosToken = token;
            PlayerPrefs.SetString(NOS_TOKEN_KEY, token);
        }

        public static string GetNosObject()
        {
            if (string.IsNullOrEmpty(sNosObject))
            {
                sNosObject = PlayerPrefs.GetString(NOS_OBJECT_KEY);
            }
            return sNosObject;
        }

        public static void SetNosObject(string nosObject)
        {
            sNosObject = nosObject;
            PlayerPrefs.SetString(NOS_OBJECT_KEY, sNosObject);
        }

        public static string GetNosBucket()
        {
            if (string.IsNullOrEmpty(sNosBucket))
            {
                sNosBucket = PlayerPrefs.GetString(NOS_BUCKET_KEY);
            }
            return sNosBucket;
        }

        public static void SetNosBucket(string nosBucket)
        {
            sNosBucket = nosBucket;
            PlayerPrefs.SetString(NOS_BUCKET_KEY, sNosBucket);
        }
    }
}
