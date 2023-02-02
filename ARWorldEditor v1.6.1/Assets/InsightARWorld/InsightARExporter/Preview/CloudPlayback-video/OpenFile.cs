#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class OpenFile : MonoBehaviour
{
    public static string path = "";
    public static string OpenWinFolder(string tmpPath)
    {
        return EditorUtility.OpenFolderPanel("选择图片文件夹", tmpPath, "");
        //使用如下：
        OpenDialogDir ofn2 = new OpenDialogDir();
        ofn2.pszDisplayName = new string(new char[2000]);
        ofn2.lpszTitle = "Open Project";
        IntPtr pidlPtr = DllOpenFileDialog.SHBrowseForFolder(ofn2);

        char[] charArray = new char[2000];
        for (int i = 0; i < 2000; i++)
            charArray[i] = '\0';

        DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray);
        string fullDirPath = new String(charArray);
        fullDirPath = fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));
        Debug.Log(fullDirPath);
        return fullDirPath;
    }
    public static string OpenWinFile(string title)
    {
        OpenDialogFile ofn = new OpenDialogFile();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "All Files\0*.*\0\0";// "All Files\0*.*\0\0"; 所有格式均显示 
                                            //ofn.filter = "All Files\0*.*\0\0";
        ofn.file = new string(new char[256]);      // 存放目录路径缓冲区  
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = Application.dataPath;        //默认路径  
        ofn.title = title;       //窗口名称
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | /*0x00000200 |*/ 0x00000008;   //OFN_EXPLORER |OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR    
        DllOpenFileDialog.GetOpenFileName(ofn);
        Debug.Log(ofn.file);
        return ofn.file;
        //if (DllOpenFileDialog.GetOpenFileName(ofn))
        //{
        //    //多选时 Path为父文件夹路径，单选时为文件路径
            
        //}
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenDialogFile
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenDialogDir
{
    public IntPtr hwndOwner = IntPtr.Zero;
    public IntPtr pidlRoot = IntPtr.Zero;
    public String pszDisplayName = null;
    public String lpszTitle = null;
    public UInt32 ulFlags = 0;
    public IntPtr lpfn = IntPtr.Zero;
    public IntPtr lParam = IntPtr.Zero;
    public int iImage = 0;
}

public class DllOpenFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenDialogFile ofn);

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenDialogFile ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern IntPtr SHBrowseForFolder([In, Out] OpenDialogDir ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

}

#endif