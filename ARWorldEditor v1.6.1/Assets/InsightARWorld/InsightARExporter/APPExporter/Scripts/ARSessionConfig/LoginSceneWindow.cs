#if UNITY_EDITOR
using ARWorldEditor;
using UnityEditor;
using UnityEngine;

namespace ARWorldEditor
{
    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    public class LoginSceneWindow : EditorWindow
    {
        #region params
        static LoginSceneWindow _instance;
        private Texture logoTexture;
        private string loginEmail = "";
        private string loginPwd = "";
        private GUISkin skin;
        private const string EMAIL_TITLE = "网易通行证";
        private const string PWD_TITLE = "密码";
        private bool toggleOn = false;
        #endregion

        #region unity functions
        private void Awake()
        {

        }


        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            Rect windowRect = _instance.position;
            Rect loginRect = new Rect(0, 0, windowRect.width, windowRect.height);
            DrawLoginPanel(loginRect);
        }

        private void OnDestroy()
        {
            CloseWindow();
        }
        #endregion

        #region custom functions
        private void CloseWindow()
        {
            if (toggleOn)
            {
                PlayerPrefs.SetString("email", loginEmail);
                PlayerPrefs.SetString("pwd", loginPwd);
                PlayerPrefs.SetString("toggle", "1");
            }
            else
            {
                PlayerPrefs.DeleteKey("email");
                PlayerPrefs.DeleteKey("pwd");
                PlayerPrefs.DeleteKey("toggle");
            }
        }
        public static void ShowWindow()
        {
            if (_instance == null)
            {
                _instance = (LoginSceneWindow)EditorWindow.GetWindow(typeof(LoginSceneWindow), true, "洞见AR-World");
                _instance.minSize = new Vector2(1000, 750);
                _instance.LoadAssets();
            }
            _instance.Show();
        }

        private void LoadAssets()
        {
            logoTexture = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.imageDirectory + "ARWORLDlogo" + ".png", typeof(Texture)) as Texture;
            skin = AssetDatabase.LoadAssetAtPath(AlgorithmGlobal.FileDirectory + "LoginSkin.guiskin", typeof(GUISkin)) as GUISkin;
            loginEmail = PlayerPrefs.GetString("email");
            loginPwd = PlayerPrefs.GetString("pwd");
            toggleOn = PlayerPrefs.GetString("toggle") == "1" ? true : false;
        }



        private void DrawLoginPanel(Rect rect)
        {
            GUI.DrawTexture(new Rect(rect.x + 278, rect.y + 180, 444, 71), logoTexture);
            GUI.skin = skin;
            GUI.Label(new Rect(rect.x + 278, rect.y + 298, 200, 37), EMAIL_TITLE, GUI.skin.customStyles[1]);
            loginEmail = GUI.TextField(new Rect(rect.x + 278, rect.y + 350, 444, 37), loginEmail);
            GUI.Label(new Rect(rect.x + 278, rect.y + 411, 200, 30), PWD_TITLE, GUI.skin.customStyles[1]);
            loginPwd = GUI.PasswordField(new Rect(rect.x + 278, rect.y + 456, 444, 37), loginPwd, '*');
            toggleOn = GUI.Toggle(new Rect(rect.x + 278, rect.y + 495, 200, 37), toggleOn, "记住密码?", GUI.skin.toggle);
            if (GUI.Button(new Rect(rect.x + 278, rect.y + 556, 444, 48), "登录", skin.button))
            {
                OnClickLoginHandler(loginEmail, loginPwd);
            }



            GUI.skin = null;
        }

        private void OnClickLoginHandler(string email, string pwd)
        {
            if (UserController.UserLogin)
            {
                Debug.Log("已经登录，请不要重复登录");
                _instance.Close();
                return;
            }

            UserController.Login(loginEmail, loginPwd, OnLoginSuccess, OnLoginError);
        }

        private void OnLoginSuccess()
        {
            _instance.Close();
        }

        private void OnLoginError()
        {

        }
        #endregion

    }
}
#endif
