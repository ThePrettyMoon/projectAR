
[System.Serializable]
public class AlgorithmData
{
    [System.Serializable]
    public class MarkerImage
    {
        public string md5 = "";
        public string remotePath = "";
        public string remoteName = "";
        public string localPath = "";
        public string localGUIDPath = "";
        public string localName = "";
        public float markerWidth = 1.0f;
        public void Copy(MarkerImage b)
        {
            this.md5 = b.md5;
            this.remotePath = b.remotePath;
            this.remoteName = b.remoteName;
            this.localPath = b.localPath;
            this.localGUIDPath = b.localGUIDPath;
            this.localName = b.localName;
        }
    }
    [System.Serializable]
    public class VideoConfig
    {
        public enum POSITION { back, front };
        public enum ORIENTATION { LandScapeRight, LandScapeLeft };

        // 设定帧率，默认30
        public int fps = 30;
        //预览图像的分辨率级别，默认480p，支持720p
        public string preset = "720p";
        //设定前置相机还是后置相机，默认back
        public POSITION position = POSITION.back;
        // 设置相机的方向，默认right
        public ORIENTATION orientation = ORIENTATION.LandScapeRight;
        // 设置相机的图像格式，默认420f 
        public string format = "420f";

        public static bool operator ==(VideoConfig a, VideoConfig b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(VideoConfig a, VideoConfig b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            VideoConfig b = (VideoConfig)obj;
            return (fps == b.fps
                && preset == b.preset
                && position == b.position
                && orientation == b.orientation
                && format == b.format);
        }

        public override int GetHashCode()
        {
            return fps.GetHashCode() ^ preset.GetHashCode() ^ position.GetHashCode() ^ orientation.GetHashCode() ^ format.GetHashCode();
        }

        public override string ToString()
        {
            string ret = "";
            ret += "-fps:" + fps.ToString() + ",";
            ret += "-preset:" + preset.ToString() + ",";
            ret += "-position:" + position.ToString() + ",";
            ret += "-orientation:" + orientation.ToString() + ",";
            ret += "-format:" + format.ToString();
            return ret;
        }
    }

    // 总开关
    public bool isARConfigEnabled = true;

    // 服务器设置
    public bool bServerSettings = false;
    public string textureUploadURL = "http://mr-test.dongjian.netease.com/biz/nos/upload";
    public string markerRequestURL = "http://mr-test.dongjian.netease.com/biz/at/genalg";

    // 基础设置
    public bool bBasicSettings = true;
    public string sceneName = "";
    // 检测类型，1：图片识别；2：3D识别（仅洞见APP）；3：场景识别；4：人脸识别（仅洞见APP）。

    public int algDetectingType = 3;
    public int detectGestureOrPose = 0;// 0表示不需要检测，1表示手势检测，2表示人体检测；
    public bool bDetectGesture = false;// 当检测类型为3和6或3和4时需要额外加这个参数
    public bool bDetectPose = false;// 当检测类型为3和6或3和4时需要额外加这个参数
                                    // 当检测类型为图片识别时的跟踪类型，1：图片跟踪；2：贴屏幕跟踪；3：场景跟踪
    public int algTracking2DType = 1;
    // 当检测类型为3D识别（立体物体）时的跟踪类型，7：3D跟踪（仅洞见APP）；8：SLAM跟踪（仅洞见APP）；
    public int algTracking3DType = 7;
    // 当检测类型为场景识别（无特定目标）时的跟踪类型，2：贴屏幕跟踪；3：场景跟踪；6：3DOF跟踪；8：SLAM跟踪（仅洞见APP）；9：人脸跟踪（仅洞见APP）
    public int algTrackingFreeType = 3;
    // 当检测类型为人脸识别时的跟踪类型，9：人脸跟踪（仅洞见APP）
    public int algTrackingFaceType = 9;

    // 检测类型为人脸识别时的相机方向
    public int bCameraAngle = 0;

    // 跟踪类型为场景跟踪时的性能设置，4：稳定跟踪，高功耗；6：初级跟踪，低功耗
    public int performTrackingSceneType = 4;

    // 当检测类型为图片识别且跟踪类型为图片跟踪时的丢失选项，0：丢失后不跟踪；1：丢失后低性能的自动跟踪；2：丢失后中性能的自动跟踪；3：丢失后贴屏幕跟踪
    public int lostTrackingtype = 0;

    // 识别目标为图片时的图片信息
    public MarkerImage[] markerImageArray = null;

    // 摄像头设置
    public bool bCameraVideoSettings = false;
    public VideoConfig videoConfig = new VideoConfig();

    // 高级设置
    public bool bAdvancedSettings = false;

    // 单位米，初始化的默认距离，默认1.0
    public float groundDistance = 1.0f;
    //  使用光照估计功能，默认1
    public bool bEnableLightEstimate = true;
    // 使用ARKit，仅iOS可用 0不使用，1使用，默认1
    public bool bUseARKIT = true;
    // 使用ARCore,仅Android可用 0不使用，1使用，默认1
    public bool bUseARCore = false;
    // 是否使用华为AR，默认0
    public bool isUseHWAR = false;
    // 算法日志的输出管理 0不输出，1输出，2写日志文件，默认1
    public int LogVerbose = 1;
    // 日志输出的过滤值 0，verbose 1，debug 2，info 3，warning 4，error 5，off，默认2
    public int LogPriority = 2;
    // 保存运行时数据，调试使用，1保存，0不保存，默认0
    public bool bSaveTestData = false;
    //保存数据下只保存IMU数据 1保存，0不保存，默认0
    public bool bOnlySaveIMU = false;
    // 使用本地数据运行 1加载本地，0不使用本地 默认路径是 根目录/InsightAR/TestData 也可以配置路径bLoadDataPath
    public bool bLoadTestData = false;
    // 读取本地数据模式时候配置的路径
    public string bLoadDataPath = "/InsightAR/TestData";
    // 模型坐标系和AR坐标系有差异的时候用于 更改AR坐标系的标记 0，1两种
    public int markerDirection = 0;
    // 2D detect+VR(IMU)模式下是否持续检测
    public bool bAlwaysDetect = false;
    // VR(IMU)模式下的陀螺仪旋转灵敏度
    public float imuRate = 0.5f;
    // 2D检测用的图像宽度，单位米  
    public float markerWidth = 1.0f;
    // 2D检测设定的尺度规模   
    public float initScale = 1.0f;
    // 2D算法配置，默认为1，需要降低跟踪要求的时候可以配置为0
    public bool isUseRobustMatch = true;
    // 2D算法配置，默认为10，需要降低跟踪要求和精度的时候可以配置为更低的数值
    public int detectWaitforTrackNum = 10;
    // 极易识别模式，默认为0，云识别为1
    public bool detectEasiest = false;
    // Tracking之前预留的帧数，默认10，之后才会进行算法的初始化
    public int defaultDetectCount = 10;
    // VIO进行hittest的时候是否进行重置，增加体验建议设置为1

    // 手势检测帧率
    public int detectFps = 5;
    // 是否启用手势跟踪
    public bool bTrackGesture = true;

    public bool bFirstHittestNeedReset = false;

    public override string ToString()
    {
        string ret = "";
        ret += this.toStringBasic();

        AlgorithmData defaultData = new AlgorithmData();

        // 高级设置
        ret += "\n";
        ret += groundDistance == defaultData.groundDistance ? "" : "groundDistance = " + groundDistance.ToString() + "\n";
        ret += bEnableLightEstimate == defaultData.bEnableLightEstimate ? "" : "bEnableLightEstimate = " + (bEnableLightEstimate ? "1" : "0") + "\n";
        ret += bUseARKIT == defaultData.bUseARKIT ? "" : "bUseARKIT = " + (bUseARKIT ? "1" : "0") + "\n";
        ret += bUseARCore == defaultData.bUseARCore ? "" : "bUseARCore = " + (bUseARCore ? "1" : "0") + "\n";
        ret += isUseHWAR == defaultData.isUseHWAR ? "" : "isUseHWAR = " + (isUseHWAR ? "1" : "0") + "\n";
        ret += bSaveTestData == defaultData.bSaveTestData ? "" : "bSaveTestData = " + (bSaveTestData ? "1" : "0") + "\n";
        ret += bOnlySaveIMU == defaultData.bOnlySaveIMU ? "" : "bOnlySaveIMU = " + (bOnlySaveIMU ? "1" : "0") + "\n";

        // 摄像机设置
        ret += "\n";
        ret += videoConfig == defaultData.videoConfig ? "" : "videoConfig = " + videoConfig.ToString() + "\n";
        return ret;
    }

    private string toStringBasic()
    {
        string ret = "";
        ret += "algDetectingType = " + algDetectingType.ToString() + "\n";
        switch (algDetectingType)
        {
            // 图片识别
            case 1:
                // 图片识别下的跟踪选项
                switch (algTracking2DType)
                {
                    // 图片跟踪
                    case 1:
                        // 这里由于算法历史原因导致1 1 3需要改成1 2 3所以要做一个额外判断
                        if (algTracking2DType == 1 && lostTrackingtype == 3)
                        {
                            ret += "algTrackingType = 2\n";
                            ret += "lostTrackingtype = 3\n";
                        }
                        else
                        {
                            ret += "algTrackingType = " + algTracking2DType.ToString() + "\n";
                            ret += "lostTrackingtype = " + lostTrackingtype.ToString() + "\n";
                        }
                        break;
                    // 贴屏幕跟踪
                    case 2:
                        ret += "algTrackingType = " + algTracking2DType.ToString() + "\n";
                        ret += "imuRate = " + imuRate.ToString() + "\n";
                        break;
                    // 场景跟踪
                    case 3:
                        ret += "algTrackingType = " + algTracking2DType.ToString() + "\n";
                        break;
                    default:
                        ret += "algTrackingType = " + algTracking2DType.ToString() + "\n";
                        break;
                }

                string name = "", nos = "";
                for (int i = 0; i < markerImageArray.Length; i++)
                {
                    name += markerImageArray[i].localName + ',';
                    nos += markerImageArray[i].remoteName + ',';
                }

                ret += "targetCount = " + markerImageArray.Length + "\n";
                ret += "targetName = " + name.Substring(0, name.Length - 1) + '\n';
                ret += "targetNOS = " + nos.Substring(0, nos.Length - 1) + '\n';
                ret += "makerWidth = " + markerWidth.ToString() + "\n";
                ret += "initScale = " + initScale.ToString() + "\n";
                ret += "markerDirection = " + markerDirection.ToString() + "\n";
                break;
            // 3D识别
            case 2:
                ret += "algTrackingType = " + algTracking3DType.ToString() + "\n";
                break;
            // 场景识别
            case 3:
                switch (algTrackingFreeType)
                {
                    // 贴屏幕跟踪
                    case 2:
                        ret += "algTrackingType = " + algTrackingFreeType.ToString() + "\n";
                        ret += "imuRate = " + imuRate.ToString() + "\n";
                        break;
                    // 场景跟踪：
                    case 3:
                        ret += "algTrackingType = " + performTrackingSceneType.ToString() + "\n";
                        ret += "bDetectGesture = " + (bDetectGesture ? "1" : "0") + "\n";
                        if (bDetectGesture) ret += "bTrackGesture = " + (bTrackGesture ? "1" : "0") + "\n";
                        ret += "bDetectPose = " + (bDetectPose ? "1" : "0") + "\n";
                        break;
                    case 6:
                        ret += "algTrackingType = " + algTrackingFreeType.ToString() + "\n";
                        ret += "bDetectGesture = " + (bDetectGesture ? "1" : "0") + "\n";
                        if (bDetectGesture) ret += "bTrackGesture = " + (bTrackGesture ? "1" : "0") + "\n";
                        ret += "bDetectPose = " + (bDetectPose ? "1" : "0") + "\n";
                        break;
                    default:
                        ret += "algTrackingType = " + algTrackingFreeType.ToString() + "\n";
                        break;
                }
                ret += "groundDistance = " + groundDistance.ToString() + "\n";
                ret += "defaultDetectCount = " + defaultDetectCount.ToString() + "\n";
                ret += "bFirstHittestNeedReset = " + (bFirstHittestNeedReset ? "1" : "0") + "\n";
                break;
            // 人脸识别
            case 4:
                ret += "algTrackingType = " + algTrackingFaceType.ToString() + "\n";
                ret += "bCameraAngle = " + bCameraAngle.ToString() + "\n";
                break;
            default:
                break;
        }
        return ret;
    }
}
