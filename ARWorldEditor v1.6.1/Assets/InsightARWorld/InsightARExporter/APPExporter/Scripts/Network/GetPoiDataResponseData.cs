using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class GetPoiDataResponseData 
    {
        public PoiDataResponseData result;
    }

    [Serializable]
    public class PoiDataResponseData
    {
        public string poiData;
    }
}