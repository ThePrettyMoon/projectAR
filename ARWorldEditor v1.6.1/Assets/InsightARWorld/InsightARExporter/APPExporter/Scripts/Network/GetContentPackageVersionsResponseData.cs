using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GetContentPackageVersionsResponseData : BaseResponseData
    {
        public List<ContentPackageVersionsResultData> result;
    }

    [Serializable]
    public class ContentPackageVersionsResultData
    {
        public long contentpackageId; //内容包ID
        public string version;     //内容包版本 V12
        public int versionCode;//版本数字 12
        public string sdkVersion; //适配sdk版本
        public int aduitStatus;   //审核状态，1-待审核，3-未过审
        public int deviceType;//端类型 1-手机端 3-sim眼镜端
    }
}