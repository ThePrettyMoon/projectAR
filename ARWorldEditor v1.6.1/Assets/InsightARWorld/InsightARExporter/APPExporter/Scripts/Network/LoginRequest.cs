using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 登陆接口
    /// </summary>
    public class LoginRequest : BaseInsightRequest
    {
        public LoginRequest(LoginRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("email", reqparam.email);
            AddBody("pwd", reqparam.pwd);
        }

        public override string GetApi()
        {
            return "/api/editor/login";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(LoginResponseData);
        }

    }
}

