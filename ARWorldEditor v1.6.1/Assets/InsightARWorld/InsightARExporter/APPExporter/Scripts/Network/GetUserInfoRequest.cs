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
    public class GetUserInfoRequest : BaseInsightRequest
    {
        public GetUserInfoRequest(GetUserInfoRequestData reqparam) : base(TimeUtility.GetTimeStampMilli())
        {
        }

        public override string GetApi()
        {
            return "/api/editor/getUserInfo";
        }

        public override string GetMethod()
        {
            return HttpMethod.POST;
        }

        public override Type GetModel()
        {
            return typeof(GetUserInfoResponseData);
        }

    }
}


