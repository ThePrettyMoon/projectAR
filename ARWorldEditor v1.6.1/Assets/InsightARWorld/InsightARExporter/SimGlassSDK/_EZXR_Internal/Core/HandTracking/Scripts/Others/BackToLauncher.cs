using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLauncher : MonoBehaviour
{
    /// <summary>
    /// 指示应用是否已经初始化，主要用于解决OnApplicationPause在应用首次安装打开时会返回false造成应用退出，用于确保应用正常开始执行逻辑了才判断是否被切出去
    /// </summary>
    bool inited = false;

    // Start is called before the first frame update
    void Start()
    {
        inited = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Back to Launcher via Escape");
            StartCoroutine(OpenPackage("com.ezxr.ARGlass_Launcher"));
        }
    }

    public void OnButtonClicked()
    {
        Debug.Log("Back to Launcher via OnButtonClicked");
        StartCoroutine(OpenPackage("com.ezxr.ARGlass_Launcher"));
    }

    IEnumerator OpenPackage(string pkgName, AndroidJavaObject activity = null)
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("打开：" + pkgName);

        if (!Application.isEditor)
        {
            if (activity == null)
            {
                AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }

            using (AndroidJavaObject joPackageManager = activity.Call<AndroidJavaObject>("getPackageManager"))
            {
                using (AndroidJavaObject joIntent = joPackageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", pkgName))
                {
                    if (null != joIntent)
                    {
                        activity.Call("startActivity", joIntent);
                    }
                    else
                    {
                        Debug.Log("未安装此软件：" + pkgName);
                    }
                }
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        //if (pause && inited)
        //{
        //    AndroidJavaClass system = new AndroidJavaClass("android.os.Process");
        //    system.CallStatic("killProcess", system.CallStatic<int>("myPid")); ;
        //}
    }
}
