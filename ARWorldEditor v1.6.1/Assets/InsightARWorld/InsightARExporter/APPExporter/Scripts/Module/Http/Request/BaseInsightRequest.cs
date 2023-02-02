using System.Collections.Generic;
using ARWorldEditor;

namespace ARWorldEditor
{
    public abstract class BaseInsightRequest : BaseRequest
    {
        #region params
        private const int TIMEOUT = 10;
        #endregion

        #region custom functions

        public BaseInsightRequest(long time)
        {
            string token = InsightConfigManager.Instance.GetToken();
            long requestT = time;
            string nonce = RandomUtility.Random();
            AddBody("token", token);
            AddBody("t", requestT);
            AddBody("nonce", nonce);
            AddBody("sign", EncodeUtility.MD5(requestT + "|" + nonce+"|"+token));
        }

        /// <summary>
        /// query
        /// </summary>
        /// <param name="listener"></param>
        public override void Query(HttpRequestListener listener)
        {
            if (this is GetTimestampRequest)
            {
                base.Query(listener);
                return;
            }

            base.Query(listener);
        }

        /// <summary>
        /// super query
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="timestampDelta"></param>
        private void SuperQuery(HttpRequestListener listener, long timestampDelta)
        {
            Dictionary<string, object> bodyMap = GetBodyMap();
            long requestT = TimeUtility.GetTimeStampMilli() + timestampDelta;
            if (bodyMap != null && bodyMap.Count > 0)
            {
                object result;
                if (bodyMap.TryGetValue("reqbase", out result))
                {
                    BaseRequestData requestData = (BaseRequestData)result;
                    if (requestData != null)
                    {
                        requestData.ResetTimeStamp(requestT);
                        ResetQuery(listener, requestT);
                        return;
                    }
                }
            }
            ResetQuery(listener, requestT);
        }

        /// <summary>
        /// reset query
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="requestT"></param>
        private void ResetQuery(HttpRequestListener listener, long requestT)
        {
           /* string nonce = RandomUtility.Random();
            AddQuery("t", requestT);
            AddQuery("v", HttpUtility.VERSION_NO);
            AddQuery("nonce", nonce);
            AddQuery("sign", EncodeUtility.MD5(requestT + "|" + nonce));*/
            base.Query(listener);
        }

        /// <summary>
        /// get domain
        /// </summary>
        /// <returns></returns>
        public override string GetDomain()
        {
            return InsightConfigManager.Instance.GetApiDomain();
        }

        /// <summary>
        /// get time out
        /// </summary>
        /// <returns></returns>
        public override int GetTimeout()
        {
            return TIMEOUT;
        }
        #endregion
    }
}
