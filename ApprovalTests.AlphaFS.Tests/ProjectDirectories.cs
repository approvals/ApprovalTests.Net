using System;
using System.Reflection;
using Alphaleonis.Win32.Filesystem;

namespace ApprovalTests.AlphaFS.Tests
{
	public static class ProjectDirectories
	{
		public static string AlphaFSTestDirectoryPath = alphaFSTestDirectoryPath ?? (alphaFSTestDirectoryPath = Path.Combine(GetSolutionDirectory(), "ApprovalTests.AlphaFS.Tests"));
		
		private static string GetSolutionDirectory()
		{
			var assemblyDirectory = Path.GetDirectoryName(Path.GetFullPath(Assembly.GetExecutingAssembly().Location));
			var result = FindSolutionDirectoryStartingFrom(assemblyDirectory);
			if (result == null)
				result = FindSolutionDirectoryStartingFrom(Environment.CurrentDirectory);
			if (result == null)
			{
				throw new Exception($"Не получилось найти директорию солюшена. Текущая директория {Environment.CurrentDirectory}. Директория со сборкой {assemblyDirectory}");
			}
			return result;
		}

		private static string FindSolutionDirectoryStartingFrom(string directory)
		{
			while(Path.GetPathRoot(directory) != directory)
			{
				if (File.Exists(Path.Combine(directory, solutionName)))
					return directory;
				directory = Path.GetDirectoryName(directory);
			}
			return null;
		}

		private static string alphaFSTestDirectoryPath;
		private const string solutionName = "ApprovalTests.sln";
	}
}