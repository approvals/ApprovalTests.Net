using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApprovalTests.Reporters
{
	public class DotNet4Utilities
	{
		public static string GetFirstWorking(string path, string[] paths)
		{
			string fullPath = path;
			foreach (var p in paths)
			{
				fullPath = p + System.IO.Path.DirectorySeparatorChar + path;
				if (File.Exists(fullPath))
				{
					break;
				}
			}

			return fullPath;
		}

		public static string GetPathInProgramFilesX86(string path)
		{
			string[] paths = GetProgramFilesPaths();
			return GetFirstWorking(path, paths);
		}

		public static string[] GetProgramFilesPaths()
		{
			var paths = new HashSet<string>();
			paths.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)"));
			paths.Add(Environment.GetEnvironmentVariable("ProgramFiles"));
			paths.Add(Environment.GetEnvironmentVariable("ProgramW6432"));
			return paths.Where(p => p != null).ToArray();
		}
	}
}