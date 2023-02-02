
using System;
using System.Collections.Generic;
using ARWorldEditor;

namespace ARWorldEditor
{

[Serializable]
    public class CreateContentVersionResponseData : BaseResponseData
    {
        public List<CreateContentVersionResonseResult> result;
    }
}

public class CreateContentVersionResonseResult
{
    public int contentpackageId;
    public string version;
    public string sdkVersion;
    public int aduitStatus;
}
