using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取内容上传的POI数据,虚拟poi数据
    /// </summary>
    public class GetPoiDataRequest : BaseInsightRequest
    {
        public GetPoiDataRequest(GetPoiDataRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("contentId", reqparam.contentId);
        }

        public override string GetApi()
        {
            return "/api/editor/getPoiData";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetPoiDataResponseData);
        }

    }
}