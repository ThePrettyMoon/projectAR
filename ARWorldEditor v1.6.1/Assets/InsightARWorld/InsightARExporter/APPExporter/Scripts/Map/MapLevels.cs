using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    /// 对应levels文件解析
    /// </summary>
    [Serializable]
    public class MapLevels 
    {
        public int min;
        public int max;
        public List<int> missing;
        public List<LevelInfo> levels;
    }

    [Serializable]
    public class LevelInfo
    {
        [JsonProperty(PropertyName = "ref")]
        public int reference;
        public string name;
        public float height;
    }
}