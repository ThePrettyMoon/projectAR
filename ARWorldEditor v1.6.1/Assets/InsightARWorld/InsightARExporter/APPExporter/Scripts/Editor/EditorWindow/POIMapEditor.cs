using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(POIMap))]
public class POIMapEditor : Editor
{
    POIMap poiMapScript;
    private List<string> levelList;
    private List<int> levelIndexList;
    private bool showRealityList = false;
    private int lastRealityLevelIndex;
    private bool cancelPoiOperation = false; //是否发生poi撤销操作

    private bool showVirtualList = false;
    private int lastVirtualLevelIndex;

    private SerializedProperty realityPoiProperty;
    private SerializedProperty virtualPoiProperty;
    private SerializedProperty realityPoiListProperty;
    private SerializedProperty virtualPoiListProperty;
    private SerializedProperty levelProperty;
    private SerializedProperty mapIdProperty;
    private SerializedProperty contentForPoiListProperty;

    RealityPointsOfInterestSubLayerPropertiesDrawer realityPoiDrawer = new RealityPointsOfInterestSubLayerPropertiesDrawer();
    VirtualPointsOfInterestSubLayerPropertiesDrawer virtualPoiDrawer = new VirtualPointsOfInterestSubLayerPropertiesDrawer();


    private GUIContent realityTitle = new GUIContent
    {
        text = "Reality POI",
        tooltip = "geo POI list"
    };

    private GUIContent virtualTitle = new GUIContent
    {
        text = "Virtual POI",
        tooltip = "virtual POI list"
    };

    private GUIContent levelTitle = new GUIContent
    {
        text = "Level"
    };
    private GUIContent heightAboveGround = new GUIContent
    {
        text = "HeightAboveGround (m)"
    };
    private GUIContent utmZone = new GUIContent
    {
        text = "UTM Zone"
    };


    private void OnEnable()
    {
        poiMapScript = (POIMap)target;
        realityPoiProperty = serializedObject.FindProperty("realityPoiMap");
        virtualPoiProperty = serializedObject.FindProperty("virtualPoiMap");
        realityPoiListProperty = serializedObject.FindProperty("realityPoiList");
        virtualPoiListProperty = serializedObject.FindProperty("virtualPoiList");
        levelProperty = serializedObject.FindProperty("levelList");
        mapIdProperty = serializedObject.FindProperty("mapId");
        contentForPoiListProperty = serializedObject.FindProperty("contentForPoiList");
    }

    /// <summary>
    /// 自定义界面
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (levelList == null)
        {
            levelList = new List<string>();
            levelIndexList = new List<int>();
            for (int i = 0; i < levelProperty.arraySize; i++)
            {
                levelList.Add(levelProperty.GetArrayElementAtIndex(i).intValue.ToString());
                levelIndexList.Add(i);
            }
        }

        GUIStyle style = new GUIStyle("Button");
        style.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Map Id:");
        EditorGUILayout.LabelField(mapIdProperty.longValue.ToString());
        // 处理reality poi
        showRealityList = EditorGUILayout.Foldout(showRealityList, realityTitle);

        if (showRealityList)
        {
            EditorGUILayout.BeginHorizontal();
            float height = realityPoiProperty.FindPropertyRelative("heightAboveGround").floatValue = EditorGUILayout.FloatField(heightAboveGround.text, realityPoiProperty.FindPropertyRelative("heightAboveGround").floatValue);
            poiMapScript.UpdateHeightAboveGround(height);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(levelTitle);
            EditorGUI.BeginChangeCheck();
            realityPoiProperty.FindPropertyRelative("currentLevelIndex").intValue = EditorGUILayout.IntPopup(realityPoiProperty.FindPropertyRelative("currentLevelIndex").intValue, levelList.ToArray(), levelIndexList.ToArray());
            if (realityPoiProperty.FindPropertyRelative("currentLevelIndex").intValue < levelProperty.arraySize)
            {
                realityPoiProperty.FindPropertyRelative("currentLevel").intValue = levelProperty.GetArrayElementAtIndex(realityPoiProperty.FindPropertyRelative("currentLevelIndex").intValue).intValue;
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorHelper.CheckForModifiedProperty(realityPoiProperty);
            }


            bool needUpdateReality = realityPoiProperty.FindPropertyRelative("currentLevelIndex").intValue != lastRealityLevelIndex;
        
            lastRealityLevelIndex = realityPoiProperty.FindPropertyRelative("currentLevelIndex").intValue;

            EditorGUILayout.EndHorizontal();

            realityPoiDrawer.DrawUI(realityPoiProperty, realityPoiListProperty, needUpdateReality, realityPoiProperty.FindPropertyRelative("currentLevel").intValue);
        }

        //处理virtual poi
        showVirtualList = EditorGUILayout.Foldout(showVirtualList, virtualTitle);

        if (showVirtualList)
        {
            EditorGUILayout.BeginHorizontal();
            virtualPoiProperty.FindPropertyRelative("utmZone").intValue = EditorGUILayout.IntField(utmZone.text, virtualPoiProperty.FindPropertyRelative("utmZone").intValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(levelTitle);

            EditorGUI.BeginChangeCheck();

            virtualPoiProperty.FindPropertyRelative("currentLevelIndex").intValue = EditorGUILayout.IntPopup(virtualPoiProperty.FindPropertyRelative("currentLevelIndex").intValue, levelList.ToArray(), levelIndexList.ToArray());
   
            if (virtualPoiProperty.FindPropertyRelative("currentLevelIndex").intValue < levelProperty.arraySize)
            {
                virtualPoiProperty.FindPropertyRelative("currentLevel").intValue = levelProperty.GetArrayElementAtIndex(virtualPoiProperty.FindPropertyRelative("currentLevelIndex").intValue).intValue;
            }
            bool needUpdateVirtual = virtualPoiProperty.FindPropertyRelative("currentLevelIndex").intValue != lastVirtualLevelIndex;

            lastVirtualLevelIndex = virtualPoiProperty.FindPropertyRelative("currentLevelIndex").intValue;
            EditorGUILayout.EndHorizontal();
            if (cancelPoiOperation)
            {
                virtualPoiDrawer.DrawUI( virtualPoiProperty, virtualPoiListProperty, contentForPoiListProperty, true, virtualPoiProperty.FindPropertyRelative("currentLevel").intValue);
                cancelPoiOperation = false;
            }
            else
            {
                virtualPoiDrawer.DrawUI(virtualPoiProperty,virtualPoiListProperty, contentForPoiListProperty, needUpdateVirtual, virtualPoiProperty.FindPropertyRelative("currentLevel").intValue);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Cancel"), (GUIStyle)"minibuttonleft"))
            {
                poiMapScript.OnClickCancelVirtualPoiHandler();
                cancelPoiOperation = true;
            }

            if (GUILayout.Button(new GUIContent("Update"), (GUIStyle)"minibuttonright"))
            {
                poiMapScript.OnClickUploadVirtualPoiHandler();
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorHelper.CheckForModifiedProperty(virtualPoiProperty,true);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    private void OnDisable()
    {
     if(levelIndexList!=null) levelIndexList.Clear();
     if(levelList!=null) levelList.Clear();
    }

    private void OnDestroy()
    {
        
    }
}
