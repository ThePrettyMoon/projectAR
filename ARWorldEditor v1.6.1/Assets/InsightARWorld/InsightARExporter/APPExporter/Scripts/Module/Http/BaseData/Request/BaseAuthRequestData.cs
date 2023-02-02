using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取用户相关的产品列表
    /// </summary>
    [Serializable]
    public class BaseAuthRequestData : BaseRequestData
    {
        #region params

        public static BaseAuthRequestData GetBaseAuthRequestData(long time)
        {
            string token = InsightConfigManager.Instance.GetToken();
            return BaseAuthRequestData.Obtain(time, token);
        }

        public override void ResetTimeStamp(long timestamp)
        {
        }
        #endregion

        #region custom functions

        /// <summary>
        /// obtain
        /// </summary>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static BaseAuthRequestData Obtain(long t,string token)
        {
            BaseAuthRequestData baseAuthRequestData = new BaseAuthRequestData();
            string nonce = RandomUtility.Random();
            baseAuthRequestData.t = t;
            baseAuthRequestData.nonce = nonce;
            baseAuthRequestData.token = token;
            baseAuthRequestData.sign = EncodeUtility.MD5(t + "|" + nonce+ "|"+ token);
            return baseAuthRequestData;
        }
        #endregion
    }
}
