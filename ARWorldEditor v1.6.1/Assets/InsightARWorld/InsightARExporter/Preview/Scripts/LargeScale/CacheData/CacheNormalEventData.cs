using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通事件
/// </summary>
public class CacheNormalEventData : CacheBaseData
{

    public int cid;
    public string name;
    public long totalSize;
    public int orientation;
    public string coverImageUrl;
    public string materials;
    public long sarPid;
    public long updateTime;
    public string cloudRelocUrl;
    public string tags;
    public bool navEnabled;
    public int sid;
    public long distance;
    public string province;
    public string city;
    public string district;
    public AW_GeoPosition centroid;
    public string address;

    //pid
    public ProductState state;
    public string downloadPath;
    public string unzipPath;
    public int downloadProgress;
    public DownloadState downloadState;
    public UnZipState unZipState;

    /// <summary>
    /// obtain values
    /// </summary>
    /// <param name="arProduct"></param>
   public void ObtainContentValues(ArProduct arProduct)
    {
       
        // key 
        this.cid = arProduct.Cid;
        this.name = arProduct.Name;
        this.totalSize = arProduct.TotalSize;
        this.orientation = arProduct.Orientation;
        this.coverImageUrl = arProduct.CoverImageUrl;
        this.sarPid = arProduct.SarPid;
        this.updateTime = arProduct.UpdateTime;
        this.cloudRelocUrl = arProduct.CloudRelocUrl;
        this.navEnabled = arProduct.NavEnabled;
        this.sid = arProduct.Sid;
        this.materials = JsonUtil.Serialize(arProduct.ProductMaterials);
        this.tags = JsonUtil.Serialize(arProduct.Tags);
        this.distance = arProduct.Distance;
        this.province = arProduct.Province;
        this.city = arProduct.City;
        this.district = arProduct.District;
        this.centroid = arProduct.Centroid;
        this.address = arProduct.Address;

        this.state = arProduct.State;
        this.downloadPath = arProduct.DownloadPath;
        this.unzipPath = arProduct.UnzipPath;
        this.downloadProgress = arProduct.DownloadProgress;
        this.downloadState = arProduct.DownloadState;
        this.unZipState = arProduct.UnzipState;
    }

    /// <summary>
    /// query
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public ArProduct ObtainObject()
    {
        ArProduct arProduct = new ArProduct();

        arProduct.Cid = this.cid;
        arProduct.Name = this.name;
        arProduct.TotalSize = this.totalSize;
        arProduct.Orientation = this.orientation;
        arProduct.CoverImageUrl = this.coverImageUrl;
        arProduct.SarPid = this.sarPid;
        arProduct.UpdateTime = this.updateTime;
        arProduct.CloudRelocUrl = this.cloudRelocUrl;
        arProduct.NavEnabled = this.navEnabled;
        arProduct.Sid = this.sid;
        arProduct.ProductMaterials = JsonUtil.Deserialization<List<ProductMaterial>>(this.materials);
        arProduct.Tags = JsonUtil.Deserialization<List<AW_Tag>>(this.tags);
        arProduct.Distance = this.distance;
        arProduct.Province = this.province;
        arProduct.City = this.city;
        arProduct.District = this.district;
        arProduct.Centroid = this.centroid;
        arProduct.Address = this.address;

        arProduct.State = this.state;
        arProduct.DownloadPath = this.downloadPath;
        arProduct.UnzipPath = this.unzipPath;
        arProduct.DownloadProgress = this.downloadProgress;
        arProduct.DownloadState = this.downloadState;
        arProduct.UnzipState = this.unZipState;
        return arProduct;
    }

}
