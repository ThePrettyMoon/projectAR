/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace RenderEngine
{
	class UtilityExecute
	{
		public static string GetPlatformName()
		{
			if( UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor ) return "windows";
			else if( UnityEngine.Application.platform == UnityEngine.RuntimePlatform.OSXEditor ) return "darwin";
			//else if( Application.platform == RuntimePlatform.LinuxEditor ) return "linux";
			return "unknown";
		}

		public static string GetShellName()
		{
			if( UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor ) return "cmd";
			else if( UnityEngine.Application.platform == UnityEngine.RuntimePlatform.OSXEditor ) return "bash";
			//else if( Application.platform == RuntimePlatform.LinuxEditor ) return "bash";
			return "unknown";
		}

		public static string GetExecuteExtension()
		{
			if( UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor ) return ".exe";
			else if( UnityEngine.Application.platform == UnityEngine.RuntimePlatform.OSXEditor ) return "";
			return "";
		}
			
	public static string ExecuteTool( string tool , string argument, bool wait = true )
	{
		return ExecuteCmd(Path.Combine(ExporterConfig.EXPORTER_DIRECTORY, "tools", GetPlatformName(), tool), argument, wait);
		// return ExecuteCmd("Tools/" + GetPlatformName() + "/" + tool , argument , wait );
	}

	// public static string ExecuteThirdTool( string tool, string argument )
	// {
	// 	return ExecuteTool()
	// }

	public static string ExecuteCmd( string command , string argument , bool wait )
	{
		if( '\\' == Path.DirectorySeparatorChar )
			command = command.Replace( "/" , "\\" );
		else
			command = command.Replace( "\\" , "/" );
		System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = UnityEngine.Application.dataPath + "/../"+command + GetExecuteExtension();
		proc.StartInfo.Arguments = argument; 
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.StartInfo.CreateNoWindow = true;
		proc.Start();
		
		StringBuilder ret = new StringBuilder();
		if (wait)
		{
			while( !proc.HasExited )
			{
				//ret.Append( proc.StandardOutput.ReadToEnd() );
				ret.Append( proc.StandardError.ReadToEnd() );
			}
			proc.WaitForExit();
		}
		return ret.ToString();
	}
	}
}