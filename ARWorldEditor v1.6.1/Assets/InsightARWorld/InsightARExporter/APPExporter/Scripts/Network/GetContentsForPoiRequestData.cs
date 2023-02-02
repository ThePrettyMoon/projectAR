using System;

namespace ARWorldEditor
{
    [Serializable]
    public class GetContentsForPoiRequestData 
    {
        // 1 native,2 unity
        public int engineType;
        //所属应用id => 改为内容id
        public long contentId;
    }
}
