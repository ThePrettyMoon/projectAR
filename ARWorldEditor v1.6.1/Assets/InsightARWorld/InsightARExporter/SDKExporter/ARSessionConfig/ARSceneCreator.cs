#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using Dummiesman;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ARWorldEditor
{
	public class ARSceneCreator : EditorWindow
	{
		#region params
		const int tabCount = 20;

		static MyContentsResponseData myProductsData = null;
		static List<MyContentsResultData> myLandmarkProducts;
		static List<MyContentsResultData> myTeanProducts;
		static List<MyContentsResultData> myCoordinationContents;
		static List<MyContentsResultData> selectProductsList;
		static ARSceneCreator _instance;

		int algCount = 0;
		int selectedContentType = 0;
		int curSelectedContentType = 0;
		Texture line;
		Dictionary<long, List<Texture2D>> gifTextureDic;
		Texture2D defaultGifTxture;
		GUISkin skin;
		GUIStyle urlStyle;
		GUIStyle popUpStyle;
		Vector2 scrollPos = Vector2.zero;
		bool isRunning = false;
		string warningString;
		Texture defaultsceneicon;
		Texture defaulteventicon;

		#region ver 1.3 编辑器需求 
		static string[] TAB = new string[] { "我的内容", "协助内容", "团队内容" };
		int belogingContentTabSelect = 0;
		int resBelogingContentTabSelect = 0;
		GUIStyle belogingContentTabStyle;
		#endregion

		#region ver 1.4.2
		GUIStyle descriptStyle;
		GUIStyle versionStyle;
		GUIStyle teamOrDeveloperStyle;
		GUIStyle scenePopUpStyle;
		GUIStyle searchStyle;
		string curSearchContent = "";
		string lastSearchContent = "";
		#endregion

		#endregion

		#region unity functions

		protected void Awake()
		{
			skin = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.FileDirectory + "UTSkin.guiskin", typeof(GUISkin)) as GUISkin;
			belogingContentTabStyle = new GUIStyle("Tab middle");
			skin.label.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
			skin.button.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
			skin.customStyles[1].normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;

			//InitStyle();
        }
		void InitStyle()
		{
            descriptStyle = GUI.skin.customStyles[468];
            descriptStyle.fontSize = 15;
            descriptStyle.fontStyle = FontStyle.Normal;

            versionStyle = skin.customStyles[4];
            teamOrDeveloperStyle = skin.customStyles[5];
            searchStyle = new GUIStyle(EditorStyles.toolbarSearchField);
            searchStyle.fontSize = 20;
            searchStyle.fixedHeight = 40;

            scenePopUpStyle = new GUIStyle(EditorStyles.popup);
            scenePopUpStyle.alignment = TextAnchor.MiddleCenter;
            scenePopUpStyle.fixedWidth = 200;
            scenePopUpStyle.fontSize = 14;
            scenePopUpStyle.fixedHeight = 40;

            popUpStyle = new GUIStyle(EditorStyles.popup);
            popUpStyle.alignment = TextAnchor.MiddleLeft;
            popUpStyle.fixedWidth = 150;
            popUpStyle.fontSize = 12;
            popUpStyle.fixedHeight = 22;
            popUpStyle.contentOffset = Vector2.zero;

            urlStyle = GUI.skin.customStyles[45];
        }

		/// <summary>
		/// 加载默认资源
		/// </summary>
		void LoadDefaultResources()
		{
			gifTextureDic = new Dictionary<long, List<Texture2D>>();

			defaultGifTxture = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.imageDirectory + AlgorithmGlobal.DEFAULT_LOGO_PATH, typeof(Texture2D)) as Texture2D;
			line = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.imageDirectory + "pic_ut_line.png", typeof(Texture)) as Texture;

			defaultsceneicon = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.imageDirectory + "landmarkericon.png", typeof(Texture)) as Texture;
			defaulteventicon = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.imageDirectory + "eventicon.png", typeof(Texture)) as Texture;

		}

		/// <summary>
		/// 加载资源
		/// </summary>
		void ReloadAssets()
		{
			try
			{
				List<MyContentsResultData> data = null;
				if (belogingContentTabSelect == 0)
				{
					data = myLandmarkProducts;
				}
				else if (belogingContentTabSelect == 1)
				{
					data = myCoordinationContents;
				} else if (belogingContentTabSelect == 2)
				{
					data = myTeanProducts;
				}

				if (data == null)
				{
					algCount = 0;
					return;
				}



				selectProductsList = data.FindAll(p => p.contentType == selectedContentType);

				//再根据搜索功能筛选
				if (!string.IsNullOrEmpty(curSearchContent))
				{
					selectProductsList = selectProductsList.FindAll(p => p.name.Contains(curSearchContent));
				}

				algCount = selectProductsList == null ? 0 : selectProductsList.Count;

				if (algCount == 0) return;
				float count = 0;
				float totalCount = algCount;
				EditorUtility.DisplayProgressBar("加载图片", "加载中", 0);
				for (int i = 0; i < algCount; ++i)
				{
					MyContentsResultData minorScene = selectProductsList[i];
					string path = AlgorithmGlobal.imageDirectory + minorScene.GetContentIcon() + ".png";
					int index = i;
					if (minorScene.logo != null)
					{
						List<Texture2D> gifTex = new List<Texture2D>();
						string downloadPath = AlgorithmGlobal.imageDirectory + minorScene.logo;
						string nosUrl = minorScene.GetLogoNosUrl();

						//如果有图片，不需要重复请求
						if (gifTextureDic != null && gifTextureDic.ContainsKey(minorScene.id))
						{
                            count++;
                            //Debug.LogError("count：" + count);
                            EditorUtility.DisplayCancelableProgressBar("加载图片", "加载中", count / totalCount);
                            if (count == algCount)
                            {
                                //Debug.Log("algCount:" + algCount);
                                EditorUtility.ClearProgressBar();
                            }
                            continue;
                        }	
						
						TextureDataFetch.Instance.GetTexture(nosUrl, (Texture2D text) =>
						 {
							 count++;
							 //Debug.LogError("count："+count);
                             EditorUtility.DisplayCancelableProgressBar("加载图片", "加载中", count/ totalCount);
                             if (count == algCount)
                             {
								 //Debug.Log("algCount:"+ algCount);
                                 EditorUtility.ClearProgressBar();
                             }
                             gifTex.Add(text);
							 if (!gifTextureDic.ContainsKey(minorScene.id)) gifTextureDic.Add(minorScene.id, gifTex);
						 }, (string code, string msg) =>
						 {
                             count++;
                             //Debug.LogError("count：" + count);
                             EditorUtility.DisplayCancelableProgressBar("加载图片", "加载中", (float)count / totalCount);
							 if (count == algCount)
							 {
                                 //EditorUtility.ClearProgressBar();
                             }
                             Debug.Log("get texture error " + code + " " + msg);
						 });
					}
					//进入将选中的selected ix默认值
					minorScene.SelectedVersionIdx = 0;
                    minorScene.SelectedSimGlassVersionIdx= 0;
                }
			}
			catch (System.Exception e)
			{
				Debug.LogError("我的内容资源加载失败！");
				Debug.LogError(e.ToString());
				this.Close();
			}
		}

		public static void ShowWindow()
		{
			if (_instance == null)
			{
				_instance = (ARSceneCreator)EditorWindow.GetWindow(typeof(ARSceneCreator), true, "我的内容");
				_instance.minSize = new Vector2(1000, 750);
			}
            _instance.Show();
			_instance.GetMyProducts();
		}

		protected void OnInspectorUpdate()
		{
			if (!isRunning) return;
			Repaint();
		}


		protected void OnGUI()
		{
			if (!isRunning) return;

			//GUI.DrawTexture(new Rect(0, 640, 120, 120), logo);
			if (_instance.descriptStyle == null)
			{
                _instance.InitStyle();
			}


			if (myProductsData == null || _instance.algCount == 0)
			{
				DrawWarningView(warningString);
			}

			Rect windowRect = _instance.position;
			float tabHeight = 50;
			float tabScale = (float)(windowRect.height) / tabCount;
			var upDis = 50;
			Rect rect = new Rect(10, tabHeight + upDis, windowRect.width, windowRect.height - tabHeight - upDis);

			//顶部选项
			belogingContentTabSelect = GUILayout.Toolbar(belogingContentTabSelect, TAB, belogingContentTabStyle, GUILayout.Width(windowRect.width), GUILayout.Height(tabHeight));
			//左侧选项
			//DrawTabView();
			//顶部选择
			DrawSceneOrEventToggle(rect);
			//搜索框
			DrawSearchContentItem(rect);


			if (_instance.algCount > 0)
			{
				DrawProductView(rect, tabScale);
			}
		}

		protected void OnDestroy()
		{
			isRunning = false;
			if (gifTextureDic != null) gifTextureDic.Clear();
			if (selectProductsList != null) selectProductsList.Clear();
		}
		#endregion

		#region custom functions

		/// <summary>
		/// 返回产品列表
		/// </summary>
		void GetMyProducts()
		{
			MyProducts.GetMyProducts(GetMyProductsSuccess, GetMyProductsFail);
		}

		/// <summary>
		/// 获取产品列表成功
		/// </summary>
		/// <param name="responseData"></param>
		void GetMyProductsSuccess(MyContentsResponseData responseData)
		{
			//        foreach (var item in responseData.result)
			//        {
			//item.navigateModel = 2;
			//Debug.LogError(item.navigateModel);
			//        }
			myProductsData = responseData;
			myLandmarkProducts = responseData.result.myContents;
			myCoordinationContents = responseData.result.coordinationContents;
			myTeanProducts = responseData.result.teamContents;

			_instance.warningString = _instance.algCount == 0 ? "当前无内容" : "";
			_instance.LoadDefaultResources();
			_instance.ReloadAssets();
			isRunning = true;
		}
		/// <summary>
		/// 获取产品失败
		/// </summary>
		void GetMyProductsFail(string code, string msg)
		{
			//会话失效，关闭界面，打开登陆界面
			if (code == ServerResponseCode.RESPONSE_SESSION_INVALIDATION)
			{
				isRunning = false;
				myProductsData = null;
				_instance.Close();
				UserController.ReEnterLoginView();
			}
			else
			{
				isRunning = true;
				myProductsData = null;
				warningString = "网络异常，请稍后重试!";
			}
		}

		/// <summary>
		/// 没有产品视图
		/// </summary>
		void DrawWarningView(string content)
		{
			//GUI.skin = skin;
			GUI.Label(new Rect(_instance.position.width / 2.0f - 150, _instance.position.height / 2.0f - 60, 200, 60), content, skin.customStyles[2]);
			GUI.skin = null;
		}

		/// <summary>
		/// 绘制选择界面
		/// </summary>
		void DrawTabView()
		{
			///场景按钮
			if (GUI.Button(new Rect(20, 84, 80, 80), defaultsceneicon))
			{
				if (selectedContentType != 1)
				{
					selectedContentType = 1;
					_instance.ReloadAssets();
				}
			}

			///事件按钮
			if (GUI.Button(new Rect(20, 184, 80, 80), defaulteventicon))
			{
				if (selectedContentType != 2)
				{
					selectedContentType = 2;
					_instance.ReloadAssets();
				}
			}

			if (resBelogingContentTabSelect != belogingContentTabSelect)
			{
				resBelogingContentTabSelect = belogingContentTabSelect;
				_instance.ReloadAssets();
			}
		}

		void DrawSceneOrEventToggle(Rect rect)
		{
			var x = rect.width - 250;
			var y = rect.y - 45;
			var wid = 200;
			var height = 40;
			curSelectedContentType = EditorGUI.Popup(new Rect(x, y, wid, height), curSelectedContentType, new string[] { "场景", "事件" }, scenePopUpStyle);

			if (curSelectedContentType == 0)
			{
				//场景
				if (selectedContentType != 1)
				{
					selectedContentType = 1;
					_instance.ReloadAssets();
				}
			}
			else if (curSelectedContentType == 1)
			{
				//事件
				if (selectedContentType != 2)
				{
					selectedContentType = 2;
					_instance.ReloadAssets();
				}
			}
			if (resBelogingContentTabSelect != belogingContentTabSelect)
			{
				resBelogingContentTabSelect = belogingContentTabSelect;
				_instance.ReloadAssets();
			}
		}

		/// <summary>
		/// 绘制产品列表
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="tabScale"></param>
		void DrawProductView(Rect rect, float tabScale)
		{
			var height = 160;
			var contentHeight = selectProductsList.Count * height;
			//滑动条
			if (contentHeight > rect.height)
			{
				float scrollbarWidth = skin.verticalScrollbar.fixedWidth;
				Rect scrollbarRect = new Rect(rect.width - scrollbarWidth, rect.y, scrollbarWidth, rect.height);
				scrollPos.y = GUI.VerticalScrollbar(scrollbarRect, scrollPos.y, 200f, 0, contentHeight - rect.height + 200f);
			}
			scrollPos = GUI.BeginScrollView(rect, scrollPos, new Rect(rect.x, rect.y, rect.width - 20, contentHeight), false,false, GUIStyle.none, GUIStyle.none);

			for (int i = 0; i < selectProductsList.Count; ++i)
			{
				Rect sceneRect = new Rect(rect.x, rect.y + height * i, rect.width, height);
				DrawScene(sceneRect, i, tabScale, selectProductsList[i]);
				// 画分隔线
				GUI.DrawTexture(new Rect(sceneRect.x, sceneRect.y + sceneRect.height, sceneRect.width, 1), line);
			}
			GUI.EndScrollView();
		}

		/// <summary>
		/// 绘制事件view
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="index"></param>
		/// <param name="tabScale"></param>
		/// <param name="sceneInfo"></param>
		void DrawScene(Rect rect, int index, float tabScale, MyContentsResultData sceneInfo)
		{
			Vector2 space = new Vector2(10, 10);

			//GUI.DrawTexture(new Rect(rect.x + space.x , rect.y + 24, tabScale, tabScale), buttonTextures[index]);

			Rect rectWithoutSpace = new Rect(rect.x + space.x + tabScale, rect.y + space.y, rect.width - 2 * space.x - tabScale, rect.height - 2 * space.y);
			Rect gifRect = new Rect(rectWithoutSpace.x, rect.y + 15, 240, 134.4f);

			if (gifTextureDic.ContainsKey(sceneInfo.id)) GUI.DrawTexture(gifRect, gifTextureDic[sceneInfo.id][0]);
			else GUI.DrawTexture(gifRect, defaultGifTxture);

			Rect lableRect = new Rect(rectWithoutSpace.x + gifRect.width + 32, gifRect.y, rectWithoutSpace.width - gifRect.width, rectWithoutSpace.height);

			bool containMobileDevice = false;
			bool containSimGlassDevice = false;
            bool hasSimGlass = false;
            bool hasMobile = false;
			const int mobileDevicesType = (int)ContentsDevicesType.Mobile;
			const int simGlassesDevicesType = (int)ContentsDevicesType.SimGlass;
            string[] versions = sceneInfo.GetVersionStrings();
            string[] mobileVersions = sceneInfo.GetTargetVersionStrings(mobileDevicesType);
            string[] simGlassVersions = sceneInfo.GetTargetVersionStrings(simGlassesDevicesType);
            int[] types = sceneInfo.GetAllDevicesType();
			

            foreach (int type in types)
            {
                if (type == mobileDevicesType)//mobile端
                {
                    containMobileDevice = true;
                }
                else if (type == simGlassesDevicesType)//sim Glass 
                {
                    containSimGlassDevice = true;
                }
            }
            if (mobileVersions.Length > 0)
            {
                hasMobile = true;
            }
            if (simGlassVersions.Length > 0)
            {
                hasSimGlass = true;
            }

            GUILayout.BeginArea(lableRect);
			{
                //GUI.skin = skin;
                GUILayout.Label(sceneInfo.name, skin.customStyles[1]);
				//标题
                GUILayout.BeginHorizontal();
                {
                    if (sceneInfo.contentType == 1)
                    {
                        if (sceneInfo.GetNavMode() != null) //新版通过navigateModel判断
                        {
                            string navigationTip = sceneInfo.GetNavModeName();
                            GUILayout.Label(sceneInfo.mapName, descriptStyle);
                            GUILayout.Label(navigationTip, descriptStyle);
                        }
                        else //兼容旧版 navigate属性
                        {
                            string navigationTip = sceneInfo.navigate == 1 ? "导航" : "";
                            GUILayout.Label(sceneInfo.mapName, descriptStyle);
                            GUILayout.Label(navigationTip, descriptStyle);
                        }
                    }
                    else if (sceneInfo.contentType == 2)
                    {
                        GUILayout.Label(sceneInfo.GetAlgorithmTypeDesc(), descriptStyle);
                    }
				
                    GUILayout.Label(sceneInfo.GetOrientationType(), descriptStyle);
                    GUILayout.Label(sceneInfo.GetEngineName(), descriptStyle);

                    //根据设备端判断移动or眼镜端
                    foreach (var item in sceneInfo.GetAllDevicesType().OrderBy(u => u).ToArray())
                    {
                        if (item == (int)ContentsDevicesType.Mobile)
                        {
                            GUILayout.Label("移动设备", descriptStyle);
                        }
                        else if (item == (int)ContentsDevicesType.SimGlass)
                        {
                            GUILayout.Label("SIM", descriptStyle);
                        }
                    }
                }
                GUILayout.EndHorizontal();

				//mobile\glasses 内容选择
                GUILayout.BeginArea(new Rect(0, 57, 600, 80));
				{

					float selectButtonDistance = 180;
					float selectButtonWidth = 120;
                   
                    //mobile
                    if (containMobileDevice)
					{
                        GUILayout.Space(5);
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("移动端内容版本", versionStyle);
							//版本列表
                            if (hasMobile)
                            {
                                sceneInfo.SelectedVersionIdx = EditorGUILayout.Popup("", sceneInfo.SelectedVersionIdx, mobileVersions, popUpStyle, GUILayout.Width(150));
                            }
                            else
                            {
                                EditorGUI.BeginDisabledGroup(true);
                                {
                                    sceneInfo.SelectedVersionIdx = EditorGUILayout.Popup("", sceneInfo.SelectedVersionIdx, new string[] { "无可编辑版本，请新建" }, popUpStyle, GUILayout.Width(150));
                                }
                                EditorGUI.EndDisabledGroup();
                            }
							//新增版本
                            EditorGUI.BeginDisabledGroup(sceneInfo.appId == null);
                            {
                                if (GUILayout.Button("+", GUILayout.Width(22), GUILayout.Height(22)))
                                {
                                    long appID = (long)sceneInfo.appId;
                                    //获取sdk版本
                                    NetDataFetchManager.Instance.GetAppSdkVersions(appID,1, new OnOasisNetworkDataFetchCallback<GetAppSdkVersionsResponseData>(
                                   (GetAppSdkVersionsResponseData response) =>
                                   {
                                       OnGetAppSdkVersionsSuccess(response, sceneInfo.id, sceneInfo.engineType, mobileDevicesType);
                                   }, (string code, string msg) =>
                                   {
                                       OnGetAppSdkVersionsFail(code, msg);
                                   }));
                                }
                            }
                            EditorGUI.EndDisabledGroup();
							GUILayout.Space(selectButtonDistance);
							//选择该版本
                            if (!hasMobile) GUI.enabled = false;
                            if (GUILayout.Button("选择", GUILayout.Width(selectButtonWidth)))
                            {
                                Debug.LogFormat("选择业务场景 ：{0} - {1}", sceneInfo.id, sceneInfo.name);
                                //如果本地有缓存的版本，则加载上一次发布的版本的场景
                                _instance.Close();

                                //MyProducts.MyProduct = sceneInfo;
                                sceneInfo.SelectDevicesType = (int)ContentsDevicesType.Mobile;
                                PlayerPrefs.SetString("MyProduct", JsonUtil.Serialize(sceneInfo));
                                if (LocalSceneCacheManager.Instance.LoadFromCache(sceneInfo.id.ToString(),mobileDevicesType.ToString(), mobileVersions[sceneInfo.SelectedVersionIdx]))
                                {
                                    return;
                                }

                                //下载地图资源
                                if (sceneInfo.contentType == 1)
                                {
                                    MapResources.DownloadMaps((long)sceneInfo.mapId, (GetMapResourcesResponseData mapData) => { OnDownloadMapSuccess(sceneInfo, mapData,mobileDevicesType); }, OnDownloadMapProgress, OnDownloadMapError);
                                }
                                else if (sceneInfo.contentType == 2)
                                {
                                    CreateOrUpdateEventScene(sceneInfo, mobileDevicesType);
                                }
                            }
							GUI.enabled = true;

                        }
                        GUILayout.EndHorizontal();
                    }
                   
                    //glasses
                    if (containSimGlassDevice)
					{
                        GUILayout.Space(5);
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("眼镜端内容版本", versionStyle);
                            //版本列表
                            if (hasSimGlass)
                            {
                                sceneInfo.SelectedSimGlassVersionIdx = EditorGUILayout.Popup("", sceneInfo.SelectedSimGlassVersionIdx, simGlassVersions, popUpStyle, GUILayout.Width(150));
                            }
                            else
                            {
                                EditorGUI.BeginDisabledGroup(true);
                                {
                                    sceneInfo.SelectedSimGlassVersionIdx = EditorGUILayout.Popup("", sceneInfo.SelectedSimGlassVersionIdx, new string[] { "无可编辑版本，请新建" }, popUpStyle, GUILayout.Width(150));
                                }
                                EditorGUI.EndDisabledGroup();
                            }
                            //新增版本
                            EditorGUI.BeginDisabledGroup(sceneInfo.appId == null);
                            {
                                if (GUILayout.Button("+",GUILayout.Width(22),GUILayout.Height(22)))
                                {
                                    long appID = (long)sceneInfo.appId;
                                    //获取sdk版本
                                    NetDataFetchManager.Instance.GetAppSdkVersions(appID, simGlassesDevicesType, new OnOasisNetworkDataFetchCallback<GetAppSdkVersionsResponseData>(
                                   (GetAppSdkVersionsResponseData response) =>
                                   {
                                       OnGetAppSdkVersionsSuccess(response, sceneInfo.id, sceneInfo.engineType, simGlassesDevicesType);
                                   }, (string code, string msg) =>
                                   {
                                       OnGetAppSdkVersionsFail(code, msg);
                                   }));
                                }
                            }
                            EditorGUI.EndDisabledGroup();
                            GUILayout.Space(selectButtonDistance);
                            //选择该版本
                            if (!hasSimGlass) GUI.enabled = false;
                            if (GUILayout.Button("选择",GUILayout.Width(selectButtonWidth)))
                            {
                                Debug.LogFormat("选择业务场景 ：{0} - {1}", sceneInfo.id, sceneInfo.name);
                                //如果本地有缓存的版本，则加载上一次发布的版本的场景
                                _instance.Close();

								//MyProducts.MyProduct = sceneInfo;
								sceneInfo.SelectDevicesType = (int)ContentsDevicesType.SimGlass;
                                PlayerPrefs.SetString("MyProduct", JsonUtil.Serialize(sceneInfo));
                                if (LocalSceneCacheManager.Instance.LoadFromCache(sceneInfo.id.ToString(),simGlassesDevicesType.ToString(), simGlassVersions[sceneInfo.SelectedSimGlassVersionIdx]))
                                {
                                    return;
                                }

                                //下载地图资源
                                if (sceneInfo.contentType == 1)
                                {
                                    MapResources.DownloadMaps((long)sceneInfo.mapId, (GetMapResourcesResponseData mapData) => { OnDownloadMapSuccess(sceneInfo, mapData,simGlassesDevicesType); }, OnDownloadMapProgress, OnDownloadMapError);
                                }
                                else if (sceneInfo.contentType == 2)
                                {
                                    CreateOrUpdateEventScene(sceneInfo,simGlassesDevicesType);
                                }
                            }
                            GUI.enabled = true;
                        }
                        GUILayout.EndHorizontal();
                    }

					
                  
                  
                }
                GUILayout.EndArea();
            }
            GUILayout.EndArea();
            GUI.enabled = true;
			GUI.skin = null;
		}

		/// <summary>
		/// 地图下载完成
		/// </summary>
		/// <param name="sceneId"></param>
		private void OnDownloadMapSuccess(MyContentsResultData sceneInfo, GetMapResourcesResponseData mapInfo, int devicesType)
		{
			EditorUtility.ClearProgressBar();
			CreateOrUpdateARScene(sceneInfo, mapInfo, devicesType);
		}

		/// <summary>
		/// 地图下载过程中
		/// </summary>
		private void OnDownloadMapProgress(string fileName, float progress)
		{
			EditorUtility.DisplayProgressBar("下载地图资源", "下载" + fileName + "...", progress);
		}

		/// <summary>
		/// 地图下载失败
		/// </summary>
		private void OnDownloadMapError(string code, string msg)
		{
			EditorUtility.ClearProgressBar();
			isRunning = false;
			//会话失效，打开登陆界面
			if (code == ServerResponseCode.RESPONSE_SESSION_INVALIDATION)
			{
				UserController.ReEnterLoginView();
			}
		}

		/// <summary>
		/// 创建子事件,如果模板改变需要更新版本处理
		/// </summary>
		/// <param name="sceneInfo"></param>
		void CreateOrUpdateEventScene(MyContentsResultData sceneInfo,int devicesType)
		{
			long sceneId = sceneInfo.id;
			string sceneName = string.Format("scene_{0}_{1}.unity", sceneId, sceneInfo.GetCurrentVersionInfo());
			
			string sceneDirectory = "";

            if (devicesType == (int)ContentsDevicesType.Mobile)
            {
                sceneDirectory = Path.Combine(ConfigGlobal.SCENE_MOBILE_PATH, string.Format(string.Format("{0}_{1}", sceneId, sceneInfo.GetCurrentVersionInfo())));
            }
            else if (devicesType == (int)ContentsDevicesType.SimGlass)
            {
                EditorUtility.DisplayDialog("Error", "子事件暂未支持SIMGlass" + devicesType, "确认");
                return;
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "未知 devicesType:" + devicesType, "确认");
                return;
            }
            string scenePath = Path.Combine(sceneDirectory, sceneName);
			Scene currentScene;

			if (File.Exists(scenePath))
			{
				currentScene = EditorSceneManager.OpenScene(scenePath);
			}
			else
			{
				if (!Directory.Exists(sceneDirectory)) Directory.CreateDirectory(sceneDirectory);
				currentScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
				string sceneTemplateName = "";
				if (sceneInfo.algorithmType == 1)
				{
					if (sceneInfo.algorithmName == "单图识别")
					{
						sceneTemplateName = "image_template_untracked.unity";
					}
					else if (sceneInfo.algorithmName == "单图识别跟踪")
					{
						sceneTemplateName = "image_template.unity";
					} else
					{
						sceneTemplateName = "image_template.unity";
					}
					//Debug.Log("sceneInfo.algorithmName _ "+ sceneInfo.algorithmName);
				}
				else if (sceneInfo.algorithmType == 2)
				{
					if (sceneInfo.algorithmName == "物体识别跟踪")
						sceneTemplateName = "object_template.unity";
					else if (sceneInfo.algorithmName == "物体识别")
						sceneTemplateName = "object_template_untracked.unity";
				}
				else if (sceneInfo.algorithmType == 3)
				{
					sceneTemplateName = "hand_template.unity";
				}
				else if (sceneInfo.algorithmType == 4)
				{
					sceneTemplateName = "body_template.unity";
				}
				else if (sceneInfo.algorithmType == 5)
				{
					if (sceneInfo.algorithmName == "天空分割")
					{
						sceneTemplateName = "sky_template.unity";
					}
					else
					{
						sceneTemplateName = "landmark_template.unity";
					}
				}
				Scene additivedScene = EditorSceneManager.OpenScene(Path.Combine(ConfigGlobal.TEMPLATE_SCENE_PATH, sceneTemplateName), OpenSceneMode.Additive);

				EditorSceneManager.MergeScenes(additivedScene, currentScene);
			}

			AssetDatabase.SaveAssets();
			EditorSceneManager.SaveScene(currentScene, scenePath);
			AssetDatabase.Refresh();
		}

		/// <summary>
		/// 创建主场景
		/// </summary>
		/// <param name="sceneInfo"></param>
		void CreateOrUpdateARScene(MyContentsResultData sceneInfo, GetMapResourcesResponseData mapInfo,int devicesType)
		{
			long sceneId = sceneInfo.id;
			string sceneName = string.Format("scene_{0}_{1}.unity", sceneId, sceneInfo.GetCurrentVersionInfo());
            string sceneDirectory = "";
            if (devicesType == (int)ContentsDevicesType.Mobile)
            {
                sceneDirectory = Path.Combine(ConfigGlobal.SCENE_MOBILE_PATH, string.Format(string.Format("{0}_{1}", sceneId, sceneInfo.GetCurrentVersionInfo())));
            }
            else if (devicesType == (int)ContentsDevicesType.SimGlass)
            {
                sceneDirectory = Path.Combine(ConfigGlobal.SCENE_GLASS_PATH, string.Format(string.Format("{0}_{1}", sceneId, sceneInfo.GetCurrentVersionInfo())));
				if (sceneInfo.navigateModel != 1)
				{
                    EditorUtility.DisplayDialog("Error", "sim眼镜仅支持主场景模式，请确认当前场景是否为 主场景模式。" + devicesType, "确认");
                    return;
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "未知 devicesType:" + devicesType, "确认");
                return;
            }
            string scenePath = Path.Combine(sceneDirectory, sceneName);
			Scene currentScene;

			if (File.Exists(scenePath))
			{
				currentScene = EditorSceneManager.OpenScene(scenePath);
			}
			else
			{
				if (!Directory.Exists(sceneDirectory)) Directory.CreateDirectory(sceneDirectory);
				currentScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
				Scene additivedScene = currentScene;
                if (devicesType == (int)ContentsDevicesType.Mobile)
                {
                    string sceneTemplateName = sceneInfo.GetTemplateSceneName();
                    additivedScene = EditorSceneManager.OpenScene(Path.Combine(ConfigGlobal.TEMPLATE_SCENE_PATH, sceneTemplateName), OpenSceneMode.Additive);
                }
                else if (devicesType == (int)ContentsDevicesType.SimGlass)
                {
					additivedScene = EditorSceneManager.OpenScene(ConfigGlobal.TEMPLATE_SIMGLASSSCENE_PATH, OpenSceneMode.Additive);
                }
				else
				{
                    EditorUtility.DisplayDialog("Error", "未知 devicesType:"+devicesType, "确认");
					return;
                }
				EditorSceneManager.MergeScenes(additivedScene, currentScene);
			}

			AssetDatabase.Refresh();

            string mapRootPath = Path.Combine(ConfigGlobal.MAP_PATH, sceneInfo.mapId.ToString(), ((int)MapResourcesType.RESOURCE_TYPE_LOW_POLY_MESH).ToString());

            //string objPath = Path.Combine(mapRootPath, ConfigGlobal.LOW_POLY_MODEL_NAME + ".obj");


			//找mtl文件
            string mtlPath = "";
            bool objMtlExit = TryGetMTLFilePath(mapRootPath, out mtlPath);
            if (!objMtlExit)
            {
                Debug.LogError("未找到路径下MTL文件\n" + mapRootPath);
            }
            //找obj
            List<string> objPathList;
			bool objExit = TryGetOBJFilesPath(mapRootPath, out objPathList);
            if (!objExit)
			{
                Debug.LogError("未找到路径下Obj文件\n" + mapRootPath);
            }
			//找地图mesh贴图
			List<string> imgPathList;
			bool imgExit = TryGetImageFilePath(mapRootPath, out imgPathList);
			if (!imgExit)
			{
                Debug.LogError("未找到路径下贴图（png或jpg）文件\n" + mapRootPath);
            }

            //World
            GameObject worldObj = ARWorldEditor.GameObjectExtension.Find("World");
            if (worldObj == null)
            {
                worldObj = new GameObject();
                worldObj.name = "World";
                worldObj.transform.localPosition = Vector3.zero;
            }

            //Land
            GameObject realitysceneObj = ARWorldEditor.GameObjectExtension.Find(ConfigGlobal.SCENE_WORLD_MODEL_NAME + "/" + ConfigGlobal.LOW_POLY_MODEL_NAME);
            if (realitysceneObj != null) GameObject.DestroyImmediate(realitysceneObj);
            realitysceneObj = new GameObject("Land");
            realitysceneObj.isStatic = true;
            realitysceneObj.transform.parent = worldObj.transform;
            realitysceneObj.transform.localPosition = Vector3.zero;
            realitysceneObj.transform.localRotation = Quaternion.identity;

            string originMaterialPath;
            Transform subObjTrans;
            Material copyRealityMaterial;

            if (objMtlExit)
			{
				Dictionary<string, Transform> subObjTransList = new Dictionary<string, Transform>();

				foreach (var objPath in objPathList)
				{
					//通过插件加载OBJ模型文件
					var objLoder = new OBJLoader();
					var landObj = objLoder.Load(objPath);//
					landObj.tag = ConfigGlobal.TAG_REALITYSCENE;

					//如果没有找到defualt命名的节点，则遍历land，获取有meshFilter组件的节点
					subObjTrans = landObj.transform.Find("default");
					if (subObjTrans == null)
					{
						//Debug.Log("count:"+landObj.transform.childCount);
						for (int i = 0; i < landObj.transform.childCount; i++)
						{
							var child = landObj.transform.GetChild(i);
							//Debug.Log("child:" + child.name);
							var mesh = child.GetComponent<MeshFilter>();
							if (mesh != null)
							{
								subObjTrans = child;
								break;
							}
						}
					}

					//realityShader获取
					originMaterialPath = Path.Combine(ConfigGlobal.REALITY_SCENE_MATERIAL_PATH, ConfigGlobal.REALITY_SCENE_MATERIAL_NAME);
					Material realityMaterial = AssetDatabase.LoadAssetAtPath(originMaterialPath, typeof(Material)) as Material;
					Shader realityShader = realityMaterial.shader;

					//获取材质，并对材质重新链接图片引用，否则材质在保存后出现图片丢失
					///noText:
					///(1)这里存在mtl文件中图片名称与实际外部图片名称不对应的遗留问题，这里做了兼容适配
					///(2)读取的mtl文件可能存在异常，当读取失败时，使用兼容方式读取
					bool noText = false;
                    if (objLoder.Materials==null)
                    {
						noText = true;
                    }
                    else
                    {
						foreach (var item in objLoder.Materials)
						{
							item.Value.shader = realityShader;
							if (item.Value.mainTexture == null)
							{
								//说明mtl文件中材质未对应上png或jpg，需要使用直接读取png的方式
								noText = true;
								break;
							}
							string path = Path.Combine(mapRootPath, item.Value.mainTexture.name + ".png");
							Texture2D tx = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
							//jpg验证
							if (tx == null)
							{
								path = Path.Combine(mapRootPath, item.Value.mainTexture.name + ".jpg");
								tx = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
							}
							item.Value.mainTexture = tx;
							string materialPath = Path.Combine(sceneDirectory, item.Key + ".mat");
							item.Value.SetFloat("_Alpha", 1);
							AssetDatabase.CreateAsset(item.Value, materialPath);
						}
					}

					//兼容性，当通过mtl找不到图片时，直接使用外部的图片方式
					if (noText)
                    {
						var meshRender = subObjTrans.GetComponent<MeshRenderer>();
						var mapMaterial = meshRender.sharedMaterial;
						Material newRealityMaterial = new Material(realityMaterial);
						string imgPath = Path.Combine(mapRootPath, imgPathList[0]);
						Texture2D tx = AssetDatabase.LoadAssetAtPath<Texture2D>(imgPath);
						newRealityMaterial.mainTexture = tx;
						meshRender.sharedMaterial = newRealityMaterial;
						meshRender.sharedMaterial.SetFloat("_Alpha", 1);
						string materialPath = Path.Combine(sceneDirectory, mapMaterial.name + ".mat");
						AssetDatabase.CreateAsset(newRealityMaterial, materialPath);
					}

					var meshrender = subObjTrans.GetComponent<MeshRenderer>();
					meshrender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
					subObjTrans.gameObject.tag = ConfigGlobal.TAG_REALITYSCENE;
					subObjTransList.Add(landObj.name, subObjTrans);
				}

				//判断是否存在default节点
				if (subObjTransList.Count > 0)
				{
					foreach (var item in subObjTransList)
					{
						var defualtTrans = item.Value;
						var name = item.Key;

						var lastParent = defualtTrans.parent;
						defualtTrans.transform.parent = realitysceneObj.transform;
						defualtTrans.transform.position = Vector3.zero;
						defualtTrans.transform.rotation = Quaternion.identity;
						defualtTrans.transform.localScale = Vector3.one;
						GameObject.DestroyImmediate(lastParent.gameObject) ;
						//save mesh
						Mesh mesh = defualtTrans.GetComponent<MeshFilter>().sharedMesh;
						Mesh meshToSave = Object.Instantiate(mesh) as Mesh;
						MeshUtility.Optimize(meshToSave);
						//x轴翻转
						Vector3[] vertices = meshToSave.vertices;
						for (int i = 0; i < vertices.Length; i++)
						{
							vertices[i].x *= -1;
						}
						meshToSave.vertices = vertices;
						defualtTrans.GetComponent<MeshFilter>().mesh = meshToSave;
						string meshPath = Path.Combine(sceneDirectory, name + ConfigGlobal.REALITY_SCENE_MESH_NAME);
						AssetDatabase.CreateAsset(meshToSave, meshPath);
					}
				}
				//根据导航参数
				//1.增加POI编辑器
				//2.增加导航模板编辑器
				AddNavElementEditor(sceneInfo,realitysceneObj);
			}
			else if (objExit && imgExit) 
			{
				//默认当没有mtl文件时，只有一个obj文件和一个png图片
				var objPath = objPathList[0];
				var imgName = imgPathList[0];
				
				//加载obj
                var objLoder = new OBJLoader();
                var landObj = objLoder.Load(objPath);//
                landObj.tag = ConfigGlobal.TAG_REALITYSCENE;

                //如果没有找到defualt命名的节点，则遍历land，获取有meshFilter组件的节点
                subObjTrans = landObj.transform.Find("default");
                if (subObjTrans == null)
                {
                    //Debug.Log("count:"+landObj.transform.childCount);
                    for (int i = 0; i < landObj.transform.childCount; i++)
                    {
                        var child = landObj.transform.GetChild(i);
                        //Debug.Log("child:" + child.name);
                        var mesh = child.GetComponent<MeshFilter>();
                        if (mesh != null)
                        {
                            subObjTrans = child;
                            break;
                        }
                    }
                }
				if (subObjTrans == null)
				{
					string msg = "Obj模型异常，子节点无defualt，并且子节点下无mesh存在\n地图拉取已终止";
                    Debug.LogError(msg);
					EditorUtility.DisplayDialog("Error", msg, "确认");
					return;
				}
                subObjTrans.gameObject.tag = ConfigGlobal.TAG_REALITYSCENE;

                //材质设置与保存
                originMaterialPath = Path.Combine(ConfigGlobal.REALITY_SCENE_MATERIAL_PATH, ConfigGlobal.REALITY_SCENE_MATERIAL_NAME);
                Material realityMaterial = AssetDatabase.LoadAssetAtPath(originMaterialPath, typeof(Material)) as Material;
				Material newRealityMaterial = new Material(realityMaterial);
				var meshRender = subObjTrans.GetComponent<MeshRenderer>();
                var mapMaterial = meshRender.sharedMaterial;//只获取名字
				string imgPath = Path.Combine(mapRootPath,imgName);
                Texture2D tx = AssetDatabase.LoadAssetAtPath<Texture2D>(imgPath);
                newRealityMaterial.mainTexture = tx;
				meshRender.sharedMaterial = newRealityMaterial;
                meshRender.sharedMaterial.SetFloat("_Alpha", 1);
                string materialPath = Path.Combine(sceneDirectory, mapMaterial.name + ".mat");
                AssetDatabase.CreateAsset(newRealityMaterial, materialPath);

                //备份mesh文件
                if (subObjTrans!=null)
                {
                    var defualtTrans = subObjTrans;
                    var name = subObjTrans.name;

                    var lastParent = defualtTrans.parent;
                    defualtTrans.transform.parent = realitysceneObj.transform;
                    defualtTrans.transform.position = Vector3.zero;
                    defualtTrans.transform.rotation = Quaternion.identity;
                    defualtTrans.transform.localScale = Vector3.one;
                    GameObject.DestroyImmediate(lastParent.gameObject);
                    //save mesh
                    Mesh mesh = defualtTrans.GetComponent<MeshFilter>().sharedMesh;
                    Mesh meshToSave = Object.Instantiate(mesh) as Mesh;
                    MeshUtility.Optimize(meshToSave);
                    //x轴翻转
                    Vector3[] vertices = meshToSave.vertices;
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        vertices[i].x *= -1;
                    }
                    meshToSave.vertices = vertices;
                    defualtTrans.GetComponent<MeshFilter>().mesh = meshToSave;
                    string meshPath = Path.Combine(sceneDirectory, name + ConfigGlobal.REALITY_SCENE_MESH_NAME);
                    AssetDatabase.CreateAsset(meshToSave, meshPath);
                }
            }
			else if(objExit)
			{
                var objPath = objPathList[0];
                //加载obj
                var objLoder = new OBJLoader();
                var landObj = objLoder.Load(objPath);//
                landObj.tag = ConfigGlobal.TAG_REALITYSCENE;

                //如果没有找到defualt命名的节点，则遍历land，获取有meshFilter组件的节点
                subObjTrans = landObj.transform.Find("default");
                if (subObjTrans == null)
                {
                    //Debug.Log("count:"+landObj.transform.childCount);
                    for (int i = 0; i < landObj.transform.childCount; i++)
                    {
                        var child = landObj.transform.GetChild(i);
                        //Debug.Log("child:" + child.name);
                        var mesh = child.GetComponent<MeshFilter>();
                        if (mesh != null)
                        {
                            subObjTrans = child;
                            break;
                        }
                    }
                }
                if (subObjTrans == null)
                {
                    string msg = "Obj模型异常，子节点无defualt，并且子节点下无mesh存在\n地图拉取已终止";
                    Debug.LogError(msg);
                    EditorUtility.DisplayDialog("Error", msg, "确认");
                    return;
                }
                subObjTrans.gameObject.tag = ConfigGlobal.TAG_REALITYSCENE;
                //备份mesh文件
                if (subObjTrans != null)
                {
                    var defualtTrans = subObjTrans;
                    var name = subObjTrans.name;

                    var lastParent = defualtTrans.parent;
                    defualtTrans.transform.parent = realitysceneObj.transform;
                    defualtTrans.transform.position = Vector3.zero;
                    defualtTrans.transform.rotation = Quaternion.identity;
                    defualtTrans.transform.localScale = Vector3.one;
                    GameObject.DestroyImmediate(lastParent.gameObject);
                    //save mesh
                    Mesh mesh = defualtTrans.GetComponent<MeshFilter>().sharedMesh;
                    Mesh meshToSave = Object.Instantiate(mesh) as Mesh;
                    MeshUtility.Optimize(meshToSave);
                    //x轴翻转
                    Vector3[] vertices = meshToSave.vertices;
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        vertices[i].x *= -1;
                    }
                    meshToSave.vertices = vertices;
                    defualtTrans.GetComponent<MeshFilter>().mesh = meshToSave;
                    string meshPath = Path.Combine(sceneDirectory, name + ConfigGlobal.REALITY_SCENE_MESH_NAME);
                    AssetDatabase.CreateAsset(meshToSave, meshPath);
                }
            }
            //根据导航参数
            //1.增加POI编辑器
            //2.增加导航模板编辑器
            AddNavElementEditor(sceneInfo, realitysceneObj);

            //add camera preview
            GameObject toolmanGo = GameObject.Find("toolman");
			if (toolmanGo == null)
			{
				string toolManPath = Path.Combine(ConfigGlobal.MODEL_PREFAB_PATH, ConfigGlobal.TOOL_MAN_NAME);
				GameObject toolManPrefab = AssetDatabase.LoadAssetAtPath(toolManPath, typeof(GameObject)) as GameObject;
				toolmanGo = GameObject.Instantiate(toolManPrefab);
				toolmanGo.name = "toolman";
				toolmanGo.AddComponent<CameraPositionPreview>();
			}
			CameraPositionPreview cameraPositionPreview = toolmanGo.GetComponent<CameraPositionPreview>();
			cameraPositionPreview.InitPath((long)sceneInfo.mapId);

			AssetDatabase.SaveAssets();
			EditorSceneManager.SaveScene(currentScene, scenePath);
			AssetDatabase.Refresh();
			Debug.LogFormat("create scene ：{0} - {1} success", sceneInfo.id, sceneInfo.name);
		}

		/// <summary>
		/// 增加poi节点
		/// </summary>
		/// <param name="root"></param>
		/// <param name="sceneInfo"></param>
		/// <param name="origin"></param>
		/// <param name="matrix"></param>
		void AddPoiObject(Transform root, MyContentsResultData sceneInfo, List<double> origin, List<double> matrix)
		{
			GameObject poiGo = GameObject.Find("World/Land/POI");
			if (poiGo == null)
			{
				poiGo = new GameObject();
				poiGo.transform.parent = root.transform;
				poiGo.transform.localPosition = Vector3.zero;
				poiGo.name = "POI";
			}
			POIMap poiMap = poiGo.GetComponent<POIMap>();
			if (poiMap == null) poiMap = poiGo.AddComponent<POIMap>();
			poiMap.Init((long)sceneInfo.appId, (long)sceneInfo.id, (long)sceneInfo.mapId, sceneInfo.engineType, origin, matrix, poiGo.transform);
		}
		void AddNavElementEditor(MyContentsResultData sceneInfo)
		{
			GameObject navRoot = GameObject.Find("naviroot");
			if (navRoot == null)
			{
				var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(ConfigGlobal.NAV_ROOT);
				navRoot = Instantiate(prefab);
				navRoot.name = "naviroot";
				navRoot.transform.localPosition = Vector3.zero;
			}
			var sc = navRoot.GetComponent<NavElement>();
			if (sc != null)
			{
				sc.Init(sceneInfo.id);
				return;
			}
			NavElement nav = navRoot.AddComponent<NavElement>();
			nav.Init(sceneInfo.id);
		}

		/// <summary>
		/// 绘制URL
		/// </summary>
		/// <param name="rect"></param>
		void DrawURL(Rect rect)
		{
			GUILayout.BeginArea(rect);
			GUILayout.BeginHorizontal();
			GUILayout.Label(AlgorithmGlobal.URL_TITLE, skin.label);
			GUILayout.BeginVertical();
			GUILayout.Space(6);

			if (GUILayout.Button(AlgorithmGlobal.URL_CONTENT, urlStyle))
			{
				Application.OpenURL(AlgorithmGlobal.URL);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		void DrawSearchContentItem(Rect position)
		{
			float moveDis = 200;
			var searchTextPosition = new Rect(position.x + moveDis, position.y - 45, 200, 40);
			var searchPosition = new Rect(position.x + moveDis+130, position.y - 45, 200, 40);
			var resetButtonPosition = new Rect(position.x + moveDis+350, position.y - 45,50,40);
			EditorGUI.LabelField(searchTextPosition, "请输入需要查询的内容");
			curSearchContent = EditorGUI.TextField(searchPosition, "", curSearchContent, searchStyle);
            if (curSearchContent != lastSearchContent)
            {
				lastSearchContent = curSearchContent;
				_instance.ReloadAssets();
            }
            if (GUI.Button(resetButtonPosition,"重置"))
            {
				lastSearchContent = curSearchContent = "";
				curSearchContent = EditorGUI.TextField(searchPosition, "", curSearchContent, searchStyle);
				//_instance.ShowModal();
				//_instance.ShowNotification(new GUIContent("asdad"));
				GUIUtility.keyboardControl = 0;
				_instance.ReloadAssets();
			}
		}

		void OnGetAppSdkVersionsSuccess(GetAppSdkVersionsResponseData data,long contentID, int engineType,int deviceType)
		{
			AddContentVersionEditorWindow.ShowWindow(data, contentID,engineType, deviceType);
        }
		void OnGetAppSdkVersionsFail(string code,string msg)
		{
			EditorUtility.DisplayDialog("Error","获取App SDK Version失败。\n code = "+code+"\n msg = "+msg,"确认");
		}

		void AddNavElementEditor(MyContentsResultData sceneInfo, GameObject realitysceneObj)
		{
            //v1.6.1 平台去掉原点和矩阵，通过Geo资源包中transform.json读取（经纬坐标点和对应的变换矩阵）
            #region 从geo中获取原点与矩阵
            string mapRootPath = Path.Combine(ConfigGlobal.MAP_PATH, sceneInfo.mapId.ToString(), ((int)MapResourcesType.RESOURCE_TYPE_TWOD_DISPLAYM_MAP).ToString());
			string transformJsonWholePath = Path.Combine(mapRootPath,ConfigGlobal.TRANSFORM_NAME);
			if (!File.Exists(transformJsonWholePath))
			{
                EditorUtility.DisplayDialog("Error", "未找到Geo文件包中Transform.json文件，请检查平台-地图配置-二维显示地图，是否有配置正确Geo文件", "确认");
				return;
            }
            string transformDataString = File.ReadAllText(transformJsonWholePath);
            JObject transformJObject = JObject.Parse(transformDataString);
            if (transformJObject["UtmOffsetEast"]== null || transformJObject["UtmOffsetNorth"] == null || transformJObject["TfInfos"] == null)
            {
                EditorUtility.DisplayDialog("Error", "transform.json文件错误，json字段未找到，请检查json：UtmOffsetEast 、UtmOffsetNorth 、TfInfos 字段", "确认");
                return;
            }
            var x =(double)transformJObject["UtmOffsetEast"];
			var y = (double)transformJObject["UtmOffsetNorth"];
			List<double> origin = new List<double>() { x, y };
            // 矩阵信息数组
            var tfInfos = transformJObject["TfInfos"];
            List<double>  matrixList = new List<double>();
            // 查找楼层的矩阵信息 当前任意一层矩阵皆可
            foreach (var tfInfo in tfInfos)
            {
                var levelId = (int)tfInfo["FloorIds"];
                var dataArray = (JArray)tfInfo["TransformFromAlg2Geo"];
                matrixList = ArrayToList(dataArray);
				break;
            }
            #endregion

            //根据导航参数
            //1.增加POI编辑器
            //2.增加导航模板编辑器
            if (sceneInfo.GetNavMode() != null) //新版通过navigateModel判断
            {
                //添加poi编辑器
                if (sceneInfo.GetNavMode() == 2 || sceneInfo.GetNavMode() == 3 || sceneInfo.GetNavMode() == 4)
                {
                    AddPoiObject(realitysceneObj.transform, sceneInfo, origin, matrixList);
                }
                //添加导航元素编辑器
                if (sceneInfo.GetNavMode() == 4)
                {
                    AddNavElementEditor(sceneInfo);
                }
            }
            else //兼容旧版 navigate属性
            {
                if (sceneInfo.navigate == 1)
                {
                    AddPoiObject(realitysceneObj.transform, sceneInfo, origin, matrixList);
                    AddNavElementEditor(sceneInfo);
                }
            }
        }
        private List<double> ArrayToList(JArray jArray)
        {
            List<double> list = new List<double>(16);
            for (int i = 0; i < jArray.Count; i++)
            {
                list.Add((float)jArray[i]);
            }
            return list;
        }
        /// <summary>
        ///     获取MTL文件路径,只要找到一个MTL文件就默认为使用该文件
        /// </summary>
        /// <param name="parentPath">mtl所在文件夹目录</param>
        /// <param name="path"></param>
        /// <returns></returns>
        bool TryGetMTLFilePath(string parentPath,out string path)
		{
			path = null;
            if (!Directory.Exists(parentPath))
            {
                EditorUtility.DisplayDialog("Error", "地图MTL文件未找到，请尝试重新拉取内容", "确认");
				return false;
            }
            DirectoryInfo info = new DirectoryInfo(parentPath);
            FileInfo[] files = info.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name.EndsWith(".mtl"))
                {
                    path = file.FullName;
					return true;
                }
            }
			path= null; 
			return false;
        }
		bool TryGetOBJFilesPath(string parentPath,out List<string> objPathList)
		{
			objPathList = new List<string>();
            if (!Directory.Exists(parentPath))
            {
                EditorUtility.DisplayDialog("Error", "地图OBJ文件夹未找到，请尝试重新拉取内容", "确认");
				return false;
            }
            DirectoryInfo info = new DirectoryInfo(parentPath);
            FileInfo[] files = info.GetFiles();
			foreach (var item in files)
			{
				if (item.Name.EndsWith(".obj"))
				{
					objPathList.Add(item.FullName);
				}
			}

			if (objPathList.Count <= 0) 
				return false;
            return true;
        }
		bool TryGetImageFilePath(string parentPath, out List<string> imgPathList)
		{

			imgPathList = new List<string>();
            if (!Directory.Exists(parentPath))
            {
                EditorUtility.DisplayDialog("Error", "地图Texture文件未找到，请尝试重新拉取内容", "确认");
				return false;
            }
            DirectoryInfo info = new DirectoryInfo(parentPath);
            FileInfo[] files = info.GetFiles();
            foreach (var item in files)
            {
                if (item.Name.EndsWith(".png")|| item.Name.EndsWith(".jpg"))
                {
                    imgPathList.Add(item.Name);
                }
            }
            if (imgPathList.Count <= 0)
                return false;
            return true;
        }

        #endregion
    }
}

#endif