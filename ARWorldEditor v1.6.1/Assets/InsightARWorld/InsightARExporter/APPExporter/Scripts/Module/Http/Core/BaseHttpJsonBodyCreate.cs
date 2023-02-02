using System.Collections.Generic;

namespace ARWorldEditor
{
    /// <summary>
    /// 请求基类，抽象数据产生
    /// </summary>
    public class BaseHttpJsonBodyCreate : BaseHttpCreate
    {
        private const string TAG = "HttpRequest";
        public BaseHttpJsonBodyCreate(BaseRequest request) : base(request) { }

        public override string Url()
        {
           return GenerateUrl();
        }

        public override string Body()
        {
            return GenerateJsonBody();
        }

        public override string ContentType()
        {
            return "application/json; charset=utf-8";
        }

        public override Dictionary<string, string> Headers()
        {
            return GetHeaders();
        }

        public override int Timeout()
        {
            return GetTimeout();
        }

        public override int MaxConnectionCount()
        {
            return GetMaxConnectionCount();
        }
    }
}
