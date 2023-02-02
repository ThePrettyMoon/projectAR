using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    public class GetTimestampRequest : BaseInsightRequest
    {
        public GetTimestampRequest(long time) : base(time) { }

        public override string GetApi()
        {
            return "/ar/media/getTimestamp.do";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(ServerTimeResponse);
        }
    }
}
