using EZXR.Glass.Hand;
using EZXR.Glass.ImageDetection;
using EZXR.Glass.SixDof;
using EZXR.Glass.SpatialMesh;
using EZXR.Glass.SpatialPositioning;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARAbilities : MonoBehaviour
{
    static ARProjectConfig config;
    /// <summary>
    /// 进行所有代码之前必须先执行的初始化操作，取到ARSDK的配置文件
    /// </summary>
    static void Init()
    {
        if (config == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Editor/ARProjectConfig.asset";
            if ((AssetDatabase.LoadAssetAtPath(filePath, typeof(ARProjectConfig)) as ARProjectConfig) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("892b737a1963162488e8f0164f77b58f");
            }
            config = AssetDatabase.LoadAssetAtPath(filePath, typeof(ARProjectConfig)) as ARProjectConfig;
        }
    }

    #region SpatialTracking
    [MenuItem("ARSDK/SpatialTracking/Enable", false, 70)]
    public static void EnableSpatialTracking()
    {
        Init();

        if (FindObjectOfType<HMDPoseTracker>() == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Core/SixDof/Prefabs/CameraRig.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(filePath) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("06bbeec2a98d8bd47a5d23c6d228eb60");
            }
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(filePath));
        }

        config.spatialTracking = true;
    }
    [MenuItem("ARSDK/SpatialTracking/Enable", true, 70)]
    static bool ValidateEnableSpatialTracking()
    {
        Init();
        return !config.spatialTracking;
    }

    [MenuItem("ARSDK/SpatialTracking/Disable", false, 70)]
    public static void DisableSpatialTracking()
    {
        Init();

        //HMDPoseTracker obj = FindObjectOfType<HMDPoseTracker>();
        //if (obj != null)
        //{
        //    DestroyImmediate(obj.gameObject);
        //}

        config.spatialTracking = false;
    }
    [MenuItem("ARSDK/SpatialTracking/Disable", true, 70)]
    static bool ValidateDisableSpatialTracking()
    {
        Init();
        return config.spatialTracking;
    }
    #endregion

    #region SpatialPositioning
    [MenuItem("ARSDK/SpatialPositioning/Enable", false, 70)]
    public static void EnableSpatialPositioning()
    {
        Init();

        if (FindObjectOfType<SpatialPositioningManager>() == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Core/SpatialPositioning/Prefabs/SpatialPositioningManager.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(filePath) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("06bbeec2a98d8bd47a5d23c6d228eb60");
            }
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(filePath));
        }

        config.spatialPositioning = true;
    }
    [MenuItem("ARSDK/SpatialPositioning/Enable", true, 70)]
    static bool ValidateEnableSpatialPositioning()
    {

        Init();
        return !config.spatialPositioning;
    }

    [MenuItem("ARSDK/SpatialPositioning/Disable", false, 70)]
    public static void DisableSpatialPositioning()
    {
        Init();

        SpatialPositioningManager obj = FindObjectOfType<SpatialPositioningManager>();
        if (obj != null)
        {
            DestroyImmediate(obj.gameObject);
        }

        config.spatialPositioning = false;
    }
    [MenuItem("ARSDK/SpatialPositioning/Disable", true, 70)]
    static bool ValidateDisableSpatialPositioning()
    {

        Init();
        return config.spatialPositioning;
    }
    #endregion

    #region HandTracking
    [MenuItem("ARSDK/HandTracking/Enable", false, 70)]
    public static void EnableHandTracking()
    {
        Init();

        if (FindObjectOfType<ARHandManager>() == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Core/HandTracking/Prefabs/HandRig.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(filePath) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("6f7860851f83dcf4cb1798a8fac3f550");
            }
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(filePath));
        }

        config.handTracking = true;
    }
    [MenuItem("ARSDK/HandTracking/Enable", true, 70)]
    static bool ValidateEnableHandTracking()
    {

        Init();
        return !config.handTracking;
    }

    [MenuItem("ARSDK/HandTracking/Disable", false, 70)]
    public static void DisableHandTracking()
    {
        Init();

        ARHandManager obj = FindObjectOfType<ARHandManager>();
        if (obj != null)
        {
            DestroyImmediate(obj.gameObject);
        }

        config.handTracking = false;
    }
    [MenuItem("ARSDK/HandTracking/Disable", true, 70)]
    static bool ValidateDisableHandTracking()
    {

        Init();
        return config.handTracking;
    }
    #endregion

    #region PlaneDetection
    [MenuItem("ARSDK/PlaneDetection/Enable", false, 70)]
    public static void EnablePlaneDetection()
    {
        Init();

        if (FindObjectOfType<PlaneDetectionManager>() == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Core/SixDof/Prefabs/PlaneDetectionManager.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(filePath) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("1a44cb9e3dd5f5046a7333d3057af165");
            }
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(filePath));
        }

        config.planeDetection = true;
    }
    [MenuItem("ARSDK/PlaneDetection/Enable", true, 70)]
    static bool ValidateEnablePlaneDetection()
    {
        Init();
        return !config.planeDetection;
    }

    [MenuItem("ARSDK/PlaneDetection/Disable", false, 70)]
    public static void DisablePlaneDetection()
    {
        Init();

        PlaneDetectionManager obj = FindObjectOfType<PlaneDetectionManager>();
        if (obj != null)
        {
            DestroyImmediate(obj.gameObject);
        }

        config.planeDetection = false;
    }
    [MenuItem("ARSDK/PlaneDetection/Disable", true, 70)]
    static bool ValidateDisablePlaneDetection()
    {
        Init();
        return config.planeDetection;
    }
    #endregion

    #region ImageDetection
    [MenuItem("ARSDK/ImageDetection/Enable", false, 70)]
    public static void EnableImageDetection()
    {
        Init();

        if (FindObjectOfType<ImageDetectionManager>() == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Core/ImageDetection/Prefabs/ImageDetectionManager.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(filePath) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("2b69b40edd61d3549983d584b38a6059");
            }
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(filePath));
        }

        config.imageDetection = true;
    }
    [MenuItem("ARSDK/ImageDetection/Enable", true, 70)]
    static bool ValidateEnableImageDetection()
    {
        Init();
        return !config.imageDetection;
    }

    [MenuItem("ARSDK/ImageDetection/Disable", false, 70)]
    public static void DisableImageDetection()
    {
        Init();

        ImageDetectionManager obj = FindObjectOfType<ImageDetectionManager>();
        if (obj != null)
        {
            DestroyImmediate(obj.gameObject);
        }

        config.imageDetection = false;
    }
    [MenuItem("ARSDK/ImageDetection/Disable", true, 70)]
    static bool ValidateDisableImageDetection()
    {
        Init();
        return config.imageDetection;
    }
    #endregion

    #region SpatialMesh
    [MenuItem("ARSDK/Additional Abilities/SpatialMesh/Enable", false, 70)]
    public static void EnableSpatialMesh()
    {
        Init();

        if (FindObjectOfType<SpatialMeshManager>() == null)
        {
            string filePath = "Assets/EZXRGlassSDK/Core/SpatialMesh/Prefabs/SpatialMeshManager.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(filePath) == null)
            {
                filePath = AssetDatabase.GUIDToAssetPath("9cb4ff0104470844eafdb0f39509e0b2");
            }
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(filePath));
        }

        config.spatialMesh = true;
    }

    [MenuItem("ARSDK/Additional Abilities/SpatialMesh/Enable", true, 70)]
    static bool ValidateEnableSpatialMesh()
    {
        return !config.spatialMesh;
    }

    [MenuItem("ARSDK/Additional Abilities/SpatialMesh/Disable", false, 70)]
    static void DisableSpatialMesh()
    {
        Init();

        SpatialMeshManager obj = FindObjectOfType<SpatialMeshManager>();
        if (obj != null)
        {
            DestroyImmediate(obj.gameObject);
        }

        config.spatialMesh = false;
    }
    [MenuItem("ARSDK/Additional Abilities/SpatialMesh/Disable", true, 70)]
    static bool ValidateDisableSpatialMesh()
    {
        return config.spatialMesh;
    }
    #endregion
}
