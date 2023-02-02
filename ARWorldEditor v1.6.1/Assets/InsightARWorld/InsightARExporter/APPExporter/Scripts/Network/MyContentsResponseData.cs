
using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;


namespace ARWorldEditor
{
    public enum ContentsDevicesType
    {
        Mobile = 1,
        SimGlass = 3
    }
    [Serializable]
    public class MyContentsResponseData : BaseResponseData
    {
        public ContentResponseDataResult result;
    }

    public class ContentResponseDataResult
    {
        public List<MyContentsResultData> myContents;
        public List<MyContentsResultData> coordinationContents;
        public List<MyContentsResultData> teamContents;
    }
    [System.Serializable]
    public class MyContentsResultData
    {
        public long id;       //内容id;
        public long? gmtCreate;
        public long? gmtModified;
        public string name;  //内容名称
        public int? contentType; //内容类型，1-landmark ar，2-event ar
        public string logo; //封面logo
        public int? orientation; //屏幕方向：1-竖向，3-横向
        public int? algorithmType; //算法类型 1-Image AR，2-Object AR，3-Hand AR，4-Body AR 5 Landmark AR
        public string algorithmName; //算法功能
        public int? navigate; //启用导航：1-启用，0-不启用
        public int? navigateModel;
        public int engineType; //引擎类型：0-全部支持，1-端sdk，2-unity sdk
        public long? appId;    //所属应用ID
        public string appName; //所属应用名
        public long? mapId;      //地图ID
        public string mapName; //地图名称
        public string mapAddress;  //地图地址
        public string cloudRelocUrl; //云定位地址
        public int[] deviceTypeList;//1-mobile ,3-simGlass

        public string mode = "模板";//临时处理 模式属性
        public List<ContentPackageVersionsResultData> packageVersionList;
        
        public string userName;
        public string teamName;

        //方便记录设备类型
        [JsonIgnore]
        private int selectDevicesType;

        //选中的version id
        [JsonIgnore]
        private long selectedVersionId;

        //选中的version 索引
        [JsonIgnore]
        private int selectedVersionIdx;

        [JsonIgnore]
        private int selectedSimGlassVersionIdx;

        


        public long SelectedVersionId
        {
            get
            {
                return selectedVersionId;
            }
            set
            {
                selectedVersionId = value;
            }
        }

        public int SelectedVersionIdx
        {
            get
            {
                return selectedVersionIdx;
            }
            set
            {
                selectedVersionIdx = value;
            }
        }

        #region v1.6.0 加入sim眼睛端，做多选择处理
        public int SelectedSimGlassVersionIdx
        {
            get
            {
                return selectedSimGlassVersionIdx;
            }
            set
            {
                selectedSimGlassVersionIdx = value;
            }
        }
        public int SelectDevicesType
        {
            get
            {
                return selectDevicesType;
            }
            set
            {
                selectDevicesType = value;
            }
        }

        #endregion

        /// <summary>
        /// 返回所有版本信息
        /// </summary>
        /// <returns></returns>
        public string[] GetVersionStrings()
        {
            List<string> list = new List<string>();
            //Debug.Log("packageVersionList != null" + (packageVersionList != null)+ packageVersionList.Count);
            if (packageVersionList != null)
            {
                for (int i = 0; i < packageVersionList.Count; i++)
                {
                    //Debug.Log("GetVersionStrings:" + i);
                    list.Add(packageVersionList[i].version);
                }
            }
            return list.ToArray();
        }
        public string[] GetTargetVersionStrings(int deviceType)
        {
            List<string> list = new List<string>();
            if (packageVersionList != null)
            {
                for (int i = 0; i < packageVersionList.Count; i++)
                {
                    //Debug.Log("GetTargetVersionStrings:"+i);
                    if (packageVersionList[i].deviceType == deviceType)
                    {
                        list.Add(packageVersionList[i].version);
                    }
                }
            }
            return list.ToArray();
        }
        public int[] GetAllDevicesType()
        {
            return deviceTypeList;
        }
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetCurrentVersionInfo()
        {
            int index = 0;
            switch ((ContentsDevicesType)SelectDevicesType)
            {
                case ContentsDevicesType.Mobile:
                    index = SelectedVersionIdx;
                    if (packageVersionList.Count < index) return string.Empty;
                    break;
                case ContentsDevicesType.SimGlass:
                    index = SelectedSimGlassVersionIdx;
                    if (packageVersionList.Count < index) return string.Empty;
                    break;
                default:
                    break;
            }
            List<ContentPackageVersionsResultData>  findTargetDevicesTypeArray =  packageVersionList.FindAll(data => data.deviceType == SelectDevicesType);
            if (findTargetDevicesTypeArray.Count < index) return string.Empty;
            return findTargetDevicesTypeArray[index].version;
        }

        public long GetCurrentPackageId()
        {
            int index = 0;
            switch ((ContentsDevicesType)SelectDevicesType)
            {
                case ContentsDevicesType.Mobile:
                    index = SelectedVersionIdx;
                    if (packageVersionList.Count < index) return 0;
                    break;
                case ContentsDevicesType.SimGlass:
                    index = SelectedSimGlassVersionIdx;
                    if (packageVersionList.Count < index) return 0;
                    break;
                default:
                    break;
            }
            List<ContentPackageVersionsResultData> findTargetDevicesTypeArray = packageVersionList.FindAll(data => data.deviceType == SelectDevicesType);
            if (findTargetDevicesTypeArray.Count < index) return 0;
            return findTargetDevicesTypeArray[index].contentpackageId;
        }

        public string GetAlgorithmTypeDesc()
        {
            if (algorithmType == 1)
            {
                return "Image AR";
            }
            else if (algorithmType == 2)
            {
                return "Object AR";
            }
            else if (algorithmType == 3)
            {
                return "Hand AR";
            }
            else if (algorithmType == 4)
            {
                return "Body AR";
            }
            else if (algorithmType == 5)
            {
                return "Landmark AR";
            }
            return "Default AR";
        }

        public string GetContentTypeDesc()
        {
            if (contentType == 1)
            {
                return "场景";
            }
            else if (contentType == 2)
            {
                return "事件";
            }
            return "默认";
        }

        /// <summary>
        /// package id
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public long GetPackageId(int index)
        {
            if (packageVersionList.Count < index) return -1;
            List<ContentPackageVersionsResultData> findTargetDevicesTypeArray;
            findTargetDevicesTypeArray = packageVersionList.FindAll(data => data.deviceType == SelectDevicesType);
            if (findTargetDevicesTypeArray.Count < index) return -1;
            return findTargetDevicesTypeArray[index].contentpackageId;
        }
        /// <summary>
        /// get contenttype
        /// </summary>
        /// <returns></returns>
        public string GetContentIcon()
        {
            if (contentType == 1)
            {
                return "landmarkericon";
            }
            else if (contentType == 2)
            {
                return "eventicon";
            }
            return "defaultaricon";
        }

        public string GetEngineName()
        {
            if (engineType == 1)
            {
                return "Native";
            }
            else if (engineType == 2)
            {
                return "Unity";
            }
            return "全部支持";
        }

        public string GetOrientationType()
        {
            if (orientation == 1)
            {
                return "竖屏";
            }
            else if (orientation == 3)
            {
                return "横屏";
            }
            return "默认";
        }

        /// <summary>
        /// 新版本导航解耦后模式 ，参考 GetNavModeName
        /// navigate = 1 对应 navigateModel 2、3、4 
        /// navigate = 0 对应 navigateModel 1
        /// 
        /// </summary>
        /// <returns></returns>
        public int? GetNavMode()
        {
            if (navigateModel == null)
            {
                return navigateModel;
            }
            return (int)navigateModel;
        }

        public string GetNavModeName()
        {
            if (navigateModel == null)
            {
                return "";
            }

            switch (navigateModel)
            {
                case 1:
                    return "主场景模式";
                case 2:
                    return "POI导航模式";
                case 3:
                    return "2D地图模式";
                case 4:
                    return "导航模式";
                default:
                    throw new System.Exception("未知模式");
            }
        }
        /// <summary>
        ///  case 1: sceneTemplateName = "landmarker_template.unity";
        ///  case 2: sceneTemplateName = "navigate_poi_template.unity";
        ///  case 3: sceneTemplateName = "navigate_box2d_template.unity";
        ///  case 4: sceneTemplateName = "navigate_template.unity";
        /// </summary>
        /// <returns></returns>
        public string GetTemplateSceneName()
        {
            string sceneTemplateName = "";
            if (navigateModel != null) //新版通过navigateModel判断 
            {
                switch (navigateModel)
                {
                    case 1:
                        sceneTemplateName = "landmarker_template.unity";
                        break;
                    case 2:
                        sceneTemplateName = "navigate_poi_template.unity";
                        break;
                    case 3:
                        sceneTemplateName = "navigate_poi_template.unity";
                        break;
                    case 4:
                        sceneTemplateName = "navigate_template.unity";
                        break;
                    default:
                        break;
                }
            }
            else //兼容旧版 navigate属性
            {
                sceneTemplateName = navigate == 1 ? "navigate_template.unity" : "landmarker_template.unity";
            }
            return sceneTemplateName;
        }

        public string GetLogoNosUrl()
        {
            return "http://" + NOSConfig.HOST_NAME + "/" + logo;
        }

    }
}