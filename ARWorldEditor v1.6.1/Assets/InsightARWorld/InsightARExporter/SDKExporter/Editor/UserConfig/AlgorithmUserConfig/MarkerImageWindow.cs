using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UserConfig
{
    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    public class MarkerImageWindow : BaseConfigWindow {
        static MarkerImageWindow markerImageWindow = null;
        static List<Texture2D> algMarkerTextureDisplayList = null;
        static int currentCount;

        public delegate void IsMarkerChangedCallBack(bool isMarkerChanged);
        static IsMarkerChangedCallBack isMarkerChangedCallBack;
        
        public static MarkerImageWindow OnShow(List<Texture2D> texList, IsMarkerChangedCallBack callBack)
        {
            algMarkerTextureDisplayList = texList;
            currentCount = algMarkerTextureDisplayList.Count;
            isMarkerChangedCallBack = callBack;
            // Get existing open window or if none, make a new one:
            markerImageWindow = (MarkerImageWindow) EditorWindow.GetWindow(typeof(MarkerImageWindow), true, "描述文件管理");
            markerImageWindow.Show();
            return markerImageWindow;
        }

        static Vector2 scrollPos;
        void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            currentCount = EditorGUILayout.IntField("需要跟踪的图片文件数量：", currentCount > 0 ? currentCount : 1 );
            if (currentCount > algMarkerTextureDisplayList.Count)
            {
                int count = currentCount - algMarkerTextureDisplayList.Count;
                while (0 < count --)
                    algMarkerTextureDisplayList.Add(null);
            }
            else if (currentCount < algMarkerTextureDisplayList.Count)
            {
                int count = algMarkerTextureDisplayList.Count - currentCount;
                while (0 < count --)
                    algMarkerTextureDisplayList.RemoveAt(algMarkerTextureDisplayList.Count - 1);
            }
            for (int i = 0; i < currentCount; i ++)
                algMarkerTextureDisplayList[i] = (Texture2D) EditorGUILayout.ObjectField("需要跟踪的图片文件：", algMarkerTextureDisplayList[i], typeof(Texture2D), true);
            EditorGUILayout.EndScrollView();
            
            #region 功能按钮
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("保存"))
            {
                isMarkerChangedCallBack(true);
                markerImageWindow.Close();
            }
            if (GUILayout.Button("取消")) markerImageWindow.Close();
            EditorGUILayout.EndHorizontal();
            #endregion
        }
    }
}