using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.NET
{
    public abstract class BaseRequest
    {
        /// <summary>
        ///  当前请求的类型
        /// </summary>
        /// <returns></returns>
        public abstract string GetMethod();

        /// <summary>
        /// 当前请求的路径，注意是相对路径
        /// </summary>
        /// <returns></returns>
        public abstract string GetApi();

        /// <summary>
        /// 域名
        /// </summary>
        /// <returns></returns>
        public abstract string GetDomain();

        /// <summary>
        /// 重连时间
        /// </summary>
        /// <returns></returns>
        public abstract int GetTimeout();

        /// <summary>
        /// 返回当前数据类型，通过json转化
        /// </summary>
        public abstract System.Type GetModel();

        /// <summary>
        /// get 请求，添加查询参数
        /// </summary>
        private Dictionary<string, object> queryMap;

        /// <summary>
        /// post 请求的body体，get请求无效
        /// </summary>
        private Dictionary<string, object> bodyMap;

        private Dictionary<string, string> headMap;

        public void AddHead(string key, string value)
        {
            if (headMap == null)
            {
                headMap = new Dictionary<string, string>();
            }
            string outValue;
            if (!headMap.TryGetValue(key, out outValue))
            {
                headMap.Add(key, value);
            }
        }

        public void AddQuery(string key, object value)
        {
            if (queryMap == null)
            {
                queryMap = new Dictionary<string, object>();
            }
            object outValue;
            if (!queryMap.TryGetValue(key, out outValue))
            {
                queryMap.Add(key, value);
            }
        }

        public void AddBody(string key, object value)
        {
            if (bodyMap == null)
            {
                bodyMap = new Dictionary<string, object>();
            }

            object outValue;
            if (!bodyMap.TryGetValue(key, out outValue))
            {
                bodyMap.Add(key, value);
            }
        }

        public Dictionary<string, object> GetBodyMap()
        {
            return bodyMap;
        }

        public Dictionary<string, object> GetQueryMap()
        {
            return queryMap;
        }

        public Dictionary<string, string> GetHeadMap()
        {
            return headMap;
        }

        public virtual void Query(HttpRequestListener listener)
        {
            HttpRequestWrap wrap = new HttpRequestWrap(this, listener);
#if UNITY_EDITOR
            HttpManager.Instance.Post(this, wrap);
#else
            LsHttpNetWorkWithNative.Instance.lsNetworkRequest(this, listener);
#endif

        }
    }
}
