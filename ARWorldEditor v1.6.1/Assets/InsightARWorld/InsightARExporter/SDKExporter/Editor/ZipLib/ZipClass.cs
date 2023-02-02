using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

class ZipClass
{
    private int compressionLevel = 9;
    private byte[] buffer = new byte[2048];

    #region 单例模式

    private static ZipClass instance;
    private static readonly object SynObject = new object();

    public static ZipClass Instance
    {
        get
        {
            lock (SynObject)
            {
                if (instance == null)
                {
                    instance = new ZipClass();
                }
                return instance;
            }
        }
    }

    #endregion

    //public ZipClass()
    //{
    //}

    ///// <summary>
    ///// 构造函数
    ///// </summary>
    ///// <param name="bufferSize">缓冲区大小</param>
    ///// <param name="compressionLevel">压缩率：0-9</param>
    //public ZipClass(int bufferSize, int compressionLevel)
    //{
    //    buffer = new byte[bufferSize];
    //    this.compressionLevel = compressionLevel;
    //}

    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="fileToZip">要压缩的文件路径</param>
    /// <param name="zipedFile">压缩后的文件路径</param>
    public void ZipFile(string fileToZip, string zipedFile)
    {
        if (!File.Exists(fileToZip))
        {
            throw new FileNotFoundException("The specified file " + fileToZip + " could not be found.");
        }
        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile)))
        {
            string fileName = Path.GetFileName(fileToZip);
            ZipEntry zipEntry = new ZipEntry(fileName);
            zipStream.PutNextEntry(zipEntry);
            zipStream.SetLevel(compressionLevel);
            using (FileStream streamToZip = new FileStream(fileToZip, FileMode.Open, FileAccess.Read))
            {
                int size = streamToZip.Read(buffer, 0, buffer.Length);
                zipStream.Write(buffer, 0, size);
                while (size < streamToZip.Length)
                {
                    int sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
        }
    }

    /// <summary>
    /// 得到目录下的所有文件
    /// </summary>
    /// <param name="directory">目录</param>
    /// <returns></returns>
    public ArrayList GetFileList(string directory)
    {
        ArrayList fileList = new ArrayList();
        bool isEmpty = true;
        foreach (string file in Directory.GetFiles(directory))
        {
            fileList.Add(file);
            isEmpty = false;
        }
        if (isEmpty)
        {
            if (Directory.GetDirectories(directory).Length == 0)
            {
                fileList.Add(directory + @"/");
            }
        }
        foreach (string dirs in Directory.GetDirectories(directory))
        {
            foreach (object obj in GetFileList(dirs))
            {
                fileList.Add(obj);
            }
        }
        return fileList;
    }

    /// <summary>
    /// 压缩文件夹
    /// </summary>
    /// <param name="directoryToZip">要压缩的文件夹路径</param>
    /// <param name="zipedDirectory">压缩后的文件路径</param>
    /// <param name="onlyZipSub">是否只压缩子目录和文件（压缩包中将不包含被压缩目录本身）</param>
    public void ZipDirectory(string directoryToZip, string zipFileDirectory, bool onlyZipSub = false)
    {
        // If file exists, it will be overwrited.
        FileStream zipFileStream = File.Create(zipFileDirectory);
        // Get the list first in case of including the zip file.
        Hashtable fileList = getAllFiles(directoryToZip);  
        
        using (ZipOutputStream zipoutputstream = new ZipOutputStream(zipFileStream))
        {  
//            zipoutputstream.SetLevel(CompressionLevel); 
           // Crc32 crc = new Crc32();  
            foreach (DictionaryEntry item in fileList)
            {  
                FileStream fs = File.OpenRead(item.Key.ToString());  
                byte[] buffer = new byte[fs.Length];  
                fs.Read(buffer, 0, buffer.Length);  
                ZipEntry entry = new ZipEntry(item.Key.ToString().Substring((onlyZipSub ? new DirectoryInfo(directoryToZip) : Directory.GetParent(directoryToZip)).FullName.Length + 1)); 
                entry.IsUnicodeText = true;
//                entry.DateTime = (DateTime)item.Value;  
                entry.Size = fs.Length;  
                fs.Close();  
             //   crc.Reset();  
              //  crc.Update(buffer);  
              //  entry.Crc = crc.Value;  
                zipoutputstream.PutNextEntry(entry);  
                zipoutputstream.Write(buffer, 0, buffer.Length);  
            }  
        } 

//        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedDirectory)))
//        {
//            ArrayList fileList = GetFileList(directoryToZip);
//            int directoryNameLength = (Directory.GetParent(directoryToZip)).ToString().Length+1;
//            zipStream.SetLevel(compressionLevel);
//            ZipEntry zipEntry = null;
//            FileStream fileStream = null;
//            foreach (string fileName in fileList)
//            {
//                zipEntry = new ZipEntry(fileName.Remove(0, directoryNameLength));
//                zipStream.PutNextEntry(zipEntry);
//                if (!fileName.EndsWith(@"/"))
//                {
//                    fileStream = File.OpenRead(fileName);
//                    int unReadBytesCount = (int)fileStream.Length;
//                    while (unReadBytesCount > 0)
//                    {
//                        int readCount = fileStream.Read(buffer, 0, buffer.Length);
//                        unReadBytesCount -= readCount;
//                        zipStream.Write(buffer, 0, readCount);
//                    }
//                    fileStream.Close();
//                }
//            }
//        }
    }

    public void ZipDirectory(string directoryToZip, FileStream zipFileStream)
    {
        // Get the list first in case of including the zip file.
        Hashtable fileList = getAllFiles(directoryToZip);  
        using (ZipOutputStream zipoutputstream = new ZipOutputStream(zipFileStream))
        {  
//            zipoutputstream.SetLevel(CompressionLevel); 
            //Crc32 crc = new Crc32();  
            foreach (DictionaryEntry item in fileList)
            {  
                FileStream fs = File.OpenRead(item.Key.ToString());  
                byte[] buffer = new byte[fs.Length];  
                fs.Read(buffer, 0, buffer.Length);  
                ZipEntry entry = new ZipEntry(item.Key.ToString().Substring((Directory.GetParent(directoryToZip)).FullName.Length + 1)); 
                entry.IsUnicodeText = true;
//                entry.DateTime = (DateTime)item.Value;  
                entry.Size = fs.Length;  
                fs.Close();  
             //   crc.Reset();  
              //  crc.Update(buffer);  
              //  entry.Crc = crc.Value;  
                zipoutputstream.PutNextEntry(entry);  
                zipoutputstream.Write(buffer, 0, buffer.Length);  
            }  
        } 
    }

    /// <summary>  
    /// 获取所有文件  
    /// </summary>  
    /// <returns></returns>  
    private Hashtable getAllFiles(string dir)
    {  
        Hashtable FilesList = new Hashtable();  
        DirectoryInfo fileDire = new DirectoryInfo(dir);  
        if (!fileDire.Exists)
        {  
            throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");  
        }  

        this.getAllDirFiles(fileDire, FilesList);  
        this.getAllDirsFiles(fileDire.GetDirectories(), FilesList);  
        return FilesList;  
    }

    /// <summary>  
    /// 获取一个文件夹下的所有文件夹里的文件  
    /// </summary>  
    /// <param name="dirs"></param>  
    /// <param name="filesList"></param>  
    private void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)
    {  
        foreach (DirectoryInfo dir in dirs)
        {  
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {  
                filesList.Add(file.FullName.Replace('\\','/'), file.LastWriteTime);  
            }  
            this.getAllDirsFiles(dir.GetDirectories(), filesList);  
        }  
    }

    /// <summary>  
    /// 获取一个文件夹下的文件  
    /// </summary>  
    /// <param name="strDirName">目录名称</param>  
    /// <param name="filesList">文件列表HastTable</param>  
    private void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)
    {  
        foreach (FileInfo file in dir.GetFiles("*.*"))
        {  
            filesList.Add(file.FullName.Replace('\\','/'), file.LastWriteTime);  
        }  
    }
}