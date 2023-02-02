using System;

namespace ARWorldEditor
{
    [Serializable]
    public class UploadPOIDataRequestData 
    {
        public long contentId; //内容包id
        public string poiData; //json格式的poi文本数据，例："[{\"properties\":{\"amenity\":\"table\",\"height\":\"0.75\",\"level\":\"1\",\"name\":\"\",\"type\":\"Object\",\"id\":\"b8e5a43302007587f68993993424ca7d\"},\"coordinates\":[120.22857163765,30.24422831914],\"type\":\"Point\"}]"
    }
}


