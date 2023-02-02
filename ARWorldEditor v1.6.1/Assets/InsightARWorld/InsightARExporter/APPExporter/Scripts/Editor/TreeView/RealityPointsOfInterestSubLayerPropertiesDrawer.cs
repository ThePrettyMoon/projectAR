
	using UnityEngine;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEditor.IMGUI.Controls;
	using System.Linq;

public class RealityPointsOfInterestSubLayerPropertiesDrawer
{
	string objectId = "";
	static float _lineHeight = EditorGUIUtility.singleLineHeight;

	FeatureSubLayerTreeView layerTreeView;
	IList<int> selectedLayers = new List<int>();

	private TreeModel<FeatureTreeElement> treeModel;
	[SerializeField]
	TreeViewState m_TreeViewState;

	[SerializeField]
	MultiColumnHeaderState m_MultiColumnHeaderState;

	bool m_Initialized = false;
	public bool isLayerAdded = false;

	private GUIContent propertyTitle = new GUIContent
	{
		text = "Properties",
		tooltip = "Poi Properties"
	};

	private GUIContent prefabContent = new GUIContent
	{
		text = "Prefab",
		tooltip = "POI Prefab"
	};

	//功能添加中...
	private GUIContent scaleWithWorld = new GUIContent
	{
		text = "Scale down with world",
		tooltip = "Scale down with world"
	};

	int SelectionIndex
	{
		get
		{
			return EditorPrefs.GetInt(objectId + "LocationPrefabsLayerProperties_selectionIndex");
		}
		set
		{
			EditorPrefs.SetInt(objectId + "LocationPrefabsLayerProperties_selectionIndex", value);
		}
	}

	public void DrawUI(SerializedProperty mapProperty, SerializedProperty property, bool needUpdate, int level)
	{
		objectId = property.serializedObject.targetObject.GetInstanceID().ToString();
		var prefabItemArray = property;
		int arraySize = 0;
		for (int i = 0; i < property.arraySize; i++)
		{
			if (property.GetArrayElementAtIndex(i).FindPropertyRelative("properties.level").stringValue != level.ToString()) continue;
			arraySize++;
		}

		var layersRect = EditorGUILayout.GetControlRect(GUILayout.MinHeight(Mathf.Max(arraySize + 1, 1) * _lineHeight + MultiColumnHeader.DefaultGUI.defaultHeight),
														GUILayout.MaxHeight((arraySize + 1) * _lineHeight + MultiColumnHeader.DefaultGUI.defaultHeight));
		//重新初始化
		if (needUpdate)
		{
			m_TreeViewState = null;
			m_Initialized = false;
			m_MultiColumnHeaderState = null;
			layerTreeView = null;
		}

		if (!m_Initialized)
		{
			bool firstInit = m_MultiColumnHeaderState == null;
			var headerState = FeatureSubLayerTreeView.CreateDefaultMultiColumnHeaderState();
			if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
			{
				MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
			}
			m_MultiColumnHeaderState = headerState;

			var multiColumnHeader = new FeatureSectionMultiColumnHeader(headerState);

			if (firstInit)
			{
				multiColumnHeader.ResizeToFit();
			}

			treeModel = new TreeModel<FeatureTreeElement>(GetData(prefabItemArray, level));
			if (m_TreeViewState == null)
			{
				m_TreeViewState = new TreeViewState();
			}

			if (layerTreeView == null)
			{
				layerTreeView = new FeatureSubLayerTreeView(m_TreeViewState, multiColumnHeader, treeModel, FeatureSubLayerTreeView.uniqueIdPoI);
			}
			layerTreeView.multiColumnHeader = multiColumnHeader;
			m_Initialized = true;
		}

		layerTreeView.Layers = prefabItemArray;
		layerTreeView.Reload();
		layerTreeView.OnGUI(layersRect);

		if (layerTreeView.hasChanged)
		{
			EditorHelper.CheckForModifiedProperty(property);
			layerTreeView.hasChanged = false;
		}

		selectedLayers = layerTreeView.GetSelection();
		//if there are selected elements, set the selection index at the first element.
		//if not, use the Selection index to persist the selection at the right index.
		if (selectedLayers.Count > 0)
		{
			//ensure that selectedLayers[0] isn't out of bounds
			if (selectedLayers[0] - FeatureSubLayerTreeView.uniqueIdPoI > prefabItemArray.arraySize - 1)
			{
				selectedLayers[0] = prefabItemArray.arraySize - 1 + FeatureSubLayerTreeView.uniqueIdPoI;
			}

			SelectionIndex = selectedLayers[0];

		}
		/*else
		{
			selectedLayers = new int[1] { SelectionIndex };
			if (SelectionIndex > 0 && (SelectionIndex - FeatureSubLayerTreeView.uniqueIdPoI <= arraySize - 1))
			{
				layerTreeView.SetSelection(selectedLayers);
			}
		}*/


		GUILayout.Space(EditorGUIUtility.singleLineHeight);

		if (selectedLayers.Count == 1 && prefabItemArray.arraySize != 0 && selectedLayers[0] - FeatureSubLayerTreeView.uniqueIdPoI >= 0)
		{
			//ensure that selectedLayers[0] isn't out of bounds
			if (selectedLayers[0] - FeatureSubLayerTreeView.uniqueIdPoI > prefabItemArray.arraySize - 1)
			{
				selectedLayers[0] = prefabItemArray.arraySize - 1 + FeatureSubLayerTreeView.uniqueIdPoI;
			}
			SelectionIndex = selectedLayers[0];

			var layerProperty = prefabItemArray.GetArrayElementAtIndex(SelectionIndex - FeatureSubLayerTreeView.uniqueIdPoI);

			layerProperty.isExpanded = true;
			bool isLayerActive = layerProperty.FindPropertyRelative("isActive").boolValue;
			if (!isLayerActive)
			{
				GUI.enabled = false;
			}
			DrawSingleItemPrefabProperties(mapProperty, layerProperty);
			if (!isLayerActive)
			{
				GUI.enabled = true;
			}
		}
		else
		{
			GUILayout.Space(15);
			GUILayout.Label("Select a visualizer to see properties");
		}
	}

	void DrawLayerLocationPrefabProperties(SerializedProperty layerProperty, SerializedProperty property)
	{
		EditorGUILayout.PropertyField(layerProperty);
	}

	/// <summary>
	/// 绘制选中item属性
	/// </summary>
	/// <param name="layerProperty"></param>
	/// <param name="transform"></param>
	void DrawSingleItemPrefabProperties(SerializedProperty mapProperty, SerializedProperty layerProperty)
	{
		EditorGUILayout.BeginVertical();
		propertyTitle.text = layerProperty.FindPropertyRelative("properties.name").stringValue + " Properties";
		EditorGUILayout.LabelField(propertyTitle);
		EditorGUI.indentLevel++;

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(layerProperty.FindPropertyRelative("prefab"), prefabContent);
		if (EditorGUI.EndChangeCheck())
		{
			EditorHelper.CheckForModifiedProperty(mapProperty);
			PlayerPrefs.SetString(mapProperty.FindPropertyRelative("cotentId").longValue+ layerProperty.FindPropertyRelative("properties.id").stringValue, AssetDatabase.GetAssetPath(layerProperty.FindPropertyRelative("prefab").objectReferenceValue));
		}

		EditorGUI.BeginChangeCheck();
		layerProperty.FindPropertyRelative("scaleDown").boolValue = EditorGUILayout.Toggle(scaleWithWorld, layerProperty.FindPropertyRelative("scaleDown").boolValue);

		if (EditorGUI.EndChangeCheck())
		{
			EditorHelper.CheckForModifiedProperty(mapProperty);
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();
	}

	IList<FeatureTreeElement> GetData(SerializedProperty subLayerArray,int currentLevel)
	{
		List<FeatureTreeElement> elements = new List<FeatureTreeElement>();
		string name = string.Empty;
		string type = string.Empty;
		string level = string.Empty;

		int id = 0;
		var root = new FeatureTreeElement("Root", -1, 0);
		elements.Add(root);

		for (int i = 0; i < subLayerArray.arraySize; i++)
		{
			var subLayer = subLayerArray.GetArrayElementAtIndex(i);
			level = subLayer.FindPropertyRelative("properties.level").stringValue;
			name = subLayer.FindPropertyRelative("properties.name").stringValue;
			id = i + FeatureSubLayerTreeView.uniqueIdPoI;
			if (level != currentLevel.ToString()) continue;
			type = subLayer.FindPropertyRelative("properties.type").stringValue;

			FeatureTreeElement element = new FeatureTreeElement(name, 0, id);
			element.Name = name;
			element.Type = type;
			elements.Add(element);
		}
		return elements;
	}
}

