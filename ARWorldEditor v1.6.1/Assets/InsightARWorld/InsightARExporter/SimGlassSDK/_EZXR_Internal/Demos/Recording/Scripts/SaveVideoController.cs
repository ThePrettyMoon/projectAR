using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using System.Threading.Tasks;

public class SaveVideoController : MonoBehaviour
{
    public Button saveButton;
    public Camera phoneCamera;
    private IMediaRecorder recorder;
    private CameraInput cameraInput;

    int cnt = 0;

    bool beSave = false;
    bool beVideoSave = false;

    private static int img_width;
    private static int img_height;
    // Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(HandleSaveButtonClick);
        phoneCamera.enabled = true;
        CameraResolution cameraResolution = new CameraResolution();
        NativeTracking.GetRGBCameraResolution(ref cameraResolution);
        img_width = cameraResolution.width;
        img_height = cameraResolution.height;
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnPostRender()
    {
        //if(beSave)
        //{
        //    File.WriteAllBytes(Application.persistentDataPath + "/image" + cnt++ + ".png", ARRender.bytes);
        //}
    }

    private void HandleSaveButtonClick()
    {
        if(beSave)
        {
            phoneCamera.enabled = false;

            cameraInput.Dispose();

            Debug.Log("UNITY LOG ========= Stop Record");

            GetPathAsync();
            //var path = await  recorder.FinishWriting();

            Debug.Log("UNITY LOG ========= Path Saved");

            saveButton.transform.Find("Text").GetComponent<Text>().text = "Start Save Video";
            beSave = false;

            beVideoSave = true;
        }
        else
        {
            phoneCamera.enabled = true;

            saveButton.transform.Find("Text").GetComponent<Text>().text = "Stop Save Video";
            beSave = true;

            var frameRate = 30;
            var sampleRate = 0;
            var channelCount = 0;
            var clock = new RealtimeClock();
            //recorder = new MP4Recorder(img_width, img_height, frameRate, sampleRate, channelCount);
             recorder = new MP4Recorder(1280, 720, frameRate, sampleRate, channelCount);
            cameraInput = new CameraInput(recorder, clock, phoneCamera);
            Debug.Log("UNITY LOG ========= Start Record");

        }
    }

    private async void GetPathAsync()
    {
        var path = await recorder.FinishWriting();
        Debug.Log("Saved Recording to : " + path);
    }

    //private void TextureToMat(in Color32[] c, ref Mat img)
    //{
    //    byte[] imageArray = new byte[img.Rows * img.Cols];
    //    for(var i = 0; i < img.Rows; i ++)
    //    {
    //        for(var j = 0; j < img.Cols;j ++)
    //        {
    //            var col = c[j + i * img.Cols];
    //            imageArray[j + i * img.Rows] = col.r;
    //        }
    //    }
    //    img.SetArray(0, 0, imageArray);
    //}
}
