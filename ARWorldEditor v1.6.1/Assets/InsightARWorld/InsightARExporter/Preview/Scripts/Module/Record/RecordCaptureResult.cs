using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 录屏结果回调
/// </summary>
public enum InsightARCaptureRecordResult
{
    IARRecordNotInited = -1,
    IARRecordInitSuccess = 0,
    IARRecordInitFail = 1,
    IARRecordError = 2,
    IARRecordSaveSuccess = 3,
    IARRecordStart = 4,
    InsightARRecordAuthorizationStatusDenied = 5,
    InsightARRecordDonotStart = 6,
};
