using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MainContentData
{
    private int cid;
    public string name;
    public long totalSize;
    public int orientation;
    public string coverImageUrl;
    public List<ProductMaterial> materials;
    public int sarPid;
    public long updateTime;
    public string cloudRelocUrl;
    public List<AW_Tag> tags;
    public bool navEnabled;
    public int sid;
    public long distance;
    public string province;
    public string city;
    public string district;
    public AW_GeoPosition centroid;
    public string address;

    [JsonIgnore]
    public int Cid
    {
        get
        {
            return cid;
        }
        set
        {
            cid = value;
        }
    }
    [JsonIgnore]
    public string CloudRelocUrl
    {
        get
        {
            return cloudRelocUrl;
        }
        set
        {
            cloudRelocUrl = value;
        }
    }
}
