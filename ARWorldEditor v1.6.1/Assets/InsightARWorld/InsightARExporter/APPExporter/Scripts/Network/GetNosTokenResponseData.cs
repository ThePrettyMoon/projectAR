using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GetNosTokenResponseData : BaseResponseData
    {
        public NosTokenResultData result;
    }

    [Serializable]
    public class NosTokenResultData
    {
        public string bucket; //nos桶

        [JsonProperty(PropertyName = "object")]
        public string nos_obj; //
        public string token; //nos token
    }
}