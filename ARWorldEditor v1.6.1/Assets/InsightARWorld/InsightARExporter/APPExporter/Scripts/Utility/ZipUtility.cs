using System.IO.Compression;
using System.Text;

namespace ARWorldEditor
{
    /// <summary>
    /// 文件解压缩
    /// </summary>
    public class ZipUtility 
    {
        /// <summary>
        /// unzip
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="unzipDirectory"></param>
        public static void Unzip(string zipPath,string unzipDirectory)
        {
            ZipFile.ExtractToDirectory(zipPath, unzipDirectory, Encoding.GetEncoding("gbk"));
        }

        /// <summary>
        /// zip
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="destArchivePath"></param>
        public static void Zip(string sourceDirectory,string destArchivePath)
        {
            ZipFile.CreateFromDirectory(sourceDirectory, destArchivePath);
        }
    }
}
