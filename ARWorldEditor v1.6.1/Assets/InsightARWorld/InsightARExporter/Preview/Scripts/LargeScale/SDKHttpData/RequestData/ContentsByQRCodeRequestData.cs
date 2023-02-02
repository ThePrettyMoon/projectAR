using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 查询内容（扫描二维码）
/// </summary>
[Serializable]
public class ContentsByQRCodeRequestData
{
    private string qrCode;
    private AW_ClientInfo client;

    public ContentsByQRCodeRequestData(string _qrCode, AW_ClientInfo clientInfo)
    {
        qrCode = _qrCode;
        client = clientInfo;
    }

    [JsonIgnore]
    public string QrCode
    {
        get
        {
            return qrCode;
        }
        set
        {
            qrCode = value;
        }
    }

    [JsonIgnore]
    public AW_ClientInfo Client
    {
        get
        {
            return client;
        }
        set
        {
            client = value;
        }
    }
}
