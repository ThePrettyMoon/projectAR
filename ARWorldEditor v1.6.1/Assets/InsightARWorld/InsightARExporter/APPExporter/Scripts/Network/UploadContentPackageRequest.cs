using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 上传内容包
    /// </summary>
    public class UploadContentPackageRequest : BaseInsightRequest
    {
        public UploadContentPackageRequest(UploadContentPackageRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("contentId", reqparam.contentId);
            AddBody("contentPackageId", reqparam.contentPackageId);
            AddBody("updateDes", reqparam.updateDes);
            AddBody("resourceList", reqparam.resourceList);
        }

        public override string GetApi()
        {
            return "/api/editor/uploadContentPackage";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(UploadContentPackageResponseData);
        }

    }
}