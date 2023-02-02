using UnityEngine;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

class UnZipClass
{
    private byte[] buffer = new byte[2048];

    #region 单例模式
    private static UnZipClass instance;
    private static readonly object SynObject = new object();

    public static UnZipClass Instance
    {
        get
        {
            lock (SynObject)
            {
                if (instance == null)
                {
                    instance = new UnZipClass();
                }
                return instance;
            }
        }
    }
    #endregion
    ///

    /// 缓冲区大小
    public UnZipClass(int bufferSize)
    {
        buffer = new byte[bufferSize];
    }
        
    private UnZipClass()
    {
    }

    /// <summary>  
    /// 功能：解压zip格式的文件。  
    /// </summary>  
    /// <param name="zipFilePath">压缩文件路径</param>  
    /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>  
    /// <param name="err">出错信息</param>  
    /// <returns>根目录名</returns>  
    public string UnZip(string zipFilePath, string unZipDir)
    {  
        string rootName = "";
        if (zipFilePath == string.Empty)
        {  
			UnityEditor.EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE
				, "压缩文件名为空！"
				, "OK!");
        }  
        if (!File.Exists(zipFilePath))
        {  
			UnityEditor.EditorUtility.DisplayDialog(RenderEngine.ExporterConfig.TITLE
				, "压缩文件 " + zipFilePath + " 不存在。"
				, "OK!");
        }  
        //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹  
        if (unZipDir == string.Empty)
            unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));  
        if (!unZipDir.EndsWith("/"))
            unZipDir += "/";  
        if (!Directory.Exists(unZipDir))
            Directory.CreateDirectory(unZipDir);  

        using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
        {  

            ZipEntry theEntry;  
            while ((theEntry = s.GetNextEntry()) != null)
            {  
                string nameProcessed = theEntry.Name.Replace(@"\", "/");
                string directoryName = Path.GetDirectoryName(nameProcessed);
                string fileName = Path.GetFileName(nameProcessed);
                if (string.IsNullOrEmpty(rootName))
                {
                    if (!string.IsNullOrEmpty(directoryName))
                    {
                        rootName = directoryName.Split('/')[0];
                        if (Directory.Exists(Path.Combine(unZipDir, rootName)))
                        {
                            Directory.Delete(Path.Combine(unZipDir, rootName), true);
                        }
                    }
                    else if (!string.IsNullOrEmpty(fileName))
                    {
                        rootName = fileName;
                        if (File.Exists(Path.Combine(unZipDir, rootName)))
                        {
                            File.Delete(Path.Combine(unZipDir, rootName));
                        }
                    }
                }
                if (directoryName.Length > 0)
                {  
                    Directory.CreateDirectory(unZipDir + directoryName);  
                }  
                if (!directoryName.EndsWith("/"))
                {
                    directoryName += "/";  
                }
                if (fileName != string.Empty)
                {  
                    using (FileStream streamWriter = File.Create(unZipDir + nameProcessed))
                    {  

                        int size = 2048;  
                        byte[] data = new byte[2048];  
                        while (true)
                        {  
                            size = s.Read(data, 0, data.Length);  
                            if (size > 0)
                            {  
                                streamWriter.Write(data, 0, size);  
                            }
                            else
                            {  
                                break;  
                            }  
                        }  
                    }  
                }  
            }  
        }  
        return rootName;
    }

    ///

    /// 解压缩文件
    ///

    /// 压缩文件路径
    /// 解压缩文件路径
    public void UnZipFile1(string zipFilePath, string unZipFilePath)
    {
        using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipFilePath)))
        {
            ZipEntry zipEntry = null;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(zipEntry.Name);
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (zipEntry.CompressedSize == 0)
                        break;
                    using (FileStream stream = File.Create(unZipFilePath + fileName))
                    {
                        while (true)
                        {
                            int size = zipStream.Read(buffer, 0, buffer.Length);
                            if (size > 0)
                            {
                                stream.Write(buffer, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    ///

    /// 解压缩目录
    ///

    /// 压缩目录路径
    /// 解压缩目录路径
    public string UnZipDirectory1(string zipDirectoryPath, string unZipDirecotyPath)
    {
        string rootName = "";
        using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipDirectoryPath)))
        {
            ZipEntry zipEntry = null;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                string directoryName = Path.GetDirectoryName(zipEntry.Name);
                string fileName = Path.GetFileName(zipEntry.Name);
                if (string.IsNullOrEmpty(rootName))
                {
                    if (!string.IsNullOrEmpty(directoryName))
                    {
                        rootName = directoryName;
                        if (Directory.Exists(Path.Combine(unZipDirecotyPath, rootName)))
                        {
                            Directory.Delete(Path.Combine(unZipDirecotyPath, rootName), true);
                        }
                    }
                    else if (!string.IsNullOrEmpty(fileName))
                    {
                        rootName = fileName;
                        if (File.Exists(Path.Combine(unZipDirecotyPath, rootName)))
                        {
                            File.Delete(Path.Combine(unZipDirecotyPath, rootName));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(directoryName))
                {
                    Directory.CreateDirectory(unZipDirecotyPath + directoryName);
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (zipEntry.CompressedSize == 0)
                        break;
                    if (zipEntry.IsDirectory)
                    {
                        directoryName = Path.GetDirectoryName(unZipDirecotyPath + zipEntry.Name);
                        Directory.CreateDirectory(directoryName);
                    }
                    using (FileStream stream = File.Create(unZipDirecotyPath + zipEntry.Name))
                    {
                        while (true)
                        {
                            int size = zipStream.Read(buffer, 0, buffer.Length);
                            if (size > 0)
                            {
                                stream.Write(buffer, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        return rootName;
    }
}