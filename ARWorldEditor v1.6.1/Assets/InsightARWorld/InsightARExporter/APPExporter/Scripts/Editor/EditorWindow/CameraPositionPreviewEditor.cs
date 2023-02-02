using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraPositionPreview))]
[CanEditMultipleObjects]
public class CameraPositionPreviewEditor : Editor
{
    SerializedProperty lerpSpeedProperty;
    SerializedProperty rotateOrientationProperty;
    SerializedProperty cameraIdProperty;
    SerializedProperty cameraIdsProperty;
    SerializedProperty cameraIdxProperty;
    List<string> cameraIdList;
    List<int> optionList;

    // Start is called before the first frame update
    private void OnEnable()
    {
        lerpSpeedProperty = serializedObject.FindProperty("lerpFrameCount");
        rotateOrientationProperty = serializedObject.FindProperty("rotateOrientation");
        cameraIdProperty = serializedObject.FindProperty("currentCameraId");
        cameraIdsProperty = serializedObject.FindProperty("cameraIds");
        cameraIdxProperty = serializedObject.FindProperty("currentCameraIdx");
        cameraIdList = new List<string>();
        optionList = new List<int>();
        for (int i = 0; i < cameraIdsProperty.arraySize; i++)
        {
            cameraIdList.Add(cameraIdsProperty.GetArrayElementAtIndex(i).intValue.ToString());
            optionList.Add(i);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.IntSlider(lerpSpeedProperty,1,1000, new GUIContent("插值帧数", "位姿间插值帧数"));
        rotateOrientationProperty.boolValue = EditorGUILayout.Toggle(new GUIContent("屏幕切换", "切换相机横竖屏"), rotateOrientationProperty.boolValue);

        if (cameraIdList != null && cameraIdList.Count > 0)
        {
            EditorGUILayout.LabelField(new GUIContent("相机列表", "相机列表下拉框"));
            cameraIdxProperty.intValue = EditorGUILayout.IntPopup(cameraIdxProperty.intValue, cameraIdList.ToArray(), optionList.ToArray());
            if (cameraIdList.Count > cameraIdxProperty.intValue)
            {
                cameraIdProperty.intValue = int.Parse(cameraIdList[cameraIdxProperty.intValue]);
            }
        }
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
