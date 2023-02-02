using System;
using Newtonsoft.Json;

namespace ARWorldEditor
{
    [Serializable]
    public abstract class BaseRequestData
    {
        public string token;
        public long t;
        public string nonce; //随机数
        public string sign; // MD5(t+"|"+nonce+"|"+token)



        [JsonIgnore]
        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }

        [JsonIgnore]
        public long Timestamp
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

        [JsonIgnore]
        public string Nonce
        {
            get
            {
                return nonce;
            }
            set
            {
                nonce = value;
            }
        }

        [JsonIgnore]
        public string Sign
        {
            get
            {
                return sign;
            }
            set
            {
                sign = value;
            }
        }

        public abstract void ResetTimeStamp(long timestamp);
    }
}
