using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

		public static IEnumerable<string> LocateFileFromEnviormentPath(string toFind)
		{
			var results = new List<string>();
			if (File.Exists(toFind))
			{
				results.Add(Path.GetFullPath(toFind));
			}
			var values = Environment.GetEnvironmentVariable("PATH");
			var found = values.Split(Path.PathSeparator).Select(path => Path.Combine(path, toFind)).Where(File.Exists);
			results.AddRange(found);
			return results.ToArray();
		}
	}
}