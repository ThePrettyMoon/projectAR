using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    ///  data类，处理state等信息
    /// </summary>
    public class BaseDbData
    {
        //默认需要下载
        private ProductState mState = ProductState.STATE_NEED_DOWNLOAD;
        private UnZipState mUnzipState;
        private DownloadState mDownloadState;
        private int mDownloadProgress;
        private string mDownloadPath;
        private string mUnzipPath;

        /// <summary>
        /// is valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return mUnzipState == UnZipState.FINISHED;
        }

        /// <summary>
        /// 返回产品state
        /// </summary>
        /// <returns></returns>
        public ProductState State
        {
            get
            {
                return mState;
            }
            set
            {
                mState = value;
            }
        }

        /// <summary>
        /// download state
        /// </summary>
        public DownloadState DownloadState
        {
            get
            {
                return mDownloadState;
            }
            set
            {
                mDownloadState = value;
            }
        }

        /// <summary>
        /// 解压状态
        /// </summary>
        public UnZipState UnzipState
        {
            get
            {
                return mUnzipState;
            }
            set
            {
                mUnzipState = value;
            }
        }

        /// <summary>
        /// 下载进度
        /// </summary>
        public int DownloadProgress
        {
            get
            {
                return mDownloadProgress;
            }
            set
            {
                mDownloadProgress = value;
            }
        }


        /// <summary>
        /// 下载路径
        /// </summary>
        public string DownloadPath
        {
            get
            {
                return mDownloadPath;
            }
            set
            {
                mDownloadPath = value;
            }
        }

        /// <summary>
        /// 解压路径
        /// </summary>
        public string UnzipPath
        {
            get
            {
                return mUnzipPath;
            }
            set
            {
                mUnzipPath = value;
            }
        }
    }
}
