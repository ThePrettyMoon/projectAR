#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ARWorldEditor
{
    public static class SerializedPropertyExtensions 
    {
        public static void SetObjectValue(this SerializedProperty prop, System.Object toValue)
        {
            switch (prop.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    prop.boolValue = (bool)toValue;
                    break;
                case SerializedPropertyType.Bounds:
                    prop.boundsValue = (Bounds)toValue;
                    break;
                case SerializedPropertyType.Color:
                    prop.colorValue = (Color)toValue;
                    break;
                case SerializedPropertyType.Float:
                    prop.floatValue = (float)toValue;
                    break;
                case SerializedPropertyType.Integer:
                    prop.intValue = (int)toValue;
                    break;
                case SerializedPropertyType.ObjectReference:
                    prop.objectReferenceValue = toValue as UnityEngine.Object;
                    break;
                case SerializedPropertyType.Rect:
                    prop.rectValue = (Rect)toValue;
                    break;
                case SerializedPropertyType.String:
                    prop.stringValue = (string)toValue;
                    break;
                case SerializedPropertyType.Vector2:
                    prop.vector2Value = (Vector2)toValue;
                    break;
                case SerializedPropertyType.Vector3:
                    prop.vector3Value = (Vector3)toValue;
                    break;
            }
        }
    }
}
#endif
