using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class CameraPathMap : MonoBehaviour
{
    
    public CameraPath m_cameraPath;
    public CameraPathAnimator m_cameraPathAnimator;
    public float firstPositionPercentage ;
    public PathDirection pathDirection;
    public float playSpeed;
    public float playTime;
    public PlayMode playMode ;
    public DevicesType device;
    public bool continuePlayNext;
    public string[] devicesArrayCN;
    public bool isCurveLineMode;

    public void Init(CameraPath cameraPath, CameraPathAnimator cameraPathAnimator)
    {
        m_cameraPath = cameraPath;
        m_cameraPathAnimator = cameraPathAnimator;
        m_cameraPath.interpolation = CameraPath.Interpolation.Hermite;
        m_cameraPath.hermiteTension = 0;
        m_cameraPath.hermiteBias = 0;
        SetDevicesArray();
    }
    public void SetCameraAnimatorPlayMode()
    {
        if (pathDirection == PathDirection.StartToEnd)
        {
            if (playMode == PlayMode.Once)
            {
                m_cameraPathAnimator.animationMode = CameraPathAnimator.animationModes.once;
            }
            else if (playMode == PlayMode.Loop)
            {
                m_cameraPathAnimator.animationMode = CameraPathAnimator.animationModes.loop;
            }
        }
        else if(pathDirection == PathDirection.EndToStart)
        {
            if (playMode == PlayMode.Once)
            {
                m_cameraPathAnimator.animationMode = CameraPathAnimator.animationModes.reverse;
            }
            else if (playMode == PlayMode.Loop)
            {
                m_cameraPathAnimator.animationMode = CameraPathAnimator.animationModes.reverseLoop;
            }
        }
    }

    public static string[] PathDirectionCN = new string[]
    {
        "起点 -> 终点","终点 -> 起点"
    };
    public static string[] PlayModeCN = new string[]
    {
        "单次","循环"
    };
    public static Vector2Int[] ScreenSolutions = new Vector2Int[]
    {
        new Vector2Int(1080,1920),
        new Vector2Int(1284,2778),//0
        new Vector2Int(1170,2532),//1
        new Vector2Int(1080 ,2340),//2
        new Vector2Int(828 ,1792),//3
        new Vector2Int(1242,2688),//4
        new Vector2Int(1125,2436),//5
        new Vector2Int(1242,2208),//6
        new Vector2Int(750,1134),//7
        new Vector2Int(1200,2640),//8
        new Vector2Int(1176,2400),//9
        new Vector2Int(1080,2340),//10
        new Vector2Int(1080,2244),//11
        new Vector2Int(1080,2340),//12
        new Vector2Int(1080,1920),//13
        new Vector2Int(1080,2340),//14
        new Vector2Int(1080,1920),//15
        new Vector2Int(1080,2400),//16
        new Vector2Int(1440,3040),//17
        new Vector2Int(1440,2960),//18
        new Vector2Int(1440,3120),//19
        new Vector2Int(1080,2340),//20
        new Vector2Int(1080,1920),//21
        new Vector2Int(1080,2400),//22

    };
    private static Dictionary<DevicesType,string> DevicesDic = new Dictionary<DevicesType, string>()
    {
        [DevicesType.Default] = "默认",
        [DevicesType.IPhone_12_ProMax] = "_iPhone 12 Pro Max",
        [DevicesType.IPhone_12_Pro] = "_iPhone 12 Pro",
        [DevicesType.IPhone_12_Mini] = "_iPhone 12 Mini",
        [DevicesType.IPhone_11] = "_iPhone 11 [11，XR]",
        [DevicesType.IPhone_XS_Max] = "_iPhone XS Max[XS，11 Pro]",
        [DevicesType.IPhone_X] = "_iPhone X [X,XS,11 Pro]",
        [DevicesType.IPhone_8_Plus] = "_iPhone 8+ [8+, 7+, 6S+, 6+]",
        [DevicesType.IPhone_8] = "_iPhone 8 [8, 7, 6S, 6]",
        [DevicesType.HUAWEI_P40_ProPlus] = "_Huawei P40 Pro+",
        [DevicesType.HUAWEI_Mate30_Pro] = "_HUAWEI Mate30 Pro",
        [DevicesType.HUAWEI_Mate30] = "_HUAWEI Mate30 [Mate 30,P30]",
        [DevicesType.HUAWEI_Mate20] = "_HUAWEI Mate20",
        [DevicesType.MI_9] = "_小米9 [9,9Pro 5G,MIX3]",
        [DevicesType.MI_Note3] = "_小米Note 3",
        [DevicesType.Vivo_IQOO] = "_Vivo iQOO",
        [DevicesType.Vivo_X9_Plus] = "_Vivo X9 Plus [X9 Plus, X9s Plus]",
        [DevicesType.SamsungGalaxy_A70] = "_Samsung Galaxy A70[A70，A80]",
        [DevicesType.SamsungGalaxy_S10_Plus] = "_Samsung Galaxy S10+",
        [DevicesType.SamsungGalaxy_S9] = "_Samsung Galaxy S9",
        [DevicesType.OnePlus_7_Pro] = "_OnePlus 7 Pro",
        [DevicesType.OnePlus_7] = "_OnePlus 7[7,Find X]",
        [DevicesType.Oppo_R9s_Plus] = "_Oppo R9s Plus [R9s Plus, R11 Plus]",
        [DevicesType.Oppo_Reno2] = "_Oppo Reno2",
    };
    
    private void SetDevicesArray()
    {
        devicesArrayCN = new string[DevicesDic.Count];
        for (int i = 0; i < DevicesDic.Count; i++)
        {
            devicesArrayCN[i] = DevicesDic[(DevicesType)i];
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
        var size = ScreenSolutions[(int)device];
        string des = DevicesDic[device];
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
    public void SetCameraPathInterpolation()
    {
        if (isCurveLineMode)
        {
            m_cameraPath.interpolation = CameraPath.Interpolation.Linear;
        }
        else
        {
            m_cameraPath.interpolation = CameraPath.Interpolation.Hermite;
        }
    }
    
}

public class CameraPoint
{
    public int Index;
    public Vector3 position;
    public Vector3 eulerAngle;
    public bool isUnderControl;
    public Vector3 controlPoint1;
    public Vector3 controlPoint2;
}
public enum PathDirection
{
    StartToEnd,
    EndToStart,
}
public enum PlayMode
{
    Once,
    Loop,
}
public enum DevicesType
{
    Default,
    IPhone_12_ProMax,
    IPhone_12_Pro,
    IPhone_12_Mini,
    IPhone_11,
    IPhone_XS_Max,
    IPhone_X,
    IPhone_8_Plus,
    IPhone_8,
    HUAWEI_P40_ProPlus,
    HUAWEI_Mate30_Pro,
    HUAWEI_Mate30,
    HUAWEI_Mate20,
    MI_9,
    MI_Note3,
    Vivo_IQOO,
    Vivo_X9_Plus,
    SamsungGalaxy_A70,
    SamsungGalaxy_S10_Plus,
    SamsungGalaxy_S9,
    OnePlus_7_Pro,
    OnePlus_7,
    Oppo_R9s_Plus,
    Oppo_Reno2
}
public enum PathLineMode
{
    Line,
    Curve,
}
#endif