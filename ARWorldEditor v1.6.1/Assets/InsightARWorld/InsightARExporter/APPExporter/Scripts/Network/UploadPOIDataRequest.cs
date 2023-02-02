using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 上传POI数据
    /// </summary>
    public class UploadPOIDataRequest : BaseInsightRequest
    {
        public UploadPOIDataRequest(UploadPOIDataRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("contentId", reqparam.contentId);
            AddBody("poiData", reqparam.poiData);
        }

        public override string GetApi()
        {
            return "/api/editor/uploadPOIData";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(UploadPOIDataResponseData);
        }

    }
}