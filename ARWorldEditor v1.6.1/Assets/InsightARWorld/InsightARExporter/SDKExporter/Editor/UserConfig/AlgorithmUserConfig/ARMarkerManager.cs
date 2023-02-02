#define USE_EDITOR_WEB_REQUEST

// #if !AUTHORING_TOOL
// #define USE_EDITOR_WEB_REQUEST_ASYNC
// #endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEditor;

namespace UserConfig
{
    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    public class ARMarkerManager
    {
        public static string TextureUploadURL { get; set; }
        public static string MarkerRequestURL { get; set; }

        #region temporary json data
        [System.Serializable]
        public class ImageReceiveData
        {
            [System.Serializable]
            public class Result
            {
                public string fileName;
                public string objectName;
                public string url;
            };
            public int code;
            public string msg;
            public Result result;
        };
        [System.Serializable]
        public class MarkerAlgSendData
        {
            [System.Serializable]
            public class Image
            {
                public string image;
                public string type = "web";
            }
            public Image[] imageList;
            public int sdkVersionType = 3;
            public bool multiMarker = false;
        }

        [System.Serializable]
        public class MarkerAlgReceiveData
        {
            public int code;
            public string msg;
            public string result;
        }
        #endregion

        // singleton
        private static ARMarkerManager instance;
        public static ARMarkerManager Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new ARMarkerManager();
                    // instance = FindObjectOfType(typeof(ARMarkerManager)) as ARMarkerManager;
                    // if (null == instance)
                    // {
                    // 	GameObject go = FindObjectOfType(typeof(GameObject)) as GameObject;
                    // 	instance = (null != go) ? go.AddComponent<ARMarkerManager>() : new GameObject().AddComponent<ARMarkerManager>();
                    // }
                }
                return instance;
            }
        }

        private static AlgorithmData.MarkerImage[] markerImageArray = null;

        // call back
        // 0: success, 1: upload network error, 2: marker request network error, 3: marker download error, 4: other error
        public delegate void ProcessDoneCallBack(int status, string content, AlgorithmData.MarkerImage[] markerImageArray = null, byte[] wwwBytes = null);
        static ProcessDoneCallBack processDoneCallBack;

        // 为了AT
        public void RequestMarkerForAT(AlgorithmData.MarkerImage[] texArray, ProcessDoneCallBack callback)
        {
            markerImageArray = texArray;
            processDoneCallBack = callback;
            RequestMarkerDesc();
        }
        public void RequestMarker(AlgorithmData.MarkerImage[] texArray, ProcessDoneCallBack callback)
        {
            bool flag = false;
            markerImageArray = texArray;
            processDoneCallBack = callback;
            int seek;
            for (seek = 0; seek < texArray.Length; seek++)
                if (string.Empty == texArray[seek].remotePath)
                {

#if USE_EDITOR_WEB_REQUEST
                    UploadPNG(seek);
#else
				StartCoroutine(UploadPNG(seek));
#endif
                    flag = true;
                }

            if (!flag)
                processDoneCallBack(0, null);
        }

#if USE_EDITOR_WEB_REQUEST
        void UploadPNG(int index)
        {
            Debug.Log("开始上传图片...");
#if true
            {
                // byte[] bytes = File.ReadAllBytes(markerImageArray[index].localPath);

                FnWebRequestEditor.instance.ThreadSafe = true;
                string filePath = markerImageArray[index].localPath;
#if USE_EDITOR_WEB_REQUEST_ASYNC
                FnWebRequestEditor.instance.HttpUploadFileAsync(TextureUploadURL,new FileInfo(filePath).Name, filePath, index.ToString(), "application/octet-stream", null, UploadPNGCallBack, null);
#else
                FnWebRequestEditor.instance.HttpUploadFile(TextureUploadURL,new FileInfo(filePath).Name, filePath, index.ToString(), "application/octet-stream", null, UploadPNGCallBack, null);
#endif
            }
#else
			{
				// test code
				ImageReceiveData imageReceiveData = JsonUtility.FromJson<ImageReceiveData>("{\"code\":200,\"msg\":\"成功\",\"result\":{\"fileName\":\"MovieTex.jpg\",\"objectName\":\"e83c2703-a856-4882-9b80-3afc375b2127.jpg\",\"url\":\"http://ar-scene-source.nosdn.127.net/e83c2703-a856-4882-9b80-3afc375b2127.jpg\",\"fileSize\":113906}}");
				// 如果返回信息不是成功
				if (imageReceiveData.code != 200)
				{
					// 直接结束，返回错误信息
					processDoneCallBack(1, imageReceiveData.msg);
				}
				else
				{
					markerImageArray[index].remotePath = imageReceiveData.result.url;
					markerImageArray[index].remoteName = Path.GetFileName(markerImageArray[index].remotePath);
					ImageUploadDone();
				}
				yield return null;
			}
#endif
        }

        void UploadPNGCallBack(FnWebRequestEditor.ResponseData responseData, string identifier)
        {
            if (responseData.isError)
            {
                processDoneCallBack(2, responseData.error);
                return;
            }

            string text = System.Text.Encoding.UTF8.GetString(responseData.data);
            int index = System.Int32.Parse(identifier);
            ImageReceiveData imageReceiveData = JsonUtility.FromJson<ImageReceiveData>(text);
            // 如果返回信息不是成功
            if (imageReceiveData.code != 200)
            {
                // 直接结束，返回错误信息
                processDoneCallBack(1, imageReceiveData.msg);
            }
            else
            {
                Debug.Log(imageReceiveData.result.fileName + "上传成功");
                markerImageArray[index].remotePath = imageReceiveData.result.url;
                markerImageArray[index].remoteName = Path.GetFileName(markerImageArray[index].remotePath);
                ImageUploadDone();
            }
        }

#else

		IEnumerator UploadPNG(int index)
		{
			Debug.Log("开始上传图片...");
#if true
			{
				byte[] bytes = File.ReadAllBytes(markerImageArray[index].localPath);

				WWWForm form = new WWWForm();
				form.AddBinaryData("file", bytes, Path.GetFileName(markerImageArray[index].localPath), "multipart/form-data");

				using (var w = UnityWebRequest.Post(TextureUploadURL, form))
				{
					yield return w.Send();
					if (w.isNetworkError || w.isHttpError) 
					{
						// 直接结束，返回错误信息
						processDoneCallBack(1, w.error);
					}
					else 
					{
						ImageReceiveData imageReceiveData = JsonUtility.FromJson<ImageReceiveData>(w.downloadHandler.text);
						// 如果返回信息不是成功
						if (imageReceiveData.code != 200)
						{
							// 直接结束，返回错误信息
							processDoneCallBack(1, imageReceiveData.msg);
						}
						else
						{
							Debug.Log(imageReceiveData.result.fileName + "上传成功");
							markerImageArray[index].remotePath = imageReceiveData.result.url;
							markerImageArray[index].remoteName = Path.GetFileName(markerImageArray[index].remotePath);
							ImageUploadDone();
						}
					}
				}
			}
#else
			{
				// test code
				ImageReceiveData imageReceiveData = JsonUtility.FromJson<ImageReceiveData>("{\"code\":200,\"msg\":\"成功\",\"result\":{\"fileName\":\"MovieTex.jpg\",\"objectName\":\"e83c2703-a856-4882-9b80-3afc375b2127.jpg\",\"url\":\"http://ar-scene-source.nosdn.127.net/e83c2703-a856-4882-9b80-3afc375b2127.jpg\",\"fileSize\":113906}}");
				// 如果返回信息不是成功
				if (imageReceiveData.code != 200)
				{
					// 直接结束，返回错误信息
					processDoneCallBack(1, imageReceiveData.msg);
				}
				else
				{
					markerImageArray[index].remotePath = imageReceiveData.result.url;
					markerImageArray[index].remoteName = Path.GetFileName(markerImageArray[index].remotePath);
					ImageUploadDone();
				}
				yield return null;
			}
#endif
		}
#endif

        private void ImageUploadDone()
        {
            int seek;
            for (seek = 0; seek < markerImageArray.Length; seek++)
                if (string.Empty == markerImageArray[seek].remotePath)
                    break;

            // 所有图片均已上传
            if (seek >= markerImageArray.Length)
#if USE_EDITOR_WEB_REQUEST
                RequestMarkerDesc();
#else
				StartCoroutine(RequestMarkerDesc());
				// processDoneCallBack(0, null, markerImageArray);
#endif
        }

#if USE_EDITOR_WEB_REQUEST
        void RequestMarkerDesc()
        {
            Debug.Log("开始请求描述文件...");
            // parse json
            MarkerAlgSendData markerAlgSendData = new MarkerAlgSendData();
            markerAlgSendData.imageList = new MarkerAlgSendData.Image[markerImageArray.Length];
            for (int i = 0; i < markerImageArray.Length; i++)
            {
                markerAlgSendData.imageList[i] = new MarkerAlgSendData.Image();
                markerAlgSendData.imageList[i].image = markerImageArray[i].remotePath;
            }
            if(UserConfig.AlgSessionConfigExporter.config.ARSessionConfigs.markerTrackingMaxNum > 1)
                markerAlgSendData.multiMarker = true;
            string jsonString = JsonUtility.ToJson(markerAlgSendData) ?? "";
            FnWebRequestEditor.instance.ThreadSafe = true;
#if USE_EDITOR_WEB_REQUEST_ASYNC
            FnWebRequestEditor.instance.CreateRequestAsync(MarkerRequestURL, jsonString, string.Empty, RequestMarkerDescCallBack, null, FnWebRequestEditor.RequestMode.POST, "application/json");
#else
            FnWebRequestEditor.ResponseData data = FnWebRequestEditor.instance.CreateRequest(MarkerRequestURL, jsonString, FnWebRequestEditor.RequestMode.POST, "application/json");
			RequestMarkerDescCallBack(data, string.Empty);
#endif
        }

        void RequestMarkerDescCallBack(FnWebRequestEditor.ResponseData responseData, string index)
        {
            if (responseData.isError)
            {
                processDoneCallBack(2, responseData.error);
                return;
            }

            string text = System.Text.Encoding.UTF8.GetString(responseData.data);
            MarkerAlgReceiveData markerAlgReceiveData = JsonUtility.FromJson<MarkerAlgReceiveData>(text);
            // 如果返回信息不是成功
            if (markerAlgReceiveData.code != 200 || string.IsNullOrEmpty(markerAlgReceiveData.result))
            {
                // 直接结束，返回错误信息
                processDoneCallBack(2, markerAlgReceiveData.msg);
            }
            else
            {
                Debug.Log("描述文件请求成功");
#if USE_EDITOR_WEB_REQUEST_ASYNC
                FnWebRequestEditor.instance.CreateRequestAsync(markerAlgReceiveData.result, string.Empty, string.Empty, DownloadMarkerCallBack, null, FnWebRequestEditor.RequestMode.GET, string.Empty);
#else
                FnWebRequestEditor.ResponseData data = FnWebRequestEditor.instance.CreateRequest(markerAlgReceiveData.result, string.Empty, FnWebRequestEditor.RequestMode.GET, string.Empty);
				DownloadMarkerCallBack(data, string.Empty);
#endif
            }
        }

#else

		IEnumerator RequestMarkerDesc()
		{
			Debug.Log("开始请求描述文件...");
			// parse json
			MarkerAlgSendData markerAlgSendData = new MarkerAlgSendData();
			markerAlgSendData.imageList = new MarkerAlgSendData.Image[markerImageArray.Length];
			for (int i = 0; i < markerImageArray.Length; i ++)
			{
				markerAlgSendData.imageList[i] = new MarkerAlgSendData.Image();
				markerAlgSendData.imageList[i].image = markerImageArray[i].remotePath;
			}

			string jsonString = JsonUtility.ToJson(markerAlgSendData) ?? "";
			using (var w = new UnityWebRequest(MarkerRequestURL, "POST"))
			{
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
				w.uploadHandler = (UploadHandler) new UploadHandlerRaw(bytes);
				w.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
				w.SetRequestHeader("Content-Type", "application/json");
				yield return w.Send();
				if (w.isNetworkError || w.isHttpError) 
				{
					// 直接结束，返回错误信息
					processDoneCallBack(2, w.error);
				}
				else 
				{
					MarkerAlgReceiveData markerAlgReceiveData = JsonUtility.FromJson<MarkerAlgReceiveData>(w.downloadHandler.text);
					// 如果返回信息不是成功
					if (markerAlgReceiveData.code != 200)
					{
						// 直接结束，返回错误信息
						processDoneCallBack(2, markerAlgReceiveData.msg);
					}
					else
					{
						Debug.Log("描述文件请求成功");
						StartCoroutine(DownloadMarker(markerAlgReceiveData.result));
					}
				}
			}
		}

#endif

#if USE_EDITOR_WEB_REQUEST

        void DownloadMarkerCallBack(FnWebRequestEditor.ResponseData responseData, string identifier)
        {
            if (responseData.isError)
            {
                processDoneCallBack(3, responseData.error);
                return;
            }

            if (null == responseData || null == responseData.data)
            {
                // 直接结束，返回错误信息
                processDoneCallBack(3, "描述文件下载失败");
            }
            else
            {
                processDoneCallBack(0, null, markerImageArray, responseData.data);
            }
        }

#else

		IEnumerator DownloadMarker(string url)
		{
			using (var www = new WWW(url))
			{
				yield return www;
				if (!string.IsNullOrEmpty(www.error))
				{
					// 直接结束，返回错误信息
					processDoneCallBack(3, www.error);
				}
				else
				{
					if (null == www || null == www.bytes)
					{
						// 直接结束，返回错误信息
						processDoneCallBack(3, "描述文件下载失败");
					}
					else
					{
						processDoneCallBack(0, null, markerImageArray, www.bytes);
					}
				}
			}
		}
#endif
    }
}

