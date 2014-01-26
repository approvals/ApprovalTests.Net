using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace ApprovalTests.Reporters
{
	public class VisualStudioProjectFileAdder
	{
		public static void IncludeFileInCurrentProject(string approved)
		{
			var p = new Project(GetCurrentProjectFile(approved));
			if (!p.Items.Any(i => approved.EndsWith(i.UnevaluatedInclude)))
			{
				p.AddItem("Content", approved);
				p.Save();
			}
		}

		public static string GetCurrentProjectFile(string file)
		{
			var dir = Path.GetDirectoryName(file);
			if (dir == null)
			{
				return null;
			}
			var csproj = new DirectoryInfo(dir).EnumerateFiles("*.csproj").FirstOrDefault();
			if (csproj != null)
			{
				return csproj.FullName;
			}
			return GetCurrentProjectFile(dir);
		}
	}
}