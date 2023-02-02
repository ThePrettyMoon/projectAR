using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// DeviceFeature, check AR Support
/// </summary>
[Serializable]
public class DeviceFeatureResponseData
{
    public bool arSupport;

    [JsonIgnore]
    public bool ArSupport
    {
        get
        {
            return arSupport;
        }
    }

    public override string ToString()
    {
        return "DeviceFeatureResponseData" + " arSupport = " + arSupport;
    }
}
