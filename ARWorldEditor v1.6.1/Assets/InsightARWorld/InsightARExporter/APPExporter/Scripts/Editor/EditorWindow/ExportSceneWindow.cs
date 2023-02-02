#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ARWorldEditor;
using RenderEngine;
using System;
using static ARWorldEditor.LocalSceneCacheManager;

namespace ARSession
{
    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    public class ExportSceneWindow : EditorWindow
    {
        #region params
        private const long MAX_FILE_LENGTH = 200 * 1024 * 1024;
        private static ExportSceneWindow _instance;
        private GUISkin skin;
        private GUIStyle popUpStyle;
        private MyContentsResultData myContent;
       
        private PlatformType selectPlatform;
        private string updateDescTxt="";
        private string[] supportPlatforms = new string[] { "iOS+Android", "iOS", "Android" };
        //0-ios 1-android 2-sim glass
        private bool[] mobileSupportPlatforms = new bool[] { true, true, true };
        private bool isRunning = false;
        #endregion

        #region custom functions
        private void Awake()
        {
            skin = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.FileDirectory + "ExportSkin.guiskin", typeof(GUISkin)) as GUISkin;
            popUpStyle = new GUIStyle(EditorStyles.popup);
            popUpStyle.alignment = TextAnchor.MiddleLeft;
            popUpStyle.fixedWidth = 215;
            popUpStyle.fixedHeight = 25;
            popUpStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            popUpStyle.active.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            popUpStyle.hover.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            popUpStyle.focused.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;

            skin.textArea.fixedHeight = 107;
            skin.textArea.fixedWidth = 309;

            skin.customStyles[1].normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            skin.label.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
        }

        public static void ShowWindow()
        {
            if (_instance == null)
            {
                _instance = (ExportSceneWindow)EditorWindow.GetWindow(typeof(ExportSceneWindow), true, "我的内容");
                _instance.minSize = new Vector2(500, 550);
                _instance.Show();
                _instance.LoadAssets();
            }
        }

        private void OnInspectorUpdate()
        {
            if (!isRunning) return;
            Repaint();
        }

        private void OnGUI()
        {
            if (!isRunning) return;
            if (myContent != null)
            {
                DrawExportPanelView();
            }
            else
            {
                DrawWarningView();
            }
        }

        private void OnDestroy()
        {
            isRunning = false;
        }
        #endregion

        #region custom functions
        /// <summary>
        /// 资源加载
        /// </summary>
        private void LoadAssets()
        {
            //myContent = MyProducts.MyProduct;
            myContent = LoadMyContent();
            //加载mobile选择缓存
            if (myContent!= null&&myContent.SelectDevicesType == (int)ContentsDevicesType.Mobile && 
                LocalSceneCacheManager.Instance.LoadPlatformFromCache(myContent.id.ToString(), out var data))
            {
                mobileSupportPlatforms[0] = data.iosToggle;
                mobileSupportPlatforms[1] = data.androidToggle;
            }
            isRunning = true;
        }

        public static MyContentsResultData LoadMyContent()
        {
            return JsonUtil.Deserialization<MyContentsResultData>(PlayerPrefs.GetString("MyProduct"));
        }

        /// <summary>
        /// 显示导出界面
        /// </summary>
        private void DrawExportPanelView()
        {
            GUI.skin = skin;
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            Vector2 beginPos = new Vector2(95, 50);
            GUI.Label(new Rect(beginPos.x, beginPos.y, 80, 18), "内容信息", GUI.skin.customStyles[1]);
            GUILayout.Space(20);
            GUI.Label(new Rect(beginPos.x + 100, beginPos.y, 150, 20), "ID:" + myContent.id, GUI.skin.label);
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUI.Label(new Rect(beginPos.x, beginPos.y + 33, 80, 18), "内容名称", GUI.skin.customStyles[1]);
            GUILayout.Space(20);
            GUI.Label(new Rect(beginPos.x + 100, beginPos.y + 33, 200, 20), myContent.name, GUI.skin.label);
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUI.Label(new Rect(beginPos.x, beginPos.y + 66, 80, 18), "内容分类", GUI.skin.customStyles[1]);
            GUILayout.Space(20);
            GUI.Label(new Rect(beginPos.x + 100, beginPos.y + 66, 150, 20), myContent.GetContentTypeDesc(), GUI.skin.label);
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUI.Label(new Rect(beginPos.x, beginPos.y + 99, 80, 18), "上线应用", GUI.skin.customStyles[1]);
            GUILayout.Space(20);
            GUI.Label(new Rect(beginPos.x + 100, beginPos.y + 99, 200, 20), myContent.appName);
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUI.Label(new Rect(beginPos.x, beginPos.y + 132, 80, 18), "内容包版本", GUI.skin.customStyles[1]);
            GUILayout.Space(20);
            GUI.Label(new Rect(beginPos.x + 100, beginPos.y + 132, 150, 20), myContent.GetCurrentVersionInfo());
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            GUI.Label(new Rect(beginPos.x, beginPos.y + 165, 80, 18), "发布平台", GUI.skin.customStyles[1]);
            GUILayout.Space(20);
            GUILayout.BeginArea(new Rect(beginPos.x + 100, beginPos.y + 165, 215, 70));
            float mobileDistance =0;
            //标题
            if (myContent.SelectDevicesType == (int)ContentsDevicesType.Mobile)
            {
                mobileDistance = 50;
                EditorGUILayout.LabelField("移动端");
                GUILayout.Space(10);
                mobileSupportPlatforms[0]  = GUILayout.Toggle(mobileSupportPlatforms[0], "  IOS", EditorStyles.toggle);
                GUILayout.Space(5);
                mobileSupportPlatforms[1] = GUILayout.Toggle(mobileSupportPlatforms[1], "  Android", EditorStyles.toggle);
            }
            else if(myContent.SelectDevicesType == (int)ContentsDevicesType.SimGlass)
            {
                EditorGUILayout.LabelField("Sim眼镜端");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
         
            GUILayout.Space(15);
            GUI.Label(new Rect(beginPos.x, beginPos.y + 205+ mobileDistance, 150, 18), "更新日志", GUI.skin.customStyles[1]);
            GUILayout.Space(15);
            GUILayout.BeginArea(new Rect(beginPos.x, beginPos.y + 238 + mobileDistance, 309, 107));
            EditorGUI.BeginChangeCheck();
            updateDescTxt = EditorGUILayout.TextArea(updateDescTxt, GUI.skin.textArea);
            if (EditorGUI.EndChangeCheck())
            {
                if (updateDescTxt.Length >= 50)
                {
                    updateDescTxt = updateDescTxt.Substring(0, 50);
                    GUI.FocusControl(null);
                }
            }
            GUILayout.EndArea();
            GUI.Label(new Rect(beginPos.x + 238, beginPos.y + 323 + mobileDistance, 60, 20), updateDescTxt.Length + "/" + "50", GUI.skin.customStyles[2]);
            GUILayout.Space(30);



            if (GUI.Button(new Rect(beginPos.x, beginPos.y + 375 + mobileDistance, 140, 30), "取消", GUI.skin.button))
            {
                OnClickCancelHandler();
                GUIUtility.ExitGUI();
            }

            var enbalePublish = true;

            if (myContent.SelectDevicesType == (int)ContentsDevicesType.Mobile)
            {
                if (mobileSupportPlatforms[0] == true && mobileSupportPlatforms[1] == true)
                {
                    selectPlatform = PlatformType.PLATFORM_TYPE_ALL;
                }
                else if (mobileSupportPlatforms[0] == true)
                {
                    selectPlatform = PlatformType.PLATFORM_TYPE_IOS;
                }
                else if (mobileSupportPlatforms[1] == true)
                {
                    selectPlatform = PlatformType.PLATFORM_TYPE_ANDROID;
                }
                else
                {
                    //不可发布
                    enbalePublish = false;
                }
            }
            else if (myContent.SelectDevicesType == (int)ContentsDevicesType.SimGlass)
            {
                selectPlatform = PlatformType.PLATFORM_TYPE_ANDROID;
            }
            else
            {
                Debug.LogError("SelectDevicesType 类型异常");
            }

            GUI.enabled = enbalePublish;
            if (GUI.Button(new Rect(beginPos.x + 169, beginPos.y + 375 + mobileDistance, 140, 30), "发布", GUI.skin.button))
            {
                if (myContent.SelectDevicesType == (int)ContentsDevicesType.Mobile)
                {
                    LocalSceneCacheManager.Instance.SaveToPlatformDic(myContent.id.ToString(), mobileSupportPlatforms[0], mobileSupportPlatforms[1]);
                }
                OnClickPublishHandler(myContent, (EngineType)myContent.engineType, selectPlatform, myContent.SelectDevicesType, updateDescTxt);
                GUIUtility.ExitGUI();

            }
            GUI.enabled = true;
            GUI.skin = null;
        }

        /// <summary>
        /// 显示没有选择界面
        /// </summary>
        private void DrawWarningView()
        {
            GUI.skin = skin;
            GUI.Label(new Rect(_instance.position.width / 2.0f -100 , _instance.position.height / 2.0f - 60, 200, 60), "请先选择场景！", GUI.skin.customStyles[3]);
            GUI.skin = null;
        }

        /// <summary>
        /// 点击上传按钮
        /// </summary>
        private void OnClickPublishHandler(MyContentsResultData myContent,EngineType engineType,PlatformType platform,int devicesType,string updateDesc)
        {
            long contentId = myContent.id;
            long contentPackageId = myContent.GetCurrentPackageId();
            string version = myContent.GetCurrentVersionInfo();

            //关闭界面
            _instance.Close();

            //判断一下当前场景名称和预期一致
            string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            string currentScenePath = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
            string exportSceneName = string.Format("scene_{0}_{1}", contentId, version);
            if (currentSceneName != exportSceneName)
            {
                if (!EditorUtility.DisplayDialog("上传提示", "当前场景和本地缓存场景名称不一致，是否仍要上传", "确认", "取消")){
                    return;
                }
            }
            //将发布场景数据缓存到本地，方便后续使用当前场景
            LocalSceneCacheManager.Instance.SaveToDic(contentId.ToString(),currentSceneName,version,devicesType.ToString(), currentScenePath);
            

            ExportProgressEditorWindow.ShowWindow();
            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentStartPublish, 0f);

            EditorCoroutines.StartCoroutine(GetExportPackageList(engineType, platform, devicesType, (List<ContentPackageData> resourceList) =>
            {
                ARWorldEditor.NetDataFetchManager.Instance.UploadContentPackage(contentId, contentPackageId, updateDesc,
              resourceList, new ARWorldEditor.OnOasisNetworkDataFetchCallback<UploadContentPackageResponseData>((UploadContentPackageResponseData response) =>
              {
                  ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentPublishSuccess, 1f);
                  OnUploadContentSuccess(response, contentId);
              }, (string code, string msg) =>
              {
                  ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentPublishFail + "\nError code:\n" + code + "\n错误信息：\n" + msg, 1f);
                  OnUploadContentError(code, msg);
              }));
            }),this);

          
        }

        /// <summary>
        /// 返回资源列表
        /// </summary>
        /// <param name="platformType"></param>
        /// <returns></returns>
        private IEnumerator  GetExportPackageList(EngineType engineType, PlatformType platformType, int devicesType, Action<List<ContentPackageData>> callback)
        {
            string bucketName = NOSConfig.GetNosBucket();
            List<ContentPackageData> list = new List<ContentPackageData>();

            List<ExportScenePath> scenePaths = GetFullExportScenePath(engineType, platformType, devicesType);
            if (scenePaths == null || scenePaths.Count == 0)
            {
                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.Error_PackageIsNull, 1f);
                callback(list);
                yield break;
            }

            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentReadyToUpload, 0.7f);
            yield return new WaitForSecondsRealtime(0.1f);
            for (int i = 0; i < scenePaths.Count; i++)
            {
                //默认zip后缀
                string uuId = ARWorldEditor.RandomUtility.GUID()+".zip";
                //NosUtility.Instance.PutObject(bucketName, uuId, scenePaths[i].path);
                NosUtility.Instance.UploadPart(bucketName, uuId, scenePaths[i].path);

                long fileLength = ARWorldEditor.FileUtility.GetFileLength(scenePaths[i].path);
                if (fileLength > MAX_FILE_LENGTH)
                {
                    ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.Error_SceneZipOver100MB, 1f);
                    Debug.LogError("scene zip file can't exceed 100mb");
                    callback(null);
                    yield break;
                }
                ContentPackageData contentPackage = GetContentPackage(fileLength, uuId, (int)scenePaths[i].type, scenePaths[i].path);
                list.Add(contentPackage);
            }
            ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.ContentFinishUpload, 0.9f);
            callback(list);
            yield break;
        }

        /// <summary>
        /// get export scene path
        /// </summary>
        /// <param name="engineType"></param>
        /// <param name="platformType"></param>
        /// <returns></returns>
        private List<ExportScenePath> GetFullExportScenePath(EngineType engineType, PlatformType platformType,int devicesType)
        {
            List<ExportScenePath> exportPaths = new List<ExportScenePath>();
            string bundlePath = "";

            bool buildSceneSuccess = false;
            if (platformType == PlatformType.PLATFORM_TYPE_ANDROID && devicesType == (int)ContentsDevicesType.Mobile)
            {

                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.AndroidBuildStart,0.2f);
                if (engineType == EngineType.ENGINE_TYPE_SDK) buildSceneSuccess =ExporterMenu.ExportInsightBundleAndroidProduct(out bundlePath);
                else buildSceneSuccess = ExporterSceneTool.ExportSceneAndroid(out bundlePath);
                if(!buildSceneSuccess)
                {
                    EditorUtility.DisplayDialog("提示", "场景导出失败，请检查工程设置", "确认");
                    return null;
                }
                ExportScenePath scenePath = GetExportScenePath(bundlePath, platformType);
                exportPaths.Add(scenePath);
                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.AndroidFinishBuild, 0.5f);
            }
            else if (platformType == PlatformType.PLATFORM_TYPE_IOS && devicesType == (int)ContentsDevicesType.Mobile)
            {
                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.IOSBuildStart, 0.2f);
                if (engineType == EngineType.ENGINE_TYPE_SDK) buildSceneSuccess = ExporterMenu.ExportInsightBundleiOSProduct(out bundlePath);
                else buildSceneSuccess = ExporterSceneTool.ExportSceneiOS(out bundlePath);

                if (!buildSceneSuccess)
                {
                    EditorUtility.DisplayDialog("提示", "场景导出失败，请检查工程设置", "确认");
                    return null;
                }

                ExportScenePath scenePath = GetExportScenePath(bundlePath, platformType);
                exportPaths.Add(scenePath);
                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.IOSFinishBuild, 0.5f);
            }
            else if(platformType == PlatformType.PLATFORM_TYPE_ANDROID && devicesType == (int)ContentsDevicesType.SimGlass)
            {
                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.SimGlassBuildStart, 0.2f);
                if (engineType == EngineType.ENGINE_TYPE_SDK) buildSceneSuccess = ExporterMenu.ExportInsightBundleAndroidProduct(out bundlePath);
                else buildSceneSuccess = ExporterSceneTool.ExportSceneSimGlass(out bundlePath);
                if (!buildSceneSuccess)
                {
                    EditorUtility.DisplayDialog("提示", "场景导出失败，请检查工程设置", "确认");
                    return null;
                }
                ExportScenePath scenePath = GetExportScenePath(bundlePath, platformType);
                exportPaths.Add(scenePath);
                ExportProgressEditorWindow.SetCurrentProgressState(ExportProgressEditorWindow.SimGlassFinishBuild, 0.5f);
            }
            else if (platformType == PlatformType.PLATFORM_TYPE_ALL && devicesType == (int)ContentsDevicesType.Mobile)
            {
               
                bool isAndroidPlatform = IsPlatformAndroid();
                ExportProgressEditorWindow.SetCurrentProgressState(isAndroidPlatform?
                    ExportProgressEditorWindow.AndroidBuildStart: ExportProgressEditorWindow.IOSBuildStart, 0.2f);


                if (engineType == EngineType.ENGINE_TYPE_SDK)
                {
                    if (isAndroidPlatform) buildSceneSuccess = ExporterMenu.ExportInsightBundleAndroidProduct(out bundlePath);
                    else buildSceneSuccess = ExporterMenu.ExportInsightBundleiOSProduct(out bundlePath);
                }
                else
                {
                  
                    if (isAndroidPlatform) buildSceneSuccess = ExporterSceneTool.ExportSceneAndroid(out bundlePath);
                    else buildSceneSuccess = ExporterSceneTool.ExportSceneiOS(out bundlePath);
                }
                if (!buildSceneSuccess)
                {
                    EditorUtility.DisplayDialog("提示", "场景导出失败，请检查工程设置", "确认");
                    return null;
                }

                ExportProgressEditorWindow.SetCurrentProgressState(isAndroidPlatform ?
                  ExportProgressEditorWindow.AndroidFinishBuild : ExportProgressEditorWindow.IOSFinishBuild, 0.4f);

                ExportScenePath scenPath = isAndroidPlatform ? GetExportScenePath(bundlePath, PlatformType.PLATFORM_TYPE_ANDROID) :
                    GetExportScenePath(bundlePath, PlatformType.PLATFORM_TYPE_IOS);
                exportPaths.Add(scenPath);

                //切换到另外一个平台
             
                bool isiOSPlatform = isAndroidPlatform;
                ExportProgressEditorWindow.SetCurrentProgressState(isiOSPlatform ?
                 ExportProgressEditorWindow.IOSBuildStart : ExportProgressEditorWindow.AndroidBuildStart, 0.4f);
                if (engineType == EngineType.ENGINE_TYPE_SDK)
                {
                    if (isiOSPlatform) buildSceneSuccess = ExporterMenu.ExportInsightBundleiOSProduct(out bundlePath);
                    else buildSceneSuccess = ExporterMenu.ExportInsightBundleAndroidProduct(out bundlePath);
                }
                else
                {
                    if (isiOSPlatform) buildSceneSuccess = ExporterSceneTool.ExportSceneiOS(out bundlePath);
                    else buildSceneSuccess = ExporterSceneTool.ExportSceneAndroid(out bundlePath);
                }

                if (!buildSceneSuccess)
                {
                    EditorUtility.DisplayDialog("提示", "场景导出失败，请检查工程设置", "确认");
                    return null;
                }
                ExportProgressEditorWindow.SetCurrentProgressState(isiOSPlatform ?
                    ExportProgressEditorWindow.IOSFinishBuild : ExportProgressEditorWindow.AndroidFinishBuild, 0.7f);
                ExportScenePath scenPath1 = isiOSPlatform ? GetExportScenePath(bundlePath, PlatformType.PLATFORM_TYPE_IOS) :
     GetExportScenePath(bundlePath, PlatformType.PLATFORM_TYPE_ANDROID);
                exportPaths.Add(scenPath1);
            }

            return exportPaths;
        }

        /// <summary>
        /// 判断是否为安卓平台
        /// </summary>
        /// <returns></returns>
        private bool IsPlatformAndroid()
        {
#if UNITY_ANDROID
            return true;
#else
            return false;

#endif
        }

        /// <summary>
        /// 返回导出场景路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="platformType"></param>
        /// <returns></returns>
        private ExportScenePath GetExportScenePath(string path,PlatformType platformType)
        {
            ExportScenePath scenePath = new ExportScenePath();
            scenePath.path = Path.GetFullPath(path);
            scenePath.type = platformType;
            return scenePath;
        }

        /// <summary>
        /// 返回内容包信息
        /// </summary>
        /// <param name="fileLength"></param>
        /// <param name="uuId"></param>
        /// <param name="platform"></param>
        /// <param name="fileToUpload"></param>
        /// <returns></returns>
        private ContentPackageData GetContentPackage(long fileLength,string uuId,int platform, string fileToUpload)
        {
            ContentPackageData contentPackage = new ContentPackageData();
            contentPackage.md5 = ARWorldEditor.EncodeUtility.GetMD5HashFromFile(fileToUpload);
            contentPackage.platform = platform;
            contentPackage.nosObj = uuId;
            contentPackage.size = fileLength;
            contentPackage.name = Path.GetFileName(fileToUpload);
            return contentPackage;
        }

        /// <summary>
        /// 上传成功
        /// </summary>
        /// <param name="response"></param>
        /// <param name="contentId"></param>
        private void OnUploadContentSuccess(UploadContentPackageResponseData response, long contentId)
        {
            Debug.Log("upload content success");
#if DEBUG_TEST
            Application.OpenURL(string.Format("http://ar-world-test.netease.com/content/detail/{0}", contentId));
#else
            Application.OpenURL(string.Format("http://arworld.netease.com/content/detail/{0}", contentId));
#endif
        }

        /// <summary>
        /// 上传失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        private void OnUploadContentError(string code, string msg)
        {
            string errorStr = "upload content error " + code + " " + msg;
            Debug.Log(errorStr);
            EditorUtility.DisplayDialog("上传异常", errorStr, "确认");

            //会话失效，打开登陆界面
            if (code == ServerResponseCode.RESPONSE_SESSION_INVALIDATION)
            {
                isRunning = false;
                UserController.ReEnterLoginView();
            }
        }

        /// <summary>
        /// 点击取消按钮
        /// </summary>
        private void OnClickCancelHandler()
        {
            _instance.Close();
        }
#endregion
    }
    public class ExportScenePath
    {
        public string path;
        public PlatformType type;
    }
}
#endif
