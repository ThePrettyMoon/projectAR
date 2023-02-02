using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityResourceData
{
    public string localPath;
    public string brand;
    public string appKey;
    public string appSecret;
    public UnityProductData content;
}
public class UnityProductData {

    public bool navEnabled;
    public int cid;
    public int sid;
    public string cloudRelocUrl;
    public string name;
    public long totalSize;
    public int orientation;
    public int engineType;

    public int snapshotAuditStatus;
    public int snapshotType;
    public List<AW_Tag> tags;
    public List<ProductMaterial> materials;
    public int sarPid;
    public long updateTime;
    public AW_GeoPosition centroid;
    public long packageSdkVersion;
    public int contentType;
    public string distance;
    public string address;
    public string coverImageUrl;
    public Creator creator;

}

public class Creator {
    public string nick;
    public string avatar;
    public int gender;
}