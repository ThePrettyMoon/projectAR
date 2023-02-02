using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System;
#if UNITY_EDITOR

public enum InitializedType
{
    Already = 0,
    NoInitialized_Normal,
    NoInitialized_SelectEnd
}
[CustomEditor(typeof(CameraPathMap))]
internal class CameraPathMapEditor: Editor
{
    [NonSerialized] InitializedType m_Initialized = InitializedType.NoInitialized_Normal;
    private bool showCameraPathPointList = true;
    [SerializeField] TreeViewState m_TreeViewState;
    CameraPathMapView m_TreeView;
    CameraPath m_cameraPath;
    CameraPathMap m_pathMap;
    
    private GUIContent cameraPathTitle = new GUIContent
    {
        text = "相机路径点",
        tooltip = "在这里可以添加相机运动的路径位置"
    };
    public override void OnInspectorGUI()
    {
        InitIfNeeded();
        serializedObject.Update();
        ToolsStateChangeUpdate();
        if (UnityEditorInternal.InternalEditorUtility.GetIsInspectorExpanded(m_pathMap.m_cameraPath) == false)
        {
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(m_pathMap.m_cameraPath, true);
        }
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("路径点模式", GUILayout.Width(80));

        m_pathMap.isCurveLineMode = EditorGUILayout.ToggleLeft("直线模式", m_pathMap.isCurveLineMode);
        var isCurveMode = EditorGUILayout.ToggleLeft("曲线模式", !m_pathMap.isCurveLineMode);
        if (isCurveMode == m_pathMap.isCurveLineMode)
        {
            m_pathMap.isCurveLineMode = !m_pathMap.isCurveLineMode;
        }
        m_pathMap.SetCameraPathInterpolation();
        EditorGUILayout.EndHorizontal();

        showCameraPathPointList = EditorGUILayout.Foldout(showCameraPathPointList, cameraPathTitle);
        Rect rect = new Rect(treeViewRect.x, treeViewRect.y, treeViewRect.width,EditorGUIUtility.singleLineHeight);
        //获取当前缓存的路径点
        if (showCameraPathPointList)
        {
            rect.y += treeViewRect.height;
            DoTreeView(treeViewRect);
        }

        if (GUILayout.Button("新增路径点位"))
        {
            AddNewPathPoint();
        }
        GUILayout.TextArea("", GUI.skin.horizontalSlider);
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        EditorGUILayout.LabelField("相机路径动画：", GUILayout.MaxWidth(80));
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing* 4);
        //初始位置
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("初始位置", GUILayout.Width(80));
        EditorGUILayout.LabelField("0", GUILayout.Width(15));
        m_pathMap.m_cameraPathAnimator.startPercent = EditorGUILayout.Slider(m_pathMap.m_cameraPathAnimator.startPercent * 100, 0, 100) *0.01f;
        if (m_pathMap.firstPositionPercentage != m_pathMap.m_cameraPathAnimator.startPercent * 100)
        {
            m_pathMap.firstPositionPercentage = m_pathMap.m_cameraPathAnimator.startPercent * 100;
        }
        EditorGUILayout.LabelField("%",GUILayout.Width(15));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing*4);

        //路径方向
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("路径方向", GUILayout.Width(80));
        m_pathMap.pathDirection = (PathDirection)EditorGUILayout.Popup((int)m_pathMap.pathDirection,CameraPathMap.PathDirectionCN/*,GUILayout.MaxWidth(300)*/);

        EditorGUILayout.EndHorizontal();
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
        //播放模式
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("播放模式", GUILayout.Width(80));
        m_pathMap.playMode = (PlayMode)EditorGUILayout.Popup((int)m_pathMap.playMode, CameraPathMap.PlayModeCN/*,GUILayout.MaxWidth(300)*/);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
        m_pathMap.SetCameraAnimatorPlayMode();
        //播放速度
        EditorGUILayout.BeginHorizontal();
        m_pathMap.m_cameraPathAnimator.pathSpeed = EditorGUILayout.FloatField("播放速度", m_pathMap.m_cameraPathAnimator.pathSpeed);
        if (m_pathMap.m_cameraPathAnimator.pathSpeed != m_pathMap.playSpeed)
        {
            m_pathMap.playSpeed = m_pathMap.m_cameraPathAnimator.pathSpeed;
        }
        EditorGUILayout.LabelField("米/秒", GUILayout.Width(40));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
        //播放时间
        EditorGUILayout.BeginHorizontal();
        float animationTime = m_cameraPath.pathLength / m_pathMap.m_cameraPathAnimator.pathSpeed;
        float newTime = EditorGUILayout.FloatField("播放时间", animationTime);
        if (animationTime != newTime)
        {
            m_pathMap.m_cameraPathAnimator.pathSpeed = m_cameraPath.pathLength / newTime;
            m_pathMap.playTime = newTime;
        }
        EditorGUILayout.LabelField("秒", GUILayout.Width(25));
        EditorGUILayout.EndHorizontal();
        ///提示
        if (m_cameraPath.storedPointResolution > m_pathMap.playSpeed / 10)
        {
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
            EditorGUILayout.HelpBox("当前播放速度可能太低，请提高速度", MessageType.Error);
        }
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
        //预览机型
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("预览机型", GUILayout.Width(80));
        DevicesType devicesType = (DevicesType)EditorGUILayout.Popup((int)m_pathMap.device, m_pathMap.devicesArrayCN/*,GUILayout.MaxWidth(300)*/);
        if (m_pathMap.device != devicesType)
        {
            m_pathMap.device = devicesType;
            m_pathMap.SetGameViewSize();
        }
     
        EditorGUILayout.HelpBox("预览机型：调整预览时屏幕尺寸,模拟实机演示", MessageType.Info);
     
        //Screen.SetResolution((int)solution.x,(int) solution.y,false);
        EditorGUILayout.EndHorizontal();
      
        GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4);
        //运行
        if (GUILayout.Button("预览该路线"))
        {
            RunThisPath();
        }
        SceneView.RepaintAll();
    }
    void InitIfNeeded()
    {
        //Debug.Log("InitIfNeeded:"+ m_Initialized);
        if (m_Initialized!= InitializedType.Already)
        {
            m_pathMap = (CameraPathMap)target;
            m_cameraPath = m_pathMap.m_cameraPath;
            m_cameraPath.pathHighLightEvent -= SetTreeViewPointSelection;
            m_cameraPath.pathHighLightEvent += SetTreeViewPointSelection;
            if (m_TreeViewState == null)
                m_TreeViewState = new TreeViewState();

           
            var treeModel = new TreeModel<CameraPathElement>(GetData());
            m_TreeView = new CameraPathMapView(m_TreeViewState, treeModel,this);
            if (m_Initialized == InitializedType.NoInitialized_SelectEnd)
            {
                SetSceneViewPointSelection(treeModel.numberOfDataElements);
                var index = treeModel.numberOfDataElements - 2;
                var element = treeModel.Find(index);
                if (element != null)
                {
                    element.enabled = true;
                    m_TreeView.SetExpanded(index, true);
                }
            }
            m_Initialized =InitializedType.Already;
        }
    }
    /// <summary>
    /// 从cameraPath中读取路径点
    /// </summary>
    /// <returns></returns>
    IList<CameraPathElement> GetData()
    {
        int numTotalElements = 3;
        var treeElements = new List<CameraPathElement>(numTotalElements);
        int IDCounter = -1;
        var root = new CameraPathElement("Root", -1, IDCounter);
        treeElements.Add(root);
        

        for (int i = 0; i < m_cameraPath.realNumberOfPoints; i++)
        {
            var posPointSc = m_cameraPath.GetPoint(i);
            var rotatePointSc = m_cameraPath.orientationList[i];
            IDCounter++;
            var child1 = new CameraPathElement(posPointSc.displayName, 0, IDCounter,posPointSc, rotatePointSc);
            treeElements.Add(child1);
        }
        return treeElements;
    }
    
    Rect treeViewRect
    {
        get { return new Rect(20, 30, 700, m_TreeView.totalHeight); }
    }
    void DoTreeView(Rect rect)
    {
        m_TreeView.OnGUI(rect);
    }
    private void OnSceneGUI()
    {
        if (m_Initialized != InitializedType.Already)
            return;
    }
    #region Button
    public void SetCurrentPointCameraDirToRoadDir(TreeViewItem<CameraPathElement> element)
    {
        var point = element.data.rotatePoint;
        point.rotation.SetLookRotation(m_cameraPath.GetPathDirection(point.percent, false));
    }
    public void SetAllPointCameraDirToRoadDir(TreeViewItem<CameraPathElement> element)
    {
        var point = element.data.rotatePoint;
        CameraPathOrientationList orientationList = m_cameraPath.orientationList;
        if (orientationList.realNumberOfPoints > 0)
        {
            //For each point, do the logic of look rotation (the same than above)
            for (int i = 0; i < orientationList.realNumberOfPoints; i++)
            {
                CameraPathOrientation currentPoint = orientationList[i];
                currentPoint.rotation.SetLookRotation(m_cameraPath.GetPathDirection(currentPoint.percent, false));
            }
        }
    }
    public void DeletCurrentPathPoint(TreeViewItem<CameraPathElement> element)
    {
        var rotatePoint = element.data.rotatePoint;
        var pathPoint = element.data.pathPoint;
        if (m_cameraPath.realNumberOfPoints <= 2 )
        {
            if (EditorUtility.DisplayDialog("提示", "路线点小于2时，该路径将会被删除", "确定"))
            {
                GameObject.DestroyImmediate(((CameraPathMap)target).gameObject);
                return;
            }
        }
        m_cameraPath.RemovePoint(pathPoint);
        m_Initialized = InitializedType.NoInitialized_Normal;
        m_TreeView.treeModel.RemoveElements(new List<int>() { element.id });

    }
    public void AddNewPathPoint()
    {
        CameraPathControlPoint newPoint = m_cameraPath.gameObject.AddComponent<CameraPathControlPoint>();//ScriptableObject.CreateInstance<CameraPathControlPoint>();
        Vector3 finalPathPosition = m_cameraPath.GetPathPosition(1.0f);
        Vector3 finalPathDirection = m_cameraPath.GetPathDirection(1.0f);
        float finalArcLength = m_cameraPath.StoredArcLength(m_cameraPath.numberOfCurves - 1);
        if (finalArcLength < Mathf.Epsilon) finalArcLength = 1;
        Vector3 newPathPointPosition = finalPathPosition + finalPathDirection * (finalArcLength);
        newPoint.worldPosition = newPathPointPosition;
        newPoint.forwardControlPointLocal = m_cameraPath[m_cameraPath.realNumberOfPoints - 1].forwardControlPointLocal;
        m_cameraPath.AddPoint(newPoint);
        GUI.changed = true;
        m_Initialized = InitializedType.NoInitialized_SelectEnd;
    }
    public void RunThisPath()
    {
        var curMapObject = ((CameraPathMap)target).transform.gameObject;
        List<GameObject> AllPathObjcet = new List<GameObject>();
        for (int i = 0; i < curMapObject.transform.parent.childCount; i++)
        {
            var item = curMapObject.transform.parent.GetChild(i);
            if (item.GetComponent<CameraPathAnimator>() != null)
            {
                AllPathObjcet.Add(item.gameObject);
            }
        }
        foreach (var item in AllPathObjcet)
        {
            var sc = item.GetComponent<CameraPathAnimator>();
            if (sc!=null)
            {
                sc.playOnStart = false;
            }
        }
        var curSc = curMapObject.GetComponent<CameraPathAnimator>();
        curSc.playOnStart = true;
        EditorApplication.isPlaying = true;
    }
    #endregion
    public void SetSceneViewPointSelection(int index)
    {
        m_cameraPath.selectedPoint = (index);
        CameraPathEditorSceneGUI.curIndex = index;
    }
    public void SetTreeViewPointSelection(int index)
    {
        List<int> list = new List<int>() { index };
        m_TreeView.SetSelection(list);
        m_TreeView.SetViewItemVisible(list);
    }
    public void ToolsStateChangeUpdate()
    {
        if (Tools.current == Tool.Move)
        {
            m_cameraPath.pointMode = CameraPath.PointModes.Transform;
        }
        else if(Tools.current == Tool.Rotate)
        {
            m_cameraPath.pointMode = CameraPath.PointModes.Orientations;
        }
    }
}
#endif