using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取我的内容列表
    /// </summary>
    public class GetMyContentsRequest : BaseInsightRequest
    {
        public GetMyContentsRequest(GetMyContentsRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
        
        }

        public override string GetApi()
        {
            return "/api/editor/getMyContents";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetMyContentsResponseData);
        }

    }
}