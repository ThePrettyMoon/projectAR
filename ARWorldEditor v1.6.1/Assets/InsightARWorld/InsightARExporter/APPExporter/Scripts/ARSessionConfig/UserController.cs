#if UNITY_EDITOR

using System;
using ARWorldEditor;
using UnityEditor;
using UnityEngine;

namespace ARWorldEditor
{
    [InitializeOnLoad]
    public class UserController
    {
        #region params
        private static bool sUserLogin = false;
        private const string USER_LOGIN_KEY = "User_Login";
        private const string USER_INFO_KEY = "User_Info";
        public static bool UserLogin
        {
            get
            {
                sUserLogin = !string.IsNullOrEmpty(PlayerPrefs.GetString(USER_LOGIN_KEY));
                return sUserLogin;
            }
        }
        public static bool UserRoleIsSuperManager
        {
            get
            {
                return PlayerPrefs.GetString(USER_INFO_KEY) == "superManager";
            }
        }
        #endregion

        #region custom functions
        static UserController()
        {

        }

        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {

            EditorApplication.wantsToQuit -= Quit;

            EditorApplication.wantsToQuit += Quit;

        }

        public static bool Quit()
        {
            PlayerPrefs.DeleteKey(USER_LOGIN_KEY);
            sUserLogin = false;
            PlayerPrefs.Save();
            return true;
        }

        /// <summary>
        /// 重新进入登陆界面
        /// </summary>
        public static void ReEnterLoginView()
        {
            Quit();
            Debug.Log("会话过期，跳转到登陆界面");
            LoginSceneWindow.ShowWindow();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        public static void Login(string email, string pwd, Action onSuccess, Action onFail)
        {
            ARWorldEditor.NetDataFetchManager.Instance.Login(email, pwd, new OnOasisNetworkDataFetchCallback<ARWorldEditor.LoginResponseData>(
                (ARWorldEditor.LoginResponseData response) =>
                {
                    OnLoginSuccess(onSuccess);
                }, (string code, string msg) =>
                {
                    OnLoginFail(code, msg, onFail);
                }));
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        public static void Logout(Action onSuccess, Action onFail)
        {
            ARWorldEditor.NetDataFetchManager.Instance.Logout(new OnOasisNetworkDataFetchCallback<LogoutResponseData>(
             (LogoutResponseData response) =>
             {
                 OnLogOutSuccess(onSuccess);

             }, (string code, string msg) =>
             {
                 OnLogoutFail(code, msg, onFail);
             }));
        }

        /// <summary>
        /// 登陆成功
        /// </summary>
        /// <param name="onSuccess"></param>
        private static void OnLoginSuccess(Action onSuccess)
        {
            Debug.Log("login success");
            sUserLogin = true;
            PlayerPrefs.SetString(USER_LOGIN_KEY, "userLogin");
            onSuccess?.Invoke();

            //登陆成功后获取一次nostoken
            ARWorldEditor.NetDataFetchManager.Instance.GetNosToken(new OnOasisNetworkDataFetchCallback<GetNosTokenResponseData>(
                (GetNosTokenResponseData response) =>
                {
                 //   Debug.Log("get token success");
                }, (string code, string msg) =>
                 {
                  //   Debug.Log("get token fail " + code + " " + msg);
                 }));
            NetDataFetchManager.Instance.GetUserInfo(new OnOasisNetworkDataFetchCallback<GetUserInfoResponseData>(
           (GetUserInfoResponseData data) => {
               PlayerPrefs.SetString(USER_INFO_KEY, data.result.roleCode);
           },
           (string code, string msg) => {
               EditorUtility.DisplayDialog("Error", "获取我的用户权限失败\n code = " + code + "\n msg = " + msg, "确认");
           }
            ));
        }

        /// <summary>
        /// 登陆失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="onFail"></param>
        private static void OnLoginFail(string code, string msg, Action onFail)
        {
            string content = "login fail " + code + " msg " + msg;
            if (!string.IsNullOrEmpty(code))
            {
                if (code == "00000005")
                {
                    EditorUtility.DisplayDialog("Error", content+ "\n当前账号密码不匹配，请重新输入！", "确认");
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", content, "确认");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", content, "确认");
            }
            Debug.Log(content);
            sUserLogin = false;
            PlayerPrefs.DeleteKey(USER_LOGIN_KEY);
            onFail?.Invoke();
        }

        /// <summary>
        /// 登出成功
        /// </summary>
        /// <param name="onSuccess"></param>
        private static void OnLogOutSuccess(Action onSuccess)
        {
            Debug.Log("logout success");
            sUserLogin = false;
            PlayerPrefs.DeleteKey(USER_LOGIN_KEY);
            onSuccess?.Invoke();
        }

        /// <summary>
        /// 登出失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="onFail"></param>
        private static void OnLogoutFail(string code, string msg, Action onFail)
        {
            Debug.Log("logout fail " + code + " msg " + msg);
            onFail?.Invoke();
        }
#endregion
    }
}
#endif