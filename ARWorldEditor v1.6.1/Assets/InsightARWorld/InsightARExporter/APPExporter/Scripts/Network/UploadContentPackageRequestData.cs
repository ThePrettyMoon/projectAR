using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;

namespace ARWorldEditor
{
    [Serializable]
    public class UploadContentPackageRequestData : ARWorldEditor.BaseRequestData
    {
        public long contentId;
        public long contentPackageId;
        public string updateDes;  //更新说明，针对内容包
        public List<ContentPackageData> resourceList;

        public UploadContentPackageRequestData(BaseAuthRequestData baseRequestData)
        {
            this.sign = baseRequestData.sign;
            this.nonce = baseRequestData.nonce;
            this.t = baseRequestData.t;
            this.token = baseRequestData.token;
        }

        public override void ResetTimeStamp(long timestamp)
        {
        }
    }

    [Serializable]
    public class ContentPackageData
    {
        public int platform; //1-IOS ,2-Android
        public string nosObj;   //资源上传nos后返回的nosobj
        public long size;        //资源包大小，字节
        public string name;     //资源名称
        public string md5;      //内容包MD5值

        public override string ToString()
        {
            return "contentpackage " + " platform " + platform + " nosObj " + nosObj + " size " + size + " name " + name + " md5 " + md5;
        }
    }
}