using System;
using Newtonsoft.Json;

namespace ARWorldEditor
{
    [Serializable]
    public abstract class BaseResponseData 
    {
        public string code;
        public string msg;

        [JsonIgnore]
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        [JsonIgnore]
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
            }
        }
    }
}
