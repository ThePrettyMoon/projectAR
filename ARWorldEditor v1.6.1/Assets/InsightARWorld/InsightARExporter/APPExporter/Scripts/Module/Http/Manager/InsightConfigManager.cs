using ARWorldEditor;

namespace ARWorldEditor
{
    public class InsightConfigManager
    {
        protected class SpKey
        {
            public const string AR_SDK_TOKEN = "ar_sdk_token";
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

        private string mApiDomain;

        private string mSdkSecret;
        private string mToken;
        private string mDeviceId;


        private BaseSharedPreference mPreference;

        public InsightConfigManager()
        {
#if DEBUG_TEST
            mApiDomain = "http://ar-world-test.netease.com";
#else
        mApiDomain = "https://arworld.netease.com";
#endif

            mPreference = new BaseSharedPreference();
        }

        public string GetApiDomain()
        {
            return mApiDomain;
        }

        public void SetApiDomain(string domain)
        {
            mApiDomain = domain;
        }

        public string GetToken()
        {
            if (string.IsNullOrEmpty(mToken))
            {
                mToken = mPreference.Get(SpKey.AR_SDK_TOKEN);
            }
            return mToken;
        }

        public string GetSDKSecret()
        {
            if (string.IsNullOrEmpty(mSdkSecret))
            {
                mSdkSecret = mPreference.Get(SpKey.AR_SDK_SECRET);
            }
            return mSdkSecret;
        }


        public void SetToken(string token)
        {
            mToken = token;
            mPreference.Put(SpKey.AR_SDK_TOKEN, token);
        }

        public void SetSDKSecret(string sdkSecret)
        {
            mSdkSecret = sdkSecret;
            mPreference.Put(SpKey.AR_SDK_SECRET, sdkSecret);
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

    }
}
