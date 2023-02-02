using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InsightConfigManager
{
    protected class SpKey
    {
        public const string KEY_APP_KEY = "key_app_key";
        public const string KEY_APP_SECRET = "key_app_secret";

        public const string KEY_AR_SUPPORT = "key_ar_support";
        public const string KEY_TIMESTAMP_DELTA = "key_timestamp_delta";

        public const string AR_SDK_TOKEN = "ar_sdk_token";
        public const string AR_SDK_KEY = "ar_sdk_key";
        public const string AR_SDK_SECRET = "ar_sdk_secret";
        public const string AR_SDK_GROUP_ID = "ar_sdk_group_id";

        public const string SP_VERSION_NO = "sp_version_no";
        public const string KEY_SO_STATE = "key_so_state";
        public const string KEY_SO_DOWNLOAD_VERSION = "key_so_download_version";
        public const string KEY_SO_LOAD_TIME = "key_so_load_time";
        public const string KEY_CLOUD_INTERVAL_TIME = "key_cloud_interval_time";
        public const string KEY_CLOUD_REQUEST_TIME = "key_cloud_request_time";
        public const string KEY_DOWNLOAD_EVENT_ON_ALL = "key_download_event_on_all";
        public const string KEY_SHOW_WINDOW_POP_IN_CLOUD_EVENT = "key_show_window_pop_in_cloud_event";
        public const string KEY_GET_ONLINE_RESOURCE_STATE = "key_get_online_resource_state";
        public const string KEY_DOWNLOAD_PARENT_ROOT_PATH = "key_download_parent_root_path";
        public const string KEY_RESOURCE_SAVE_GROUP = "key_resource_save_group";
        public const string KEY_LOGO_SHOW = "key_logo_show";
        public const string KEY_DOWNLOADPAUSE_ONDESTROY = "key_downloadpause_ondestroy";
        public const string KEY_SHOW_DOWNLOAD_PROGRESS = "key_show_download_progress";
        public const string KEY_IMAGE_SAVE_DIR = "key_image_save_dir";
        public const string KEY_LINK_SHOW = "key_link_show";
        public const string KEY_AR_DEVICE_ID = "key_ar_device_id";
        public const string KEY_AUTHOR_INFO_SHOW = "key_author_info_show";
        public const string KEY_CERT_UPDATE_TIME = "key_cert_update_time";
        public const string KEY_OASIS_DEVICE_ID = "key_oasis_device_id";

        public const string KEY_MAP_MODE = "key_map_mode";
        public const string KEY_RESPONSIBLE_CODE = "key_responsible_code";
#if DEBUG_TEST

#if UNITY_EDITOR
        public const string APP_KEY = "AR-AUW4-YVYZRQOUNENQF-A-T ";
        public const string APP_SECRET = "vIEV6NV1MY";
        public const string BUNDLE_ID = "com.ezxr.oasis_android";
#elif UNITY_ANDROID
        public const string APP_KEY = "AR-AUW4-YVYZRQOUNENQF-A-T ";
        public const string APP_SECRET = "vIEV6NV1MY";
        public const string BUNDLE_ID = "com.ezxr.oasis_android";
#elif UNITY_IOS
        /* public const string APP_KEY = "AR-U5QX-6Y2MT43LDFI62-I-T";
         public const string APP_SECRET = "o9oXpPJmNv";*/

        /*public const string APP_KEY = "AR-RHMY-29FOCIL6AOCX7-I-T";
        public const string APP_SECRET = "t749YrwKgZ";*/

        public const string APP_KEY = "AR-HE7I-R98V7LVKVZPYO-I-T";
        public const string APP_SECRET = "75Wq1e2FT6";
        public const string BUNDLE_ID = "com.ezxr.oasis";
#endif
#else
#if UNITY_EDITOR
        public const string APP_KEY = "AR-AUW4-YVYZRQOUNENQF-A-T ";
        public const string APP_SECRET = "vIEV6NV1MY";
        public const string BUNDLE_ID = "com.ezxr.oasis_android";
#elif UNITY_ANDROID
        // 正式
        public const string APP_KEY = "NAR-PNMY-ILPKDJKNCVRFX-I-F";
        public const string APP_SECRET = "Fu6GrIpOdh";

        //测试
        //public const string APP_KEY = "NAR-XSOK-QYNVKPFE5JR5H-I-T";
        //public const string APP_SECRET = "6oKpf6ksA6";

        public const string BUNDLE_ID = "com.ezxr.oasis_android";

#elif UNITY_IOS
        // 正式
        //public const string APP_KEY = "NAR-IFWZ-UO3FDBLRYMLPR-I-F";
        //public const string APP_SECRET = "89AYRCRVch";

        //测试
        public const string APP_KEY = "NAR-NRFW-UKL3PX5KOATRE-I-T";
        public const string APP_SECRET = "HWukfX0b1j";
        public const string BUNDLE_ID = "com.ezxr.arworld";
#endif
#endif
        public const string BASIC_EVENT_ID = "3659";

    }

    private static InsightConfigManager _instance;
    public static InsightConfigManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InsightConfigManager();
            }
            return _instance;
        }
    }
    private long mDelta;
    //表明是否同步过时间戳
    private string mApiDomain;
    private string mApiDomainSDK;
    private bool mSyncTimeStamp;

    private string mSdkSecret;
    private string mSdkKey;
    private string mToken;
    private int mGroupID;
    private string mCurrentVersion;
    private string mDeviceId;

    private string mMapMode;
    private string mBundleID;
    private bool arSupport;

    private BaseSharedPreference mPreference;

    private MainContentData mMainContentData;

    private GeoLevelsData geoLevelsData;

    public InsightConfigManager()
    {
#if DEBUG_TEST
        mApiDomain = "https://gw-dongjian-test.netease.com/ar-world/aw-sdk";
#else
        //mApiDomain = "https://gw-dongjian.netease.com/ar-world/aw-sdk";
        mApiDomain = "https://gw-dongjian-test.netease.com/ar-world/aw-sdk";
#endif
        mApiDomainSDK = "/ar-world/aw-sdk";
        mPreference = new BaseSharedPreference();
        mMainContentData = new MainContentData();
        geoLevelsData = new GeoLevelsData();

        mDelta = 0;
        arSupport = false;
        mGroupID = 0;
    }

    public long GetTimetampDelta()
    {
        if (long.TryParse(mPreference.Get(SpKey.KEY_TIMESTAMP_DELTA), out mDelta)) {
            return mDelta;
        }
        return mDelta;
    }

    public void SetTimetampDelta(long delta)
    {
        mDelta = delta;
        mPreference.Put(SpKey.KEY_TIMESTAMP_DELTA, delta);
    }

    public string GetApiDomain()
    {
        return mApiDomain;
    }

    public void SetApiDomain(string domain)
    {
        
        if (string.IsNullOrEmpty(domain))
        {
            InsightDebug.Log("InsightConfigManager", "domain is null or empty");
        }
        else {
            mApiDomain = domain + mApiDomainSDK;
        }
    }

    public bool GetArSupport()
    {
        if (!arSupport) {
            if (bool.TryParse(mPreference.Get(SpKey.KEY_AR_SUPPORT), out arSupport))
            {
                return arSupport;
            }
        }
        return arSupport;
    }

    public void SetArSupport(bool support)
    {
        arSupport = support;
        mPreference.Put<bool>(SpKey.KEY_AR_SUPPORT, arSupport);
    }

    public int GetMainContentCid()
    {
        return mMainContentData.Cid;
    }

    public void SetMainContentCid(int cid)
    {
        mMainContentData.Cid = cid;
    }

    public string GetMainContentCloudUrl()
    {
        return mMainContentData.CloudRelocUrl;
    }

    public void SetMainContentCloudUrl(string url)
    {
        mMainContentData.CloudRelocUrl = url;
    }

    public bool IsSyncTimestamp()
    {
        return mSyncTimeStamp;
    }

    public void SetSyncTimestamp(bool sync)
    {
        mSyncTimeStamp = sync;
    }

    public string GetToken()
    {
        if (string.IsNullOrEmpty(mToken))
        {
            mToken = mPreference.Get(SpKey.AR_SDK_TOKEN);
        }
        return mToken;
    }

    public string GetSDKKey()
    {
        if (string.IsNullOrEmpty(mSdkKey))
        {
            mSdkKey = mPreference.Get(SpKey.AR_SDK_KEY);
        }
        return mSdkKey;
    }

    public string GetSDKSecret()
    {
        if (string.IsNullOrEmpty(mSdkSecret))
        {
            mSdkSecret = mPreference.Get(SpKey.AR_SDK_SECRET);
        }
        return mSdkSecret;
    }

    public string GetSDKBundleID()
    {
#if UNITY_EDITOR
        return SpKey.BUNDLE_ID;
#else
        if (string.IsNullOrEmpty(mBundleID))
        {
            mBundleID = mPreference.Get(SpKey.BUNDLE_ID);
        }
        return mBundleID;
#endif

    }

    public void SetToken(string token)
    {
        mToken = token;
        mPreference.Put(SpKey.AR_SDK_TOKEN, token);
    }

    public void SetSDKKey(string sdkKey)
    {
        mSdkKey = sdkKey;
        mPreference.Put(SpKey.AR_SDK_KEY, sdkKey);
    }

    public void SetSDKSecret(string sdkSecret)
    {
        mSdkSecret = sdkSecret;
        mPreference.Put(SpKey.AR_SDK_SECRET, sdkSecret);
    }

    public void SetSDKBundleID(string bundleId)
    {
        mBundleID = bundleId;
        mPreference.Put(SpKey.BUNDLE_ID, mBundleID);
    }

    public void SetGroupID(int groupID)
    {
        mGroupID = groupID;
        mPreference.Put(SpKey.AR_SDK_GROUP_ID, groupID);
    }

    public int GetGroupID()
    {

        if (mGroupID == 0) {
            if (int.TryParse(mPreference.Get(SpKey.AR_SDK_GROUP_ID), out mGroupID)) {
                return mGroupID;
            }
        } 
        return mGroupID;
    }

    public void SetCurrentVersion(string version)
    {
        mCurrentVersion = version;

        mPreference.Put(SpKey.SP_VERSION_NO, version);
    }

    public void SetDeviceId(string uuid)
    {
        mDeviceId = uuid;
        mPreference.Put(SpKey.KEY_OASIS_DEVICE_ID, uuid);
    }

    public string GetDeviceId()
    {
        return string.IsNullOrEmpty(mDeviceId) ? mPreference.Get(SpKey.KEY_OASIS_DEVICE_ID) : mDeviceId;
    }


    /// <summary>
    /// get secret
    /// </summary>
    /// <returns></returns>
    public string GetResourceGroupSecret()
    {
#if UNITY_EDITOR
        return SpKey.APP_SECRET;
#else
        return GetSDKSecret();
#endif

    }

    /// <summary>
    /// 返回key
    /// </summary>
    /// <returns></returns>
    public string GetResourceGroupKey()
    {
#if UNITY_EDITOR
        return SpKey.APP_KEY;
#else
        return GetSDKKey();
#endif

    }

    /// <summary>
    /// 获取是否同意过免责声明
    /// </summary>
    /// <param name="mode"></param>
    public void SetResponsibleCode(string code)
    {
        mPreference.Put(SpKey.KEY_RESPONSIBLE_CODE, code);
    }

    public string GetResponsibleCode()
    {
        return mPreference.Get(SpKey.KEY_RESPONSIBLE_CODE);
    }


    public void SetGeoLevelsData(GeoLevelsData levelsData) {

        geoLevelsData = levelsData;

    }

    public GeoLevelsData GetGeoLevelsData()
    {

        return geoLevelsData;

    }
}
