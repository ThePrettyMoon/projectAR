using System;
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ARWorldEditor
{
    [Serializable]
    public class GetMyContentsResponseData : BaseResponseData
    {
        public List<GetMyContentsResultData> result; 
        public int GetContentLengthByType(int contentType)
        {
            if (result == null) return 0;
            return result.FindAll(p => p.contentType == contentType).Count;
        }
    }

    [System.Serializable]
    public class GetMyContentsResultData
    {
        public long id;       //内容id;
        public long ?gmtCreate;
        public long ?gmtModified;
        public string name;  //内容名称
        public int ?contentType; //内容类型，1-landmark ar，2-event ar
        public string logo; //封面logo
        public int ?orientation; //屏幕方向：1-竖向，3-横向
        public int ?algorithmType; //算法类型 1-Image AR，2-Object AR，3-Hand AR，4-Body AR 5 Landmark AR
        public string algorithmName; //算法功能
        public int ?navigate; //启用导航：1-启用，0-不启用
        public int ?navigateModel;
        public int engineType; //引擎类型：0-全部支持，1-端sdk，2-unity sdk
        public long ?appId;    //所属应用ID
        public string appName; //所属应用名
        public long ?mapId;      //地图ID
        public string mapName; //地图名称
        public string mapAddress;  //地图地址
        public string cloudRelocUrl; //云定位地址
    
        public string mode = "模板";//临时处理 模式属性
        public List<ContentPackageVersionsResultData> packageVersionList;

        //选中的version id
        [JsonIgnore]
        public long selectedVersionId ;

        //选中的version 索引
        [JsonIgnore]
        public int selectedVersionIdx ;


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

        /// <summary>
        /// 返回所有版本信息
        /// </summary>
        /// <returns></returns>
        public string[] GetVersionStrings()
        {
            List<string> list = new List<string>();
            if (packageVersionList != null)
            {
                for(int i = 0; i < packageVersionList.Count; i++)
                {
                    list.Add(packageVersionList[i].version);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetCurrentVersionInfo()
        {
            if (packageVersionList.Count < selectedVersionIdx) return string.Empty;
            return packageVersionList[selectedVersionIdx].version ;
        }

        public long GetCurrentPackageId()
        {
            if (packageVersionList.Count < selectedVersionIdx) return 0;
            return packageVersionList[selectedVersionIdx].contentpackageId;
        }

        public string GetAlgorithmTypeDesc()
        {
            if(algorithmType == 1)
            {
                return "Image AR";
            }else if(algorithmType == 2)
            {
                return "Object AR";
            }else if(algorithmType == 3)
            {
                return "Hand AR";
            }else if(algorithmType == 4)
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
            if(contentType == 1)
            {
                return "场景";
            }else if(contentType == 2)
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
            return packageVersionList[index].contentpackageId;
        }
        /// <summary>
        /// get contenttype
        /// </summary>
        /// <returns></returns>
        public string GetContentIcon()
        {
            if(contentType == 1)
            {
                return "landmarkericon";
            }else if(contentType == 2)
            {
                return "eventicon";
            }
            return "defaultaricon";
        }

        public string GetEngineName()
        {
            if(engineType == 1)
            {
                return "Native";
            }else if(engineType == 2)
            {
                return "Unity";
            }
            return "全部支持";
        }

        public string GetOrientationType()
        {
            if(orientation == 1)
            {
                return "竖屏";
            }else if(orientation == 3)
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
            return  "http://"+NOSConfig.HOST_NAME + "/" + logo;
        }

    }
}