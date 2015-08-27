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
			return new FileInfo(stackFrame.GetFileName()).Directory.FullName + Path.DirectorySeparatorChar;
		}

		public static string ScrubPath(this string text, string path)
		{
			return text == null ? null : text.Replace(path, @"..." + Path.DirectorySeparatorChar);
		}

		public static string GetAdjacentFile(string relativePath)
		{
			return GetDirectoryForCaller(1) + relativePath;
		}

		public static IEnumerable<string> LocateFileFromEnviormentPath(string toFind)
		{
			var results = new List<string>();
			if (File.Exists(toFind))
			{
				results.Add(Path.GetFullPath(toFind));
			}

#if __MonoCS__
			if (toFind.IndexOf (".exe") >= 0)
			{
				var trimmedToFind = toFind.Substring(0, toFind.Length - 4);
				results.AddRange(FindProgramOnPath(trimmedToFind));
			}
#endif
			results.AddRange(FindProgramOnPath(toFind));
			return results.ToArray();
		}

		static IList<string> EnvironmentPaths;

		private static IEnumerable<string> FindProgramOnPath(string programName)
		{
			if (EnvironmentPaths == null) {
				EnvironmentPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
#if __MonoCS__
				// not sure why this path is not included in the environment variables
				// but couldn't find find p4merge without it.
				EnvironmentPaths.Add("/usr/local/bin");
#endif
			}
			return EnvironmentPaths.Select(path => Path.Combine(path, programName)).Where(File.Exists);
		}
	}
}