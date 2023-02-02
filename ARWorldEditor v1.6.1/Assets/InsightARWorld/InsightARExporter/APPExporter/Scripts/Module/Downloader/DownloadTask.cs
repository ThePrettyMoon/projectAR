using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARWorldEditor
{
    public delegate void DownloadErrorCallBack(DownloadTask downloadTask, int code, string msg);
    public delegate void DownloadProgressCallBack(DownloadTask downloadTask, long curSize, long allSize);
    public delegate void DownloadCompleteCallBack(DownloadTask downloadTask);

    public class DownloadTask
    {
        private string fileName; //下载的文件，作为标识，
        private string downUrl; //远程地址
        private string savePath; //本地地址
        private string tempPath; //临时缓存文件地址
        private long size; //文件长度，非必须
        private string md5; //需要校验的md5，非必须
        private bool isDelete; //用于清理正在下载的文件
        private bool isPaused = false;
        private bool isCanceled = false;

        public DownloadErrorCallBack errorFun;
        public DownloadProgressCallBack progressFun;
        public DownloadCompleteCallBack completeFun;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        public string DownloadUrl
        {
            get
            {
                return downUrl;
            }
            set
            {
                downUrl = value;
            }
        }

        public string SavePath
        {
            get
            {
                return savePath;
            }
            set
            {
                savePath = value;
            }
        }

        public string TempPath
        {
            get
            {
                return tempPath;
            }
            set
            {
                tempPath = value;
            }
        }

        public long Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public bool IsDelete
        {
            get
            {
                return isDelete;
            }
            set
            {
                isDelete = value;
            }
        }

        public string MD5
        {
            get
            {
                return md5;
            }
            set
            {
                md5 = value;
            }
        }

        public bool Canceled
        {
            get
            {
                return isCanceled;
            }
            set
            {
                isCanceled = true;
            }
        }

        public bool Paused
        {
            get
            {
                return isPaused;
            }
            set
            {
                isPaused = true;
            }
        }

        public DownloadTask()
        {
        }

        /// <summary>
        /// add listener
        /// </summary>
        /// <param name="onComplete"></param>
        /// <param name="onProgress"></param>
        /// <param name="onError"></param>
        public void AddListener(DownloadCompleteCallBack onComplete, DownloadProgressCallBack onProgress, DownloadErrorCallBack onError)
        {
            this.completeFun = onComplete;
            this.progressFun = onProgress;
            this.errorFun = onError;
        }
    }
}