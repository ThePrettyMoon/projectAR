using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 根据GPS获取附近建筑
/// </summary>
[Serializable]
public class LandmarkersNearbyResponseData 
{
    public List<ArProduct> scenes;
}

[Serializable]
public class NearbyScene
{
    public int sceneId;
    public long gmtCreate;
    public long gmtModifited;
    public string name;
    public int resType;
    public int? outdoorPid;
    public int? indoorPid;
    public string outFence;
    public string outline;
    public string inFence;

    public int areaId;
    public string areaName;

    public string indoorAvatarUrl;
    public string outdoorAvatarUrl;
    
    public ArProduct outdoorProduct;
    public ArProduct indoorProduct;
}


