using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ARWorldEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace ARWorldEditor
{
	public class UnityHttpRequest : IHttpRequest
	{
		private const string TAG = "UnityHttpRequest";
		private IHttpRequestCreate httpWebRequest;

		private event Action<string, HttpResponse> onError;
		private event Action<HttpResponse> onSuccess;
		private event Action<float> onDownloadProgrss;
		private event Action<float> onUploadProgress;

		private float downloadProgress;
		private float uploadProgress;

		//重连次数
		private const int MAX_RETRY_COUNT = 3;

		public UnityHttpRequest(IHttpRequestCreate _request)
		{
			httpWebRequest = _request;
		}
		#region create

		#region post
		public static IHttpRequest Post(BaseRequest request)
		{
			BaseHttpJsonBodyCreate bodyCreate = new BaseHttpJsonBodyCreate(request);
			string uri = bodyCreate.Url();
			string body = bodyCreate.Body();
			string contentType = bodyCreate.ContentType();
			int timeout = bodyCreate.Timeout();
			Dictionary<string, string> headers = bodyCreate.Headers();
			byte[] bytes = string.IsNullOrEmpty(body) ? null : Encoding.UTF8.GetBytes(body);
			return Post(uri, bytes, contentType, headers, timeout);
		}

		public static IHttpRequest Post(string uri, byte[] bytes, string contentType, Dictionary<string, string> headers, int timeout, bool useHttpContinue = false)
		{
			Debug.Log("Post Url " + uri);
			HttpRequestPostCompleteData httpRequestPostCompleteData = HttpRequestPostCompleteData.Post(uri, bytes, contentType, headers, timeout);
			return new UnityHttpRequest(httpRequestPostCompleteData);
		}

		public static IHttpRequest Post(string uri, byte[] bytes, string contentType, Dictionary<string, string> headers)
		{
			return Post(uri, bytes, contentType, headers, 10);
		}

		public static IHttpRequest Post(string uri, byte[] bytes, string contentType)
		{
			return Post(uri, bytes, contentType, null, 10);
		}


		public static IHttpRequest Post(string uri, string postData)
		{
			HttpRequestPostData httpRequestPostData = HttpRequestPostData.Post(uri, postData);
			return new UnityHttpRequest(httpRequestPostData);
		}

		public static IHttpRequest Post(string uri, WWWForm formData)
		{
			HttpRequestPostFormData httpRequestPostData = HttpRequestPostFormData.Post(uri, formData);
			return new UnityHttpRequest(httpRequestPostData);
		}

		public static IHttpRequest Post(string uri, Dictionary<string, string> formData)
		{
			HttpRequestPostDictFormData httpRequestPostDictFormData = HttpRequestPostDictFormData.Post(uri, formData);
			return new UnityHttpRequest(httpRequestPostDictFormData);
		}

		public static IHttpRequest Post(string uri, List<IMultipartFormSection> multipartForm)
		{
			HttpRequestPostMultiFormData httpRequestPostMultiFormData = HttpRequestPostMultiFormData.Post(uri, multipartForm);
			return new UnityHttpRequest(httpRequestPostMultiFormData);
		}

		public static IHttpRequest PostJson(string uri, string json)
		{
			return Post(uri, Encoding.UTF8.GetBytes(json), "application/json", null, 10);
		}

		public static IHttpRequest Post(string uri, string json, string contentType)
		{
			return Post(uri, Encoding.UTF8.GetBytes(json), contentType, null, 10);
		}

		public static IHttpRequest PostJson<T>(string uri, T payload) where T : class
		{
			return PostJson(uri, JsonUtility.ToJson(payload));
		}
		#endregion

		#region get
		public static IHttpRequest Get(BaseRequest request)
		{
			BaseHttpJsonBodyCreate bodyCreate = new BaseHttpJsonBodyCreate(request);
			string uri = bodyCreate.Url();
			int timeout = bodyCreate.Timeout();
			Dictionary<string, string> headers = bodyCreate.Headers();
			return Get(uri,headers,timeout);
		}

		public static IHttpRequest Get(string uri, Dictionary<string, string> headers, int timeout = 10, bool useHttpContinue = false)
        {
			HttpRequestGetCompleteData httpRequestGetCompleteData = HttpRequestGetCompleteData.Get(uri, headers, timeout, useHttpContinue);
			return new UnityHttpRequest(httpRequestGetCompleteData);
        }
		public static IHttpRequest Get(string uri, Dictionary<string, string> headers)
		{
			return Get(uri, headers, 10);
		}
		public static IHttpRequest Get(string uri)
		{
			return Get(uri, null, 10);
		}

		public static IHttpRequest GetTexture(string uri)
		{
			HttpRequestGetTexture httpRequestGetTexture = HttpRequestGetTexture.Get(uri);
			return new UnityHttpRequest(httpRequestGetTexture);
		}

		/// <summary>
		/// 设置下载路径
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="downloadFilePath"></param>
		/// <returns></returns>
		public static IHttpRequest GetFile(string uri, string downloadFilePath)
		{
			HttpRequestGetFile httpRequestGetFile = HttpRequestGetFile.Get(uri, downloadFilePath);
			return new UnityHttpRequest(httpRequestGetFile);
		}

		#endregion

		#region put

		public static IHttpRequest Put(string uri, byte[] bodyData)
		{
			HttpRequestPutData httpRequestPutData = HttpRequestPutData.Put(uri, Encoding.UTF8.GetString(bodyData));
			return new UnityHttpRequest(httpRequestPutData);
		}

		public static IHttpRequest Put(string uri, string bodyData)
		{
			HttpRequestPutData httpRequestPutData = HttpRequestPutData.Put(uri, bodyData);
			return new UnityHttpRequest(httpRequestPutData);
		}
		#endregion

		#region delete

		public static IHttpRequest Delete(string uri)
		{
			HttpRequestDeleteData httpRequestDeleteData = HttpRequestDeleteData.Delete(uri);
			return new UnityHttpRequest(httpRequestDeleteData);
		}

		#endregion

		#region head

		public static IHttpRequest Head(string uri)
		{
			HttpRequestHeadData httpRequestHeadData = HttpRequestHeadData.Head(uri);
			return new UnityHttpRequest(httpRequestHeadData);
		}
		#endregion

		#endregion

		#region send
		public IHttpRequest AddListener(Action<HttpResponse> _onSuccess, Action<string, HttpResponse> _onError,
			Action<float> _uploadProgrss = null, Action<float> _downloadProgess = null)
		{
			this.onSuccess += _onSuccess;
			this.onError += _onError;
			this.onDownloadProgrss += _downloadProgess;
			this.onUploadProgress += onUploadProgress;
			return this;
		}

		public IEnumerator Send()
		{
			int count = 0;
			HttpResponse httpResponse = null;
			while (count < MAX_RETRY_COUNT)
			{
				count++;

				httpWebRequest.CreateWebRequest();

				yield return httpWebRequest.GetWebRequest().SendWebRequest();

				httpResponse = HttpResponse.CreateResponse(httpWebRequest.GetWebRequest());

				if (string.IsNullOrEmpty(httpResponse.Error))
				{
					onSuccess?.Invoke(httpResponse);
					yield break;
				}
				else
				{
					Debug.Log("http request error " + httpResponse.Error);
				}

				httpWebRequest.Dispose();

				Debug.Log("Http Request Retry Count " + count);
			}

			onError?.Invoke(NetworkCode.HTTP_ERROR.ToString(), null);
		}

		public void Abort()
		{
			if (httpWebRequest!=null && httpWebRequest.GetWebRequest() != null )
			{
				httpWebRequest.GetWebRequest().Abort();
			}
		}

		/// <summary>
		/// dispose
		/// </summary>
		public void Dispose()
		{
			if (httpWebRequest!=null && httpWebRequest.GetWebRequest() != null )
			{
				httpWebRequest.GetWebRequest().Dispose();
			}
		}

		public void UpdateProgress()
		{
			if (httpWebRequest != null && httpWebRequest.GetWebRequest() != null)
			{
				UpdateProgress(ref downloadProgress, httpWebRequest.GetWebRequest().downloadProgress, onDownloadProgrss);
				UpdateProgress(ref uploadProgress, httpWebRequest.GetWebRequest().uploadProgress, onUploadProgress);
			}
		}

		private void UpdateProgress(ref float currentProgress, float progress, Action<float> onProgress)
		{
			if (currentProgress < progress)
			{
				currentProgress = progress;
				onProgress?.Invoke(progress);
			}
		}
		#endregion
	}
}
