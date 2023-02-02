namespace ARWorldEditor
{
    public class ServerResponseCode 
    {
        //成功
        public const string RESPONSE_OK = "00000000";
        //会话失效
        public const string RESPONSE_SESSION_INVALIDATION = "00000003";
        //登陆失败一般是密码错误
        public const string RESPONSE_LOGIN_FAIL = "00000005";
    }
}
