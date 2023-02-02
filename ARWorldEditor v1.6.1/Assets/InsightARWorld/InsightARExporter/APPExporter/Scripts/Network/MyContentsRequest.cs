using RTools.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    /// 获取我的内容列表
    /// </summary>
    public class MyContentsRequest : BaseInsightRequest
    {
        public MyContentsRequest(MyContentsRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("deviceType", reqparam.deviceType);
        }

        public override string GetApi()
        {
            return "/api/editor/myContents";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(MyContentsResponseData);
        }

    }
}
