namespace ARWorldEditor
{
    /// <summary>
    /// 文件下载状态
    /// </summary>
    public enum FileDownloadState
    {
        None,
        ResetSize,
        Downloading,
        CheckMD5,
        Unziping,
        Complete,
        Error,
        Pause,
        Cancel,
    }
}
