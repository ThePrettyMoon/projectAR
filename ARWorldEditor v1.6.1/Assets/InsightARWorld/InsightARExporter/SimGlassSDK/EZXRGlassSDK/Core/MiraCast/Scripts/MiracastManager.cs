using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ARMiracast
using Unity.RenderStreaming;
#endif
//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEditor.PackageManager;
//using UnityEditor.PackageManager.Requests;
//using PackageInfo = UnityEditor.PackageManager.PackageInfo;
//#endif

namespace EZXR.Glass.MiraCast
{
    [ExecuteInEditMode]
    public class MiracastManager : MonoBehaviour
    {
        #region 单例
        private static MiracastManager _instance;
        public static MiracastManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

#if ARMiracast
    public RenderStreaming renderStreaming;
#endif

        private void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(gameObject);

#if ARMiracast
        renderStreaming = GetComponent<RenderStreaming>();
#endif
        }

        //#if UNITY_EDITOR
        //    ListRequest listRequest;

        //    private void OnEnable()
        //    {
        //        EditorApplication.update += ClientListProgress;
        //        listRequest = Client.List(true, false);
        //    }

        //    void ClientListProgress()
        //    {
        //        if (listRequest.IsCompleted)
        //        {
        //            switch (listRequest.Status)
        //            {
        //                case StatusCode.Failure:
        //                    Debug.LogError(listRequest.Error.message);
        //                    break;

        //                case StatusCode.InProgress:
        //                    break;

        //                case StatusCode.Success:
        //                    PackageCollection packageInfos = listRequest.Result;
        //                    bool exists = false;
        //                    foreach (PackageInfo item in packageInfos)
        //                    {
        //                        //Debug.Log("package: " + item.name);
        //                        if (item.name == "com.unity.renderstreaming")
        //                        {
        //                            exists = true;
        //                            break;
        //                        }
        //                    }

        //                    if (!exists)
        //                    {
        //                        if (EditorUtility.DisplayDialog("Error", "MiraCast is not Enabled!\n\nDo you want to Enable it now?", "Yes", "No"))
        //                        {
        //                            MiraCastEditor.EnableMiraCast();
        //                        }
        //                    }
        //                    break;
        //            }

        //            EditorApplication.update -= ClientListProgress;
        //        }
        //    }
        //#endif
    }
}