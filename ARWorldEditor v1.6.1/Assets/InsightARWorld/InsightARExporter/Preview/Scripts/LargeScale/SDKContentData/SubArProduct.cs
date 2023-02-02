using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubArProduct
{
    public int cid;
    public int sid;
    public string name;
    public long totalSize;
    public int orientation;
    public string coverImageUrl;

    public int sarPid;
    public long updateTime;
    public bool promptBeforeDownload;
    public List<ProductMaterial> materials;

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
    public List<ProductMaterial> Materials
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
    public int SarPid
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

    public static ArProduct Invert(SubArProduct subArProduct)
    {
        ArProduct product = new ArProduct();
        product.Cid = subArProduct.Cid;
        product.Sid = subArProduct.Sid;
        product.Name = subArProduct.Name;
        product.TotalSize = subArProduct.TotalSize;
        product.Orientation = subArProduct.Orientation;
        product.CoverImageUrl = subArProduct.CoverImageUrl;
        product.SarPid = subArProduct.SarPid;
        product.UpdateTime = subArProduct.UpdateTime;
        product.PromptBeforeDownload = subArProduct.PromptBeforeDownload;
        if (subArProduct.Materials != null && subArProduct.Materials.Count > 0)
        {
            List<ProductMaterial> list = new List<ProductMaterial>();
            for (int i = 0; i < subArProduct.Materials.Count; i++)
            {
                ProductMaterial material = new ProductMaterial
                {
                    Mid = subArProduct.Materials[i].Mid,
                    Type = subArProduct.Materials[i].Type,
                    Size = subArProduct.Materials[i].Size,
                    Url = subArProduct.Materials[i].Url,
                    Md5 = subArProduct.Materials[i].Md5,
                    Path = subArProduct.Materials[i].Path,
                };
                //Debug.Log("ProductMaterials URL: " + material.Url);
                list.Add(material);
            }
            product.ProductMaterials = list;
            
        }
        return product;
    }

    public static string ToString(SubArProduct subArProduct) {
        return JsonUtil.Serialize(subArProduct);
    }
}
