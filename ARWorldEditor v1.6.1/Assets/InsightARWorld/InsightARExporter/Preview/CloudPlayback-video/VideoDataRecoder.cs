using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;
using UnityEngine.Events;
#if UNITY_EDITOR
namespace ARWorldEditor.CloudPlayBack
{

    #region DataClass
    /// <summary>
    /// 注意：因为算法侧约定图片以横向形式，这时候，数据中的cy需要看作为cx ,fy看做fx
    /// https://blog.csdn.net/renmengqisheng/article/details/121011525
    /// 所以水平方向FOV=2 * atan(cx, fx)，实际是垂向FOV
    /// </summary>
    public enum ScreenHVType
    {
        Horizontal,
        Vertical,
    }
    //locviotraj 文件解析
    public class locVioTraj
    {
        public List<InsightARDebugFrameCamInfoLoc> loc_vio_traj;
    }
    public class InsightARDebugFrameCamInfoLoc
    {
        public int state;            // 2: detecting; 3: tracking; 4: trackingLimited

        //截取小数点后三位，不进行四舍五入
        public string timeStamp;        // timestamp
        public float[] position;      // x, y, z
        public float[] rotation;      // qx, qy, qz, qw
    }
    //cameraparam 文件解析
    public class CameraParams
    {
        public List<InsightARDebugFrameImageMetaLoc> camera_params;
    }
    public class InsightARDebugFrameImageMetaLoc
    {
        //截取小数点后三位，不进行四舍五入
        public string imageTimeStamp;      // timestamp
        public float fx;                  // fx
        public float fy;                  // fy
        public float cx;                  // cx
        public float cy;                  // cy
        public int imageWidth;          // width
        public int imageHeight;         // height
    }

    //缓存每一个pose数据
    public class RecordPoint
    {
        public string imagePath;
        public Texture2D imageTexSprite;

        public Vector3 position;
        public Quaternion rotation;
        /// <summary>
        /// x:width y:height
        /// </summary>
        public Vector2Int imgWidthAndHeight;
        public float hFov;
        public float vFov;
        public string cameraType;
        /// <summary>
        /// x:width y:height
        /// </summary>
        public Vector2Int cameraWidthAndHeight;


        //为渲染一个点的相机pose，创建绑定脚本的对象 
        public GameObject attachGameObj;
        //渲染相机pose的脚本
        public DrawFrustum guiScript;
        public RecordPoint(string path, Vector3 pos, Quaternion qua)
        {
            imagePath = path;
            position = pos;
            rotation = qua;
        }
    }
    #endregion


    public class VideoDataRecoder : MonoBehaviour
    {
        public string camFilePath = "";
        public string tumFilePath = "";
        public string imageFolderPath = "";

        public static List<RecordPoint> poseList = new List<RecordPoint>();
        public static Dictionary<string, RecordPoint> poseDic = new Dictionary<string, RecordPoint>();

        private Slider slider;
        private Text totalCountText;
        private Text curCountText;
        private InputField input;
        private Camera camera;
        private Button playButton;


        //视频流背景
        private RawImage videoImage;
        private CanvasScaler videoCanvasScaler;

        private int curPointIndex = 1;
        private int lastPointIndex = 0;
        private RecordPoint firstInitRecordPoint;//缓存一次带图像的数据，方便对视频流回放组件进行初始化

        //控制视频流播放
        private bool enableVideoPlay = false;
        //是否播放图片是暂停
        [Tooltip("当播放到图片时暂停播放")]
        public bool pauseThePlay = false;

        //横竖屏控制
        public ScreenHVType screenHVType = ScreenHVType.Horizontal;
        /// <summary>
        /// 初始化目录
        /// </summary>
        /// <param name="ezxrbm_bm_stone_previewPath"></param>
        public void Init(string rootpath, string tumpath, string campath)
        {
            this.camFilePath = campath;
            this.tumFilePath = tumpath;
            this.imageFolderPath = rootpath;
        }

        public void Start()
        {
            camera = Camera.main;
            //限制在编辑器中的帧率

            //读取文件
            LoadFile();

            //初始视频流canvas
            InitVideoCanvas();

            //初始相机pose显示GUi
            InitSinglePointList();

            //滑动控制相机pose
            InitControlMudule();

            //设置Game视图尺寸
            SetGameViewSize();

            //初始图片列表
            StartCoroutine(LoadTexture());

            Application.targetFrameRate = 30;
        }
        /// <summary>
        /// 加载文件
        /// </summary>
        public void LoadFile()
        {
            poseList.Clear();
            poseDic.Clear();
            firstInitRecordPoint = null;

            if (string.IsNullOrEmpty(camFilePath))
            {
                EditorUtility.DisplayDialog("错误", "相机参数文件路径为空，请在编辑器中输入对应文件的文件名", "确认");
                return;
            }
            if (string.IsNullOrEmpty(tumFilePath))
            {
                EditorUtility.DisplayDialog("错误", "相机pose数据文件路径为空，请在编辑器中输入对应文件的文件名", "确认");
                return;
            }
            string tumText, camText;
            try
            {
                tumText = File.ReadAllText(tumFilePath);
                camText = File.ReadAllText(camFilePath);
            }
            catch (System.Exception e)
            {
                EditorUtility.DisplayDialog("错误", "文件读取异常，详细请查看unity console error", "确认");
                throw e;
            }

            #region tumText解析
            if (tumText[tumText.Length - 4] == ',')
            {
                tumText.Remove(tumText.Length - 4);
            }
            locVioTraj tumClass = JsonUtil.Deserialization<locVioTraj>(tumText);
            foreach (var item in tumClass.loc_vio_traj)
            {
                Matrix4x4 matrixLeft = new Matrix4x4(
                    new Vector4(1, 0, 0, 0),
                    new Vector4(0, 0, 1, 0),
                    new Vector4(0, 1, 0, 0),
                    new Vector4(0, 0, 0, 1));

                Matrix4x4 matrixRight = new Matrix4x4(
                    new Vector4(1, 0, 0, 0),
                    new Vector4(0, -1, 0, 0),
                    new Vector4(0, 0, 1, 0),
                    new Vector4(0, 0, 0, 1));

                Matrix4x4 matrixMid = new Matrix4x4();
                matrixMid.SetTRS(new Vector3(item.position[0], item.position[1], item.position[2]),
                    new Quaternion(item.rotation[0], item.rotation[1], item.rotation[2], item.rotation[3]), Vector3.one);

                var result = matrixLeft * matrixMid * matrixRight;
                float w = Mathf.Sqrt(1 + result.m00 + result.m11 + result.m22) / 2;
                float tt = 4 * w;
                float x = (result.m21 - result.m12) / tt;
                float y = (result.m02 - result.m20) / tt;
                float z = (result.m10 - result.m01) / tt;

                string imagePath = item.timeStamp + ".jpg";
                Vector3 pos = new Vector3(result.m03, result.m13, result.m23);
                Quaternion angle = new Quaternion(x, y, z, w);
                RecordPoint single = new RecordPoint(imagePath, pos, angle);
                if (!poseDic.ContainsKey(item.timeStamp))
                {
                    poseList.Add(single);
                    poseDic.Add(item.timeStamp, single);
                }
                else
                {
                    EditorUtility.DisplayDialog("错误", "重复数据 ，请联系技术侧确认：\n"+item.timeStamp, "确认");
                }
            }
            #endregion

            #region camText解析
            //Debug.LogError("tum text");
            if (camText[camText.Length - 4] == ',')
            {
                camText.Remove(camText.Length - 4);
            }
            CameraParams camClass = JsonUtil.Deserialization<CameraParams>(camText);

            //预处理 因为 首次Camera_params数据可能是一个比较早时间获取的，
            //所以，如果出现camera_param首次时间戳早于loc_vio时间戳时
            //将该次图片忽略

            var firstLocVicTime = double.Parse(tumClass.loc_vio_traj[0].timeStamp);
            var firstCameraParamsTime = double.Parse( camClass.camera_params[0].imageTimeStamp);
            if (firstCameraParamsTime < firstLocVicTime)
            {
                EditorUtility.DisplayDialog("错误", "camera_param首次时间戳早于loc_vio_traj时间戳，请联系技术侧确认：\n", "确认");
                //camClass.camera_params.RemoveAt(0);
            }
            //Debug.Log("firstLocVicTime:"+ firstLocVicTime+ " _ firstCameraParamsTime"+ firstCameraParamsTime);
            for (int i = 0; i < camClass.camera_params.Count; i++)
            {
                var item = camClass.camera_params[i];

                if (poseDic.TryGetValue(item.imageTimeStamp, out var connectItem))
                {
                    connectItem.cameraType = "";
                    connectItem.imgWidthAndHeight = new Vector2Int(item.imageWidth, item.imageHeight);
                    connectItem.vFov = 2 * Mathf.Atan(item.cx / item.fx) * 180 / Mathf.PI;
                    connectItem.hFov = 2 * Mathf.Atan(item.cy / item.fy) * 180 / Mathf.PI;
                    connectItem.cameraWidthAndHeight = new Vector2Int(item.imageWidth, item.imageHeight);
                    if (firstInitRecordPoint == null)
                    {
                        firstInitRecordPoint = connectItem;
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("错误", "camera_params与loc_vio 中时间戳未同步，请联系技术侧确认：\n" + item.imageTimeStamp, "确认");
                    return;
                    //Debug.LogError("camera_params与loc_vio 中时间戳未同步，请确认：\\n" + item.imageTimeStamp);
                    //存在camera_params 时间戳未完全对齐Loc_vio情况，在0.001单位上有误差，在编辑器中进行调整
                    var temp = double.Parse(item.imageTimeStamp) - 0.001;
                    if (poseDic.TryGetValue(temp.ToString("f3"), out connectItem))
                    {
                        connectItem.cameraType = "";
                        connectItem.imgWidthAndHeight = new Vector2Int(item.imageWidth, item.imageHeight);
                        connectItem.vFov = 2 * Mathf.Atan(item.cx / item.fx) * 180 / Mathf.PI;
                        connectItem.hFov = 2 * Mathf.Atan(item.cy / item.fy) * 180 / Mathf.PI;
                        connectItem.cameraWidthAndHeight = new Vector2Int(item.imageWidth, item.imageHeight);
                        if (firstInitRecordPoint == null)
                        {
                            firstInitRecordPoint = connectItem;
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("错误", "camera_params与loc_vio 中时间戳未同步，请确认：\n"+ item.imageTimeStamp, "确认");
                    }
                }
            }
            #endregion

        }
        public IEnumerator LoadTexture()
        {
            for (int i = 0; i < poseList.Count; i++)
            {
                var posePoint = poseList[i];
                var imageFilePath = posePoint.imagePath;
                var imageWholePath = this.imageFolderPath +"\\"+ imageFilePath;
                if (!File.Exists(imageWholePath))
                {
                    //EditorUtility.DisplayDialog("异常", imageWholePath + "\n路径地址查找为空，请检查图片地址是否正常", "确认");
                    //if (EditorUtility.DisplayDialog("提示", "中断生成?", "确认", "继续（存在异常）"))
                    //{
                    //    break;
                    //}
                    continue;
                }
                Texture2D tx = new Texture2D(posePoint.imgWidthAndHeight.x, posePoint.imgWidthAndHeight.y);
                var imageByte = getImageByte(imageWholePath);
                tx.LoadImage(imageByte);
                posePoint.imageTexSprite = tx;
                yield return new WaitForEndOfFrame();
            }
        }
        public void InitSinglePointList()
        {
            GameObject prefab = Resources.Load("Sphere") as GameObject;
            var parent = new GameObject("parent").transform;
            parent.parent = transform;
            foreach (var item in poseList)
            {
                var obj = GameObject.Instantiate(prefab, parent).transform;
                obj.position = item.position;
                obj.rotation = item.rotation;
                obj.parent = parent;
                obj.hideFlags = HideFlags.HideInHierarchy;
                item.attachGameObj = obj.gameObject;
                //objs.Add(obj.gameObject);
            }
        }
        public void InitControlMudule()
        {
            var controlCanvas = GameObject.Instantiate(Resources.Load("controlCanvas") as GameObject);
            var controlCanvasScaler = controlCanvas.GetComponent<CanvasScaler>();
            controlCanvasScaler.scaleFactor = firstInitRecordPoint.imgWidthAndHeight.x / 1600f;
            slider = controlCanvas.GetComponentInChildren<Slider>();
            slider.maxValue = poseList.Count;
            slider.minValue = 1;
            slider.onValueChanged.AddListener((float value) => { OnSliderScroll(value); });
            totalCountText = slider.transform.Find("total").GetComponent<Text>();
            curCountText = slider.transform.Find("cur").GetComponent<Text>();
            input = slider.transform.Find("targetNum").GetComponent<InputField>();
            input.onValueChanged.AddListener((string value) => { OnInputFieldChange(value); });
            totalCountText.text = poseList.Count.ToString();
            OnSliderScroll(curPointIndex);

            this.playButton = controlCanvas.transform.Find("Play").GetComponent<Button>();
            var rePlayButton = controlCanvas.transform.Find("Restart").GetComponent<Button>();
            var playObj = controlCanvas.transform.Find("Play/play").gameObject;
            var pauseObj = controlCanvas.transform.Find("Play/pause").gameObject;
            var frameInput = controlCanvas.transform.Find("frameInput").GetComponent<InputField>();

            playButton.onClick.AddListener(() =>
            {
                enableVideoPlay = !enableVideoPlay;
                if (enableVideoPlay)
                {
                    pauseObj.SetActive(true);
                    playObj.SetActive(false);
                }
                else
                {
                    pauseObj.SetActive(false);
                    playObj.SetActive(true);
                }
            });

            rePlayButton.onClick.AddListener(() =>
            {
                if (!enableVideoPlay) enableVideoPlay = true;
                curPointIndex = 1;
                lastPointIndex = 0;
            });

            frameInput.onValueChanged.AddListener((string tt) =>
            {
                Application.targetFrameRate = int.Parse(tt);
            });
        }
        public void InitVideoCanvas()
        {
            var canvsPrefab = (GameObject)Resources.Load("videoCanvas");
            var newVideoCanvsObj = GameObject.Instantiate(canvsPrefab, this.transform);
            var newVideoCanvs = newVideoCanvsObj.GetComponent<Canvas>();

            videoCanvasScaler = newVideoCanvsObj.GetComponent<CanvasScaler>();
            videoImage = newVideoCanvsObj.transform.GetChild(0).GetComponent<RawImage>();
            //设置观察相机
            newVideoCanvs.worldCamera = camera;
            //设置距离尽可能远
            newVideoCanvs.planeDistance = 999;//(相机最远平面 - 1)
            //设置
            videoCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            //设置canvas 分辨率
            videoCanvasScaler.referenceResolution = new Vector2(firstInitRecordPoint.imgWidthAndHeight.x, firstInitRecordPoint.imgWidthAndHeight.y);
            if (screenHVType == ScreenHVType.Vertical)
            {
                videoCanvasScaler.referenceResolution = new Vector2(firstInitRecordPoint.imgWidthAndHeight.y, firstInitRecordPoint.imgWidthAndHeight.x);
                videoCanvasScaler.matchWidthOrHeight = 1;
            }
        }
        public void SetGameViewSize()
        {
            UnityEditor.GameViewSizeGroupType gameViewSizeGroupType = UnityEditor.GameViewSizeGroupType.Standalone;
#if UNITY_ANDROID
            gameViewSizeGroupType = UnityEditor.GameViewSizeGroupType.Android;
#elif UNITY_IOS
            gameViewSizeGroupType = UnityEditor.GameViewSizeGroupType.iOS;
#endif

            var imgSize = firstInitRecordPoint.imgWidthAndHeight;
            var size = new Vector2Int(imgSize.x, imgSize.y);
            if (screenHVType == ScreenHVType.Vertical)
            {
                size = new Vector2Int(imgSize.y, imgSize.x);
            }
            string des = "视频回放-" + size.x + "*" + size.y;
            int index = GameViewUtils.FindSize(gameViewSizeGroupType, des);
            if (index == -1)
            {
                GameViewUtils.AddResolution(GameViewUtils.GameViewSizeType.FixedResolution, gameViewSizeGroupType, size.x, size.y, des);
                index = GameViewUtils.FindSize(gameViewSizeGroupType, size.x, size.y);
                Debug.Log("GameView:" + des);
                GameViewUtils.SetSize(index);
            }
            else
            {
                Debug.Log("GameView:" + des);
                GameViewUtils.SetSize(index);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (curPointIndex < slider.maxValue)
                {
                    curPointIndex++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (curPointIndex > 1)
                {
                    curPointIndex--;
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                if (curPointIndex < slider.maxValue)
                {
                    curPointIndex++;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (curPointIndex > 1)
                {
                    curPointIndex--;
                }
            }

            if (lastPointIndex != curPointIndex)
            {
                if (slider == null)
                {
                    return;
                }
                slider.value = curPointIndex;
                lastPointIndex = curPointIndex;

            }

            if (enableVideoPlay)
            {
                if (poseList[curPointIndex].imageTexSprite!=null && pauseThePlay)
                {
                    playButton.onClick.Invoke();
                }

                curPointIndex++;
                if (curPointIndex >= poseList.Count - 1)
                {
                    enableVideoPlay = false;
                    return;
                }
            }

        }
        /// <summary>  
        /// 根据图片路径返回图片的字节流byte[]  
        /// </summary>  
        /// <param name="imagePath">图片路径</param>  
        /// <returns>返回的字节流</returns>  
        private static byte[] getImageByte(string imagePath)
        {
            using (FileStream files = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                byte[] imgByte = new byte[files.Length];
                files.Read(imgByte, 0, imgByte.Length);
                //Debug.Log(imgByte[1]);
                return imgByte;
            }
        }
        public void OnSliderScroll(float value)
        {
            int num = (int)value;
            if (num > poseList.Count || num < 1)
            {
                return;
            }
            curCountText.text = num.ToString();
            curPointIndex = num;
            num--;
            var point = poseList[num];
            //设置camera pose
            camera.transform.position = point.position;
            camera.transform.rotation = point.rotation;
            //设置相机 pose GUI显示 
            for (int i = 0; i < poseList.Count; i++)
            {
                if (i <= num)
                {
                    if (!poseList[i].attachGameObj.activeSelf)
                    {
                        poseList[i].attachGameObj.SetActive(true);
                    }
                }
                else
                {
                    if (poseList[i].attachGameObj.activeSelf)
                    {
                        poseList[i].attachGameObj.SetActive(false);
                    }
                }
            }

            //设置相机FOV

            //设置当前视频流背景

            //图片
            if (point.imageTexSprite != null)
            {
                videoImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(point.imgWidthAndHeight.x, point.imgWidthAndHeight.y);
                videoImage.texture = point.imageTexSprite;
            }
            else
            {
                videoImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(firstInitRecordPoint.imgWidthAndHeight.x, firstInitRecordPoint.imgWidthAndHeight.y);
                videoImage.texture = null;
            }

            //横屏
            if (screenHVType == ScreenHVType.Horizontal)
            {
                //fov
                if (point.hFov != 0)
                {
                    camera.fieldOfView = point.hFov;//Mathf.Atan(point.cameraWidthAndHeight.y / point.rate) * 2 *180 / Mathf.PI ;
                }
                else
                {
                    camera.fieldOfView = firstInitRecordPoint.hFov;//Mathf.Atan(point.cameraWidthAndHeight.y / point.rate) * 2 *180 / Mathf.PI ;
                }

            }
            //竖屏
            else
            {
                if (point.vFov != 0)
                {
                    camera.fieldOfView = point.vFov;//Mathf.Atan(point.cameraWidthAndHeight.y / point.rate) * 2 *180 / Mathf.PI ;
                }
                else
                {
                    camera.fieldOfView = firstInitRecordPoint.vFov;//Mathf.Atan(point.cameraWidthAndHeight.y / point.rate) * 2 *180 / Mathf.PI ;
                }

                camera.transform.Rotate(Vector3.forward, 90);
                videoImage.transform.localEulerAngles = Vector3.forward * -90;
            }
            //Selection.activeGameObject = objs[num];
        }
        public void OnInputFieldChange(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            curPointIndex = int.Parse(value);
        }
    }

    public class ReadTextEditor : EditorWindow
    {
        public static string tumTextPath = "";
        public static string camTextPath = "";
        public static string rootPath = "";
        //[MenuItem("EZXR/端云回放-视频", false, 10000)]
        public static void ShowWindow()
        {
            ReadTextEditor myWindow = (ReadTextEditor)EditorWindow.GetWindow(typeof(ReadTextEditor), false, "端云回放-视频", true);//创建窗口
            myWindow.Show();//展示
        }
        private void OnGUI()
        {
            GUILayout.Label("注意，以下路径选择请对照文档，注意层级");
            GUILayout.Space(20);

            //tum
            GUILayout.Label("1---tum文件完整路径，包含文件名，如xxx\\xx\\xx\\loc_preview_tum.text");
            tumTextPath = GUILayout.TextField(tumTextPath);
            if (GUILayout.Button("tum文件选择"))
            {
                tumTextPath = OpenFile.OpenWinFile("tum文件");
            }
            GUILayout.Space(10);

            //cam
            GUILayout.Label("2---cam文件完整路径，包含文件名，如xxx\\xx\\xx\\loc_preview_cam.text");
            camTextPath = GUILayout.TextField(camTextPath);
            if (GUILayout.Button("cam文件选择"))
            {
                camTextPath = OpenFile.OpenWinFile("cam文件");
            }
            GUILayout.Space(10);

            //Root
            GUILayout.Label("3---图片Root文件夹路径");
            rootPath = GUILayout.TextField(rootPath);
            if (GUILayout.Button("ROOT文件夹选择"))
            {
                rootPath = OpenFile.OpenWinFolder(rootPath);
            }
            GUILayout.Space(10);
            GUILayout.Label("确认路径完整后可点击生成控制器");
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(tumTextPath) || string.IsNullOrEmpty(camTextPath));
            if (GUILayout.Button("生成"))
            {
                Create(rootPath, tumTextPath, camTextPath);
            }
            EditorGUI.EndDisabledGroup();


            GUILayout.BeginVertical();
            GUILayout.Label("\n测试方式：滑动进度条或快捷键");
            GUILayout.Label("快捷键：↑ - 连续增加");
            GUILayout.Label("快捷键：↓ - 连续减小");
            GUILayout.Label("快捷键：← - 单独减小");
            GUILayout.Label("快捷键：右 - 单独增加");



            GUILayout.EndVertical();
        }

        public static void Create(string rootPath, string tumPath, string camPath)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            GameObject parent = null;
            foreach (var item in scene.GetRootGameObjects())
            {
                if (item.name == "点云视频回放")
                {
                    parent = item;
                    break;
                }
            }
            VideoDataRecoder sc;
            if (parent == null)
            {
                parent = new GameObject("点云视频回放");
                sc = parent.AddComponent<VideoDataRecoder>();
            }
            else
            {
                sc = parent.GetComponent<VideoDataRecoder>();
            }
            sc.Init(rootPath, tumPath, camPath);
        }
    }
}

#endif