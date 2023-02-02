using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EZXR.NET
{
    public interface OnHttpCallback 
    {
        void OnResult(string result, IHttpBaseRequest request);

        void OnError(Exception e, IHttpBaseRequest request);
    }
}
