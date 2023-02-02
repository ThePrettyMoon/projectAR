using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ARMarker : MonoBehaviour
{
    [System.Serializable]
    public class ImageReceiveData
    {
        [System.Serializable]
        public class Result
        {
            public string fileName;
            public string objectName;
            public string url;
        };
        public int code;
        public string msg;
        public Result result;
    };

    [System.Serializable]
    public class MarkerAlgSendData
    {
        [System.Serializable]
        public class Image
        {
            public string image;
            public string type = "web";
        }
        public Image[] imageList;
        public int sdkVersionType = 3;
        public bool multiMarker = false;
    }

    [System.Serializable]
    public class MarkerAlgReceiveData
    {
        public int code;
        public string msg;
        public string result;
    }

    public class MarkerInfo
    {
        public Texture marker;
        public string localPath;
        public string url;
    }

    string TextureUploadURL = "http://mr-test.dongjian.netease.com/biz/nos/upload";
    string MarkerRequestURL = "http://mr-test.dongjian.netease.com/biz/at/genalg";
    public MarkerInfo[] markerImageArray;


    public void PrepareData()
    {
        //for(int i = 0; i < markerImageArray.Length; i++)
        //{
        //    markerImageArray[i].marker
        //}
    }

    public void RequestMarker()
    {
        int seek;
        for (seek = 0; seek < markerImageArray.Length; seek++)
        {
            if (string.Empty == markerImageArray[seek].url)
            {
                UploadPNG(seek);
            }
        }
    }

    void UploadPNG(int index)
    {
        Debug.Log("开始上传图片...");

        FnWebRequestRuntime.instance.ThreadSafe = true;
        string filePath = markerImageArray[index].localPath;
        FnWebRequestRuntime.instance.HttpUploadFileAsync(TextureUploadURL, filePath, index.ToString(), "application/octet-stream", null, UploadPNGCallBack, null);
    }

    void UploadPNGCallBack(FnWebRequestBase.ResponseData responseData, string identifier)
    {
        if (responseData.isError)
        {
            Debug.Log("上传文件失败：" + responseData.error);
            return;
        }

        string text = System.Text.Encoding.UTF8.GetString(responseData.data);
        int index = System.Int32.Parse(identifier);
        ImageReceiveData imageReceiveData = JsonUtility.FromJson<ImageReceiveData>(text);
        // 如果返回信息不是成功
        if (imageReceiveData.code != 200)
        {
            Debug.Log("上传文件失败：" + imageReceiveData.msg);
        }
        else
        {
            Debug.Log(imageReceiveData.result.fileName + "上传成功");
            markerImageArray[index].url = imageReceiveData.result.url;
            ImageUploadDone();
        }
    }

    private void ImageUploadDone()
    {
        int seek;
        for (seek = 0; seek < markerImageArray.Length; seek++)
            if (string.Empty == markerImageArray[seek].url)
                break;

        // 所有图片均已上传
        if (seek >= markerImageArray.Length)
            RequestMarkerDesc();
    }

    void RequestMarkerDesc()
    {
        Debug.Log("开始请求描述文件...");

        MarkerAlgSendData markerAlgSendData = new MarkerAlgSendData();
        markerAlgSendData.imageList = new MarkerAlgSendData.Image[markerImageArray.Length];
        for (int i = 0; i < markerImageArray.Length; i++)
        {
            markerAlgSendData.imageList[i] = new MarkerAlgSendData.Image();
            markerAlgSendData.imageList[i].image = markerImageArray[i].url;
        }
        markerAlgSendData.multiMarker = true;
        string jsonString = JsonUtility.ToJson(markerAlgSendData) ?? "";

        FnWebRequestRuntime.instance.ThreadSafe = true;
        FnWebRequestRuntime.instance.CreateRequestAsync(MarkerRequestURL, jsonString, string.Empty, RequestMarkerDescCallBack, null, FnWebRequestBase.RequestMode.POST, "application/json");
    }

    void RequestMarkerDescCallBack(FnWebRequestBase.ResponseData responseData, string index)
    {
        if (responseData.isError)
        {
            Debug.Log("算法图片描述文件生成失败：" + responseData.error);
            return;
        }

        string text = System.Text.Encoding.UTF8.GetString(responseData.data);
        MarkerAlgReceiveData markerAlgReceiveData = JsonUtility.FromJson<MarkerAlgReceiveData>(text);
        // 如果返回信息不是成功
        if (markerAlgReceiveData.code != 200 || string.IsNullOrEmpty(markerAlgReceiveData.result))
        {
            Debug.Log("算法图片描述文件生成失败：" + markerAlgReceiveData.msg);
        }
        else
        {
            Debug.Log("描述文件请求成功");
            FnWebRequestRuntime.instance.CreateRequestAsync(markerAlgReceiveData.result, string.Empty, string.Empty, DownloadMarkerCallBack, null, FnWebRequestBase.RequestMode.GET, string.Empty);
        }
    }

    void DownloadMarkerCallBack(FnWebRequestBase.ResponseData responseData, string identifier)
    {
        if (responseData.isError)
        {
            Debug.Log("算法图片描述文件生成失败：" + responseData.error);
            return;
        }

        if (null == responseData || null == responseData.data)
        {
            Debug.Log("算法图片描述文件生成失败！");
        }
        else
        {
            // 保存描述文件包
            if (null != responseData.data)
            {
                //todo
            }
        }
    }
}

[CustomEditor(typeof(ARMarker))]
public class ARMarkerInspector : Editor
{
    bool showImageConfig;
    public override void OnInspectorGUI()
    {
        //showImageConfig = EditorGUILayout.Foldout(showImageConfig, "识别图片");
        //if (showImageConfig)
        //{
        //    for (int i = 0; i < config.markerList.Count; ++i)
        //    {
        //        EditorGUILayout.BeginHorizontal();
        //        config.isFolded[i] = EditorGUILayout.Foldout(config.isFolded[i], "识别图片#" + (i + 1));
        //        // 308不显示删除按钮
        //        if (config.markerList.Count > 1)
        //        {
        //            if (configItem.itemMaxNum <= 1)
        //            {
        //                if (GUILayout.Button("删除", GUILayout.Width(50)))
        //                {
        //                    config.markerList.RemoveAt(i);
        //                    config.isFolded.RemoveAt(i);
        //                    i--;
        //                    continue;
        //                }
        //            }

        //        }
        //        EditorGUILayout.EndHorizontal();

        //        if (config.isFolded[i])
        //        {
        //            config.markerList[i].imageFile = EditorGUILayout.ObjectField("图片文件(JPG/PNG)", config.markerList[i].imageFile, typeof(Texture), true) as Texture;
        //            if (config.markerList[i].imageFile)
        //            {
        //                string ext = Path.GetExtension(AssetDatabase.GetAssetPath(config.markerList[i].imageFile)).ToLower();
        //                config.markerList[i].isValid = (ext == ".jpg" || ext == ".jpeg" || ext == ".png");
        //                config.markerList[i].isRepeated = MarkerInfo.IsRepeatedTexture(config.markerList, i);

        //                if (config.markerList[i].isValid && (!config.markerList[i].isRepeated))
        //                {
        //                    if (config.markerList[i].markerName == "")
        //                        config.markerList[i].markerName = "marker" + DateTime.Now.ToString("HHmmss"); // TODO 极限情况不同天同秒的加一秒
        //                }
        //            }

        //            if (!config.markerList[i].isValid)
        //            {
        //                EditorGUILayout.HelpBox("请选择JPG或PNG格式的图片！", MessageType.Error);
        //            }
        //            if (config.markerList[i].isRepeated)
        //            {
        //                EditorGUILayout.HelpBox("该图片已经选过，请选择不同的图片！", MessageType.Warning);
        //            }

        //            // GUI.SetNextControlName("name" + i);
        //            string newName = EditorGUILayout.TextField(new GUIContent("自定义名称", "自定义名称仅包含字母和数字"), config.markerList[i].markerName);
        //            // if(GUI.GetNameOfFocusedControl() == ("name" + i))
        //            // {
        //            // 	Debug.Log("focus on " + "name" + i);
        //            if (newName != null && newName != "" && newName != config.markerList[i].markerName)
        //            {
        //                // 重名检测和有效性检测
        //                if (Regex.IsMatch(newName, @"^[A-Za-z0-9]*$"))
        //                {
        //                    if (!MarkerInfo.DuplicationMarkerName(config.markerList, newName))
        //                        config.markerList[i].markerName = newName;
        //                    else
        //                        EditorUtility.DisplayDialog("InsightARSetting", "自定义名称不能重复，请输入其他名称。", "确定");
        //                }
        //                else
        //                {
        //                    EditorUtility.DisplayDialog("InsightARSetting", "自定义名称仅包含字母和数字，请输入其他名称。", "确定");
        //                }
        //            }
        //            // }


        //            config.markerList[i].markerWidth = EditorGUILayout.FloatField(new GUIContent("图片长边尺寸(m)", "线下物料图片长边的尺寸"), config.markerList[i].markerWidth);
        //            if (config.markerList[i].markerWidth <= 0)
        //                config.markerList[i].markerWidth = 1;
        //            if (configItem.imageDirectionEnabled)
        //            {
        //                config.markerList[i].markerDirection = EditorGUILayout.IntPopup(configItem.displayName, config.markerList[i].markerDirection,
        //                                                                                configItem.enumDisplay, configItem.enumValue);
        //            }
        //        }

        //    }

        //    if (configItem.paramName != "")
        //    {
        //        int enumIdx = (int)paramFieldDic[configItem.paramName].GetValue(result.ARSessionConfigs);
        //        paramFieldDic[configItem.paramName].SetValue(result.ARSessionConfigs,
        //                                                     EditorGUILayout.IntPopup(configItem.displayName, enumIdx,
        //                                                                              configItem.enumDisplay, configItem.enumValue));
        //    }


        //    if (configItem.itemMaxNum <= 1)
        //    {
        //        if (GUILayout.Button("新建识别图片"))
        //        {
        //            MarkerInfo info = new MarkerInfo();
        //            info.isValid = true;
        //            config.markerList.Add(info);
        //            config.isFolded.Add(true);
        //        }
        //    }

        //}
    }
}
