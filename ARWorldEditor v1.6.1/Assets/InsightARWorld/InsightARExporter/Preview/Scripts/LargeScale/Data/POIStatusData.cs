using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POINeedUpdateData
{
    NO_NEEDUPDATE = 0,
    NEEDUPDATE = 1,
}
public enum POIDownloadResult
{
    DOWNLOAD_DONE = 1,
    DOWNLOAD_DOING_OR_UNDO = 0,
    DOWNLOAD_FAILED = -1,
}

public class POIStatusData
{
    public string downloadResult;
    public string cid;
    public string needUpdate;

    [JsonIgnore]
    public string DownloadResult
    {
        get
        {
            return downloadResult;
        }
        set
        {
            downloadResult = value;
        }
    }

    [JsonIgnore]
    public string Cid
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
    public string NeedUpdate
    {
        get
        {
            return needUpdate;
        }
        set
        {
            needUpdate = value;
        }
    }
}
