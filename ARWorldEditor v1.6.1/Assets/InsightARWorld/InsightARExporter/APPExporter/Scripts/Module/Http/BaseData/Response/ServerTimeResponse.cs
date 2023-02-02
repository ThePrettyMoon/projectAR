using System;
using Newtonsoft.Json;

namespace ARWorldEditor
{
    /// <summary>
    /// 服务端时间戳返回数据
    /// </summary>
    [Serializable]
    public class ServerTimeResponse 
    {
        public long t;

        [JsonIgnore]
        public long Time
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        }
    }
}
