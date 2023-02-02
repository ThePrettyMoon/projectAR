using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EZXR.NET
{
    public abstract class BaseInsightRequest : BaseRequest
    {
        #region params
        private bool needSyncTimestamp;
        private const int TIMEOUT = 10;
        #endregion

        #region custom functions
        public BaseInsightRequest(bool isNeedSyncTimestamp)
        {
            needSyncTimestamp = isNeedSyncTimestamp;
        }

        /// <summary>
        /// query
        /// </summary>
        /// <param name="listener"></param>
        public override void Query(HttpRequestListener listener)
        {
            //if (this is GetTimestampRequest)
            //{
            //    base.Query(listener);
            //    return;
            //}

            //bool isConfigSyncTimeStamp = InsightConfigManager.Instance.IsSyncTimestamp();
            //if (isConfigSyncTimeStamp || !needSyncTimestamp)
            //{
                base.Query(listener);
            //}
            //else
            //{
            //    SyncTimeStamp(listener);
            //}
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
