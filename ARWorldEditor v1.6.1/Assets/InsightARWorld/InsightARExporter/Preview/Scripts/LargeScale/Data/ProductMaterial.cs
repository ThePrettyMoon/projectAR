using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class ProductMaterial : BaseDbData
{
    public int mid;
    public int type;
    public long size;
    public string url;
    public string md5;
    public string path;

    [JsonIgnore]
    public long Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
        }
    }


    [JsonIgnore]
    public string Url
    {
        get
        {
            return url;
        }
        set
        {
            url = value;
        }
    }

    [JsonIgnore]
    public int Mid
    {
        get
        {
            return mid;
        }
        set
        {
            mid = value;
        }
    }

    [JsonIgnore]
    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    [JsonIgnore]
    public string Md5
    {
        get
        {
            return md5;
        }
        set
        {
            md5 = value;
        }
    }

    [JsonIgnore]
    public string Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }
    public ProductMaterial Clone()
    {
        ProductMaterial material = new ProductMaterial
        {
            Mid = mid,
            Type = type,
            Url = url,
            Size = size,
            Md5 = md5
        };
        return material;
    }
}
