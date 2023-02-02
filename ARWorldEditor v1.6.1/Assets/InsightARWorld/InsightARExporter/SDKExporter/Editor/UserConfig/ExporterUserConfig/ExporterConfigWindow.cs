using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace UserConfig
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    public class ExporterConfigWindow : BaseConfigWindow 
    {
        const string tempDictionary = "InsightExporterConfig/";
        const string jsonConfigFileName = "-config.json";

        // Data Model
		static ExporterData data = null;

        // 显示在主面板上的动态资源列表
        static int prefabsCount = 0;
        static List<SceneAsset> prefabs;

        // 显示在主面板上的sharelogo
        static Texture2D shareLogo;

        // 显示在主面板上的默认JS代码库列表
        static List<string> JSLibrariesName;
        static List<bool> JSLibrariesStatus;

        // 显示在主面板上的自定义JS代码库列表

        static int JSLibrariesCustomCount = 0;
        static List<TextAsset> JSLibrariesCustomAsset;
        static List<bool> JSLibrariesCustomStatus;

        // 导出配置主窗口
        static ExporterConfigWindow exporterConfigWindow = null;

        new public static void ConfigDialog()
        {
            BaseConfigWindow.ConfigDialog();

            GetDataFromJson(out data, out prefabs, out shareLogo, out JSLibrariesCustomAsset);

            // Get existing open window or if none, make a new one:
            exporterConfigWindow = (ExporterConfigWindow) EditorWindow.GetWindow(typeof(ExporterConfigWindow), true, "导出工具配置");
            exporterConfigWindow.Show();
        }

        public static void GetDataFromJson(out ExporterData data, out List<SceneAsset> prefabs, out Texture2D shareLogo, out List<TextAsset> JSLibrariesCustomAsset)
        {
            data = null;
            prefabs = new List<SceneAsset>();
            JSLibrariesName = new List<string>();
            JSLibrariesStatus = new List<bool>();
            JSLibrariesCustomAsset = new List<TextAsset>();
            JSLibrariesCustomStatus = new List<bool>();
            shareLogo = null;

            try
			{
				string configContent = File.ReadAllText(GetTempFilePrefix(tempDictionary) + jsonConfigFileName, System.Text.Encoding.UTF8);
				data = JsonUtility.FromJson<ExporterData>(configContent);
			}
            catch (System.Exception)
			{
				data = new ExporterData();
			}
            finally
            {
                // 通过路径恢复动态资源包
                if (null != data.PrefabPaths)
                {
                    prefabsCount = data.PrefabPaths.Length;
                    for (int i = 0; i < prefabsCount; i ++)
                    {
                        if (string.Empty != data.PrefabPaths[i])
                        {
                            try
                            {
                                prefabs.Add((SceneAsset) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(data.PrefabPaths[i]), typeof(SceneAsset)));
                            }
                            catch (System.Exception)
                            {
								UnityEditor.EditorUtility.DisplayDialog (RenderEngine.ExporterConfig.TITLE
									, "找不到动态资源包，请前往菜单Exporter/Exporter Config中重新指定"
									, "OK!");
                            }
                        }
                    }
                }
                else
                    prefabsCount = 0;

                // 通过路径恢复shareLogo
                if (null != data.ShareLogoTexturePath)
                {
                    try
                    {
                        shareLogo = (Texture2D) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(data.ShareLogoTexturePath), typeof(Texture2D));
                    }
                    catch (System.Exception)
                    {
						UnityEditor.EditorUtility.DisplayDialog (RenderEngine.ExporterConfig.TITLE
							, "找不到ShareLogo，请前往菜单Exporter/Exporter Config中重新指定"
							, "OK!");
                    }
                }

                foreach (string path in Directory.GetFiles(RenderEngine.ExporterConfig.JS_LIBRARY_PATH))
                    if (System.IO.Path.GetExtension(path).ToLower().Equals(".js"))
                    {
                        bool found = false;
                        var name = System.IO.Path.GetFileName(path);
                        if(null != data.JSLibrariesName)
                            foreach (var item in data.JSLibrariesName) if (item == name) { found = true; break; }

                        JSLibrariesName.Add(name);
                        JSLibrariesStatus.Add(found);
                    }
                
                if (null != data.JSLibrariesCustomPath)
                {
                    JSLibrariesCustomCount = data.JSLibrariesCustomPath.Length;
                    for (int i = 0; i < JSLibrariesCustomCount; i ++)
                    {
                        JSLibrariesCustomStatus.Add(data.JSLibrariesCustomStatus[i]);
                        if (string.Empty != data.JSLibrariesCustomPath[i])
                        {
                            try
                            {
                                JSLibrariesCustomAsset.Add((TextAsset) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(data.JSLibrariesCustomPath[i]), typeof(TextAsset)));
                            }
                            catch (System.Exception)
                            {
                                UnityEditor.EditorUtility.DisplayDialog (RenderEngine.ExporterConfig.TITLE
                                    , "找不到JS代码库" + data.JSLibrariesCustomPath[i] + "，请前往菜单Exporter/Exporter Config中重新指定"
                                    , "OK!");
                            }
                        }
                        else
                            JSLibrariesCustomAsset.Add(null);
                    }
                }
                else
                    JSLibrariesCustomCount = 0;
            }
        }

        void OnGUI()
        {
            // EditorGUILayout.Space();
            // data.ExportType = EditorGUILayout.IntPopup("资源导出格式：", data.ExportType, new string[] {"SDK", "APP"}, new int[] {0, 1});

            EditorGUILayout.Space();
			GUILayout.Label( "资源导出路径：");
			EditorGUILayout.BeginHorizontal();
            GUILayout.Label("SDK-Scene-iOS", GUILayout.Width(120));
			RenderEngine.ExporterConfig.IOSDestinationPath = EditorGUILayout.TextField(RenderEngine.ExporterConfig.IOSDestinationPath);
			if( GUILayout.Button("重置") )
            {
				RenderEngine.ExporterConfig.IOSDestinationPath = RenderEngine.ExporterConfig.defaultIOSDestinationPath;
                GUI.FocusControl("");
            }
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
            GUILayout.Label("SDK-Scene-Android", GUILayout.Width(120));
			RenderEngine.ExporterConfig.AndroidDestinationPath = EditorGUILayout.TextField(RenderEngine.ExporterConfig.AndroidDestinationPath);
			if( GUILayout.Button("重置") )
            {
				RenderEngine.ExporterConfig.AndroidDestinationPath = RenderEngine.ExporterConfig.defaultAndroidDestinationPath;
                GUI.FocusControl("");
            }
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
            GUILayout.Label("SDK/APP-Bundle", GUILayout.Width(120));
			RenderEngine.ExporterConfig.BundleDestinationPath = EditorGUILayout.TextField(RenderEngine.ExporterConfig.BundleDestinationPath);
			if( GUILayout.Button("重置") )
            {
				RenderEngine.ExporterConfig.BundleDestinationPath = RenderEngine.ExporterConfig.defaultBundleDestinationPath;
                GUI.FocusControl("");
            }
			EditorGUILayout.EndHorizontal();
            RenderEngine.ExporterConfig.OpenTargetFolder = EditorGUILayout.Toggle("完成后打开文件夹", RenderEngine.ExporterConfig.OpenTargetFolder);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("动态资源包（请将场景文件拖入此处）：");
            prefabsCount = EditorGUILayout.IntField("数量：", prefabsCount < 0 ? 0 : prefabsCount);
            if (prefabsCount > prefabs.Count)
            {
                int count = prefabsCount - prefabs.Count;
                while (0 < count --)
                    prefabs.Add(null);
            }
            else if (prefabsCount < prefabs.Count)
            {
                int count = prefabs.Count - prefabsCount;
                while (0 < count --)
                    prefabs.RemoveAt(prefabs.Count - 1);
            }
            for (int i = 0; i < prefabsCount; i ++)
                prefabs[i] = (SceneAsset) EditorGUILayout.ObjectField("场景文件", prefabs[i], typeof(SceneAsset), true);

            // SerializedObject so = new SerializedObject(this);
            // SerializedProperty prefabsProperty = so.FindProperty("prefabs");
            // EditorGUILayout.PropertyField(prefabsProperty, new GUIContent("动态资源包（请将场景文件拖入此处）："), true);
            // so.ApplyModifiedProperties();
            GUILayout.Label("备注：请将动态资源文件命名为model0, model1, model2, ... , modelN");

            EditorGUILayout.Space();
            shareLogo = (Texture2D) EditorGUILayout.ObjectField("分享Logo", shareLogo, typeof(Texture2D), true);
            data.ShareLogoTexturePath = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(shareLogo));

            EditorGUILayout.Space();
            GUILayout.Label( "默认JS资源库：" );
            for (int i = 0; i <JSLibrariesName.Count; i ++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(JSLibrariesName[i], GUILayout.Width(200));
                GUILayout.Label("自动", GUILayout.Width(25));
                JSLibrariesStatus[i] = !EditorGUILayout.Toggle(!JSLibrariesStatus[i]);
                GUILayout.Label("总是", GUILayout.Width(25));
                JSLibrariesStatus[i] = EditorGUILayout.Toggle(JSLibrariesStatus[i]);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
            GUILayout.Label( "说明：" );
            GUILayout.Label( "     “自动”表示检测到代码里有require时则导出该库" );
            GUILayout.Label( "     “总是”表示总是导出该库" );
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("自定义JS资源库（请将JS代码文件拖入此处）：");
            JSLibrariesCustomCount = EditorGUILayout.IntField("数量：", JSLibrariesCustomCount < 0 ? 0 : JSLibrariesCustomCount);
            if (JSLibrariesCustomCount > JSLibrariesCustomAsset.Count)
            {
                int count = JSLibrariesCustomCount - JSLibrariesCustomAsset.Count;
                while (0 < count --)
                {
                    JSLibrariesCustomAsset.Add(null);
                    JSLibrariesCustomStatus.Add(false);
                }

            }
            else if (JSLibrariesCustomCount < JSLibrariesCustomAsset.Count)
            {
                int count = JSLibrariesCustomAsset.Count - JSLibrariesCustomCount;
                while (0 < count --)
                {
                    JSLibrariesCustomAsset.RemoveAt(JSLibrariesCustomAsset.Count - 1);
                    JSLibrariesCustomStatus.RemoveAt(JSLibrariesCustomStatus.Count - 1);
                }
            }
            for (int i = 0; i < JSLibrariesCustomCount; i ++)
            {
                EditorGUILayout.BeginHorizontal();
                JSLibrariesCustomAsset[i] = (TextAsset) EditorGUILayout.ObjectField(JSLibrariesCustomAsset[i], typeof(TextAsset), false, GUILayout.Width(200));
                GUILayout.Label("自动", GUILayout.Width(25));
                JSLibrariesCustomStatus[i] = !EditorGUILayout.Toggle(!JSLibrariesCustomStatus[i]);
                GUILayout.Label("总是", GUILayout.Width(25));
                JSLibrariesCustomStatus[i] = EditorGUILayout.Toggle(JSLibrariesCustomStatus[i]);
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            GUILayout.Label("注意：删除场景的.meta文件会导致该配置清空！");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("保存"))
            {
                // 保存prefab路径信息
                if (null != prefabs)
                {
                    data.PrefabPaths = new string[prefabsCount];
                    for (int i = 0; i < prefabsCount; i ++)
                    {
                        if (null != prefabs[i])
                            data.PrefabPaths[i] = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(prefabs[i]));
                        else
                            data.PrefabPaths[i] = string.Empty;
                    }
                }

                // 保存默认JS资源库信息
                if (null != JSLibrariesName)
                {
                    int count = 0;
                    for (int i = 0; i <JSLibrariesName.Count; i ++) if (JSLibrariesStatus[i]) count ++;
                    data.JSLibrariesName = new string[count];
                    count = 0;
                    for (int i = 0; i <JSLibrariesName.Count; i ++)
                    if (JSLibrariesStatus[i])
                            data.JSLibrariesName[count ++] = JSLibrariesName[i];
                }

                // 保存自定义JS资源库信息
                if (null != JSLibrariesCustomAsset)
                {
                    data.JSLibrariesCustomPath = new string[JSLibrariesCustomCount];
                    data.JSLibrariesCustomStatus = new bool[JSLibrariesCustomCount];
                    for (int i = 0; i < JSLibrariesCustomCount; i ++)
                    {
                        if (null != JSLibrariesCustomAsset[i])
                            data.JSLibrariesCustomPath[i] = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(JSLibrariesCustomAsset[i]));
                        else
                            data.JSLibrariesCustomPath[i] = string.Empty;

                        data.JSLibrariesCustomStatus[i] = JSLibrariesCustomStatus[i];
                    }
                }

                // 写入json
                WriteFile(GetTempFilePrefix(tempDictionary) + jsonConfigFileName, JsonUtility.ToJson(data, true));

                // 写入PlayerPref
                PlayerPrefs.SetInt("GLTFOpenTargetFolder", RenderEngine.ExporterConfig.OpenTargetFolder ? 1 : 0);

                exporterConfigWindow.Close();
                // UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
                Debug.Log("导出工具配置保存成功！");
            }
            if (GUILayout.Button("取消"))
                exporterConfigWindow.Close();
            EditorGUILayout.EndHorizontal();
        }

    }
}
