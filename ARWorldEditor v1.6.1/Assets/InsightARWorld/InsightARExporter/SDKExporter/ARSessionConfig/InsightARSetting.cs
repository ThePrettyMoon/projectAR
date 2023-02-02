#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ARWorldEditor
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[HelpURL("https://ardoc.ai.163.com")]
	public class InsightARSetting : MonoBehaviour
	{
		public delegate void DeleteRFCallBack(string path);
		public bool chooseType = false;
		public int mainTypeIndex = 0;
		public int subTypeIndex = 0;
		public ExportStruct configExport = new ExportStruct();
		public List<MarkerInfo> markerList = new List<MarkerInfo>();
		// 已经上传过服务器的marker信息，用于比对是否重复上传同一张图片
		public List<RemoteMarkerInfo> remoteMarkerList = new List<RemoteMarkerInfo>();
		public int objectARIndex = 0;
		public string[] objectARResourcesPath;
		public CheckBoxItem[] enumValue = new CheckBoxItem[0];
		public List<bool> isFolded = new List<bool> { true };
		public bool showImageConfig = true;
		public bool showToggleGroup = true;


		public bool useOverrideAlgorithmConfig = true;

#if UNITY_EDITOR
		public UnityEditor.DefaultAsset overrideAlgorithmConfig;
#endif

		public void SetData(int typeIndex, int subIndex)
		{
			Selection.activeGameObject = gameObject;
			mainTypeIndex = typeIndex;
			subTypeIndex = subIndex;
			chooseType = true;
		}

	}

	[CustomEditor(typeof(InsightARSetting))]
	public class SubSessionEditor : Editor
	{
		InsightARSetting config;
		MinorScene algData;
		public ExportStruct result;
		string[] majorValue;
		string[] minorValue;
		int tempMajorIndex = 0;
		Dictionary<string, FieldInfo> paramFieldDic;
		Dictionary<string, bool> paramNotSetted;
		string[] objectARName;

		protected void OnEnable()
		{
			config = (InsightARSetting)serializedObject.targetObject;

			/*if(AlgorithmGlobal.algorithmData == null)
		{
			AlgorithmGlobal.InitAlgorithmData();
		}

		if(algData == null || algData.title == "")
			algData = AlgorithmGlobal.algorithmData.majorSceneList[config.mainTypeIndex].minorSceneList[config.subTypeIndex];

		majorValue = new string[AlgorithmGlobal.algSceneType.Keys.Count];
		AlgorithmGlobal.algSceneType.Keys.CopyTo(majorValue, 0);
		minorValue = AlgorithmGlobal.algSceneType[majorValue[config.mainTypeIndex]].ToArray();
		tempMajorIndex = config.mainTypeIndex;

		result = config.configExport;
		result.ARSessionConfigs.scene = algData.sceneID;

		paramFieldDic = new Dictionary<string, FieldInfo>();
		paramNotSetted = new Dictionary<string, bool>();
		foreach(FieldInfo field in result.ARSessionConfigs.GetType().GetFields())
		{
			paramFieldDic.Add(field.Name, field);
			paramNotSetted.Add(field.Name, false);
			if(config.chooseType)
				paramNotSetted[field.Name] = true;
			// Debug.Log("paramField: " + field.Name);
		}
		config.chooseType = false;
		*/
		}


		public override void OnInspectorGUI()
		{
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update();

			//config.useOverrideAlgorithmConfig = EditorGUILayout.Toggle(new GUIContent("自定义算法配置文件"), config.useOverrideAlgorithmConfig);

			//if(config.useOverrideAlgorithmConfig)
			{
				config.overrideAlgorithmConfig = EditorGUILayout.ObjectField(new GUIContent("文件目录"), config.overrideAlgorithmConfig, typeof(DefaultAsset)) as DefaultAsset;

				return;
			}
			/*
					config.mainTypeIndex = EditorGUILayout.Popup("AR 场景类型", config.mainTypeIndex, majorValue);
					if(tempMajorIndex != config.mainTypeIndex)
					{
						config.subTypeIndex = 0;
						minorValue = AlgorithmGlobal.algSceneType[majorValue[config.mainTypeIndex]].ToArray();
						tempMajorIndex = config.mainTypeIndex;
					}
					config.subTypeIndex = EditorGUILayout.Popup("  ", config.subTypeIndex, minorValue);
					algData = AlgorithmGlobal.algorithmData.majorSceneList[config.mainTypeIndex].minorSceneList[config.subTypeIndex];

					if(result.ARSessionConfigs.scene != algData.sceneID)
					{
						config.markerList.Clear();
						config.remoteMarkerList.Clear();
						config.enumValue = new CheckBoxItem[0];
						config.objectARIndex = 0;
						config.objectARResourcesPath = new string[0];
						result.ARSessionConfigs = new SessionConfigs();
						result.ARSessionConfigs.scene = algData.sceneID;
						var buffer = new List<string>(paramNotSetted.Keys);
						foreach(var key in buffer)
						{
							paramNotSetted[key] = true;
						}
					}

					EditorGUILayout.Space();
					GUIStyle style = new GUIStyle();
					style.wordWrap = true;
					GUILayout.Label(algData.caption, style);
					EditorGUILayout.Space();

					for(int i = 0; i < algData.configItemList.Count; ++i)
					{
						var item = algData.configItemList[i];

						ConfigType type = (ConfigType)Enum.Parse(typeof(ConfigType), item.type);
						switch(type)
						{
							case ConfigType.Tip:
								EditorGUILayout.SelectableLabel(item.tipContent, style, GUILayout.ExpandHeight(true), GUILayout.MaxHeight(60));

								break;
							case ConfigType.Marker:
								ImageConfigEditorGUI(item);

								break;
							case ConfigType.Bool:
								bool valueBol = (bool)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs);
								if(paramNotSetted[item.paramName])
									valueBol = bool.Parse(item.defaultValue);
								paramFieldDic[item.paramName].SetValue(result.ARSessionConfigs, EditorGUILayout.Toggle(new GUIContent(item.displayName, item.toolTip), valueBol));
								if((bool)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs) != bool.Parse(item.defaultValue))
									paramNotSetted[item.paramName] = false;

								break;
							case ConfigType.Text:
								string valueTex = (string)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs);
								if(paramNotSetted[item.paramName])
									valueTex = item.defaultValue;
								paramFieldDic[item.paramName].SetValue(result.ARSessionConfigs, EditorGUILayout.TextField(new GUIContent(item.displayName, item.toolTip), valueTex));
								if((string)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs) != item.defaultValue)
									paramNotSetted[item.paramName] = false;

								break;
							case ConfigType.Float:
								float valueFlt = (float)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs);
								if(paramNotSetted[item.paramName])
									valueFlt = float.Parse(item.defaultValue);
								float newFlt = EditorGUILayout.FloatField(new GUIContent(item.displayName, item.toolTip), valueFlt);
								if(item.range != null && item.range.Length > 0)
								{
									if(IsValidFloat(newFlt, item.range))
										paramFieldDic[item.paramName].SetValue(result.ARSessionConfigs, newFlt);
								}
								if((float)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs) != float.Parse(item.defaultValue))
									paramNotSetted[item.paramName] = false;

								break;
							case ConfigType.DropDown:
								int valueDrop = 0;
								if(item.enumDisplay.Length < 1)
								{
									ObjectARConfigGUI(item);
								}
								else
								{
									if(item.paramName == "isUseFrontCamera")
									{
										valueDrop = (bool)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs) ? 1 : 2;
										if(paramNotSetted[item.paramName])
											valueDrop = int.Parse(item.defaultValue);
										int newValue = EditorGUILayout.IntPopup(item.displayName, valueDrop, item.enumDisplay, item.enumValue);
										bool setValue = newValue == 1 ? true : false;
										paramFieldDic[item.paramName].SetValue(result.ARSessionConfigs, setValue);
										if(newValue != int.Parse(item.defaultValue))
											paramNotSetted[item.paramName] = false;
									}
									else
									{
										valueDrop = (int)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs);
										if(paramNotSetted[item.paramName])
											valueDrop = int.Parse(item.defaultValue);
										paramFieldDic[item.paramName].SetValue(result.ARSessionConfigs, EditorGUILayout.IntPopup(item.displayName, valueDrop, item.enumDisplay, item.enumValue));
										if((int)paramFieldDic[item.paramName].GetValue(result.ARSessionConfigs) != int.Parse(item.defaultValue))
											paramNotSetted[item.paramName] = false;
									}
								}

								break;
							case ConfigType.CheckBox:
								if(config.enumValue.Length < 1)
									if(InitEnumValue(item))
										break;
								config.showToggleGroup = EditorGUILayout.Foldout(config.showToggleGroup, item.displayName);
								if(config.showToggleGroup)
								{
									for(int enumIndex = 0; enumIndex < item.enumDisplay.Length; ++enumIndex)
									{
										config.enumValue[enumIndex].enable = EditorGUILayout.Toggle(item.enumDisplay[enumIndex].Split(',')[0], config.enumValue[enumIndex].enable);
									}
								}

								break;

							default:
								break;
						}

					}

					serializedObject.ApplyModifiedProperties();

					if(GUI.changed)
					{
						EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
					}

				}

				void ObjectARConfigGUI(ConfigurationItem configItem)
				{
					string path = AlgorithmGlobal.objectARResourceDirectory + result.ARSessionConfigs.scene;
					if(!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
						AssetDatabase.Refresh();
					}

					string tempResourceName = "";
					if(config.objectARResourcesPath != null && config.objectARResourcesPath.Length > config.objectARIndex)	
						tempResourceName = Path.GetFileNameWithoutExtension(config.objectARResourcesPath[config.objectARIndex]);
					config.objectARResourcesPath = Directory.GetFiles(path, AlgorithmGlobal.objectARResourceExtension);

					objectARName = new string[config.objectARResourcesPath.Length];
					config.objectARIndex = 0;
					for(int i = 0; i < config.objectARResourcesPath.Length; ++i)
					{
						objectARName[i] = Path.GetFileNameWithoutExtension(config.objectARResourcesPath[i]);
						if(objectARName[i] == tempResourceName)
							config.objectARIndex = i;
					}
					config.objectARIndex = EditorGUILayout.Popup(configItem.displayName, config.objectARIndex, objectARName);

					string texturePath = Path.Combine(path, objectARName[config.objectARIndex] + ".png");
					if(File.Exists(texturePath))
					{
						Texture image = AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture)) as Texture;
						Texture2D assetPreview = AssetPreview.GetAssetPreview(image);
						GUILayout.Label(assetPreview);
					}

					EditorGUILayout.HelpBox(configItem.tipContent, MessageType.Info);

				}

				void ImageConfigEditorGUI(ConfigurationItem configItem)
				{
					// 设置同时追踪的最大图片数量
					paramFieldDic["markerTrackingMaxNum"].SetValue(result.ARSessionConfigs, configItem.itemMaxNum);

					if(config.markerList == null || config.markerList.Count == 0)
					{
						config.markerList = new List<MarkerInfo>();
						MarkerInfo info = new MarkerInfo();
						info.isValid = true;
						config.markerList.Add(info);
						if(configItem.itemMaxNum > 1)
						{
							for(int i = 0; i < configItem.itemMaxNum - 1; ++i)
							{
								MarkerInfo marker = new MarkerInfo();
								marker.isValid = true;
								config.markerList.Add(marker);
								config.isFolded.Add(true);
							}
						}
					}

					config.showImageConfig = EditorGUILayout.Foldout(config.showImageConfig, "识别图片");
					if(config.showImageConfig)
					{
						for(int i = 0; i < config.markerList.Count; ++i)
						{
							EditorGUILayout.BeginHorizontal();
							config.isFolded[i] = EditorGUILayout.Foldout(config.isFolded[i], "识别图片#" + (i + 1));
							// 308不显示删除按钮
							if(config.markerList.Count > 1)
							{
								if(configItem.itemMaxNum <= 1)
								{
									if(GUILayout.Button("删除", GUILayout.Width(50)))
									{
										config.markerList.RemoveAt(i);
										config.isFolded.RemoveAt(i);
										i--;
										continue;
									}
								}

							}
							EditorGUILayout.EndHorizontal();

							if(config.isFolded[i])
							{
								config.markerList[i].imageFile = EditorGUILayout.ObjectField("图片文件(JPG/PNG)", config.markerList[i].imageFile, typeof(Texture), true) as Texture;
								if(config.markerList[i].imageFile)
								{
									string ext  = Path.GetExtension(AssetDatabase.GetAssetPath(config.markerList[i].imageFile)).ToLower();
									config.markerList[i].isValid = (ext == ".jpg" || ext == ".jpeg" || ext == ".png");
									config.markerList[i].isRepeated = MarkerInfo.IsRepeatedTexture(config.markerList, i);

									if(config.markerList[i].isValid && (!config.markerList[i].isRepeated))
									{
										if(config.markerList[i].markerName == "")
											config.markerList[i].markerName = "marker" + DateTime.Now.ToString("HHmmss"); // TODO 极限情况不同天同秒的加一秒
									}
								}

								if(!config.markerList[i].isValid)
								{
									EditorGUILayout.HelpBox("请选择JPG或PNG格式的图片！", MessageType.Error);
								}
								if(config.markerList[i].isRepeated)
								{
									EditorGUILayout.HelpBox("该图片已经选过，请选择不同的图片！", MessageType.Warning);
								}

								// GUI.SetNextControlName("name" + i);
								string newName = EditorGUILayout.TextField(new GUIContent("自定义名称", "自定义名称仅包含字母和数字"), config.markerList[i].markerName);
								// if(GUI.GetNameOfFocusedControl() == ("name" + i))
								// {
								// 	Debug.Log("focus on " + "name" + i);
									if(newName != null && newName != "" && newName != config.markerList[i].markerName)
									{
										// 重名检测和有效性检测
										if( Regex.IsMatch(newName, @"^[A-Za-z0-9]*$") )
										{
											if(!MarkerInfo.DuplicationMarkerName(config.markerList, newName))
												config.markerList[i].markerName = newName;
											else
												EditorUtility.DisplayDialog("InsightARSetting", "自定义名称不能重复，请输入其他名称。", "确定");
										}
										else
										{
											EditorUtility.DisplayDialog("InsightARSetting", "自定义名称仅包含字母和数字，请输入其他名称。", "确定");
										}
									}
								// }


								config.markerList[i].markerWidth = EditorGUILayout.FloatField(new GUIContent("图片长边尺寸(m)", "线下物料图片长边的尺寸"), config.markerList[i].markerWidth);
								if(config.markerList[i].markerWidth <= 0)
									config.markerList[i].markerWidth = 1;
								if(configItem.imageDirectionEnabled)
								{
									config.markerList[i].markerDirection = EditorGUILayout.IntPopup(configItem.displayName, config.markerList[i].markerDirection, 
																									configItem.enumDisplay, configItem.enumValue);
								}
							}	

						}

						if(configItem.paramName != "")
						{
							int enumIdx = (int)paramFieldDic[configItem.paramName].GetValue(result.ARSessionConfigs);
							paramFieldDic[configItem.paramName].SetValue(result.ARSessionConfigs, 
																		 EditorGUILayout.IntPopup(configItem.displayName, enumIdx, 
																								  configItem.enumDisplay, configItem.enumValue));
						}


						if(configItem.itemMaxNum <= 1)
						{
							if(GUILayout.Button("新建识别图片"))
							{
								MarkerInfo info = new MarkerInfo();
								info.isValid = true;
								config.markerList.Add(info);
								config.isFolded.Add(true);
							}
						}

					}
					*/
		}

		bool InitEnumValue(ConfigurationItem configItem)
		{
			config.enumValue = new CheckBoxItem[configItem.enumDisplay.Length];
			string[] defaultValue = configItem.defaultValue.Split(',');
			if (defaultValue.Length != configItem.enumDisplay.Length)
			{
				Debug.LogError("CheckBox参数默认值的个数与参数名称个数不匹配!");
				return false;
			}
			for (int i = 0; i < configItem.enumDisplay.Length; ++i)
			{
				config.enumValue[i] = new CheckBoxItem(configItem.enumDisplay[i].Split(',')[1], bool.Parse(defaultValue[i]));
			}
			return true;
		}

		bool IsValidFloat(float value, string range)
		{
			string[] splits = range.Split(',');
			float min = splits[0].Contains("MIN") ? float.MinValue : float.Parse(splits[0].Substring(1, splits[0].Length - 1));
			float max = splits[1].Contains("MAX") ? float.MaxValue : float.Parse(splits[1].Substring(1, splits[1].Length - 1));
			if (splits[0].Contains("["))
			{
				if (value < min)
					return false;
			}
			else
			{
				if (value <= min)
					return false;
			}

			if (splits[1].Contains("]"))
			{
				if (value > max)
					return false;
			}
			else
			{
				if (value >= max)
					return false;
			}

			return true;
		}

	}

}
#endif
