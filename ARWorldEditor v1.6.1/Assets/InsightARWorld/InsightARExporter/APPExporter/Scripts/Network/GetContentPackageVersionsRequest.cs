using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取某个内容包下面的内容包版本信息
    /// </summary>
    public class GetContentPackageVersionsRequest : BaseInsightRequest
    {
        public GetContentPackageVersionsRequest(GetContentPackageVersionsRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("contentId", reqparam.contentId);
        }

        public override string GetApi()
        {
            return "/api/editor/getContentPackageVersions";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetContentPackageVersionsResponseData);
        }

    }
}