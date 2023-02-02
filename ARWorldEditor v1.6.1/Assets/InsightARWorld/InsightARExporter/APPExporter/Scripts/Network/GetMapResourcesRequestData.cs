using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    [Serializable]
    public class GetMapResourcesRequestData : ARWorldEditor.BaseRequestData
    {
        public long mapId;

        public GetMapResourcesRequestData(BaseAuthRequestData baseRequestData)
        {
            this.sign = baseRequestData.sign;
            this.nonce = baseRequestData.nonce;
            this.t = baseRequestData.t;
            this.token = baseRequestData.token;
        }

        public override void ResetTimeStamp(long timestamp)
        {
        }
    }
}
