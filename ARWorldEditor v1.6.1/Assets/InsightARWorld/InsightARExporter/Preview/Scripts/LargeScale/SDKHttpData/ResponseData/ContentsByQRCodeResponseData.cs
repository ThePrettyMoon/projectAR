using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询内容（扫描二维码）
/// </summary>
[Serializable]
public class ContentsByQRCodeResponseData
{
    public AW_ResultObjectType resultObjectType;
    public ArProduct content;
    public SubArProduct subContent;

    [JsonIgnore]
    public AW_ResultObjectType ResultObjectType
    {
        get
        {
            return resultObjectType;
        }
    }

    [JsonIgnore]
    public ArProduct Content
    {
        get
        {
            return content;
        }
    }


    [JsonIgnore]
    public SubArProduct SubContent
    {
        get
        {
            return subContent;
        }
    }
    public override string ToString()
    {
        return "ContentsByQRCodeResponseData" + " content = " + content.Cid;
    }
}
