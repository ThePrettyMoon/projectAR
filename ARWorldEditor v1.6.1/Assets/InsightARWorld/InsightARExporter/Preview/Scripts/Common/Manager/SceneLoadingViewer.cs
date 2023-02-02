using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadingViewer : UnitySingleton<SceneLoadingViewer>
{

    private GameObject loadingCanvas;
    private GameObject instantedLoadingCanvas;
    private GameObject loadingViews;
    private GameObject exitingViews;
    private GameObject loadingBg;

    //供外部调用实例，方能
    public void manInstance() {

        if(loadingCanvas == null)
            loadingCanvas = Resources.Load("prefabs/LoadingCanvas_DontDestory") as GameObject;

        GameObject obj = GameObject.Find("LoadingCanvas");
        if (obj)
        {
            instantedLoadingCanvas = obj;
        }
        else {
            instantedLoadingCanvas = Instantiate(loadingCanvas);
            instantedLoadingCanvas.name = "LoadingCanvas";
            instantedLoadingCanvas.AddComponent<DontDestoryObjectOnLoad>();
            loadingBg = GameObject.Find("LoadingBG");
            loadingViews = GameObject.Find("LoadingView");
            exitingViews = GameObject.Find("ExitingView");
            loadingBg.SetActive(false);
            loadingViews.SetActive(false);
            exitingViews.SetActive(false);
        }
        instantedLoadingCanvas.SetActive(false);

    }
    /// <summary>
    /// 关闭所有加载视图，在成功进入内容之后
    /// </summary>
    public void HideAllLoadingCanvas()
    {
        instantedLoadingCanvas.SetActive(false);
    }
    /// <summary>
    /// 退出主内容进度视图
    /// </summary>
    public void ExitLoadingCanvas()
    {
        instantedLoadingCanvas.SetActive(true);
        loadingViews.SetActive(false);
        loadingBg.SetActive(true);
        exitingViews.SetActive(true);
    }

    /// <summary>
    /// 加载主内容视图
    /// </summary>
    public void EnterLoadingView()
    {
        instantedLoadingCanvas.SetActive(true);
        exitingViews.SetActive(false);
        loadingBg.SetActive(true);
        loadingViews.SetActive(true);
    }

    /// <summary>
    /// 退出子内容进度视图
    /// </summary>
    public void ExitSubLoadingCanvas()
    {
        instantedLoadingCanvas.SetActive(true);
        loadingViews.SetActive(false);
        loadingBg.SetActive(false);
        exitingViews.SetActive(true);
    }

    /// <summary>
    /// 加载子内容视图
    /// </summary>
    public void EnterSubLoadingView()
    {
        instantedLoadingCanvas.SetActive(true);
        exitingViews.SetActive(false);
        loadingBg.SetActive(false);
        loadingViews.SetActive(true);
    }

    public Image tryGetLoadingImage()
    {
        var line = GameObject.Find("LoadingLinebg");
        if (line)
        {
            var fill = line.GetComponent<Image>();
            if (fill)
            {
                if (fill.type != Image.Type.Filled)
                {
                    fill.type = Image.Type.Filled;
                    fill.fillMethod = Image.FillMethod.Horizontal;
                    fill.fillOrigin = 0;
                    fill.fillAmount = 0.01f;
                }
            }
            return fill;
        }
        return null;
    }

    public void trySetLoadingProgress(Image image, float progress)
    {
        if (Mathf.Approximately(progress, 1.0f))
            progress = 0;
        if (image) image.fillAmount = progress;
    }
}
