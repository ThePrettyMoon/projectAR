
	using UnityEngine;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEditor.IMGUI.Controls;
	using System.Linq;
    using ARWorldEditor;

    public class VirtualPointsOfInterestSubLayerPropertiesDrawer
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

	private GUIContent transformTitle = new GUIContent()
	{
		text = "Transform",
		tooltip = "POI Position，Rotation And Scale"
	};

	private GUIContent radiusTitile = new GUIContent()
	{
		text = "Radius",
		tooltip = "POI Trigger Distance"
	};

	private GUIContent algModeTitle = new GUIContent
	{
		text = "Algorithm mode",
		tooltip = "POI algorithm mode"
	};

	private GUIContent contentForPoiTitle = new GUIContent
	{
		text = "Linked event",
		tooltip = "POI linked event"
	};

	private GUIContent infoRadiusTitle = new GUIContent
	{
		text = "Information",
		tooltip = "trigger POI information distance"
	};

	private GUIContent previewRadiusTitle = new GUIContent
	{
		text = "Preview",
		tooltip = "trigger POI preview distance"
	};

	private GUIContent experienceRadiusTitle = new GUIContent
	{
		text = "Experience",
		tooltip = "trigger POI experience distance"
	};

	private GUIContent prefabContent = new GUIContent
	{
		text = "Prefab",
		tooltip = "POI prefab"
	};

	//功能添加中...
	private GUIContent scaleWithWorld = new GUIContent
	{
		text = "Scale down with world",
		tooltip = "scale down with world"
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

	public void DrawUI(SerializedProperty mapProperty, SerializedProperty property, SerializedProperty poiProperty, bool needUpdate, int level)
	{
		objectId = property.serializedObject.targetObject.GetInstanceID().ToString();
		var prefabItemArray = property;

		int arraySize = 0;
		for (int i = 0; i < property.arraySize; i++)
		{
			if (property.GetArrayElementAtIndex(i).FindPropertyRelative("properties.level").stringValue != level.ToString()) continue;
			arraySize++;
		}

		var layersRect = EditorGUILayout.GetControlRect(GUILayout.MinHeight(Mathf.Max(prefabItemArray.arraySize + 1, 1) * _lineHeight + MultiColumnHeader.DefaultGUI.defaultHeight),
														GUILayout.MaxHeight((prefabItemArray.arraySize + 1) * _lineHeight + MultiColumnHeader.DefaultGUI.defaultHeight));

		//重新初始化,重新添加数据
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

			treeModel = new TreeModel<FeatureTreeElement>(GetData(prefabItemArray,level));
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
		layerTreeView.canRename = true;
		layerTreeView.Reload();
		layerTreeView.OnGUI(layersRect);
		if (layerTreeView.hasChanged)
		{
			EditorHelper.CheckForModifiedProperty(mapProperty);
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

		EditorGUILayout.BeginHorizontal();
		
		if (GUILayout.Button(new GUIContent("Add Virtual POI"), (GUIStyle)"minibuttonleft"))
		{
			prefabItemArray.arraySize++;
			arraySize++;

			//找到最大的楼层数
			int maxLevelCount = 0;
			for(int i = 0; i < prefabItemArray.arraySize; i++)
            {
				int levelNum = StringUtility.ParseInt(prefabItemArray.GetArrayElementAtIndex(i).FindPropertyRelative("properties.name").stringValue.Replace("VPOI", ""));
				if (levelNum > maxLevelCount) maxLevelCount = levelNum;
            }

			var poiItem = prefabItemArray.GetArrayElementAtIndex(prefabItemArray.arraySize - 1);
			poiItem.FindPropertyRelative("properties.id").stringValue = ARWorldEditor.RandomUtility.GUID(); //唯一id
			poiItem.FindPropertyRelative("properties.name").stringValue = "VPOI" + (maxLevelCount+1) ; 
			poiItem.FindPropertyRelative("properties.type").stringValue = "POI";
			poiItem.FindPropertyRelative("properties.x_content_id").stringValue = "";
			poiItem.FindPropertyRelative("properties.x_content_alg_mode").stringValue = AlgorithmType.unchange.ToString();
			poiItem.FindPropertyRelative("properties.level").stringValue = level.ToString();
			poiItem.FindPropertyRelative("properties.x_name_radius").stringValue = "5";
			poiItem.FindPropertyRelative("properties.x_name_radius").stringValue = "5";
			poiItem.FindPropertyRelative("properties.x_preview_content_radius").stringValue = "3";
			poiItem.FindPropertyRelative("properties.x_content_radius").stringValue = "1";
			poiItem.FindPropertyRelative("geometry.position").vector3Value = Vector3.zero;
			poiItem.FindPropertyRelative("geometry.rotation").vector3Value = Vector3.zero;
			poiItem.FindPropertyRelative("geometry.scale").vector3Value = Vector3.one;
			poiItem.FindPropertyRelative("prefab").objectReferenceValue = mapProperty.FindPropertyRelative("defaultTrans").objectReferenceValue;
			var geometry = poiItem.FindPropertyRelative("geometry");
			var coordinates = poiItem.FindPropertyRelative("geometry.coordinates");
			var type = poiItem.FindPropertyRelative("geometry.type");

			// Set defaults here because SerializedProperty copies the previous element.
			poiItem.FindPropertyRelative("isActive").boolValue = true;
			poiItem.FindPropertyRelative("algIndex").intValue = -1;

			//Refreshing the tree
			layerTreeView.Layers = prefabItemArray;
			layerTreeView.AddElementToTree(poiItem);
			layerTreeView.Reload();

			//不能选择已添加的层
			//selectedLayers = new int[1] { prefabItemArray.arraySize - 1 };
			//layerTreeView.SetSelection(selectedLayers);

			if (EditorHelper.DidModifyProperty(property))
			{
				isLayerAdded = true;
			}
		}

		if (GUILayout.Button(new GUIContent("Remove Virtual POI"), (GUIStyle)"minibuttonright"))
		{
			if (selectedLayers == null || selectedLayers.Count == 0)
			{
				Debug.Log("please first  select a poi to remove");
				return;
			}

			if (arraySize == 0)
			{
				Debug.Log("poi list is empty");
				return;
			}

			foreach (var index in selectedLayers.OrderByDescending(i => i))
			{
				if (layerTreeView != null)
				{
					//var poiSubLayer = prefabItemArray.GetArrayElementAtIndex(index - FeatureSubLayerTreeView.uniqueIdPoI);

					layerTreeView.RemoveItemFromTree(index);
					prefabItemArray.DeleteArrayElementAtIndex(index - FeatureSubLayerTreeView.uniqueIdPoI);
					layerTreeView.treeModel.SetData(GetData(prefabItemArray, level));

					arraySize--;
				}
			}
			selectedLayers = new int[0];
			layerTreeView.SetSelection(selectedLayers);
		}

		EditorGUILayout.EndHorizontal();

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
			DrawSingleItemPrefabProperties( layerProperty,poiProperty);

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
	/// 重新写prefab item
	/// </summary>
	/// <param name="layerProperty"></param>
	void DrawSingleItemPrefabProperties(SerializedProperty layerProperty, SerializedProperty poiProperty)
	{
		EditorGUILayout.BeginVertical();
		propertyTitle.text = layerProperty.FindPropertyRelative("properties.name").stringValue + " Properties";
		EditorGUILayout.LabelField(propertyTitle);
		EditorGUI.indentLevel++;

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(layerProperty.FindPropertyRelative("prefab"), prefabContent);
		if (EditorGUI.EndChangeCheck())
		{
			string assetPath = AssetDatabase.GetAssetPath(layerProperty.FindPropertyRelative("prefab").objectReferenceValue);
			layerProperty.FindPropertyRelative("replacePrefab").boolValue = true;
		}

		layerProperty.FindPropertyRelative("scaleDown").boolValue = EditorGUILayout.Toggle(scaleWithWorld, layerProperty.FindPropertyRelative("scaleDown").boolValue);


		EditorGUI.indentLevel--;
		List<int> idx = new List<int>();
		List<GUIContent> ids = new List<GUIContent>();
		//增加无选项
		idx.Add(-1);
		ids.Add(new GUIContent("---"));
		for (int i = 0; i < poiProperty.arraySize; i++)
		{
			ids.Add(new GUIContent(poiProperty.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue));
			idx.Add(i);
		}
		SerializedProperty indexItem = layerProperty.FindPropertyRelative("algIndex");
		SerializedProperty contentIdProperty = layerProperty.FindPropertyRelative("properties.x_content_id");
		SerializedProperty algorithmModeProperty = layerProperty.FindPropertyRelative("properties.x_content_alg_mode");
		if (string.IsNullOrEmpty(contentIdProperty.stringValue))
		{
			indexItem.intValue = -1;
		}
		else
		{
			int contentId = StringUtility.ParseInt(contentIdProperty.stringValue);
			for (int i = 0; i < poiProperty.arraySize; i++)
			{
				if (poiProperty.GetArrayElementAtIndex(i).FindPropertyRelative("id").intValue == contentId)
				{
					indexItem.intValue = i;
					break;
				}
			}
		}

		indexItem.intValue = EditorGUILayout.IntPopup(contentForPoiTitle, indexItem.intValue, ids.ToArray(), idx.ToArray());
		if (ids.Count > indexItem.intValue)
		{
			if (indexItem.intValue == -1) { contentIdProperty.stringValue = ""; }
			else
			{
				contentIdProperty.stringValue = poiProperty.GetArrayElementAtIndex(indexItem.intValue).FindPropertyRelative("id").intValue.ToString();
				algorithmModeProperty.stringValue = poiProperty.GetArrayElementAtIndex(indexItem.intValue).FindPropertyRelative("algorithmMode").stringValue;
			}
		}

		ids.Clear();
		idx.Clear();

		//SerializedProperty algMode = layerProperty.FindPropertyRelative("properties.x_content_alg_mode");
		//algMode.stringValue = EditorGUILayout.EnumPopup(algModeTitle, StringUtility.ParseEnum<AlgorithmType>(algMode.stringValue)).ToString();

		EditorGUILayout.LabelField(transformTitle);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(layerProperty.FindPropertyRelative("geometry.position"), new GUIContent("Position"));
		EditorGUILayout.PropertyField(layerProperty.FindPropertyRelative("geometry.rotation"), new GUIContent("Rotation"));
		EditorGUILayout.PropertyField(layerProperty.FindPropertyRelative("geometry.scale"), new GUIContent("Scale"));
		EditorGUI.indentLevel--;

		EditorGUILayout.LabelField(radiusTitile);
		EditorGUI.indentLevel++;

		EditorGUI.BeginChangeCheck();
		SerializedProperty nameRadius = layerProperty.FindPropertyRelative("properties.x_name_radius");
		EditorGUILayout.PropertyField(nameRadius, infoRadiusTitle);
        if (EditorGUI.EndChangeCheck())
        {
			float nameRadiusFloat = StringUtility.ParseFloat(nameRadius.stringValue);
			nameRadius.stringValue = ((int)nameRadiusFloat).ToString();
        }

		EditorGUI.BeginChangeCheck();
		SerializedProperty previewRadius = layerProperty.FindPropertyRelative("properties.x_preview_content_radius");
		EditorGUILayout.PropertyField(previewRadius, previewRadiusTitle);
		if (EditorGUI.EndChangeCheck())
		{
			float previewRadiusFloat = StringUtility.ParseFloat(previewRadius.stringValue);
			previewRadius.stringValue = ((int)previewRadiusFloat).ToString();
		}

		EditorGUI.BeginChangeCheck();
		SerializedProperty contentRadius = layerProperty.FindPropertyRelative("properties.x_content_radius");
		EditorGUILayout.PropertyField(contentRadius, experienceRadiusTitle);
		if (EditorGUI.EndChangeCheck())
		{
			float contentRadiusFloat = StringUtility.ParseFloat(contentRadius.stringValue);
			contentRadius.stringValue = ((int)contentRadiusFloat).ToString();
		}

		EditorGUI.indentLevel--;

		EditorGUILayout.EndVertical();
	}

	IList<FeatureTreeElement> GetData(SerializedProperty subLayerArray, int currentLevel)
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

