using ARWorldEditor;

namespace ARWorldEditor
{
    public class AllProductsRequestData : BaseRequestData
    {
        //  public string token;
        public string signature;

        public AllProductsRequestData()
        {

        }

        public override void ResetTimeStamp(long timestamp)
        {
        }

        #region custom functions

        /// <summary>
        /// obtain
        /// </summary>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static AllProductsRequestData Obtain(long time, string token, string secret, long delta)
        {
            AllProductsRequestData baseAuthRequestData = new AllProductsRequestData();
            //  baseAuthRequestData.secret = secret;
            baseAuthRequestData.token = token;
            // baseAuthRequestData.apn = NetworkUtility.GetCurrentNetType();

            long requestT = time + delta;
            baseAuthRequestData.signature = EncodeUtility.MD5(secret + "|" + requestT);
            return baseAuthRequestData;

        }
        #endregion
    }
}
