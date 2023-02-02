#if UNITY_EDITOR
using ARWorldEditor;
using EZXR.CloudPlayBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
namespace ARWorldEditor
{
    /// <summary>
    /// 端云回放编辑器
    /// 只有超级管理员权限才允许使用
    /// </summary>
    public class CloudPlaybackEditorWindow : EditorWindow
    {
        #region param
        static CloudPlaybackEditorWindow _instance;
        private const string URL = "https://gw-dongjian-test.netease.com/services/wx-tool-test/api/playbacks/listByContent";
        private CloudPlayBackResponseDataResult cloudData;
        Vector2 scrollPos = Vector2.zero;
        private string searchStr = "";
        private const int pageNum = 1;
        private const int pageSize = 100;
        private List<CloudPlayBackResponseDataItem> searchItemList;

        #endregion
        public static GetMyContentsResultData LoadMyContent()
        {
            return JsonUtil.Deserialization<GetMyContentsResultData>(PlayerPrefs.GetString("MyProduct"));
        }

        [MenuItem("EZXR/端云回放 &s", true, 400)]
        static bool CheckValid()
        {
            return UserController.UserRoleIsSuperManager && UserController.UserLogin;
        }

        [MenuItem("EZXR/端云回放 &s", false, 401)]
        public static void ShowWindow()
        {
            EditorCoroutines.StartCoroutine(RequestPost(LoadMyContent().id), new object());
        }
        private void OnGUI()
        {
            float preHeight = 20f;
            float indent = 10f;//缩进10个单位
            int lineCount = 20;
            float height = position.height / lineCount;
            GUILayout.Space(preHeight);

            if (cloudData.data.Count <= 0)
            {
                GUILayout.Label("无对应ID :"+ LoadMyContent().id + " 端云回放数据");
                return;
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            GUILayout.BeginHorizontal();
            GUILayout.Space(indent);
            GUILayout.Label("ID", GUILayout.Width(40));
            GUILayout.Label("备注", GUILayout.Width(120f));
            GUILayout.Label("采集人", GUILayout.Width(120f));
            GUILayout.Label("采集时间", GUILayout.Width(120f));
            searchStr = EditorGUILayout.TextField("", searchStr, "SearchTextField", GUILayout.Width(120f));
            GUILayout.EndHorizontal();
            GUILayout.Space(indent);


            if (!string.IsNullOrEmpty(searchStr))
            {
                searchItemList = cloudData.data.FindAll(p => p.name.Contains(searchStr));
            }
            else
            {
                searchItemList = cloudData.data;
            }

            for (int i = 0; i < searchItemList.Count; i++)
            {
                var item = searchItemList[i];
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.id.ToString(), GUILayout.Width(40));
                GUILayout.Label(item.name, GUILayout.Width(120f));
                GUILayout.Label(item.createUserId?.ToString(), GUILayout.Width(120f));
                GUILayout.Label(item.startTime, GUILayout.Width(130f));
                GUILayout.Space(10);
                if (GUILayout.Button("拉取", GUILayout.Width(120f)))
                {
                    OnButtonGet(searchItemList[i].data, searchItemList[i].name + "_"+ searchItemList[i].id);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(height);
            }
            EditorGUILayout.EndScrollView();
        }
        private void OnButtonGet(string url, string name)
        {
            string cloudRootPath = Directory.GetParent(ConfigGlobal.DataPath).FullName;

            string downloadPath = Path.Combine(cloudRootPath, ConfigGlobal.CLOUD_PLAY_BACK_PATH) + name + "/";

            string downloadFilePath = downloadPath + name + ".zip";
            string tempPath = Path.Combine(ConfigGlobal.MAP_TEMP_PATH, name) + ".zip";

            //文件全路径
            string camTextFilePath = downloadPath + "/camera_params.txt";
            string tumTextFilePath = downloadPath + "/loc_vio_traj.txt";
            string imgRootPath = downloadPath + "/loc_images/";
            //确认已经解压完毕，并且有一个文件存在
            if (File.Exists(downloadPath + "/camera_params.txt"))
            {
                Debug.Log("已存在解压文件，跳过解压");
                ARWorldEditor.CloudPlayBack.ReadTextEditor.Create(imgRootPath, tumTextFilePath, camTextFilePath);
                _instance.Close();
                return;
            }

            //确定已经有.zip文件存在，未解压
            if (File.Exists(downloadFilePath))
            {
                Debug.Log(".zip文件存在，准备解压");
                UnzipAndDeleteZipFile(downloadFilePath);
                //根据文件目录传入到文件下载路径
                ARWorldEditor.CloudPlayBack.ReadTextEditor.Create(imgRootPath, tumTextFilePath, camTextFilePath);
                _instance.Close();
                return;
            }

         

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }
            else
            {
                Directory.Delete(downloadPath,true);
                Directory.CreateDirectory(downloadPath);
            }


            //todo 下载URL对应的文件
            EditorCoroutines.StartCoroutine(downloadCoroutine(url, downloadFilePath, tempPath, name,
                //fail
                (int code, string msg) => { },
                //suc
                () =>
                {
                    //Debug.LogError("suc");
                    UnzipAndDeleteZipFile(downloadFilePath);
                    //多层操作
                    if (!TryGetImageFolderPath(downloadPath, imgRootPath,out imgRootPath))
                    {
                        EditorUtility.DisplayDialog("Error","未找到端云回放数据，请检查解压后目录loc_images\n "+imgRootPath,"确认");
                        return;
                    }
                    var rootPath = new DirectoryInfo(imgRootPath).Parent.FullName;
                    //Debug.Log("rootPath:"+rootPath);
                    camTextFilePath = rootPath+"/camera_params.txt";
                    tumTextFilePath = rootPath + "/loc_vio_traj.txt";

                    //根据文件目录传入到文件下载路径
                    ARWorldEditor.CloudPlayBack.ReadTextEditor.Create(imgRootPath,tumTextFilePath,camTextFilePath);
                    _instance.Close();
                },
                //progress
                (string str, float progress) => { }), new object());

        }
        static IEnumerator RequestPost(long contentId)
        {
            UnityWebRequest unityWebRequest = new UnityWebRequest(URL, "POST");

            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(JsonUtil.Serialize(new CloudPlayBackRequestData() { pageSize = pageSize, pageNum = pageNum, contentId = contentId }));
            unityWebRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
            unityWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            unityWebRequest.SetRequestHeader("Content-Type", "application/json");
            unityWebRequest.useHttpContinue = false;

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
            {
                Debug.LogError("失败：" + unityWebRequest.error);
                EditorUtility.DisplayDialog("Error", "请求端云回放数据失败\nError:" + unityWebRequest.error, "确认");
            }
            else
            {
                string result = unityWebRequest.downloadHandler.text;
                //Debug.Log("成功" + result);
                var responseData = JsonUtil.Deserialization<CloudPlayBackResponseData>(result);


                if (_instance == null)
                {
                    _instance = (CloudPlaybackEditorWindow)EditorWindow.GetWindow(typeof(CloudPlaybackEditorWindow), true, "Cloud Data");
                    _instance.minSize = new Vector2(600f, 600f);
                    _instance.maxSize = new Vector2(600f, 600f);
                }
                _instance.cloudData = responseData.result;
                _instance.Show();
            }
        }

        static IEnumerator RequestCloudPlayBackData(string url)
        {
            UnityWebRequest unityWebRequest = new UnityWebRequest(URL, "Get");

            unityWebRequest.useHttpContinue = false;

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
            {
                Debug.LogError("失败：" + unityWebRequest.error);
                EditorUtility.DisplayDialog("Error", "请求端云回放数据失败\nError:" + unityWebRequest.error, "确认");
            }
            else
            {
                byte[] result = unityWebRequest.downloadHandler.data;
                //写入到本地

                Debug.LogError("成功" + result);
            }
        }
        /// <summary>
        /// 下载地图协程
        /// </summary>
        /// <param name="url"></param>
        /// <param name="downloadPath"></param>
        /// <param name="tempFilePath"></param>
        /// <param name="fileName"></param>
        /// <param name="onError"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onProgress"></param>
        /// <returns></returns>
        IEnumerator downloadCoroutine(string url, string downloadPath, string tempFilePath, string fileName = null, Action<int, string> onError = null,
    Action onSuccess = null, Action<string, float> onProgress = null)
        {
            ARWorldEditor.DownloadTask downloadTask = new ARWorldEditor.DownloadTask();
            downloadTask.DownloadUrl = url;
            downloadTask.SavePath = downloadPath;
            downloadTask.TempPath = tempFilePath;
            downloadTask.FileName = fileName;

            bool isDownloadFinish = false;
            downloadTask.AddListener(new ARWorldEditor.DownloadCompleteCallBack((ARWorldEditor.DownloadTask task) =>
            {
                isDownloadFinish = true;
                onSuccess?.Invoke();

            }), new ARWorldEditor.DownloadProgressCallBack((ARWorldEditor.DownloadTask task, long curSize, long totalSize) =>
            {
                //每个事件的progress
                if (onProgress != null)
                {
                    float progress = 0.0f;
                    if (totalSize != 0)
                    {
                        progress = (float)curSize / totalSize;
                    }
                    onProgress(task.FileName, progress);
                }
            }), new ARWorldEditor.DownloadErrorCallBack((ARWorldEditor.DownloadTask task, int code, string msg) =>
            {
                isDownloadFinish = true;
                onError?.Invoke(code, msg);
            }));
            ARWorldEditor.FileDownloadManager.Instance.DownloadAsync(downloadTask);
            //一直等待返回执行结果
            while (!isDownloadFinish)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="savePath"></param>
        private void UnzipAndDeleteZipFile(string zipFilePath)
        {
            if (!zipFilePath.EndsWith(".zip")) return;
            string unzipDirectory = Path.GetDirectoryName(zipFilePath);
            ARWorldEditor.ZipUtility.Unzip(zipFilePath, unzipDirectory);
            //解压后删除本地zip 文件
            File.Delete(zipFilePath);
        }

        bool TryGetImageFolderPath(string unzipRootPath,string resImgPath, out string imgPath)
        {
            imgPath = "";
            if (!Directory.Exists(unzipRootPath))
            {
                EditorUtility.DisplayDialog("Error", "地图文件夹未找到，请尝试重新拉取内容", "确认");
                return false;
            }

            if (File.Exists(resImgPath))
            {
                imgPath = resImgPath;
                return true;
            }

            //内部如果看到这里，请与数据记录平台负责人联系
            Debug.Log("目标文件不存在，可能文件目录错误，或者文件丢失，或者文件弃用，开始深入查找");

            var dirList =  Directory.GetDirectories(unzipRootPath, "*", SearchOption.AllDirectories);
            foreach (var item in dirList)
            {
                //Debug.Log("path:"+item);
                DirectoryInfo temp = new DirectoryInfo(item);
                if (temp.Name == "loc_images")
                {
                    imgPath = temp.FullName;
                    break;
                }
            }


            if (string.IsNullOrEmpty(imgPath))
            {
                Debug.LogError("未找到loc_images");
                return false;
            }
            return true;
        }

    }




    public class CloudPlayBackRequestData
    {
        public int pageNum;
        public int pageSize;
        public long contentId;
    }

    public class CloudPlayBackResponseDataItem
    {
        public int id { get; set; }
        public string gmtCreate { get; set; }
        public string gmtModified { get; set; }
        public string data { get; set; }
        public string createUserId { get; set; }
        public string name { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string extra { get; set; }
        public int platform { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string osVersion { get; set; }
        public string fileName { get; set; }
        public string contentId { get; set; }
        public string parsedData { get; set; }
        public string parsedResult { get; set; }
    }

    public class CloudPlayBackResponseDataResult
    {
        public int total { get; set; }
        public List<CloudPlayBackResponseDataItem> data { get; set; }
        public int pageSize { get; set; }
        public int pageNum { get; set; }
    }

    public class CloudPlayBackResponseData
    {
        public string code { get; set; }
        public CloudPlayBackResponseDataResult result { get; set; }
        public string msg { get; set; }
    }
}
#endif