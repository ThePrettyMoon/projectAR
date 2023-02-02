using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace UserConfig
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class AlgConfigWindow : BaseConfigWindow 
	{
		const string tempDictionary = "InsightARConfig/";

		const string markerFileName = "-desc.zip";
		const string jsonConfigFileName = "-config.json";


		// Data Model
		static AlgorithmData m = null;
		// 识别目标类型项
		string[] algDetectingTypeNames = new string[] {"二维图片", "无特定目标", "立体物体（仅洞见APP）", "人脸识别"};
		static int[] algDetectingTypeSizes = {1, 3, 2, 4};

		static List<Texture2D> algMarkerImageDisplayList = new List<Texture2D>();
		// 二维图片的跟踪类型项
		static string[] algTracking2DTypeNames = new string[] {"图片跟踪", "贴屏幕跟踪", "场景跟踪"};
		static int[] algTracking2DTypeSizes = {1, 2, 3};

		// 3D识别（立体物体）的跟踪类型项
		static string[] algTracking3DTypeNames = new string[] {"3D跟踪（仅洞见APP）", "SLAM跟踪（仅洞见APP）"};
		static int[] algTracking3DTypeSizes = {7, 8};

		// 场景识别（无特定目标）的跟踪类型项
		static string[] algTrackingFreeTypeNames = new string[] {"贴屏幕跟踪", "场景跟踪", "3DOF跟踪", "SLAM跟踪（仅洞见APP）", "人脸跟踪"};
		static int[] algTrackingFreeTypeSizes = {2, 3, 6, 8, 9};

		// 人脸识别的跟踪类型项
		static string[] algTrackingFaceTypeNames = new string[] {"人脸跟踪"};
		static int[] algTrackingFaceTypeSizes = {9};
		// 跟踪类型为场景跟踪时的性能设置
		static string[] performTrackingSceneTypeNames = new string[] {"稳定跟踪，高功耗", "初级跟踪，低功耗"};
		static int[] performTrackingSceneTypeSizes = {4, 6};

		// 图片丢失后的跟踪模式
		static string[] algLostTrackingTypeNames = new string[] { "不跟踪", "低性能的自动跟踪", "中性能的自动跟踪", "贴屏幕跟踪" };
		static int[] algLostTrackingTypeSizes = {0, 1, 2, 3};

		// 图片跟踪的跟踪方向
		static string[] markerDirectionTypeNames = new string[] { "平行", "垂直"};
		static int[] markerDirectionTypeSizes = {0, 1};

		// 算法配置主窗口
		static AlgConfigWindow algConfigWindow = null;
		// 描述文件管理窗口
		static MarkerImageWindow markerImageWindow = null;
		static bool isJsonSaving = false;
		static bool isMarkerChanged = false;

		public delegate void DeleteRFCallBack(string path);
		// static DeleteRFCallBack deleteRFCallBack = null;

		//返回json生成成功的状态给云打包
		public delegate void FinishStateForATCallBack();
		static FinishStateForATCallBack finishStateForATCallBack = null;

		// AT专用函数，AlgorithmData已经全部做好，只需要去生成desc，然后下载到本地，并保存AlgorithmData成json文件即可
		public static void GenerateJsonFromAT(AlgorithmData atData, FinishStateForATCallBack callback)
		{
			m = atData;
			ARMarkerManager.TextureUploadURL = m.textureUploadURL;
			ARMarkerManager.MarkerRequestURL = m.markerRequestURL;
			finishStateForATCallBack = callback;

			// 总开关是否开启
			if (!m.isARConfigEnabled)
				OnAllJsonDataDone(0, null);
			else
			{
				// 首先判断2D marker是否为空
				if (1 == m.algDetectingType && !IsMarkerValid())
					return;
				else
				{
					isJsonSaving = true;
					// 如果当前为图片模式，则由RequestMarkerForAT请求网络数据后唤起回调OnAllJsonDataDone
					if (1 == m.algDetectingType)
						ARMarkerManager.Instance.RequestMarkerForAT(m.markerImageArray, OnAllJsonDataDone);
					// 无需修改Marker，直接回调保存Json
					else
						OnAllJsonDataDone(0, null);
				}
			}
		}

		public static void ExportConfig(string path, DeleteRFCallBack callback = null)
		{
			string prefix = GetTempFilePrefix(tempDictionary);
			try
			{
				string configContent = File.ReadAllText(prefix + jsonConfigFileName, System.Text.Encoding.UTF8);
				m = JsonUtility.FromJson<AlgorithmData>(configContent);
			}
			catch (System.Exception)
			{
				m = new AlgorithmData();
			}
			finally
			{
				if (m.isARConfigEnabled)
				{
					// 清空目录方法回调
					if (null != callback)
						callback(path);

					string extraConfig = string.Empty;
					// 导出人脸、手势、人体的必要资源
					if (m.algDetectingType == 4)
					{
						RenderEngine.UtilityFileSystem.CopyDirectory(RenderEngine.ExporterConfig.EXPORTER_DIRECTORY + "/UserConfig/Resources/Face/", path, true);
					}
					else if (m.algDetectingType == 3 && (m.algTrackingFreeType == 3 || m.algTrackingFreeType == 6))
					{
						if (m.bDetectGesture)
						{
							extraConfig = @"///////////////////////////////////////////////////////////////////////////
imageWidth = 224
imageHeight = 224
ncnnInputIndex = 0
ncnnOutputIndex = 72
is_norm = 1
scoreThresh = 0.5
featureNames = conv_pred_11_output
modelVersion = yl
modelVersionInfo = y_t_p_gesture_p_f_0913
bDecrypt = 1
///////////////////////////////////////////////////////////////////
/// 检测框平滑相关参数
/////////////////////////////////////////////////////////////////
bDetctionSmooth = 0 //
smoothFrameNum = 6  // 检测框平滑的参考帧数
boxStillThresh = 0.03 //判断框为静止的阈值，以224大小的图像为例，中心点抖动7个像素内为静止 7 / 224 = 0.03
boxMovePreRefWeight = 0.7 //框对上一帧的依赖权重";
							RenderEngine.UtilityFileSystem.CopyDirectory(RenderEngine.ExporterConfig.EXPORTER_DIRECTORY + "/UserConfig/Resources/Gesture/", path, true);
						}
						else if (m.bDetectPose)
						{
							extraConfig = @"imageWidth = 240
imageHeight = 320
featureNames = Openpose__concat_stage5__0
inputFeatureName = image__0
bDecrypt = 1";
							RenderEngine.UtilityFileSystem.CopyDirectory(RenderEngine.ExporterConfig.EXPORTER_DIRECTORY + "/UserConfig/Resources/Pose/", path, true);
						}
					}

					// 导出config文本文件
					WriteFile(path + "/config.txt", m.ToString() + extraConfig);

					// 导出算法marker包
					if (m.algDetectingType == 1 && File.Exists(prefix + markerFileName))
						UnZipClass.Instance.UnZip(prefix + markerFileName, path);
					// clear meta files
					RenderEngine.UtilityFileSystem.FileDeleteByExtension( path, ".meta" , true );
				}
			}
		}
		new public static void ConfigDialog()
		{
			BaseConfigWindow.ConfigDialog();

			// 保存中，则显示保存中提示对话框
			if (isJsonSaving)
			{
				EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "正在保存，请稍候", "确定");
				return;
			}

			try
			{
				string configContent = File.ReadAllText(GetTempFilePrefix(tempDictionary) + jsonConfigFileName, System.Text.Encoding.UTF8);
				m = JsonUtility.FromJson<AlgorithmData>(configContent);
			}
			catch (System.Exception)
			{
				m = new AlgorithmData();
				m.markerImageArray = new AlgorithmData.MarkerImage[1];
				m.markerImageArray[0] = new AlgorithmData.MarkerImage();
			}
			finally
			{
				// restore images
				algMarkerImageDisplayList.Clear();
				foreach (var image in m.markerImageArray)
				{
					if (null != image)
					{
						Texture2D tmpTex = (Texture2D)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(image.localGUIDPath), typeof(Texture2D));
						algMarkerImageDisplayList.Add(tmpTex);
					}
					else
						algMarkerImageDisplayList.Add(null);
				}

				// Get existing open window or if none, make a new one:
				algConfigWindow = (AlgConfigWindow) EditorWindow.GetWindow(typeof(AlgConfigWindow), true, "AR算法配置");
				algConfigWindow.Show();
			}
		}

		void OnGUI()
		{
			//总开关
			m.isARConfigEnabled = EditorGUILayout.BeginToggleGroup("开启算法配置功能", m.isARConfigEnabled);

			#region 基础设置
			EditorGUILayout.Space();

			m.bBasicSettings = EditorGUILayout.Foldout(m.bBasicSettings, "基础设置", true);
			if (m.bBasicSettings)
			{
				// 场景名称
				// m.sceneName = EditorGUILayout.TextField ("场景名称：", m.sceneName);

				// 识别目标类型
				m.algDetectingType = EditorGUILayout.IntPopup("识别目标的类型：", m.algDetectingType, algDetectingTypeNames, algDetectingTypeSizes);

				// 跟踪类型与图片上传
				switch (m.algDetectingType)
				{
					// 图片识别
					case 1:
						// 图片识别下的跟踪选项
						m.algTracking2DType = EditorGUILayout.IntPopup("跟踪类型：", m.algTracking2DType, algTracking2DTypeNames, algTracking2DTypeSizes);
						switch (m.algTracking2DType)
						{
							// 图片跟踪
							case 1:
								m.lostTrackingtype = EditorGUILayout.IntPopup("图片丢失后的跟踪模式：", m.lostTrackingtype, algLostTrackingTypeNames, algLostTrackingTypeSizes);
								break;
							// 贴屏幕跟踪
							case 2:
								m.imuRate = EditorGUILayout.FloatField("陀螺仪旋转灵敏度：", m.imuRate);
								break;
							// 场景跟踪
							case 3:
								m.performTrackingSceneType = EditorGUILayout.IntPopup("性能设置：", m.performTrackingSceneType, performTrackingSceneTypeNames, performTrackingSceneTypeSizes);
								break;
							default:
								break;
						}
						m.markerWidth = EditorGUILayout.FloatField("图片宽度（米）：", m.markerWidth);
						m.initScale = EditorGUILayout.FloatField("尺度缩放因子：", m.initScale);
						m.markerDirection = EditorGUILayout.IntPopup("模型相对图片的朝向：", m.markerDirection, markerDirectionTypeNames, markerDirectionTypeSizes);
						if (GUILayout.Button("图片文件管理..."))
						{
							markerImageWindow = MarkerImageWindow.OnShow(algMarkerImageDisplayList, OnMarkerChanged);
						}
						break;
					// 3D识别
					case 2:
						// 3D识别下的跟踪选项
						m.algTracking3DType = EditorGUILayout.IntPopup("跟踪类型：", m.algTracking3DType, algTracking3DTypeNames, algTracking3DTypeSizes);
						break;
					// 场景识别
					case 3:
						// 场景识别下的跟踪选项
						m.algTrackingFreeType = EditorGUILayout.IntPopup("跟踪类型：", m.algTrackingFreeType, algTrackingFreeTypeNames, algTrackingFreeTypeSizes);
						switch (m.algTrackingFreeType)
						{
							// 贴屏幕跟踪
							case 2:
								m.imuRate = EditorGUILayout.FloatField("陀螺仪旋转灵敏度：", m.imuRate);
								break;
							// 场景跟踪：
							case 3:
							case 6:
								if (m.algTrackingFreeType == 3)
									m.performTrackingSceneType = EditorGUILayout.IntPopup("性能设置：", m.performTrackingSceneType, performTrackingSceneTypeNames, performTrackingSceneTypeSizes);

								m.detectGestureOrPose = EditorGUILayout.IntPopup("手势或人体检测", m.detectGestureOrPose, new string[] {"无", "手势检测", "人体检测"}, new int[]{0, 1, 2});
								m.bDetectGesture = m.detectGestureOrPose == 1;
								m.bDetectPose = m.detectGestureOrPose == 2;
								if (m.bDetectGesture) m.bTrackGesture = EditorGUILayout.Toggle("启用手势跟踪", m.bTrackGesture);
								// m.bUseARKIT = false;
								break;
							default:
								break;
						}
						m.groundDistance = EditorGUILayout.Slider(new GUIContent("初始化的默认距离（米）", "虚拟物体第一次放置后与手机的距离"), m.groundDistance, 0.1f, 5f);
						m.defaultDetectCount = EditorGUILayout.DelayedIntField(new GUIContent("跟踪前保留帧数：","Tracking之前预留的帧数，之后才会进行算法的初始化"), m.defaultDetectCount);
						m.bFirstHittestNeedReset = EditorGUILayout.Toggle(new GUIContent("第一次hittest时重置世界坐标", "当第一次使用hittest时，重置世界坐标系原点到该位置，建议勾选"), m.bFirstHittestNeedReset);
						break;
					case 4:
						// 人脸识别下的跟踪选项
						m.algTrackingFaceType = EditorGUILayout.IntPopup("跟踪类型：", m.algTrackingFaceType, algTrackingFaceTypeNames, algTrackingFaceTypeSizes);
						m.bCameraAngle = EditorGUILayout.DelayedIntField(new GUIContent("相机角度","一般横屏为0，竖屏为90"), m.bCameraAngle);
						break;
					default:
						break;
				}
			}
			#endregion

			#region 高级设置
			EditorGUILayout.Space();
			m.bAdvancedSettings = EditorGUILayout.Foldout(m.bAdvancedSettings, "高级设置", true);
			// m.bAdvancedSettings = EditorGUILayout.ToggleLeft ("高级设置", m.bAdvancedSettings);
			if (m.bAdvancedSettings)
			{
				m.bEnableLightEstimate = EditorGUILayout.Toggle(new GUIContent("使用光照估计","开启后，会自动检测环境光照，并反映到虚拟物体上"), m.bEnableLightEstimate);
				if (m.algDetectingType == 3 && (m.algTrackingFreeType == 3 || m.algTrackingFreeType == 6) &&  m.detectGestureOrPose == 0)
					m.bUseARKIT = EditorGUILayout.Toggle(new GUIContent("使用ARKit","该功能开启后，仅在iOS系统上生效"), m.bUseARKIT);
				m.bUseARCore = EditorGUILayout.Toggle(new GUIContent("使用ARCore","该功能开启后，仅在Android系统上生效"), m.bUseARCore);
				m.isUseHWAR = EditorGUILayout.Toggle(new GUIContent("使用华为AR","该功能开启后，仅在华为手机上生效"), m.isUseHWAR);

				m.bSaveTestData = EditorGUILayout.Toggle("保存运行时数据", m.bSaveTestData);
				if (m.bSaveTestData)
					m.bOnlySaveIMU = EditorGUILayout.Toggle("只保存IMU数据", m.bOnlySaveIMU);

				// m.bLoadTestData = EditorGUILayout.BeginToggleGroup("使用本地数据运行", m.bLoadTestData);
				// m.bLoadDataPath = EditorGUILayout.TextField ("运行路径：", m.bLoadDataPath);
				// EditorGUILayout.EndToggleGroup();

			}
			#endregion

			#region 摄像头设置
			EditorGUILayout.Space();
			m.bCameraVideoSettings = EditorGUILayout.Foldout(m.bCameraVideoSettings, "摄像头设置", true);
			// m.bCameraVideoSettings = EditorGUILayout.ToggleLeft ("相机视频流设置", m.bCameraVideoSettings);
			if (m.bCameraVideoSettings)
			{
				m.videoConfig.fps = EditorGUILayout.IntField("帧率：", m.videoConfig.fps);
				m.videoConfig.preset = EditorGUILayout.TextField("预览图像的分辨率级别：", m.videoConfig.preset);
				m.videoConfig.position = (AlgorithmData.VideoConfig.POSITION) EditorGUILayout.EnumPopup("前置/后置摄像头：", m.videoConfig.position);
				m.videoConfig.orientation = (AlgorithmData.VideoConfig.ORIENTATION) EditorGUILayout.EnumPopup("相机方向：", m.videoConfig.orientation);
				m.videoConfig.format = EditorGUILayout.TextField("格式：", m.videoConfig.format);
			}
			#endregion

			#region 服务器设置
			EditorGUILayout.Space();
			m.bServerSettings = EditorGUILayout.Foldout(m.bServerSettings, "常规设置", true);
			if (m.bServerSettings)
			{
				m.textureUploadURL = EditorGUILayout.TextField("图片上传服务器地址：", m.textureUploadURL);
				m.markerRequestURL = EditorGUILayout.TextField("算法服务器地址：", m.markerRequestURL);
			}
			ARMarkerManager.TextureUploadURL = m.textureUploadURL;
			ARMarkerManager.MarkerRequestURL = m.markerRequestURL;
			#endregion

			EditorGUILayout.EndToggleGroup();

			EditorGUI.BeginDisabledGroup (markerImageWindow != null);
			#region 功能按钮
			GUILayout.FlexibleSpace();
			GUILayout.Label("注意：删除场景的.meta文件会导致该配置清空！");
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("保存"))
			{
				// 总开关是否开启
				if (!m.isARConfigEnabled)
				{
					OnAllJsonDataDone(0, null);
					//关闭窗口
					if (markerImageWindow)
						markerImageWindow.Close();
					algConfigWindow.Close();
				}
				else
				{
					// 首先判断2D marker是否为空
					if (1 == m.algDetectingType && !IsMarkerValid())
						EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "请使用<图片文件管理...>选择需要跟踪的图片文件", "确定");
					else
					{
						isJsonSaving = true;
						// 如果当前为图片模式且Marker有修改，则由SaveMarkerData请求网络数据后唤起回调OnAllJsonDataDone
						if (1 == m.algDetectingType && isMarkerChanged)
							SaveMarkerData();
						// 无需修改Marker，直接回调保存Json
						else
							OnAllJsonDataDone(0, null);

						//关闭窗口
						if (markerImageWindow)
							markerImageWindow.Close();
						algConfigWindow.Close();
					}
				}
			}
			if (GUILayout.Button("重置"))
				if (EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE, "确定重置吗？", "确定","取消"))
					m = new AlgorithmData();
			if (GUILayout.Button("取消"))
			{
				if (markerImageWindow)
					markerImageWindow.Close();
				algConfigWindow.Close();
			}
			EditorGUILayout.EndHorizontal();
			#endregion
			EditorGUI.EndDisabledGroup ();
		}

		static bool IsMarkerValid()
		{
			for (int i = 0; i < algMarkerImageDisplayList.Count; i ++)
				if (null == algMarkerImageDisplayList[i])
					return false;
			return true;
		}

		static void SaveMarkerData()
		{
			// 图片数量可能会有变化，所以要resize数组大小
			if (algMarkerImageDisplayList.Count != m.markerImageArray.Length)
			{
				System.Array.Resize(ref m.markerImageArray, algMarkerImageDisplayList.Count);
				// 分配内存
				for (int i = 0; i < m.markerImageArray.Length; i ++)
					if (null == m.markerImageArray[i])
						m.markerImageArray[i] = new AlgorithmData.MarkerImage();
			}


			// 用于上传图片的临时数组
			AlgorithmData.MarkerImage[] tmpMarkerImageArray = new AlgorithmData.MarkerImage[algMarkerImageDisplayList.Count];

			for (int i = 0; i < algMarkerImageDisplayList.Count; i ++)
			{
				tmpMarkerImageArray[i] = new AlgorithmData.MarkerImage();
				Texture2D tex = algMarkerImageDisplayList[i];
				string path = AssetDatabase.GetAssetPath(tex);
				// 计算该图的md5信息
				string md5 = Md5Sum(File.ReadAllBytes(path));
				// 如果该图片已存在，则直接拷贝其值
				int seek;
				for (seek = 0; seek < algMarkerImageDisplayList.Count; seek ++)
					if (m.markerImageArray[seek].md5 == md5)
					{
						tmpMarkerImageArray[i].Copy(m.markerImageArray[seek]);
						tmpMarkerImageArray[i].localPath = path;
						tmpMarkerImageArray[i].localGUIDPath = AssetDatabase.AssetPathToGUID(path);
						tmpMarkerImageArray[i].localName = Path.GetFileName(path);
						break;
					}
				// 如果图片不存在，则remotePath为空，表示要上传图片
				if (seek >= algMarkerImageDisplayList.Count)
				{
					tmpMarkerImageArray[i].md5 = md5;
					tmpMarkerImageArray[i].localPath = path;
					tmpMarkerImageArray[i].localGUIDPath = AssetDatabase.AssetPathToGUID(path);
					tmpMarkerImageArray[i].localName = Path.GetFileName(path);
					tmpMarkerImageArray[i].remotePath = "";
				}
			}

			ARMarkerManager.Instance.RequestMarker(tmpMarkerImageArray, OnAllJsonDataDone);
		}

		static void OnAllJsonDataDone(int status, string content, AlgorithmData.MarkerImage[] markerImageArray = null, byte[] wwwBytes = null)
		{
			if (status == 0)
			{
				// 保存图片的上传信息
				if (null != markerImageArray)
					for(int i = 0; i < markerImageArray.Length; i ++)
						m.markerImageArray[i].Copy(markerImageArray[i]);

				// 保存描述文件包
				if (null != wwwBytes)
					WriteFile(GetTempFilePrefix(tempDictionary) + markerFileName, wwwBytes);

				WriteFile(GetTempFilePrefix(tempDictionary) + jsonConfigFileName, JsonUtility.ToJson(m, true));
				Debug.Log("算法配置保存成功！");
			}
			else
			{
				Debug.Log("图片描述文件生成失败，请检查网络并重试：" + content);
			}

			isJsonSaving = false;

			if (finishStateForATCallBack != null)
				finishStateForATCallBack();
		}

		static void OnMarkerChanged(bool flag)
		{
			isMarkerChanged = flag;
		}

		private static string Md5Sum(byte[] byteToEncrypt)
		{
			// System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
			// byte[] bytes = ue.GetBytes(strToEncrypt);

			// encrypt bytes
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashBytes = md5.ComputeHash(byteToEncrypt);

			// Convert the encrypted bytes back to a string (base 16)
			string hashString = "";
			for (int i = 0; i < hashBytes.Length; i++)
				hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');

			return hashString.PadLeft(32, '0');
		}

		void OnDestroy()
		{
			if (markerImageWindow)
				markerImageWindow.Close();
		}
	}
}
