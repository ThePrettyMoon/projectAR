using System.Collections;
using System;
using Newtonsoft.Json;

namespace ARWorldEditor
{
    public class CreateContentVersionRequestData
    {
        [JsonIgnore]
        public long contentId { get; set; }
        [JsonIgnore]
        public int sdkVersionId { get; set; }
        [JsonIgnore]
        public int engineType { get; set; }
        [JsonIgnore]
        public int deviceType { get; set; }
    }
}