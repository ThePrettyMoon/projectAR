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
    public class GetAppSdkVersionsRequest : BaseInsightRequest
    {
        public GetAppSdkVersionsRequest(GetAppSdkVersionsRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
            AddBody("appId", reqparam.appId);
            AddBody("deviceType", reqparam.deviceType);
        }

        public override string GetApi()
        {
            return "/api/editor/getAppSdkVersions";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetAppSdkVersionsResponseData);
        }

    }
}


