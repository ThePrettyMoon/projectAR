using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 登出
    /// </summary>
    public class LogoutRequest : BaseInsightRequest
    {
        public LogoutRequest(LogoutRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
  
        }

        public override string GetApi()
        {
            return "/api/editor/logout";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(LogoutResponseData);
        }

    }
}


