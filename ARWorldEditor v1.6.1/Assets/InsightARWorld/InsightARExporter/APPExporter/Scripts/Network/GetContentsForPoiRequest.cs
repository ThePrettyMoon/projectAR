using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取能操作的虚拟poi内容列表
    /// </summary>
    public class GetContentsForPoiRequest : BaseInsightRequest
    {
        public GetContentsForPoiRequest(GetContentsForPoiRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("contentId", reqparam.contentId);
            AddBody("engineType", reqparam.engineType);
        }

        public override string GetApi()
        {
            return "/api/editor/getContentsForPoi";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetContentsForPoiResponseData);
        }

    }
}