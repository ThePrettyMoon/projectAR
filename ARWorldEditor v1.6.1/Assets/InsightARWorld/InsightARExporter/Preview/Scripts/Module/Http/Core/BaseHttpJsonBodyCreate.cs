using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EZXR.NET
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

        public override string strHeaders()
        {
            return GenerateJsonHeader();
        }

        public override int Timeout()
        {
            return GetTimeout();
        }

        public override int MaxConnectionCount()
        {
            return GetMaxConnectionCount();
        }

        public override string Method()
        {

            return "";
        }
    }
}
