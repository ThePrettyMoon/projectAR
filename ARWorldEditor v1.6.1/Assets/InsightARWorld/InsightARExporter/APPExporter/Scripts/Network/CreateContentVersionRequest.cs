using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 登陆接口
    /// </summary>
    public class CreateContentVersionRequest : BaseInsightRequest
    {
        public CreateContentVersionRequest(CreateContentVersionRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("contentId", reqparam.contentId);
            AddBody("engineType", reqparam.engineType);
            AddBody("sdkVersionId", reqparam.sdkVersionId);
            AddBody("deviceType", reqparam.deviceType);
        }

        public override string GetApi()
        {
            return "/api/editor/createContentVersion";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(CreateContentVersionResponseData);
        }

    }
}


