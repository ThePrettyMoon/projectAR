using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class TimestampResponseData
{
    public long timestamp;

    [JsonIgnore]
    public long Timestamp
    {
        get
        {
            return timestamp;
        }
    }
}

/// <summary>
/// response code
/// </summary>
public enum RESPONSE_CODE
{
    SUCCESS = 200,
    PARAM_ERROR_CODE = 400000,
    PARAM_NONCE_NOT_EXIST = 400001,
    PARAM_TOKEN_NOT_EXIST = 400002,
    PARAM_SIGN_NOT_EXIST = 400003,
    PARAM_TIMESTAMP_NOT_EXIST = 400004,
    REPLAY_ATTACK_TIMESTAMP = 400005,
    REPLAY_ATTACK_NONCE = 400006,
    SIGNATURE_ERROR = 400007,
    PARAM_USER_ID_NOT_EXIST = 400008,
    USER_NOT_LOGGED_IN = 400009,
    TOKEN_INVALID = 401, //token失效
    MISSION_NOT_EXIST = 410001,//任务不存在
    MISSION_STATUS_ERROR = 410002,//任务状态错误
    ACHIEVEMENT_STATUS_ERROR = 410003,//成就状态错误
    SAR_GRADE_NOT_EXISTS = 480001, //成绩不存在
    SAR_USER_NOT_EXISTS = 480002,  //用户不存在
    SERVER_ERROR = 555555,     // 服务器忙，请稍后重试
}
