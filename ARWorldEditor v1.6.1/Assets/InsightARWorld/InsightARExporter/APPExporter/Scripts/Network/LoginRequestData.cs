using System;
using Newtonsoft.Json;

namespace ARWorldEditor
{
    [Serializable]
    public class LoginRequestData 
    {
        public string email; //用户邮箱
        public string pwd; //邮箱密码md5值，全部小写

        [JsonIgnore]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        [JsonIgnore]
        public string Pwd
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;
            }
        }
    }
}
