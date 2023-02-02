using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class SampleWindow : EditorWindow
{
    bool groupEnabled = true;
    bool myBool = true;
    float myFloat = 1.23f;

    #region FadeGroup
    AnimBool m_ShowExtraFields;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;
    #endregion

    string curChoose = "Click To Choose";

    // Add menu to the Window menu
    //[MenuItem("EditorUtils/SampleWindow &s")]
    static void ShowWindow()
    {
        // Get existing open window or if none, make a new one:
        SampleWindow window = (SampleWindow)EditorWindow.GetWindow(typeof(SampleWindow));
        window.Show();
    }

    void OnEnable()
    {
        #region FadeGroup
        m_ShowExtraFields = new AnimBool(true);
        m_ShowExtraFields.valueChanged.AddListener(Repaint);
        #endregion
    }

    void OnGUI()
    {
        GUILayout.Label("Label");
        GUILayout.Label("Label-Bold", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("HelpBox", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        EditorGUILayout.HelpBox("None", MessageType.None);
        EditorGUILayout.HelpBox("Info", MessageType.Info);
        EditorGUILayout.HelpBox("Warning", MessageType.Warning);
        EditorGUILayout.HelpBox("Error", MessageType.Error);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        #region ToggleGroup
        groupEnabled = EditorGUILayout.BeginToggleGroup("ToggleGroup", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        #endregion

        #region FadeGroup
        m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("FadeGroup", m_ShowExtraFields.target);
        if (EditorGUILayout.BeginFadeGroup(m_ShowExtraFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PrefixLabel("ColorField");
            m_Color = EditorGUILayout.ColorField(m_Color);
            EditorGUILayout.PrefixLabel("TextField");
            m_String = EditorGUILayout.TextField(m_String);
            EditorGUILayout.PrefixLabel("IntSlider");
            m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();
        #endregion

        #region DropDown
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("DropDown", EditorStyles.boldLabel);
        if (GUILayout.Button(curChoose))
        {
            GenericMenu menu = new GenericMenu();

            AddMenuItemForPlatform(menu, "Game");
            AddMenuItemForPlatform(menu, "Boy");
            AddMenuItemForPlatform(menu, "Player/A");
            menu.AddSeparator("Player/");
            AddMenuItemForPlatform(menu, "Player/B");

            menu.ShowAsContext();
        }
        void AddMenuItemForPlatform(GenericMenu menu, string selectedName)
        {
            menu.AddItem(new GUIContent(selectedName), selectedName.Equals(curChoose), OnDropDownSelected, selectedName);
        }

        void OnDropDownSelected(object platformName)
        {
            curChoose = (string)platformName;
        }
        EditorGUILayout.EndVertical();
        #endregion

    }
}