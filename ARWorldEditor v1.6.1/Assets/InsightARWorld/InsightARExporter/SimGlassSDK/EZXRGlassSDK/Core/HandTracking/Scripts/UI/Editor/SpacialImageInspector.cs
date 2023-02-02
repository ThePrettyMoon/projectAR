using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EZXR.Glass.UI
{
    [CustomEditor(typeof(SpacialImage))]
    [CanEditMultipleObjects]
    public class SpacialImageInspector : Editor
    {
        SerializedProperty positionType;
        SerializedProperty size;
        SerializedProperty text;
        //SerializedProperty sharedMaterial;
        SerializedProperty color;
        SerializedProperty texture;
        SerializedProperty sortingOrder;
        SpacialImage _target;

        private void OnEnable()
        {
            _target = target as SpacialImage;
            positionType = serializedObject.FindProperty("positionType");
            size = serializedObject.FindProperty("size");
            text = serializedObject.FindProperty("text");
            //defaultMaterial = serializedObject.FindProperty("defaultMaterial");
            //sharedMaterial = serializedObject.FindProperty("sharedMaterial");
            color = serializedObject.FindProperty("color");
            texture = serializedObject.FindProperty("texture");
            sortingOrder = serializedObject.FindProperty("sortingOrder");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(positionType);
            EditorGUILayout.PropertyField(size);
            EditorGUILayout.PropertyField(text);
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
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(sortingOrder);
            serializedObject.ApplyModifiedProperties();

            SceneView.RepaintAll();
        }
    }
}