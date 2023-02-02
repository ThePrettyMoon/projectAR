using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    [Serializable]
    public class GetContentPackageVersionsRequestData: ARWorldEditor.BaseRequestData
    {
        public int contentId;  //内容ID

        public GetContentPackageVersionsRequestData(BaseAuthRequestData baseRequestData)
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
