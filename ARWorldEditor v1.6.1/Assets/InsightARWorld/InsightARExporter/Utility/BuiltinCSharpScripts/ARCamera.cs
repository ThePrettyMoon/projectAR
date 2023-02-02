using UnityEngine;

namespace ARWorldEditor
{
    [RequireComponent(typeof(Camera))]
    public class ARCamera : MonoBehaviour
    {
        //public bool followMainCamera = true;

        Camera currentCamera;


        private void Awake()
        {
            currentCamera = GetComponent<Camera>();

            if (Camera.main == null)
            {
                GameObject cameraGo = new GameObject("Main Camera");
                Camera mainCamera = cameraGo.AddComponent<Camera>();
                cameraGo.tag = "MainCamera";

                mainCamera.fieldOfView = currentCamera.fieldOfView;
                mainCamera.transform.position = currentCamera.transform.position;
                mainCamera.transform.rotation = currentCamera.transform.rotation;
                mainCamera.transform.localScale = currentCamera.transform.localScale;
            }

            Camera.main.cullingMask = 0;
        }


        private void LateUpdate()
        {
            //if(followMainCamera)
            {
                currentCamera.fieldOfView = Camera.main.fieldOfView;
                currentCamera.transform.position = Camera.main.transform.position;
                currentCamera.transform.rotation = Camera.main.transform.rotation;
                currentCamera.transform.localScale = Camera.main.transform.localScale;
            }
        }
    }
}

