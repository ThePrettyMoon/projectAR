using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取地图资源
    /// </summary>
    public class GetMapResourcesRequest : BaseInsightRequest
    {
        public GetMapResourcesRequest(GetMapResourcesRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("mapId", reqparam.mapId);
        }

        public override string GetApi()
        {
            return "/api/editor/getMapResources";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetMapResourcesResponseData);
        }

    }
}