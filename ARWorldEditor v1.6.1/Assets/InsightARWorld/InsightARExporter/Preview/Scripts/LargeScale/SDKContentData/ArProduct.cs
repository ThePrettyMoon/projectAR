using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

/// <summary>
/// 获取产品信息后的返回数据
/// </summary>
[Serializable]
public class ArProduct : BaseDbData
{
    private int cid;
    private string name;
    private long totalSize;
    private int orientation;
    private string coverImageUrl;
    private List<ProductMaterial> materials;
    private long sarPid;
    private long updateTime;
    private string cloudRelocUrl;
    private List<AW_Tag> tags;
    private bool navEnabled;
    private int sid;
    private long distance;
    private string province;
    private string city;
    private string district;
    private AW_GeoPosition centroid;
    private string address;

    /// <summary>
    /// 字内容专有
    /// </summary>
    private bool promptBeforeDownload;

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
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
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
    [JsonIgnore]
    public long TotalSize
    {
        get
        {
            return totalSize;
        }
        set
        {
            totalSize = value;
        }
    }
    [JsonIgnore]
    public int Orientation
    {
        get
        {
            return orientation;
        }
        set
        {
            orientation = value;
        }
    }
    [JsonIgnore]
    public string CoverImageUrl
    {
        get
        {
            return coverImageUrl;
        }
        set
        {
            coverImageUrl = value;
        }
    }
    [JsonIgnore]
    public List<ProductMaterial> ProductMaterials
    {
        get
        {
            return materials;
        }
        set
        {
            materials = value;
        }
    }
    [JsonIgnore]
    public long SarPid
    {
        get
        {
            return sarPid;
        }
        set
        {
            sarPid = value;
        }
    }
    [JsonIgnore]
    public long UpdateTime
    {
        get
        {
            return updateTime;
        }
        set
        {
            updateTime = value;
        }
    }
    [JsonIgnore]
    public List<AW_Tag> Tags
    {
        get
        {
            return tags;
        }
        set
        {
            tags = value;
        }
    }
    [JsonIgnore]
    public bool NavEnabled
    {
        get
        {
            return navEnabled;
        }
        set
        {
            navEnabled = value;
        }
    }
    [JsonIgnore]
    public int Sid
    {
        get
        {
            return sid;
        }
        set
        {
            sid = value;
        }
    }
    [JsonIgnore]
    public long Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
        }
    }
    [JsonIgnore]
    public string Province
    {
        get
        {
            return province;
        }
        set
        {
            province = value;
        }
    }
    [JsonIgnore]
    public string City
    {
        get
        {
            return city;
        }
        set
        {
            city = value;
        }
    }
    [JsonIgnore]
    public string District
    {
        get
        {
            return district;
        }
        set
        {
            district = value;
        }
    }
    [JsonIgnore]
    public AW_GeoPosition Centroid
    {
        get
        {
            return centroid;
        }
        set
        {
            centroid = value;
        }
    }
    
    [JsonIgnore]
    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            address = value;
        }
    }

    [JsonIgnore]
    public bool PromptBeforeDownload
    {
        get
        {
            return promptBeforeDownload;
        }
        set
        {
            promptBeforeDownload = value;
        }
    }
    public ArProduct Clone()
    {
        ArProduct product = new ArProduct();
        product.Cid = cid;
        product.Name = name;
        product.TotalSize = totalSize;
        product.Orientation = orientation;
        product.CoverImageUrl = coverImageUrl;
        product.SarPid = sarPid;
        product.UpdateTime = updateTime;
        product.CloudRelocUrl = cloudRelocUrl;
        product.NavEnabled = navEnabled;
        product.Sid = sid;
        product.Distance = distance;
        product.City = city;
        product.Province = province;
        product.District = district;
        product.Centroid = centroid;
        product.Address = address;
        //List<ProductMaterial> materials = product.ProductMaterials;
        if (materials != null && materials.Count > 0)
        {
            List<ProductMaterial> list = new List<ProductMaterial>();
            for (int i = 0; i < materials.Count; i++)
            {
                ProductMaterial material = materials[i].Clone();
                list.Add(material);
            }
            product.ProductMaterials = list;
        }

        if (tags != null && tags.Count > 0)
        {
            List<AW_Tag> list = new List<AW_Tag>();
            list.AddRange(tags);
            product.Tags = list;
        }

        return product;
    }

}


