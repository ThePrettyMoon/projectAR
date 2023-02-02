using System.IO;

namespace ARWorldEditor
{
    public static class FileUtility
    {
        /// <summary>
        /// 返回目录下文件个数
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static int GetDirectorySubFileCount(string directoryPath)
        {
            return Directory.GetFiles(directoryPath).Length;
        }

        /// <summary>
        /// delete directory
        /// </summary>
        /// <param name="path"></param>
        public static void DirectoryDeleteRF(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (null != directoryInfo && directoryInfo.Exists)
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
                directoryInfo.Delete(true);
            }
        }

        /// <summary>
        /// 返回文件长度
        /// </summary>
        public static long GetFileLength(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Length;
        }
    }
}
