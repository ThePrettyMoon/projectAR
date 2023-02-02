using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    ///  存储数据
    /// </summary>
    public class BaseSharedPreference 
    {
        public void Put<T>(string key, T value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }

        public void Put(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public void Put(string key,int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public string Get(string key)
        {
            return PlayerPrefs.GetString(key);
        }
    }
}
