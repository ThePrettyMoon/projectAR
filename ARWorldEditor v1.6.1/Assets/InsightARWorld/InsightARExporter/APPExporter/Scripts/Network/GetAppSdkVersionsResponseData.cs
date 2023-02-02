
using System;
using System.Collections.Generic;
using ARWorldEditor;

namespace ARWorldEditor
{

[Serializable]
    public class GetAppSdkVersionsResponseData : BaseResponseData
    {
        public  List<GetAppSdkVersionsResonseResult> result;
    }
}

public class GetAppSdkVersionsResonseResult
{
    public int id;
    public string appId;
    public int sdkVersionId;
    public string version;
    //public long gmtCreate;
    //public string gmtModified;
}
