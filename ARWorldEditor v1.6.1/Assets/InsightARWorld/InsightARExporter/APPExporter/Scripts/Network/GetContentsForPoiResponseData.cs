using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GetContentsForPoiResponseData 
    {
        public List<ContentsForPoiResponseData> result;
    }

    [Serializable]
    public class ContentsForPoiResponseData
    {
        public int id;
        public string name;
        public int ?algorithmType;
        public string algorithmName;
        public string algorithmMode;
    }
}