using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[InitializeOnLoad]
public class LayerChangeListener
{

    /*
       编辑器，物体layer层的设置，10-20层SDK使用，用户在>20层进行设置，
      8-PostProcessing
      9-RealityScene
      10-bgCamera
      11-maskCamera
      12-arCamera
      13-panorama
       */

    /// <summary>
    /// 内部设置Layer在这里添加，否则会被替换
    /// </summary>
    public static Dictionary<int, string> InnerSettingLayers = new Dictionary<int, string>()
    {
        [8] = "PostProcessing",
        [9] = "RealityScene",
        [10] = "bgCamera",
        [11] = "maskCamera",
        [12] = "arCamera",
        [13] = "panorama",
    };
    /// <summary>
    /// 内部设置Tag在这里添加，否则会被替换
    /// </summary>
    public static Dictionary<int, string> InnerSettingTag = new Dictionary<int, string>()
    {
        [0] = ARWorldEditor.ConfigGlobal.TAG_REALITYSCENE,
    };

    public static void SetLayerAndTag()
    {
        SetLayer();
        SetTag();
    }

    static void SetLayer()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layers = tagManager.FindProperty("layers");
        if (layers == null || !layers.isArray)
        {
            Debug.LogWarning("Can't set up the layers.  It's possible the format of the layers and tags data has changed in this version of Unity.");
            Debug.LogWarning("Layers is null: " + (layers == null));
            return;
        }
        //内部设置Layer层级
        foreach (var layer in InnerSettingLayers)
        {
            int value = layer.Key;

            if (value > 20)
            {
                Debug.Log("警告");
                EditorUtility.DisplayDialog("警告", "编辑器内部设置Layer 限制为 10-20层", "知道了");
                continue;
            }
            SerializedProperty layerSP = layers.GetArrayElementAtIndex(value);
            if (layerSP.stringValue == layer.Value)
            {
                //Debug.LogError("已设置该Layer:"+ item);
            }
            else
            {
                if (string.IsNullOrEmpty(layerSP.stringValue))
                {
                    layerSP.stringValue = layer.Value;
                }
                else
                {
                    Debug.Log("提示");
                    if (EditorUtility.DisplayDialog("提示", "当前Layer：" + layerSP.stringValue + "不等于 预设值：" + layer, "使用预设值", "忽略"))
                    {
                        layerSP.stringValue = layer.Value;
                    }
                }
            }
        }
        tagManager.ApplyModifiedProperties();
    }
    static void SetTag()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty it = tagManager.GetIterator();
        while (it.NextVisible(true))
        {
            if (it.name == "tags")
            {
                foreach (var item in InnerSettingTag)
                {
                    //Debug.LogError("setTag");
                    if (isHasTag(item.Value))
                        continue;
                    it.InsertArrayElementAtIndex(item.Key);
                    it.GetArrayElementAtIndex(item.Key).stringValue = item.Value;
                }
            }
        }
        tagManager.ApplyModifiedProperties();
    }
    static bool isHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
                return true;
        }
        return false;
    }
    static bool isHasLayer(string layer)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                return true;
        }

        return false;
    }

}
