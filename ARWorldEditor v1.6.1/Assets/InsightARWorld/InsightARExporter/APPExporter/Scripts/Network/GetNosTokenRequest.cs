using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取Nos上传Token 配置
    /// </summary>
    public class GetNosTokenRequest : BaseInsightRequest
    {
        public GetNosTokenRequest(GetNosTokenRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {

        }

        public override string GetApi()
        {
            return "/api/editor/getNosToken";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetNosTokenResponseData);
        }

    }
}