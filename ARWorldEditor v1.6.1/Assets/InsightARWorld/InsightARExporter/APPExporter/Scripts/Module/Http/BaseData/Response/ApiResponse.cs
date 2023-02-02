using System;

namespace ARWorldEditor
{
    [Serializable]
    public class ApiResponse 
    {
        public object result;
        public string code;
        public string msg;


        public string Code()
        {
            return code;
        }

        public string Message()
        {
            return msg;
        }

    }
}
