using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

internal class CameraPathMapView : CameraPathTreeViewWithTreeModel<CameraPathElement>
{
	float unfoldHeight = 280f;
	float foldHeight = 30;
	CameraPathMapEditor m_editor;
	static class Styles
	{
		public static GUIStyle background = "RL Background";
		public static GUIStyle headerBackground = "RL Header";
	}
	//static Texture2D elementIcons = EditorGUIUtility.FindTexture("ViewToolZoom On");

	public CameraPathMapView(TreeViewState state, TreeModel<CameraPathElement> model,CameraPathMapEditor editor)
		: base(state, model)
	{
		// Custom setup
		//显示外边框
		this.showBorder = true;
		//展开的箭头向下位移
		//customFoldoutYOffset = 100f;
		m_editor = editor;
		Reload();

	}
	/// <summary>
	/// 确定每一个元素关闭和打开时的Rect的高度
	/// </summary>
	/// <param name="row"></param>
	/// <param name="item"></param>
	/// <returns></returns>
	protected override float GetCustomRowHeight(int row, TreeViewItem item)
	{
		var myItem = (TreeViewItem<CameraPathElement>)item;
		if (myItem.data.enabled)
        {
            if (myItem.data.isUnderControl)
            {
				return unfoldHeight + EditorGUIUtility.singleLineHeight*4;
			}
			return unfoldHeight;
		}
		return foldHeight;
	}

	/// <summary>
	/// Inspector GUI回调
	/// </summary>
	/// <param name="rect"></param>
	public override void OnGUI(Rect rect)
	{
		// Background
		if (Event.current.type == EventType.Repaint)
			DefaultStyles.backgroundOdd.Draw(rect, false, false, false, false);
        // TreeView
        //在Inspector界面开辟一块Rect区域
        //注意宽度会动态随Inspector变化而变化，只需要传入高即可
		var contentRect = EditorGUILayout.GetControlRect(GUILayout.MinHeight(foldHeight*2), GUILayout.MaxHeight(totalHeight));
		base.OnGUI(contentRect);
	}

	#region 实际控制渲染属性
	/// <summary>
	/// 每一个元素渲染的GUI回调
	/// args包含了渲染元素的状态
	/// </summary>
	/// <param name="args"></param>
	protected override void RowGUI(RowGUIArgs args)
	{
		var item = (TreeViewItem<CameraPathElement>)args.item;
		var contentIndent = GetContentIndent(item);
		// Background
		var bgRect = args.rowRect;
		bgRect.x = contentIndent;
		bgRect.width = Mathf.Max(bgRect.width - contentIndent, 155f) - 5f;
		bgRect.yMin += 2f;
		bgRect.yMax -= 2f;
		DrawItemBackground(bgRect);

		// Custom label
		var headerRect = bgRect;
		headerRect.xMin += 5f;
		headerRect.xMax -= 10f;
		headerRect.height = Styles.headerBackground.fixedHeight;
		HeaderGUI(headerRect, args.label, item);

		// Controls
		var controlsRect = headerRect;
		controlsRect.xMin += 20f;
		controlsRect.y += headerRect.height;
		if (item.data.enabled)
			ControlsGUI(controlsRect, item);
	}
	/// <summary>
	/// 绘制背景
	/// </summary>
	/// <param name="bgRect"></param>
	void DrawItemBackground(Rect bgRect)
	{
		if (Event.current.type == EventType.Repaint)
		{
			var rect = bgRect;
			rect.height = Styles.headerBackground.fixedHeight;
			Styles.headerBackground.Draw(rect, false, false, false, false);

			rect.y += rect.height;
			rect.height = bgRect.height - rect.height;
			Styles.background.Draw(rect, false, false, false, false);
		}
	}
	/// <summary>
	/// 控制标题显示的GUI
	/// </summary>
	/// <param name="headerRect"></param>
	/// <param name="label"></param>
	/// <param name="item"></param>
	void HeaderGUI(Rect headerRect, string label, TreeViewItem<CameraPathElement> item)
	{
		headerRect.y += 1f;
		//if (GUILayout.Button("asd", GUI.skin.customStyles[189],))
		//{
		//	Debug.Log(123);
		//}
		// Do toggle
		Rect toggleRect = headerRect;
		toggleRect.width = 16;
		//EditorGUI.BeginChangeCheck();
		//item.data.enabled = EditorGUI.Toggle(toggleRect, item.data.enabled); // hide when outside cell rect
		//if (EditorGUI.EndChangeCheck())
			RefreshCustomRowHeights();

		Rect labelRect = headerRect;
		labelRect.xMin += toggleRect.width + 2f;
		//Rect iconRect = new Rect(headerRect.x - 21, headerRect.y, 16, 16);
		//GUI.DrawTexture(iconRect, item.icon);
		GUI.Label(labelRect, label);
		
	}
	Vector3 lastPos = Vector3.zero;
	Vector3 lastRotate = Vector3.zero;
	/// <summary>
	/// 控制属性显示GUI
	/// </summary>
	/// <param name="controlsRect"></param>
	/// <param name="item"></param>
	void ControlsGUI(Rect controlsRect, TreeViewItem<CameraPathElement> item)
	{
		var rect = controlsRect;
		rect.y += 3f;
		rect.height = EditorGUIUtility.singleLineHeight;

		//名字显示
		string oldName = item.displayName;
        if (oldName != item.data.pathPoint.displayName)
        {
            oldName = item.data.pathPoint.displayName;
            item.displayName = item.data.pathPoint.displayName;
        }
        item.displayName = EditorGUI.DelayedTextField(rect, "名字", item.displayName);

		if (item.displayName.Length == 0)
        {
			item.data.pathPoint.customName = oldName;
			item.displayName = oldName;
        }
        else
		{
			item.data.pathPoint.customName = item.displayName;
		}

		rect.y += rect.height + EditorGUIUtility.singleLineHeight;

		//position显示和设置
        if (lastPos != item.data.pathPoint.localPosition)
        {
			lastPos = item.data.pathPoint.localPosition;
			item.data.position = lastPos;
        }
		item.data.position = EditorGUI.Vector3Field(rect,new GUIContent("坐标"), item.data.position);
		item.data.pathPoint.localPosition = item.data.position;

		rect.y += rect.height + EditorGUIUtility.singleLineHeight;

		//rotation显示和设置
		if (lastRotate != item.data.rotatePoint.rotation.eulerAngles)
		{
			lastRotate = item.data.rotatePoint.rotation.eulerAngles;
			item.data.eulerAngle = lastRotate;
		}
		item.data.eulerAngle = EditorGUI.Vector3Field(rect,"相机视角", item.data.eulerAngle);
		item.data.rotatePoint.rotation = Quaternion.Euler(item.data.eulerAngle);

		rect.y += rect.height + EditorGUIUtility.singleLineHeight*2;

		//Button
        if (GUI.Button(new Rect(rect.x + rect.width * 0.5f, rect.y, rect.width*0.5f, rect.height), new GUIContent("当前相机视角沿路径方向","tips")))
        {
			SetCurrentPointCameraDirToRoadDir(item);
		}
		rect.y += rect.height + EditorGUIUtility.singleLineHeight;
		if (GUI.Button(new Rect(rect.x + rect.width * 0.5f, rect.y, rect.width * 0.5f, rect.height), new GUIContent("所有相机视角沿路径方向", "tips")))
		{
			SetAllPointCameraDirToRoadDir(item);
		}
		rect.y += rect.height + EditorGUIUtility.singleLineHeight;
		//bool old = item.data.isUnderControl;
		//item.data.isUnderControl = EditorGUI.Toggle(rect, "弧度点控制",item.data.isUnderControl);
		//rect.y += rect.height + EditorGUIUtility.singleLineHeight;
		//if (old != item.data.isUnderControl) 
		//	Reload();
		//if (item.data.isUnderControl)
  //      {
		//	item.data.controlPoint1 = EditorGUI.Vector3Field(rect, new GUIContent("弧度点1"), item.data.controlPoint1);
		//	rect.y += rect.height + EditorGUIUtility.singleLineHeight;
		//	item.data.controlPoint2 = EditorGUI.Vector3Field(rect, new GUIContent("弧度点2"), item.data.controlPoint2);
		//	rect.y += rect.height + EditorGUIUtility.singleLineHeight;
		//}
		if (GUI.Button(rect, new GUIContent("删除当前路径点", "tips")))
		{
			DeletCurrentPathPoint(item);
		}
		SceneView.RepaintAll();
	}

    protected override void SelectionChanged(IList<int> selectedIds)
    {
        base.SelectionChanged(selectedIds);
		m_editor.SetSceneViewPointSelection(selectedIds[0]);
		//在Inspector中选择时，不需要隐藏之前已扩展的
		SetViewItemVisible(selectedIds, false);
	
		SceneView.RepaintAll();
	}
    #endregion

    #region Button Method

    private void SetCurrentPointCameraDirToRoadDir(TreeViewItem<CameraPathElement> element)
    {
		m_editor.SetCurrentPointCameraDirToRoadDir(element);
	}
	private void SetAllPointCameraDirToRoadDir(TreeViewItem<CameraPathElement> element)
    {
		m_editor.SetAllPointCameraDirToRoadDir(element);

	}
	private void DeletCurrentPathPoint(TreeViewItem<CameraPathElement> element)
	{
		m_editor.DeletCurrentPathPoint(element);
	}
    #endregion

    #region Rename 修改单个point的名称
    protected override Rect GetRenameRect(Rect rowRect, int row, TreeViewItem item)
	{
		// Match label perfectly
		var renameRect = base.GetRenameRect(rowRect, row, item);
		//renameRect.xMin += 25f;
		//renameRect.y += 2f;
		renameRect.height = 30f;
        return renameRect;
	}
	// Rename
	//--------
	protected override bool CanRename(TreeViewItem item)
	{
		return false;
		// Only allow rename if we can show the rename overlay with a certain width (label might be clipped by other columns)
		Rect renameRect = GetRenameRect(treeViewRect, 0, item);
		return renameRect.width > 30;
	}
	protected override void RenameEnded(RenameEndedArgs args)
	{
		// Set the backend name and reload the tree to reflect the new model
		if (args.acceptedRename)
		{
			var element = treeModel.Find(args.itemID);
			element.name = args.newName;
			Reload();
		}
	}
    #endregion

    #region ViewItem的显示
	public void SetViewItemVisible( IList<int> selectList,bool fadeOther = true)
    {
		IList<TreeViewItem> treeViewItems = FindRows(selectList);

		if (fadeOther)
        {
			var allItems = GetRows();
            foreach (var item in allItems)
            {
				var cameraPathViewItem = (TreeViewItem<CameraPathElement>)item;
				cameraPathViewItem.data.enabled = false;
			}
        }

        foreach (var item in treeViewItems)
        {
			var cameraPathViewItem = (TreeViewItem<CameraPathElement>)item;
			cameraPathViewItem.data.enabled = true;
		}
		Reload();
	}
    #endregion

}
