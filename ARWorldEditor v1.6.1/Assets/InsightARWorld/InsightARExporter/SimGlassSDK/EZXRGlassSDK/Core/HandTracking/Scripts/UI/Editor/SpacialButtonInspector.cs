using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EZXR.Glass.UI
{
    [CustomEditor(typeof(SpacialButton))]
    [CanEditMultipleObjects]
    public class SpacialButtonInspector : Editor
    {
        SerializedProperty positionType;
        SerializedProperty size;
        SerializedProperty text;
        SerializedProperty pressed;
        //SerializedProperty defaultMaterial;
        //SerializedProperty sharedMaterial;
        SerializedProperty color;
        SerializedProperty texture;
        SerializedProperty icon;
        SerializedProperty preTriggerZoneThickness;
        SerializedProperty rearTriggerZoneThickness;
        SerializedProperty clicked;
        SerializedProperty sortingOrder;
        SpacialButton _target;

        private void OnEnable()
        {
            _target = target as SpacialButton;
            positionType = serializedObject.FindProperty("positionType");
            size = serializedObject.FindProperty("size");
            text = serializedObject.FindProperty("text");
            pressed = serializedObject.FindProperty("pressed");
            //defaultMaterial = serializedObject.FindProperty("defaultMaterial");
            //sharedMaterial = serializedObject.FindProperty("sharedMaterial");
            color = serializedObject.FindProperty("color");
            texture = serializedObject.FindProperty("texture");
            icon = serializedObject.FindProperty("icon");
            preTriggerZoneThickness = serializedObject.FindProperty("preTriggerZoneThickness");
            rearTriggerZoneThickness = serializedObject.FindProperty("rearTriggerZoneThickness");
            clicked = serializedObject.FindProperty("clicked");
            sortingOrder = serializedObject.FindProperty("sortingOrder");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(positionType);
            EditorGUILayout.PropertyField(size, new GUIContent("Size（单位：m）"));
            EditorGUILayout.PropertyField(text);
            EditorGUILayout.PropertyField(pressed);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    Undo.RecordObject(_target, "Changed PanelSize");
            //}
            //EditorGUILayout.PropertyField(defaultMaterial);
            //EditorGUILayout.PropertyField(sharedMaterial);
            EditorGUILayout.PropertyField(color);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(texture);
                if (GUILayout.Button("Set Native Size"))
                {
                    Undo.RecordObject(_target, "Set Native Size");
                    _target.size = new Vector3(_target.texture.width * SpacialUIController.Instance.unitsPerPixel, _target.texture.height * SpacialUIController.Instance.unitsPerPixel, _target.size.z);
                    SceneView.RepaintAll();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(icon);
                if (GUILayout.Button("Set Native Size"))
                {
                    Undo.RecordObject(_target, "Set Native Size");
                    _target.iconSize = new Vector3(_target.icon.width * SpacialUIController.Instance.unitsPerPixel, _target.icon.height * SpacialUIController.Instance.unitsPerPixel, _target.iconSize.z);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(preTriggerZoneThickness, new GUIContent("前置触发区厚度（单位：m）"));
            EditorGUILayout.PropertyField(rearTriggerZoneThickness, new GUIContent("后置触发区厚度（单位：m）"));
            EditorGUILayout.PropertyField(clicked);
            EditorGUILayout.PropertyField(sortingOrder);
            serializedObject.ApplyModifiedProperties();

            SceneView.RepaintAll();
        }
    }
}