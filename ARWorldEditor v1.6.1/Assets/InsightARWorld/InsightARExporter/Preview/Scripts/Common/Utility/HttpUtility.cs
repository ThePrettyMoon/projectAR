using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HttpUtility
{
    public const string VERSION_NO = BuildConfig.VERSION_NAME;
    public const string PROTOCOL_NO = BuildConfig.PROTOCOL_NO;

#if DEBUG_TEST
#if UNITY_EDITOR
    public const string OPERATESYSTEM_ID = "Editor";
    public const string BUNDLE_ID = "com.ezxr.xixiar";
#elif UNITY_ANDROID
        /* public const string OPERATESYSTEM_ID = "android";
         public const string BUNDLE_ID = "com.ezxr.oasis_android";*/
        public const string OPERATESYSTEM_ID = "android";
        public const string BUNDLE_ID = "com.ezxr.xixiar";
#elif UNITY_IOS
        /*  public const string OPERATESYSTEM_ID = "ios";
            public const string BUNDLE_ID = "com.ezxr.oasis";*/
        public const string OPERATESYSTEM_ID = "ios";
        public const string BUNDLE_ID = "com.ezxr.xixiar";
#endif

#else

#if UNITY_EDITOR
    public const string OPERATESYSTEM_ID = "Editor";
    public const string BUNDLE_ID = "com.ezxr.xixiar";
#elif UNITY_IOS
        public const string OPERATESYSTEM_ID = "ios";
        public const string BUNDLE_ID = "com.wz.ionline";
#elif UNITY_ANDROID
    public const string OPERATESYSTEM_ID = "android";
        public const string BUNDLE_ID = "com.wz.aonline";
#endif

#endif


}

