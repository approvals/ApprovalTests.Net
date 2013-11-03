using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ApprovalUtilities.Utilities
{
	public static class PathUtilities
	{
		public static string GetDirectoryForCaller()
		{
			return GetDirectoryForCaller(1);
		}


		public static string GetDirectoryForCaller(int callerStackDepth)
		{
			var stackFrame = new StackTrace(true).GetFrame(callerStackDepth + 1);
			return GetDirectoryForStackFrame(stackFrame);
		}

		public static string GetDirectoryForStackFrame(StackFrame stackFrame)
		{
			return new FileInfo(stackFrame.GetFileName()).Directory.FullName + "\\";
		}

		public static string ScrubPath(this string text, string path)
		{
			return text == null ? null : text.Replace(path, @"...\");
		}

		public static string GetAdjacentFile(string fileName)
		{
			return GetDirectoryForCaller(1) + fileName;
		}

		public static string[] LocateFileFromEnviormentPath(string toFind)
		{
			string processName = @"C:\Windows\System32\where.exe";
			if (!File.Exists(processName))
			{
                // report the actual error so the developer can do something about it. And no more TypeInit & TypeInvoke exceptions :)
                return new string[] { "'where.exe' does not exist, automated searching for your diff tool will not work" };
			}
			return
				GetOutputFromProcess(processName, toFind).Split('\n').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).
					ToArray();
		}

		public static string GetOutputFromProcess(string processName, string arguments)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo
				{
					CreateNoWindow = true,
					RedirectStandardOutput = true,
					RedirectStandardInput = true,
					UseShellExecute = false,
					Arguments = arguments,
					FileName = processName
				};

			StringBuilder outputBuilder = new StringBuilder();
			Process process = new Process {StartInfo = processStartInfo, EnableRaisingEvents = true};
			process.OutputDataReceived += (sender, e) => outputBuilder.AppendLine(e.Data);
			process.Start();
			process.BeginOutputReadLine();
			process.WaitForExit();
			process.CancelOutputRead();

			// use the output
			return outputBuilder.ToString();
		}
	}
}