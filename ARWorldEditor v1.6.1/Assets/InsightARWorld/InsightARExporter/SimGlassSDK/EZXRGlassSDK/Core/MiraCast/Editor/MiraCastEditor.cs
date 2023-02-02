using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace EZXR.Glass.MiraCast
{
    [InitializeOnLoad]
    public class MiraCastEditor : EditorWindow
    {
        static AddRequest addRequest;
        static RemoveRequest removeRequest;
        static Queue<string> packageNameQueue;

        static ARProjectConfig config;

        /// <summary>
        /// 进行所有代码之前必须先执行的初始化操作，取到ARSDK的配置文件
        /// </summary>
        static void Init()
        {
            if (config == null)
            {
                string filePath = "Assets/EZXRGlassSDK/Editor/ARProjectConfig.asset";
                if ((AssetDatabase.LoadAssetAtPath(filePath, typeof(ARProjectConfig)) as ARProjectConfig) == null)
                {
                    filePath = AssetDatabase.GUIDToAssetPath("892b737a1963162488e8f0164f77b58f");
                }
                config = AssetDatabase.LoadAssetAtPath(filePath, typeof(ARProjectConfig)) as ARProjectConfig;
            }
        }

        [MenuItem("ARSDK/Additional Abilities/MiraCast/Enable", false, 70)]
        public static void EnableMiraCast()
        {
            if (EditorUtility.DisplayDialog("Importing Package", "Start Import Packages: \n1) com.unity.renderstreaming@3.1.0-exp.2\n2) com.unity.inputsystem@1.0.2\n3) com.unity.webrtc@2.4.0-exp.4\n\n\nPlease make sure that this will not make conflicts!", "Yes"))
            {
                EditorApplication.update += PackageAddProgress;

                EditorUtility.DisplayProgressBar("Importing Package", "Is importing package, please wait...", 0);

                addRequest = Client.Add("com.unity.renderstreaming@3.1.0-exp.2");
            }
        }
        [MenuItem("ARSDK/Additional Abilities/MiraCast/Enable", true, 70)]
        static bool ValidateEnableMiraCast()
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            return !symbols.Contains("ARMiracast");
        }

        [MenuItem("ARSDK/Additional Abilities/MiraCast/Disable", false, 70)]
        static void StartRemovingPackages()
        {
            //去掉ScriptingDefineSymbols中的ARMiracast，避免移除package的时候会因为引用丢失而报错
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (symbols.Contains("ARMiracast;"))
            {
                symbols = symbols.Replace("ARMiracast;", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
            }
            else if (symbols.Contains("ARMiracast"))
            {
                symbols = symbols.Replace("ARMiracast", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
            }

            AssetDatabase.SaveAssets();

            //如果场景中存在MiracastManager，则删除
            MiracastManager miracastManager = FindObjectOfType<MiracastManager>();
            if (miracastManager != null)
            {
                DestroyImmediate(miracastManager.gameObject);
            }

            //如果场景中存在PhoneScreenUI/Canvas/MiracastUI，则删除
            PhoneUIRenderer phoneScreenUI = FindObjectOfType<PhoneUIRenderer>();
            if (phoneScreenUI != null)
            {
                Transform miracastUI = phoneScreenUI.transform.Find("Canvas/MiracastUI");
                if (miracastUI != null)
                {
                    DestroyImmediate(miracastUI.gameObject);
                }
            }

            packageNameQueue = new Queue<string>();
            packageNameQueue.Enqueue("com.unity.renderstreaming");

            // callback for every frame in the editor
            EditorApplication.update += PackageRemovalProgress;
            EditorApplication.LockReloadAssemblies();

            EditorUtility.DisplayProgressBar("Removing Package", "Is removing package, please wait...", 0);

            var nextRequestStr = packageNameQueue.Dequeue();
            removeRequest = Client.Remove(nextRequestStr);

            return;
        }
        [MenuItem("ARSDK/Additional Abilities/MiraCast/Disable", true, 70)]
        static bool ValidateStartRemovingPackages()
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            return symbols.Contains("ARMiracast");
        }

        static void PackageAddProgress()
        {
            if (addRequest != null && addRequest.IsCompleted)
            {
                switch (addRequest.Status)
                {
                    case StatusCode.Failure:
                        EditorUtility.ClearProgressBar();
                        Debug.LogError("Couldn't add package '" + "': " + addRequest.Error.message);
                        break;

                    case StatusCode.InProgress:
                        break;

                    case StatusCode.Success:
                        EditorUtility.ClearProgressBar();
                        Debug.Log("Added package: " + addRequest.Result.name);

                        //向ScriptingDefineSymbols添加ARMiracast
                        var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
                        if (!symbols.Contains("ARMiracast"))
                        {
                            symbols += ";ARMiracast";

                            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbols);
                        }

                        AssetDatabase.SaveAssets();

                        //如果场景中不存在MiracastManager的话需要实例化一个
                        if (FindObjectOfType<MiracastManager>() == null)
                        {
                            Object obj = PrefabUtility.InstantiatePrefab(ResourcesManager.Load<GameObject>("MiracastManager"));
                            obj.name = "MiracastManager";
                        }

                        //如果场景中不存在PhoneScreenUI的话需要实例化一个，并且向其下的Canvas添加MiracastUI
                        PhoneUIRenderer phoneScreenUI = FindObjectOfType<PhoneUIRenderer>();
                        if (phoneScreenUI == null)
                        {
                            phoneScreenUI = (PrefabUtility.InstantiatePrefab(ResourcesManager.Load<GameObject>("PhoneScreenUI")) as GameObject).GetComponent<PhoneUIRenderer>();
                            phoneScreenUI.name = "PhoneScreenUI";
                        }
                        if (phoneScreenUI != null)
                        {
                            PrefabUtility.InstantiatePrefab(ResourcesManager.Load<GameObject>("MiracastUI"), phoneScreenUI.transform.Find("Canvas")).name = "MiracastUI";
                        }

                        Init();
                        config.miraCast = true;
                        break;
                }

                EditorApplication.update -= PackageAddProgress;
            }
        }

        static void PackageRemovalProgress()
        {
            if (removeRequest.IsCompleted)
            {
                switch (removeRequest.Status)
                {
                    case StatusCode.Failure:    // couldn't remove package
                        EditorUtility.ClearProgressBar();
                        Debug.LogError("Couldn't remove package '" + removeRequest.PackageIdOrName + "': " + removeRequest.Error.message);
                        break;

                    case StatusCode.InProgress:
                        break;

                    case StatusCode.Success:
                        EditorUtility.ClearProgressBar();
                        Debug.Log("Removed package: " + removeRequest.PackageIdOrName);

                        Init();
                        config.miraCast = false;
                        break;
                }

                if (packageNameQueue.Count > 0)
                {
                    var nextRequestStr = packageNameQueue.Dequeue();
                    Debug.Log("Requesting removal of '" + nextRequestStr + "'.");
                    removeRequest = Client.Remove(nextRequestStr);

                }
                else
                {    // no more packages to remove
                    EditorApplication.update -= PackageRemovalProgress;
                    EditorApplication.UnlockReloadAssemblies();
                }
            }

            return;
        }
    }
}