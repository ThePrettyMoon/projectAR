using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Compression;

/// <summary>
/// 文件解压缩
/// </summary>
public class UnzipFile 
{
    /// <summary>
    /// unzip
    /// </summary>
    /// <param name="zipPath"></param>
    /// <param name="unzipDirectory"></param>
    public static void Unzip(string zipPath,string unzipDirectory)
    {
        ZipFile.ExtractToDirectory(zipPath, unzipDirectory);
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
