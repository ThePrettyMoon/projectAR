using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWContentData {}

/// <summary>
/// 同 subArProduct
/// </summary>
public class AW_SubContent
{

    public int cid;
    public string name;
    public long totalSize;
    public AW_Orientation orientation;
    public string coverImageUrl;
    public List<ProductMaterial> materials;
    public int sarPid;
    public long updateTime;
    public bool promptBeforeDownload;


}
/// <summary>
/// 同 ArProduct
/// </summary>
public class AW_Landmarker
{
    public int cid;
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

}

public class AW_Material:BaseDbData
{
    public int mid;
    public int type;
    public long size;
    public string url;
    public string md5;
}

public class AW_Tag
{
    public int mid;
    public string name;
}

public class AW_GeoPosition
{
    public long latitude;
    public long longitude;
}

public enum AW_Orientation {
    PORTRAIT = 1,
    LANDSCAPE = 2
}

public class AW_ClientInfo
{
    //【必填*】SDK版本号
    public string sdkVersion;
    //【必填*】平台
    public int platform;
    //【必填*】设备品牌
    public string brand;
    //【必填*】设备型号
    public string model;
    //【必填*】设备系统版本
    public string osVersion;
    //【选填】接入点
    public string xnwa;
}


public enum AW_Platform
{
    IOS_SDK = 3,
    ANDROID_SDK = 4,
    IOS_UNITY_SDK = 5,
    ANDROID_UNITY_SDK = 6
}

public enum AW_CoordType {
    GCJ02 = 1,
    WGS84 = 2,
    BD09 = 3
}

public enum AW_ResultObjectType {
    LANDMARKER = 1,
    SUB_CONTENT = 2
}

public enum AW_BuildType {

    RELEASE = 1,
    DEBUG = 2

}

public enum AW_SdkResourceType {

    ALG = 1,
    RENDER = 2

}

public enum AW_ABIType {

    ARM_32 = 0,
    ARM_64 = 1
}

public class AW_SdkResource {

    public int id;
    public AW_SdkResourceType type;
    public string url;
    public long size;
    public string md5;
    public AW_ABIType abi;

}