using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GetAllSDKVersionsResponseData : BaseResponseData
    {
        public List<AllSDKVersionsResultData> result;
    }

    [Serializable]
    public class AllSDKVersionsResultData
    {
        public int id; //主键
        public string sdkVersion; //sdk版本号
    }
}