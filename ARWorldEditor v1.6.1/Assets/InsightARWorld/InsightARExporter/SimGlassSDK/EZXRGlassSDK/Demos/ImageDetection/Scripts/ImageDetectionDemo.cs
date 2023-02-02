using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass;
using System.IO;
using EZXR.Glass.ImageDetection;

public class ImageDetectionDemo : MonoBehaviour
{
    public GameObject image;
    /// <summary>
    /// 用于显示检测到的Image
    /// </summary>
    Renderer imageRenderer;
    /// <summary>
    /// Image原图
    /// </summary>
    public Texture2D[] images;
    /// <summary>
    /// 用于从ImageTrackingManager得到检测到的Image的信息
    /// </summary>
    ImageDetectionInfo imageDetectionInfo;

    // Start is called before the first frame update
    void Start()
    {
        imageRenderer = image.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (image != null)
        {
            if (ImageDetectionManager.isReady)
            {
                if (ImageDetectionManager.GetDetectionResult(out imageDetectionInfo))
                {
                    Debug.Log("ImageName：" + imageDetectionInfo.name);

                    foreach (Texture2D image in images)
                    {
                        //if (image.name == imageDetectionInfo.name)
                        if ("default" == imageDetectionInfo.name)
                        {
                            imageRenderer.material.mainTexture = image;
                            break;
                        }
                    }

                    image.transform.position = imageDetectionInfo.position;
                    image.transform.rotation = imageDetectionInfo.rotation;
                    image.transform.localScale = new Vector3(imageDetectionInfo.width, imageDetectionInfo.height,0.001f );

                    image.SetActive(true);
                }
            }
        }
    }
}
