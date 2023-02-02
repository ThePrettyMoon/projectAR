using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class CameraPathCreateFactory
{
    public static void Create()
    {
        bool checkResult = ARSession.ExportSceneWindow.LoadMyContent()?.contentType == 1;
        if (checkResult == false)
        {
            EditorUtility.DisplayDialog("错误", "当前内容类型不支持创建模拟路径！", "确认");
            return;
        }

        //在当前场景下创建一个path节点
        var root = GameObject.Find("World");
        if (root == null)
        {
            EditorUtility.DisplayDialog("错误", "未找到指定的根路径！", "确认");
            return;
        }
        var land = root.transform.Find("Land");
        if (land == null)
        {
            EditorUtility.DisplayDialog("错误", "未找到指定的根路径！", "确认");
            return;
        }
       

        //创建节点
        var pathObj = new GameObject(GetName(land)).transform;
        pathObj.parent = land;
        var cameraPath = pathObj.gameObject.AddComponent<CameraPath>();
        var cameraPathMap = pathObj.gameObject.AddComponent<CameraPathMap>();
        var anim = pathObj.gameObject.AddComponent<CameraPathAnimator>();

        cameraPath.InitByCameraPathFactorty();
        //创建联系
        cameraPathMap.Init(cameraPath, anim);

        UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(cameraPath, true);
        UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(cameraPathMap, true);
        UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(anim, false);

        if (Camera.main != null)
            anim.animationObject = Camera.main.transform;

        Selection.objects = new Object[] { pathObj.gameObject };
        SceneView.lastActiveSceneView.FrameSelected();
    }
    //指定路径的名字
    static string GetName(Transform parent)
    {
        int index = 1;
        while (true)
        {
            bool repeat = false;
            string name = "path " + index;
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child.name.Contains(name))
                {
                    index++;
                    repeat = true;
                    break;
                }
            }

            if (repeat == false)
            {
                return name;
            }
        }
    }


}
#endif