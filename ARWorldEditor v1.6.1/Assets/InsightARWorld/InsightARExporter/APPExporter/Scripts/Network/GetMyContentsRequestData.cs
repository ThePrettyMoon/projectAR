using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    [Serializable]
    public class GetMyContentsRequestData : ARWorldEditor.BaseRequestData
    {
        public GetMyContentsRequestData(BaseAuthRequestData baseRequestData)
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
