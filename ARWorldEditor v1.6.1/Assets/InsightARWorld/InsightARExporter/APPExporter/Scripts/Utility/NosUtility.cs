#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using ARWorldEditor;
using Netease.Cloud.NOS;
using UnityEditor;
using UnityEngine;

namespace ARWorldEditor
{
    public class NosUtility
    {
        #region params
        private static NosClient sNosClient;

        private static NosUtility _instance;

        public static NosUtility Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NosUtility();
                    _instance.InitsNosClient();
                }

                return _instance;
            }
        }

        private void InitsNosClient()
        {
            string endPoint = NOSConfig.END_POINT;
            string accessKeyId = NOSConfig.ACCESS_KEY;
            string accessKeySecret = NOSConfig.SECRET_KEY;

            ClientConfiguration conf = new ClientConfiguration();
            // 设置sNosClient使用的最大连接数
            conf.MaxConnections = 200;
            // 设置socket超时时间,单位：毫秒
            conf.SocketTimeout = 10000;
            // 设置失败请求重试次数
            conf.MaxErrorRetry = 2;

            sNosClient = new NosClient(endPoint, accessKeyId, accessKeySecret);
        }
        #endregion

        #region 上传

        /// <summary>
        /// 流式上传
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="content"></param>
        public void PutObject(string bucket, string key, Stream content)
        {
            try
            {
                sNosClient.PutObject(bucket, key, content);
                Debug.LogFormat("Put object:{0} succeeded", key);
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 上传文件
        /// 上传的文件内容不超过 100M
        /// </summary>
        /// <param name="桶名"></param>
        /// <param name="对象名"></param>
        /// <param name="上传的文件"></param>
        public void PutObject(string bucket, string key, string fileToUpload)
        {
            try
            {
                sNosClient.PutObject(bucket, key, fileToUpload);
                Debug.LogFormat("Put object:{0} succeeded", key);
            }
            catch (NosException ex)
            {
                string str = string.Format("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
                Debug.Log(str);
                EditorUtility.DisplayDialog("上传异常", str, "确认");
            }
            catch (Exception ex)
            {
                string str = string.Format("Failed with error info: {0}", ex.Message);
                Debug.Log(str);
                EditorUtility.DisplayDialog("上传异常", str, "确认");
            }
        }

        /// <summary>
        /// 分块上传
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <param name="key">对象名</param>
        /// <param name="fileToUpload">上传的文件</param>
        public void UploadPart(string bucket, string key, string fileToUpload)
        {
            try
            {
                /*
            * 初始化一个分块上传事件
            * */
                var initRequest = new InitiateMultipartUploadRequest(bucket, key);
                var initResult = sNosClient.InitiateMultipartUpload(initRequest);

                /*
            * 上传分块
            * */

                // 设置每块为 1M
                const int partSize = 1024 * 1024 * 1;

                var partFile = new FileInfo(fileToUpload);
                var fileSize = partFile.Length;
                // 计算分块数目
                var partCount = fileSize / partSize;
                if (fileSize % partSize != 0)
                {
                    partCount++;
                }

                // 新建一个 List 保存每个分块上传后的 ETag 和 PartNumber
                var partETags = new List<PartETag>();
                //upload the file
                using (var fs = new FileStream(partFile.FullName, FileMode.Open))
                {
                    for (var i = 0; i < partCount; i++)
                    {
                        // 跳到每个分块的开头
                        long skipBytes = partSize * i;
                        fs.Position = skipBytes;

                        // 计算每个分块的大小
                        var size = partSize < partFile.Length - skipBytes ? partSize : partFile.Length - skipBytes;

                        // 创建 UploadPartRequest，上传分块
                        var uploadPartRequest = new UploadPartRequest(bucket, key, initResult.UploadId)
                        {
                            Content = fs,
                            PartSize = size,
                            PartNumber = (i + 1)
                        };

                        var uploadPartResult = sNosClient.UploadPart(uploadPartRequest);

                        // 将返回的 PartETag 保存到 List 中。
                        partETags.Add(uploadPartResult.PartETag);
                    }

                    /*
           * 完成分块上传事件
           * */
                    var completeRequest = new CompleteMultipartUploadRequest(bucket, key, initResult.UploadId);
                    foreach (var partETag in partETags)
                    {
                        completeRequest.PartETags.Add(partETag);
                    }
                    sNosClient.CompleteMultipartUpload(completeRequest);
                    fs.Close();
                }
                Debug.LogFormat("Upload Part succeeded");
            }
            catch (NosException ex)
            {
                string str = string.Format("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}", ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
                Debug.Log(str);
                EditorUtility.DisplayDialog("上传异常", str, "确认");
            }
            catch (Exception ex)
            {
                string str = string.Format("Failed with error info: {0}", ex.Message);
                Debug.Log(str);
                EditorUtility.DisplayDialog("上传异常", str, "确认");
            }
        }

        /// <summary>
        /// 取消分块上传
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="uploadId"></param>
        public void AbortMultipartUpload(string bucket, string key, string uploadId)
        {
            try
            {
                var abortRequest = new AbortMultipartUploadRequest(bucket, key, uploadId);
                sNosClient.AbortMultipartUpload(abortRequest);
                Debug.LogFormat("Abort Multipart Upload succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        // <summary>
        /// 列出已上传的分块
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <param name="key">对象名</param>
        /// <param name="uploadId">分块上传的 ID</param>
        public void ListParts(string bucket, string key, string uploadId)
        {
            try
            {
                var listPartsRequest = new ListPartsRequest(bucket, key, uploadId);
                var result = sNosClient.ListParts(listPartsRequest);
                Debug.LogFormat("List parts succeeded");

                foreach (var part in result.Parts)
                {
                    Debug.LogFormat(part.ToString());
                }
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        // <summary>
        /// 查看当前正在进行的分片上传任务
        /// </summary>
        /// <param name="bucket">桶名</param>
        public void ListMultipartUploads(string bucket)
        {
            try
            {
                var listMultipartUploadsRequest = new ListMultipartUploadsRequest(bucket)
                {
                    KeyMarker = "",
                    MaxUploads = 10
                };
                var result = sNosClient.ListMultipartUploads(listMultipartUploadsRequest);

                Debug.LogFormat("List multi Uploads succeeded");
                foreach (var mu in result.MultipartUploads)
                {
                    Debug.LogFormat("Key:{0}, UploadId:{1}", mu.Key, mu.UploadId);
                }
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }
        #endregion
        /// <summary>
        /// 下载文件到内存
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        #region 下载
        public void GetObject(string bucket, string key)
        {
            try
            {
                var result = sNosClient.GetObject(bucket, key);
                //Debug.LogFormat(result.Content);
                Debug.LogFormat("Get object succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <param name="bucket">对象名</param>
        /// <param name="bucket">下载到的本地文件</param>
        public void GetObject(string bucket, string key, string fileToDownload)
        {
            try
            {
                sNosClient.GetObject(bucket, key, fileToDownload);
                Debug.LogFormat("Get object succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }
        #endregion

        #region 文件管理
        /// <summary>
        /// 列出指定存储空间下的文件的摘要信息
        /// </summary>
        /// <param name="bucket">桶名</param>
        public void ListObjects(string bucket)
        {
            try
            {
                var keys = new List<string>();
                ObjectListing result = null;
                string nextMarker = string.Empty;
                do
                {
                    var listObjectsRequest = new ListObjectsRequest(bucket)
                    {
                        Marker = nextMarker,
                        MaxKeys = 100
                    };
                    result = sNosClient.ListObjects(listObjectsRequest);

                    foreach (var summary in result.ObjectSummarise)
                    {
                        Debug.LogFormat(summary.Key);
                        keys.Add(summary.Key);
                    }

                    nextMarker = result.NextMarker;
                } while (result.IsTruncated);

                Debug.LogFormat("List objects of bucket:{0} succeeded ", bucket);
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 判断指定桶内是否存在指定文件
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <param name="key">对象名</param>
        /// 默认不能对目录进行判断。
        ///判断目录内文件是否存在时参数 key 格式为 "dir1/file1"。
        public void DoesObjectExist(string bucket, string key)
        {
            try
            {
                var exist = sNosClient.DoesObjectExist(bucket, key);
                Debug.LogFormat("exist ? " + exist);
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <param name="key">对象名</param>
        /// 
        /// 参数 key 书写格式：
        ///key 为空目录时格式为 "dir1/"。
        ///key 为目录内文件时格式为 "dir1/file1"。
        public void DeleteObject(string bucket, string key)
        {
            try
            {
                sNosClient.DeleteObject(bucket, key);
                Debug.LogFormat("Delete object succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 删除多个文件
        /// </summary>
        /// <param name="bucket">桶名</param>
        public void DeleteObjects(string bucket)
        {
            try
            {
                var keys = new List<string>();
                var listObjectsResult = sNosClient.ListObjects(bucket);
                foreach (var summary in listObjectsResult.ObjectSummarise)
                {
                    keys.Add(summary.Key);
                }
                var result = sNosClient.DeleteObjects(bucket, keys, false);

                Debug.LogFormat("Delete objects succeeded");

                if (result.Keys != null)
                {
                    foreach (var deletedObject in result.Keys)
                    {
                        Console.Write("\n[Deleted]: " + deletedObject.Key);
                    }
                }
                if (result.Error != null)
                {
                    foreach (var deleteError in result.Error)
                    {
                        Console.Write("\n[Error]: " + deleteError.Key + "\t" + deleteError.Code + "\t" + deleteError.Message);
                    }
                }
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sourceBucket">源桶名</param>
        /// <param name="sourceKey">源对象名</param>
        /// <param name="targetBucket">目标桶名</param>
        /// <param name="targetKey">目标对象名</param>
        public void CopyObject(string sourceBucket, string sourceKey, string targetBucket, string targetKey)
        {
            try
            {
                var request = new CopyObjectRequest(sourceBucket, sourceKey, targetBucket, targetKey);
                sNosClient.CopyObject(request);
                Debug.LogFormat("Copy object succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceBucket">源桶名</param>
        /// <param name="sourceKey">源对象名</param>
        /// <param name="targetBucket">目标桶名</param>
        /// <param name="targetKey">目标对象名</param>
        /// 暂时不支持跨桶的文件 move
        public void MoveObject(string sourceBucket, string sourceKey, string targetBucket, string targetKey)
        {
            try
            {
                var request = new MoveObjectRequest(sourceBucket, sourceKey, targetBucket, targetKey);
                sNosClient.MoveObject(request);
                Debug.LogFormat("Move object succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 获取文件的文件元信息
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <param name="key">对象名</param>
        public void GetObjectMetadata(string bucket, string key)
        {
            try
            {
                var objectMetadata = sNosClient.GetObjectMetadata(bucket, key);
                Debug.LogFormat("Get Object Metadata succeeded");
            }
            catch (NosException ex)
            {
                Debug.LogFormat("Failed with HTTPStatus: {0}; \nErrorCode: {1}; \nErrorMessage: {2}; \nRequestID:{3}; \nResource:{4}",
                    ex.StatusCode, ex.ErrorCode, ex.Message, ex.RequestId, ex.Resource);
            }
            catch (Exception ex)
            {
                Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }
        #endregion
    }
}
#endif