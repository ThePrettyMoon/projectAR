using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ARWorldEditor;

namespace ARSession
{
    public class LogoutScene
    {
        private static LogoutScene _instance;

        public static void Logout()
        {
            if (_instance == null)
            {
                _instance = new LogoutScene();
            }

            UserController.Logout(OnLogoutSuccess, OnLogoutFail);
        }

        private static void OnLogoutSuccess()
        {
            EditorUtility.DisplayDialog("退出", "账号已登出", "确认");
            //清空产品
            //MyProducts.MyProduct = null;
            PlayerPrefs.DeleteKey("MyProduct");
        }

        private static void OnLogoutFail()
        {

        }
    }
}
