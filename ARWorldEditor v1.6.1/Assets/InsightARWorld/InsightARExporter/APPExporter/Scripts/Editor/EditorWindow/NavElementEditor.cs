using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 1-主场景模式，2-POI导航模式，3-2D地图模式，4-导航模式
/// </summary>

[CustomEditor(typeof(NavElement))]
public class NavElementEditor :Editor
{
    private NavElement targetScript;
    private SerializedProperty groundArrowHeight;
    private SerializedProperty turnArrowHeight;
    private SerializedProperty upOrDownArrowHeight;
    private SerializedProperty destPointHeight;

    private SerializedProperty groundArrowPrefab;
    private SerializedProperty turnArrowPrefab;
    private SerializedProperty upOrDownArrowPrefab;
    private SerializedProperty destPointPrefab;

    private GUIContent groundArrow = new GUIContent
    {
        text = "贴地箭头",
    };

    private GUIContent turnArrow = new GUIContent
    {
        text = "转弯箭头",
    };

    private GUIContent upDownArrow = new GUIContent
    {
        text = "上下楼箭头"
    };
    private GUIContent finalIcon = new GUIContent
    {
        text = "终点图标",
    };
    private GUIContent height = new GUIContent
    {
        text = "高度:",
    };
    private GUIContent prefab = new GUIContent
    {
        text = "内容文件:",
    };
    private GUIContent reset = new GUIContent
    {
        text = "Reset",
        tooltip = "提示：点击会将所有属性还原到默认状态",
    };
    private GUIContent update = new GUIContent
    {
        text = "Update",
        tooltip = "提示：点击保存改动",
    };

    void OnEnable()
    {
        targetScript = (NavElement)target;

        groundArrowHeight = serializedObject.FindProperty("groundArrowHeight");
        groundArrowPrefab = serializedObject.FindProperty("groundArrowPrefab");

        turnArrowHeight = serializedObject.FindProperty("turnArrowHeight");
        turnArrowPrefab = serializedObject.FindProperty("turnArrowPrefab");

        upOrDownArrowHeight = serializedObject.FindProperty("upOrDownArrowHeight");
        upOrDownArrowPrefab = serializedObject.FindProperty("upOrDownArrowPrefab");

        destPointHeight = serializedObject.FindProperty("destPointHeight");
        destPointPrefab = serializedObject.FindProperty("destPointPrefab");

    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        #region groundArrow
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(groundArrow.text);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(height.text);
        groundArrowHeight.floatValue = EditorGUILayout.FloatField(groundArrowHeight.floatValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(prefab.text);
        groundArrowPrefab.objectReferenceValue = EditorGUILayout.ObjectField(groundArrowPrefab.objectReferenceValue, typeof(GameObject), allowSceneObjects: false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        #endregion

        #region turnArrow
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(turnArrow.text);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(height.text);
        turnArrowHeight.floatValue = EditorGUILayout.FloatField(turnArrowHeight.floatValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(prefab.text);
        turnArrowPrefab.objectReferenceValue = EditorGUILayout.ObjectField(turnArrowPrefab.objectReferenceValue, typeof(GameObject), allowSceneObjects: false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);
        #endregion

        #region upDownArrow
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(upDownArrow.text);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(height.text);
        upOrDownArrowHeight.floatValue = EditorGUILayout.FloatField(upOrDownArrowHeight.floatValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(prefab.text);
        upOrDownArrowPrefab.objectReferenceValue = EditorGUILayout.ObjectField(upOrDownArrowPrefab.objectReferenceValue, typeof(GameObject), allowSceneObjects: false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);
        #endregion

        #region finalIcon
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(finalIcon.text);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(height.text);
        destPointHeight.floatValue = EditorGUILayout.FloatField(destPointHeight.floatValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(prefab.text);
        destPointPrefab.objectReferenceValue = EditorGUILayout.ObjectField(destPointPrefab.objectReferenceValue, typeof(GameObject), allowSceneObjects: false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);
        #endregion


        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(reset))
        {
            targetScript.ElementReset();
        }

        if (GUILayout.Button(update))
        {
            targetScript.ElementUpdate();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }


}
