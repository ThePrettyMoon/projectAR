using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[InitializeOnLoad]
public class ARSDKAbout : EditorWindow
{
    public ARSDKConfig config;
    Texture2D logo;

    [MenuItem("ARSDK/About", false, 99)]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(ARSDKAbout), false, "About");
        window.minSize = new Vector2(300, 350);
    }

    void OnGUI()
    {
        if (logo == null)
        {
            logo = Resources.Load<Texture2D>("ezxr_logo");
        }

        GUIStyle label_Red = new GUIStyle(EditorStyles.label);
        label_Red.normal.textColor = Color.red;
        GUIStyle label_Green = new GUIStyle(EditorStyles.label);
        label_Green.normal.textColor = Color.green;


        GUILayout.Button(logo, GUILayout.Height(150));

        GUILayout.Label("Version：" + config.version);

        GUILayout.FlexibleSpace();

        GUILayout.Label("License Type：");

        GUILayout.BeginHorizontal();
        {
            GUILayout.Space(20);

            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("SpatialTracking：");
                if (config.spatialTracking)
                {
                    GUILayout.Label("Authorized", label_Green);
                }
                else
                {
                    GUILayout.Label("Unauthorized", label_Red);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("SpatialPositioning：");
                if (config.spatialPositioning)
                {
                    GUILayout.Label("Authorized", label_Green);
                }
                else
                {
                    GUILayout.Label("Unauthorized", label_Red);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("HandTracking：");
                if (config.handTracking)
                {
                    GUILayout.Label("Authorized", label_Green);
                }
                else
                {
                    GUILayout.Label("Unauthorized", label_Red);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("ImageDetection：");
                if (config.imageDetection)
                {
                    GUILayout.Label("Authorized", label_Green);
                }
                else
                {
                    GUILayout.Label("Unauthorized", label_Red);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("ObjectDetection：");
                if (config.objectDetection)
                {
                    GUILayout.Label("Authorized", label_Green);
                }
                else
                {
                    GUILayout.Label("Unauthorized", label_Red);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}