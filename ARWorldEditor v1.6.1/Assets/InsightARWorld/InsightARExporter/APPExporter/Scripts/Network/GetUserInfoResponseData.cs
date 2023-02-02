
using System;
using System.Collections.Generic;
using ARWorldEditor;

namespace ARWorldEditor
{

[Serializable]
    public class GetUserInfoResponseData : BaseResponseData
    {
        public GetUserInfoResponseDataResult result;
    }
}

public class GetUserInfoResponseDataResult
{
    public string roleCode;
}
