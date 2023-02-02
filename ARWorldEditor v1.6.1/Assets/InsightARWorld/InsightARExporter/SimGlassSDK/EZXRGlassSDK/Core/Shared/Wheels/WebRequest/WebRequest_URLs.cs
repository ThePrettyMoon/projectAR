using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* 在此代码页面存放所有的url以及自定义的url获取方法 */
namespace Wheels.Unity
{
    public partial class UnityWebRequest :MonoBehaviour
    {
        //    public class KeyValuePair
        //    {
        //        string key;
        //        string value;
        //
        //        public KeyValuePair(string _key, string _value)
        //        {
        //            key = _key;
        //            value = _value;
        //        }
        //    }
        public Dictionary<string ,string> urlDic = new Dictionary<string, string>()
        {
//        { "getProductCategories","https://ar.hz.netease.com/furnishing/getProductCategories.do" },
//        { "getProducts","https://ar.hz.netease.com/furnishing/getProducts.do" },
            { "getProductCategories","https://mmtest.hz.netease.com/yqm11_8282/furnishing/getProductCategories.do" },
            { "getProducts","https://mmtest.hz.netease.com/yqm11_8282/furnishing/getProducts.do" },
            { "productCollect","https://mmtest.hz.netease.com/yqm11_8282/furnishing/productCollect.do" }
        };


        public string GetURL(string urlKey)
        {
            if (urlDic.ContainsKey(urlKey))
            {
                string url = urlDic[urlKey] + "?t=" + Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds).ToString() + "&v=1.0";
                Debug.Log("GetURL：" + url);
                return url;
            }
            else
            {
                return "";//如果urlDic中没有指定urlKey的键值则返回空字符串
            }
        }
    }
}