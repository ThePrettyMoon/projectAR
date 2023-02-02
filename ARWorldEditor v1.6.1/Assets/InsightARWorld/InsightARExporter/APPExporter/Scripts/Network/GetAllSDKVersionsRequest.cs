using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取所有sdk版本信息，已废弃
    /// </summary>
    public class GetAllSDKVersionsRequest : BaseInsightRequest
    {
        public GetAllSDKVersionsRequest(GetAllSDKVersionsRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
        }

        public override string GetApi()
        {
            return "/api/editor/getAllSDKVersions";
        }

        public override string GetMethod()
        {
            return HttpMethod.GET;
        }

        public override Type GetModel()
        {
            return typeof(GetAllSDKVersionsResponseData);
        }

    }
}