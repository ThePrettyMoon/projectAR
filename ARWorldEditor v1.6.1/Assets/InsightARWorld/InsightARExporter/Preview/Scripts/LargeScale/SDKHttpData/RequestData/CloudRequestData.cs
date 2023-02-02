using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// cloud request model
/// </summary>
[Serializable]
public class CloudRequestData 
{
    public CloudData alg;

    public CloudRequestData()
    {
        alg = new CloudData();
    }
}

[Serializable]
public class CloudData
{
    public string imageEncodingData;
    public string protobufEncodingData;
}
